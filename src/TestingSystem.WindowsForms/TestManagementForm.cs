using Microsoft.Extensions.DependencyInjection;
using System.Diagnostics;
using TestingSystem.Core.Models;
using TestingSystem.Data.Repositories;
using TestingSystem.Services.Interfaces;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

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

        var loadStopwatch = Stopwatch.StartNew();

        if (_currentUser.Role == UserRole.User)
        {
            this.Text = "Доступные тесты";
            btnCreateTestTool.Visible = false;
            btnEdit.Visible = false;
            btnDelete.Visible = false;
            btnToggleActive.Visible = false;
            btnManageQuestions.Visible = false;

            // Показываем кнопки для прохождения тестов
            btnStartTest.Visible = true;
            btnStartTest.Enabled = false; 

            // Добавляем обработчик выбора теста
            listViewTests.SelectedIndexChanged += ListViewTests_SelectedIndexChanged;
        }
        else
        {
            this.Text = "Управление тестами";
            btnCreateTestTool.Visible = true;
            btnEdit.Visible = true;
            btnDelete.Visible = true;
            btnToggleActive.Visible = true;
            btnManageQuestions.Visible = true;
            btnStartTest.Visible = false;
        }

        LoadTestsWithTiming();

        loadStopwatch.Stop();
        if (loadStopwatch.ElapsedMilliseconds > 6000)
        {
            MessageBox.Show($"Время загрузки формы: {loadStopwatch.ElapsedMilliseconds} мс\n" +
                           $"Требование: не более 6000 мс\n" +
                           $"⚠ Превышено!", "Время загрузки формы");
        }
    }

    private void ListViewTests_SelectedIndexChanged(object? sender, EventArgs e)
    {
        // Активируем кнопку "Начать тест" только если выбран тест и он активен
        if (listViewTests.SelectedItems.Count > 0)
        {
            var testId = (int)listViewTests.SelectedItems[0].Tag;
            var test = _tests.FirstOrDefault(t => t.Id == testId);
            btnStartTest.Enabled = test != null && test.IsActive;

            if (test != null && !test.IsActive)
            {
                toolTip1.SetToolTip(btnStartTest, "Этот тест неактивен");
            }
            else
            {
                toolTip1.SetToolTip(btnStartTest, "Начать прохождение теста");
            }
        }
        else
        {
            btnStartTest.Enabled = false;
        }
    }

    private async Task LoadTestsWithTiming()
    {
        try
        {
            listViewTests.Items.Clear();

            var dbStopwatch = Stopwatch.StartNew();

            if (_currentUser.Role == UserRole.User)
            {
                // Пользователи видят все активные тесты
                _tests = (await _testService.GetActiveTestsAsync()).ToList();
            }
            else
            {
                // Админы видят все свои тесты
                _tests = (await _testService.GetTestsByAuthorAsync(_currentUser.Id)).ToList();
            }

            dbStopwatch.Stop();

            // Проверяем требования к БД
            if (dbStopwatch.ElapsedMilliseconds > 4000)
            {
                MessageBox.Show($"⚠ Время отклика БД превышено!\n" +
                              $"Фактическое: {dbStopwatch.ElapsedMilliseconds} мс\n" +
                              $"Требование: не более 4000 мс",
                              "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

            foreach (var test in _tests)
            {
                var item = new ListViewItem(test.Id.ToString());
                item.SubItems.Add(test.Title);
                item.SubItems.Add(test.Description ?? "");
                item.SubItems.Add((test.Questions?.Count ?? 0).ToString());

                if (_currentUser.Role == UserRole.User)
                {
                    // Для пользователей показываем только статус активности
                    item.SubItems.Add(test.IsActive ? "Доступен" : "Недоступен");
                }
                else
                {
                    item.SubItems.Add(test.IsActive ? "Активен" : "Неактивен");
                }

                item.SubItems.Add(test.CreatedDate.ToString("dd.MM.yyyy"));
                item.Tag = test.Id;
                listViewTests.Items.Add(item);
            }

            var activeTests = _tests.Count(t => t.IsActive);
            var totalQuestions = _tests.Sum(t => t.Questions?.Count ?? 0);

            if (_currentUser.Role == UserRole.User)
            {
                lblStats.Text = $"Доступно тестов: {activeTests} из {_tests.Count} | Всего вопросов: {totalQuestions}";
            }
            else
            {
                lblStats.Text = $"Всего тестов: {_tests.Count} | Активных: {activeTests} | Всего вопросов: {totalQuestions}";
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Ошибка загрузки тестов: {ex.Message}", "Ошибка",
                MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }

    private void BtnStartTest_Click(object? sender, EventArgs e)
    {
        if (listViewTests.SelectedItems.Count == 0) return;

        var testId = (int)listViewTests.SelectedItems[0].Tag;
        var test = _tests.FirstOrDefault(t => t.Id == testId);
        if (test == null) return;

        if (!test.IsActive)
        {
            MessageBox.Show("Этот тест недоступен для прохождения", "Информация",
                MessageBoxButtons.OK, MessageBoxIcon.Information);
            return;
        }

        // Получаем IStatisticsRepository
        var statisticsRepository = Program.ServiceProvider.GetRequiredService<IStatisticsRepository>();

        // Создаем форму с 4 параметрами
        var testTakingForm = new TestTakingForm(_questionService, statisticsRepository, test, _currentUser);
        testTakingForm.ShowDialog();
    }

    private void BtnCreateTest_Click(object? sender, EventArgs e)
    {
        var actionStopwatch = Stopwatch.StartNew();

        if (_currentUser.Role != UserRole.Admin)
        {
            MessageBox.Show("Только администраторы могут создавать тесты", "Ограничение",
                MessageBoxButtons.OK, MessageBoxIcon.Information);
            return;
        }

        var form = new CreateTestForm(_testService, _currentUser);
        form.TestCreated += async (s, args) => await LoadTestsWithTiming();
        form.ShowDialog();

        actionStopwatch.Stop();
        MessageBox.Show($"Время отклика на действие 'Создать тест': {actionStopwatch.ElapsedMilliseconds} мс\n" +
                       $"Требование: не более 3000 мс\n" +
                       $"{(actionStopwatch.ElapsedMilliseconds <= 3000 ? "✓ Успешно" : "✗ Превышено")}",
                       "Время отклика UI");
    }

    private void BtnEdit_Click(object? sender, EventArgs e)
    {
        var actionStopwatch = Stopwatch.StartNew();

        if (listViewTests.SelectedItems.Count == 0)
        {
            MessageBox.Show("Выберите тест", "Информация",
                MessageBoxButtons.OK, MessageBoxIcon.Information);
            return;
        }

        if (_currentUser.Role != UserRole.Admin)
        {
            MessageBox.Show("Только администраторы могут редактировать тесты", "Ограничение",
                MessageBoxButtons.OK, MessageBoxIcon.Information);
            return;
        }

        var testId = (int)listViewTests.SelectedItems[0].Tag;
        var test = _tests.FirstOrDefault(t => t.Id == testId);
        if (test == null) return;

        var form = new EditTestForm(_testService, test);
        form.TestUpdated += async (s, args) => await LoadTestsWithTiming();
        form.ShowDialog();

        actionStopwatch.Stop();
        MessageBox.Show($"Время отклика на действие 'Редактировать': {actionStopwatch.ElapsedMilliseconds} мс\n" +
                       $"Требование: не более 5000 мс\n" +
                       $"{(actionStopwatch.ElapsedMilliseconds <= 5000 ? "✓ Успешно" : "✗ Превышено")}",
                       "Время отклика CRUD");
    }

    private async void BtnDelete_Click(object? sender, EventArgs e)
    {
        var actionStopwatch = Stopwatch.StartNew();

        if (listViewTests.SelectedItems.Count == 0)
        {
            MessageBox.Show("Выберите тест", "Информация",
                MessageBoxButtons.OK, MessageBoxIcon.Information);
            return;
        }

        if (_currentUser.Role != UserRole.Admin)
        {
            MessageBox.Show("Только администраторы могут удалять тесты", "Ограничение",
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
            var dbStopwatch = Stopwatch.StartNew();
            var (success, message) = await _testService.DeleteTestAsync(testId);
            dbStopwatch.Stop();

            MessageBox.Show(message, success ? "Успех" : "Ошибка",
                MessageBoxButtons.OK, success ? MessageBoxIcon.Information : MessageBoxIcon.Error);

            if (success)
            {
                await LoadTestsWithTiming();
                MessageBox.Show($"Время операции удаления в БД: {dbStopwatch.ElapsedMilliseconds} мс");
            }
        }

        actionStopwatch.Stop();
        MessageBox.Show($"Время отклика на действие 'Удалить': {actionStopwatch.ElapsedMilliseconds} мс\n" +
                       $"Требование: не более 3000 мс\n" +
                       $"{(actionStopwatch.ElapsedMilliseconds <= 3000 ? "✓ Успешно" : "✗ Превышено")}",
                       "Время отклика UI");
    }

    private async void BtnToggleActive_Click(object? sender, EventArgs e)
    {
        var actionStopwatch = Stopwatch.StartNew();

        if (listViewTests.SelectedItems.Count == 0)
        {
            MessageBox.Show("Выберите тест", "Информация",
                MessageBoxButtons.OK, MessageBoxIcon.Information);
            return;
        }

        if (_currentUser.Role != UserRole.Admin)
        {
            MessageBox.Show("Только администраторы могут изменять статус тестов", "Ограничение",
                MessageBoxButtons.OK, MessageBoxIcon.Information);
            return;
        }

        var testId = (int)listViewTests.SelectedItems[0].Tag;
        var test = _tests.FirstOrDefault(t => t.Id == testId);
        if (test == null) return;

        var newStatus = !test.IsActive;

        var dbStopwatch = Stopwatch.StartNew();
        var (success, message) = await _testService.ToggleTestActivationAsync(testId, newStatus);
        dbStopwatch.Stop();

        MessageBox.Show(message, success ? "Успех" : "Ошибка",
            MessageBoxButtons.OK, success ? MessageBoxIcon.Information : MessageBoxIcon.Error);

        if (success)
        {
            await LoadTestsWithTiming();
            MessageBox.Show($"Время операции активации в БД: {dbStopwatch.ElapsedMilliseconds} мс");
        }

        actionStopwatch.Stop();
        MessageBox.Show($"Время отклика на действие 'Активировать': {actionStopwatch.ElapsedMilliseconds} мс\n" +
                       $"Требование: не более 3000 мс\n" +
                       $"{(actionStopwatch.ElapsedMilliseconds <= 3000 ? "✓ Успешно" : "✗ Превышено")}",
                       "Время отклика UI");
    }

    private void BtnManageQuestions_Click(object? sender, EventArgs e)
    {
        var actionStopwatch = Stopwatch.StartNew();

        if (listViewTests.SelectedItems.Count == 0)
        {
            MessageBox.Show("Выберите тест", "Информация",
                MessageBoxButtons.OK, MessageBoxIcon.Information);
            return;
        }

        var testId = (int)listViewTests.SelectedItems[0].Tag;
        var test = _tests.FirstOrDefault(t => t.Id == testId);
        if (test == null) return;

        var imageService = Program.ServiceProvider.GetRequiredService<IImageService>();
        var form = new QuestionManagementForm(_questionService, imageService, test, _currentUser);
        form.ShowDialog();

        actionStopwatch.Stop();
        MessageBox.Show($"Время отклика на действие 'Управление вопросами': {actionStopwatch.ElapsedMilliseconds} мс\n" +
                       $"Требование: не более 3000 мс\n" +
                       $"{(actionStopwatch.ElapsedMilliseconds <= 3000 ? "✓ Успешно" : "✗ Превышено")}",
                       "Время отклика UI");
    }

    private void BtnRefresh_Click(object? sender, EventArgs e)
    {
        var actionStopwatch = Stopwatch.StartNew();
        LoadTestsWithTiming();
        actionStopwatch.Stop();

        MessageBox.Show($"Время отклика на действие 'Обновить': {actionStopwatch.ElapsedMilliseconds} мс\n" +
                       $"Требование: не более 3000 мс\n" +
                       $"{(actionStopwatch.ElapsedMilliseconds <= 3000 ? "✓ Успешно" : "✗ Превышено")}",
                       "Время отклика UI");
    }
}