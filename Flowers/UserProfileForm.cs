using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using Flowers.Data;
using Flowers.Models;

namespace Flowers
{
    public partial class UserProfileForm : Form
    {
        public UserProfileForm()
        {
            InitializeComponent();
            LoadUser();
        }

        private void LoadUser()
        {
            txtEmail.Text = CurrentUser.Email;

            string sql = "SELECT Phone, FullName FROM Users WHERE Id = @Id";
            DataTable table = Database.ExecuteQuery(sql, new SqlParameter("@Id", CurrentUser.Id));
            if (table.Rows.Count > 0)
            {
                var row = table.Rows[0];
                txtPhone.Text = row["Phone"].ToString();
                txtFullName.Text = row["FullName"].ToString();
            }

            lblRole.Text = "Роль: " + CurrentUser.RoleName;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            var email = txtEmail.Text.Trim();
            var phone = txtPhone.Text.Trim();
            var fullName = txtFullName.Text.Trim();

            if (string.IsNullOrWhiteSpace(email) ||
                string.IsNullOrWhiteSpace(phone) ||
                string.IsNullOrWhiteSpace(fullName))
            {
                MessageBox.Show("Заполните все поля.", "Профиль", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            string sql = @"
UPDATE Users
SET Email = @Email,
    Phone = @Phone,
    FullName = @FullName
WHERE Id = @Id";

            try
            {
                Database.ExecuteNonQuery(sql,
                    new SqlParameter("@Email", email),
                    new SqlParameter("@Phone", phone),
                    new SqlParameter("@FullName", fullName),
                    new SqlParameter("@Id", CurrentUser.Id));

                CurrentUser.Email = email;
                CurrentUser.FullName = fullName;

                MessageBox.Show("Данные обновлены.", "Профиль", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка сохранения: " + ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}

