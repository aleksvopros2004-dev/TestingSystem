using Microsoft.Extensions.DependencyInjection;
using TestingSystem.Core.Models;
using TestingSystem.Data.Repositories;
using TestingSystem.Services.Interfaces;

namespace TestingSystem.WindowsForms;

public partial class QuestionManagementForm : Form
{
    private readonly IQuestionService _questionService;
    private readonly Test _test;
    private List<Question> _questions = new();

    public QuestionManagementForm(IQuestionService questionService, Test test)
    {
        _questionService = questionService;
        _test = test;
        InitializeComponent();
        LoadQuestionsAsync();
    }

    private void InitializeComponent()
    {
        this.Text = $"Вопросы теста: {_test.Title} (ID: {_test.Id})";
        this.Size = new Size(900, 600);
        this.StartPosition = FormStartPosition.CenterParent;
        CreateControls();
    }

    private void CreateControls()
    {
        // Панель инструментов
        var toolStrip = new ToolStrip();
        var btnAddQuestion = new ToolStripButton("Добавить вопрос");
        var btnRefresh = new ToolStripButton("Обновить");

        btnAddQuestion.Click += BtnAddQuestion_Click;
        btnRefresh.Click += async (s, e) => await LoadQuestionsAsync();

        toolStrip.Items.AddRange(new ToolStripItem[] { btnAddQuestion, btnRefresh });

        // Список вопросов
        var listView = new ListView
        {
            Location = new Point(20, 40),
            Size = new Size(700, 400),
            View = View.Details,
            FullRowSelect = true,
            GridLines = true,
            Name = "listQuestions"
        };

        listView.Columns.Add("ID", 50);
        listView.Columns.Add("Текст вопроса", 400);
        listView.Columns.Add("Тип", 120);
        listView.Columns.Add("Вариантов", 80);
        listView.Columns.Add("Порядок", 60);

        // Кнопки управления
        var btnEdit = new Button
        {
            Text = "Редактировать",
            Location = new Point(730, 40),
            Size = new Size(140, 30)
        };

        var btnDelete = new Button
        {
            Text = "Удалить",
            Location = new Point(730, 80),
            Size = new Size(140, 30)
        };

        var btnMoveUp = new Button
        {
            Text = "↑ Выше",
            Location = new Point(730, 120),
            Size = new Size(140, 30)
        };

        var btnMoveDown = new Button
        {
            Text = "↓ Ниже",
            Location = new Point(730, 160),
            Size = new Size(140, 30)
        };

        btnEdit.Click += BtnEdit_Click;
        btnDelete.Click += BtnDelete_Click;
        btnMoveUp.Click += async (s, e) => await MoveQuestion(-1);
        btnMoveDown.Click += async (s, e) => await MoveQuestion(1);

        // Панель отладки
        var txtDebug = new TextBox
        {
            Location = new Point(20, 450),
            Size = new Size(700, 100),
            Multiline = true,
            ScrollBars = ScrollBars.Vertical,
            ReadOnly = true,
            Name = "txtDebug"
        };

        this.Controls.AddRange(new Control[]
        {
            toolStrip, listView,
            btnEdit, btnDelete, btnMoveUp, btnMoveDown,
            txtDebug
        });
    }

