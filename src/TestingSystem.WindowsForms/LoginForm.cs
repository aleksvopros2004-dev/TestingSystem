using Microsoft.Extensions.DependencyInjection;
using TestingSystem.Core.Models;
using TestingSystem.Services.Interfaces;

namespace TestingSystem.WindowsForms
{
    public partial class LoginForm : Form
    {
        private readonly IAuthService _authService;
        public User? CurrentUser { get; private set; }

        public LoginForm(IAuthService authService)
        {
            _authService = authService;
            InitializeComponent();
            SetupControls();
        }

        private void SetupControls()
        {
            // Установка значений по умолчанию для тестирования
            txtLogin.Text = "admin";
            txtPassword.Text = "admin123";
        }

        private void BtnRegister_Click(object? sender, EventArgs e)
        {
            var registerForm = Program.ServiceProvider.GetRequiredService<RegisterForm>();
            registerForm.UserRegistered += (s, e) =>
            {
                lblMessage.Text = "Регистрация успешна! Теперь вы можете войти.";
                lblMessage.ForeColor = Color.Green;
            };
            registerForm.ShowDialog();
        }

        private async void BtnLogin_Click(object? sender, EventArgs e)
        {
            var login = txtLogin.Text.Trim();
            var password = txtPassword.Text;

            // Валидация
            if (string.IsNullOrEmpty(login) || string.IsNullOrEmpty(password))
            {
                lblMessage.Text = "Заполните все поля";
                return;
            }

            // Блокируем кнопку во время выполнения
            btnLogin.Enabled = false;
            lblMessage.Text = "Проверка...";
            lblMessage.ForeColor = Color.Blue;

            try
            {
                // Вызов сервиса аутентификации
                var result = await _authService.LoginAsync(login, password);

                if (result.Success && result.User != null)
                {
                    CurrentUser = result.User;
                    lblMessage.Text = "Успешный вход!";
                    lblMessage.ForeColor = Color.Green;

                    // Задержка для отображения сообщения
                    await Task.Delay(500);

                    // Открываем главную форму
                    OpenMainForm();
                }
                else
                {
                    lblMessage.Text = result.Message;
                    lblMessage.ForeColor = Color.Red;
                }
            }
            catch (Exception ex)
            {
                lblMessage.Text = $"Ошибка: {ex.Message}";
                lblMessage.ForeColor = Color.Red;
            }
            finally
            {
                btnLogin.Enabled = true;
            }
        }

        private void OpenMainForm()
        {
            if (CurrentUser == null) return;

            var mainForm = new MainForm(CurrentUser);
            Console.WriteLine($"Opening MainForm for user: {CurrentUser.FullName}, Role: {CurrentUser.Role}");
            mainForm.Show();
            this.Hide();
        }

        private void BtnExit_Click(object? sender, EventArgs e)
        {
            Application.Exit();
        }

        private void LoginForm_Load(object sender, EventArgs e)
        {
            // Загрузка формы
        }
    }
}