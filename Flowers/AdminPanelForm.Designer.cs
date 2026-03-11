namespace Flowers
{
    partial class AdminPanelForm
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            this.tabControlMain = new System.Windows.Forms.TabControl();
            this.tabProducts = new System.Windows.Forms.TabPage();
            this.btnDeleteProduct = new System.Windows.Forms.Button();
            this.btnEditProduct = new System.Windows.Forms.Button();
            this.btnAddProduct = new System.Windows.Forms.Button();
            this.dgvProductsAdmin = new System.Windows.Forms.DataGridView();
            this.tabOrders = new System.Windows.Forms.TabPage();
            this.dgvOrdersAdmin = new System.Windows.Forms.DataGridView();
            this.tabUsers = new System.Windows.Forms.TabPage();
            this.btnSaveUsers = new System.Windows.Forms.Button();
            this.dgvUsersAdmin = new System.Windows.Forms.DataGridView();
            this.tabControlMain.SuspendLayout();
            this.tabProducts.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvProductsAdmin)).BeginInit();
            this.tabOrders.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvOrdersAdmin)).BeginInit();
            this.tabUsers.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvUsersAdmin)).BeginInit();
            this.SuspendLayout();
            // 
            // tabControlMain
            // 
            this.tabControlMain.Controls.Add(this.tabProducts);
            this.tabControlMain.Controls.Add(this.tabOrders);
            this.tabControlMain.Controls.Add(this.tabUsers);
            this.tabControlMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControlMain.Location = new System.Drawing.Point(0, 0);
            this.tabControlMain.Name = "tabControlMain";
            this.tabControlMain.SelectedIndex = 0;
            this.tabControlMain.Size = new System.Drawing.Size(800, 450);
            this.tabControlMain.TabIndex = 0;
            // 
            // tabProducts
            // 
            this.tabProducts.Controls.Add(this.btnDeleteProduct);
            this.tabProducts.Controls.Add(this.btnEditProduct);
            this.tabProducts.Controls.Add(this.btnAddProduct);
            this.tabProducts.Controls.Add(this.dgvProductsAdmin);
            this.tabProducts.Location = new System.Drawing.Point(4, 22);
            this.tabProducts.Name = "tabProducts";
            this.tabProducts.Padding = new System.Windows.Forms.Padding(3);
            this.tabProducts.Size = new System.Drawing.Size(792, 424);
            this.tabProducts.TabIndex = 0;
            this.tabProducts.Text = "Товары";
            this.tabProducts.UseVisualStyleBackColor = true;
            // 
            // btnDeleteProduct
            // 
            this.btnDeleteProduct.Location = new System.Drawing.Point(226, 385);
            this.btnDeleteProduct.Name = "btnDeleteProduct";
            this.btnDeleteProduct.Size = new System.Drawing.Size(100, 27);
            this.btnDeleteProduct.TabIndex = 3;
            this.btnDeleteProduct.Text = "Удалить";
            this.btnDeleteProduct.UseVisualStyleBackColor = true;
            this.btnDeleteProduct.Click += new System.EventHandler(this.btnDeleteProduct_Click);
            // 
            // btnEditProduct
            // 
            this.btnEditProduct.Location = new System.Drawing.Point(120, 385);
            this.btnEditProduct.Name = "btnEditProduct";
            this.btnEditProduct.Size = new System.Drawing.Size(100, 27);
            this.btnEditProduct.TabIndex = 2;
            this.btnEditProduct.Text = "Изменить";
            this.btnEditProduct.UseVisualStyleBackColor = true;
            this.btnEditProduct.Click += new System.EventHandler(this.btnEditProduct_Click);
            // 
            // btnAddProduct
            // 
            this.btnAddProduct.Location = new System.Drawing.Point(14, 385);
            this.btnAddProduct.Name = "btnAddProduct";
            this.btnAddProduct.Size = new System.Drawing.Size(100, 27);
            this.btnAddProduct.TabIndex = 1;
            this.btnAddProduct.Text = "Добавить";
            this.btnAddProduct.UseVisualStyleBackColor = true;
            this.btnAddProduct.Click += new System.EventHandler(this.btnAddProduct_Click);
            // 
            // dgvProductsAdmin
            // 
            this.dgvProductsAdmin.BackgroundColor = System.Drawing.Color.White;
            this.dgvProductsAdmin.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvProductsAdmin.Location = new System.Drawing.Point(6, 6);
            this.dgvProductsAdmin.MultiSelect = false;
            this.dgvProductsAdmin.Name = "dgvProductsAdmin";
            this.dgvProductsAdmin.ReadOnly = true;
            this.dgvProductsAdmin.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvProductsAdmin.Size = new System.Drawing.Size(780, 370);
            this.dgvProductsAdmin.TabIndex = 0;
            // 
            // tabOrders
            // 
            this.tabOrders.Controls.Add(this.dgvOrdersAdmin);
            this.tabOrders.Location = new System.Drawing.Point(4, 22);
            this.tabOrders.Name = "tabOrders";
            this.tabOrders.Padding = new System.Windows.Forms.Padding(3);
            this.tabOrders.Size = new System.Drawing.Size(792, 424);
            this.tabOrders.TabIndex = 1;
            this.tabOrders.Text = "Заказы";
            this.tabOrders.UseVisualStyleBackColor = true;
            // 
            // dgvOrdersAdmin
            // 
            this.dgvOrdersAdmin.BackgroundColor = System.Drawing.Color.White;
            this.dgvOrdersAdmin.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvOrdersAdmin.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvOrdersAdmin.Location = new System.Drawing.Point(3, 3);
            this.dgvOrdersAdmin.MultiSelect = false;
            this.dgvOrdersAdmin.Name = "dgvOrdersAdmin";
            this.dgvOrdersAdmin.ReadOnly = true;
            this.dgvOrdersAdmin.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvOrdersAdmin.Size = new System.Drawing.Size(786, 418);
            this.dgvOrdersAdmin.TabIndex = 0;
            // 
            // tabUsers
            // 
            this.tabUsers.Controls.Add(this.btnSaveUsers);
            this.tabUsers.Controls.Add(this.dgvUsersAdmin);
            this.tabUsers.Location = new System.Drawing.Point(4, 22);
            this.tabUsers.Name = "tabUsers";
            this.tabUsers.Padding = new System.Windows.Forms.Padding(3);
            this.tabUsers.Size = new System.Drawing.Size(792, 424);
            this.tabUsers.TabIndex = 2;
            this.tabUsers.Text = "Пользователи";
            this.tabUsers.UseVisualStyleBackColor = true;
            // 
            // btnSaveUsers
            // 
            this.btnSaveUsers.Location = new System.Drawing.Point(8, 385);
            this.btnSaveUsers.Name = "btnSaveUsers";
            this.btnSaveUsers.Size = new System.Drawing.Size(150, 27);
            this.btnSaveUsers.TabIndex = 1;
            this.btnSaveUsers.Text = "Сохранить роли";
            this.btnSaveUsers.UseVisualStyleBackColor = true;
            this.btnSaveUsers.Click += new System.EventHandler(this.btnSaveUsers_Click);
            // 
            // dgvUsersAdmin
            // 
            this.dgvUsersAdmin.BackgroundColor = System.Drawing.Color.White;
            this.dgvUsersAdmin.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvUsersAdmin.Location = new System.Drawing.Point(6, 6);
            this.dgvUsersAdmin.MultiSelect = false;
            this.dgvUsersAdmin.Name = "dgvUsersAdmin";
            this.dgvUsersAdmin.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvUsersAdmin.Size = new System.Drawing.Size(780, 370);
            this.dgvUsersAdmin.TabIndex = 0;
            // 
            // AdminPanelForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(245, 249, 252);
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.tabControlMain);
            this.Name = "AdminPanelForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Управление магазином";
            this.tabControlMain.ResumeLayout(false);
            this.tabProducts.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvProductsAdmin)).EndInit();
            this.tabOrders.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvOrdersAdmin)).EndInit();
            this.tabUsers.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvUsersAdmin)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControlMain;
        private System.Windows.Forms.TabPage tabProducts;
        private System.Windows.Forms.TabPage tabOrders;
        private System.Windows.Forms.TabPage tabUsers;
        private System.Windows.Forms.DataGridView dgvProductsAdmin;
        private System.Windows.Forms.Button btnDeleteProduct;
        private System.Windows.Forms.Button btnEditProduct;
        private System.Windows.Forms.Button btnAddProduct;
        private System.Windows.Forms.DataGridView dgvOrdersAdmin;
        private System.Windows.Forms.DataGridView dgvUsersAdmin;
        private System.Windows.Forms.Button btnSaveUsers;
    }
}

