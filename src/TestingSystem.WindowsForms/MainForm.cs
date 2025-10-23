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
        public User? CurrentUser { get; set; }
        public MainForm()
        {
            InitializeComponent();
            SetupForm();
        }
        private void InitializeComponent()
        {
            this.Text = "Система тестирования";
            this.Size = new Size(800, 600);
            this.StartPosition = FormStartPosition.CenterScreen;
        }

        private void SetupForm()
        {
            // Приветствие
            var lblWelcome = new Label
            {
                Text = CurrentUser != null
                    ? $"Добро пожаловать, {CurrentUser.FullName} ({CurrentUser.Role})!"
                    : "Добро пожаловать в систему тестирования!",
                Location = new Point(20, 20),
                Size = new Size(400, 30),
                Font = new Font("Arial", 12, FontStyle.Bold)
            };



            // Панель управления для администратора
            /*if (CurrentUser?.Role == UserRole.Admin)
            {*/
                var btnManageTests = new Button
                {
                    Text = "Управление тестами",
                    Location = new Point(20, 70),
                    Size = new Size(150, 40),
                    Font = new Font("Arial", 10, FontStyle.Bold)
                };
                btnManageTests.Click += BtnManageTests_Click;

                this.Controls.Add(btnManageTests);
            /*}*/

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
    }
}