    private async Task LoadQuestionsAsync()
    {
        var listView = this.Controls.Find("listQuestions", true).FirstOrDefault() as ListView;
        var txtDebug = this.Controls.Find("txtDebug", true).FirstOrDefault() as TextBox;

        if (listView == null || txtDebug == null) return;

        try
        {
            listView.Items.Clear();
            txtDebug.Text = "Загрузка вопросов...\r\n";

            // 1. Пробуем получить вопросы через сервис
            var questionsEnumerable = await _questionService.GetQuestionsByTestAsync(_test.Id);
            _questions = questionsEnumerable.ToList();

            txtDebug.AppendText($"Получено {_questions.Count} вопросов из сервиса\r\n");

            // 2. Если вопросы не загрузились, используем репозиторий напрямую
            if (_questions.Count == 0)
            {
                txtDebug.AppendText("Вопросы не найдены через сервис. Используем репозиторий...\r\n");

                // Получаем репозиторий через DI
                var questionRepository = Program.ServiceProvider.GetRequiredService<IQuestionRepository>();
                var directQuestions = await questionRepository.GetByTestIdAsync(_test.Id);
                _questions = directQuestions.ToList();

                txtDebug.AppendText($"Через репозиторий получено {_questions.Count} вопросов\r\n");
            }

            // 3. Отображаем вопросы
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

                listView.Items.Add(item);
            }

            txtDebug.AppendText($"\r\nВсего отображено {listView.Items.Count} вопросов\r\n");
        }
        catch (Exception ex)
        {
            txtDebug.AppendText($"ОШИБКА: {ex.Message}\r\n{ex.StackTrace}\r\n");
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
        var txtDebug = this.Controls.Find("txtDebug", true).FirstOrDefault() as TextBox;
        if (txtDebug != null)
            txtDebug.AppendText("Открытие формы создания вопроса...\r\n");

        var form = new CreateQuestionForm(_questionService, _test);
        form.QuestionCreated += async (s, args) =>
        {
            if (txtDebug != null)
                txtDebug.AppendText("Вопрос создан, обновляем список...\r\n");
            await LoadQuestionsAsync();
        };
        form.ShowDialog();
    }

