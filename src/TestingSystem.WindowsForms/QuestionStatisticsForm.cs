using TestingSystem.Core.Interfaces;  // ← добавляем using
using TestingSystem.Core.Models;

namespace TestingSystem.WindowsForms
{
    public partial class QuestionStatisticsForm : Form
    {
        private readonly QuestionStatistics _question;
        private readonly ILemmatizationService _lemmatizationService;  // ← интерфейс

        public QuestionStatisticsForm(QuestionStatistics question, ILemmatizationService lemmatizationService)
        {
            _question = question;
            _lemmatizationService = lemmatizationService;

            InitializeComponent();
            this.btnClose.Click += BtnClose_Click;
            LoadQuestionData();
        }

        private void LoadQuestionData()
        {
            // Текст вопроса
            lblQuestionText.Text = _question.QuestionText;

            // Тип вопроса
            string questionTypeText = _question.QuestionType switch
            {
                "SingleChoice" => "Один вариант",
                "MultipleChoice" => "Несколько вариантов",
                "TextAnswer" => "Текстовый ответ",
                _ => _question.QuestionType ?? "Неизвестный тип"
            };
            lblType.Text = questionTypeText;

            // Статистика
            lblStats.Text = $"Всего ответов: {_question.TotalAnswers} | " +
                            $"Правильных: {_question.CorrectAnswers} | " +
                            $"Правильных (%): {_question.CorrectPercentage:F1}% | " +
                            $"Баллов: {_question.Points} | " +
                            $"Средний балл: {_question.AveragePointsEarned:F1}";

            // Определяем, какие данные показывать
            if (_question.OptionPopularity != null && _question.OptionPopularity.Any())
            {
                LoadOptionPopularity();
                lblWordsTitle.Visible = false;
                listViewWords.Visible = false;
                lblOptionsTitle.Visible = true;
                listViewOptions.Visible = true;
                lblNoData.Visible = false;
            }
            else if (_question.CommonWords != null && _question.CommonWords.Any())
            {
                LoadWordFrequency();
                lblOptionsTitle.Visible = false;
                listViewOptions.Visible = false;
                lblWordsTitle.Visible = true;
                listViewWords.Visible = true;
                lblNoData.Visible = false;
            }
            else
            {
                lblOptionsTitle.Visible = false;
                listViewOptions.Visible = false;
                lblWordsTitle.Visible = false;
                listViewWords.Visible = false;
                lblNoData.Visible = true;
            }
        }

        private void LoadOptionPopularity()
        {
            listViewOptions.Items.Clear();

            var sortedOptions = _question.OptionPopularity
                .OrderByDescending(o => o.SelectionCount)
                .ToList();

            foreach (var option in sortedOptions)
            {
                var item = new ListViewItem(option.OptionText);
                item.SubItems.Add(option.SelectionCount.ToString());
                item.SubItems.Add($"{option.SelectionPercentage:F1}%");
                item.SubItems.Add(option.IsCorrect ? "✓ Да" : "✗ Нет");

                if (option.IsCorrect)
                {
                    item.BackColor = Color.FromArgb(200, 255, 200);
                }
                else if (option.SelectionPercentage > 30)
                {
                    item.BackColor = Color.FromArgb(255, 200, 200);
                }

                listViewOptions.Items.Add(item);
            }

            // Автоматическая ширина колонок
            foreach (ColumnHeader col in listViewOptions.Columns)
            {
                col.Width = -2;
            }
        }

        private void LoadWordFrequency()
        {
            listViewWords.Items.Clear();

            foreach (var word in _question.CommonWords.OrderByDescending(w => w.Count))
            {
                var item = new ListViewItem(word.NormalizedForm);
                item.SubItems.Add(word.Count.ToString());
                listViewWords.Items.Add(item);
            }

            // Автоматическая ширина колонок
            foreach (ColumnHeader col in listViewWords.Columns)
            {
                col.Width = -2;
            }
        }

        private void BtnClose_Click(object? sender, EventArgs e)
        {
            this.Close();
        }
    }
}