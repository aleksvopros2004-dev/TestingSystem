using TestingSystem.Core.Models;
using TestingSystem.Services.Interfaces;

namespace TestingSystem.WindowsForms;

public partial class CreateQuestionForm : Form
{
    private readonly IQuestionService _questionService;
    private readonly Test _test;
    private List<AnswerOption> _answerOptions = new();
    private Panel? _answersPanel;

    public event EventHandler? QuestionCreated;

    public CreateQuestionForm(IQuestionService questionService, Test test)
    {
        _questionService = questionService;
        _test = test;
        InitializeComponent();
    }

    private void InitializeComponent()
    {
        this.Text = "Добавление вопроса";
        this.Size = new Size(600, 500);
        this.StartPosition = FormStartPosition.CenterParent;
        this.FormBorderStyle = FormBorderStyle.FixedDialog;
        this.MaximizeBox = false;
        CreateControls();
    }

    private void CreateControls()
    {
        // Заголовок
        var lblTitle = new Label
        {
            Text = $"Добавление вопроса к тесту: {_test.Title}",
            Location = new Point(20, 20),
            Size = new Size(560, 25),
            Font = new Font("Arial", 12, FontStyle.Bold),
            TextAlign = ContentAlignment.MiddleCenter
        };

        // Тип вопроса
        var lblType = new Label
        {
            Text = "Тип вопроса:",
            Location = new Point(20, 60),
            Size = new Size(100, 25)
        };

        var cmbType = new ComboBox
        {
            Location = new Point(130, 60),
            Size = new Size(200, 25),
            DropDownStyle = ComboBoxStyle.DropDownList,
            Name = "cmbType"
        };
        cmbType.Items.AddRange(new[] { "Один вариант", "Несколько вариантов", "Текстовый ответ" });
        cmbType.SelectedIndex = 0;
        cmbType.SelectedIndexChanged += (s, e) => UpdateAnswerPanel();

        // Текст вопроса
        var lblText = new Label
        {
            Text = "Текст вопроса:",
            Location = new Point(20, 100),
            Size = new Size(100, 25)
        };

        var txtQuestion = new TextBox
        {
            Location = new Point(130, 100),
            Size = new Size(420, 80),
            Multiline = true,
            ScrollBars = ScrollBars.Vertical,
            Name = "txtQuestion"
        };

        // Панель для вариантов ответов
        _answersPanel = new Panel
        {
            Location = new Point(20, 200),
            Size = new Size(530, 150),
            BorderStyle = BorderStyle.FixedSingle,
            AutoScroll = true,
            Name = "pnlAnswers"
        };

        // Кнопка добавления варианта
        var btnAddOption = new Button
        {
            Text = "Добавить вариант",
            Location = new Point(20, 360),
            Size = new Size(150, 30),
            Name = "btnAddOption"
        };
        btnAddOption.Click += (s, e) => AddAnswerOption();

        // Кнопки сохранения/отмены
        var btnSave = new Button
        {
            Text = "Сохранить",
            Location = new Point(350, 360),
            Size = new Size(100, 35),
            Name = "btnSave"
        };
        btnSave.Click += async (s, e) => await SaveQuestion();

        var btnCancel = new Button
        {
            Text = "Отмена",
            Location = new Point(460, 360),
            Size = new Size(100, 35)
        };
        btnCancel.Click += (s, e) => this.Close();

        // Сообщение
        var lblMessage = new Label
        {
            Location = new Point(20, 410),
            Size = new Size(540, 40),
            TextAlign = ContentAlignment.MiddleCenter,
            Name = "lblMessage",
            ForeColor = Color.Red
        };

        this.Controls.AddRange(new Control[]
        {
            lblTitle,
            lblType, cmbType,
            lblText, txtQuestion,
            _answersPanel,
            btnAddOption,
            btnSave, btnCancel,
            lblMessage
        });

        // Добавляем начальные варианты
        AddAnswerOption();
        AddAnswerOption();
    }

    private void AddAnswerOption()
    {
        if (_answersPanel == null) return;

        var index = _answerOptions.Count;
        var yPos = 10 + (index * 35);

        // Получаем тип вопроса
        var cmbType = this.Controls.Find("cmbType", true).FirstOrDefault() as ComboBox;
        var questionType = cmbType?.SelectedIndex ?? 0;

        // Элемент для отметки правильности
        Control correctControl;
        if (questionType == 0) // Один вариант
        {
            correctControl = new RadioButton
            {
                Location = new Point(10, yPos),
                Size = new Size(20, 20),
                Tag = index,
                Name = $"rdoCorrect_{index}"
            };
        }
        else if (questionType == 1) // Несколько вариантов
        {
            correctControl = new CheckBox
            {
                Location = new Point(10, yPos),
                Size = new Size(20, 20),
                Tag = index,
                Name = $"chkCorrect_{index}"
            };
        }
        else // Текстовый ответ
        {
            return; // Для текстового ответа варианты не нужны
        }

        // Поле для текста варианта
        var txtOption = new TextBox
        {
            Location = new Point(40, yPos),
            Size = new Size(400, 25),
            Text = $"Вариант {index + 1}",
            Tag = index,
            Name = $"txtOption_{index}"
        };

        // Кнопка удаления
        var btnDelete = new Button
        {
            Text = "×",
            Location = new Point(450, yPos),
            Size = new Size(30, 25),
            ForeColor = Color.Red,
            Font = new Font("Arial", 10, FontStyle.Bold),
            Tag = index,
            Name = $"btnDelete_{index}"
        };
        btnDelete.Click += (s, e) =>
        {
            if (btnDelete.Tag is int idx)
            {
                // Удаляем контролы
                _answersPanel.Controls.RemoveByKey($"rdoCorrect_{idx}");
                _answersPanel.Controls.RemoveByKey($"chkCorrect_{idx}");
                _answersPanel.Controls.RemoveByKey($"txtOption_{idx}");
                _answersPanel.Controls.RemoveByKey($"btnDelete_{idx}");

                // Удаляем из списка
                _answerOptions.RemoveAll(o => true); // Упрощенно - пересоздадим весь список позже

                // Пересоздаем варианты
                RecreateAnswerOptions();
            }
        };

        _answersPanel.Controls.Add(correctControl);
        _answersPanel.Controls.Add(txtOption);
        _answersPanel.Controls.Add(btnDelete);

        // Сохраняем вариант
        _answerOptions.Add(new AnswerOption
        {
            OptionText = $"Вариант {index + 1}",
            IsCorrect = false
        });
    }

