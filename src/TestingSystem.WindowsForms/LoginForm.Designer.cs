namespace TestingSystem.WindowsForms
{
    partial class LoginForm
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
            lblPassword = new Label();
            txtPassword = new TextBox();
            tableLayout = new TableLayoutPanel();
            buttonPanel = new FlowLayoutPanel();
            btnExit = new Button();
            btnRegister = new Button();
            btnLogin = new Button();
            lblMessage = new Label();
            lblTestInfo = new Label();
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
            lblTitle.Text = "Вход в систему тестирования";
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
            // lblPassword
            // 
            lblPassword.Dock = DockStyle.Fill;
            lblPassword.Location = new Point(23, 100);
            lblPassword.Name = "lblPassword";
            lblPassword.Size = new Size(145, 40);
            lblPassword.TabIndex = 3;
            lblPassword.Text = "Пароль:";
            lblPassword.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // txtPassword
            // 
            txtPassword.Dock = DockStyle.Fill;
            txtPassword.Location = new Point(174, 103);
            txtPassword.Name = "txtPassword";
            txtPassword.PasswordChar = '●';
            txtPassword.Size = new Size(253, 25);
            txtPassword.TabIndex = 4;
            // 
            // tableLayout
            // 
            tableLayout.ColumnCount = 2;
            tableLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 40F));
            tableLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 60F));
            tableLayout.Controls.Add(lblTitle, 0, 0);
            tableLayout.Controls.Add(lblLogin, 0, 1);
            tableLayout.Controls.Add(txtLogin, 1, 1);
            tableLayout.Controls.Add(lblPassword, 0, 2);
            tableLayout.Controls.Add(txtPassword, 1, 2);
            tableLayout.Controls.Add(buttonPanel, 0, 3);
            tableLayout.Controls.Add(lblMessage, 0, 4);
            tableLayout.Controls.Add(lblTestInfo, 0, 5);
            tableLayout.Dock = DockStyle.Fill;
            tableLayout.Location = new Point(0, 0);
            tableLayout.Name = "tableLayout";
            tableLayout.Padding = new Padding(20);
            tableLayout.RowCount = 6;
            tableLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 40F));
            tableLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 40F));
            tableLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 40F));
            tableLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 50F));
            tableLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 30F));
            tableLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 30F));
            tableLayout.Size = new Size(450, 263);
            tableLayout.TabIndex = 0;
            // 
            // buttonPanel
            // 
            tableLayout.SetColumnSpan(buttonPanel, 2);
            buttonPanel.Controls.Add(btnExit);
            buttonPanel.Controls.Add(btnRegister);
            buttonPanel.Controls.Add(btnLogin);
            buttonPanel.Dock = DockStyle.Fill;
            buttonPanel.FlowDirection = FlowDirection.RightToLeft;
            buttonPanel.Location = new Point(23, 143);
            buttonPanel.Name = "buttonPanel";
            buttonPanel.Size = new Size(404, 44);
            buttonPanel.TabIndex = 5;
            // 
            // btnExit
            // 
            btnExit.AutoSize = true;
            btnExit.Location = new Point(326, 3);
            btnExit.Name = "btnExit";
            btnExit.Size = new Size(75, 29);
            btnExit.TabIndex = 0;
            btnExit.Text = "Выход";
            btnExit.Click += BtnExit_Click;
            // 
            // btnRegister
            // 
            btnRegister.AutoSize = true;
            btnRegister.Location = new Point(245, 3);
            btnRegister.Name = "btnRegister";
            btnRegister.Size = new Size(75, 29);
            btnRegister.TabIndex = 1;
            btnRegister.Text = "Регистрация";
            btnRegister.Click += BtnRegister_Click;
            // 
            // btnLogin
            // 
            btnLogin.AutoSize = true;
            btnLogin.BackColor = Color.FromArgb(0, 120, 215);
            btnLogin.FlatAppearance.BorderSize = 0;
            btnLogin.FlatStyle = FlatStyle.Flat;
            btnLogin.ForeColor = Color.White;
            btnLogin.Location = new Point(164, 3);
            btnLogin.Name = "btnLogin";
            btnLogin.Size = new Size(75, 29);
            btnLogin.TabIndex = 2;
            btnLogin.Text = "Войти";
            btnLogin.UseVisualStyleBackColor = false;
            btnLogin.Click += BtnLogin_Click;
            // 
            // lblMessage
            // 
            tableLayout.SetColumnSpan(lblMessage, 2);
            lblMessage.Dock = DockStyle.Fill;
            lblMessage.ForeColor = Color.Red;
            lblMessage.Location = new Point(23, 193);
            lblMessage.Name = "lblMessage";
            lblMessage.Size = new Size(404, 30);
            lblMessage.TabIndex = 6;
            lblMessage.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // lblTestInfo
            // 
            tableLayout.SetColumnSpan(lblTestInfo, 2);
            lblTestInfo.Dock = DockStyle.Fill;
            lblTestInfo.ForeColor = Color.Gray;
            lblTestInfo.Location = new Point(23, 223);
            lblTestInfo.Name = "lblTestInfo";
            lblTestInfo.Size = new Size(404, 30);
            lblTestInfo.TabIndex = 7;
            lblTestInfo.Text = "Тестовый аккаунт: admin / admin123";
            lblTestInfo.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // LoginForm
            // 
            BackColor = Color.White;
            ClientSize = new Size(450, 263);
            Controls.Add(tableLayout);
            Font = new Font("Segoe UI", 10F);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            Name = "LoginForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Вход в систему";
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
        private Label lblPassword;
        private TextBox txtPassword;
        private TableLayoutPanel tableLayout;
        private FlowLayoutPanel buttonPanel;
        private Button btnExit;
        private Button btnRegister;
        private Button btnLogin;
        private Label lblMessage;
        private Label lblTestInfo;
    }
}