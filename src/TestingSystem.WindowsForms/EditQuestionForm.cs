using TestingSystem.Core.Models;
using TestingSystem.Services.Interfaces;

namespace TestingSystem.WindowsForms;

public partial class EditQuestionForm : Form
{
    private readonly IQuestionService _questionService;
    private readonly Question _question;
    private List<AnswerOption> _answerOptions = new();
    private Panel? _answersPanel;

    public event EventHandler? QuestionUpdated;

    public EditQuestionForm(IQuestionService questionService, Question question)
    {
        _questionService = questionService;
        _question = question;
        _answerOptions = question.AnswerOptions?.ToList() ?? new List<AnswerOption>();
        InitializeComponent();
        LoadQuestionData();
    }

    private void InitializeComponent()
    {
        this.Text = "Редактирование вопроса";
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
            Text = "Редактирование вопроса",
            Location = new Point(20, 20),
            Size = new Size(560, 25),
            Font = new Font("Arial", 12, FontStyle.Bold),
            TextAlign = ContentAlignment.MiddleCenter
        };

        // Тип вопроса (только для чтения)
        var lblType = new Label
        {
            Text = "Тип вопроса:",
            Location = new Point(20, 60),
            Size = new Size(100, 25)
        };

        var txtType = new TextBox
        {
            Location = new Point(130, 60),
            Size = new Size(200, 25),
            ReadOnly = true
        };

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

        // Порядковый номер (только для отображения, без возможности изменения)
        var lblOrder = new Label
        {
            Text = "Порядковый номер:",
            Location = new Point(20, 190),
            Size = new Size(120, 25)
        };

        var txtOrder = new TextBox
        {
            Location = new Point(150, 190),
            Size = new Size(100, 25),
            ReadOnly = true,
            BackColor = SystemColors.Control,
            Name = "txtOrder"
        };

        // Панель для вариантов ответов (если не текстовый вопрос)
        if (_question.QuestionType != "TextAnswer")
        {
            var lblAnswers = new Label
            {
                Text = "Варианты ответов:",
                Location = new Point(20, 230),
                Size = new Size(150, 25),
                Font = new Font("Arial", 9, FontStyle.Bold)
            };

            _answersPanel = new Panel
            {
                Location = new Point(20, 260),
                Size = new Size(530, 150),
                BorderStyle = BorderStyle.FixedSingle,
                AutoScroll = true,
                Name = "pnlAnswers"
            };

            // Кнопка добавления варианта
            var btnAddOption = new Button
            {
                Text = "Добавить вариант",
                Location = new Point(20, 420),
                Size = new Size(150, 30)
            };

            btnAddOption.Click += (s, e) => AddAnswerOption();

            this.Controls.AddRange(new Control[] { lblAnswers, _answersPanel, btnAddOption });
        }

        // Кнопки сохранения/отмены
        var btnSave = new Button
        {
            Text = "Сохранить",
            Location = new Point(350, _question.QuestionType == "TextAnswer" ? 230 : 420),
            Size = new Size(100, 35),
            Name = "btnSave"
        };

        btnSave.Click += async (s, e) => await SaveQuestion();

        var btnCancel = new Button
        {
            Text = "Отмена",
            Location = new Point(460, _question.QuestionType == "TextAnswer" ? 230 : 420),
            Size = new Size(100, 35)
        };

        btnCancel.Click += (s, e) => this.Close();

        // Сообщение
        var lblMessage = new Label
        {
            Location = new Point(20, _question.QuestionType == "TextAnswer" ? 280 : 470),
            Size = new Size(540, 40),
            TextAlign = ContentAlignment.MiddleCenter,
            Name = "lblMessage",
            ForeColor = Color.Red
        };