    private void UpdateAnswerPanel()
    {
        var cmbType = this.Controls.Find("cmbType", true).FirstOrDefault() as ComboBox;
        var questionType = cmbType?.SelectedIndex ?? 0;

        // Показываем/скрываем панель с вариантами
        if (_answersPanel != null)
        {
            _answersPanel.Visible = questionType != 2; // 2 = текстовый ответ
        }

        // Обновляем кнопку добавления
        var btnAddOption = this.Controls.Find("btnAddOption", true).FirstOrDefault() as Button;
        if (btnAddOption != null)
        {
            btnAddOption.Enabled = questionType != 2;
        }

        // Пересоздаем варианты для нового типа
        RecreateAnswerOptions();
    }

    private void RecreateAnswerOptions()
    {
        if (_answersPanel == null) return;

        // Очищаем панель
        _answersPanel.Controls.Clear();

        // Пересоздаем все варианты
        for (int i = 0; i < _answerOptions.Count; i++)
        {
            var yPos = 10 + (i * 35);
            var cmbType = this.Controls.Find("cmbType", true).FirstOrDefault() as ComboBox;
            var questionType = cmbType?.SelectedIndex ?? 0;

            Control correctControl;
            if (questionType == 0) // Один вариант
            {
                correctControl = new RadioButton
                {
                    Location = new Point(10, yPos),
                    Size = new Size(20, 20),
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
                    Tag = i,
                    Name = $"chkCorrect_{i}"
                };
            }

            var txtOption = new TextBox
            {
                Location = new Point(40, yPos),
                Size = new Size(400, 25),
                Text = _answerOptions[i].OptionText,
                Tag = i,
                Name = $"txtOption_{i}"
            };

            var btnDelete = new Button
            {
                Text = "×",
                Location = new Point(450, yPos),
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
                    RecreateAnswerOptions();
                }
            };

            _answersPanel.Controls.Add(correctControl);
            _answersPanel.Controls.Add(txtOption);
            _answersPanel.Controls.Add(btnDelete);
        }
    }

    private async Task SaveQuestion()
    {
        var txtQuestion = this.Controls.Find("txtQuestion", true).FirstOrDefault() as TextBox;
        var cmbType = this.Controls.Find("cmbType", true).FirstOrDefault() as ComboBox;
        var lblMessage = this.Controls.Find("lblMessage", true).FirstOrDefault() as Label;
        var btnSave = this.Controls.Find("btnSave", true).FirstOrDefault() as Button;

        if (txtQuestion == null || cmbType == null || lblMessage == null || btnSave == null)
            return;

        // Валидация
        if (string.IsNullOrWhiteSpace(txtQuestion.Text))
        {
            lblMessage.Text = "Введите текст вопроса";
            return;
        }

        // Определяем тип вопроса
        string questionType = cmbType.SelectedIndex switch
        {
            0 => "SingleChoice",
            1 => "MultipleChoice",
            2 => "TextAnswer",
            _ => "SingleChoice"
        };

        // Собираем варианты ответов
        var answerOptions = new List<AnswerOption>();
        if (questionType != "TextAnswer" && _answersPanel != null)
        {
            // Собираем данные из контролов
            for (int i = 0; i < _answerOptions.Count; i++)
            {
                var txtOption = _answersPanel.Controls.Find($"txtOption_{i}", true).FirstOrDefault() as TextBox;
                if (txtOption != null && !string.IsNullOrWhiteSpace(txtOption.Text))
                {
                    bool isCorrect = false;

                    if (questionType == "SingleChoice")
                    {
                        var rdo = _answersPanel.Controls.Find($"rdoCorrect_{i}", true).FirstOrDefault() as RadioButton;
                        isCorrect = rdo?.Checked ?? false;
                    }
                    else if (questionType == "MultipleChoice")
                    {
                        var chk = _answersPanel.Controls.Find($"chkCorrect_{i}", true).FirstOrDefault() as CheckBox;
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

            // Проверяем правильные ответы
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

        // Определяем порядковый номер
        var questions = await _questionService.GetQuestionsByTestAsync(_test.Id);
        var orderIndex = questions.Any() ? questions.Max(q => q.OrderIndex) + 1 : 1;

        // Создаем вопрос
        var question = new Question
        {
            TestId = _test.Id,
            QuestionText = txtQuestion.Text.Trim(),
            QuestionType = questionType,
            OrderIndex = orderIndex,
            AnswerOptions = answerOptions
        };

        // Блокируем кнопку
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
}