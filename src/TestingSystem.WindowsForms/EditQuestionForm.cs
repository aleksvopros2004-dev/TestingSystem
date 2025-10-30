using TestingSystem.Core.Models;
using TestingSystem.Services.Interfaces;

namespace TestingSystem.WindowsForms;

public partial class EditQuestionForm : Form
{
    private readonly IQuestionService _questionService;
    private readonly Question _question;
    private List<AnswerOption> _answerOptions = new();

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
            Name = "cmbQuestionType",
            Enabled = false // Нельзя менять тип после создания
        };
        cmbQuestionType.Items.AddRange(new object[] { "SingleChoice", "MultipleChoice", "TextAnswer" });

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

        // Порядковый номер
        var lblOrderIndex = new Label
        {
            Text = "Порядковый номер:",
            Location = new Point(20, 130),
            Size = new Size(100, 20)
        };

        var numOrderIndex = new NumericUpDown
        {
            Location = new Point(130, 130),
            Size = new Size(80, 20),
            Minimum = 1,
            Maximum = 100,
            Name = "numOrderIndex"
        };

        // Панель для вариантов ответов
        var pnlAnswers = new Panel
        {
            Location = new Point(20, 160),
            Size = new Size(540, 200),
            BorderStyle = BorderStyle.FixedSingle,
            Name = "pnlAnswers"
        };

        // Кнопки для управления вариантами ответов
        var btnAddOption = new Button
        {
            Text = "Добавить вариант",
            Location = new Point(20, 370),
            Size = new Size(120, 30),
            Name = "btnAddOption"
        };
        btnAddOption.Click += BtnAddOption_Click;

        // Кнопки сохранения/отмены
        var btnSave = new Button
        {
            Text = "Сохранить",
            Location = new Point(350, 420),
            Size = new Size(100, 30),
            Name = "btnSave"
        };
        btnSave.Click += BtnSave_Click;

        var btnCancel = new Button
        {
            Text = "Отмена",
            Location = new Point(460, 420),
            Size = new Size(100, 30)
        };
        btnCancel.Click += (s, e) => this.Close();

        // Сообщение
        var lblMessage = new Label
        {
            Location = new Point(20, 460),
            Size = new Size(540, 20),
            TextAlign = ContentAlignment.MiddleCenter,
            Name = "lblMessage",
            ForeColor = Color.Red
        };

        this.Controls.AddRange(new Control[]
        {
            lblQuestionType, cmbQuestionType,
            lblQuestionText, txtQuestionText,
            lblOrderIndex, numOrderIndex,
            pnlAnswers,
            btnAddOption,
            btnSave, btnCancel,
            lblMessage
        });

        UpdateAnswersPanel();
    }

    private void LoadQuestionData()
    {
        var cmbQuestionType = this.Controls.Find("cmbQuestionType", true).FirstOrDefault() as ComboBox;
        var txtQuestionText = this.Controls.Find("txtQuestionText", true).FirstOrDefault() as TextBox;
        var numOrderIndex = this.Controls.Find("numOrderIndex", true).FirstOrDefault() as NumericUpDown;
        var btnAddOption = this.Controls.Find("btnAddOption", true).FirstOrDefault() as Button;

        if (cmbQuestionType != null)
        {
            cmbQuestionType.SelectedItem = _question.QuestionType;
            btnAddOption.Enabled = _question.QuestionType != "TextAnswer";
        }
        if (txtQuestionText != null) txtQuestionText.Text = _question.QuestionText;
        if (numOrderIndex != null) numOrderIndex.Value = _question.OrderIndex ?? 1;
    }

    private void BtnAddOption_Click(object? sender, EventArgs e)
    {
        _answerOptions.Add(new AnswerOption
        {
            QuestionId = _question.Id,
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

            // RadioButton или CheckBox в зависимости от типа вопроса
            Control correctControl;
            if (cmbQuestionType?.SelectedItem?.ToString() == "SingleChoice")
            {
                correctControl = new RadioButton
                {
                    Location = new Point(10, 10 + i * 30),
                    Size = new Size(20, 20),
                    Checked = option.IsCorrect,
                    Tag = i
                };
                ((RadioButton)correctControl).CheckedChanged += (s, e) =>
                {
                    if (((RadioButton)correctControl).Checked)
                    {
                        // Сбрасываем все остальные для SingleChoice
                        foreach (var opt in _answerOptions)
                        {
                            opt.IsCorrect = false;
                        }
                        _answerOptions[i].IsCorrect = true;
                        UpdateAnswersPanel(); // Обновляем для отображения изменений
                    }
                };
            }
            else
            {
                correctControl = new CheckBox
                {
                    Location = new Point(10, 10 + i * 30),
                    Size = new Size(20, 20),
                    Checked = option.IsCorrect,
                    Tag = i
                };
                ((CheckBox)correctControl).CheckedChanged += (s, e) =>
                {
                    if (correctControl.Tag is int index && index < _answerOptions.Count)
                    {
                        _answerOptions[index].IsCorrect = ((CheckBox)correctControl).Checked;
                    }
                };
            }

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

            pnlAnswers.Controls.AddRange(new Control[] { correctControl, txtOption, btnDelete });
        }
    }

    private async void BtnSave_Click(object? sender, EventArgs e)
    {
        var txtQuestionText = this.Controls.Find("txtQuestionText", true).FirstOrDefault() as TextBox;
        var numOrderIndex = this.Controls.Find("numOrderIndex", true).FirstOrDefault() as NumericUpDown;
        var lblMessage = this.Controls.Find("lblMessage", true).FirstOrDefault() as Label;
        var btnSave = this.Controls.Find("btnSave", true).FirstOrDefault() as Button;
        var cmbQuestionType = this.Controls.Find("cmbQuestionType", true).FirstOrDefault() as ComboBox;

        if (txtQuestionText == null || lblMessage == null || btnSave == null) return;

        // Валидация
        if (string.IsNullOrWhiteSpace(txtQuestionText.Text))
        {
            lblMessage.Text = "Введите текст вопроса";
            return;
        }

        var questionType = cmbQuestionType?.SelectedItem?.ToString();
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
        lblMessage.Text = "Сохранение...";
        lblMessage.ForeColor = Color.Blue;

        try
        {
            // Обновляем вопрос
            _question.QuestionText = txtQuestionText.Text.Trim();
            _question.OrderIndex = (int?)numOrderIndex?.Value;

            var (success, message) = await _questionService.UpdateQuestionAsync(_question);

            if (success)
            {
                // Обновляем варианты ответов (в реальном приложении нужно более сложное управление)
                // Для простоты удаляем все старые и добавляем новые
                await _questionService.DeleteQuestionAsync(_question.Id);

                var newQuestion = new Question
                {
                    TestId = _question.TestId,
                    QuestionText = _question.QuestionText,
                    QuestionType = _question.QuestionType,
                    OrderIndex = _question.OrderIndex,
                    AnswerOptions = questionType == "TextAnswer" ? new List<AnswerOption>() : _answerOptions
                };

                var (createSuccess, createMessage, newQuestionId) = await _questionService.CreateQuestionAsync(newQuestion);

                if (createSuccess && questionType != "TextAnswer")
                {
                    foreach (var option in _answerOptions)
                    {
                        option.QuestionId = newQuestionId;
                        await _questionService.AddAnswerOptionAsync(option);
                    }
                }

                lblMessage.Text = "Вопрос успешно обновлен!";
                lblMessage.ForeColor = Color.Green;

                await Task.Delay(1000);
                QuestionUpdated?.Invoke(this, EventArgs.Empty);
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
}