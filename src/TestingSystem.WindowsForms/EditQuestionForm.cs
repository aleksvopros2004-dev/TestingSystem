using System.Diagnostics;
using TestingSystem.Core.Models;
using TestingSystem.Services.Interfaces;

namespace TestingSystem.WindowsForms
{
    public partial class EditQuestionForm : Form
    {
        private readonly IQuestionService _questionService;
        private readonly IImageService _imageService;
        private readonly Question _question;
        private readonly User _currentUser;
        private List<AnswerOption> _answerOptions = new();
        private byte[]? _selectedImageData;
        private string? _selectedImageContentType;

        public event EventHandler? QuestionUpdated;

        public EditQuestionForm(IQuestionService questionService, IImageService imageService,
                       Question question, User currentUser)
        {
            _questionService = questionService;
            _imageService = imageService;
            _question = question;
            _currentUser = currentUser;
            _answerOptions = question.AnswerOptions?.ToList() ?? new List<AnswerOption>();
            _selectedImageData = question.ImageData;
            _selectedImageContentType = question.ImageContentType;

            InitializeComponent();

            if (_currentUser.Role == UserRole.Admin)
            {
                cmbType.Visible = true;
                txtTypeDisplay.Visible = false;
                cmbType.SelectedIndexChanged += CmbType_SelectedIndexChanged;
            }
            else
            {
                cmbType.Visible = false;
                txtTypeDisplay.Visible = true;
            }

            if (question.QuestionType != "TextAnswer")
            {
                pnlAnswers.Visible = true;
                lblAnswers.Visible = true;
            }

            LoadQuestionData();

            // Блокируем функционал для обычных пользователей
            if (_currentUser.Role == UserRole.User)
            {
                this.Text = "Просмотр вопроса";
                SetupReadOnlyMode();
            }
            else
            {
                this.Text = "Редактирование вопроса";
            }
        }

        private void SetupReadOnlyMode()
        {
            txtQuestion.ReadOnly = true;
            txtQuestion.BackColor = SystemColors.Control;

            txtOrder.ReadOnly = true;
            txtOrder.BackColor = SystemColors.Control;

            txtTypeDisplay.ReadOnly = true;
            txtTypeDisplay.BackColor = SystemColors.Control;

            cmbType.Visible = false;
            txtTypeDisplay.Visible = true;

            numPoints.Enabled = false;
            numPoints.BackColor = SystemColors.Control;

            btnLoadImage.Visible = false;
            btnRemoveImage.Visible = false;
            btnSave.Visible = false;
            btnCancel.Text = "Закрыть";
            btnAddOption.Visible = false;

            var lblViewMode = new Label
            {
                Text = "Режим просмотра",
                Font = new Font("Arial", 9, FontStyle.Bold),
                ForeColor = Color.Gray,
                Location = new Point(500, 10),
                Size = new Size(150, 20)
            };
            this.Controls.Add(lblViewMode);

            if (_question.QuestionType != "TextAnswer")
            {
                pnlAnswers.Visible = true;
                lblAnswers.Visible = true;
            }
        }

        private void LoadQuestionData()
        {
            txtTypeDisplay.Text = _question.QuestionType switch
            {
                "SingleChoice" => "Один вариант",
                "MultipleChoice" => "Несколько вариантов",
                "TextAnswer" => "Текстовый ответ",
                _ => _question.QuestionType
            };

            cmbType.Items.Clear();
            cmbType.Items.Add("Один вариант");
            cmbType.Items.Add("Несколько вариантов");
            cmbType.Items.Add("Текстовый ответ");

            switch (_question.QuestionType)
            {
                case "SingleChoice":
                    cmbType.SelectedIndex = 0;
                    break;
                case "MultipleChoice":
                    cmbType.SelectedIndex = 1;
                    break;
                case "TextAnswer":
                    cmbType.SelectedIndex = 2;
                    break;
            }

            txtQuestion.Text = _question.QuestionText;
            txtOrder.Text = _question.OrderIndex.ToString();

            numPoints.Value = _question.Points;
            Console.WriteLine($"Загрузка вопроса ID {_question.Id}: баллы = {_question.Points}");

            if (_selectedImageData != null && _selectedImageData.Length > 0)
            {
                ShowImagePreview(_selectedImageData);
                btnRemoveImage.Enabled = _currentUser.Role == UserRole.Admin;
            }

            if (_question.QuestionType != "TextAnswer")
            {
                if (_answerOptions == null)
                {
                    _answerOptions = _question.AnswerOptions?.ToList() ?? new List<AnswerOption>();
                }

                LoadAnswerOptions();

                lblAnswers.Visible = true;
                pnlAnswers.Visible = true;
                btnAddOption.Visible = _currentUser.Role == UserRole.Admin;
            }
            else
            {
                lblAnswers.Visible = false;
                pnlAnswers.Visible = false;
                btnAddOption.Visible = false;
            }
        }

