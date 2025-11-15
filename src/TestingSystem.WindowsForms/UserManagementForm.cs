// TestingSystem.WindowsForms/UserManagementForm.cs
using TestingSystem.Core.Models;
using TestingSystem.Data.Repositories;
using TestingSystem.Services.Interfaces;

namespace TestingSystem.WindowsForms;

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

    private void InitializeComponent()
    {
        this.Text = "Управление пользователями";
        this.Size = new Size(800, 500);
        this.StartPosition = FormStartPosition.CenterParent;
        CreateControls();
    }

    private void CreateControls()
    {
        // Панель инструментов
        var toolStrip = new ToolStrip();
        var btnCreateUser = new ToolStripButton("Создать пользователя");
        var btnRefresh = new ToolStripButton("Обновить");

        btnCreateUser.Click += BtnCreateUser_Click;
        btnRefresh.Click += async (s, e) => await LoadUsersAsync();

        toolStrip.Items.AddRange(new ToolStripItem[] { btnCreateUser, btnRefresh });

        // Список пользователей
        var listView = new ListView
        {
            Location = new Point(20, 40),
            Size = new Size(600, 300),
            View = View.Details,
            FullRowSelect = true,
            GridLines = true,
            Name = "listUsers"
        };

        listView.Columns.Add("ID", 50);
        listView.Columns.Add("Логин", 100);
        listView.Columns.Add("ФИО", 200);
        listView.Columns.Add("Роль", 80);
        listView.Columns.Add("Статус", 80);
        listView.Columns.Add("Дата регистрации", 120);

        listView.SelectedIndexChanged += ListView_SelectedIndexChanged;

        // Кнопки управления
        var btnEdit = new Button { Text = "Редактировать", Location = new Point(630, 40), Size = new Size(120, 30) };
        var btnToggleActive = new Button { Text = "Блокировать", Location = new Point(630, 80), Size = new Size(120, 30) };
        var btnChangePassword = new Button { Text = "Сменить пароль", Location = new Point(630, 120), Size = new Size(120, 30) };

        btnEdit.Click += BtnEdit_Click;
        btnToggleActive.Click += BtnToggleActive_Click;
        btnChangePassword.Click += BtnChangePassword_Click;

        // Статистика
        var lblStats = new Label
        {
            Location = new Point(20, 350),
            Size = new Size(400, 40),
            Name = "lblStats",
            Font = new Font("Arial", 9)
        };

        this.Controls.AddRange(new Control[]
        {
            toolStrip, listView,
            btnEdit, btnToggleActive, btnChangePassword,
            lblStats
        });
    }

    // Добавьте метод для обновления текста кнопки при выборе пользователя
    private void ListView_SelectedIndexChanged(object? sender, EventArgs e)
    {
        var listView = this.Controls.Find("listUsers", true).FirstOrDefault() as ListView;
        var btnToggleActive = this.Controls.Find("btnToggleActive", true).FirstOrDefault() as Button;

        if (listView?.SelectedItems.Count == 0 || btnToggleActive == null) return;

        var userId = (int)listView.SelectedItems[0].Tag;
        var user = _users.FirstOrDefault(u => u.Id == userId);

        if (user != null)
        {
            btnToggleActive.Text = user.IsActive ? "Блокировать" : "Разблокировать";
            btnToggleActive.Enabled = user.Id != _currentAdmin.Id; // Отключаем для себя
        }
    }

    private async Task LoadUsersAsync()
    {
        var listView = this.Controls.Find("listUsers", true).FirstOrDefault() as ListView;
        var lblStats = this.Controls.Find("lblStats", true).FirstOrDefault() as Label;

        if (listView == null || lblStats == null) return;

        try
        {
            listView.Items.Clear();
            _users = await _userRepository.GetAllAsync();

            Console.WriteLine($"Загружено пользователей: {_users.Count}");

            foreach (var user in _users)
            {
                var item = new ListViewItem(user.Id.ToString());
                item.SubItems.Add(user.Login);
                item.SubItems.Add(user.FullName ?? "N/A"); // Защита от null
                item.SubItems.Add(user.Role.ToString());
                item.SubItems.Add(user.IsActive ? "Активен" : "Заблокирован");
                item.SubItems.Add(user.CreatedDate.ToString("dd.MM.yyyy"));
                item.Tag = user.Id;
                listView.Items.Add(item);

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
        var listView = this.Controls.Find("listUsers", true).FirstOrDefault() as ListView;
        if (listView?.SelectedItems.Count == 0) return;

        var userId = (int)listView!.SelectedItems[0].Tag;
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
        var listView = this.Controls.Find("listUsers", true).FirstOrDefault() as ListView;
        if (listView?.SelectedItems.Count == 0)
        {
            MessageBox.Show("Выберите пользователя для изменения статуса", "Информация",
                MessageBoxButtons.OK, MessageBoxIcon.Information);
            return;
        }

        var userId = (int)listView!.SelectedItems[0].Tag;
        var user = _users.FirstOrDefault(u => u.Id == userId);
        if (user == null) return;

        // Обновляем текст кнопки в зависимости от текущего статуса
        var btnToggleActive = this.Controls.Find("btnToggleActive", true).FirstOrDefault() as Button;
        if (btnToggleActive != null)
        {
            btnToggleActive.Text = user.IsActive ? "Блокировать" : "Разблокировать";
        }

        // Остальной код без изменений...
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
        var listView = this.Controls.Find("listUsers", true).FirstOrDefault() as ListView;
        if (listView?.SelectedItems.Count == 0) return;

        var userId = (int)listView!.SelectedItems[0].Tag;
        var user = _users.FirstOrDefault(u => u.Id == userId);
        if (user == null) return;

        var form = new ChangePasswordForm(_authService, user);
        form.ShowDialog();
    }
}