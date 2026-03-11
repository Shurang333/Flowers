using System;
using System.Data;
using System.Windows.Forms;
using Flowers.Data;
using Flowers.Models;

namespace Flowers
{
    public partial class OrdersForm : Form
    {
        public OrdersForm()
        {
            InitializeComponent();
            LoadOrders();
        }

        private void LoadOrders()
        {
            string sql = @"
SELECT o.Id, o.CreatedAt, o.Status, o.TotalAmount
FROM Orders o
WHERE o.UserId = @UserId
ORDER BY o.CreatedAt DESC";

            DataTable table = Database.ExecuteQuery(sql,
                new System.Data.SqlClient.SqlParameter("@UserId", CurrentUser.Id));

            dgvOrders.DataSource = table;

            if (dgvOrders.Columns["Id"] != null)
                dgvOrders.Columns["Id"].HeaderText = "№";

            dgvOrders.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvOrders.ReadOnly = true;
            dgvOrders.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        }
    }
}

