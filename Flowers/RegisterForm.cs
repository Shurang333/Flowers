using System;
using System.Data.SqlClient;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using Flowers.Data;

namespace Flowers
{
    public partial class RegisterForm : Form
    {
        public RegisterForm()
        {
            InitializeComponent();
        }

        private void btnRegister_Click(object sender, EventArgs e)
        {
            var email = txtEmail.Text.Trim();
            var phone = txtPhone.Text.Trim();
            var fullName = txtFullName.Text.Trim();
            var pwd = txtPassword.Text;
            var pwd2 = txtConfirmPassword.Text;

            if (string.IsNullOrWhiteSpace(email) ||
                string.IsNullOrWhiteSpace(phone) ||
                string.IsNullOrWhiteSpace(fullName) ||
                string.IsNullOrWhiteSpace(pwd))
            {
                MessageBox.Show("Заполните все обязательные поля.", "Регистрация", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (!Regex.IsMatch(email, @"^[^@\s]+@[^@\s]+\.[^@\s]+$"))
            {
                MessageBox.Show("Некорректный email.", "Регистрация", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!Regex.IsMatch(phone, @"^\\+?\\d{7,15}$"))
            {
                MessageBox.Show("Некорректный номер телефона.", "Регистрация", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (pwd.Length < 6)
            {
                MessageBox.Show("Пароль должен быть не менее 6 символов.", "Регистрация", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (pwd != pwd2)
            {
                MessageBox.Show("Пароли не совпадают.", "Регистрация", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string getRoleSql = "SELECT Id FROM Roles WHERE Name = 'User'";
            var roleIdObj = Database.ExecuteScalar(getRoleSql);
            if (roleIdObj == null)
            {
                MessageBox.Show("Роль 'User' не найдена в базе данных.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            int roleId = (int)roleIdObj;

            string insertSql = @"
INSERT INTO Users (Email, Phone, PasswordHash, FullName, RoleId)
VALUES (@Email, @Phone, @Pwd, @FullName, @RoleId)";

            try
            {
                Database.ExecuteNonQuery(insertSql,
                    new SqlParameter("@Email", email),
                    new SqlParameter("@Phone", phone),
                    new SqlParameter("@Pwd", pwd),
                    new SqlParameter("@FullName", fullName),
                    new SqlParameter("@RoleId", roleId));

                MessageBox.Show("Регистрация прошла успешно. Теперь вы можете войти.", "Регистрация", MessageBoxButtons.OK, MessageBoxIcon.Information);
                DialogResult = DialogResult.OK;
                Close();
            }
            catch (SqlException ex) when (ex.Number == 2627)
            {
                MessageBox.Show("Пользователь с таким email или телефоном уже существует.", "Регистрация", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка регистрации: " + ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}

