using Microsoft.Extensions.DependencyInjection;
using TestingSystem.Core.Models;
using TestingSystem.Services.Interfaces;

namespace TestingSystem.WindowsForms;

public partial class TestManagementForm : Form
{
    private readonly ITestService _testService;
    private readonly IQuestionService _questionService;
    private readonly User _currentUser;
    private List<Test> _tests = new();

    public TestManagementForm(ITestService testService, IQuestionService questionService, User currentUser)
    {
        _testService = testService;
        _questionService = questionService;
        _currentUser = currentUser;
        InitializeComponent();
        LoadTestsAsync();
    }

    private async Task LoadTestsAsync()
    {
        try
        {
            listViewTests.Items.Clear();
            _tests = (await _testService.GetTestsByAuthorAsync(_currentUser.Id)).ToList();

            foreach (var test in _tests)
            {
                var item = new ListViewItem(test.Id.ToString());
                item.SubItems.Add(test.Title);
                item.SubItems.Add(test.Description ?? "");

                item.SubItems.Add((test.Questions?.Count ?? 0).ToString());

                item.SubItems.Add(test.IsActive ? "Активен" : "Неактивен");
                item.SubItems.Add(test.CreatedDate.ToString("dd.MM.yyyy"));
                item.Tag = test.Id;
                listViewTests.Items.Add(item);
            }

            var activeTests = _tests.Count(t => t.IsActive);
            var totalQuestions = _tests.Sum(t => t.Questions?.Count ?? 0); // Используем Questions.Count

            lblStats.Text = $"Всего тестов: {_tests.Count} | Активных: {activeTests} | Всего вопросов: {totalQuestions}";
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Ошибка загрузки тестов: {ex.Message}", "Ошибка",
                MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }

    private void BtnCreateTest_Click(object? sender, EventArgs e)
    {
        var form = new CreateTestForm(_testService, _currentUser);
        form.TestCreated += async (s, args) => await LoadTestsAsync();
        form.ShowDialog();
    }

    private void BtnEdit_Click(object? sender, EventArgs e)
    {
        if (listViewTests.SelectedItems.Count == 0)
        {
            MessageBox.Show("Выберите тест для редактирования", "Информация",
                MessageBoxButtons.OK, MessageBoxIcon.Information);
            return;
        }

        var testId = (int)listViewTests.SelectedItems[0].Tag;
        var test = _tests.FirstOrDefault(t => t.Id == testId);
        if (test == null) return;

        var form = new EditTestForm(_testService, test);
        form.TestUpdated += async (s, args) => await LoadTestsAsync();
        form.ShowDialog();
    }

    private async void BtnDelete_Click(object? sender, EventArgs e)
    {
        if (listViewTests.SelectedItems.Count == 0)
        {
            MessageBox.Show("Выберите тест для удаления", "Информация",
                MessageBoxButtons.OK, MessageBoxIcon.Information);
            return;
        }

        var testId = (int)listViewTests.SelectedItems[0].Tag;
        var test = _tests.FirstOrDefault(t => t.Id == testId);
        if (test == null) return;

        var result = MessageBox.Show($"Вы уверены, что хотите удалить тест '{test.Title}'?",
            "Подтверждение удаления", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

        if (result == DialogResult.Yes)
        {
            var (success, message) = await _testService.DeleteTestAsync(testId);
            MessageBox.Show(message, success ? "Успех" : "Ошибка",
                MessageBoxButtons.OK, success ? MessageBoxIcon.Information : MessageBoxIcon.Error);
            if (success) await LoadTestsAsync();
        }
    }

    private async void BtnToggleActive_Click(object? sender, EventArgs e)
    {
        if (listViewTests.SelectedItems.Count == 0)
        {
            MessageBox.Show("Выберите тест для изменения статуса", "Информация",
                MessageBoxButtons.OK, MessageBoxIcon.Information);
            return;
        }

        var testId = (int)listViewTests.SelectedItems[0].Tag;
        var test = _tests.FirstOrDefault(t => t.Id == testId);
        if (test == null) return;

        var newStatus = !test.IsActive;
        var (success, message) = await _testService.ToggleTestActivationAsync(testId, newStatus);
        MessageBox.Show(message, success ? "Успех" : "Ошибка",
            MessageBoxButtons.OK, success ? MessageBoxIcon.Information : MessageBoxIcon.Error);
        if (success) await LoadTestsAsync();
    }

    private void BtnManageQuestions_Click(object? sender, EventArgs e)
    {
        if (listViewTests.SelectedItems.Count == 0)
        {
            MessageBox.Show("Выберите тест для управления вопросами", "Информация",
                MessageBoxButtons.OK, MessageBoxIcon.Information);
            return;
        }

        var testId = (int)listViewTests.SelectedItems[0].Tag;
        var test = _tests.FirstOrDefault(t => t.Id == testId);
        if (test == null) return;

        // Получаем сервис изображений
        var imageService = Program.ServiceProvider.GetRequiredService<IImageService>();
        var form = new QuestionManagementForm(_questionService, imageService, test);
        form.ShowDialog();
    }

    private void BtnRefresh_Click(object? sender, EventArgs e)
    {
        LoadTestsAsync();
    }
}