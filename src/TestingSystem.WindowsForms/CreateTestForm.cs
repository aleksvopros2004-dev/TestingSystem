using TestingSystem.Core.Models;
using TestingSystem.Services.Interfaces;

namespace TestingSystem.WindowsForms;

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

    private void BtnCancel_Click(object? sender, EventArgs e)
    {
        this.Close();
    }

    private async void BtnCreate_Click(object? sender, EventArgs e)
    {
        // Валидация
        if (string.IsNullOrWhiteSpace(txtTitle.Text))
        {
            lblMessage.Text = "Введите название теста";
            return;
        }

        // Блокируем кнопку
        btnCreate.Enabled = false;
        lblMessage.Text = "Создание...";
        lblMessage.ForeColor = Color.Blue;

        try
        {
            var test = new Test
            {
                Title = txtTitle.Text.Trim(),
                Description = txtDescription?.Text?.Trim(),
                AuthorId = _currentUser.Id,
                TimeLimit = GetTimeLimit(),
                IsActive = chkActive?.Checked ?? false,
                QuestionsOrderRandom = chkRandomQuestions?.Checked ?? true,
                AnswerOptionsRandom = chkRandomAnswers?.Checked ?? true,
                IsScored = chkIsScored?.Checked ?? true
            };

            var (success, message, testId) = await _testService.CreateTestAsync(test);

            if (success)
            {
                lblMessage.Text = $"Успешно создано (ID: {testId})!";
                lblMessage.ForeColor = Color.Green;
                await Task.Delay(1500);
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
            MessageBox.Show($"Ошибка: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            lblMessage.Text = $"Ошибка: {ex.Message}";
            lblMessage.ForeColor = Color.Red;
        }
        finally
        {
            btnCreate.Enabled = true;
        }
    }

    private TimeSpan? GetTimeLimit()
    {
        var hours = (int)(numHours?.Value ?? 0);
        var minutes = (int)(numMinutes?.Value ?? 0);
        if (hours == 0 && minutes == 0) return null;
        return new TimeSpan(hours, minutes, 0);
    }

    private void lblLimitsInfo_Click(object sender, EventArgs e)
    {

    }

    private void lblMessage_Click(object sender, EventArgs e)
    {

    }
}