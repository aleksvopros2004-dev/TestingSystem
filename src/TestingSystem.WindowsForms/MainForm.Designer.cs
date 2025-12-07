namespace TestingSystem.WindowsForms
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
            lblWelcome = new Label();
            btnManageTests = new Button();
            btnManageUsers = new Button();
            btnLogout = new Button();
            SuspendLayout();
            // 
            // lblWelcome
            // 
            lblWelcome.AutoSize = true;
            lblWelcome.Font = new Font("Arial", 12F, FontStyle.Bold);
            lblWelcome.Location = new Point(18, 15);
            lblWelcome.Name = "lblWelcome";
            lblWelcome.Size = new Size(363, 19);
            lblWelcome.TabIndex = 0;
            lblWelcome.Text = "Добро пожаловать, {UserName} ({UserRole})!";
            // 
            // btnManageTests
            // 
            btnManageTests.Font = new Font("Arial", 10F, FontStyle.Bold);
            btnManageTests.Location = new Point(18, 52);
            btnManageTests.Margin = new Padding(3, 2, 3, 2);
            btnManageTests.Name = "btnManageTests";
            btnManageTests.Size = new Size(131, 42);
            btnManageTests.TabIndex = 1;
            btnManageTests.Text = "Управление тестами";
            btnManageTests.UseVisualStyleBackColor = true;
            btnManageTests.Visible = false;
            btnManageTests.Click += BtnManageTests_Click;
            // 
            // btnManageUsers
            // 
            btnManageUsers.Font = new Font("Arial", 10F, FontStyle.Bold);
            btnManageUsers.Location = new Point(18, 112);
            btnManageUsers.Margin = new Padding(3, 2, 3, 2);
            btnManageUsers.Name = "btnManageUsers";
            btnManageUsers.Size = new Size(131, 42);
            btnManageUsers.TabIndex = 2;
            btnManageUsers.Text = "Управление пользователями";
            btnManageUsers.UseVisualStyleBackColor = true;
            btnManageUsers.Visible = false;
            btnManageUsers.Click += BtnManageUsers_Click;
            // 
            // btnLogout
            // 
            btnLogout.Location = new Point(569, 15);
            btnLogout.Margin = new Padding(3, 2, 3, 2);
            btnLogout.Name = "btnLogout";
            btnLogout.Size = new Size(88, 22);
            btnLogout.TabIndex = 3;
            btnLogout.Text = "Выйти";
            btnLogout.UseVisualStyleBackColor = true;
            btnLogout.Click += BtnLogout_Click;
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.White;
            ClientSize = new Size(700, 450);
            Controls.Add(btnLogout);
            Controls.Add(btnManageUsers);
            Controls.Add(btnManageTests);
            Controls.Add(lblWelcome);
            Margin = new Padding(3, 2, 3, 2);
            Name = "MainForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Система тестирования - Главная";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label lblWelcome;
        private Button btnManageTests;
        private Button btnManageUsers;
        private Button btnLogout;
    }
}