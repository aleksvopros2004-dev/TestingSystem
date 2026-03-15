using System.Diagnostics;
using TestingSystem.Core.Models;
using TestingSystem.Services.Interfaces;

namespace TestingSystem.WindowsForms
{
    public partial class TestTakingForm : Form
    {
        private readonly IQuestionService _questionService;
        private readonly Test _test;
        private readonly User _currentUser;
        private List<Question> _questions;
        private int _currentQuestionIndex = 0;
        private Dictionary<int, object> _userAnswers = new Dictionary<int, object>();
        private Stopwatch _testStopwatch;
        private int _correctAnswers = 0;

        public TestTakingForm(IQuestionService questionService, Test test, User currentUser)
        {
            _questionService = questionService;
            _test = test;
            _currentUser = currentUser;

            InitializeComponent();

            this.Text = $"Прохождение теста: {_test.Title}";
            lblTestTitle.Text = _test.Title;
            lblTestDescription.Text = _test.Description ?? "Нет описания";

            _testStopwatch = Stopwatch.StartNew();

            LoadQuestionsAsync();
        }

        private async Task LoadQuestionsAsync()
        {
            try
            {
                var dbStopwatch = Stopwatch.StartNew();
                var questionsEnumerable = await _questionService.GetQuestionsByTestAsync(_test.Id);
                dbStopwatch.Stop();

                Console.WriteLine($"Время загрузки вопросов для теста: {dbStopwatch.ElapsedMilliseconds} мс");

                _questions = questionsEnumerable.ToList();

                if (_questions.Count == 0)
                {
                    MessageBox.Show("В этом тесте нет вопросов", "Информация",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.Close();
                    return;
                }

                // Перемешиваем вопросы, если нужно
                if (_test.QuestionsOrderRandom)
                {
                    var rnd = new Random();
                    _questions = _questions.OrderBy(x => rnd.Next()).ToList();
                }

                lblQuestionCounter.Text = $"Вопрос {_currentQuestionIndex + 1} из {_questions.Count}";
                DisplayCurrentQuestion();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка загрузки вопросов: {ex.Message}", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
            }
        }

        private void DisplayCurrentQuestion()
        {
            if (_questions == null || _questions.Count == 0 || _currentQuestionIndex >= _questions.Count)
                return;

            var question = _questions[_currentQuestionIndex];

            // Очищаем панель с вариантами ответов
            pnlAnswers.Controls.Clear();

            // Отображаем текст вопроса
            lblQuestionText.Text = question.QuestionText;

            // Отображаем изображение, если есть
            if (question.ImageData != null && question.ImageData.Length > 0)
            {
                try
                {
                    using var ms = new MemoryStream(question.ImageData);
                    pictureBoxQuestion.Image = Image.FromStream(ms);
                    pictureBoxQuestion.Visible = true;
                }
                catch
                {
                    pictureBoxQuestion.Visible = false;
                }
            }
            else
            {
                pictureBoxQuestion.Visible = false;
            }

            // Создаем контролы для ответов в зависимости от типа вопроса
            int yPos = 10;

            if (question.QuestionType == "TextAnswer")
            {
                // Текстовый ответ
                var txtAnswer = new TextBox
                {
                    Location = new Point(10, yPos),
                    Size = new Size(500, 100),
                    Multiline = true,
                    ScrollBars = ScrollBars.Vertical,
                    Tag = question.Id
                };

                // Если уже был ответ на этот вопрос, восстанавливаем его
                if (_userAnswers.ContainsKey(question.Id))
                {
                    txtAnswer.Text = _userAnswers[question.Id]?.ToString() ?? "";
                }

                txtAnswer.TextChanged += (s, e) =>
                {
                    _userAnswers[question.Id] = txtAnswer.Text;
                };

                pnlAnswers.Controls.Add(txtAnswer);
            }
            else
            {
                var options = question.AnswerOptions?.ToList() ?? new List<AnswerOption>();

                // Перемешиваем варианты ответов, если нужно
                if (_test.AnswerOptionsRandom)
                {
                    var rnd = new Random();
                    options = options.OrderBy(x => rnd.Next()).ToList();
                }

                foreach (var option in options)
                {
                    if (question.QuestionType == "SingleChoice")
                    {
                        // Один вариант ответа (RadioButton)
                        var rdoOption = new RadioButton
                        {
                            Text = option.OptionText,
                            Location = new Point(10, yPos),
                            Size = new Size(500, 25),
                            Tag = option.Id,
                            Name = $"rdo_{option.Id}"
                        };

                        // Если уже был ответ на этот вопрос, восстанавливаем выбор
                        if (_userAnswers.ContainsKey(question.Id) &&
                            _userAnswers[question.Id]?.ToString() == option.Id.ToString())
                        {
                            rdoOption.Checked = true;
                        }

                        rdoOption.CheckedChanged += (s, e) =>
                        {
                            if (rdoOption.Checked)
                            {
                                _userAnswers[question.Id] = option.Id;
                            }
                        };

                        pnlAnswers.Controls.Add(rdoOption);
                        yPos += 30;
                    }
                    else if (question.QuestionType == "MultipleChoice")
                    {
                        // Несколько вариантов ответа (CheckBox)
                        var chkOption = new CheckBox
                        {
                            Text = option.OptionText,
                            Location = new Point(10, yPos),
                            Size = new Size(500, 25),
                            Tag = option.Id,
                            Name = $"chk_{option.Id}"
                        };

                        // Если уже был ответ на этот вопрос, восстанавливаем выбор
                        if (_userAnswers.ContainsKey(question.Id))
                        {
                            var selectedIds = _userAnswers[question.Id] as List<int>;
                            if (selectedIds != null && selectedIds.Contains(option.Id))
                            {
                                chkOption.Checked = true;
                            }
                        }

                        chkOption.CheckedChanged += (s, e) =>
                        {
                            UpdateMultipleChoiceAnswer(question.Id);
                        };

                        pnlAnswers.Controls.Add(chkOption);
                        yPos += 30;
                    }
                }
            }
        }

        private void UpdateMultipleChoiceAnswer(int questionId)
        {
            var selectedIds = new List<int>();

            foreach (Control control in pnlAnswers.Controls)
            {
                if (control is CheckBox chk && chk.Checked && chk.Tag is int optionId)
                {
                    selectedIds.Add(optionId);
                }
            }

            _userAnswers[questionId] = selectedIds;
        }

        private void BtnNext_Click(object? sender, EventArgs e)
        {
            // Проверяем, ответил ли пользователь на текущий вопрос
            var currentQuestion = _questions[_currentQuestionIndex];

            if (!_userAnswers.ContainsKey(currentQuestion.Id))
            {
                var result = MessageBox.Show("Вы не ответили на этот вопрос. Продолжить?",
                    "Предупреждение", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (result == DialogResult.No)
                    return;
            }

            if (_currentQuestionIndex < _questions.Count - 1)
            {
                _currentQuestionIndex++;
                lblQuestionCounter.Text = $"Вопрос {_currentQuestionIndex + 1} из {_questions.Count}";
                DisplayCurrentQuestion();
            }
            else
            {
                // Это был последний вопрос - показываем результаты
                FinishTest();
            }
        }

        private void BtnPrevious_Click(object? sender, EventArgs e)
        {
            if (_currentQuestionIndex > 0)
            {
                _currentQuestionIndex--;
                lblQuestionCounter.Text = $"Вопрос {_currentQuestionIndex + 1} из {_questions.Count}";
                DisplayCurrentQuestion();
            }
        }

        private void FinishTest()
        {
            _testStopwatch.Stop();

            // Подсчитываем правильные ответы
            CalculateResults();

            // Показываем результаты
            var timeSpan = _testStopwatch.Elapsed;
            var timeString = $"Время: {timeSpan.Minutes} мин {timeSpan.Seconds} сек";

            var resultForm = new TestResultForm(_test.Title, _questions.Count, _correctAnswers, timeString);
            resultForm.ShowDialog();

            this.Close();
        }

        private void CalculateResults()
        {
            _correctAnswers = 0;

            foreach (var question in _questions)
            {
                if (!_userAnswers.ContainsKey(question.Id))
                    continue;

                if (question.QuestionType == "SingleChoice")
                {
                    // Для одного варианта - сравниваем ID выбранного варианта с правильным
                    var selectedOptionId = _userAnswers[question.Id] as int?;
                    var correctOption = question.AnswerOptions?.FirstOrDefault(o => o.IsCorrect);

                    if (correctOption != null && selectedOptionId == correctOption.Id)
                    {
                        _correctAnswers++;
                    }
                }
                else if (question.QuestionType == "MultipleChoice")
                {
                    // Для нескольких вариантов - сравниваем множества
                    var selectedIds = _userAnswers[question.Id] as List<int> ?? new List<int>();
                    var correctIds = question.AnswerOptions?
                        .Where(o => o.IsCorrect)
                        .Select(o => o.Id)
                        .OrderBy(id => id)
                        .ToList() ?? new List<int>();

                    var selectedSorted = selectedIds.OrderBy(id => id).ToList();

                    if (selectedSorted.SequenceEqual(correctIds))
                    {
                        _correctAnswers++;
                    }
                }
                else if (question.QuestionType == "TextAnswer")
                {
                    // Для текстового ответа - сравниваем с правильным (простейший вариант)
                    var userText = _userAnswers[question.Id]?.ToString() ?? "";
                    var correctOption = question.AnswerOptions?.FirstOrDefault(o => o.IsCorrect);

                    if (correctOption != null &&
                        userText.Trim().Equals(correctOption.OptionText.Trim(), StringComparison.OrdinalIgnoreCase))
                    {
                        _correctAnswers++;
                    }
                }
            }
        }

        private void BtnCancel_Click(object? sender, EventArgs e)
        {
            var result = MessageBox.Show("Вы уверены, что хотите прервать тестирование?",
                "Подтверждение", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                this.Close();
            }
        }
    }
}