using TestingSystem.Core.Models;
using TestingSystem.Services.Interfaces;

namespace TestingSystem.WindowsForms;

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

    private void InitializeComponent()
    {
        this.Text = $"Смена пароля для {_user.FullName}";
        this.Size = new Size(400, 250);
        this.StartPosition = FormStartPosition.CenterParent;
        this.FormBorderStyle = FormBorderStyle.FixedDialog;
        this.MaximizeBox = false;

        CreateControls();
    }

    private void CreateControls()
    {
        // Новый пароль
        var lblNewPassword = new Label
        {
            Text = "Новый пароль:",
            Location = new Point(20, 30),
            Size = new Size(100, 20)
        };

        var txtNewPassword = new TextBox
        {
            Location = new Point(130, 30),
            Size = new Size(200, 20),
            PasswordChar = '*',
            Name = "txtNewPassword"
        };

        // Подтверждение пароля
        var lblConfirmPassword = new Label
        {
            Text = "Подтверждение:",
            Location = new Point(20, 60),
            Size = new Size(100, 20)
        };

        var txtConfirmPassword = new TextBox
        {
            Location = new Point(130, 60),
            Size = new Size(200, 20),
            PasswordChar = '*',
            Name = "txtConfirmPassword"
        };

        // Кнопки
        var btnChange = new Button
        {
            Text = "Сменить пароль",
            Location = new Point(130, 100),
            Size = new Size(120, 30),
            Name = "btnChange"
        };
        btnChange.Click += BtnChange_Click;

        var btnCancel = new Button
        {
            Text = "Отмена",
            Location = new Point(260, 100),
            Size = new Size(80, 30)
        };
        btnCancel.Click += (s, e) => this.Close();

        // Сообщение
        var lblMessage = new Label
        {
            Location = new Point(20, 140),
            Size = new Size(350, 40),
            TextAlign = ContentAlignment.MiddleCenter,
            Name = "lblMessage",
            ForeColor = Color.Red
        };

        this.Controls.AddRange(new Control[]
        {
            lblNewPassword, txtNewPassword,
            lblConfirmPassword, txtConfirmPassword,
            btnChange, btnCancel,
            lblMessage
        });
    }

    // TestingSystem.WindowsForms/ChangePasswordForm.cs
    private async void BtnChange_Click(object? sender, EventArgs e)
    {
        var txtNewPassword = this.Controls.Find("txtNewPassword", true).FirstOrDefault() as TextBox;
        var txtConfirmPassword = this.Controls.Find("txtConfirmPassword", true).FirstOrDefault() as TextBox;
        var lblMessage = this.Controls.Find("lblMessage", true).FirstOrDefault() as Label;
        var btnChange = this.Controls.Find("btnChange", true).FirstOrDefault() as Button;

        if (txtNewPassword == null || txtConfirmPassword == null || lblMessage == null || btnChange == null)
            return;

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
            // Используем исправленный метод без проверки старого пароля
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
}