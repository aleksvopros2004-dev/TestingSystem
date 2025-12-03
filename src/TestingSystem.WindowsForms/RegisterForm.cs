using TestingSystem.Services.Interfaces;

namespace TestingSystem.WindowsForms;

public partial class RegisterForm : Form
{
    private readonly IAuthService _authService;

    public event EventHandler? UserRegistered;

    public RegisterForm(IAuthService authService)
    {
        _authService = authService;
        InitializeComponent();
    }

    private void InitializeComponent()
    {
        this.Text = "Регистрация нового пользователя";
        this.Size = new Size(450, 350);
        this.StartPosition = FormStartPosition.CenterParent;
        this.FormBorderStyle = FormBorderStyle.FixedDialog;
        this.MaximizeBox = false;

        CreateControls();
    }

    private void CreateControls()
    {
        // Заголовок
        var lblTitle = new Label
        {
            Text = "Регистрация нового пользователя",
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

        // Пароль
        var lblPassword = new Label
        {
            Text = "Пароль:",
            Location = new Point(20, 130),
            Size = new Size(100, 20)
        };

        var txtPassword = new TextBox
        {
            Location = new Point(130, 130),
            Size = new Size(250, 20),
            PasswordChar = '*',
            Name = "txtPassword"
        };

        // Подтверждение пароля
        var lblConfirmPassword = new Label
        {
            Text = "Подтверждение:",
            Location = new Point(20, 160),
            Size = new Size(100, 20)
        };

        var txtConfirmPassword = new TextBox
        {
            Location = new Point(130, 160),
            Size = new Size(250, 20),
            PasswordChar = '*',
            Name = "txtConfirmPassword"
        };

        // Кнопки
        var btnRegister = new Button
        {
            Text = "Зарегистрироваться",
            Location = new Point(130, 200),
            Size = new Size(120, 30),
            Name = "btnRegister"
        };
        btnRegister.Click += BtnRegister_Click;

        var btnCancel = new Button
        {
            Text = "Отмена",
            Location = new Point(260, 200),
            Size = new Size(80, 30)
        };
        btnCancel.Click += (s, e) => this.Close();

        // Сообщение
        var lblMessage = new Label
        {
            Location = new Point(20, 240),
            Size = new Size(400, 40),
            TextAlign = ContentAlignment.MiddleCenter,
            Name = "lblMessage",
            ForeColor = Color.Red
        };

        // Информация
        var lblInfo = new Label
        {
            Text = "После регистрации вы сможете войти в систему с указанными данными",
            Location = new Point(20, 280),
            Size = new Size(400, 20),
            Font = new Font("Arial", 8),
            ForeColor = Color.Gray,
            TextAlign = ContentAlignment.MiddleCenter
        };

        this.Controls.AddRange(new Control[]
        {
            lblTitle,
            lblLogin, txtLogin,
            lblFullName, txtFullName,
            lblPassword, txtPassword,
            lblConfirmPassword, txtConfirmPassword,
            btnRegister, btnCancel,
            lblMessage,
            lblInfo
        });
    }

    private async void BtnRegister_Click(object? sender, EventArgs e)
    {
        var txtLogin = this.Controls.Find("txtLogin", true).FirstOrDefault() as TextBox;
        var txtFullName = this.Controls.Find("txtFullName", true).FirstOrDefault() as TextBox;
        var txtPassword = this.Controls.Find("txtPassword", true).FirstOrDefault() as TextBox;
        var txtConfirmPassword = this.Controls.Find("txtConfirmPassword", true).FirstOrDefault() as TextBox;
        var lblMessage = this.Controls.Find("lblMessage", true).FirstOrDefault() as Label;
        var btnRegister = this.Controls.Find("btnRegister", true).FirstOrDefault() as Button;

        if (txtLogin == null || txtFullName == null || txtPassword == null ||
            txtConfirmPassword == null || lblMessage == null || btnRegister == null)
            return;

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
}