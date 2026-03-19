using System;
using System.Collections.Generic;
using System.Data;
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
            // Чтобы DataGridView не пытался форматировать ячейки
            // в момент смены DataSource (это часто вызывает CurrencyManager-ошибки).
            dgvCart.CellFormatting -= DgvCart_CellFormatting;
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

            dgvCart.CellFormatting += DgvCart_CellFormatting;
        }

        private void DgvCart_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            try
            {
                // служебные строки (например, заголовок) и ситуации пересоздания DataSource
                if (e.RowIndex < 0 || e.RowIndex >= dgvCart.Rows.Count) return;
                if (e.ColumnIndex < 0 || e.ColumnIndex >= dgvCart.Columns.Count) return;

                if (dgvCart.Columns[e.ColumnIndex].Name != "Image") return;

                var row = dgvCart.Rows[e.RowIndex];
                var item = row.DataBoundItem as CartItem;

                // Заглушка, если реальной картинки нет/не загрузилась
                e.Value = ImagePlaceholder.Get();

                if (item != null && !string.IsNullOrEmpty(item.ImagePath))
                {
                    string fullPath = System.IO.Path.Combine(Application.StartupPath, item.ImagePath);
                    if (System.IO.File.Exists(fullPath))
                    {
                        e.Value = System.Drawing.Image.FromFile(fullPath);
                    }
                }
            }
            catch
            {
                // Никогда не допускаем падения из-за форматирования картинки
                e.Value = ImagePlaceholder.Get();
            }
        }

        private void btnRemoveItem_Click(object sender, EventArgs e)
        {
            // DataBoundItem устойчивее к проблемам с именами/видимостью колонок.
            var row = dgvCart.CurrentRow;
            if (row == null || row.DataBoundItem == null)
            {
                if (dgvCart.SelectedRows.Count == 0)
                {
                    MessageBox.Show("Выберите товар в корзине.", "Корзина",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                row = dgvCart.SelectedRows[0];
            }

            if (row.DataBoundItem is CartItem item)
            {
                Cart.Instance.RemoveItem(item.ProductId);
                LoadCart();
                return;
            }

            // Fallback: попробуем прочитать ProductId из колонки.
            try
            {
                var value = row.Cells["ProductId"]?.Value;
                if (value == null || value == DBNull.Value)
                    return;

                int productId = Convert.ToInt32(value);
                Cart.Instance.RemoveItem(productId);
                LoadCart();
            }
            catch
            {
                MessageBox.Show("Не удалось удалить выбранную позицию.", "Корзина",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnCheckout_Click(object sender, EventArgs e)
        {
            if (Cart.Instance.Items.Count == 0)
            {
                MessageBox.Show("Корзина пуста.", "Корзина", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (CurrentUser.Id <= 0)
            {
                MessageBox.Show("Пользователь не задан. Выполните вход заново.", "Заказ",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            decimal total = Cart.Instance.GetTotal();

            using (var conn = Database.GetConnection())
            {
                conn.Open();
                using (var tran = conn.BeginTransaction())
                {
                    bool committed = false;
                    try
                    {
                        int orderId;

                        // Схема Orders может отличаться (CreatedAt/Status могут быть NOT NULL с или без DEFAULT).
                        // Поэтому определяем метаданные и подставляем только то, что действительно нужно.
                        bool hasCreatedAt = false;
                        bool needCreatedAt = false;
                        bool hasStatus = false;
                        bool needStatus = false;
                        bool statusIsString = false;

                        using (var cmdMeta = new SqlCommand(@"
SELECT
    c.name AS ColumnName,
    c.is_nullable,
    t.name AS DataType,
    dc.definition AS DefaultDefinition
FROM sys.columns c
JOIN sys.objects o ON c.object_id = o.object_id
JOIN sys.types t ON c.user_type_id = t.user_type_id
LEFT JOIN sys.default_constraints dc
    ON dc.parent_object_id = c.object_id AND dc.parent_column_id = c.column_id
WHERE o.name = 'Orders'
  AND c.name IN ('CreatedAt', 'Status');", conn, tran))
                        {
                            using (var r = cmdMeta.ExecuteReader())
                            {
                                while (r.Read())
                                {
                                    var col = r["ColumnName"]?.ToString();
                                    bool isNullable = Convert.ToInt32(r["is_nullable"]) == 1;
                                    bool hasDefault = r["DefaultDefinition"] != DBNull.Value && !string.IsNullOrWhiteSpace(r["DefaultDefinition"].ToString());
                                    string dataType = r["DataType"]?.ToString();

                                    if (string.Equals(col, "CreatedAt", StringComparison.OrdinalIgnoreCase))
                                    {
                                        hasCreatedAt = true;
                                        needCreatedAt = !isNullable && !hasDefault;
                                    }
                                    else if (string.Equals(col, "Status", StringComparison.OrdinalIgnoreCase))
                                    {
                                        hasStatus = true;
                                        needStatus = !isNullable && !hasDefault;
                                        statusIsString = !string.IsNullOrWhiteSpace(dataType) &&
                                                          (dataType.Contains("char") || dataType.Contains("text") || dataType.Contains("nchar") || dataType.Contains("nvarchar"));
                                    }
                                }
                            }
                        }

                        var columns = new List<string> { "UserId", "TotalAmount" };
                        var values = new List<string> { "@UserId", "@TotalAmount" };

                        if (needCreatedAt && hasCreatedAt)
                        {
                            columns.Add("CreatedAt");
                            values.Add("GETDATE()");
                        }

                        SqlParameter statusParam = null;
                        if (needStatus && hasStatus)
                        {
                            columns.Add("Status");
                            values.Add("@Status");

                            // Для строкового статуса пробуем универсальное значение.
                            // Для числового/битового - 0.
                            statusParam = new SqlParameter("@Status", statusIsString ? (object)"Pending" : 0);
                        }

                        string insertOrder = @"
INSERT INTO Orders (" + string.Join(", ", columns) + @")
VALUES (" + string.Join(", ", values) + @");
SELECT CAST(SCOPE_IDENTITY() AS int);";

                        using (var cmdOrder = new SqlCommand(insertOrder, conn, tran))
                        {
                            cmdOrder.Parameters.AddWithValue("@UserId", CurrentUser.Id);
                            cmdOrder.Parameters.AddWithValue("@TotalAmount", total);
                            if (statusParam != null)
                                cmdOrder.Parameters.Add(statusParam);

                            object idObj = cmdOrder.ExecuteScalar();
                            if (idObj == null || idObj == DBNull.Value)
                                throw new Exception("Не удалось получить Id созданного заказа (SCOPE_IDENTITY() вернул NULL).");

                            orderId = Convert.ToInt32(idObj);
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
                        committed = true;
                        Cart.Instance.Clear();
                        LoadCart();
                        MessageBox.Show("Заказ успешно оформлен!", "Заказ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    catch (Exception ex)
                    {
                        // Если Commit уже выполнен, Rollback может бросить вторичную ошибку
                        // ("SqlTransaction завершен..."), которая скрывает первопричину.
                        if (!committed)
                        {
                            try { tran.Rollback(); } catch { /* ignore */ }
                        }

                        MessageBox.Show("Ошибка при оформлении заказа: " + ex.Message, "Ошибка",
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
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

