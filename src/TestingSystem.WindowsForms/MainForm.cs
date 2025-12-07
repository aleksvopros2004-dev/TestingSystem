using Microsoft.Extensions.DependencyInjection;
using TestingSystem.Core.Models;
using TestingSystem.Data.Repositories;
using TestingSystem.Services.Interfaces;

namespace TestingSystem.WindowsForms
{
    public partial class MainForm : Form
    {
        public User CurrentUser { get; }

        public MainForm(User user)
        {
            CurrentUser = user ?? throw new ArgumentNullException(nameof(user));
            InitializeComponent();
            SetupForm();
            Console.WriteLine($"MainForm created for user: {CurrentUser.FullName}, Role: {CurrentUser.Role}");
        }

        private void SetupForm()
        {
            lblWelcome.Text = $"Добро пожаловать, {CurrentUser.FullName} ({CurrentUser.Role})!";

            if (CurrentUser?.Role == UserRole.Admin)
            {
                btnManageTests.Visible = true;
                btnManageUsers.Visible = true;
            }
            else
            {
                btnManageTests.Visible = false;
                btnManageUsers.Visible = false;
            }
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

        private void BtnLogout_Click(object? sender, EventArgs e)
        {
            var loginForm = Program.ServiceProvider.GetRequiredService<LoginForm>();
            loginForm.Show();
            this.Close();
        }
    }
}