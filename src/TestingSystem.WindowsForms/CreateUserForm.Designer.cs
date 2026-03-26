namespace TestingSystem.WindowsForms
{
    partial class CreateUserForm
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
            lblTitle = new Label();
            lblLogin = new Label();
            txtLogin = new TextBox();
            lblFullName = new Label();
            txtFullName = new TextBox();
            lblRole = new Label();
            cmbRole = new ComboBox();
            lblPassword = new Label();
            txtPassword = new TextBox();
            lblConfirmPassword = new Label();
            txtConfirmPassword = new TextBox();
            tableLayout = new TableLayoutPanel();
            buttonPanel = new FlowLayoutPanel();
            btnCancel = new Button();
            btnCreate = new Button();
            lblMessage = new Label();
            tableLayout.SuspendLayout();
            buttonPanel.SuspendLayout();
            SuspendLayout();
            // 
            // lblTitle
            // 
            tableLayout.SetColumnSpan(lblTitle, 2);
            lblTitle.Dock = DockStyle.Fill;
            lblTitle.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            lblTitle.Location = new Point(23, 20);
            lblTitle.Name = "lblTitle";
            lblTitle.Size = new Size(404, 40);
            lblTitle.TabIndex = 0;
            lblTitle.Text = "Создание пользователя";
            lblTitle.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // lblLogin
            // 
            lblLogin.Dock = DockStyle.Fill;
            lblLogin.Location = new Point(23, 60);
            lblLogin.Name = "lblLogin";
            lblLogin.Size = new Size(145, 40);
            lblLogin.TabIndex = 1;
            lblLogin.Text = "Логин:";
            lblLogin.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // txtLogin
            // 
            txtLogin.Dock = DockStyle.Fill;
            txtLogin.Location = new Point(174, 63);
            txtLogin.Name = "txtLogin";
            txtLogin.Size = new Size(253, 25);
            txtLogin.TabIndex = 2;
            // 
            // lblFullName
            // 
            lblFullName.Dock = DockStyle.Fill;
            lblFullName.Location = new Point(23, 100);
            lblFullName.Name = "lblFullName";
            lblFullName.Size = new Size(145, 40);
            lblFullName.TabIndex = 3;
            lblFullName.Text = "ФИО:";
            lblFullName.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // txtFullName
            // 
            txtFullName.Dock = DockStyle.Fill;
            txtFullName.Location = new Point(174, 103);
            txtFullName.Name = "txtFullName";
            txtFullName.Size = new Size(253, 25);
            txtFullName.TabIndex = 4;
            // 
            // lblRole
            // 
            lblRole.Dock = DockStyle.Fill;
            lblRole.Location = new Point(23, 140);
            lblRole.Name = "lblRole";
            lblRole.Size = new Size(145, 40);
            lblRole.TabIndex = 5;
            lblRole.Text = "Роль:";
            lblRole.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // cmbRole
            // 
            cmbRole.Dock = DockStyle.Fill;
            cmbRole.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbRole.FormattingEnabled = true;
            cmbRole.Location = new Point(174, 143);
            cmbRole.Name = "cmbRole";
            cmbRole.Size = new Size(253, 25);
            cmbRole.TabIndex = 6;
            // 
            // lblPassword
            // 
            lblPassword.Dock = DockStyle.Fill;
            lblPassword.Location = new Point(23, 180);
            lblPassword.Name = "lblPassword";
            lblPassword.Size = new Size(145, 40);
            lblPassword.TabIndex = 7;
            lblPassword.Text = "Пароль:";
            lblPassword.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // txtPassword
            // 
            txtPassword.Dock = DockStyle.Fill;
            txtPassword.Location = new Point(174, 183);
            txtPassword.Name = "txtPassword";
            txtPassword.PasswordChar = '●';
            txtPassword.Size = new Size(253, 25);
            txtPassword.TabIndex = 8;
            // 
            // lblConfirmPassword
            // 
            lblConfirmPassword.Dock = DockStyle.Fill;
            lblConfirmPassword.Location = new Point(23, 220);
            lblConfirmPassword.Name = "lblConfirmPassword";
            lblConfirmPassword.Size = new Size(145, 40);
            lblConfirmPassword.TabIndex = 9;
            lblConfirmPassword.Text = "Подтверждение:";
            lblConfirmPassword.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // txtConfirmPassword
            // 
            txtConfirmPassword.Dock = DockStyle.Fill;
            txtConfirmPassword.Location = new Point(174, 223);
            txtConfirmPassword.Name = "txtConfirmPassword";
            txtConfirmPassword.PasswordChar = '●';
            txtConfirmPassword.Size = new Size(253, 25);
            txtConfirmPassword.TabIndex = 10;
            // 
            // tableLayout
            // 
            tableLayout.ColumnCount = 2;
            tableLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 40F));
            tableLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 60F));
            tableLayout.Controls.Add(lblTitle, 0, 0);
            tableLayout.Controls.Add(lblLogin, 0, 1);
            tableLayout.Controls.Add(txtLogin, 1, 1);
            tableLayout.Controls.Add(lblFullName, 0, 2);
            tableLayout.Controls.Add(txtFullName, 1, 2);
            tableLayout.Controls.Add(lblRole, 0, 3);
            tableLayout.Controls.Add(cmbRole, 1, 3);
            tableLayout.Controls.Add(lblPassword, 0, 4);
            tableLayout.Controls.Add(txtPassword, 1, 4);
            tableLayout.Controls.Add(lblConfirmPassword, 0, 5);
            tableLayout.Controls.Add(txtConfirmPassword, 1, 5);
            tableLayout.Controls.Add(buttonPanel, 0, 6);
            tableLayout.Controls.Add(lblMessage, 0, 7);
            tableLayout.Dock = DockStyle.Fill;
            tableLayout.Location = new Point(0, 0);
            tableLayout.Name = "tableLayout";
            tableLayout.Padding = new Padding(20);
            tableLayout.RowCount = 8;
            tableLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 40F));
            tableLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 40F));
            tableLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 40F));
            tableLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 40F));
            tableLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 40F));
            tableLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 40F));
            tableLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 50F));
            tableLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 30F));
            tableLayout.Size = new Size(450, 333);
            tableLayout.TabIndex = 0;
            // 
            // buttonPanel
            // 
            tableLayout.SetColumnSpan(buttonPanel, 2);
            buttonPanel.Controls.Add(btnCancel);
            buttonPanel.Controls.Add(btnCreate);
            buttonPanel.Dock = DockStyle.Fill;
            buttonPanel.FlowDirection = FlowDirection.RightToLeft;
            buttonPanel.Location = new Point(23, 263);
            buttonPanel.Name = "buttonPanel";
            buttonPanel.Size = new Size(404, 44);
            buttonPanel.TabIndex = 11;
            // 
            // btnCancel
            // 
            btnCancel.AutoSize = true;
            btnCancel.Location = new Point(326, 3);
            btnCancel.Name = "btnCancel";
            btnCancel.Size = new Size(75, 29);
            btnCancel.TabIndex = 0;
            btnCancel.Text = "Отмена";
            btnCancel.Click += BtnCancel_Click;
            // 
            // btnCreate
            // 
            btnCreate.AutoSize = true;
            btnCreate.BackColor = Color.FromArgb(0, 120, 215);
            btnCreate.FlatAppearance.BorderSize = 0;
            btnCreate.FlatStyle = FlatStyle.Flat;
            btnCreate.ForeColor = Color.White;
            btnCreate.Location = new Point(245, 3);
            btnCreate.Name = "btnCreate";
            btnCreate.Size = new Size(75, 29);
            btnCreate.TabIndex = 1;
            btnCreate.Text = "Создать";
            btnCreate.UseVisualStyleBackColor = false;
            btnCreate.Click += BtnCreate_Click;
            // 
            // lblMessage
            // 
            tableLayout.SetColumnSpan(lblMessage, 2);
            lblMessage.Dock = DockStyle.Fill;
            lblMessage.ForeColor = Color.Red;
            lblMessage.Location = new Point(23, 313);
            lblMessage.Name = "lblMessage";
            lblMessage.Size = new Size(404, 30);
            lblMessage.TabIndex = 12;
            lblMessage.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // CreateUserForm
            // 
            BackColor = Color.White;
            ClientSize = new Size(450, 333);
            Controls.Add(tableLayout);
            Font = new Font("Segoe UI", 10F);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            Name = "CreateUserForm";
            StartPosition = FormStartPosition.CenterParent;
            Text = "Создание пользователя";
            tableLayout.ResumeLayout(false);
            tableLayout.PerformLayout();
            buttonPanel.ResumeLayout(false);
            buttonPanel.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private Label lblTitle;
        private Label lblLogin;
        private TextBox txtLogin;
        private Label lblFullName;
        private TextBox txtFullName;
        private Label lblRole;
        private ComboBox cmbRole;
        private Label lblPassword;
        private TextBox txtPassword;
        private Label lblConfirmPassword;
        private TextBox txtConfirmPassword;
        private TableLayoutPanel tableLayout;
        private FlowLayoutPanel buttonPanel;
        private Button btnCancel;
        private Button btnCreate;
        private Label lblMessage;
    }
}