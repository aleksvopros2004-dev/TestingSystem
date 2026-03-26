using System.Diagnostics;
using System.Windows.Forms.DataVisualization.Charting;
using TestingSystem.Core.Interfaces;
using TestingSystem.Core.Models;
using TestingSystem.Data.Repositories;
using TestingSystem.Services.Interfaces;
using TestingSystem.Services.Services;

namespace TestingSystem.WindowsForms
{
    public partial class StatisticsForm : Form
    {
        private readonly ITestService _testService;
        private readonly IStatisticsRepository _statisticsRepository;
        private readonly ExcelExportService _exportService;
        private readonly ILemmatizationService _lemmatizationService;  // ← интерфейс
        private readonly User _currentUser;
        private TestStatistics? _currentStats;
        private List<Test> _tests = new();

        public StatisticsForm(
            ITestService testService,
            IStatisticsRepository statisticsRepository,
            ExcelExportService exportService,
            ILemmatizationService lemmatizationService,  // ← интерфейс
            User currentUser)
        {
            _testService = testService;
            _statisticsRepository = statisticsRepository;
            _exportService = exportService;
            _lemmatizationService = lemmatizationService;
            _currentUser = currentUser;

            InitializeComponent();
            LoadTests();

            this.comboBoxTests.SelectedIndexChanged += ComboBoxTests_SelectedIndexChanged;
            this.buttonRefresh.Click += ButtonRefresh_Click;
            this.buttonExportExcel.Click += ButtonExportExcel_Click;
            this.listViewQuestions.SelectedIndexChanged += ListViewQuestions_SelectedIndexChanged;
        }

