// TestingSystem.WindowsForms/CreateUserForm.cs
using TestingSystem.Core.Models;
using TestingSystem.Services.Interfaces;

namespace TestingSystem.WindowsForms;

public partial class CreateUserForm : Form
{
    private readonly IAuthService _authService;
    private readonly User _currentAdmin;

    public event EventHandler? UserCreated;

    public CreateUserForm(IAuthService authService, User currentAdmin)
    {
        _authService = authService;
        _currentAdmin = currentAdmin;
        InitializeComponent();
    }

    private void InitializeComponent()
    {
        SuspendLayout();
        // 
        // CreateUserForm
        // 
        ClientSize = new Size(434, 311);
        FormBorderStyle = FormBorderStyle.FixedDialog;
        MaximizeBox = false;
        Name = "CreateUserForm";
        StartPosition = FormStartPosition.CenterParent;
        Text = "Создание нового пользователя";
        Load += CreateUserForm_Load;
        ResumeLayout(false);
    }

    private void CreateControls()
    {
        // Заголовок
        var lblTitle = new Label
        {
            Text = "Создание нового пользователя",
            Location = new Point(20, 20),
            Size = new Size(400, 25),
            Font = new Font("Arial", 12, FontStyle.Bold),
            TextAlign = ContentAlignment.MiddleCenter
        };

        // Логин
        var lblLogin = new Label
        {
            Text = "Логин:",
            Location = new Point(20, 70),
            Size = new Size(100, 20)
        };

        var txtLogin = new TextBox
        {
            Location = new Point(130, 70),
            Size = new Size(250, 20),
            Name = "txtLogin"
        };

        // ФИО
        var lblFullName = new Label
        {
            Text = "ФИО:",
            Location = new Point(20, 100),
            Size = new Size(100, 20)
        };

        var txtFullName = new TextBox
        {
            Location = new Point(130, 100),
            Size = new Size(250, 20),
            Name = "txtFullName"
        };

        // Роль
        var lblRole = new Label
        {
            Text = "Роль:",
            Location = new Point(20, 130),
            Size = new Size(100, 20)
        };

        var cmbRole = new ComboBox
        {
            Location = new Point(130, 130),
            Size = new Size(150, 20),
            DropDownStyle = ComboBoxStyle.DropDownList,
            Name = "cmbRole"
        };
        cmbRole.Items.AddRange(new object[] { UserRole.User, UserRole.Admin });
        cmbRole.SelectedIndex = 0;

        // Пароль
        var lblPassword = new Label
        {
            Text = "Пароль:",
            Location = new Point(20, 160),
            Size = new Size(100, 20)
        };

        var txtPassword = new TextBox
        {
            Location = new Point(130, 160),
            Size = new Size(250, 20),
            PasswordChar = '*',
            Name = "txtPassword"
        };

        // Подтверждение пароля
        var lblConfirmPassword = new Label
        {
            Text = "Подтверждение:",
            Location = new Point(20, 190),
            Size = new Size(100, 20)
        };

        var txtConfirmPassword = new TextBox
        {
            Location = new Point(130, 190),
            Size = new Size(250, 20),
            PasswordChar = '*',
            Name = "txtConfirmPassword"
        };

        // Кнопки
        var btnCreate = new Button
        {
            Text = "Создать",
            Location = new Point(130, 230),
            Size = new Size(100, 30),
            Name = "btnCreate"
        };
        btnCreate.Click += BtnCreate_Click;

        var btnCancel = new Button
        {
            Text = "Отмена",
            Location = new Point(240, 230),
            Size = new Size(80, 30)
        };
        btnCancel.Click += (s, e) => this.Close();

        // Сообщение
        var lblMessage = new Label
        {
            Location = new Point(20, 270),
            Size = new Size(400, 40),
            TextAlign = ContentAlignment.MiddleCenter,
            Name = "lblMessage",
            ForeColor = Color.Red
        };

        this.Controls.AddRange(new Control[]
        {
            lblTitle,
            lblLogin, txtLogin,
            lblFullName, txtFullName,
            lblRole, cmbRole,
            lblPassword, txtPassword,
            lblConfirmPassword, txtConfirmPassword,
            btnCreate, btnCancel,
            lblMessage
        });
    }

    private async void BtnCreate_Click(object? sender, EventArgs e)
    {
        var txtLogin = this.Controls.Find("txtLogin", true).FirstOrDefault() as TextBox;
        var txtFullName = this.Controls.Find("txtFullName", true).FirstOrDefault() as TextBox;
        var cmbRole = this.Controls.Find("cmbRole", true).FirstOrDefault() as ComboBox;
        var txtPassword = this.Controls.Find("txtPassword", true).FirstOrDefault() as TextBox;
        var txtConfirmPassword = this.Controls.Find("txtConfirmPassword", true).FirstOrDefault() as TextBox;
        var lblMessage = this.Controls.Find("lblMessage", true).FirstOrDefault() as Label;
        var btnCreate = this.Controls.Find("btnCreate", true).FirstOrDefault() as Button;

        if (txtLogin == null || txtFullName == null || cmbRole == null ||
            txtPassword == null || txtConfirmPassword == null || lblMessage == null || btnCreate == null)
            return;

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

    private void CreateUserForm_Load(object sender, EventArgs e)
    {

    }
}