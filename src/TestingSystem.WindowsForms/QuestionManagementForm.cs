using System.Diagnostics; 
using TestingSystem.Core.Models;
using TestingSystem.Services.Interfaces;

namespace TestingSystem.WindowsForms;

public partial class QuestionManagementForm : Form
{
    private readonly IQuestionService _questionService;
    private readonly IImageService _imageService;
    private readonly Test _test;
    private readonly User _currentUser;
    private List<Question> _questions = new();

    public QuestionManagementForm(IQuestionService questionService, IImageService imageService, Test test, User currentUser)
    {
        _questionService = questionService;
        _imageService = imageService;
        _test = test;
        _currentUser = currentUser;

        InitializeComponent();

        var loadStopwatch = Stopwatch.StartNew();

        if (_currentUser.Role == UserRole.User)
        {
            this.Text = $"Просмотр вопросов теста: {test.Title}";
            btnAddQuestionTool.Text = "Просмотр (только чтение)";
            btnAddQuestionTool.Enabled = false;
        }
        else
        {
            this.Text = $"Управление вопросами теста: {test.Title}";
        }

        LoadQuestionsWithTiming();

        loadStopwatch.Stop();
        MessageBox.Show($"Время загрузки формы вопросов: {loadStopwatch.ElapsedMilliseconds} мс\n" +
                       $"Требование: не более 6000 мс\n" +
                       $"{(loadStopwatch.ElapsedMilliseconds <= 6000 ? "✓ Успешно" : "✗ Превышено")}",
                       "Время загрузки формы");
    }

