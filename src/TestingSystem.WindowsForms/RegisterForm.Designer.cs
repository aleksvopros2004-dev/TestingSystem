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
            btnRegister = new Button();
            btnCancel = new Button();
            lblMessage = new Label();
            lblInfo = new Label();
            SuspendLayout();
            // 
            // lblTitle
            // 
            lblTitle.AutoSize = true;
            lblTitle.Font = new Font("Arial", 12F, FontStyle.Bold);
            lblTitle.Location = new Point(18, 15);
            lblTitle.Name = "lblTitle";
            lblTitle.Size = new Size(289, 19);
            lblTitle.TabIndex = 0;
            lblTitle.Text = "Регистрация нового пользователя";
            lblTitle.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // lblLogin
            // 
            lblLogin.AutoSize = true;
            lblLogin.Location = new Point(18, 52);
            lblLogin.Name = "lblLogin";
            lblLogin.Size = new Size(44, 15);
            lblLogin.TabIndex = 1;
            lblLogin.Text = "Логин:";
            // 
            // txtLogin
            // 
            txtLogin.Location = new Point(114, 52);
            txtLogin.Margin = new Padding(3, 2, 3, 2);
            txtLogin.Name = "txtLogin";
            txtLogin.Size = new Size(219, 23);
            txtLogin.TabIndex = 2;
            // 
            // lblFullName
            // 
            lblFullName.AutoSize = true;
            lblFullName.Location = new Point(18, 75);
            lblFullName.Name = "lblFullName";
            lblFullName.Size = new Size(37, 15);
            lblFullName.TabIndex = 3;
            lblFullName.Text = "ФИО:";
            // 
            // txtFullName
            // 
            txtFullName.Location = new Point(114, 75);
            txtFullName.Margin = new Padding(3, 2, 3, 2);
            txtFullName.Name = "txtFullName";
            txtFullName.Size = new Size(219, 23);
            txtFullName.TabIndex = 4;
            // 
            // lblPassword
            // 
            lblPassword.AutoSize = true;
            lblPassword.Location = new Point(18, 98);
            lblPassword.Name = "lblPassword";
            lblPassword.Size = new Size(52, 15);
            lblPassword.TabIndex = 5;
            lblPassword.Text = "Пароль:";
            // 
            // txtPassword
            // 
            txtPassword.Location = new Point(114, 98);
            txtPassword.Margin = new Padding(3, 2, 3, 2);
            txtPassword.Name = "txtPassword";
            txtPassword.PasswordChar = '*';
            txtPassword.Size = new Size(219, 23);
            txtPassword.TabIndex = 6;
            // 
            // lblConfirmPassword
            // 
            lblConfirmPassword.AutoSize = true;
            lblConfirmPassword.Location = new Point(18, 120);
            lblConfirmPassword.Name = "lblConfirmPassword";
            lblConfirmPassword.Size = new Size(97, 15);
            lblConfirmPassword.TabIndex = 7;
            lblConfirmPassword.Text = "Подтверждение:";
            // 
            // txtConfirmPassword
            // 
            txtConfirmPassword.Location = new Point(114, 120);
            txtConfirmPassword.Margin = new Padding(3, 2, 3, 2);
            txtConfirmPassword.Name = "txtConfirmPassword";
            txtConfirmPassword.PasswordChar = '*';
            txtConfirmPassword.Size = new Size(219, 23);
            txtConfirmPassword.TabIndex = 8;
            // 
            // btnRegister
            // 
            btnRegister.Location = new Point(89, 150);
            btnRegister.Margin = new Padding(3, 2, 3, 2);
            btnRegister.Name = "btnRegister";
            btnRegister.Size = new Size(130, 22);
            btnRegister.TabIndex = 9;
            btnRegister.Text = "Зарегистрироваться";
            btnRegister.UseVisualStyleBackColor = true;
            btnRegister.Click += BtnRegister_Click;
            // 
            // btnCancel
            // 
            btnCancel.Location = new Point(228, 150);
            btnCancel.Margin = new Padding(3, 2, 3, 2);
            btnCancel.Name = "btnCancel";
            btnCancel.Size = new Size(70, 22);
            btnCancel.TabIndex = 10;
            btnCancel.Text = "Отмена";
            btnCancel.UseVisualStyleBackColor = true;
            btnCancel.Click += BtnCancel_Click;
            // 
            // lblMessage
            // 
            lblMessage.ForeColor = Color.Red;
            lblMessage.Location = new Point(18, 180);
            lblMessage.Name = "lblMessage";
            lblMessage.Size = new Size(350, 30);
            lblMessage.TabIndex = 11;
            lblMessage.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // lblInfo
            // 
            lblInfo.AutoSize = true;
            lblInfo.Font = new Font("Arial", 8F);
            lblInfo.ForeColor = Color.Gray;
            lblInfo.Location = new Point(69, 210);
            lblInfo.Name = "lblInfo";
            lblInfo.Size = new Size(263, 14);
            lblInfo.TabIndex = 12;
            lblInfo.Text = "После регистрации вы сможете войти в систему";
            lblInfo.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // RegisterForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(380, 233);
            Controls.Add(lblInfo);
            Controls.Add(lblMessage);
            Controls.Add(btnCancel);
            Controls.Add(btnRegister);
            Controls.Add(txtConfirmPassword);
            Controls.Add(lblConfirmPassword);
            Controls.Add(txtPassword);
            Controls.Add(lblPassword);
            Controls.Add(txtFullName);
            Controls.Add(lblFullName);
            Controls.Add(txtLogin);
            Controls.Add(lblLogin);
            Controls.Add(lblTitle);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            Margin = new Padding(3, 2, 3, 2);
            MaximizeBox = false;
            Name = "RegisterForm";
            StartPosition = FormStartPosition.CenterParent;
            Text = "Регистрация нового пользователя";
            Load += RegisterForm_Load;
            ResumeLayout(false);
            PerformLayout();
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
        private Button btnRegister;
        private Button btnCancel;
        private Label lblMessage;
        private Label lblInfo;
    }
}