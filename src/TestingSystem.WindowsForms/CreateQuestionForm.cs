using System.Windows.Forms;
using TestingSystem.Core.Models;
using TestingSystem.Services.Interfaces;

namespace TestingSystem.WindowsForms
{
    public partial class CreateQuestionForm : Form
    {
        private readonly IQuestionService _questionService;
        private readonly IImageService _imageService;
        private readonly Test _test;
        private List<AnswerOption> _answerOptions = new();
        private byte[]? _selectedImageData;
        private string? _selectedImageContentType;

        public event EventHandler? QuestionCreated;

        public CreateQuestionForm(IQuestionService questionService, IImageService imageService, Test test)
        {
            _questionService = questionService;
            _imageService = imageService;
            _test = test;

            InitializeComponent();

            lblTitle.Text = $"Добавление вопроса к тесту: {test?.Title ?? "Неизвестный тест"}";

            AddInitialAnswerOptions();
        }

        private void AddInitialAnswerOptions()
        {
            // Добавляем начальные варианты ответов
            AddAnswerOption();
            AddAnswerOption();
            UpdateOptionsCounter();
        }

        private void AddAnswerOption()
        {
            if (_answerOptions.Count >= 10)
            {
                MessageBox.Show("Максимальное количество вариантов ответов - 10", "Ограничение",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            var newOption = new AnswerOption
            {
                OptionText = $"Вариант {_answerOptions.Count + 1}",
                IsCorrect = false
            };

            _answerOptions.Add(newOption);
            LoadAnswerOptions();
            UpdateOptionsCounter();
        }

        private void LoadAnswerOptions()
        {
            pnlAnswers.Controls.Clear();

            for (int i = 0; i < _answerOptions.Count; i++)
            {
                var option = _answerOptions[i];
                var yPos = 10 + (i * 35);

                // Получаем тип вопроса
                var questionType = cmbType.SelectedIndex;

                // Если текстовый ответ, не показываем варианты
                if (questionType == 2)
                {
                    pnlAnswers.Visible = false;
                    return;
                }

                pnlAnswers.Visible = true;

                // CheckBox/Radiobutton для отметки правильности
                Control correctControl;
                if (questionType == 0) // Один вариант
                {
                    correctControl = new RadioButton
                    {
                        Location = new Point(10, yPos),
                        Size = new Size(20, 20),
                        Checked = option.IsCorrect,
                        Tag = i,
                        Name = $"rdoCorrect_{i}"
                    };
                }
                else // Несколько вариантов
                {
                    correctControl = new CheckBox
                    {
                        Location = new Point(10, yPos),
                        Size = new Size(20, 20),
                        Checked = option.IsCorrect,
                        Tag = i,
                        Name = $"chkCorrect_{i}"
                    };
                }

                // Текст варианта
                var txtOption = new TextBox
                {
                    Location = new Point(40, yPos),
                    Size = new Size(550, 25),
                    Text = option.OptionText,
                    Tag = i,
                    Name = $"txtOption_{i}"
                };

                // Кнопка удаления
                var btnDelete = new Button
                {
                    Text = "×",
                    Location = new Point(600, yPos),
                    Size = new Size(30, 25),
                    ForeColor = Color.Red,
                    Font = new Font("Arial", 10, FontStyle.Bold),
                    Tag = i,
                    Name = $"btnDelete_{i}"
                };

                btnDelete.Click += (s, e) =>
                {
                    if (btnDelete.Tag is int idx && idx < _answerOptions.Count)
                    {
                        _answerOptions.RemoveAt(idx);
                        LoadAnswerOptions();
                        UpdateOptionsCounter();
                    }
                };

                pnlAnswers.Controls.Add(correctControl);
                pnlAnswers.Controls.Add(txtOption);
                pnlAnswers.Controls.Add(btnDelete);
            }
        }

        private void UpdateOptionsCounter()
        {
            lblOptionsCounter.Text = $"Вариантов: {_answerOptions.Count}/10";

            // Меняем цвет при приближении к лимиту
            if (_answerOptions.Count >= 8)
            {
                lblOptionsCounter.ForeColor = Color.Orange;
            }
            else if (_answerOptions.Count >= 10)
            {
                lblOptionsCounter.ForeColor = Color.Red;
            }
            else
            {
                lblOptionsCounter.ForeColor = Color.Gray;
            }
        }

        private void UpdateAnswerPanel()
        {
            var questionType = cmbType.SelectedIndex;
            pnlAnswers.Visible = questionType != 2;
            btnAddOption.Enabled = questionType != 2;
            LoadAnswerOptions();
        }

        private void BtnLoadImage_Click(object? sender, EventArgs e)
        {
            using var openFileDialog = new OpenFileDialog
            {
                Filter = "Image Files|*.jpg;*.jpeg;*.png;*.gif;*.bmp",
                Title = "Выберите изображение",
                RestoreDirectory = true
            };

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                LoadImageFromPath(openFileDialog.FileName);
            }
        }

        private void BtnRemoveImage_Click(object? sender, EventArgs e)
        {
            _selectedImageData = null;
            _selectedImageContentType = null;
            pictureBox.Image = null;
            pictureBox.Visible = false;
            btnRemoveImage.Enabled = false;
        }

        private void LoadImageFromPath(string filePath)
        {
            try
            {
                var fileInfo = new FileInfo(filePath);
                if (fileInfo.Length > 5 * 1024 * 1024)
                {
                    MessageBox.Show("Размер изображения не должен превышать 5 MB", "Ошибка",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                _selectedImageData = _imageService.LoadImageFromFile(filePath);

                if (_selectedImageData == null)
                {
                    MessageBox.Show("Не удалось загрузить изображение", "Ошибка",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                if (!_imageService.IsValidImage(_selectedImageData))
                {
                    MessageBox.Show("Выбранный файл не является допустимым изображением", "Ошибка",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    _selectedImageData = null;
                    return;
                }

                _selectedImageContentType = _imageService.GetImageContentTypeFromExtension(filePath);
                ShowImagePreview(_selectedImageData);

                btnRemoveImage.Enabled = true;

                var sizeKB = _imageService.GetImageSizeInKB(_selectedImageData);
                lblMessage.Text = $"Изображение загружено ({sizeKB} KB)";
                lblMessage.ForeColor = Color.Green;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при загрузке изображения: {ex.Message}", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ShowImagePreview(byte[] imageData)
        {
            try
            {
                using var ms = new MemoryStream(imageData);
                pictureBox.Image = System.Drawing.Image.FromStream(ms);
                pictureBox.Visible = true;
            }
            catch
            {
                // Игнорируем ошибки предпросмотра
            }
        }

        private async void BtnSave_Click(object? sender, EventArgs e)
        {
            // Валидация
            if (string.IsNullOrWhiteSpace(txtQuestion.Text))
            {
                lblMessage.Text = "Введите текст вопроса";
                return;
            }

            // ПРОВЕРКА: Максимум 50 вопросов в тесте
            var questions = await _questionService.GetQuestionsByTestAsync(_test.Id);
            if (questions.Count() >= 50)
            {
                lblMessage.Text = "В тесте уже максимальное количество вопросов (50)";
                return;
            }

            string questionType = cmbType.SelectedIndex switch
            {
                0 => "SingleChoice",
                1 => "MultipleChoice",
                2 => "TextAnswer",
                _ => "SingleChoice"
            };

            var answerOptions = new List<AnswerOption>();
            if (questionType != "TextAnswer")
            {
                // ПРОВЕРКА: Максимум 10 вариантов ответов
                if (_answerOptions.Count > 10)
                {
                    lblMessage.Text = "Максимальное количество вариантов ответов - 10";
                    return;
                }

                for (int i = 0; i < _answerOptions.Count; i++)
                {
                    var txtOption = pnlAnswers.Controls.Find($"txtOption_{i}", true).FirstOrDefault() as TextBox;
                    if (txtOption != null && !string.IsNullOrWhiteSpace(txtOption.Text))
                    {
                        bool isCorrect = false;
                        if (questionType == "SingleChoice")
                        {
                            var rdo = pnlAnswers.Controls.Find($"rdoCorrect_{i}", true).FirstOrDefault() as RadioButton;
                            isCorrect = rdo?.Checked ?? false;
                        }
                        else if (questionType == "MultipleChoice")
                        {
                            var chk = pnlAnswers.Controls.Find($"chkCorrect_{i}", true).FirstOrDefault() as CheckBox;
                            isCorrect = chk?.Checked ?? false;
                        }

                        answerOptions.Add(new AnswerOption
                        {
                            OptionText = txtOption.Text.Trim(),
                            IsCorrect = isCorrect
                        });
                    }
                }

                if (answerOptions.Count == 0)
                {
                    lblMessage.Text = "Добавьте хотя бы один вариант ответа";
                    return;
                }

                var correctCount = answerOptions.Count(o => o.IsCorrect);
                if (correctCount == 0)
                {
                    lblMessage.Text = "Укажите хотя бы один правильный вариант";
                    return;
                }

                if (questionType == "SingleChoice" && correctCount > 1)
                {
                    lblMessage.Text = "Для вопроса с одним вариантом можно выбрать только один правильный ответ";
                    return;
                }
            }

            var orderIndex = questions.Any() ? questions.Max(q => q.OrderIndex) + 1 : 1;

            var question = new Question
            {
                TestId = _test.Id,
                QuestionText = txtQuestion.Text.Trim(),
                QuestionType = questionType,
                OrderIndex = orderIndex,
                AnswerOptions = answerOptions,
                ImageData = _selectedImageData,
                ImageContentType = _selectedImageContentType
            };

            btnSave.Enabled = false;
            lblMessage.Text = "Создание вопроса...";
            lblMessage.ForeColor = Color.Blue;

            try
            {
                var (success, message, questionId) = await _questionService.CreateQuestionAsync(question);

                if (success)
                {
                    lblMessage.Text = $"Вопрос успешно создан (ID: {questionId})!";
                    lblMessage.ForeColor = Color.Green;
                    await Task.Delay(1500);
                    QuestionCreated?.Invoke(this, EventArgs.Empty);
                    this.Close();
                }
                else
                {
                    lblMessage.Text = message;
                    lblMessage.ForeColor = Color.Red;
                    btnSave.Enabled = true;
                }
            }
            catch (Exception ex)
            {
                lblMessage.Text = $"Ошибка: {ex.Message}";
                lblMessage.ForeColor = Color.Red;
                btnSave.Enabled = true;
            }
        }

        private void BtnCancel_Click(object? sender, EventArgs e)
        {
            this.Close();
        }

        private void CmbType_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateAnswerPanel();
        }

        private void BtnAddOption_Click(object sender, EventArgs e)
        {
            AddAnswerOption();
        }
    }
}