using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
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
        private void InitializeComponent()
        {
            // Основные настройки формы
            this.Text = "Система тестирования - Вход";
            this.Size = new Size(400, 350);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;

            CreateControls();
            SetupControls();
        }

        private void CreateControls()
        {
            this.Text = "Система тестирования - Вход";
            this.Size = new Size(400, 350); // Увеличили высоту для кнопки регистрации
            this.StartPosition = FormStartPosition.CenterScreen;
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;

            // Заголовок
            var lblTitle = new Label
            {
                Text = "Вход в систему тестирования",
                Location = new Point(20, 20),
                Size = new Size(360, 25),
                Font = new Font("Arial", 12, FontStyle.Bold),
                TextAlign = ContentAlignment.MiddleCenter
            };

            // Лейбл логина
            var lblLogin = new Label
            {
                Text = "Логин:",
                Location = new Point(50, 70),
                Size = new Size(100, 20),
                TextAlign = ContentAlignment.MiddleLeft
            };

            // Поле ввода логина
            var txtLogin = new TextBox
            {
                Location = new Point(150, 70),
                Size = new Size(200, 20),
                Name = "txtLogin"
            };

            // Лейбл пароля
            var lblPassword = new Label
            {
                Text = "Пароль:",
                Location = new Point(50, 100),
                Size = new Size(100, 20),
                TextAlign = ContentAlignment.MiddleLeft
            };

            // Поле ввода пароля
            var txtPassword = new TextBox
            {
                Location = new Point(150, 100),
                Size = new Size(200, 20),
                PasswordChar = '*',
                Name = "txtPassword"
            };

            // Кнопка регистрации
            var btnRegister = new Button
            {
                Text = "Регистрация",
                Location = new Point(50, 140),
                Size = new Size(90, 30),
                Name = "btnRegister"
            };
            btnRegister.Click += BtnRegister_Click;

            // Кнопка входа
            var btnLogin = new Button
            {
                Text = "Войти",
                Location = new Point(150, 140),
                Size = new Size(100, 30),
                Name = "btnLogin"
            };
            btnLogin.Click += BtnLogin_Click;

            // Кнопка выхода
            var btnExit = new Button
            {
                Text = "Выход",
                Location = new Point(260, 140),
                Size = new Size(80, 30)
            };
            btnExit.Click += (s, e) => Application.Exit();


            // Лейбл сообщений
            var lblMessage = new Label
            {
                Location = new Point(50, 190),
                Size = new Size(300, 40),
                ForeColor = Color.Red,
                Name = "lblMessage",
                TextAlign = ContentAlignment.MiddleCenter
            };

            // Информация о тестовом пользователе
            var lblTestInfo = new Label
            {
                Text = "Тестовый аккаунт: admin / admin123",
                Location = new Point(50, 240),
                Size = new Size(300, 20),
                Font = new Font("Arial", 8),
                ForeColor = Color.Gray,
                TextAlign = ContentAlignment.MiddleCenter
            };

            // Добавление элементов на форму
            this.Controls.AddRange(new Control[]
            {
                lblTitle,
                lblLogin, txtLogin,
                lblPassword, txtPassword,
                btnRegister, btnLogin, btnExit,
                lblMessage,
                lblTestInfo
            });
        }

        private void BtnRegister_Click(object? sender, EventArgs e)
        {
            var registerForm = Program.ServiceProvider.GetRequiredService<RegisterForm>();
            registerForm.UserRegistered += (s, e) =>
            {
                var lblMessage = this.Controls.Find("lblMessage", true).FirstOrDefault() as Label;
                if (lblMessage != null)
                {
                    lblMessage.Text = "Регистрация успешна! Теперь вы можете войти.";
                    lblMessage.ForeColor = Color.Green;
                }
            };
            registerForm.ShowDialog();
        }



        private void SetupControls()
        {
            // Установка значений по умолчанию для тестирования
            var txtLogin = this.Controls.Find("txtLogin", true).FirstOrDefault() as TextBox;
            var txtPassword = this.Controls.Find("txtPassword", true).FirstOrDefault() as TextBox;

            if (txtLogin != null) txtLogin.Text = "admin";
            if (txtPassword != null) txtPassword.Text = "admin123";
        }

        private async void BtnLogin_Click(object? sender, EventArgs e)
        {
            var txtLogin = this.Controls.Find("txtLogin", true).FirstOrDefault() as TextBox;
            var txtPassword = this.Controls.Find("txtPassword", true).FirstOrDefault() as TextBox;
            var lblMessage = this.Controls.Find("lblMessage", true).FirstOrDefault() as Label;

            if (txtLogin == null || txtPassword == null || lblMessage == null)
                return;

            var login = txtLogin.Text.Trim();
            var password = txtPassword.Text;

            // Валидация
            if (string.IsNullOrEmpty(login) || string.IsNullOrEmpty(password))
            {
                lblMessage.Text = "Заполните все поля";
                return;
            }

            // Блокируем кнопку во время выполнения
            var btnLogin = this.Controls.Find("btnLogin", true).FirstOrDefault() as Button;
            if (btnLogin != null) btnLogin.Enabled = false;

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
                if (btnLogin != null) btnLogin.Enabled = true;
            }
        }

        private void OpenMainForm()
        {
            if (CurrentUser == null) return;

            // Создаем MainForm и передаем пользователя в конструктор
            var mainForm = new MainForm(CurrentUser);

            Console.WriteLine($"Opening MainForm for user: {CurrentUser.FullName}, Role: {CurrentUser.Role}");

            mainForm.Show();
            this.Hide();
        }

        private void LoginForm_Load(object sender, EventArgs e)
        {

        }
    }
}
