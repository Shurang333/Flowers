using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using Flowers.Data;
using Flowers.Models;

namespace Flowers
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            var login = txtLogin.Text.Trim();
            var password = txtPassword.Text;

            if (string.IsNullOrWhiteSpace(login) || string.IsNullOrWhiteSpace(password))
            {
                MessageBox.Show("Введите логин и пароль.", "Вход", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            try
            {
                string sql = @"
SELECT u.Id, u.FullName, u.Email, r.Name AS RoleName
FROM Users u
JOIN Roles r ON u.RoleId = r.Id
WHERE (u.Email = @login OR u.Phone = @login)
  AND u.PasswordHash = @pwd";

                DataTable table = Database.ExecuteQuery(sql,
                    new SqlParameter("@login", login),
                    new SqlParameter("@pwd", password));

                if (table.Rows.Count == 0)
                {
                    MessageBox.Show("Неверный логин или пароль.", "Вход", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                var row = table.Rows[0];
                CurrentUser.Id = (int)row["Id"];
                CurrentUser.FullName = row["FullName"].ToString();
                CurrentUser.Email = row["Email"].ToString();
                CurrentUser.RoleName = row["RoleName"].ToString();
                
                var mainForm = new MainForm();
                mainForm.Show();
                this.Hide();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка при входе: " + ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnRegister_Click(object sender, EventArgs e)
        {
            using (var regForm = new RegisterForm())
            {
                regForm.ShowDialog(this);
            }
        }
    }
}

