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

    private void InitializeComponent()
    {
        this.Text = "Управление тестами";
        this.Size = new Size(1000, 600);
        this.StartPosition = FormStartPosition.CenterParent;
        CreateControls();
    }

    private void CreateControls()
    {
        // Панель инструментов
        var toolStrip = new ToolStrip();
        var btnCreateTest = new ToolStripButton("Создать тест");
        var btnRefresh = new ToolStripButton("Обновить");

        btnCreateTest.Click += BtnCreateTest_Click;
        btnRefresh.Click += async (s, e) => await LoadTestsAsync();

        toolStrip.Items.AddRange(new ToolStripItem[] { btnCreateTest, btnRefresh });

        // Список тестов
        var listView = new ListView
        {
            Location = new Point(20, 40),
            Size = new Size(700, 400),
            View = View.Details,
            FullRowSelect = true,
            GridLines = true,
            Name = "listTests"
        };

        listView.Columns.Add("ID", 50);
        listView.Columns.Add("Название", 200);
        listView.Columns.Add("Описание", 250);
        listView.Columns.Add("Вопросов", 80);
        listView.Columns.Add("Статус", 80);
        listView.Columns.Add("Дата создания", 120);

        // Кнопки управления
        var btnEdit = new Button
        {
            Text = "Редактировать",
            Location = new Point(730, 40),
            Size = new Size(150, 30),
            Name = "btnEdit"
        };

        var btnDelete = new Button
        {
            Text = "Удалить",
            Location = new Point(730, 80),
            Size = new Size(150, 30),
            Name = "btnDelete"
        };

        var btnToggleActive = new Button
        {
            Text = "Активировать",
            Location = new Point(730, 120),
            Size = new Size(150, 30),
            Name = "btnToggleActive"
        };

        var btnManageQuestions = new Button
        {
            Text = "Управление вопросами",
            Location = new Point(730, 160),
            Size = new Size(150, 30),
            Name = "btnManageQuestions"
        };

        btnEdit.Click += BtnEdit_Click;
        btnDelete.Click += BtnDelete_Click;
        btnToggleActive.Click += BtnToggleActive_Click;
        btnManageQuestions.Click += BtnManageQuestions_Click;

        // Статистика
        var lblStats = new Label
        {
            Location = new Point(20, 450),
            Size = new Size(400, 40),
            Name = "lblStats",
            Font = new Font("Arial", 9)
        };

        this.Controls.AddRange(new Control[]
        {
            toolStrip, listView,
            btnEdit, btnDelete, btnToggleActive, btnManageQuestions,
            lblStats
        });
    }

    private async Task LoadTestsAsync()
    {
        var listView = this.Controls.Find("listTests", true).FirstOrDefault() as ListView;
        var lblStats = this.Controls.Find("lblStats", true).FirstOrDefault() as Label;

        if (listView == null || lblStats == null) return;

        try
        {
            listView.Items.Clear();
            _tests = (await _testService.GetTestsByAuthorAsync(_currentUser.Id)).ToList();

            foreach (var test in _tests)
            {
                var item = new ListViewItem(test.Id.ToString());
                item.SubItems.Add(test.Title);
                item.SubItems.Add(test.Description ?? "");
                item.SubItems.Add(test.Questions?.Count.ToString() ?? "0");
                item.SubItems.Add(test.IsActive ? "Активен" : "Неактивен");
                item.SubItems.Add(test.CreatedDate.ToString("dd.MM.yyyy"));
                item.Tag = test.Id;
                listView.Items.Add(item);
            }

            var activeTests = _tests.Count(t => t.IsActive);
            var totalQuestions = _tests.Sum(t => t.Questions?.Count ?? 0);
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
        var listView = this.Controls.Find("listTests", true).FirstOrDefault() as ListView;
        if (listView?.SelectedItems.Count == 0)
        {
            MessageBox.Show("Выберите тест для редактирования", "Информация",
                MessageBoxButtons.OK, MessageBoxIcon.Information);
            return;
        }

        var testId = (int)listView!.SelectedItems[0].Tag;
        var test = _tests.FirstOrDefault(t => t.Id == testId);
        if (test == null) return;

        var form = new EditTestForm(_testService, test);
        form.TestUpdated += async (s, args) => await LoadTestsAsync();
        form.ShowDialog();
    }

    private async void BtnDelete_Click(object? sender, EventArgs e)
    {
        var listView = this.Controls.Find("listTests", true).FirstOrDefault() as ListView;
        if (listView?.SelectedItems.Count == 0)
        {
            MessageBox.Show("Выберите тест для удаления", "Информация",
                MessageBoxButtons.OK, MessageBoxIcon.Information);
            return;
        }

        var testId = (int)listView!.SelectedItems[0].Tag;
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
        var listView = this.Controls.Find("listTests", true).FirstOrDefault() as ListView;
        if (listView?.SelectedItems.Count == 0)
        {
            MessageBox.Show("Выберите тест для изменения статуса", "Информация",
                MessageBoxButtons.OK, MessageBoxIcon.Information);
            return;
        }

        var testId = (int)listView!.SelectedItems[0].Tag;
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
        var listView = this.Controls.Find("listTests", true).FirstOrDefault() as ListView;
        if (listView?.SelectedItems.Count == 0)
        {
            MessageBox.Show("Выберите тест для управления вопросами", "Информация",
                MessageBoxButtons.OK, MessageBoxIcon.Information);
            return;
        }

        var testId = (int)listView!.SelectedItems[0].Tag;
        var test = _tests.FirstOrDefault(t => t.Id == testId);
        if (test == null) return;

        var form = new QuestionManagementForm(_questionService, test);
        form.ShowDialog();
    }
}