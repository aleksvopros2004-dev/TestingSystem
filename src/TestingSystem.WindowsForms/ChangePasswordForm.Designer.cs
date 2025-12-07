namespace TestingSystem.WindowsForms
{
    partial class ChangePasswordForm
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
            lblNewPassword = new Label();
            txtNewPassword = new TextBox();
            lblConfirmPassword = new Label();
            txtConfirmPassword = new TextBox();
            btnChange = new Button();
            btnCancel = new Button();
            lblMessage = new Label();
            SuspendLayout();
            // 
            // lblNewPassword
            // 
            lblNewPassword.AutoSize = true;
            lblNewPassword.Location = new Point(18, 22);
            lblNewPassword.Name = "lblNewPassword";
            lblNewPassword.Size = new Size(91, 15);
            lblNewPassword.TabIndex = 0;
            lblNewPassword.Text = "Новый пароль:";
            // 
            // txtNewPassword
            // 
            txtNewPassword.Location = new Point(114, 22);
            txtNewPassword.Margin = new Padding(3, 2, 3, 2);
            txtNewPassword.Name = "txtNewPassword";
            txtNewPassword.PasswordChar = '*';
            txtNewPassword.Size = new Size(176, 23);
            txtNewPassword.TabIndex = 1;
            // 
            // lblConfirmPassword
            // 
            lblConfirmPassword.AutoSize = true;
            lblConfirmPassword.Location = new Point(18, 45);
            lblConfirmPassword.Name = "lblConfirmPassword";
            lblConfirmPassword.Size = new Size(97, 15);
            lblConfirmPassword.TabIndex = 2;
            lblConfirmPassword.Text = "Подтверждение:";
            // 
            // txtConfirmPassword
            // 
            txtConfirmPassword.Location = new Point(114, 45);
            txtConfirmPassword.Margin = new Padding(3, 2, 3, 2);
            txtConfirmPassword.Name = "txtConfirmPassword";
            txtConfirmPassword.PasswordChar = '*';
            txtConfirmPassword.Size = new Size(176, 23);
            txtConfirmPassword.TabIndex = 3;
            // 
            // btnChange
            // 
            btnChange.Location = new Point(87, 75);
            btnChange.Margin = new Padding(3, 2, 3, 2);
            btnChange.Name = "btnChange";
            btnChange.Size = new Size(132, 22);
            btnChange.TabIndex = 4;
            btnChange.Text = "Сменить пароль";
            btnChange.UseVisualStyleBackColor = true;
            btnChange.Click += BtnChange_Click;
            // 
            // btnCancel
            // 
            btnCancel.Location = new Point(228, 75);
            btnCancel.Margin = new Padding(3, 2, 3, 2);
            btnCancel.Name = "btnCancel";
            btnCancel.Size = new Size(70, 22);
            btnCancel.TabIndex = 5;
            btnCancel.Text = "Отмена";
            btnCancel.UseVisualStyleBackColor = true;
            btnCancel.Click += BtnCancel_Click;
            // 
            // lblMessage
            // 
            lblMessage.ForeColor = Color.Red;
            lblMessage.Location = new Point(18, 105);
            lblMessage.Name = "lblMessage";
            lblMessage.Size = new Size(306, 30);
            lblMessage.TabIndex = 6;
            lblMessage.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // ChangePasswordForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(350, 188);
            Controls.Add(lblMessage);
            Controls.Add(btnCancel);
            Controls.Add(btnChange);
            Controls.Add(txtConfirmPassword);
            Controls.Add(lblConfirmPassword);
            Controls.Add(txtNewPassword);
            Controls.Add(lblNewPassword);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            Margin = new Padding(3, 2, 3, 2);
            MaximizeBox = false;
            Name = "ChangePasswordForm";
            StartPosition = FormStartPosition.CenterParent;
            Text = "Смена пароля";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label lblNewPassword;
        private TextBox txtNewPassword;
        private Label lblConfirmPassword;
        private TextBox txtConfirmPassword;
        private Button btnChange;
        private Button btnCancel;
        private Label lblMessage;
    }
}