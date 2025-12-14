using System.Windows.Forms;
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

        public EditQuestionForm(IQuestionService questionService, IImageService imageService, Question question, User currentUser)
        {
            _questionService = questionService;
            _imageService = imageService;
            _question = question;
            _currentUser = currentUser;
            _answerOptions = question.AnswerOptions?.ToList() ?? new List<AnswerOption>();
            _selectedImageData = question.ImageData;
            _selectedImageContentType = question.ImageContentType;

            InitializeComponent();
            LoadQuestionData();

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
            btnLoadImage.Visible = false;
            btnRemoveImage.Visible = false;
            btnSave.Visible = false;
            btnCancel.Text = "Закрыть";
            btnAddOption.Visible = false;

            foreach (Control control in pnlAnswers.Controls)
            {
                if (control is TextBox textBox)
                {
                    textBox.ReadOnly = true;
                    textBox.BackColor = SystemColors.Control;
                }
                else if (control is RadioButton radioButton)
                {
                    radioButton.Enabled = false;
                }
				else if (control is CheckBox checkBox)
				{
					checkBox.Enabled = false;
				}
				else if (control is Button button && button.Text == "*")
				{
					button.Enabled = false;
				}
			}

            if (_question.QuestionType == "TextAnswer")
            {
                pnlAnswers.Visible = false;
                lblAnswers.Visible = false;
            }
        }

        private void LoadQuestionData()
        {
            txtType.Text = _question.QuestionType switch
            {
                "SingleChoice" => "Один вариант",
                "MultipleChoice" => "Несколько вариантов",
                "TextAnswer" => "Текстовый ответ",
                _ => _question.QuestionType
            };

            txtQuestion.Text = _question.QuestionText;
            txtOrder.Text = _question.OrderIndex.ToString();

            // Загружаем изображение, если оно есть
            if (_selectedImageData != null && _selectedImageData.Length > 0)
            {
                ShowImagePreview(_selectedImageData);
                btnRemoveImage.Enabled = true;
            }

            // Здесь нужно загрузить варианты ответов для вопросов с выбором
            if (_question.QuestionType != "TextAnswer")
            {
                LoadAnswerOptions(); // Вызов метода загрузки вариантов
                pnlAnswers.Visible = true;
                btnAddOption.Visible = true;
            }
        }

		private void LoadAnswerOptions()
		{
			pnlAnswers.Controls.Clear();

			// Проверяем, есть ли варианты ответов
			if (_answerOptions == null || !_answerOptions.Any())
			{
				// Если вариантов нет, показываем сообщение
				var lblNoOptions = new Label
				{
					Text = "Нет вариантов ответов",
					Location = new Point(10, 10),
					ForeColor = Color.Gray
				};
				pnlAnswers.Controls.Add(lblNoOptions);
				return;
			}

			for (int i = 0; i < _answerOptions.Count; i++)
			{
				var option = _answerOptions[i];
				var yPos = 10 + (i * 35);

				// CheckBox/Radiobutton для отметки правильности
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
						Enabled = false // Всегда отключаем для пользователей
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
						Enabled = false // Всегда отключаем для пользователей
					};
				}
				else
				{
					// Для других типов вопросов не создаем контролы
					continue;
				}

				// Текст варианта
				var txtOption = new TextBox
				{
					Location = new Point(40, yPos),
					Size = new Size(550, 25),
					Text = option.OptionText,
					Tag = i,
					Name = $"txtOption_{i}",
					ReadOnly = true, // Всегда только чтение для пользователей
					BackColor = SystemColors.Control
				};

				// Добавляем иконку правильности для наглядности
				if (option.IsCorrect)
				{
					var lblCorrect = new Label
					{
						Text = "✓",
						Location = new Point(595, yPos),
						Size = new Size(20, 20),
						ForeColor = Color.Green,
						Font = new Font("Arial", 12, FontStyle.Bold),
						TextAlign = ContentAlignment.MiddleCenter
					};
					pnlAnswers.Controls.Add(lblCorrect);
				}

				// Кнопка удаления показываем только админам
				if (_currentUser.Role == UserRole.Admin)
				{
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
						}
					};

					pnlAnswers.Controls.Add(btnDelete);
				}

				pnlAnswers.Controls.Add(correctControl);
				pnlAnswers.Controls.Add(txtOption);
			}

			// Делаем панель видимой, если есть варианты
			if (_answerOptions.Any())
			{
				pnlAnswers.Visible = true;
				lblAnswers.Visible = true;
			}
		}

		private void AddAnswerOption()
        {
			if (_currentUser.Role != UserRole.Admin)
			{
				MessageBox.Show("Только администраторы могут создавать тесты", "Ограничение",
					MessageBoxButtons.OK,
					MessageBoxIcon.Information);
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
				MessageBox.Show("Только администраторы могут создавать тесты", "Ограничение",
					MessageBoxButtons.OK,
					MessageBoxIcon.Information);
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
				MessageBox.Show("Только администраторы могут создавать тесты", "Ограничение",
					MessageBoxButtons.OK,
					MessageBoxIcon.Information);
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
				MessageBox.Show("Только администраторы могут создавать тесты", "Ограничение",
					MessageBoxButtons.OK,
					MessageBoxIcon.Information);
				return;
			}

			// Валидация
			if (string.IsNullOrWhiteSpace(txtQuestion.Text))
            {
                lblMessage.Text = "Введите текст вопроса";
                return;
            }

            // Собираем данные вариантов ответов
            if (_question.QuestionType != "TextAnswer")
            {
                // Собираем данные из контролов
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

            // Обновляем вопрос
            _question.QuestionText = txtQuestion.Text.Trim();
            _question.AnswerOptions = _answerOptions;
            _question.ImageData = _selectedImageData;
            _question.ImageContentType = _selectedImageContentType;

            // Блокируем кнопку
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