using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using Flowers.Data;
using Flowers.Models;

namespace Flowers
{
    public partial class AdminPanelForm : Form
    {
        private DataTable productsTable;
        private DataTable ordersTable;
        private DataTable usersTable;
        private DataTable rolesTable;

        public AdminPanelForm()
        {
            InitializeComponent();
            LoadRoles();
            LoadProducts();
            LoadOrders();
            LoadUsers();

            if (!CurrentUser.IsAtLeastManager())
            {
                MessageBox.Show("Нет прав доступа.");
                Close();
                return;
            }

            // менеджеру скрываем вкладку пользователей и кнопку удаления
            if (CurrentUser.RoleName != "Admin")
            {
                tabControlMain.TabPages.Remove(tabUsers);
                btnDeleteProduct.Visible = false;
            }
        }

        private void LoadRoles()
        {
            rolesTable = Database.ExecuteQuery("SELECT Id, Name FROM Roles");
        }

        private void LoadProducts()
        {
            string sql = "SELECT Id, Name, Description, Price, OldPrice, DiscountPercent, ImagePath, IsActive FROM Products";
            productsTable = Database.ExecuteQuery(sql);
            dgvProductsAdmin.DataSource = productsTable;
            if (dgvProductsAdmin.Columns["Id"] != null)
                dgvProductsAdmin.Columns["Id"].HeaderText = "Id";
        }

        private void LoadOrders()
        {
            string sql = @"
SELECT o.Id, u.Email, o.CreatedAt, o.Status, o.TotalAmount
FROM Orders o
JOIN Users u ON o.UserId = u.Id
ORDER BY o.CreatedAt DESC";

            ordersTable = Database.ExecuteQuery(sql);
            dgvOrdersAdmin.DataSource = ordersTable;
        }

        private void LoadUsers()
        {
            string sql = @"
SELECT u.Id, u.Email, u.Phone, u.FullName, r.Name AS RoleName
FROM Users u
JOIN Roles r ON u.RoleId = r.Id";

            usersTable = Database.ExecuteQuery(sql);
            dgvUsersAdmin.DataSource = usersTable;

            if (dgvUsersAdmin.Columns["RoleName"] != null && dgvUsersAdmin.Columns["RoleName"] is DataGridViewTextBoxColumn)
            {
                int index = dgvUsersAdmin.Columns["RoleName"].Index;
                dgvUsersAdmin.Columns.RemoveAt(index);

                var comboCol = new DataGridViewComboBoxColumn();
                comboCol.Name = "RoleName";
                comboCol.HeaderText = "Роль";
                comboCol.DataPropertyName = "RoleName";
                comboCol.DataSource = rolesTable;
                comboCol.DisplayMember = "Name";
                comboCol.ValueMember = "Name";
                dgvUsersAdmin.Columns.Insert(index, comboCol);
            }
        }

        private void btnAddProduct_Click(object sender, EventArgs e)
        {
            using (var form = new EditProductForm())
            {
                if (form.ShowDialog() == DialogResult.OK)
                {
                    LoadProducts();
                }
            }
        }

        private void btnEditProduct_Click(object sender, EventArgs e)
        {
            if (dgvProductsAdmin.SelectedRows.Count == 0) return;
            int id = (int)dgvProductsAdmin.SelectedRows[0].Cells["Id"].Value;

            using (var form = new EditProductForm(id))
            {
                if (form.ShowDialog() == DialogResult.OK)
                {
                    LoadProducts();
                }
            }
        }

        private void btnDeleteProduct_Click(object sender, EventArgs e)
        {
            if (dgvProductsAdmin.SelectedRows.Count == 0) return;

            int id = (int)dgvProductsAdmin.SelectedRows[0].Cells["Id"].Value;

            var result = MessageBox.Show("Удалить товар?", "Подтверждение", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result != DialogResult.Yes) return;

            try
            {
                string sql = "DELETE FROM Products WHERE Id = @Id";
                Database.ExecuteNonQuery(sql, new SqlParameter("@Id", id));
                LoadProducts();
            }
            catch (SqlException ex)
            {
                if (ex.Number == 547)
                {
                    MessageBox.Show("Невозможно удалить товар, так как он присутствует в одном или нескольких заказах.", "Удаление", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else
                {
                    MessageBox.Show("Ошибка удаления: " + ex.Message);
                }
            }
        }

        private void btnSaveUsers_Click(object sender, EventArgs e)
        {
            if (CurrentUser.RoleName != "Admin")
            {
                MessageBox.Show("Нет прав для изменения ролей.");
                return;
            }

            foreach (DataGridViewRow row in dgvUsersAdmin.Rows)
            {
                if (row.IsNewRow) continue;

                int userId = (int)row.Cells["Id"].Value;
                string roleName = row.Cells["RoleName"].Value?.ToString();
                if (string.IsNullOrEmpty(roleName)) continue;

                DataRow[] roleRows = rolesTable.Select("Name = '" + roleName.Replace("'", "''") + "'");
                if (roleRows.Length == 0) continue;

                int roleId = (int)roleRows[0]["Id"];

                string sql = "UPDATE Users SET RoleId = @RoleId WHERE Id = @Id";
                Database.ExecuteNonQuery(sql,
                    new SqlParameter("@RoleId", roleId),
                    new SqlParameter("@Id", userId));
            }

            MessageBox.Show("Роли пользователей обновлены.");
            LoadUsers();
        }
    }
}

