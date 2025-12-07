using TestingSystem.Core.Models;
using TestingSystem.Services.Interfaces;

namespace TestingSystem.WindowsForms
{
    public partial class CreateUserForm : Form
    {
        private readonly IAuthService _authService;
        private readonly User _currentAdmin;

        public event EventHandler? UserCreated;
        private void LoadRoles()
        {
            cmbRole.Items.Clear();
            cmbRole.Items.Add(UserRole.Admin);
            cmbRole.Items.Add(UserRole.User);
            cmbRole.SelectedIndex = 0;
        }

        public CreateUserForm(IAuthService authService, User currentAdmin)
        {
            _authService = authService;
            _currentAdmin = currentAdmin;
            InitializeComponent();

            LoadRoles();
        }

        private async void BtnCreate_Click(object? sender, EventArgs e)
        {
            // Валидация
            var login = txtLogin.Text.Trim();
            var fullName = txtFullName.Text.Trim();
            var role = (UserRole)cmbRole.SelectedItem;
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
            btnCreate.Enabled = false;
            lblMessage.Text = "Создание пользователя...";
            lblMessage.ForeColor = Color.Blue;

            try
            {
                // Вызов сервиса регистрации
                var result = await _authService.RegisterAdminAsync(_currentAdmin, password, login, fullName, role);

                if (result.Success)
                {
                    lblMessage.Text = "Пользователь успешно создан!";
                    lblMessage.ForeColor = Color.Green;

                    // Задержка для отображения сообщения
                    await Task.Delay(1500);

                    // Вызываем событие
                    UserCreated?.Invoke(this, EventArgs.Empty);
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
                btnCreate.Enabled = true;
            }
        }

        private void BtnCancel_Click(object? sender, EventArgs e)
        {
            this.Close();
        }

        private void CreateUserForm_Load(object sender, EventArgs e)
        {
            // Загрузка формы
        }
    }
}