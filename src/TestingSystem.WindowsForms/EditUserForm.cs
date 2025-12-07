using TestingSystem.Core.Models;
using TestingSystem.Data.Repositories;

namespace TestingSystem.WindowsForms
{
    public partial class EditUserForm : Form
    {
        private readonly IUserRepository _userRepository;
        private readonly User _user;

        public event EventHandler? UserUpdated;

        public EditUserForm(IUserRepository userRepository, User user)
        {
            _userRepository = userRepository;
            _user = user;
            InitializeComponent();
            LoadUserData();
        }

        private void LoadUserData()
        {
            txtLogin.Text = _user.Login;
            txtFullName.Text = _user.FullName;

            // Загружаем роли в комбобокс
            cmbRole.Items.Clear();
            cmbRole.Items.Add(UserRole.Admin);
            cmbRole.Items.Add(UserRole.User);

            // Устанавливаем текущую роль пользователя
            cmbRole.SelectedItem = _user.Role;

            chkActive.Checked = _user.IsActive;
        }

        private async void BtnSave_Click(object? sender, EventArgs e)
        {
            // Валидация
            var login = txtLogin.Text.Trim();
            var fullName = txtFullName.Text.Trim();
            var role = (UserRole)cmbRole.SelectedItem;

            if (string.IsNullOrEmpty(login) || string.IsNullOrEmpty(fullName))
            {
                lblMessage.Text = "Логин и ФИО обязательны для заполнения";
                return;
            }

            // Блокируем кнопку
            btnSave.Enabled = false;
            lblMessage.Text = "Сохранение...";
            lblMessage.ForeColor = Color.Blue;

            try
            {
                // Обновляем пользователя
                _user.Login = login;
                _user.FullName = fullName;
                _user.Role = role;
                _user.IsActive = chkActive.Checked;

                var success = await _userRepository.UpdateAsync(_user);

                if (success)
                {
                    lblMessage.Text = "Пользователь успешно обновлен!";
                    lblMessage.ForeColor = Color.Green;

                    await Task.Delay(1000);
                    UserUpdated?.Invoke(this, EventArgs.Empty);
                    this.Close();
                }
                else
                {
                    lblMessage.Text = "Ошибка при обновлении пользователя";
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
                btnSave.Enabled = true;
            }
        }

        private void BtnCancel_Click(object? sender, EventArgs e)
        {
            this.Close();
        }
    }
}