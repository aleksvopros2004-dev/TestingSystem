using TestingSystem.Core.Models;
using TestingSystem.Services.Interfaces;

namespace TestingSystem.WindowsForms;

public partial class CreateQuestionForm : Form
{
    private readonly IQuestionService _questionService;
    private readonly Test _test;
    private List<AnswerOption> _answerOptions = new();

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
        // Тип вопроса
        var lblQuestionType = new Label
        {
            Text = "Тип вопроса:",
            Location = new Point(20, 20),
            Size = new Size(100, 20)
        };

        var cmbQuestionType = new ComboBox
        {
            Location = new Point(130, 20),
            Size = new Size(200, 20),
            DropDownStyle = ComboBoxStyle.DropDownList,
            Name = "cmbQuestionType"
        };
        cmbQuestionType.Items.AddRange(new object[] { "SingleChoice", "MultipleChoice", "TextAnswer" });
        cmbQuestionType.SelectedIndex = 0;
        cmbQuestionType.SelectedIndexChanged += CmbQuestionType_SelectedIndexChanged;

        // Текст вопроса
        var lblQuestionText = new Label
        {
            Text = "Текст вопроса:",
            Location = new Point(20, 60),
            Size = new Size(100, 20)
        };

        var txtQuestionText = new TextBox
        {
            Location = new Point(130, 60),
            Size = new Size(430, 60),
            Multiline = true,
            ScrollBars = ScrollBars.Vertical,
            Name = "txtQuestionText"
        };

        // Панель для вариантов ответов
        var pnlAnswers = new Panel
        {
            Location = new Point(20, 140),
            Size = new Size(540, 200),
            BorderStyle = BorderStyle.FixedSingle,
            Name = "pnlAnswers"
        };

        // Кнопки для управления вариантами ответов
        var btnAddOption = new Button
        {
            Text = "Добавить вариант",
            Location = new Point(20, 350),
            Size = new Size(120, 30),
            Name = "btnAddOption"
        };
        btnAddOption.Click += BtnAddOption_Click;

        // Кнопки сохранения/отмены
        var btnSave = new Button
        {
            Text = "Сохранить",
            Location = new Point(350, 400),
            Size = new Size(100, 30),
            Name = "btnSave"
        };
        btnSave.Click += BtnSave_Click;

        var btnCancel = new Button
        {
            Text = "Отмена",
            Location = new Point(460, 400),
            Size = new Size(100, 30)
        };
        btnCancel.Click += (s, e) => this.Close();

        // Сообщение
        var lblMessage = new Label
        {
            Location = new Point(20, 440),
            Size = new Size(540, 20),
            TextAlign = ContentAlignment.MiddleCenter,
            Name = "lblMessage",
            ForeColor = Color.Red
        };

        this.Controls.AddRange(new Control[]
        {
            lblQuestionType, cmbQuestionType,
            lblQuestionText, txtQuestionText,
            pnlAnswers,
            btnAddOption,
            btnSave, btnCancel,
            lblMessage
        });

