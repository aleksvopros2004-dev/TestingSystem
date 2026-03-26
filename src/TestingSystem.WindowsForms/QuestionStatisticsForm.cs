using System.Data;
using TestingSystem.Core.Models;
using TestingSystem.Services.Services;

namespace TestingSystem.WindowsForms;

public partial class QuestionStatisticsForm : Form
{
    private readonly QuestionStatistics _question;
    private readonly LemmatizationService _lemmatizationService;

    public QuestionStatisticsForm(QuestionStatistics question, LemmatizationService lemmatizationService)
    {
        _question = question;
        _lemmatizationService = lemmatizationService;
        InitializeComponent();
        LoadQuestionData();
    }

    private void LoadQuestionData()
    {
        lblQuestionText.Text = _question.QuestionText;
        lblType.Text = _question.QuestionType switch
        {
            "SingleChoice" => "Один вариант",
            "MultipleChoice" => "Несколько вариантов",
            "TextAnswer" => "Текстовый ответ",
            _ => _question.QuestionType
        };
        lblStats.Text = $"Всего ответов: {_question.TotalAnswers} | " +
                        $"Правильных: {_question.CorrectAnswers} | " +
                        $"Правильных (%): {_question.CorrectPercentage:F1}% | " +
                        $"Баллов: {_question.Points} | " +
                        $"Средний балл: {_question.AveragePointsEarned:F1}";

        if (_question.OptionPopularity.Any())
        {
            LoadOptionPopularity();
        }
        else if (_question.CommonWords.Any())
        {
            LoadWordFrequency();
        }
        else
        {
            lblNoData.Visible = true;
        }
    }

    private void LoadOptionPopularity()
    {
        listViewOptions.Visible = true;
        listViewOptions.Items.Clear();

        foreach (var option in _question.OptionPopularity.OrderByDescending(o => o.SelectionCount))
        {
            var item = new ListViewItem(option.OptionText);
            item.SubItems.Add(option.SelectionCount.ToString());
            item.SubItems.Add($"{option.SelectionPercentage:F1}%");
            item.SubItems.Add(option.IsCorrect ? "✓" : "");
            item.BackColor = option.IsCorrect ? Color.FromArgb(200, 255, 200) : Color.White;
            listViewOptions.Items.Add(item);
        }
    }

    private void LoadWordFrequency()
    {
        listViewWords.Visible = true;
        listViewWords.Items.Clear();

        foreach (var word in _question.CommonWords)
        {
            var item = new ListViewItem(word.NormalizedForm);
            item.SubItems.Add(word.Count.ToString());
            listViewWords.Items.Add(item);
        }
    }
}