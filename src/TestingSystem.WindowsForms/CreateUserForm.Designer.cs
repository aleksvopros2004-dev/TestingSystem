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
            btnCreate = new Button();
            btnCancel = new Button();
            lblMessage = new Label();
            SuspendLayout();
            // 
            // lblTitle
            // 
            lblTitle.AutoSize = true;
            lblTitle.Font = new Font("Arial", 12F, FontStyle.Bold);
            lblTitle.Location = new Point(18, 15);
            lblTitle.Name = "lblTitle";
            lblTitle.Size = new Size(265, 19);
            lblTitle.TabIndex = 0;
            lblTitle.Text = "Создание нового пользователя";
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
            // lblRole
            // 
            lblRole.AutoSize = true;
            lblRole.Location = new Point(18, 98);
            lblRole.Name = "lblRole";
            lblRole.Size = new Size(37, 15);
            lblRole.TabIndex = 5;
            lblRole.Text = "Роль:";
            // 
            // cmbRole
            // 
            cmbRole.DisplayMember = "cmbRole.Items.Clear();";
            cmbRole.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbRole.FormattingEnabled = true;
            cmbRole.Location = new Point(114, 98);
            cmbRole.Margin = new Padding(3, 2, 3, 2);
            cmbRole.Name = "cmbRole";
            cmbRole.Size = new Size(132, 23);
            cmbRole.TabIndex = 6;
            // 
            // lblPassword
            // 
            lblPassword.AutoSize = true;
            lblPassword.Location = new Point(18, 120);
            lblPassword.Name = "lblPassword";
            lblPassword.Size = new Size(52, 15);
            lblPassword.TabIndex = 7;
            lblPassword.Text = "Пароль:";
            // 
            // txtPassword
            // 
            txtPassword.Location = new Point(114, 120);
            txtPassword.Margin = new Padding(3, 2, 3, 2);
            txtPassword.Name = "txtPassword";
            txtPassword.PasswordChar = '*';
            txtPassword.Size = new Size(219, 23);
            txtPassword.TabIndex = 8;
            // 
            // lblConfirmPassword
            // 
            lblConfirmPassword.AutoSize = true;
            lblConfirmPassword.Location = new Point(18, 142);
            lblConfirmPassword.Name = "lblConfirmPassword";
            lblConfirmPassword.Size = new Size(97, 15);
            lblConfirmPassword.TabIndex = 9;
            lblConfirmPassword.Text = "Подтверждение:";
            // 
            // txtConfirmPassword
            // 
            txtConfirmPassword.Location = new Point(114, 142);
            txtConfirmPassword.Margin = new Padding(3, 2, 3, 2);
            txtConfirmPassword.Name = "txtConfirmPassword";
            txtConfirmPassword.PasswordChar = '*';
            txtConfirmPassword.Size = new Size(219, 23);
            txtConfirmPassword.TabIndex = 10;
            // 
            // btnCreate
            // 
            btnCreate.Location = new Point(114, 172);
            btnCreate.Margin = new Padding(3, 2, 3, 2);
            btnCreate.Name = "btnCreate";
            btnCreate.Size = new Size(88, 22);
            btnCreate.TabIndex = 11;
            btnCreate.Text = "Создать";
            btnCreate.UseVisualStyleBackColor = true;
            btnCreate.Click += BtnCreate_Click;
            // 
            // btnCancel
            // 
            btnCancel.Location = new Point(210, 172);
            btnCancel.Margin = new Padding(3, 2, 3, 2);
            btnCancel.Name = "btnCancel";
            btnCancel.Size = new Size(70, 22);
            btnCancel.TabIndex = 12;
            btnCancel.Text = "Отмена";
            btnCancel.UseVisualStyleBackColor = true;
            btnCancel.Click += BtnCancel_Click;
            // 
            // lblMessage
            // 
            lblMessage.ForeColor = Color.Red;
            lblMessage.Location = new Point(18, 202);
            lblMessage.Name = "lblMessage";
            lblMessage.Size = new Size(350, 30);
            lblMessage.TabIndex = 13;
            lblMessage.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // CreateUserForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(380, 233);
            Controls.Add(lblMessage);
            Controls.Add(btnCancel);
            Controls.Add(btnCreate);
            Controls.Add(txtConfirmPassword);
            Controls.Add(lblConfirmPassword);
            Controls.Add(txtPassword);
            Controls.Add(lblPassword);
            Controls.Add(cmbRole);
            Controls.Add(lblRole);
            Controls.Add(txtFullName);
            Controls.Add(lblFullName);
            Controls.Add(txtLogin);
            Controls.Add(lblLogin);
            Controls.Add(lblTitle);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            Margin = new Padding(3, 2, 3, 2);
            MaximizeBox = false;
            Name = "CreateUserForm";
            StartPosition = FormStartPosition.CenterParent;
            Text = "Создание нового пользователя";
            Load += CreateUserForm_Load;
            ResumeLayout(false);
            PerformLayout();
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
        private Button btnCreate;
        private Button btnCancel;
        private Label lblMessage;
    }
}