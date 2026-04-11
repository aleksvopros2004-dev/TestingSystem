using Microsoft.Extensions.DependencyInjection;
using TestingSystem.Core.Interfaces;
using TestingSystem.Core.Models;
using TestingSystem.Data.Repositories;
using TestingSystem.Services.Interfaces;
using TestingSystem.Services.Services;

namespace TestingSystem.WindowsForms
{
    public partial class MainForm : Form
    {
        public User CurrentUser { get; set; }

        public MainForm(User user)
        {
            CurrentUser = user ?? throw new ArgumentNullException(nameof(user));
            InitializeComponent();
            SetupForm();
        }

        private void SetupForm()
        {
            lblWelcome.Text = $"Добро пожаловать, {CurrentUser.FullName} ({CurrentUser.Role})!";

            // Показываем кнопки в зависимости от роли
            btnManageTests.Visible = true;
            btnManageUsers.Visible = CurrentUser.Role == UserRole.Admin;
            btnStatistics.Visible = CurrentUser.Role == UserRole.Admin;  

            if (CurrentUser?.Role == UserRole.User)
            {
                btnManageTests.Text = "Просмотр тестов";
            }
            else
            {
                btnManageTests.Text = "Управление тестами";
            }
        }

        private void BtnManageTests_Click(object? sender, EventArgs e)
        {
            var testService = Program.ServiceProvider.GetRequiredService<ITestService>();
            var questionService = Program.ServiceProvider.GetRequiredService<IQuestionService>();
            var testManagementForm = new TestManagementForm(testService, questionService, CurrentUser);
            testManagementForm.ShowDialog();
        }

        private void BtnManageUsers_Click(object? sender, EventArgs e)
        {
            var authService = Program.ServiceProvider.GetRequiredService<IAuthService>();
            var userRepository = Program.ServiceProvider.GetRequiredService<IUserRepository>();
            var userManagementForm = new UserManagementForm(authService, userRepository, CurrentUser);
            userManagementForm.ShowDialog();
        }

        private void BtnStatistics_Click(object? sender, EventArgs e)
        {
            var statisticsForm = new StatisticsForm(
                Program.ServiceProvider.GetRequiredService<ITestService>(),
                Program.ServiceProvider.GetRequiredService<IStatisticsRepository>(),
                Program.ServiceProvider.GetRequiredService<ExcelExportService>(),
                Program.ServiceProvider.GetRequiredService<ILemmatizationService>(),  
                CurrentUser);
            statisticsForm.ShowDialog();
        }

        private void BtnLogout_Click(object? sender, EventArgs e)
        {
            var loginForm = Program.ServiceProvider.GetRequiredService<LoginForm>();
            loginForm.Show();
            this.Close();
        }
    }
}