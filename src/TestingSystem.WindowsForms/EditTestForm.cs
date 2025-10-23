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
    public partial class EditTestForm : Form
    {
        private readonly ITestService _testService;
        private readonly Test _test;

        public event EventHandler? TestUpdated;

        public EditTestForm(ITestService testService, Test test)
        {
            _testService = testService;
            _test = test;
            InitializeComponent();
            LoadTestData();
        }

        private void InitializeComponent()
        {
            this.Text = $"Редактирование теста: {_test.Title}";
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
                Name = "chkRandomQuestions"
            };

            var chkRandomAnswers = new CheckBox
            {
                Text = "Случайный порядок ответов",
                Location = new Point(20, 210),
                Size = new Size(250, 20),
                Name = "chkRandomAnswers"
            };

            var chkActive = new CheckBox
            {
                Text = "Активировать тест",
                Location = new Point(20, 240),
                Size = new Size(250, 20),
                Name = "chkActive"
            };

            // Кнопки
            var btnSave = new Button
            {
                Text = "Сохранить",
                Location = new Point(180, 290),
                Size = new Size(100, 30),
                Name = "btnSave"
            };
            btnSave.Click += BtnSave_Click;

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
            btnSave, btnCancel, lblMessage
            });
        }

        private void LoadTestData()
        {
            var txtTitle = this.Controls.Find("txtTitle", true).FirstOrDefault() as TextBox;
            var txtDescription = this.Controls.Find("txtDescription", true).FirstOrDefault() as TextBox;
            var numHours = this.Controls.Find("numHours", true).FirstOrDefault() as NumericUpDown;
            var numMinutes = this.Controls.Find("numMinutes", true).FirstOrDefault() as NumericUpDown;
            var chkRandomQuestions = this.Controls.Find("chkRandomQuestions", true).FirstOrDefault() as CheckBox;
            var chkRandomAnswers = this.Controls.Find("chkRandomAnswers", true).FirstOrDefault() as CheckBox;
            var chkActive = this.Controls.Find("chkActive", true).FirstOrDefault() as CheckBox;

            if (txtTitle != null) txtTitle.Text = _test.Title;
            if (txtDescription != null) txtDescription.Text = _test.Description;

            if (_test.TimeLimit.HasValue)
            {
                if (numHours != null) numHours.Value = _test.TimeLimit.Value.Hours;
                if (numMinutes != null) numMinutes.Value = _test.TimeLimit.Value.Minutes;
            }

            if (chkRandomQuestions != null) chkRandomQuestions.Checked = _test.QuestionsOrderRandom;
            if (chkRandomAnswers != null) chkRandomAnswers.Checked = _test.AnswerOptionsRandom;
            if (chkActive != null) chkActive.Checked = _test.IsActive;
        }

        private async void BtnSave_Click(object? sender, EventArgs e)
        {
            var txtTitle = this.Controls.Find("txtTitle", true).FirstOrDefault() as TextBox;
            var txtDescription = this.Controls.Find("txtDescription", true).FirstOrDefault() as TextBox;
            var numHours = this.Controls.Find("numHours", true).FirstOrDefault() as NumericUpDown;
            var numMinutes = this.Controls.Find("numMinutes", true).FirstOrDefault() as NumericUpDown;
            var chkRandomQuestions = this.Controls.Find("chkRandomQuestions", true).FirstOrDefault() as CheckBox;
            var chkRandomAnswers = this.Controls.Find("chkRandomAnswers", true).FirstOrDefault() as CheckBox;
            var chkActive = this.Controls.Find("chkActive", true).FirstOrDefault() as CheckBox;
            var lblMessage = this.Controls.Find("lblMessage", true).FirstOrDefault() as Label;
            var btnSave = this.Controls.Find("btnSave", true).FirstOrDefault() as Button;

            if (txtTitle == null || lblMessage == null || btnSave == null) return;

            // Валидация
            if (string.IsNullOrWhiteSpace(txtTitle.Text))
            {
                lblMessage.Text = "Введите название теста";
                return;
            }

            // Блокируем кнопку
            btnSave.Enabled = false;
            lblMessage.Text = "Сохранение...";
            lblMessage.ForeColor = Color.Blue;

            try
            {
                // Обновляем объект теста
                _test.Title = txtTitle.Text.Trim();
                _test.Description = txtDescription?.Text?.Trim();
                _test.TimeLimit = GetTimeLimit(numHours, numMinutes);
                _test.QuestionsOrderRandom = chkRandomQuestions?.Checked ?? true;
                _test.AnswerOptionsRandom = chkRandomAnswers?.Checked ?? true;
                _test.IsActive = chkActive?.Checked ?? false;

                // Сохраняем в базу
                var (success, message) = await _testService.UpdateTestAsync(_test);

                if (success)
                {
                    lblMessage.Text = "Тест успешно обновлен!";
                    lblMessage.ForeColor = Color.Green;

                    // Задержка для отображения сообщения
                    await Task.Delay(1000);

                    // Вызываем событие
                    TestUpdated?.Invoke(this, EventArgs.Empty);
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
                btnSave.Enabled = true;
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
