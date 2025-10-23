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
    public partial class CreateTestForm : Form
    {
        private readonly ITestService _testService;
        private readonly User _currentUser;

        public event EventHandler? TestCreated;

        public CreateTestForm(ITestService testService, User currentUser)
        {
            _testService = testService;
            _currentUser = currentUser;
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            this.Text = "Создание нового теста";
            this.Size = new Size(500, 400);
            this.StartPosition = FormStartPosition.CenterParent;
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;

            CreateControls();
        }

        private void CreateControls()
        {
            // Название теста
            var lblTitle = new Label
            {
                Text = "Название теста:",
                Location = new Point(20, 20),
                Size = new Size(150, 20)
            };

            var txtTitle = new TextBox
            {
                Location = new Point(180, 20),
                Size = new Size(280, 20),
                Name = "txtTitle"
            };

            // Описание теста
            var lblDescription = new Label
            {
                Text = "Описание:",
                Location = new Point(20, 60),
                Size = new Size(150, 20)
            };

            var txtDescription = new TextBox
            {
                Location = new Point(180, 60),
                Size = new Size(280, 60),
                Multiline = true,
                ScrollBars = ScrollBars.Vertical,
                Name = "txtDescription"
            };

            // Ограничение по времени
            var lblTimeLimit = new Label
            {
                Text = "Ограничение по времени:",
                Location = new Point(20, 140),
                Size = new Size(150, 20)
            };

            var numHours = new NumericUpDown
            {
                Location = new Point(180, 140),
                Size = new Size(50, 20),
                Minimum = 0,
                Maximum = 5,
                Value = 0,
                Name = "numHours"
            };

            var lblHours = new Label
            {
                Text = "часов",
                Location = new Point(235, 140),
                Size = new Size(40, 20)
            };

            var numMinutes = new NumericUpDown
            {
                Location = new Point(280, 140),
                Size = new Size(50, 20),
                Minimum = 0,
                Maximum = 59,
                Value = 30,
                Name = "numMinutes"
            };

            var lblMinutes = new Label
            {
                Text = "минут",
                Location = new Point(335, 140),
                Size = new Size(40, 20)
            };

            // Настройки
            var chkRandomQuestions = new CheckBox
            {
                Text = "Случайный порядок вопросов",
                Location = new Point(20, 180),
                Size = new Size(250, 20),
                Name = "chkRandomQuestions",
                Checked = true
            };

            var chkRandomAnswers = new CheckBox
            {
                Text = "Случайный порядок ответов",
                Location = new Point(20, 210),
                Size = new Size(250, 20),
                Name = "chkRandomAnswers",
                Checked = true
            };

            var chkActive = new CheckBox
            {
                Text = "Активировать тест сразу",
                Location = new Point(20, 240),
                Size = new Size(250, 20),
                Name = "chkActive",
                Checked = true
            };

            // Кнопки
            var btnCreate = new Button
            {
                Text = "Создать",
                Location = new Point(180, 290),
                Size = new Size(100, 30),
                Name = "btnCreate"
            };
            btnCreate.Click += BtnCreate_Click;

            var btnCancel = new Button
            {
                Text = "Отмена",
                Location = new Point(290, 290),
                Size = new Size(100, 30)
            };
            btnCancel.Click += (s, e) => this.Close();

            // Сообщение
            var lblMessage = new Label
            {
                Location = new Point(20, 330),
                Size = new Size(440, 20),
                TextAlign = ContentAlignment.MiddleCenter,
                Name = "lblMessage",
                ForeColor = Color.Red
            };

            this.Controls.AddRange(new Control[]
            {
            lblTitle, txtTitle,
            lblDescription, txtDescription,
            lblTimeLimit, numHours, lblHours, numMinutes, lblMinutes,
            chkRandomQuestions, chkRandomAnswers, chkActive,
            btnCreate, btnCancel, lblMessage
            });
        }

        private async void BtnCreate_Click(object? sender, EventArgs e)
        {
            var txtTitle = this.Controls.Find("txtTitle", true).FirstOrDefault() as TextBox;
            var txtDescription = this.Controls.Find("txtDescription", true).FirstOrDefault() as TextBox;
            var numHours = this.Controls.Find("numHours", true).FirstOrDefault() as NumericUpDown;
            var numMinutes = this.Controls.Find("numMinutes", true).FirstOrDefault() as NumericUpDown;
            var chkRandomQuestions = this.Controls.Find("chkRandomQuestions", true).FirstOrDefault() as CheckBox;
            var chkRandomAnswers = this.Controls.Find("chkRandomAnswers", true).FirstOrDefault() as CheckBox;
            var chkActive = this.Controls.Find("chkActive", true).FirstOrDefault() as CheckBox;
            var lblMessage = this.Controls.Find("lblMessage", true).FirstOrDefault() as Label;
            var btnCreate = this.Controls.Find("btnCreate", true).FirstOrDefault() as Button;

            if (txtTitle == null || lblMessage == null || btnCreate == null) return;

            // Валидация
            if (string.IsNullOrWhiteSpace(txtTitle.Text))
            {
                lblMessage.Text = "Введите название теста";
                return;
            }

            // Блокируем кнопку
            btnCreate.Enabled = false;
            lblMessage.Text = "Создание теста...";
            lblMessage.ForeColor = Color.Blue;

            try
            {
                // Создаем объект теста
                var test = new Test
                {
                    Title = txtTitle.Text.Trim(),
                    Description = txtDescription?.Text?.Trim(),
                    AuthorId = _currentUser.Id,
                    TimeLimit = GetTimeLimit(numHours, numMinutes),
                    IsActive = chkActive?.Checked ?? false,
                    QuestionsOrderRandom = chkRandomQuestions?.Checked ?? true,
                    AnswerOptionsRandom = chkRandomAnswers?.Checked ?? true
                };

                // Сохраняем в базу
                var (success, message, testId) = await _testService.CreateTestAsync(test);

                if (success)
                {
                    lblMessage.Text = "Тест успешно создан!";
                    lblMessage.ForeColor = Color.Green;

                    // Задержка для отображения сообщения
                    await Task.Delay(1000);

                    // Вызываем событие
                    TestCreated?.Invoke(this, EventArgs.Empty);
                    this.Close();
                }
                else
                {
                    lblMessage.Text = message;
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

        private TimeSpan? GetTimeLimit(NumericUpDown? numHours, NumericUpDown? numMinutes)
        {
            if (numHours == null || numMinutes == null)
                return null;

            var hours = (int)numHours.Value;
            var minutes = (int)numMinutes.Value;

            if (hours == 0 && minutes == 0)
                return null;

            return new TimeSpan(hours, minutes, 0);
        }
    }
}