        UpdateAnswersPanel();
    }

    private void CmbQuestionType_SelectedIndexChanged(object? sender, EventArgs e)
    {
        var cmbQuestionType = this.Controls.Find("cmbQuestionType", true).FirstOrDefault() as ComboBox;
        var btnAddOption = this.Controls.Find("btnAddOption", true).FirstOrDefault() as Button;

        if (cmbQuestionType?.SelectedItem?.ToString() == "TextAnswer")
        {
            if (btnAddOption != null) btnAddOption.Enabled = false;
            _answerOptions.Clear();
        }
        else
        {
            if (btnAddOption != null) btnAddOption.Enabled = true;
        }

        UpdateAnswersPanel();
    }

    private void BtnAddOption_Click(object? sender, EventArgs e)
    {
        _answerOptions.Add(new AnswerOption
        {
            OptionText = $"Вариант {_answerOptions.Count + 1}",
            IsCorrect = false
        });
        UpdateAnswersPanel();
    }

    private void UpdateAnswersPanel()
    {
        var pnlAnswers = this.Controls.Find("pnlAnswers", true).FirstOrDefault() as Panel;
        var cmbQuestionType = this.Controls.Find("cmbQuestionType", true).FirstOrDefault() as ComboBox;

        if (pnlAnswers == null) return;

        pnlAnswers.Controls.Clear();

        if (cmbQuestionType?.SelectedItem?.ToString() == "TextAnswer")
        {
            var lblInfo = new Label
            {
                Text = "Для текстового ответа варианты не требуются",
                Location = new Point(10, 10),
                Size = new Size(300, 20),
                ForeColor = Color.Gray
            };
            pnlAnswers.Controls.Add(lblInfo);
            return;
        }

        for (int i = 0; i < _answerOptions.Count; i++)
        {
            var option = _answerOptions[i];

            // CheckBox для правильного ответа
            var chkCorrect = new CheckBox
            {
                Location = new Point(10, 10 + i * 30),
                Size = new Size(20, 20),
                Checked = option.IsCorrect,
                Tag = i
            };
            chkCorrect.CheckedChanged += (s, e) =>
            {
                if (chkCorrect.Tag is int index && index < _answerOptions.Count)
                {
                    _answerOptions[index].IsCorrect = chkCorrect.Checked;
                }
            };

            // Поле для текста варианта
            var txtOption = new TextBox
            {
                Location = new Point(40, 10 + i * 30),
                Size = new Size(400, 20),
                Text = option.OptionText,
                Tag = i
            };
            txtOption.TextChanged += (s, e) =>
            {
                if (txtOption.Tag is int index && index < _answerOptions.Count)
                {
                    _answerOptions[index].OptionText = txtOption.Text;
                }
            };

            // Кнопка удаления
            var btnDelete = new Button
            {
                Text = "×",
                Location = new Point(450, 10 + i * 30),
                Size = new Size(25, 20),
                ForeColor = Color.Red,
                Font = new Font("Arial", 10, FontStyle.Bold),
                Tag = i
            };
            btnDelete.Click += (s, e) =>
            {
                if (btnDelete.Tag is int index && index < _answerOptions.Count)
                {
                    _answerOptions.RemoveAt(index);
                    UpdateAnswersPanel();
                }
            };

            pnlAnswers.Controls.AddRange(new Control[] { chkCorrect, txtOption, btnDelete });
        }
    }

    private async void BtnSave_Click(object? sender, EventArgs e)
    {
        var cmbQuestionType = this.Controls.Find("cmbQuestionType", true).FirstOrDefault() as ComboBox;
        var txtQuestionText = this.Controls.Find("txtQuestionText", true).FirstOrDefault() as TextBox;
        var lblMessage = this.Controls.Find("lblMessage", true).FirstOrDefault() as Label;
        var btnSave = this.Controls.Find("btnSave", true).FirstOrDefault() as Button;

        if (cmbQuestionType == null || txtQuestionText == null || lblMessage == null || btnSave == null) return;

        // Валидация
        if (string.IsNullOrWhiteSpace(txtQuestionText.Text))
        {
            lblMessage.Text = "Введите текст вопроса";
            return;
        }

        var questionType = cmbQuestionType.SelectedItem?.ToString();
        if (questionType != "TextAnswer" && _answerOptions.Count == 0)
        {
            lblMessage.Text = "Добавьте хотя бы один вариант ответа";
            return;
        }

        if (questionType != "TextAnswer" && !_answerOptions.Any(o => o.IsCorrect))
        {
            lblMessage.Text = "Укажите хотя бы один правильный вариант";
            return;
        }

        // Блокируем кнопку
        btnSave.Enabled = false;
        lblMessage.Text = "Создание вопроса...";
        lblMessage.ForeColor = Color.Blue;

        try
        {
            // Создаем вопрос
            var question = new Question
            {
                TestId = _test.Id,
                QuestionText = txtQuestionText.Text.Trim(),
                QuestionType = questionType!,
                OrderIndex = await GetNextOrderIndex(),
                AnswerOptions = questionType == "TextAnswer" ? new List<AnswerOption>() : _answerOptions
            };

            var (success, message, questionId) = await _questionService.CreateQuestionAsync(question);

            if (success)
            {
                // Добавляем варианты ответов
                if (questionType != "TextAnswer")
                {
                    foreach (var option in _answerOptions)
                    {
                        option.QuestionId = questionId;
                        await _questionService.AddAnswerOptionAsync(option);
                    }
                }

                lblMessage.Text = "Вопрос успешно создан!";
                lblMessage.ForeColor = Color.Green;

                await Task.Delay(1000);
                QuestionCreated?.Invoke(this, EventArgs.Empty);
                this.Close();
            }
            else
            {
                lblMessage.Text = message;
                lblMessage.ForeColor = Color.Red;
            }
        }
        catch (Exception ex)
        {
            lblMessage.Text = $"Ошибка: {ex.Message}";
            lblMessage.ForeColor = Color.Red;
        }
        finally
        {
            btnSave.Enabled = true;
        }
    }

    private async Task<int> GetNextOrderIndex()
    {
        var questions = await _questionService.GetQuestionsByTestAsync(_test.Id);
        return questions.Any() ? questions.Max(q => q.OrderIndex ?? 0) + 1 : 1;
    }
}