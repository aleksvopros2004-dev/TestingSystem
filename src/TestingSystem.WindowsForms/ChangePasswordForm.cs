using TestingSystem.Core.Models;
using TestingSystem.Services.Interfaces;

namespace TestingSystem.WindowsForms
{
    public partial class ChangePasswordForm : Form
    {
        private readonly IAuthService _authService;
        private readonly User _user;

        public ChangePasswordForm(IAuthService authService, User user)
        {
            _authService = authService;
            _user = user;
            InitializeComponent();
        }

        private async void BtnChange_Click(object? sender, EventArgs e)
        {
            var newPassword = txtNewPassword.Text;
            var confirmPassword = txtConfirmPassword.Text;

            if (string.IsNullOrEmpty(newPassword) || string.IsNullOrEmpty(confirmPassword))
            {
                lblMessage.Text = "Все поля обязательны для заполнения";
                return;
            }

            if (newPassword != confirmPassword)
            {
                lblMessage.Text = "Пароли не совпадают";
                return;
            }

            if (newPassword.Length < 6)
            {
                lblMessage.Text = "Пароль должен содержать минимум 6 символов";
                return;
            }

            // Блокируем кнопку
            btnChange.Enabled = false;
            lblMessage.Text = "Смена пароля...";
            lblMessage.ForeColor = Color.Blue;

            try
            {
                var result = await _authService.ChangePasswordAsync(_user.Id, newPassword);

                if (result.Success)
                {
                    lblMessage.Text = "Пароль успешно изменен!";
                    lblMessage.ForeColor = Color.Green;

                    await Task.Delay(1500);
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
                btnChange.Enabled = true;
            }
        }

        private void BtnCancel_Click(object? sender, EventArgs e)
        {
            this.Close();
        }
    }
}