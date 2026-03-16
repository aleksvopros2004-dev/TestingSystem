namespace TestingSystem.WindowsForms
{
    public partial class TestResultForm : Form
    {
        public TestResultForm(string testTitle, int totalQuestions, int earnedPoints, int totalPoints, string timeSpent)
        {
            InitializeComponent();

            lblTestTitle.Text = testTitle;
            lblTotalQuestions.Text = totalQuestions.ToString();
            lblEarnedPoints.Text = earnedPoints.ToString();
            lblTotalPoints.Text = totalPoints.ToString();

            double percentage = totalPoints > 0 ? (double)earnedPoints / totalPoints * 100 : 0;
            lblPercentage.Text = $"{percentage:F1}%";

            lblTimeSpent.Text = timeSpent;

            // Оцениваем результат
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

            lblGrade.Text = grade;
            lblGrade.ForeColor = gradeColor;
        }

        private void BtnOk_Click(object? sender, EventArgs e)
        {
            this.Close();
        }
    }
}