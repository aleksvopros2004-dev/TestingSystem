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

                // Перемешиваем вопросы, если установлен флаг
                if (_test.QuestionsOrderRandom)
                {
                    var rnd = new Random();
                    _questions = _questions.OrderBy(x => rnd.Next()).ToList();
                    Console.WriteLine("Вопросы перемешаны");
                }
                else
                {
                    // Если не перемешивать, сортируем по OrderIndex
                    _questions = _questions.OrderBy(q => q.OrderIndex).ToList();
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
                var txtAnswer = new TextBox
                {
                    Location = new Point(10, yPos),
                    Size = new Size(500, 100),
                    Multiline = true,
                    ScrollBars = ScrollBars.Vertical,
                    Tag = question.Id
                };

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

                if (_test.AnswerOptionsRandom)
                {
                    var rnd = new Random();
                    options = options.OrderBy(x => rnd.Next()).ToList();
                }

                foreach (var option in options)
                {
                    if (question.QuestionType == "SingleChoice")
                    {
                        var rdoOption = new RadioButton
                        {
                            Text = option.OptionText,
                            Location = new Point(10, yPos),
                            Size = new Size(500, 25),
                            Tag = option.Id,
                            Name = $"rdo_{option.Id}"
                        };

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
                        var chkOption = new CheckBox
                        {
                            Text = option.OptionText,
                            Location = new Point(10, yPos),
                            Size = new Size(500, 25),
                            Tag = option.Id,
                            Name = $"chk_{option.Id}"
                        };

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
                {
                    Console.WriteLine($"Вопрос {question.Id} пропущен");
                    continue;
                }

                if (question.QuestionType == "SingleChoice")
                {
                    var selectedOptionId = _userAnswers[question.Id] as int?;
                    var correctOption = question.AnswerOptions?.FirstOrDefault(o => o.IsCorrect);

                    if (correctOption != null && selectedOptionId == correctOption.Id)
                    {
                        _correctAnswers++;
                        Console.WriteLine($"Вопрос {question.Id} (SingleChoice) - правильно");
                    }
                    else
                    {
                        Console.WriteLine($"Вопрос {question.Id} (SingleChoice) - неправильно");
                    }
                }
                else if (question.QuestionType == "MultipleChoice")
                {
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
                        Console.WriteLine($"Вопрос {question.Id} (MultipleChoice) - правильно");
                    }
                    else
                    {
                        Console.WriteLine($"Вопрос {question.Id} (MultipleChoice) - неправильно");
                    }
                }
                else if (question.QuestionType == "TextAnswer")
                {
                    var userText = _userAnswers[question.Id]?.ToString() ?? "";

                    if (!string.IsNullOrWhiteSpace(userText))
                    {
                        _correctAnswers++;
                        Console.WriteLine($"Вопрос {question.Id} (TextAnswer) - засчитан (текст введен)");
                    }
                    else
                    {
                        Console.WriteLine($"Вопрос {question.Id} (TextAnswer) - пустой ответ");
                    }
                }
            }

            Console.WriteLine($"Всего правильных ответов: {_correctAnswers} из {_questions.Count}");
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