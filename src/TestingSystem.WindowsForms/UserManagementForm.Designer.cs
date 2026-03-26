namespace TestingSystem.WindowsForms
{
    partial class UserManagementForm
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
            toolStrip = new ToolStrip();
            btnCreateUserTool = new ToolStripButton();
            btnRefreshTool = new ToolStripButton();
            tableLayout = new TableLayoutPanel();
            listViewUsers = new ListView();
            columnHeader1 = new ColumnHeader();
            columnHeader2 = new ColumnHeader();
            columnHeader3 = new ColumnHeader();
            columnHeader4 = new ColumnHeader();
            columnHeader5 = new ColumnHeader();
            columnHeader6 = new ColumnHeader();
            buttonPanel = new FlowLayoutPanel();
            btnEdit = new Button();
            btnToggleActive = new Button();
            btnChangePassword = new Button();
            lblStats = new Label();
            toolStrip.SuspendLayout();
            tableLayout.SuspendLayout();
            buttonPanel.SuspendLayout();
            SuspendLayout();
            // 
            // toolStrip
            // 
            toolStrip.ImageScalingSize = new Size(20, 20);
            toolStrip.Items.AddRange(new ToolStripItem[] { btnCreateUserTool, btnRefreshTool });
            toolStrip.Location = new Point(0, 0);
            toolStrip.Name = "toolStrip";
            toolStrip.Size = new Size(800, 27);
            toolStrip.TabIndex = 0;
            // 
            // btnCreateUserTool
            // 
            btnCreateUserTool.DisplayStyle = ToolStripItemDisplayStyle.Text;
            btnCreateUserTool.Name = "btnCreateUserTool";
            btnCreateUserTool.Size = new Size(104, 24);
            btnCreateUserTool.Text = "Создать пользователя";
            btnCreateUserTool.Click += BtnCreateUser_Click;
            // 
            // btnRefreshTool
            // 
            btnRefreshTool.DisplayStyle = ToolStripItemDisplayStyle.Text;
            btnRefreshTool.Name = "btnRefreshTool";
            btnRefreshTool.Size = new Size(65, 24);
            btnRefreshTool.Text = "Обновить";
            btnRefreshTool.Click += BtnRefresh_Click;
            // 
            // tableLayout
            // 
            tableLayout.ColumnCount = 2;
            tableLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 75F));
            tableLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25F));
            tableLayout.Controls.Add(listViewUsers, 0, 0);
            tableLayout.Controls.Add(buttonPanel, 1, 0);
            tableLayout.Controls.Add(lblStats, 0, 1);
            tableLayout.Dock = DockStyle.Fill;
            tableLayout.Location = new Point(0, 27);
            tableLayout.Name = "tableLayout";
            tableLayout.Padding = new Padding(20);
            tableLayout.RowCount = 2;
            tableLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 85F));
            tableLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 15F));
            tableLayout.Size = new Size(800, 523);
            tableLayout.TabIndex = 1;
            // 
            // listViewUsers
            // 
            listViewUsers.Columns.AddRange(new ColumnHeader[] { columnHeader1, columnHeader2, columnHeader3, columnHeader4, columnHeader5, columnHeader6 });
            listViewUsers.Dock = DockStyle.Fill;
            listViewUsers.FullRowSelect = true;
            listViewUsers.GridLines = true;
            listViewUsers.Location = new Point(23, 23);
            listViewUsers.Name = "listViewUsers";
            listViewUsers.Size = new Size(554, 404);
            listViewUsers.TabIndex = 0;
            listViewUsers.UseCompatibleStateImageBehavior = false;
            listViewUsers.View = View.Details;
            listViewUsers.SelectedIndexChanged += ListViewUsers_SelectedIndexChanged;
            // 
            // columnHeader1
            // 
            columnHeader1.Text = "ID";
            columnHeader1.Width = 50;
            // 
            // columnHeader2
            // 
            columnHeader2.Text = "Логин";
            columnHeader2.Width = 120;
            // 
            // columnHeader3
            // 
            columnHeader3.Text = "ФИО";
            columnHeader3.Width = 200;
            // 
            // columnHeader4
            // 
            columnHeader4.Text = "Роль";
            columnHeader4.Width = 80;
            // 
            // columnHeader5
            // 
            columnHeader5.Text = "Статус";
            columnHeader5.Width = 100;
            // 
            // columnHeader6
            // 
            columnHeader6.Text = "Дата регистрации";
            columnHeader6.Width = 130;
            // 
            // buttonPanel
            // 
            buttonPanel.Controls.Add(btnEdit);
            buttonPanel.Controls.Add(btnToggleActive);
            buttonPanel.Controls.Add(btnChangePassword);
            buttonPanel.Dock = DockStyle.Top;
            buttonPanel.FlowDirection = FlowDirection.TopDown;
            buttonPanel.Location = new Point(583, 23);
            buttonPanel.Name = "buttonPanel";
            buttonPanel.Size = new Size(194, 150);
            buttonPanel.TabIndex = 1;
            // 
            // btnEdit
            // 
            btnEdit.AutoSize = true;
            btnEdit.Location = new Point(3, 3);
            btnEdit.Name = "btnEdit";
            btnEdit.Size = new Size(130, 30);
            btnEdit.TabIndex = 0;
            btnEdit.Text = "Редактировать";
            btnEdit.UseVisualStyleBackColor = true;
            btnEdit.Click += BtnEdit_Click;
            // 
            // btnToggleActive
            // 
            btnToggleActive.AutoSize = true;
            btnToggleActive.Location = new Point(3, 39);
            btnToggleActive.Name = "btnToggleActive";
            btnToggleActive.Size = new Size(130, 30);
            btnToggleActive.TabIndex = 1;
            btnToggleActive.Text = "Блокировать";
            btnToggleActive.UseVisualStyleBackColor = true;
            btnToggleActive.Click += BtnToggleActive_Click;
            // 
            // btnChangePassword
            // 
            btnChangePassword.AutoSize = true;
            btnChangePassword.Location = new Point(3, 75);
            btnChangePassword.Name = "btnChangePassword";
            btnChangePassword.Size = new Size(130, 30);
            btnChangePassword.TabIndex = 2;
            btnChangePassword.Text = "Сменить пароль";
            btnChangePassword.UseVisualStyleBackColor = true;
            btnChangePassword.Click += BtnChangePassword_Click;
            // 
            // lblStats
            // 
            lblStats.Dock = DockStyle.Fill;
            lblStats.Location = new Point(23, 430);
            lblStats.Name = "lblStats";
            lblStats.Size = new Size(754, 70);
            lblStats.TabIndex = 2;
            lblStats.Text = "Всего пользователей: 0 | Активных: 0 | Администраторов: 0";
            lblStats.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // UserManagementForm
            // 
            BackColor = Color.White;
            ClientSize = new Size(800, 550);
            Controls.Add(tableLayout);
            Controls.Add(toolStrip);
            Font = new Font("Segoe UI", 10F);
            Name = "UserManagementForm";
            StartPosition = FormStartPosition.CenterParent;
            Text = "Управление пользователями";
            toolStrip.ResumeLayout(false);
            toolStrip.PerformLayout();
            tableLayout.ResumeLayout(false);
            buttonPanel.ResumeLayout(false);
            buttonPanel.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private ToolStrip toolStrip;
        private ToolStripButton btnCreateUserTool;
        private ToolStripButton btnRefreshTool;
        private TableLayoutPanel tableLayout;
        private ListView listViewUsers;
        private ColumnHeader columnHeader1;
        private ColumnHeader columnHeader2;
        private ColumnHeader columnHeader3;
        private ColumnHeader columnHeader4;
        private ColumnHeader columnHeader5;
        private ColumnHeader columnHeader6;
        private FlowLayoutPanel buttonPanel;
        private Button btnEdit;
        private Button btnToggleActive;
        private Button btnChangePassword;
        private Label lblStats;
    }
}