using System.Text;
using TestingSystem.Core.Models;

namespace TestingSystem.WindowsForms
{
    public partial class TextAnswersViewForm : Form
    {
        private readonly List<UserTextAnswer> _answers;
        private readonly string _questionText;

        public TextAnswersViewForm(List<UserTextAnswer> answers, string questionText)
        {
            _answers = answers;
            _questionText = questionText;
            InitializeComponent();

            this.Text = $"Ответы на вопрос: {questionText}";
            LoadAnswers();
        }

        private void LoadAnswers()
        {
            listViewAnswers.Items.Clear();
            lblCount.Text = $"Всего: {_answers.Count} ответов";

            foreach (var answer in _answers.OrderByDescending(a => a.AnswerDate))
            {
                var item = new ListViewItem(answer.UserName);
                item.SubItems.Add(answer.AnswerDate.ToString("dd.MM.yyyy HH:mm"));
                item.SubItems.Add(answer.AnswerText);
                item.SubItems.Add(answer.PointsEarned.ToString());

                // Подсветка длинных ответов
                if (answer.AnswerText.Length > 200)
                {
                    item.BackColor = Color.FromArgb(255, 255, 200);
                }

                listViewAnswers.Items.Add(item);
            }

            // Автоширина колонок
            colUserName.Width = 120;
            colDate.Width = 120;
            colAnswerText.Width = 450;
            colPoints.Width = 60;
        }

        private void BtnExport_Click(object sender, EventArgs e)
        {
            using var saveDialog = new SaveFileDialog
            {
                Filter = "CSV files (*.csv)|*.csv|Text files (*.txt)|*.txt",
                FileName = $"answers_{DateTime.Now:yyyyMMdd_HHmmss}.csv"
            };

            if (saveDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    var sb = new StringBuilder();
                    sb.AppendLine("Пользователь;Дата;Ответ;Баллы");

                    foreach (var answer in _answers)
                    {
                        sb.AppendLine($"\"{answer.UserName}\";\"{answer.AnswerDate:dd.MM.yyyy HH:mm}\";\"{answer.AnswerText.Replace("\"", "\"\"")}\";{answer.PointsEarned}");
                    }

                    File.WriteAllText(saveDialog.FileName, sb.ToString(), Encoding.UTF8);

                    MessageBox.Show($"Экспортировано {_answers.Count} ответов", "Успех",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Ошибка экспорта: {ex.Message}", "Ошибка",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void BtnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}