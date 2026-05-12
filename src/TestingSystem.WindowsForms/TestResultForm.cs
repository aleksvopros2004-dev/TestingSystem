using System.Text;
using TestingSystem.Core.Models;

namespace TestingSystem.WindowsForms
{
    public partial class TestResultForm : Form
    {
        public TestResultForm(string testTitle, int totalQuestions, int earnedPoints, int totalPoints,
    string timeSpent, List<QuestionResult>? questionResults = null)
        {
            InitializeComponent();

            lblTestTitle.Text = testTitle;
            lblTotalQuestions.Text = totalQuestions.ToString();
            lblPointsValue.Text = $"{earnedPoints} из {totalPoints}";

            double percentage = totalPoints > 0 ? (double)earnedPoints / totalPoints * 100 : 0;
            lblPercentage.Text = $"{percentage:F1}%";
            lblTimeSpent.Text = timeSpent;

            // Оценка
            string grade;
            Color gradeColor;

            if (percentage >= 90)
            {
                grade = "Отлично!";
                gradeColor = Color.FromArgb(76, 175, 80);
            }
            else if (percentage >= 75)
            {
                grade = "Хорошо";
                gradeColor = Color.FromArgb(33, 150, 243);
            }
            else if (percentage >= 60)
            {
                grade = "Удовлетворительно";
                gradeColor = Color.FromArgb(255, 152, 0);
            }
            else
            {
                grade = "Попробуйте еще раз";
                gradeColor = Color.FromArgb(244, 67, 54);
            }

            lblGradeText.Text = grade;
            lblGradeText.ForeColor = gradeColor;

            LoadRecommendations(questionResults);
        }

        private void LoadRecommendations(List<QuestionResult>? questionResults)
        {
            pnlRecommendationList.Controls.Clear();

            // Фильтруем только неправильные с рекомендациями
            var wrongWithRecommendations = questionResults?
                .Where(q => !q.IsCorrect && !string.IsNullOrWhiteSpace(q.Recommendation))
                .ToList();

            if (wrongWithRecommendations == null || wrongWithRecommendations.Count == 0)
            {
                panelRecommendations.Visible = false;
                tableLayout.RowStyles[2].Height = 0;
                this.Height = 420;
                return;
            }

            panelRecommendations.Visible = true;
            tableLayout.RowStyles[2].Height = 380;

            lblRecommendationTitle.Visible = false;

            pnlRecommendationList.Dock = DockStyle.Fill;
            pnlRecommendationList.AutoScroll = true;
            pnlRecommendationList.Padding = new Padding(10, 10, 10, 10);

            var titleLabel = new Label
            {
                Text = $"Рекомендации по ошибкам ({wrongWithRecommendations.Count}):",
                Location = new Point(10, 10),
                Size = new Size(pnlRecommendationList.Width - 25, 35),
                Font = new Font("Segoe UI", 11F, FontStyle.Bold),
                ForeColor = Color.FromArgb(255, 152, 0),
                TextAlign = ContentAlignment.MiddleLeft
            };
            pnlRecommendationList.Controls.Add(titleLabel);

            int yPos = 55;

            foreach (var result in wrongWithRecommendations)
            {
                string shortQuestion = result.QuestionText.Length > 80
                    ? result.QuestionText.Substring(0, 77) + "..."
                    : result.QuestionText;

                int cardWidth = pnlRecommendationList.Width - 40;

                // Панель-карточка
                var card = new Panel
                {
                    Location = new Point(10, yPos),
                    Size = new Size(cardWidth, 75),
                    BackColor = Color.FromArgb(255, 248, 240),
                    BorderStyle = BorderStyle.FixedSingle,
                    Padding = new Padding(10, 8, 10, 8)
                };

                // Вопрос
                var lblQ = new Label
                {
                    Text = $" {shortQuestion}",
                    Location = new Point(8, 5),
                    Size = new Size(card.Width - 20, 22),
                    Font = new Font("Segoe UI", 9F, FontStyle.Bold),
                    ForeColor = Color.FromArgb(180, 30, 30),
                    AutoSize = false,
                    AutoEllipsis = true
                };

                // Рекомендация
                var lblR = new Label
                {
                    Text = $" {result.Recommendation}",
                    Location = new Point(8, 30),
                    Size = new Size(card.Width - 20, 38),
                    Font = new Font("Segoe UI", 9F),
                    ForeColor = Color.FromArgb(60, 60, 60),
                    AutoSize = false
                };

                card.Controls.Add(lblQ);
                card.Controls.Add(lblR);
                pnlRecommendationList.Controls.Add(card);

                yPos += 85;
            }
        }

        private void BtnOk_Click(object? sender, EventArgs e)
        {
            this.Close();
        }
    }

    /// <summary>
    /// Модель для передачи результатов вопроса в форму
    /// </summary>
    public class QuestionResult
    {
        public string QuestionText { get; set; } = string.Empty;
        public bool IsCorrect { get; set; }
        public string? Recommendation { get; set; }
        public int EarnedPoints { get; set; }
        public int TotalPoints { get; set; }
    }
}