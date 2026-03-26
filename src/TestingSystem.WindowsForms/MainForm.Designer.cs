namespace TestingSystem.WindowsForms
{
    partial class MainForm
    {
        private System.ComponentModel.IContainer components = null;
        private TableLayoutPanel tableLayout;
        private Label lblWelcome;
        private Button btnManageTests;
        private Button btnManageUsers;
        private Button btnStatistics;  // ← добавили кнопку
        private Button btnLogout;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            tableLayout = new TableLayoutPanel();
            lblWelcome = new Label();
            btnManageTests = new Button();
            btnManageUsers = new Button();
            btnStatistics = new Button();  // ← добавили кнопку
            btnLogout = new Button();
            tableLayout.SuspendLayout();
            SuspendLayout();

            // tableLayout
            tableLayout.ColumnCount = 1;
            tableLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tableLayout.Controls.Add(lblWelcome, 0, 0);
            tableLayout.Controls.Add(btnManageTests, 0, 1);
            tableLayout.Controls.Add(btnManageUsers, 0, 2);
            tableLayout.Controls.Add(btnStatistics, 0, 3);  // ← добавили
            tableLayout.Controls.Add(btnLogout, 0, 4);
            tableLayout.Dock = DockStyle.Fill;
            tableLayout.Location = new Point(0, 0);
            tableLayout.Name = "tableLayout";
            tableLayout.Padding = new Padding(20);
            tableLayout.RowCount = 5;
            tableLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 50F));
            tableLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 60F));
            tableLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 60F));
            tableLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 60F));  // ← добавили строку
            tableLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 50F));
            tableLayout.Size = new Size(700, 300);
            tableLayout.TabIndex = 0;

            // lblWelcome
            lblWelcome.Dock = DockStyle.Fill;
            lblWelcome.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            lblWelcome.Location = new Point(23, 20);
            lblWelcome.Name = "lblWelcome";
            lblWelcome.Size = new Size(654, 50);
            lblWelcome.TabIndex = 0;
            lblWelcome.Text = "Добро пожаловать, {UserName} ({UserRole})!";
            lblWelcome.TextAlign = ContentAlignment.MiddleLeft;

            // btnManageTests
            btnManageTests.BackColor = Color.FromArgb(0, 120, 215);
            btnManageTests.FlatAppearance.BorderSize = 0;
            btnManageTests.FlatStyle = FlatStyle.Flat;
            btnManageTests.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            btnManageTests.ForeColor = Color.White;
            btnManageTests.Location = new Point(23, 73);
            btnManageTests.Name = "btnManageTests";
            btnManageTests.Size = new Size(654, 50);
            btnManageTests.TabIndex = 1;
            btnManageTests.Text = "Тесты и вопросы";
            btnManageTests.UseVisualStyleBackColor = false;
            btnManageTests.Visible = false;
            btnManageTests.Click += BtnManageTests_Click;

            // btnManageUsers
            btnManageUsers.BackColor = Color.FromArgb(0, 120, 215);
            btnManageUsers.FlatAppearance.BorderSize = 0;
            btnManageUsers.FlatStyle = FlatStyle.Flat;
            btnManageUsers.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            btnManageUsers.ForeColor = Color.White;
            btnManageUsers.Location = new Point(23, 133);
            btnManageUsers.Name = "btnManageUsers";
            btnManageUsers.Size = new Size(654, 50);
            btnManageUsers.TabIndex = 2;
            btnManageUsers.Text = "Пользователи";
            btnManageUsers.UseVisualStyleBackColor = false;
            btnManageUsers.Visible = false;
            btnManageUsers.Click += BtnManageUsers_Click;

            // btnStatistics - НОВАЯ КНОПКА
            btnStatistics.BackColor = Color.FromArgb(76, 175, 80);
            btnStatistics.FlatAppearance.BorderSize = 0;
            btnStatistics.FlatStyle = FlatStyle.Flat;
            btnStatistics.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            btnStatistics.ForeColor = Color.White;
            btnStatistics.Location = new Point(23, 193);
            btnStatistics.Name = "btnStatistics";
            btnStatistics.Size = new Size(654, 50);
            btnStatistics.TabIndex = 3;
            btnStatistics.Text = "📊 Статистика и аналитика";
            btnStatistics.UseVisualStyleBackColor = false;
            btnStatistics.Visible = false;
            btnStatistics.Click += BtnStatistics_Click;

            // btnLogout
            btnLogout.Location = new Point(23, 253);
            btnLogout.Name = "btnLogout";
            btnLogout.Size = new Size(100, 30);
            btnLogout.TabIndex = 4;
            btnLogout.Text = "Выход";
            btnLogout.Click += BtnLogout_Click;

            // MainForm
            BackColor = Color.White;
            ClientSize = new Size(700, 300);
            Controls.Add(tableLayout);
            Font = new Font("Segoe UI", 10F);
            Name = "MainForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Система тестирования";
            tableLayout.ResumeLayout(false);
            ResumeLayout(false);
        }
    }
}