        this.Controls.AddRange(new Control[]
        {
            lblTitle,
            lblType, txtType,
            lblText, txtQuestion,
            lblOrder, txtOrder, // Изменено с numOrder на txtOrder
            btnSave, btnCancel,
            lblMessage
        });
    }

    private void LoadQuestionData()
    {
        var txtType = this.Controls.OfType<TextBox>().FirstOrDefault(t => !t.Multiline && t.ReadOnly);
        var txtQuestion = this.Controls.Find("txtQuestion", true).FirstOrDefault() as TextBox;
        var txtOrder = this.Controls.Find("txtOrder", true).FirstOrDefault() as TextBox; // Изменено с numOrder на txtOrder

        if (txtType != null)
        {
            txtType.Text = _question.QuestionType switch
            {
                "SingleChoice" => "Один вариант",
                "MultipleChoice" => "Несколько вариантов",
                "TextAnswer" => "Текстовый ответ",
                _ => _question.QuestionType
            };
        }

        if (txtQuestion != null) txtQuestion.Text = _question.QuestionText;
        if (txtOrder != null) txtOrder.Text = _question.OrderIndex.ToString(); // Просто текст, без возможности изменения

        // Загружаем варианты ответов
        if (_answersPanel != null && _answerOptions.Any())
        {
            LoadAnswerOptions();
        }
    }

    private void LoadAnswerOptions()
    {
        if (_answersPanel == null) return;

        _answersPanel.Controls.Clear();

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
                    Name = $"rdoCorrect_{i}"
                };
            }
            else
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
                Size = new Size(400, 25),
                Text = option.OptionText,
                Tag = i,
                Name = $"txtOption_{i}"
            };

            // Кнопка удаления
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
                if (btnDelete.Tag is int index && index < _answerOptions.Count)
                {
                    _answerOptions.RemoveAt(index);
                    LoadAnswerOptions();
                }
            };

            _answersPanel.Controls.Add(correctControl);
            _answersPanel.Controls.Add(txtOption);
            _answersPanel.Controls.Add(btnDelete);
        }
    }

    private void AddAnswerOption()
    {
        if (_answersPanel == null) return;

        var newOption = new AnswerOption
        {
            OptionText = $"Вариант {_answerOptions.Count + 1}",
            IsCorrect = false
        };

        _answerOptions.Add(newOption);
        LoadAnswerOptions();
    }

    private async Task SaveQuestion()
    {
        var txtQuestion = this.Controls.Find("txtQuestion", true).FirstOrDefault() as TextBox;
        var lblMessage = this.Controls.Find("lblMessage", true).FirstOrDefault() as Label;
        var btnSave = this.Controls.Find("btnSave", true).FirstOrDefault() as Button;

        if (txtQuestion == null || lblMessage == null || btnSave == null)
            return;

        // Валидация
        if (string.IsNullOrWhiteSpace(txtQuestion.Text))
        {
            lblMessage.Text = "Введите текст вопроса";
            return;
        }

        // Собираем данные вариантов ответов
        if (_question.QuestionType != "TextAnswer" && _answersPanel != null)
        {
            // Собираем данные из контролов
            for (int i = 0; i < _answerOptions.Count; i++)
            {
                var txtOption = _answersPanel.Controls.Find($"txtOption_{i}", true).FirstOrDefault() as TextBox;
                if (txtOption != null)
                {
                    bool isCorrect = false;
                    if (_question.QuestionType == "SingleChoice")
                    {
                        var rdo = _answersPanel.Controls.Find($"rdoCorrect_{i}", true).FirstOrDefault() as RadioButton;
                        isCorrect = rdo?.Checked ?? false;
                    }
                    else if (_question.QuestionType == "MultipleChoice")
                    {
                        var chk = _answersPanel.Controls.Find($"chkCorrect_{i}", true).FirstOrDefault() as CheckBox;
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

        // Обновляем вопрос (порядковый номер остается без изменений)
        _question.QuestionText = txtQuestion.Text.Trim();
        _question.AnswerOptions = _answerOptions;

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
}