    private async Task LoadQuestionsWithTiming()
    {
        try
        {
            listViewQuestions.Items.Clear();

            // Замеряем время запроса к БД
            var dbStopwatch = Stopwatch.StartNew();
            var questionsEnumerable = await _questionService.GetQuestionsByTestAsync(_test.Id);
            dbStopwatch.Stop();

            Console.WriteLine($"Время запроса к БД (загрузка вопросов): {dbStopwatch.ElapsedMilliseconds} мс");

            _questions = questionsEnumerable.ToList();
            _questions = _questions.OrderBy(q => q.OrderIndex).ToList();

            foreach (var question in _questions)
            {
                var item = new ListViewItem(question.Id.ToString());

                var displayText = string.IsNullOrEmpty(question.QuestionText)
                    ? "(без текста)"
                    : (question.QuestionText.Length > 60
                        ? question.QuestionText.Substring(0, 57) + "..."
                        : question.QuestionText);

                item.SubItems.Add(displayText);

                var typeDisplay = string.IsNullOrEmpty(question.QuestionType)
                    ? "Неизвестно"
                    : GetQuestionTypeDisplay(question.QuestionType);

                item.SubItems.Add(typeDisplay);
                item.SubItems.Add(question.AnswerOptions?.Count.ToString() ?? "0");
                item.SubItems.Add(question.OrderIndex.ToString());
                item.Tag = question.Id;
                listViewQuestions.Items.Add(item);
            }

            // Проверяем требования к БД
            if (dbStopwatch.ElapsedMilliseconds > 4000)
            {
                MessageBox.Show($"⚠ Время отклика БД превышено!\n" +
                              $"Фактическое: {dbStopwatch.ElapsedMilliseconds} мс\n" +
                              $"Требование: не более 4000 мс",
                              "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Ошибка загрузки вопросов: {ex.Message}", "Ошибка",
                MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }

    private string GetQuestionTypeDisplay(string questionType)
    {
        return questionType?.ToLower() switch
        {
            "singlechoice" => "Один вариант",
            "multiplechoice" => "Несколько",
            "textanswer" => "Текстовый",
            _ => questionType ?? "Неизвестно"
        };
    }

    private void BtnAddQuestion_Click(object? sender, EventArgs e)
    {
        var actionStopwatch = Stopwatch.StartNew();

        if (_currentUser.Role != UserRole.Admin)
        {
            MessageBox.Show("Только администраторы могут добавлять вопросы", "Ограничение",
                MessageBoxButtons.OK, MessageBoxIcon.Information);
            return;
        }

        var form = new CreateQuestionForm(_questionService, _imageService, _test);
        form.QuestionCreated += async (s, args) => await LoadQuestionsWithTiming();
        form.ShowDialog();

        actionStopwatch.Stop();
        MessageBox.Show($"Время отклика на действие 'Добавить вопрос': {actionStopwatch.ElapsedMilliseconds} мс\n" +
                       $"Требование: не более 3000 мс\n" +
                       $"{(actionStopwatch.ElapsedMilliseconds <= 3000 ? "✓ Успешно" : "✗ Превышено")}",
                       "Время отклика UI");
    }

    private async void BtnEdit_Click(object? sender, EventArgs e)
    {
        var actionStopwatch = Stopwatch.StartNew();

        if (listViewQuestions?.SelectedItems.Count == 0)
        {
            MessageBox.Show("Выберите вопрос", "Информация",
                MessageBoxButtons.OK, MessageBoxIcon.Information);
            return;
        }

        var questionId = (int)listViewQuestions!.SelectedItems[0].Tag;
        var question = _questions.FirstOrDefault(q => q.Id == questionId);
        if (question == null) return;

        var form = new EditQuestionForm(_questionService, _imageService, question, _currentUser);
        form.QuestionUpdated += async (s, args) => await LoadQuestionsWithTiming();
        form.ShowDialog();

        actionStopwatch.Stop();
        MessageBox.Show($"Время отклика на действие 'Редактировать': {actionStopwatch.ElapsedMilliseconds} мс\n" +
                       $"Требование: не более 3000 мс\n" +
                       $"{(actionStopwatch.ElapsedMilliseconds <= 3000 ? "✓ Успешно" : "✗ Превышено")}",
                       "Время отклика UI");
    }

    private async void BtnDelete_Click(object? sender, EventArgs e)
    {
        var actionStopwatch = Stopwatch.StartNew();

        if (listViewQuestions?.SelectedItems.Count == 0)
        {
            MessageBox.Show("Выберите вопрос", "Информация",
                MessageBoxButtons.OK, MessageBoxIcon.Information);
            return;
        }

        if (_currentUser.Role != UserRole.Admin)
        {
            MessageBox.Show("Только администраторы могут удалять вопросы", "Ограничение",
                MessageBoxButtons.OK, MessageBoxIcon.Information);
            return;
        }

        var questionId = (int)listViewQuestions!.SelectedItems[0].Tag;
        var question = _questions.FirstOrDefault(q => q.Id == questionId);

        var result = MessageBox.Show($"Вы уверены, что хотите удалить вопрос?",
            "Подтверждение удаления", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

        if (result == DialogResult.Yes)
        {
            var dbStopwatch = Stopwatch.StartNew();
            var (success, message) = await _questionService.DeleteQuestionAsync(questionId);
            dbStopwatch.Stop();

            MessageBox.Show(message, success ? "Успех" : "Ошибка",
                MessageBoxButtons.OK, success ? MessageBoxIcon.Information : MessageBoxIcon.Error);

            if (success)
            {
                await LoadQuestionsWithTiming();
                Console.WriteLine($"Время операции удаления вопроса в БД: {dbStopwatch.ElapsedMilliseconds} мс");
            }
        }

        actionStopwatch.Stop();
        MessageBox.Show($"Время отклика на действие 'Удалить': {actionStopwatch.ElapsedMilliseconds} мс\n" +
                       $"Требование: не более 5000 мс\n" +
                       $"{(actionStopwatch.ElapsedMilliseconds <= 5000 ? "✓ Успешно" : "✗ Превышено")}",
                       "Время отклика CRUD");
    }

    private async void BtnMoveUp_Click(object? sender, EventArgs e)
    {
        var actionStopwatch = Stopwatch.StartNew();

        if (_currentUser.Role != UserRole.Admin)
        {
            MessageBox.Show("Только администраторы могут изменять порядок вопросов", "Ограничение",
                MessageBoxButtons.OK, MessageBoxIcon.Information);
            return;
        }

        await MoveQuestionWithTiming(-1);

        actionStopwatch.Stop();
        MessageBox.Show($"Время отклика на действие 'Переместить выше': {actionStopwatch.ElapsedMilliseconds} мс\n" +
                       $"Требование: не более 3000 мс\n" +
                       $"{(actionStopwatch.ElapsedMilliseconds <= 3000 ? "✓ Успешно" : "✗ Превышено")}",
                       "Время отклика UI");
    }

    private async void BtnMoveDown_Click(object? sender, EventArgs e)
    {
        var actionStopwatch = Stopwatch.StartNew();

        if (_currentUser.Role != UserRole.Admin)
        {
            MessageBox.Show("Только администраторы могут изменять порядок вопросов", "Ограничение",
                MessageBoxButtons.OK, MessageBoxIcon.Information);
            return;
        }

        await MoveQuestionWithTiming(1);

        actionStopwatch.Stop();
        MessageBox.Show($"Время отклика на действие 'Переместить ниже': {actionStopwatch.ElapsedMilliseconds} мс\n" +
                       $"Требование: не более 3000 мс\n" +
                       $"{(actionStopwatch.ElapsedMilliseconds <= 3000 ? "✓ Успешно" : "✗ Превышено")}",
                       "Время отклика UI");
    }

    private async Task MoveQuestionWithTiming(int direction)
    {
        if (listViewQuestions?.SelectedItems.Count == 0) return;

        var selectedIndex = listViewQuestions.SelectedIndices[0];
        var newIndex = selectedIndex + direction;

        if (newIndex < 0 || newIndex >= listViewQuestions.Items.Count) return;

        var questionId = (int)listViewQuestions.SelectedItems[0].Tag;
        var question = _questions.FirstOrDefault(q => q.Id == questionId);
        if (question == null) return;

        var otherQuestionId = (int)listViewQuestions.Items[newIndex].Tag;
        var otherQuestion = _questions.FirstOrDefault(q => q.Id == otherQuestionId);
        if (otherQuestion == null) return;

        var tempOrder = question.OrderIndex;
        question.OrderIndex = otherQuestion.OrderIndex;
        otherQuestion.OrderIndex = tempOrder;

        try
        {
            // Замеряем время запросов к БД
            var dbStopwatch = Stopwatch.StartNew();
            var task1 = _questionService.UpdateQuestionAsync(question);
            var task2 = _questionService.UpdateQuestionAsync(otherQuestion);
            await Task.WhenAll(task1, task2);
            dbStopwatch.Stop();

            MessageBox.Show($"Время операций обновления порядка в БД: {dbStopwatch.ElapsedMilliseconds} мс");

            var result1 = task1.Result;
            var result2 = task2.Result;

            if (result1.Success && result2.Success)
            {
                await LoadQuestionsWithTiming();
                listViewQuestions.Items[newIndex].Selected = true;
                listViewQuestions.Items[newIndex].EnsureVisible();
            }
            else
            {
                question.OrderIndex = tempOrder;
                otherQuestion.OrderIndex = question.OrderIndex;
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Ошибка перемещения: {ex.Message}", "Ошибка",
                MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }

    private void BtnRefresh_Click(object? sender, EventArgs e)
    {
        var actionStopwatch = Stopwatch.StartNew();
        LoadQuestionsWithTiming();
        actionStopwatch.Stop();

        MessageBox.Show($"Время отклика на действие 'Обновить': {actionStopwatch.ElapsedMilliseconds} мс\n" +
                       $"Требование: не более 3000 мс\n" +
                       $"{(actionStopwatch.ElapsedMilliseconds <= 3000 ? "✓ Успешно" : "✗ Превышено")}",
                       "Время отклика UI");
    }
}