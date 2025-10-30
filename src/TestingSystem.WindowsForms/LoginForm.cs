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
            this.Size = new Size(400, 300);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;

            // Создание элементов управления
            CreateControls();
        }

        private void CreateControls()
        {
            // Лейбл логина
            var lblLogin = new Label
            {
                Text = "Логин:",
                Location = new Point(50, 50),
                Size = new Size(100, 20),
                TextAlign = ContentAlignment.MiddleLeft
            };

            // Поле ввода логина
            var txtLogin = new TextBox
            {
                Location = new Point(150, 50),
                Size = new Size(200, 20),
                Name = "txtLogin"
            };

            // Лейбл пароля
            var lblPassword = new Label
            {
                Text = "Пароль:",
                Location = new Point(50, 90),
                Size = new Size(100, 20),
                TextAlign = ContentAlignment.MiddleLeft
            };

            // Поле ввода пароля
            var txtPassword = new TextBox
            {
                Location = new Point(150, 90),
                Size = new Size(200, 20),
                PasswordChar = '*',
                Name = "txtPassword"
            };

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

            // Добавление элементов на форму
            this.Controls.AddRange(new Control[]
            {
            lblLogin, txtLogin,
            lblPassword, txtPassword,
            btnLogin, btnExit,
            lblMessage
            });
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

        /*private void OpenMainForm()
        {
            var mainForm = Program.ServiceProvider.GetRequiredService<MainForm>();
            mainForm.CurrentUser = CurrentUser;
            mainForm.Show();
            this.Hide();
        }*/

        private void OpenMainForm()
        {
            if (CurrentUser == null) return;

            // Создаем MainForm и передаем пользователя в конструктор
            var mainForm = new MainForm(CurrentUser);

            Console.WriteLine($"Opening MainForm for user: {CurrentUser.FullName}, Role: {CurrentUser.Role}");

            mainForm.Show();
            this.Hide();
        }

    }
}
