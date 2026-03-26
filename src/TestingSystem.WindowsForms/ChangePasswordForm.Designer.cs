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
            tableLayout = new TableLayoutPanel();
            buttonPanel = new FlowLayoutPanel();
            tableLayout.SuspendLayout();
            buttonPanel.SuspendLayout();
            SuspendLayout();
            // 
            // lblNewPassword
            // 
            lblNewPassword.Dock = DockStyle.Fill;
            lblNewPassword.Location = new Point(23, 20);
            lblNewPassword.Name = "lblNewPassword";
            lblNewPassword.Size = new Size(138, 40);
            lblNewPassword.TabIndex = 0;
            lblNewPassword.Text = "Новый пароль:";
            lblNewPassword.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // txtNewPassword
            // 
            txtNewPassword.Dock = DockStyle.Fill;
            txtNewPassword.Location = new Point(167, 23);
            txtNewPassword.Name = "txtNewPassword";
            txtNewPassword.PasswordChar = '●';
            txtNewPassword.Size = new Size(210, 25);
            txtNewPassword.TabIndex = 1;
            // 
            // lblConfirmPassword
            // 
            lblConfirmPassword.Dock = DockStyle.Fill;
            lblConfirmPassword.Location = new Point(23, 60);
            lblConfirmPassword.Name = "lblConfirmPassword";
            lblConfirmPassword.Size = new Size(138, 40);
            lblConfirmPassword.TabIndex = 2;
            lblConfirmPassword.Text = "Подтверждение:";
            lblConfirmPassword.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // txtConfirmPassword
            // 
            txtConfirmPassword.Dock = DockStyle.Fill;
            txtConfirmPassword.Location = new Point(167, 63);
            txtConfirmPassword.Name = "txtConfirmPassword";
            txtConfirmPassword.PasswordChar = '●';
            txtConfirmPassword.Size = new Size(210, 25);
            txtConfirmPassword.TabIndex = 3;
            // 
            // btnChange
            // 
            btnChange.AutoSize = true;
            btnChange.BackColor = Color.FromArgb(0, 120, 215);
            btnChange.FlatAppearance.BorderSize = 0;
            btnChange.FlatStyle = FlatStyle.Flat;
            btnChange.ForeColor = Color.White;
            btnChange.Location = new Point(195, 3);
            btnChange.Name = "btnChange";
            btnChange.Size = new Size(75, 29);
            btnChange.TabIndex = 1;
            btnChange.Text = "Сменить";
            btnChange.UseVisualStyleBackColor = false;
            btnChange.Click += BtnChange_Click;
            // 
            // btnCancel
            // 
            btnCancel.AutoSize = true;
            btnCancel.Location = new Point(276, 3);
            btnCancel.Name = "btnCancel";
            btnCancel.Size = new Size(75, 29);
            btnCancel.TabIndex = 0;
            btnCancel.Text = "Отмена";
            btnCancel.Click += BtnCancel_Click;
            // 
            // lblMessage
            // 
            tableLayout.SetColumnSpan(lblMessage, 2);
            lblMessage.Dock = DockStyle.Fill;
            lblMessage.ForeColor = Color.Red;
            lblMessage.Location = new Point(23, 150);
            lblMessage.Name = "lblMessage";
            lblMessage.Size = new Size(354, 30);
            lblMessage.TabIndex = 5;
            lblMessage.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // tableLayout
            // 
            tableLayout.ColumnCount = 2;
            tableLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 40F));
            tableLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 60F));
            tableLayout.Controls.Add(lblNewPassword, 0, 0);
            tableLayout.Controls.Add(txtNewPassword, 1, 0);
            tableLayout.Controls.Add(lblConfirmPassword, 0, 1);
            tableLayout.Controls.Add(txtConfirmPassword, 1, 1);
            tableLayout.Controls.Add(buttonPanel, 0, 2);
            tableLayout.Controls.Add(lblMessage, 0, 3);
            tableLayout.Dock = DockStyle.Fill;
            tableLayout.Location = new Point(0, 0);
            tableLayout.Name = "tableLayout";
            tableLayout.Padding = new Padding(20);
            tableLayout.RowCount = 4;
            tableLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 40F));
            tableLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 40F));
            tableLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 50F));
            tableLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 30F));
            tableLayout.Size = new Size(400, 176);
            tableLayout.TabIndex = 0;
            // 
            // buttonPanel
            // 
            tableLayout.SetColumnSpan(buttonPanel, 2);
            buttonPanel.Controls.Add(btnCancel);
            buttonPanel.Controls.Add(btnChange);
            buttonPanel.Dock = DockStyle.Fill;
            buttonPanel.FlowDirection = FlowDirection.RightToLeft;
            buttonPanel.Location = new Point(23, 103);
            buttonPanel.Name = "buttonPanel";
            buttonPanel.Size = new Size(354, 44);
            buttonPanel.TabIndex = 4;
            // 
            // ChangePasswordForm
            // 
            BackColor = Color.White;
            ClientSize = new Size(400, 176);
            Controls.Add(tableLayout);
            Font = new Font("Segoe UI", 10F);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            Name = "ChangePasswordForm";
            StartPosition = FormStartPosition.CenterParent;
            Text = "Смена пароля";
            tableLayout.ResumeLayout(false);
            tableLayout.PerformLayout();
            buttonPanel.ResumeLayout(false);
            buttonPanel.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private Label lblNewPassword;
        private TextBox txtNewPassword;
        private Label lblConfirmPassword;
        private TextBox txtConfirmPassword;
        private Button btnChange;
        private Button btnCancel;
        private Label lblMessage;
        private TableLayoutPanel tableLayout;
        private FlowLayoutPanel buttonPanel;
    }
}