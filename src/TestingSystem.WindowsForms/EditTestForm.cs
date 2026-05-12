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

            // 👇 ИЗМЕНЁННАЯ ЗАГРУЗКА ВРЕМЕНИ
            if (_test.TimeLimit.HasValue && _test.TimeLimit.Value.TotalSeconds > 0)
            {
                chkEnableTimeLimit.Checked = true;
                numMinutes.Value = _test.TimeLimit.Value.Minutes;
                numSeconds.Value = _test.TimeLimit.Value.Seconds;
                numMinutes.Enabled = true;
                numSeconds.Enabled = true;
                lblMinutes.Enabled = true;
                lblSeconds.Enabled = true;
            }
            else
            {
                chkEnableTimeLimit.Checked = false;
                numMinutes.Value = 0;
                numSeconds.Value = 0;
                numMinutes.Enabled = false;
                numSeconds.Enabled = false;
                lblMinutes.Enabled = false;
                lblSeconds.Enabled = false;
            }

            chkRandomQuestions.Checked = _test.QuestionsOrderRandom;
            chkRandomAnswers.Checked = _test.AnswerOptionsRandom;
            chkActive.Checked = _test.IsActive;
            chkIsScored.Checked = _test.IsScored;
        }

        private async void BtnSave_Click(object? sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtTitle.Text))
            {
                lblMessage.Text = "Введите название теста";
                return;
            }

            btnSave.Enabled = false;
            lblMessage.Text = "Сохранение...";
            lblMessage.ForeColor = Color.Blue;

            try
            {
                _test.Title = txtTitle.Text.Trim();
                _test.Description = txtDescription?.Text?.Trim();
                _test.TimeLimit = GetTimeLimit();
                _test.QuestionsOrderRandom = chkRandomQuestions?.Checked ?? true;
                _test.AnswerOptionsRandom = chkRandomAnswers?.Checked ?? true;
                _test.IsActive = chkActive?.Checked ?? false;
                _test.IsScored = chkIsScored?.Checked ?? true;

                var (success, message) = await _testService.UpdateTestAsync(_test);

                if (success)
                {
                    lblMessage.Text = "Тест успешно обновлён!";
                    lblMessage.ForeColor = Color.Green;

                    await Task.Delay(1000);

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
                MessageBox.Show($"Ошибка: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                lblMessage.Text = $"Ошибка: {ex.Message}";
                lblMessage.ForeColor = Color.Red;
            }
            finally
            {
                btnSave.Enabled = true;
            }
        }

        // 👇 ИЗМЕНЁННЫЙ МЕТОД: минуты + секунды
        private TimeSpan? GetTimeLimit()
        {
            if (!chkEnableTimeLimit.Checked)
                return null;

            var minutes = (int)numMinutes.Value;
            var seconds = (int)numSeconds.Value;

            if (minutes == 0 && seconds == 0)
                return null;

            return new TimeSpan(0, minutes, seconds);
        }

        private void BtnCancel_Click(object? sender, EventArgs e)
        {
            this.Close();
        }

        private void chkEnableTimeLimit_CheckedChanged(object? sender, EventArgs e)
        {
            bool hasTimeLimit = chkEnableTimeLimit.Checked;
            numMinutes.Enabled = hasTimeLimit;
            numSeconds.Enabled = hasTimeLimit;
            lblMinutes.Enabled = hasTimeLimit;
            lblSeconds.Enabled = hasTimeLimit;

            if (!hasTimeLimit)
            {
                numMinutes.Value = 0;
                numSeconds.Value = 0;
            }
        }
    }
}