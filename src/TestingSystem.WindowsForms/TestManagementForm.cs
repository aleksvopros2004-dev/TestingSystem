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
            this.StartPosition = FormStartPosition.CenterScreen;

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
                Size = new Size(600, 400),
                View = View.Details,
                FullRowSelect = true,
                GridLines = true,
                Name = "listTests"
            };

            listView.Columns.Add("ID", 50);
            listView.Columns.Add("Название", 200);
            listView.Columns.Add("Описание", 250);
            listView.Columns.Add("Статус", 80);
            listView.Columns.Add("Дата создания", 120);

            listView.MouseDoubleClick += ListView_MouseDoubleClick;

            // Кнопки управления
            var btnEdit = new Button { Text = "Редактировать", Location = new Point(630, 40), Size = new Size(100, 30) };
            var btnDelete = new Button { Text = "Удалить", Location = new Point(630, 80), Size = new Size(100, 30) };
            var btnToggleActive = new Button { Text = "Активировать", Location = new Point(630, 120), Size = new Size(100, 30) };
            var btnManageQuestions = new Button { Text = "Вопросы", Location = new Point(630, 160), Size = new Size(100, 30) };

            btnEdit.Click += BtnEdit_Click;
            btnDelete.Click += BtnDelete_Click;
            btnToggleActive.Click += BtnToggleActive_Click;
            btnManageQuestions.Click += BtnManageQuestions_Click;

            this.Controls.AddRange(new Control[] { toolStrip, listView, btnEdit, btnDelete, btnToggleActive, btnManageQuestions });
        }

        private async Task LoadTestsAsync()
        {
            var listView = this.Controls.Find("listTests", true).FirstOrDefault() as ListView;
            if (listView == null) return;

            try
            {
                listView.Items.Clear();
                _tests = (await _testService.GetTestsByAuthorAsync(_currentUser.Id)).ToList();

                foreach (var test in _tests)
                {
                    var item = new ListViewItem(test.Id.ToString());
                    item.SubItems.Add(test.Title);
                    item.SubItems.Add(test.Description ?? "");
                    item.SubItems.Add(test.IsActive ? "Активен" : "Неактивен");
                    //item.SubItems.Add(test.CreatedDate.ToString("dd.MM.yyyy"));
                    item.Tag = test.Id;

                    listView.Items.Add(item);
                }
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
            form.TestCreated += async (s, e) => await LoadTestsAsync();
            form.ShowDialog();
        }

        private void ListView_MouseDoubleClick(object? sender, MouseEventArgs e)
        {
            BtnEdit_Click(sender, e);
        }

        private void BtnEdit_Click(object? sender, EventArgs e)
        {
            var listView = this.Controls.Find("listTests", true).FirstOrDefault() as ListView;
            if (listView?.SelectedItems.Count == 0) return;

            var testId = (int)listView!.SelectedItems[0].Tag;
            var test = _tests.FirstOrDefault(t => t.Id == testId);
            if (test == null) return;

            var form = new EditTestForm(_testService, test);
            form.TestUpdated += async (s, e) => await LoadTestsAsync();
            form.ShowDialog();
        }

        private async void BtnDelete_Click(object? sender, EventArgs e)
        {
            var listView = this.Controls.Find("listTests", true).FirstOrDefault() as ListView;
            if (listView?.SelectedItems.Count == 0) return;

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

                if (success)
                    await LoadTestsAsync();
            }
        }

        private async void BtnToggleActive_Click(object? sender, EventArgs e)
        {
            var listView = this.Controls.Find("listTests", true).FirstOrDefault() as ListView;
            if (listView?.SelectedItems.Count == 0) return;

            var testId = (int)listView!.SelectedItems[0].Tag;
            var test = _tests.FirstOrDefault(t => t.Id == testId);
            if (test == null) return;

            var (success, message) = await _testService.ToggleTestActivationAsync(testId, !test.IsActive);
            MessageBox.Show(message, success ? "Успех" : "Ошибка",
                MessageBoxButtons.OK, success ? MessageBoxIcon.Information : MessageBoxIcon.Error);

            if (success)
                await LoadTestsAsync();
        }

        private void BtnManageQuestions_Click(object? sender, EventArgs e)
        {
            var listView = this.Controls.Find("listTests", true).FirstOrDefault() as ListView;
            if (listView?.SelectedItems.Count == 0) return;

            var testId = (int)listView!.SelectedItems[0].Tag;
            var test = _tests.FirstOrDefault(t => t.Id == testId);
            if (test == null) return;

            var form = new QuestionManagementForm(_questionService, test);
            form.ShowDialog();
        }
    }
}
