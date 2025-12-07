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
            toolStrip1 = new ToolStrip();
            btnCreateUserTool = new ToolStripButton();
            btnRefreshTool = new ToolStripButton();
            listViewUsers = new ListView();
            columnHeader1 = new ColumnHeader();
            columnHeader2 = new ColumnHeader();
            columnHeader3 = new ColumnHeader();
            columnHeader4 = new ColumnHeader();
            columnHeader5 = new ColumnHeader();
            columnHeader6 = new ColumnHeader();
            btnEdit = new Button();
            btnToggleActive = new Button();
            btnChangePassword = new Button();
            lblStats = new Label();
            toolStrip1.SuspendLayout();
            SuspendLayout();
            // 
            // toolStrip1
            // 
            toolStrip1.ImageScalingSize = new Size(20, 20);
            toolStrip1.Items.AddRange(new ToolStripItem[] { btnCreateUserTool, btnRefreshTool });
            toolStrip1.Location = new Point(0, 0);
            toolStrip1.Name = "toolStrip1";
            toolStrip1.Size = new Size(686, 25);
            toolStrip1.TabIndex = 0;
            toolStrip1.Text = "toolStrip1";
            // 
            // btnCreateUserTool
            // 
            btnCreateUserTool.DisplayStyle = ToolStripItemDisplayStyle.Text;
            btnCreateUserTool.Name = "btnCreateUserTool";
            btnCreateUserTool.Size = new Size(132, 22);
            btnCreateUserTool.Text = "Создать пользователя";
            btnCreateUserTool.Click += BtnCreateUser_Click;
            // 
            // btnRefreshTool
            // 
            btnRefreshTool.DisplayStyle = ToolStripItemDisplayStyle.Text;
            btnRefreshTool.Name = "btnRefreshTool";
            btnRefreshTool.Size = new Size(65, 22);
            btnRefreshTool.Text = "Обновить";
            btnRefreshTool.Click += BtnRefresh_Click;
            // 
            // listViewUsers
            // 
            listViewUsers.Columns.AddRange(new ColumnHeader[] { columnHeader1, columnHeader2, columnHeader3, columnHeader4, columnHeader5, columnHeader6 });
            listViewUsers.FullRowSelect = true;
            listViewUsers.GridLines = true;
            listViewUsers.Location = new Point(18, 30);
            listViewUsers.Margin = new Padding(3, 2, 3, 2);
            listViewUsers.Name = "listViewUsers";
            listViewUsers.Size = new Size(526, 226);
            listViewUsers.TabIndex = 1;
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
            columnHeader2.Width = 100;
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
            columnHeader5.Width = 80;
            // 
            // columnHeader6
            // 
            columnHeader6.Text = "Дата регистрации";
            columnHeader6.Width = 120;
            // 
            // btnEdit
            // 
            btnEdit.Location = new Point(551, 30);
            btnEdit.Margin = new Padding(3, 2, 3, 2);
            btnEdit.Name = "btnEdit";
            btnEdit.Size = new Size(105, 22);
            btnEdit.TabIndex = 2;
            btnEdit.Text = "Редактировать";
            btnEdit.UseVisualStyleBackColor = true;
            btnEdit.Click += BtnEdit_Click;
            // 
            // btnToggleActive
            // 
            btnToggleActive.Location = new Point(551, 60);
            btnToggleActive.Margin = new Padding(3, 2, 3, 2);
            btnToggleActive.Name = "btnToggleActive";
            btnToggleActive.Size = new Size(105, 22);
            btnToggleActive.TabIndex = 3;
            btnToggleActive.Text = "Блокировать";
            btnToggleActive.UseVisualStyleBackColor = true;
            btnToggleActive.Click += BtnToggleActive_Click;
            // 
            // btnChangePassword
            // 
            btnChangePassword.Location = new Point(551, 90);
            btnChangePassword.Margin = new Padding(3, 2, 3, 2);
            btnChangePassword.Name = "btnChangePassword";
            btnChangePassword.Size = new Size(105, 38);
            btnChangePassword.TabIndex = 4;
            btnChangePassword.Text = "Сменить пароль";
            btnChangePassword.UseVisualStyleBackColor = true;
            btnChangePassword.Click += BtnChangePassword_Click;
            // 
            // lblStats
            // 
            lblStats.AutoSize = true;
            lblStats.Font = new Font("Arial", 9F);
            lblStats.Location = new Point(18, 262);
            lblStats.Name = "lblStats";
            lblStats.Size = new Size(339, 15);
            lblStats.TabIndex = 5;
            lblStats.Text = "Всего пользователей: 0 | Активных: 0 | Администраторов: 0";
            // 
            // UserManagementForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(686, 346);
            Controls.Add(lblStats);
            Controls.Add(btnChangePassword);
            Controls.Add(btnToggleActive);
            Controls.Add(btnEdit);
            Controls.Add(listViewUsers);
            Controls.Add(toolStrip1);
            Margin = new Padding(3, 2, 3, 2);
            Name = "UserManagementForm";
            StartPosition = FormStartPosition.CenterParent;
            Text = "Управление пользователями";
            Load += UserManagementForm_Load;
            toolStrip1.ResumeLayout(false);
            toolStrip1.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private ToolStrip toolStrip1;
        private ToolStripButton btnCreateUserTool;
        private ToolStripButton btnRefreshTool;
        private ListView listViewUsers;
        private ColumnHeader columnHeader1;
        private ColumnHeader columnHeader2;
        private ColumnHeader columnHeader3;
        private ColumnHeader columnHeader4;
        private ColumnHeader columnHeader5;
        private ColumnHeader columnHeader6;
        private Button btnEdit;
        private Button btnToggleActive;
        private Button btnChangePassword;
        private Label lblStats;
    }
}