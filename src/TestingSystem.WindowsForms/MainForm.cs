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
            if (CurrentUser.Role == UserRole.Admin)
            {
                var btnManageTests = new Button
                {
                    Text = "Управление тестами",
                    Location = new Point(20, 70),
                    Size = new Size(150, 40),
                    Font = new Font("Arial", 10, FontStyle.Bold),
                    BackColor = Color.LightBlue
                };
                btnManageTests.Click += BtnManageTests_Click;
                this.Controls.Add(btnManageTests);
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
                var loginForm = new LoginForm(Program.ServiceProvider.GetRequiredService<IAuthService>());
                loginForm.Show();
                this.Close();
            };
            this.Controls.Add(btnLogout);
        }

        private void BtnManageTests_Click(object? sender, EventArgs e)
        {
            /*MessageBox.Show("Открывается управление тестами!", "Админ функция");*/
            if (CurrentUser == null) return;

            var testService = Program.ServiceProvider.GetRequiredService<ITestService>();
            var questionService = Program.ServiceProvider.GetRequiredService<IQuestionService>();

            var testManagementForm = new TestManagementForm(testService, questionService, CurrentUser);
            testManagementForm.ShowDialog();
        }
    }
}
