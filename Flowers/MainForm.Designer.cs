namespace Flowers
{
    partial class MainForm
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
            this.panelTop = new System.Windows.Forms.Panel();
            this.lblUserInfo = new System.Windows.Forms.Label();
            this.lblTitle = new System.Windows.Forms.Label();
            this.panelFilter = new System.Windows.Forms.Panel();
            this.btnApplyFilter = new System.Windows.Forms.Button();
            this.cmbSort = new System.Windows.Forms.ComboBox();
            this.lblSort = new System.Windows.Forms.Label();
            this.txtSearch = new System.Windows.Forms.TextBox();
            this.lblSearch = new System.Windows.Forms.Label();
            this.flowCatalog = new System.Windows.Forms.FlowLayoutPanel();
            this.panelBottom = new System.Windows.Forms.Panel();
            this.btnAdminPanel = new System.Windows.Forms.Button();
            this.btnUserProfile = new System.Windows.Forms.Button();
            this.btnMyOrders = new System.Windows.Forms.Button();
            this.btnViewCart = new System.Windows.Forms.Button();
            this.btnAddToCart = new System.Windows.Forms.Button();
            this.panelTop.SuspendLayout();
            this.panelFilter.SuspendLayout();
            this.panelBottom.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelTop
            // 
            this.panelTop.BackColor = System.Drawing.Color.FromArgb(34, 139, 34);
            this.panelTop.Controls.Add(this.lblUserInfo);
            this.panelTop.Controls.Add(this.lblTitle);
            this.panelTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelTop.Location = new System.Drawing.Point(0, 0);
            this.panelTop.Name = "panelTop";
            this.panelTop.Size = new System.Drawing.Size(800, 50);
            this.panelTop.TabIndex = 0;
            // 
            // lblUserInfo
            // 
            this.lblUserInfo.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblUserInfo.AutoSize = true;
            this.lblUserInfo.Location = new System.Drawing.Point(460, 18);
            this.lblUserInfo.Name = "lblUserInfo";
            this.lblUserInfo.Size = new System.Drawing.Size(93, 13);
            this.lblUserInfo.TabIndex = 1;
            this.lblUserInfo.Text = "Имя (Роль)";
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 13F, System.Drawing.FontStyle.Bold);
            this.lblTitle.ForeColor = System.Drawing.Color.White;
            this.lblTitle.Location = new System.Drawing.Point(12, 14);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(151, 21);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "Каталог товаров";
            // 
            // panelFilter
            // 
            this.panelFilter.BackColor = System.Drawing.Color.FromArgb(245, 249, 252);
            this.panelFilter.Controls.Add(this.btnApplyFilter);
            this.panelFilter.Controls.Add(this.cmbSort);
            this.panelFilter.Controls.Add(this.lblSort);
            this.panelFilter.Controls.Add(this.txtSearch);
            this.panelFilter.Controls.Add(this.lblSearch);
            this.panelFilter.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelFilter.Location = new System.Drawing.Point(0, 50);
            this.panelFilter.Name = "panelFilter";
            this.panelFilter.Size = new System.Drawing.Size(800, 50);
            this.panelFilter.TabIndex = 1;
            // 
            // btnApplyFilter
            // 
            this.btnApplyFilter.Location = new System.Drawing.Point(620, 13);
            this.btnApplyFilter.Name = "btnApplyFilter";
            this.btnApplyFilter.Size = new System.Drawing.Size(90, 25);
            this.btnApplyFilter.TabIndex = 4;
            this.btnApplyFilter.Text = "Применить";
            this.btnApplyFilter.UseVisualStyleBackColor = true;
            this.btnApplyFilter.Click += new System.EventHandler(this.btnApplyFilter_Click);
            // 
            // cmbSort
            // 
            this.cmbSort.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbSort.FormattingEnabled = true;
            this.cmbSort.Items.AddRange(new object[] {
            "Цена по возрастанию",
            "Цена по убыванию",
            "Название (А-Я)"});
            this.cmbSort.Location = new System.Drawing.Point(370, 15);
            this.cmbSort.Name = "cmbSort";
            this.cmbSort.Size = new System.Drawing.Size(210, 21);
            this.cmbSort.TabIndex = 3;
            // 
            // lblSort
            // 
            this.lblSort.AutoSize = true;
            this.lblSort.Location = new System.Drawing.Point(300, 18);
            this.lblSort.Name = "lblSort";
            this.lblSort.Size = new System.Drawing.Size(67, 13);
            this.lblSort.TabIndex = 2;
            this.lblSort.Text = "Сортировка:";
            // 
            // txtSearch
            // 
            this.txtSearch.Location = new System.Drawing.Point(60, 15);
            this.txtSearch.Name = "txtSearch";
            this.txtSearch.Size = new System.Drawing.Size(220, 20);
            this.txtSearch.TabIndex = 1;
            // 
            // lblSearch
            // 
            this.lblSearch.AutoSize = true;
            this.lblSearch.Location = new System.Drawing.Point(12, 18);
            this.lblSearch.Name = "lblSearch";
            this.lblSearch.Size = new System.Drawing.Size(42, 13);
            this.lblSearch.TabIndex = 0;
            this.lblSearch.Text = "Поиск:";
            // 
            // flowCatalog
            // 
            this.flowCatalog.AutoScroll = true;
            this.flowCatalog.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowCatalog.Location = new System.Drawing.Point(0, 100);
            this.flowCatalog.Name = "flowCatalog";
            this.flowCatalog.Padding = new System.Windows.Forms.Padding(10);
            this.flowCatalog.Size = new System.Drawing.Size(800, 310);
            this.flowCatalog.TabIndex = 2;
            // 
            // panelBottom
            // 
            this.panelBottom.BackColor = System.Drawing.Color.FromArgb(245, 249, 252);
            this.panelBottom.Controls.Add(this.btnAdminPanel);
            this.panelBottom.Controls.Add(this.btnUserProfile);
            this.panelBottom.Controls.Add(this.btnMyOrders);
            this.panelBottom.Controls.Add(this.btnViewCart);
            this.panelBottom.Controls.Add(this.btnAddToCart);
            this.panelBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelBottom.Location = new System.Drawing.Point(0, 410);
            this.panelBottom.Name = "panelBottom";
            this.panelBottom.Size = new System.Drawing.Size(800, 40);
            this.panelBottom.TabIndex = 3;
            // 
            // btnAdminPanel
            // 
            this.btnAdminPanel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnAdminPanel.Location = new System.Drawing.Point(690, 8);
            this.btnAdminPanel.Name = "btnAdminPanel";
            this.btnAdminPanel.Size = new System.Drawing.Size(90, 25);
            this.btnAdminPanel.TabIndex = 4;
            this.btnAdminPanel.Text = "Управление";
            this.btnAdminPanel.BackColor = System.Drawing.Color.White;
            this.btnAdminPanel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAdminPanel.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(34, 139, 34);
            this.btnAdminPanel.UseVisualStyleBackColor = false;
            this.btnAdminPanel.Click += new System.EventHandler(this.btnAdminPanel_Click);
            // 
            // btnUserProfile
            // 
            this.btnUserProfile.Location = new System.Drawing.Point(450, 8);
            this.btnUserProfile.Name = "btnUserProfile";
            this.btnUserProfile.Size = new System.Drawing.Size(110, 25);
            this.btnUserProfile.TabIndex = 3;
            this.btnUserProfile.Text = "Мой профиль";
            this.btnUserProfile.UseVisualStyleBackColor = true;
            this.btnUserProfile.Click += new System.EventHandler(this.btnUserProfile_Click);
            // 
            // btnMyOrders
            // 
            this.btnMyOrders.Location = new System.Drawing.Point(330, 8);
            this.btnMyOrders.Name = "btnMyOrders";
            this.btnMyOrders.Size = new System.Drawing.Size(100, 25);
            this.btnMyOrders.TabIndex = 2;
            this.btnMyOrders.Text = "Мои заказы";
            this.btnMyOrders.UseVisualStyleBackColor = true;
            this.btnMyOrders.Click += new System.EventHandler(this.btnMyOrders_Click);
            // 
            // btnViewCart
            // 
            this.btnViewCart.Location = new System.Drawing.Point(180, 8);
            this.btnViewCart.Name = "btnViewCart";
            this.btnViewCart.Size = new System.Drawing.Size(140, 25);
            this.btnViewCart.TabIndex = 1;
            this.btnViewCart.Text = "Открыть корзину";
            this.btnViewCart.UseVisualStyleBackColor = true;
            this.btnViewCart.Click += new System.EventHandler(this.btnViewCart_Click);
            // 
            // btnAddToCart
            // 
            this.btnAddToCart.BackColor = System.Drawing.Color.FromArgb(34, 139, 34);
            this.btnAddToCart.ForeColor = System.Drawing.Color.White;
            this.btnAddToCart.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAddToCart.FlatAppearance.BorderSize = 0;
            this.btnAddToCart.Location = new System.Drawing.Point(12, 8);
            this.btnAddToCart.Name = "btnAddToCart";
            this.btnAddToCart.Size = new System.Drawing.Size(160, 25);
            this.btnAddToCart.TabIndex = 0;
            this.btnAddToCart.Text = "Добавить в корзину";
            this.btnAddToCart.UseVisualStyleBackColor = false;
            this.btnAddToCart.Click += new System.EventHandler(this.btnAddToCart_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(245, 249, 252);
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.flowCatalog);
            this.Controls.Add(this.panelBottom);
            this.Controls.Add(this.panelFilter);
            this.Controls.Add(this.panelTop);
            this.MinimumSize = new System.Drawing.Size(700, 400);
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Цветочный магазин - Каталог";
            this.panelTop.ResumeLayout(false);
            this.panelTop.PerformLayout();
            this.panelFilter.ResumeLayout(false);
            this.panelFilter.PerformLayout();
            this.panelBottom.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panelTop;
        private System.Windows.Forms.Label lblUserInfo;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Panel panelFilter;
        private System.Windows.Forms.Button btnApplyFilter;
        private System.Windows.Forms.ComboBox cmbSort;
        private System.Windows.Forms.Label lblSort;
        private System.Windows.Forms.TextBox txtSearch;
        private System.Windows.Forms.Label lblSearch;
        private System.Windows.Forms.FlowLayoutPanel flowCatalog;
        private System.Windows.Forms.Panel panelBottom;
        private System.Windows.Forms.Button btnAdminPanel;
        private System.Windows.Forms.Button btnUserProfile;
        private System.Windows.Forms.Button btnMyOrders;
        private System.Windows.Forms.Button btnViewCart;
        private System.Windows.Forms.Button btnAddToCart;
    }
}

