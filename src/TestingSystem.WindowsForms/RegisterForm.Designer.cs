namespace TestingSystem.WindowsForms
{
    partial class RegisterForm
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
            lblPassword = new Label();
            txtPassword = new TextBox();
            lblConfirmPassword = new Label();
            txtConfirmPassword = new TextBox();
            tableLayout = new TableLayoutPanel();
            buttonPanel = new FlowLayoutPanel();
            btnCancel = new Button();
            btnRegister = new Button();
            lblMessage = new Label();
            lblInfo = new Label();
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
            lblTitle.Text = "Регистрация нового пользователя";
            lblTitle.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // lblLogin
            // 
            lblLogin.Dock = DockStyle.Fill;
            lblLogin.Location = new Point(23, 60);
            lblLogin.Name = "lblLogin";
            lblLogin.Size = new Size(158, 40);
            lblLogin.TabIndex = 1;
            lblLogin.Text = "Логин:";
            lblLogin.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // txtLogin
            // 
            txtLogin.Dock = DockStyle.Fill;
            txtLogin.Location = new Point(187, 63);
            txtLogin.Name = "txtLogin";
            txtLogin.Size = new Size(240, 25);
            txtLogin.TabIndex = 2;
            // 
            // lblFullName
            // 
            lblFullName.Dock = DockStyle.Fill;
            lblFullName.Location = new Point(23, 100);
            lblFullName.Name = "lblFullName";
            lblFullName.Size = new Size(158, 40);
            lblFullName.TabIndex = 3;
            lblFullName.Text = "ФИО:";
            lblFullName.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // txtFullName
            // 
            txtFullName.Dock = DockStyle.Fill;
            txtFullName.Location = new Point(187, 103);
            txtFullName.Name = "txtFullName";
            txtFullName.Size = new Size(240, 25);
            txtFullName.TabIndex = 4;
            // 
            // lblPassword
            // 
            lblPassword.Dock = DockStyle.Fill;
            lblPassword.Location = new Point(23, 140);
            lblPassword.Name = "lblPassword";
            lblPassword.Size = new Size(158, 40);
            lblPassword.TabIndex = 5;
            lblPassword.Text = "Пароль:";
            lblPassword.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // txtPassword
            // 
            txtPassword.Dock = DockStyle.Fill;
            txtPassword.Location = new Point(187, 143);
            txtPassword.Name = "txtPassword";
            txtPassword.PasswordChar = '●';
            txtPassword.Size = new Size(240, 25);
            txtPassword.TabIndex = 6;
            // 
            // lblConfirmPassword
            // 
            lblConfirmPassword.Dock = DockStyle.Fill;
            lblConfirmPassword.Location = new Point(23, 180);
            lblConfirmPassword.Name = "lblConfirmPassword";
            lblConfirmPassword.Size = new Size(158, 40);
            lblConfirmPassword.TabIndex = 7;
            lblConfirmPassword.Text = "Подтверждение:";
            lblConfirmPassword.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // txtConfirmPassword
            // 
            txtConfirmPassword.Dock = DockStyle.Fill;
            txtConfirmPassword.Location = new Point(187, 183);
            txtConfirmPassword.Name = "txtConfirmPassword";
            txtConfirmPassword.PasswordChar = '●';
            txtConfirmPassword.Size = new Size(240, 25);
            txtConfirmPassword.TabIndex = 8;
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
            tableLayout.Controls.Add(lblPassword, 0, 3);
            tableLayout.Controls.Add(txtPassword, 1, 3);
            tableLayout.Controls.Add(lblConfirmPassword, 0, 4);
            tableLayout.Controls.Add(txtConfirmPassword, 1, 4);
            tableLayout.Controls.Add(buttonPanel, 0, 5);
            tableLayout.Controls.Add(lblMessage, 0, 6);
            tableLayout.Controls.Add(lblInfo, 0, 7);
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
            tableLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 50F));
            tableLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 30F));
            tableLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 30F));
            tableLayout.Size = new Size(450, 353);
            tableLayout.TabIndex = 0;
            // 
            // buttonPanel
            // 
            tableLayout.SetColumnSpan(buttonPanel, 2);
            buttonPanel.Controls.Add(btnCancel);
            buttonPanel.Controls.Add(btnRegister);
            buttonPanel.Dock = DockStyle.Fill;
            buttonPanel.FlowDirection = FlowDirection.RightToLeft;
            buttonPanel.Location = new Point(23, 223);
            buttonPanel.Name = "buttonPanel";
            buttonPanel.Size = new Size(404, 44);
            buttonPanel.TabIndex = 9;
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
            // btnRegister
            // 
            btnRegister.AutoSize = true;
            btnRegister.BackColor = Color.FromArgb(0, 120, 215);
            btnRegister.FlatAppearance.BorderSize = 0;
            btnRegister.FlatStyle = FlatStyle.Flat;
            btnRegister.ForeColor = Color.White;
            btnRegister.Location = new Point(174, 3);
            btnRegister.Name = "btnRegister";
            btnRegister.Size = new Size(146, 29);
            btnRegister.TabIndex = 1;
            btnRegister.Text = "Зарегистрироваться";
            btnRegister.UseVisualStyleBackColor = false;
            btnRegister.Click += BtnRegister_Click;
            // 
            // lblMessage
            // 
            tableLayout.SetColumnSpan(lblMessage, 2);
            lblMessage.Dock = DockStyle.Fill;
            lblMessage.ForeColor = Color.Red;
            lblMessage.Location = new Point(23, 270);
            lblMessage.Name = "lblMessage";
            lblMessage.Size = new Size(404, 30);
            lblMessage.TabIndex = 10;
            lblMessage.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // lblInfo
            // 
            tableLayout.SetColumnSpan(lblInfo, 2);
            lblInfo.Dock = DockStyle.Fill;
            lblInfo.ForeColor = Color.Gray;
            lblInfo.Location = new Point(23, 300);
            lblInfo.Name = "lblInfo";
            lblInfo.Size = new Size(404, 33);
            lblInfo.TabIndex = 11;
            lblInfo.Text = "После регистрации вы сможете войти в систему";
            lblInfo.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // RegisterForm
            // 
            BackColor = Color.White;
            ClientSize = new Size(450, 353);
            Controls.Add(tableLayout);
            Font = new Font("Segoe UI", 10F);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            Name = "RegisterForm";
            StartPosition = FormStartPosition.CenterParent;
            Text = "Регистрация";
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
        private Label lblPassword;
        private TextBox txtPassword;
        private Label lblConfirmPassword;
        private TextBox txtConfirmPassword;
        private TableLayoutPanel tableLayout;
        private FlowLayoutPanel buttonPanel;
        private Button btnCancel;
        private Button btnRegister;
        private Label lblMessage;
        private Label lblInfo;
    }
}