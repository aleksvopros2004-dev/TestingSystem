using TestingSystem.Services.Interfaces;

namespace TestingSystem.WindowsForms
{
    public partial class RegisterForm : Form
    {
        private readonly IAuthService _authService;

        public event EventHandler? UserRegistered;

        public RegisterForm(IAuthService authService)
        {
            _authService = authService;
            InitializeComponent();
        }

        private async void BtnRegister_Click(object? sender, EventArgs e)
        {
            // Валидация
            var login = txtLogin.Text.Trim();
            var fullName = txtFullName.Text.Trim();
            var password = txtPassword.Text;
            var confirmPassword = txtConfirmPassword.Text;

            if (string.IsNullOrEmpty(login) || string.IsNullOrEmpty(fullName) ||
                string.IsNullOrEmpty(password) || string.IsNullOrEmpty(confirmPassword))
            {
                lblMessage.Text = "Все поля обязательны для заполнения";
                return;
            }

            if (password != confirmPassword)
            {
                lblMessage.Text = "Пароли не совпадают";
                return;
            }

            // Блокируем кнопку
            btnRegister.Enabled = false;
            lblMessage.Text = "Регистрация...";
            lblMessage.ForeColor = Color.Blue;

            try
            {
                // Вызов сервиса регистрации
                var result = await _authService.RegisterUserAsync(login, password, fullName);

                if (result.Success)
                {
                    lblMessage.Text = "Регистрация успешна!";
                    lblMessage.ForeColor = Color.Green;

                    // Задержка для отображения сообщения
                    await Task.Delay(1500);

                    // Вызываем событие
                    UserRegistered?.Invoke(this, EventArgs.Empty);
                    this.Close();
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
                btnRegister.Enabled = true;
            }
        }

        private void BtnCancel_Click(object? sender, EventArgs e)
        {
            this.Close();
        }

        private void RegisterForm_Load(object sender, EventArgs e)
        {
            // Загрузка формы
        }
    }
}