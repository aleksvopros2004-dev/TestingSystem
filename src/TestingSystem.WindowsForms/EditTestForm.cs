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

        private void LoadTestData()
        {
            txtTitle.Text = _test.Title;
            txtDescription.Text = _test.Description;

            if (_test.TimeLimit.HasValue)
            {
                numHours.Value = _test.TimeLimit.Value.Hours;
                numMinutes.Value = _test.TimeLimit.Value.Minutes;
            }

            chkRandomQuestions.Checked = _test.QuestionsOrderRandom;
            chkRandomAnswers.Checked = _test.AnswerOptionsRandom;
            chkActive.Checked = _test.IsActive;
        }

        private async void BtnSave_Click(object? sender, EventArgs e)
        {
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
                _test.TimeLimit = GetTimeLimit();
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

        private TimeSpan? GetTimeLimit()
        {
            var hours = (int)numHours.Value;
            var minutes = (int)numMinutes.Value;

            if (hours == 0 && minutes == 0)
                return null;

            return new TimeSpan(hours, minutes, 0);
        }

        private void BtnCancel_Click(object? sender, EventArgs e)
        {
            this.Close();
        }
    }
}