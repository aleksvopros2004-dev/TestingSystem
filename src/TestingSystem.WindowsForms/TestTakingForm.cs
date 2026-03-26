using System.Diagnostics;
using TestingSystem.Core.Models;
using TestingSystem.Data.Repositories;
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
        private int _totalPoints = 0;
        private int _earnedPoints = 0;
        private readonly IStatisticsRepository _statisticsRepository;
        private int _sessionId;

        public TestTakingForm(
     IQuestionService questionService,
     IStatisticsRepository statisticsRepository,  
     Test test,
     User currentUser)
        {
            _questionService = questionService;
            _statisticsRepository = statisticsRepository;  
            _test = test;
            _currentUser = currentUser;

            InitializeComponent();

            this.Text = $"Прохождение теста: {_test.Title}";
            lblTestTitle.Text = _test.Title;
            lblTestDescription.Text = _test.Description ?? "Нет описания";

            pnlAnswers.AutoScroll = true;
            pnlAnswers.HorizontalScroll.Enabled = false;
            pnlAnswers.HorizontalScroll.Visible = false;
            pnlAnswers.AutoScrollMinSize = new Size(0, 0);
            panelQuestion.AutoScroll = true;

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

                if (_test.QuestionsOrderRandom)
                {
                    var rnd = new Random();
                    _questions = _questions.OrderBy(x => rnd.Next()).ToList();
                    Console.WriteLine("Вопросы перемешаны");
                }
                else
                {
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

            pnlAnswers.Controls.Clear();

            lblQuestionText.Text = question.QuestionText;

            // Проверяем наличие изображения
            bool hasImage = question.ImageData != null && question.ImageData.Length > 0;

            if (hasImage)
            {
                try
                {
                    using var ms = new MemoryStream(question.ImageData);
                    pictureBoxQuestion.Image = Image.FromStream(ms);
                    pictureBoxQuestion.Visible = true;
                    pnlAnswers.Location = new Point(20, 360);
                }
                catch
                {
                    pictureBoxQuestion.Visible = false;
                    hasImage = false;
                }
            }

            if (!hasImage)
            {
                pictureBoxQuestion.Visible = false;
                pnlAnswers.Location = new Point(20, 135);
            }

            // Устанавливаем размер панели 
            pnlAnswers.Size = new Size(800, 280);

            int yPos = 10;

            if (question.QuestionType == "TextAnswer")
            {
                var txtAnswer = new TextBox
                {
                    Location = new Point(10, yPos),
                    Size = new Size(770, 100),
                    Multiline = true,
                    ScrollBars = ScrollBars.Vertical,
                    Tag = question.Id,
                    Font = new Font("Segoe UI", 10F),
                    WordWrap = true
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

                var lblInfo = new Label
                {
                    Text = "Введите ваш ответ текстом",
                    Location = new Point(10, yPos + 110),
                    Size = new Size(300, 25),
                    ForeColor = Color.Gray,
                    Font = new Font("Segoe UI", 8F, FontStyle.Italic)
                };
                pnlAnswers.Controls.Add(lblInfo);
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
                            Size = new Size(770, 30),
                            Tag = option.Id,
                            Name = $"rdo_{option.Id}",
                            Font = new Font("Segoe UI", 10F),
                            AutoSize = false
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
                        yPos += 35;
                    }
                    else if (question.QuestionType == "MultipleChoice")
                    {
                        var chkOption = new CheckBox
                        {
                            Text = option.OptionText,
                            Location = new Point(10, yPos),
                            Size = new Size(770, 30),
                            Tag = option.Id,
                            Name = $"chk_{option.Id}",
                            Font = new Font("Segoe UI", 10F),
                            AutoSize = false
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
                        yPos += 35;
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

        private async void FinishTest()
        {
            _testStopwatch.Stop();
            var timeSpan = _testStopwatch.Elapsed;
            var timeString = $"Время: {timeSpan.Minutes} мин {timeSpan.Seconds} сек";

            // Сначала рассчитываем результаты
            CalculateResults();

            // Сохраняем сессию с рассчитанными баллами
            await SaveTestSession();

            if (_test.IsScored)
            {
                var resultForm = new TestResultForm(_test.Title, _questions.Count, _earnedPoints, _totalPoints, timeString);
                resultForm.ShowDialog();
            }
            else
            {
                MessageBox.Show($"Опрос \"{_test.Title}\" успешно пройден!\n\n" +
                    $"Количество вопросов: {_questions.Count}\n" +
                    $"{timeString}\n\n" +
                    $"Спасибо за участие!",
                    "Опрос завершен",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
            }

            this.Close();
        }

        private void CalculateResults()
        {
            _totalPoints = 0;
            _earnedPoints = 0;

            Console.WriteLine("=== НАЧАЛО РАСЧЕТА РЕЗУЛЬТАТОВ ===");
            Console.WriteLine($"Всего вопросов: {_questions.Count}");

            foreach (var question in _questions)
            {
                _totalPoints += question.Points;
                Console.WriteLine($"Вопрос {question.Id}: тип={question.QuestionType}, баллов={question.Points}");

                if (!_userAnswers.ContainsKey(question.Id))
                {
                    Console.WriteLine($"  -> Вопрос {question.Id} пропущен");
                    continue;
                }

                if (question.QuestionType == "SingleChoice")
                {
                    var selectedOptionId = _userAnswers[question.Id] as int?;
                    var correctOption = question.AnswerOptions?.FirstOrDefault(o => o.IsCorrect);

                    Console.WriteLine($"  -> Выбран вариант: {selectedOptionId}, правильный: {correctOption?.Id}");

                    if (correctOption != null && selectedOptionId == correctOption.Id)
                    {
                        _earnedPoints += question.Points;
                        Console.WriteLine($"  -> ПРАВИЛЬНО! +{question.Points} баллов");
                    }
                    else
                    {
                        Console.WriteLine($"  -> НЕПРАВИЛЬНО");
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

                    Console.WriteLine($"  -> Выбрано: [{string.Join(",", selectedIds)}], правильно: [{string.Join(",", correctIds)}]");

                    if (selectedIds.OrderBy(id => id).SequenceEqual(correctIds))
                    {
                        _earnedPoints += question.Points;
                        Console.WriteLine($"  -> ПРАВИЛЬНО! +{question.Points} баллов");
                    }
                    else
                    {
                        Console.WriteLine($"  -> НЕПРАВИЛЬНО");
                    }
                }
                else if (question.QuestionType == "TextAnswer")
                {
                    var answerText = _userAnswers[question.Id]?.ToString() ?? "";
                    Console.WriteLine($"  -> Ответ: \"{answerText}\"");

                    if (!string.IsNullOrWhiteSpace(answerText))
                    {
                        _earnedPoints += question.Points;
                        Console.WriteLine($"  -> ЗАСЧИТАНО! +{question.Points} баллов");
                    }
                    else
                    {
                        Console.WriteLine($"  -> ПУСТОЙ ОТВЕТ");
                    }
                }
            }

            Console.WriteLine($"ИТОГО: набрано {_earnedPoints} из {_totalPoints} баллов");
            Console.WriteLine("=== КОНЕЦ РАСЧЕТА ===");
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

        private async Task SaveTestSession()
        {
            Console.WriteLine($"Сохранение сессии: EarnedPoints={_earnedPoints}, TotalPoints={_totalPoints}");

            var session = new TestSession
            {
                UserId = _currentUser.Id,
                TestId = _test.Id,
                StartTime = DateTime.UtcNow.Add(-_testStopwatch.Elapsed),
                EndTime = DateTime.UtcNow,
                Duration = _testStopwatch.Elapsed,
                EarnedPoints = _earnedPoints,
                TotalPoints = _totalPoints,
                IsCompleted = true
            };

            await _statisticsRepository.SaveTestSessionAsync(session);
            Console.WriteLine($"Сохранена сессия ID: {session.Id}");

            // Сохраняем ответы
            for (int i = 0; i < _questions.Count; i++)
            {
                var question = _questions[i];
                var answer = new UserAnswer
                {
                    SessionId = session.Id,
                    QuestionId = question.Id,
                    IsCorrect = false,
                    PointsEarned = 0
                };

                if (_userAnswers.ContainsKey(question.Id))
                {
                    if (question.QuestionType == "SingleChoice")
                    {
                        var selectedOptionId = _userAnswers[question.Id] as int?;
                        var correctOption = question.AnswerOptions?.FirstOrDefault(o => o.IsCorrect);

                        if (correctOption != null && selectedOptionId == correctOption.Id)
                        {
                            answer.IsCorrect = true;
                            answer.PointsEarned = question.Points;
                        }
                        answer.SelectedOptionsJson = Newtonsoft.Json.JsonConvert.SerializeObject(new[] { selectedOptionId });
                        Console.WriteLine($"  Сохранен ответ на вопрос {question.Id}: SingleChoice, правильный={answer.IsCorrect}");
                    }
                    else if (question.QuestionType == "MultipleChoice")
                    {
                        var selectedIds = _userAnswers[question.Id] as List<int> ?? new List<int>();
                        var correctIds = question.AnswerOptions?
                            .Where(o => o.IsCorrect)
                            .Select(o => o.Id)
                            .OrderBy(id => id)
                            .ToList() ?? new List<int>();

                        if (selectedIds.OrderBy(id => id).SequenceEqual(correctIds))
                        {
                            answer.IsCorrect = true;
                            answer.PointsEarned = question.Points;
                        }
                        answer.SelectedOptionsJson = Newtonsoft.Json.JsonConvert.SerializeObject(selectedIds);
                        Console.WriteLine($"  Сохранен ответ на вопрос {question.Id}: MultipleChoice, правильный={answer.IsCorrect}");
                    }
                    else if (question.QuestionType == "TextAnswer")
                    {
                        var answerText = _userAnswers[question.Id]?.ToString() ?? "";
                        answer.AnswerText = answerText;
                        if (!string.IsNullOrWhiteSpace(answerText))
                        {
                            answer.IsCorrect = true;
                            answer.PointsEarned = question.Points;
                        }
                        Console.WriteLine($"  Сохранен ответ на вопрос {question.Id}: TextAnswer, текст=\"{answerText}\"");
                    }
                }

                await _statisticsRepository.SaveUserAnswerAsync(answer);
            }
        }
    }
}