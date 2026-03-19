using System;
using System.Data.SqlClient;
using System.Windows.Forms;
using Flowers.Data;
using Flowers.Models;

namespace Flowers
{
    public partial class CartForm : Form
    {
        public CartForm()
        {
            InitializeComponent();
            LoadCart();
            dgvCart.CellFormatting += DgvCart_CellFormatting;
        }

        private void LoadCart()
        {
            dgvCart.DataSource = null;
            dgvCart.AutoGenerateColumns = true;
            dgvCart.DataSource = Cart.Instance.Items;

            // картинка товара
            if (dgvCart.Columns["Image"] == null)
            {
                var imgCol = new DataGridViewImageColumn();
                imgCol.Name = "Image";
                imgCol.HeaderText = "Фото";
                imgCol.ImageLayout = DataGridViewImageCellLayout.Zoom;
                dgvCart.Columns.Insert(0, imgCol);
            }

            dgvCart.RowTemplate.Height = 60;
            lblTotal.Text = "Итого: " + Cart.Instance.GetTotal().ToString("0.00");
        }

        private void DgvCart_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (dgvCart.Columns[e.ColumnIndex].Name == "Image")
            {
                var row = dgvCart.Rows[e.RowIndex];
                var item = row.DataBoundItem as CartItem;
                if (item != null && !string.IsNullOrEmpty(item.ImagePath))
                {
                    try
                    {
                        string fullPath = System.IO.Path.Combine(Application.StartupPath, item.ImagePath);
                        if (System.IO.File.Exists(fullPath))
                        {
                            e.Value = System.Drawing.Image.FromFile(fullPath);
                        }
                    }
                    catch
                    {
                        
                    }
                }
            }
        }

        private void btnRemoveItem_Click(object sender, EventArgs e)
        {
            if (dgvCart.SelectedRows.Count == 0) return;
            int productId = (int)dgvCart.SelectedRows[0].Cells["ProductId"].Value;
            Cart.Instance.RemoveItem(productId);
            LoadCart();
        }

        private void btnCheckout_Click(object sender, EventArgs e)
        {
            if (Cart.Instance.Items.Count == 0)
            {
                MessageBox.Show("Корзина пуста.", "Корзина", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            decimal total = Cart.Instance.GetTotal();

            using (var conn = Database.GetConnection())
            {
                conn.Open();
                using (var tran = conn.BeginTransaction())
                {
                    try
                    {
                        string insertOrder = @"
INSERT INTO Orders (UserId, TotalAmount)
VALUES (@UserId, @TotalAmount);
SELECT SCOPE_IDENTITY();";

                        int orderId;
                        using (var cmdOrder = new SqlCommand(insertOrder, conn, tran))
                        {
                            cmdOrder.Parameters.AddWithValue("@UserId", CurrentUser.Id);
                            cmdOrder.Parameters.AddWithValue("@TotalAmount", total);
                            orderId = Convert.ToInt32(cmdOrder.ExecuteScalar());
                        }

                        string insertItem = @"
INSERT INTO OrderItems (OrderId, ProductId, Quantity, UnitPrice)
VALUES (@OrderId, @ProductId, @Quantity, @UnitPrice);";

                        foreach (var item in Cart.Instance.Items)
                        {
                            using (var cmdItem = new SqlCommand(insertItem, conn, tran))
                            {
                                cmdItem.Parameters.AddWithValue("@OrderId", orderId);
                                cmdItem.Parameters.AddWithValue("@ProductId", item.ProductId);
                                cmdItem.Parameters.AddWithValue("@Quantity", item.Quantity);
                                cmdItem.Parameters.AddWithValue("@UnitPrice", item.UnitPrice);
                                cmdItem.ExecuteNonQuery();
                            }
                        }

                        tran.Commit();
                        Cart.Instance.Clear();
                        LoadCart();
                        MessageBox.Show("Заказ успешно оформлен!", "Заказ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    catch (Exception ex)
                    {
                        tran.Rollback();
                        MessageBox.Show("Ошибка при оформлении заказа: " + ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
        }

    }

}

