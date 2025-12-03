using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TestingSystem.Core.Models;
using TestingSystem.Data.Repositories;
using TestingSystem.Services.Interfaces;

namespace TestingSystem.WindowsForms
{
    public partial class MainForm : Form
    {
        public User CurrentUser { get; }

        // Конструктор с обязательным параметром пользователя
        public MainForm(User user)
        {
            CurrentUser = user ?? throw new ArgumentNullException(nameof(user));
            InitializeComponent();
            SetupForm();

            Console.WriteLine($"MainForm created for user: {CurrentUser.FullName}, Role: {CurrentUser.Role}");
        }

        private void InitializeComponent()
        {
            this.SuspendLayout();

            this.Text = "Система тестирования - Главная";
            this.Size = new Size(800, 600);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.BackColor = Color.White;

            this.ResumeLayout(false);
        }

        private void SetupForm()
        {
            this.Controls.Clear();

            // Приветствие
            var lblWelcome = new Label
            {
                Text = $"Добро пожаловать, {CurrentUser.FullName} ({CurrentUser.Role})!",
                Location = new Point(20, 20),
                Size = new Size(400, 30),
                Font = new Font("Arial", 12, FontStyle.Bold)
            };
            this.Controls.Add(lblWelcome);

            // ПРОВЕРКА АДМИНА
            if (CurrentUser?.Role == UserRole.Admin)
            {
                var panelY = 70;
                var buttonWidth = 150;
                var buttonHeight = 40;
                var spacing = 50;

                var btnManageTests = new Button
                {
                    Text = "Управление тестами",
                    Location = new Point(20, panelY),
                    Size = new Size(buttonWidth, buttonHeight),
                    Font = new Font("Arial", 10, FontStyle.Bold)
                };
                btnManageTests.Click += BtnManageTests_Click;

                var btnManageUsers = new Button
                {
                    Text = "Управление пользователями",
                    Location = new Point(20, panelY + spacing),
                    Size = new Size(buttonWidth, buttonHeight),
                    Font = new Font("Arial", 10, FontStyle.Bold)
                };
                btnManageUsers.Click += BtnManageUsers_Click;

                this.Controls.Add(btnManageTests);
                this.Controls.Add(btnManageUsers);
            }

            // Кнопка выхода
            var btnLogout = new Button
            {
                Text = "Выйти",
                Location = new Point(650, 20),
                Size = new Size(100, 30)
            };
            btnLogout.Click += (s, e) =>
            {
                var loginForm = Program.ServiceProvider.GetRequiredService<LoginForm>();
                loginForm.Show();
                this.Close();
            };

            this.Controls.AddRange(new Control[] { lblWelcome, btnLogout });
        }

        private void BtnManageTests_Click(object? sender, EventArgs e)
        {
            if (CurrentUser == null) return;

            var testService = Program.ServiceProvider.GetRequiredService<ITestService>();
            var questionService = Program.ServiceProvider.GetRequiredService<IQuestionService>();

            var testManagementForm = new TestManagementForm(testService, questionService, CurrentUser);
            testManagementForm.ShowDialog();
        }

        private void BtnManageUsers_Click(object? sender, EventArgs e)
        {
            if (CurrentUser == null) return;

            var authService = Program.ServiceProvider.GetRequiredService<IAuthService>();
            var userRepository = Program.ServiceProvider.GetRequiredService<IUserRepository>();

            var userManagementForm = new UserManagementForm(authService, userRepository, CurrentUser);
            userManagementForm.ShowDialog();
        }
    }
}
