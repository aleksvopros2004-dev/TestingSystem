using TestingSystem.Core.Models;
using TestingSystem.Data.Repositories;
using TestingSystem.Services.Interfaces;

namespace TestingSystem.WindowsForms
{
    public partial class UserManagementForm : Form
    {
        private readonly IAuthService _authService;
        private readonly IUserRepository _userRepository;
        private readonly User _currentAdmin;
        private List<User> _users = new();

        public UserManagementForm(IAuthService authService, IUserRepository userRepository, User currentAdmin)
        {
            _authService = authService;
            _userRepository = userRepository;
            _currentAdmin = currentAdmin;
            InitializeComponent();
            LoadUsersAsync();
        }

        private async Task LoadUsersAsync()
        {
            try
            {
                listViewUsers.Items.Clear();
                _users = await _userRepository.GetAllAsync();
                Console.WriteLine($"Загружено пользователей: {_users.Count}");

                foreach (var user in _users)
                {
                    var item = new ListViewItem(user.Id.ToString());
                    item.SubItems.Add(user.Login);
                    item.SubItems.Add(user.FullName ?? "N/A");
                    item.SubItems.Add(user.Role.ToString());
                    item.SubItems.Add(user.IsActive ? "Активен" : "Заблокирован");
                    item.SubItems.Add(user.CreatedDate.ToString("dd.MM.yyyy"));
                    item.Tag = user.Id;
                    listViewUsers.Items.Add(item);
                    Console.WriteLine($"Пользователь: {user.FullName}, Активен: {user.IsActive}");
                }

                var activeUsers = _users.Count(u => u.IsActive);
                var admins = _users.Count(u => u.Role == UserRole.Admin);
                lblStats.Text = $"Всего пользователей: {_users.Count} | Активных: {activeUsers} | Администраторов: {admins}";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка загрузки пользователей: {ex.Message}", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                Console.WriteLine($"Ошибка загрузки: {ex}");
            }
        }

        private void BtnCreateUser_Click(object? sender, EventArgs e)
        {
            var form = new CreateUserForm(_authService, _currentAdmin);
            form.UserCreated += async (s, e) => await LoadUsersAsync();
            form.ShowDialog();
        }

        private void BtnEdit_Click(object? sender, EventArgs e)
        {
            if (listViewUsers.SelectedItems.Count == 0) return;

            var userId = (int)listViewUsers.SelectedItems[0].Tag;
            var user = _users.FirstOrDefault(u => u.Id == userId);

            if (user == null) return;

            // Нельзя редактировать самого себя через эту форму
            if (user.Id == _currentAdmin.Id)
            {
                MessageBox.Show("Для редактирования своего профиля используйте настройки профиля", "Информация",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            var form = new EditUserForm(_userRepository, user);
            form.UserUpdated += async (s, e) => await LoadUsersAsync();
            form.ShowDialog();
        }

        private async void BtnToggleActive_Click(object? sender, EventArgs e)
        {
            if (listViewUsers.SelectedItems.Count == 0)
            {
                MessageBox.Show("Выберите пользователя для изменения статуса", "Информация",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            var userId = (int)listViewUsers.SelectedItems[0].Tag;
            var user = _users.FirstOrDefault(u => u.Id == userId);

            if (user == null) return;

            // Обновляем текст кнопки
            btnToggleActive.Text = user.IsActive ? "Блокировать" : "Разблокировать";

            var newStatus = !user.IsActive;
            var action = newStatus ? "разблокировать" : "заблокировать";

            var result = MessageBox.Show($"Вы уверены, что хотите {action} пользователя '{user.FullName}'?",
                "Подтверждение", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                try
                {
                    user.IsActive = newStatus;
                    var success = await _userRepository.UpdateAsync(user);

                    if (success)
                    {
                        MessageBox.Show($"Пользователь успешно {(newStatus ? "разблокирован" : "заблокирован")}", "Успех",
                            MessageBoxButtons.OK, MessageBoxIcon.Information);
                        await LoadUsersAsync();
                    }
                    else
                    {
                        MessageBox.Show("Ошибка при изменении статуса пользователя", "Ошибка",
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Ошибка при изменении статуса: {ex.Message}", "Ошибка",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void BtnChangePassword_Click(object? sender, EventArgs e)
        {
            if (listViewUsers.SelectedItems.Count == 0) return;

            var userId = (int)listViewUsers.SelectedItems[0].Tag;
            var user = _users.FirstOrDefault(u => u.Id == userId);

            if (user == null) return;

            var form = new ChangePasswordForm(_authService, user);
            form.ShowDialog();
        }

        private void ListViewUsers_SelectedIndexChanged(object? sender, EventArgs e)
        {
            if (listViewUsers.SelectedItems.Count == 0) return;

            var userId = (int)listViewUsers.SelectedItems[0].Tag;
            var user = _users.FirstOrDefault(u => u.Id == userId);

            if (user != null)
            {
                btnToggleActive.Text = user.IsActive ? "Блокировать" : "Разблокировать";
                btnToggleActive.Enabled = user.Id != _currentAdmin.Id; // Отключаем для себя
            }
        }

        private void BtnRefresh_Click(object? sender, EventArgs e)
        {
            LoadUsersAsync();
        }

        private void UserManagementForm_Load(object sender, EventArgs e)
        {
            // Загрузка формы
        }
    }
}