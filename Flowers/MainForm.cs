using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;
using Flowers.Data;
using Flowers.Models;

namespace Flowers
{
    public partial class MainForm : Form
    {
        private DataTable _productsTable;

        public MainForm()
        {
            InitializeComponent();
            lblUserInfo.Text = $"{CurrentUser.FullName} ({CurrentUser.RoleName})";
            btnAdminPanel.Visible = CurrentUser.IsAtLeastManager();
            LoadProducts();
        }

        private void LoadProducts(string search = null, string sort = null)
        {
            string sql = @"SELECT Id, Name, Description, Price, OldPrice, DiscountPercent, ImagePath
                           FROM Products
                           WHERE IsActive = 1";

            var parameters = new System.Collections.Generic.List<SqlParameter>();

            if (!string.IsNullOrWhiteSpace(search))
            {
                sql += " AND (Name LIKE @search OR Description LIKE @search)";
                parameters.Add(new SqlParameter("@search", "%" + search + "%"));
            }

            if (!string.IsNullOrWhiteSpace(sort))
            {
                sql += " ORDER BY " + sort;
            }

            _productsTable = Database.ExecuteQuery(sql, parameters.ToArray());
            RenderCatalog();
        }

        private void RenderCatalog()
        {
            flowCatalog.SuspendLayout();
            flowCatalog.Controls.Clear();

            if (_productsTable == null)
            {
                flowCatalog.ResumeLayout();
                return;
            }

            foreach (DataRow row in _productsTable.Rows)
            {
                var card = CreateProductCard(row);
                flowCatalog.Controls.Add(card);
            }

            flowCatalog.ResumeLayout();
        }

        private Control CreateProductCard(DataRow row)
        {
            int id = (int)row["Id"];
            string name = row["Name"].ToString();
            string desc = row["Description"].ToString();
            decimal price = Convert.ToDecimal(row["Price"]);
            decimal? oldPrice = row["OldPrice"] != DBNull.Value ? (decimal?)Convert.ToDecimal(row["OldPrice"]) : null;
            int? discount = row["DiscountPercent"] != DBNull.Value ? (int?)Convert.ToInt32(row["DiscountPercent"]) : null;
            string imagePath = row["ImagePath"] != DBNull.Value ? row["ImagePath"].ToString() : null;

            var panel = new Panel();
            panel.Width = 220;
            panel.Height = 260;
            panel.Margin = new Padding(10);
            panel.BackColor = Color.White;
            panel.BorderStyle = BorderStyle.FixedSingle;

            var pic = new PictureBox();
            pic.Width = 200;
            pic.Height = 120;
            pic.Left = 10;
            pic.Top = 10;
            pic.SizeMode = PictureBoxSizeMode.Zoom;

            // Заглушка на случай отсутствия/ошибки загрузки картинки
            pic.Image = ImagePlaceholder.Get();

            if (!string.IsNullOrEmpty(imagePath))
            {
                try
                {
                    string fullPath = System.IO.Path.Combine(Application.StartupPath, imagePath);
                    if (System.IO.File.Exists(fullPath))
                    {
                        pic.Image = Image.FromFile(fullPath);
                    }
                }
                catch
                {
                    // если картинка не загрузилась, оставляем заглушку
                }
            }

            var lblName = new Label();
            lblName.Text = name;
            lblName.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblName.AutoSize = false;
            lblName.Width = 200;
            lblName.Height = 32;
            lblName.Left = 10;
            lblName.Top = 135;

            var lblDesc = new Label();
            lblDesc.Text = desc;
            lblDesc.Font = new Font("Segoe UI", 8F);
            lblDesc.AutoSize = false;
            lblDesc.Width = 200;
            lblDesc.Height = 36;
            lblDesc.Left = 10;
            lblDesc.Top = 165;

            var lblPrice = new Label();
            lblPrice.Left = 10;
            lblPrice.Top = 205;
            lblPrice.AutoSize = true;

            string priceText = price.ToString("0.00") + " ₽";

            if (oldPrice.HasValue)
            {
                string oldText = oldPrice.Value.ToString("0.00") + " ₽";
                if (discount.HasValue)
                {
                    priceText += $"  (-{discount.Value}%)";
                }

                lblPrice.Text = priceText;

                var lblOld = new Label();
                lblOld.Text = oldText;
                lblOld.Font = new Font("Segoe UI", 8F, FontStyle.Strikeout);
                lblOld.ForeColor = Color.Gray;
                lblOld.AutoSize = true;
                lblOld.Left = 10;
                lblOld.Top = 220;
                panel.Controls.Add(lblOld);
            }
            else
            {
                lblPrice.Text = priceText;
            }

            if (price > 1000)
            {
                lblPrice.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
                lblPrice.ForeColor = Color.FromArgb(200, 0, 0);
            }
            else
            {
                lblPrice.Font = new Font("Segoe UI", 9F, FontStyle.Regular);
                lblPrice.ForeColor = Color.FromArgb(50, 50, 50);
            }

            var btn = new Button();
            btn.Text = "В корзину";
            btn.Width = 90;
            btn.Height = 28;
            btn.Left = panel.Width - btn.Width - 12;
            btn.Top = panel.Height - btn.Height - 10;
            btn.BackColor = Color.FromArgb(34, 139, 34);
            btn.ForeColor = Color.White;
            btn.FlatStyle = FlatStyle.Flat;
            btn.FlatAppearance.BorderSize = 0;
            btn.Tag = id;
            btn.Click += (s, e) =>
            {
                string img = imagePath;
                Cart.Instance.AddItem(id, name, price, 1, img);
                MessageBox.Show("Товар добавлен в корзину.", "Корзина", MessageBoxButtons.OK, MessageBoxIcon.Information);
            };

            panel.Controls.Add(pic);
            panel.Controls.Add(lblName);
            panel.Controls.Add(lblDesc);
            panel.Controls.Add(lblPrice);
            panel.Controls.Add(btn);

            return panel;
        }

        private void btnApplyFilter_Click(object sender, EventArgs e)
        {
            string search = txtSearch.Text.Trim();
            string sort = null;

            if (cmbSort.SelectedItem != null)
            {
                switch (cmbSort.SelectedItem.ToString())
                {
                    case "Цена по возрастанию":
                        sort = "Price ASC";
                        break;
                    case "Цена по убыванию":
                        sort = "Price DESC";
                        break;
                    case "Название (А-Я)":
                        sort = "Name ASC";
                        break;
                }
            }

            LoadProducts(search, sort);
        }

        private void btnAddToCart_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Добавляйте товары через кнопки на карточках.", "Подсказка",
                MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void btnViewCart_Click(object sender, EventArgs e)
        {
            using (var cartForm = new CartForm())
            {
                cartForm.ShowDialog();
            }
        }

        private void btnMyOrders_Click(object sender, EventArgs e)
        {
            using (var ordersForm = new OrdersForm())
            {
                ordersForm.ShowDialog();
            }
        }

        private void btnUserProfile_Click(object sender, EventArgs e)
        {
            using (var profileForm = new UserProfileForm())
            {
                profileForm.ShowDialog();
                lblUserInfo.Text = $"{CurrentUser.FullName} ({CurrentUser.RoleName})";
            }
        }

        private void btnAdminPanel_Click(object sender, EventArgs e)
        {
            if (!CurrentUser.IsAtLeastManager())
            {
                MessageBox.Show("Нет прав доступа.", "Доступ запрещен", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            using (var adminForm = new AdminPanelForm())
            {
                adminForm.ShowDialog();
                LoadProducts();
            }
        }
    }
}

