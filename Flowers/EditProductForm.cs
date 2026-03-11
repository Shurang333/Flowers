using System;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Windows.Forms;
using Flowers.Data;

namespace Flowers
{
    public partial class EditProductForm : Form
    {
        private int? productId;
        private string imagePathRelative;

        public EditProductForm(int? id = null)
        {
            InitializeComponent();
            productId = id;

            if (productId.HasValue)
            {
                LoadProduct();
            }
        }

        private void LoadProduct()
        {
            string sql = "SELECT * FROM Products WHERE Id = @Id";
            DataTable table = Database.ExecuteQuery(sql, new SqlParameter("@Id", productId.Value));
            if (table.Rows.Count == 0) return;

            var row = table.Rows[0];
            txtName.Text = row["Name"].ToString();
            txtDescription.Text = row["Description"].ToString();
            numPrice.Value = Convert.ToDecimal(row["Price"]);

            if (row["OldPrice"] != DBNull.Value)
                numOldPrice.Value = Convert.ToDecimal(row["OldPrice"]);

            if (row["DiscountPercent"] != DBNull.Value)
                numDiscount.Value = Convert.ToDecimal(row["DiscountPercent"]);

            chkIsActive.Checked = (bool)row["IsActive"];

            imagePathRelative = row["ImagePath"] as string;
            if (!string.IsNullOrEmpty(imagePathRelative))
            {
                string fullPath = Path.Combine(Application.StartupPath, imagePathRelative);
                if (File.Exists(fullPath))
                {
                    picImage.ImageLocation = fullPath;
                }
            }
        }

        private void btnBrowseImage_Click(object sender, EventArgs e)
        {
            using (var dlg = new OpenFileDialog())
            {
                dlg.Filter = "Изображения|*.jpg;*.jpeg;*.png;*.bmp";
                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    string imagesDir = Path.Combine(Application.StartupPath, "ProductImages");
                    if (!Directory.Exists(imagesDir))
                    {
                        Directory.CreateDirectory(imagesDir);
                    }

                    string fileName = Path.GetFileName(dlg.FileName);
                    string destPath = Path.Combine(imagesDir, fileName);
                    File.Copy(dlg.FileName, destPath, true);

                    imagePathRelative = Path.Combine("ProductImages", fileName);
                    picImage.ImageLocation = destPath;
                }
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtName.Text))
            {
                MessageBox.Show("Название обязательно.");
                return;
            }

            if (numPrice.Value <= 0)
            {
                MessageBox.Show("Цена должна быть больше нуля.");
                return;
            }

            string sql;
            if (productId.HasValue)
            {
                sql = @"
UPDATE Products
SET Name = @Name,
    Description = @Description,
    Price = @Price,
    OldPrice = @OldPrice,
    DiscountPercent = @DiscountPercent,
    ImagePath = @ImagePath,
    IsActive = @IsActive
WHERE Id = @Id";
            }
            else
            {
                sql = @"
INSERT INTO Products (Name, Description, Price, OldPrice, DiscountPercent, ImagePath, IsActive)
VALUES (@Name, @Description, @Price, @OldPrice, @DiscountPercent, @ImagePath, @IsActive)";
            }

            object oldPriceValue = numOldPrice.Value > 0 ? (object)numOldPrice.Value : DBNull.Value;
            object discountValue = numDiscount.Value > 0 ? (object)numDiscount.Value : DBNull.Value;
            object imageValue = string.IsNullOrEmpty(imagePathRelative) ? (object)DBNull.Value : imagePathRelative;

            var parameters = new[]
            {
                new SqlParameter("@Name", txtName.Text.Trim()),
                new SqlParameter("@Description", (object)txtDescription.Text.Trim() ?? DBNull.Value),
                new SqlParameter("@Price", numPrice.Value),
                new SqlParameter("@OldPrice", oldPriceValue),
                new SqlParameter("@DiscountPercent", discountValue),
                new SqlParameter("@ImagePath", imageValue),
                new SqlParameter("@IsActive", chkIsActive.Checked),
                new SqlParameter("@Id", productId ?? (object)DBNull.Value)
            };

            try
            {
                Database.ExecuteNonQuery(sql, parameters);
                DialogResult = DialogResult.OK;
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка сохранения товара: " + ex.Message);
            }
        }
    }
}