        private void LoadAnswerOptions()
        {
            pnlAnswers.Controls.Clear();

            // Проверяем, есть ли варианты ответов
            if (_answerOptions == null || !_answerOptions.Any())
            {
                if (_currentUser.Role == UserRole.Admin)
                {
                    var lblNoOptions = new Label
                    {
                        Text = "Нет вариантов ответов. Нажмите 'Добавить вариант'",
                        Location = new Point(10, 10),
                        ForeColor = Color.Gray,
                        AutoSize = true
                    };
                    pnlAnswers.Controls.Add(lblNoOptions);
                }
                return;
            }

            for (int i = 0; i < _answerOptions.Count; i++)
            {
                var option = _answerOptions[i];
                var yPos = 10 + (i * 35);

                Control correctControl;
                if (_question.QuestionType == "SingleChoice")
                {
                    correctControl = new RadioButton
                    {
                        Location = new Point(10, yPos),
                        Size = new Size(20, 20),
                        Checked = option.IsCorrect,
                        Tag = i,
                        Name = $"rdoCorrect_{i}",
                        Enabled = _currentUser.Role == UserRole.Admin
                    };
                }
                else if (_question.QuestionType == "MultipleChoice")
                {
                    correctControl = new CheckBox
                    {
                        Location = new Point(10, yPos),
                        Size = new Size(20, 20),
                        Checked = option.IsCorrect,
                        Tag = i,
                        Name = $"chkCorrect_{i}",
                        Enabled = _currentUser.Role == UserRole.Admin
                    };
                }
                else
                {
                    continue;
                }

                var txtOption = new TextBox
                {
                    Location = new Point(40, yPos),
                    Size = new Size(520, 25),
                    Text = option.OptionText,
                    Tag = i,
                    Name = $"txtOption_{i}",
                    ReadOnly = _currentUser.Role == UserRole.User,
                    BackColor = _currentUser.Role == UserRole.User ? SystemColors.Control : SystemColors.Window
                };

                pnlAnswers.Controls.Add(correctControl);
                pnlAnswers.Controls.Add(txtOption);

                if (option.IsCorrect)
                {
                    var lblCorrect = new Label
                    {
                        Text = "✓",
                        Location = new Point(565, yPos),
                        Size = new Size(20, 20),
                        ForeColor = Color.Green,
                        Font = new Font("Arial", 12, FontStyle.Bold),
                        TextAlign = ContentAlignment.MiddleCenter
                    };
                    pnlAnswers.Controls.Add(lblCorrect);
                }

                if (_currentUser.Role == UserRole.Admin)
                {
                    var btnDelete = new Button
                    {
                        Text = "×",
                        Location = new Point(590, yPos),
                        Size = new Size(30, 25),
                        ForeColor = Color.Red,
                        Font = new Font("Arial", 10, FontStyle.Bold),
                        Tag = i,
                        Name = $"btnDelete_{i}",
                        FlatStyle = FlatStyle.Flat
                    };

                    btnDelete.Click += (s, e) =>
                    {
                        if (btnDelete.Tag is int idx && idx < _answerOptions.Count)
                        {
                            _answerOptions.RemoveAt(idx);
                            LoadAnswerOptions();
                        }
                    };

                    pnlAnswers.Controls.Add(btnDelete);
                }
            }

            pnlAnswers.Visible = true;
        }

