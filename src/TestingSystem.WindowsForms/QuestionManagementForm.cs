using TestingSystem.Core.Models;
using TestingSystem.Services.Interfaces;

namespace TestingSystem.WindowsForms
{
    public partial class QuestionManagementForm : Form
    {
        private readonly IQuestionService _questionService;
        private readonly IImageService _imageService;
        private readonly Test _test;
        private List<Question> _questions = new();

        public QuestionManagementForm(IQuestionService questionService, IImageService imageService, Test test)
        {
            _questionService = questionService;
            _imageService = imageService;
            _test = test;
            InitializeComponent();
            LoadQuestionsAsync();
        }

        private async Task LoadQuestionsAsync()
        {
            var txtDebug = this.Controls.Find("txtDebug", true).FirstOrDefault() as TextBox;

            if (listViewQuestions == null || txtDebug == null) return;

            try
            {
                listViewQuestions.Items.Clear();
                txtDebug.Text = "Загрузка вопросов...\r\n";

                // 1. Пробуем получить вопросы через сервис
                var questionsEnumerable = await _questionService.GetQuestionsByTestAsync(_test.Id);
                _questions = questionsEnumerable.ToList();

                txtDebug.AppendText($"Получено {_questions.Count} вопросов из сервиса\r\n");

                // 2. Если вопросы не загрузились, покажем сообщение
                if (_questions.Count == 0)
                {
                    txtDebug.AppendText("Вопросы не найдены.\r\n");
                    return;
                }

                // 3. Сортируем вопросы
                _questions = _questions.OrderBy(q => q.OrderIndex).ToList();

                // 4. Отображаем вопросы
                foreach (var question in _questions)
                {
                    var item = new ListViewItem(question.Id.ToString());

                    // Проверяем поля
                    if (string.IsNullOrEmpty(question.QuestionText))
                    {
                        txtDebug.AppendText($"ВНИМАНИЕ: Вопрос ID {question.Id} имеет пустой текст!\r\n");
                    }

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

                    txtDebug.AppendText($"Добавлен вопрос: ID={question.Id}, Текст='{question.QuestionText}', Тип='{question.QuestionType}', Порядок={question.OrderIndex}\r\n");
                }

                txtDebug.AppendText($"\r\nВсего отображено {listViewQuestions.Items.Count} вопросов\r\n");
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

            // Использовать _imageService
            var form = new CreateQuestionForm(_questionService, _imageService, _test);

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
            var txtDebug = this.Controls.Find("txtDebug", true).FirstOrDefault() as TextBox;

            if (listViewQuestions?.SelectedItems.Count == 0)
            {
                MessageBox.Show("Выберите вопрос для редактирования", "Информация",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            var questionId = (int)listViewQuestions!.SelectedItems[0].Tag;

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
                var form = new EditQuestionForm(_questionService, _imageService, question);
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
            if (listViewQuestions?.SelectedItems.Count == 0)
            {
                MessageBox.Show("Выберите вопрос для удаления", "Информация",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            var questionId = (int)listViewQuestions!.SelectedItems[0].Tag;
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
            if (listViewQuestions?.SelectedItems.Count == 0)
            {
                MessageBox.Show("Выберите вопрос для перемещения", "Информация",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            var selectedIndex = listViewQuestions.SelectedIndices[0];

            // Вычисляем новый индекс в списке
            var newIndex = selectedIndex + direction;

            // Проверяем границы
            if (newIndex < 0 || newIndex >= listViewQuestions.Items.Count)
            {
                return;
            }

            // Получаем ID выбранного вопроса
            var questionId = (int)listViewQuestions.SelectedItems[0].Tag;

            // Находим вопрос в списке
            var question = _questions.FirstOrDefault(q => q.Id == questionId);
            if (question == null) return;

            // Находим вопрос, с которым нужно поменяться
            var otherQuestionId = (int)listViewQuestions.Items[newIndex].Tag;
            var otherQuestion = _questions.FirstOrDefault(q => q.Id == otherQuestionId);
            if (otherQuestion == null) return;

            var txtDebug = this.Controls.Find("txtDebug", true).FirstOrDefault() as TextBox;
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
                    listViewQuestions.Items[newIndex].Selected = true;
                    listViewQuestions.Items[newIndex].EnsureVisible();

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

        private async void BtnMoveUp_Click(object? sender, EventArgs e)
        {
            await MoveQuestion(-1);
        }

        private async void BtnMoveDown_Click(object? sender, EventArgs e)
        {
            await MoveQuestion(1);
        }

        private void BtnRefresh_Click(object? sender, EventArgs e)
        {
            LoadQuestionsAsync();
        }
    }
}