        private async void LoadTests()
        {
            try
            {
                _tests = (await _testService.GetAllTestsAsync()).ToList();

                comboBoxTests.Items.Clear();
                foreach (var test in _tests)
                {
                    comboBoxTests.Items.Add($"{test.Title} (ID: {test.Id})");
                }

                if (comboBoxTests.Items.Count > 0)
                {
                    comboBoxTests.SelectedIndex = 0;
                }
                else
                {
                    labelMessage.Text = "Нет доступных тестов";
                    labelMessage.Visible = true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка загрузки тестов: {ex.Message}", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void ComboBoxTests_SelectedIndexChanged(object? sender, EventArgs e)
        {
            if (comboBoxTests.SelectedIndex < 0) return;

            var test = _tests[comboBoxTests.SelectedIndex];
            await LoadStatistics(test.Id);
        }

        private async Task LoadStatistics(int testId)
        {
            try
            {
                Cursor = Cursors.WaitCursor;
                labelMessage.Text = "Загрузка статистики...";
                labelMessage.Visible = true;

                _currentStats = await _statisticsRepository.GetTestStatisticsAsync(testId);

                DisplayGeneralStats();
                DisplayScoreDistribution();
                DisplayTopQuestions();
                DisplayRecentAttempts();

                labelMessage.Visible = false;
                Cursor = Cursors.Default;
            }
            catch (Exception ex)
            {
                Cursor = Cursors.Default;
                MessageBox.Show($"Ошибка загрузки статистики: {ex.Message}", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                labelMessage.Text = $"Ошибка: {ex.Message}";
                labelMessage.Visible = true;
            }
        }

        private void DisplayGeneralStats()
        {
            if (_currentStats == null) return;

            labelTotalAttempts.Text = $"{_currentStats.TotalAttempts} / {_currentStats.CompletedAttempts}";
            labelAverageScore.Text = $"{_currentStats.AverageScore:F1}%";
            labelMedianScore.Text = $"{_currentStats.MedianScore:F1}%";
            labelMaxScore.Text = $"{_currentStats.MaxScore}%";
            labelMinScore.Text = $"{_currentStats.MinScore}%";
            labelAverageTime.Text = $"{_currentStats.AverageTimeMinutes:F1} мин";

            if (_currentStats.AverageScore > 0)
            {
                progressAverage.Value = (int)Math.Min(_currentStats.AverageScore, 100);
            }
            else
            {
                progressAverage.Value = 0;
            }

            // Цвет прогресс-бара
            if (_currentStats.AverageScore >= 80)
                progressAverage.ForeColor = Color.Green;
            else if (_currentStats.AverageScore >= 60)
                progressAverage.ForeColor = Color.LightGreen;
            else if (_currentStats.AverageScore >= 40)
                progressAverage.ForeColor = Color.Orange;
            else
                progressAverage.ForeColor = Color.Red;
        }

        private void DisplayScoreDistribution()
        {
            if (_currentStats == null) return;

            // Очищаем предыдущие данные
            chartScoreDistribution.Series.Clear();

            var series = new Series("Распределение оценок")
            {
                ChartType = SeriesChartType.Column,
                IsValueShownAsLabel = true,
                Font = new Font("Segoe UI", 9F)
            };

            var data = new[]
            {
        new { Range = "0-20%", Value = _currentStats.Score0_20 },
        new { Range = "20-40%", Value = _currentStats.Score20_40 },
        new { Range = "40-60%", Value = _currentStats.Score40_60 },
        new { Range = "60-80%", Value = _currentStats.Score60_80 },
        new { Range = "80-100%", Value = _currentStats.Score80_100 }
    };

            bool hasData = data.Any(d => d.Value > 0);

            if (!hasData)
            {
                series.Points.AddXY("Нет данных", 0);
                chartScoreDistribution.ChartAreas[0].AxisX.Title = "";
            }
            else
            {
                foreach (var item in data)
                {
                    // Просто добавляем точки, не используя Label
                    series.Points.AddXY(item.Range, item.Value);
                }
                chartScoreDistribution.ChartAreas[0].AxisX.Title = "Результат";
            }

            chartScoreDistribution.ChartAreas[0].AxisY.Title = "Количество";
            chartScoreDistribution.Series.Add(series);
            chartScoreDistribution.Invalidate();
        }

        private void DisplayTopQuestions()
        {
            if (_currentStats == null) return;

            listViewQuestions.Items.Clear();

            var questionsWithStats = _currentStats.QuestionStats
                .Where(q => q.TotalAnswers > 0)
                .ToList();

            if (!questionsWithStats.Any())
            {
                var item = new ListViewItem("-");
                item.SubItems.Add("Нет данных о прохождениях");
                item.SubItems.Add("-");
                item.SubItems.Add("-");
                listViewQuestions.Items.Add(item);
                return;
            }

            var topWrong = questionsWithStats
                .OrderBy(q => q.CorrectPercentage)
                .Take(5)
                .ToList();

            foreach (var q in topWrong)
            {
                var item = new ListViewItem(q.QuestionId.ToString());

                var shortText = q.QuestionText.Length > 50
                    ? q.QuestionText.Substring(0, 47) + "..."
                    : q.QuestionText;
                item.SubItems.Add(shortText);

                string percentText = q.TotalAnswers > 0
                    ? $"{q.CorrectPercentage:F1}%"
                    : "0%";
                item.SubItems.Add(percentText);

                item.SubItems.Add($"{q.CorrectAnswers}/{q.TotalAnswers}");

                item.Tag = q;
                listViewQuestions.Items.Add(item);
            }

            if (topWrong.Any() && topWrong.First().CorrectPercentage < 50)
            {
                listViewQuestions.Items[0].BackColor = Color.FromArgb(255, 200, 200);
            }
        }

        private void DisplayRecentAttempts()
        {
            if (_currentStats == null) return;

            listViewAttempts.Items.Clear();

            if (_currentStats.RecentAttempts == null || !_currentStats.RecentAttempts.Any())
            {
                var item = new ListViewItem("Нет данных");
                item.SubItems.Add("");
                item.SubItems.Add("");
                item.SubItems.Add("");
                item.SubItems.Add("");
                listViewAttempts.Items.Add(item);
                return;
            }

            foreach (var attempt in _currentStats.RecentAttempts.Take(10))
            {
                var item = new ListViewItem(attempt.UserName);
                item.SubItems.Add(attempt.AttemptDate.ToString("dd.MM.yyyy HH:mm"));
                item.SubItems.Add($"{attempt.EarnedPoints}/{attempt.TotalPoints}");
                item.SubItems.Add($"{attempt.Percentage:F1}%");
                item.SubItems.Add($"{attempt.TimeSpent.Minutes:D2}:{attempt.TimeSpent.Seconds:D2}");

                if (attempt.Percentage >= 80)
                    item.BackColor = Color.FromArgb(200, 255, 200);
                else if (attempt.Percentage <= 40)
                    item.BackColor = Color.FromArgb(255, 200, 200);

                listViewAttempts.Items.Add(item);
            }
        }

        private async void ButtonExportExcel_Click(object? sender, EventArgs e)
        {
            if (_currentStats == null)
            {
                MessageBox.Show("Нет данных для экспорта", "Информация",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            using var saveDialog = new SaveFileDialog
            {
                Filter = "Excel files (*.xlsx)|*.xlsx",
                FileName = $"Статистика_теста_{_currentStats.TestTitle}_{DateTime.Now:yyyyMMdd_HHmmss}.xlsx"
            };

            if (saveDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    Cursor = Cursors.WaitCursor;
                    var excelData = _exportService.ExportTestStatistics(_currentStats);
                    await File.WriteAllBytesAsync(saveDialog.FileName, excelData);

                    MessageBox.Show($"Статистика успешно экспортирована в файл:\n{saveDialog.FileName}",
                        "Экспорт завершен", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Ошибка при экспорте: {ex.Message}", "Ошибка",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    Cursor = Cursors.Default;
                }
            }
        }

        private async void ButtonRefresh_Click(object? sender, EventArgs e)
        {
            if (comboBoxTests.SelectedIndex >= 0)
            {
                var test = _tests[comboBoxTests.SelectedIndex];
                await LoadStatistics(test.Id);
            }
        }

        private void ListViewQuestions_SelectedIndexChanged(object? sender, EventArgs e)
        {
            if (listViewQuestions.SelectedItems.Count == 0) return;

            var question = listViewQuestions.SelectedItems[0].Tag as QuestionStatistics;
            if (question != null)
            {
                // Отладочный вывод
                Console.WriteLine($"=== Выбран вопрос ===");
                Console.WriteLine($"ID: {question.QuestionId}");
                Console.WriteLine($"Текст: {question.QuestionText}");
                Console.WriteLine($"Тип: {question.QuestionType}");
                Console.WriteLine($"Вариантов: {question.OptionPopularity?.Count ?? 0}");

                var detailsForm = new QuestionStatisticsForm(question, _lemmatizationService);
                detailsForm.ShowDialog();
            }
        }
    }
}