        private void AddAnswerOption()
        {
            if (_currentUser.Role != UserRole.Admin)
            {
                MessageBox.Show("Только администраторы могут добавлять варианты ответов", "Ограничение",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

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
        }

        private void BtnLoadImage_Click(object? sender, EventArgs e)
        {
            if (_currentUser.Role != UserRole.Admin)
            {
                MessageBox.Show("Только администраторы могут загружать изображения", "Ограничение",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

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
            if (_currentUser.Role != UserRole.Admin)
            {
                MessageBox.Show("Только администраторы могут удалять изображения", "Ограничение",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

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
            if (_currentUser.Role != UserRole.Admin)
            {
                MessageBox.Show("Только администраторы могут сохранять изменения", "Ограничение",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (string.IsNullOrWhiteSpace(txtQuestion.Text))
            {
                lblMessage.Text = "Введите текст вопроса";
                return;
            }

            if (numPoints.Value < 1 || numPoints.Value > 10)
            {
                lblMessage.Text = "Количество баллов должно быть от 1 до 10";
                return;
            }

            // Собираем данные вариантов ответов
            if (_question.QuestionType != "TextAnswer")
            {
                for (int i = 0; i < _answerOptions.Count; i++)
                {
                    var txtOption = pnlAnswers.Controls.Find($"txtOption_{i}", true).FirstOrDefault() as TextBox;
                    if (txtOption != null)
                    {
                        bool isCorrect = false;
                        if (_question.QuestionType == "SingleChoice")
                        {
                            var rdo = pnlAnswers.Controls.Find($"rdoCorrect_{i}", true).FirstOrDefault() as RadioButton;
                            isCorrect = rdo?.Checked ?? false;
                        }
                        else if (_question.QuestionType == "MultipleChoice")
                        {
                            var chk = pnlAnswers.Controls.Find($"chkCorrect_{i}", true).FirstOrDefault() as CheckBox;
                            isCorrect = chk?.Checked ?? false;
                        }

                        _answerOptions[i].OptionText = txtOption.Text.Trim();
                        _answerOptions[i].IsCorrect = isCorrect;
                    }
                }

                // Удаляем пустые варианты
                _answerOptions.RemoveAll(o => string.IsNullOrWhiteSpace(o.OptionText));

                if (_answerOptions.Count == 0)
                {
                    lblMessage.Text = "Добавьте хотя бы один вариант ответа";
                    return;
                }

                // Проверяем правильные ответы
                var correctCount = _answerOptions.Count(o => o.IsCorrect);
                if (correctCount == 0)
                {
                    lblMessage.Text = "Укажите хотя бы один правильный вариант";
                    return;
                }

                if (_question.QuestionType == "SingleChoice" && correctCount > 1)
                {
                    lblMessage.Text = "Для вопроса с одним вариантом можно выбрать только один правильный ответ";
                    return;
                }
            }

            _question.QuestionText = txtQuestion.Text.Trim();
            _question.Points = (int)numPoints.Value; 
            _question.AnswerOptions = _answerOptions;
            _question.ImageData = _selectedImageData;
            _question.ImageContentType = _selectedImageContentType;

            Console.WriteLine($"Сохранение вопроса ID {_question.Id}: баллы = {_question.Points}");

            btnSave.Enabled = false;
            lblMessage.Text = "Сохранение...";
            lblMessage.ForeColor = Color.Blue;

            try
            {
                var (success, message) = await _questionService.UpdateQuestionAsync(_question);
                if (success)
                {
                    lblMessage.Text = "Вопрос успешно сохранен!";
                    lblMessage.ForeColor = Color.Green;
                    await Task.Delay(1000);
                    QuestionUpdated?.Invoke(this, EventArgs.Empty);
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

        private void CmbType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (_currentUser.Role != UserRole.Admin)
                return;

            var selectedType = cmbType.SelectedIndex switch
            {
                0 => "SingleChoice",
                1 => "MultipleChoice",
                2 => "TextAnswer",
                _ => _question.QuestionType
            };

            if (selectedType != _question.QuestionType)
            {
                var result = MessageBox.Show(
                    "Изменение типа вопроса может привести к потере данных вариантов ответов. Продолжить?",
                    "Подтверждение",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Warning);

                if (result == DialogResult.Yes)
                {
                    _question.QuestionType = selectedType;

                    _answerOptions.Clear();

                    if (_question.QuestionType == "TextAnswer")
                    {
                        lblAnswers.Visible = false;
                        pnlAnswers.Visible = false;
                        btnAddOption.Visible = false;
                    }
                    else
                    {
                        lblAnswers.Visible = true;
                        pnlAnswers.Visible = true;
                        btnAddOption.Visible = true;

                        AddInitialAnswerOptions();
                    }
                }
                else
                {
                    switch (_question.QuestionType)
                    {
                        case "SingleChoice":
                            cmbType.SelectedIndex = 0;
                            break;
                        case "MultipleChoice":
                            cmbType.SelectedIndex = 1;
                            break;
                        case "TextAnswer":
                            cmbType.SelectedIndex = 2;
                            break;
                    }
                }
            }
        }

        private void AddInitialAnswerOptions()
        {
            if (_question.QuestionType == "TextAnswer")
                return;

            // Добавляем два начальных варианта
            for (int i = 0; i < 2; i++)
            {
                var newOption = new AnswerOption
                {
                    OptionText = $"Вариант {_answerOptions.Count + 1}",
                    IsCorrect = i == 0 
                };
                _answerOptions.Add(newOption);
            }

            LoadAnswerOptions();
        }

        private void BtnCancel_Click(object? sender, EventArgs e)
        {
            this.Close();
        }

        private void BtnAddOption_Click(object sender, EventArgs e)
        {
            AddAnswerOption();
        }
    }
}