    private async void BtnEdit_Click(object? sender, EventArgs e)
    {
        var listView = this.Controls.Find("listQuestions", true).FirstOrDefault() as ListView;
        var txtDebug = this.Controls.Find("txtDebug", true).FirstOrDefault() as TextBox;

        if (listView?.SelectedItems.Count == 0)
        {
            MessageBox.Show("Выберите вопрос для редактирования", "Информация",
                MessageBoxButtons.OK, MessageBoxIcon.Information);
            return;
        }

        var questionId = (int)listView!.SelectedItems[0].Tag;

        if (txtDebug != null)
            txtDebug.AppendText($"Попытка редактирования вопроса ID: {questionId}\r\n");

        // Проверяем, есть ли вопрос в списке
        var question = _questions.FirstOrDefault(q => q.Id == questionId);
        if (question == null)
        {
            if (txtDebug != null)
                txtDebug.AppendText($"Вопрос ID {questionId} не найден в списке _questions\r\n");

            MessageBox.Show("Вопрос не найден в списке. Попробуйте обновить список.", "Ошибка",
                MessageBoxButtons.OK, MessageBoxIcon.Error);
            return;
        }

        if (txtDebug != null)
        {
            txtDebug.AppendText($"Найден вопрос: ID={question.Id}, Текст='{question.QuestionText}'\r\n");
            txtDebug.AppendText("Открытие формы редактирования...\r\n");
        }

        try
        {
            var form = new EditQuestionForm(_questionService, question);
            form.QuestionUpdated += async (s, args) =>
            {
                if (txtDebug != null)
                    txtDebug.AppendText("Вопрос обновлен, обновляем список...\r\n");
                await LoadQuestionsAsync();
            };
            form.ShowDialog();
        }
        catch (Exception ex)
        {
            if (txtDebug != null)
                txtDebug.AppendText($"Ошибка открытия формы: {ex.Message}\r\n");

            MessageBox.Show($"Ошибка открытия формы: {ex.Message}", "Ошибка",
                MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }

    private async void BtnDelete_Click(object? sender, EventArgs e)
    {
        var listView = this.Controls.Find("listQuestions", true).FirstOrDefault() as ListView;
        if (listView?.SelectedItems.Count == 0)
        {
            MessageBox.Show("Выберите вопрос для удаления", "Информация",
                MessageBoxButtons.OK, MessageBoxIcon.Information);
            return;
        }

        var questionId = (int)listView!.SelectedItems[0].Tag;
        var question = _questions.FirstOrDefault(q => q.Id == questionId);

        var result = MessageBox.Show($"Вы уверены, что хотите удалить вопрос?\nID: {questionId}\nТекст: {question?.QuestionText ?? "(без текста)"}",
            "Подтверждение удаления", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

        if (result == DialogResult.Yes)
        {
            var (success, message) = await _questionService.DeleteQuestionAsync(questionId);

            MessageBox.Show(message, success ? "Успех" : "Ошибка",
                MessageBoxButtons.OK, success ? MessageBoxIcon.Information : MessageBoxIcon.Error);

            if (success) await LoadQuestionsAsync();
        }
    }

    private async Task MoveQuestion(int direction)
    {
        var listView = this.Controls.Find("listQuestions", true).FirstOrDefault() as ListView;
        var txtDebug = this.Controls.Find("txtDebug", true).FirstOrDefault() as TextBox;

        if (listView?.SelectedItems.Count == 0)
        {
            MessageBox.Show("Выберите вопрос для перемещения", "Информация",
                MessageBoxButtons.OK, MessageBoxIcon.Information);
            return;
        }

        var selectedIndex = listView.SelectedIndices[0];

        // Вычисляем новый индекс в списке
        var newIndex = selectedIndex + direction;

        // Проверяем границы
        if (newIndex < 0 || newIndex >= listView.Items.Count)
        {
            return;
        }

        // Получаем ID выбранного вопроса
        var questionId = (int)listView.SelectedItems[0].Tag;

        // Находим вопрос в списке
        var question = _questions.FirstOrDefault(q => q.Id == questionId);
        if (question == null) return;

        // Находим вопрос, с которым нужно поменяться
        var otherQuestionId = (int)listView.Items[newIndex].Tag;
        var otherQuestion = _questions.FirstOrDefault(q => q.Id == otherQuestionId);
        if (otherQuestion == null) return;

        if (txtDebug != null)
        {
            txtDebug.AppendText($"Перемещение вопроса {question.Id} (порядок: {question.OrderIndex}) ");
            txtDebug.AppendText($"с вопросом {otherQuestion.Id} (порядок: {otherQuestion.OrderIndex})\r\n");
        }

        // Меняем порядковые номера местами
        var tempOrder = question.OrderIndex;
        question.OrderIndex = otherQuestion.OrderIndex;
        otherQuestion.OrderIndex = tempOrder;

        try
        {
            // Сохраняем оба вопроса с новыми порядковыми номерами
            var task1 = _questionService.UpdateQuestionAsync(question);
            var task2 = _questionService.UpdateQuestionAsync(otherQuestion);

            await Task.WhenAll(task1, task2);

            // Проверяем результаты
            var result1 = task1.Result;
            var result2 = task2.Result;

            if (result1.Success && result2.Success)
            {
                // Обновляем отображение
                await LoadQuestionsAsync();

                // Выделяем перемещенный вопрос
                listView.Items[newIndex].Selected = true;
                listView.Items[newIndex].EnsureVisible();

                if (txtDebug != null)
                    txtDebug.AppendText($"Успешно: {question.Id} -> {question.OrderIndex}, {otherQuestion.Id} -> {otherQuestion.OrderIndex}\r\n");
            }
            else
            {
                var errorMessage = $"Ошибки: {result1.Message}, {result2.Message}";
                if (txtDebug != null)
                    txtDebug.AppendText($"Ошибка: {errorMessage}\r\n");

                MessageBox.Show(errorMessage, "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);

                // Восстанавливаем локальные значения при ошибке
                question.OrderIndex = tempOrder;
                otherQuestion.OrderIndex = question.OrderIndex;
            }
        }
        catch (Exception ex)
        {
            if (txtDebug != null)
                txtDebug.AppendText($"Исключение: {ex.Message}\r\n");

            MessageBox.Show($"Ошибка перемещения: {ex.Message}", "Ошибка",
                MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }
}