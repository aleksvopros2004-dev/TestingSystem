using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TestingSystem.Core.Models;
using TestingSystem.Services.Interfaces;

namespace TestingSystem.WindowsForms
{
    public partial class QuestionManagementForm : Form
    {
        private readonly IQuestionService _questionService;
        private readonly Test _test;
        private List<Question> _questions = new();

        public QuestionManagementForm(IQuestionService questionService, Test test)
        {
            _questionService = questionService;
            _test = test;
            InitializeComponent();
            LoadQuestionsAsync();
        }

        private void InitializeComponent()
        {
            this.Text = $"Управление вопросами: {_test.Title}";
            this.Size = new Size(800, 600);
            this.StartPosition = FormStartPosition.CenterParent;

            CreateControls();
        }

        private void CreateControls()
        {
            // Панель инструментов
            var toolStrip = new ToolStrip();
            var btnAddQuestion = new ToolStripButton("Добавить вопрос");
            var btnRefresh = new ToolStripButton("Обновить");

            /*btnAddQuestion.Click += BtnAddQuestion_Click;*/
            btnRefresh.Click += async (s, e) => await LoadQuestionsAsync();

            toolStrip.Items.AddRange(new ToolStripItem[] { btnAddQuestion, btnRefresh });

            // Список вопросов
            var listView = new ListView
            {
                Location = new Point(20, 40),
                Size = new Size(600, 400),
                View = View.Details,
                FullRowSelect = true,
                GridLines = true,
                Name = "listQuestions"
            };

            listView.Columns.Add("№", 40);
            listView.Columns.Add("Текст вопроса", 350);
            listView.Columns.Add("Тип", 100);
            listView.Columns.Add("Вариантов", 80);

            /*listView.MouseDoubleClick += ListView_MouseDoubleClick;*/

            // Кнопки управления
            var btnEdit = new Button { Text = "Редактировать", Location = new Point(630, 40), Size = new Size(120, 30) };
            var btnDelete = new Button { Text = "Удалить", Location = new Point(630, 80), Size = new Size(120, 30) };
            var btnMoveUp = new Button { Text = "Вверх", Location = new Point(630, 120), Size = new Size(120, 30) };
            var btnMoveDown = new Button { Text = "Вниз", Location = new Point(630, 160), Size = new Size(120, 30) };

            /*btnEdit.Click += BtnEdit_Click;*/
            btnDelete.Click += BtnDelete_Click;
            btnMoveUp.Click += BtnMoveUp_Click;
            btnMoveDown.Click += BtnMoveDown_Click;

            // Информация о тесте
            var lblTestInfo = new Label
            {
                Text = $"Тест: {_test.Title}\nВопросов: 0",
                Location = new Point(20, 450),
                Size = new Size(400, 40),
                Name = "lblTestInfo"
            };

            this.Controls.AddRange(new Control[]
            {
            toolStrip, listView,
            btnEdit, btnDelete, btnMoveUp, btnMoveDown,
            lblTestInfo
            });
        }

        private async Task LoadQuestionsAsync()
        {
            var listView = this.Controls.Find("listQuestions", true).FirstOrDefault() as ListView;
            var lblTestInfo = this.Controls.Find("lblTestInfo", true).FirstOrDefault() as Label;

            if (listView == null || lblTestInfo == null) return;

            try
            {
                listView.Items.Clear();
                _questions = (await _questionService.GetQuestionsByTestAsync(_test.Id)).ToList();

                foreach (var question in _questions.OrderBy(q => q.OrderIndex))
                {
                    var item = new ListViewItem(question.OrderIndex?.ToString() ?? "");
                    item.SubItems.Add(question.QuestionText.Length > 50
                        ? question.QuestionText.Substring(0, 47) + "..."
                        : question.QuestionText);
                    item.SubItems.Add(GetQuestionTypeName(question.QuestionType));
                    item.SubItems.Add(question.AnswerOptions?.Count.ToString() ?? "0");
                    item.Tag = question.Id;

                    listView.Items.Add(item);
                }

                lblTestInfo.Text = $"Тест: {_test.Title}\nВопросов: {_questions.Count}";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка загрузки вопросов: {ex.Message}", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private string GetQuestionTypeName(string questionType)
        {
            return questionType switch
            {
                "SingleChoice" => "Один ответ",
                "MultipleChoice" => "Несколько ответов",
                "TextAnswer" => "Текстовый ответ",
                _ => questionType
            };
        }

       /* private void BtnAddQuestion_Click(object? sender, EventArgs e)
        {
            var form = new CreateQuestionForm(_questionService, _test);
            form.QuestionCreated += async (s, e) => await LoadQuestionsAsync();
            form.ShowDialog();
        }

        private void ListView_MouseDoubleClick(object? sender, MouseEventArgs e)
        {
            BtnEdit_Click(sender, e);
        }*/

        /*private void BtnEdit_Click(object? sender, EventArgs e)
        {
            var listView = this.Controls.Find("listQuestions", true).FirstOrDefault() as ListView;
            if (listView?.SelectedItems.Count == 0) return;

            var questionId = (int)listView!.SelectedItems[0].Tag;
            var question = _questions.FirstOrDefault(q => q.Id == questionId);
            if (question == null) return;

            var form = new EditQuestionForm(_questionService, question);
            form.QuestionUpdated += async (s, e) => await LoadQuestionsAsync();
            form.ShowDialog();
        }*/

        private async void BtnDelete_Click(object? sender, EventArgs e)
        {
            var listView = this.Controls.Find("listQuestions", true).FirstOrDefault() as ListView;
            if (listView?.SelectedItems.Count == 0) return;

            var questionId = (int)listView!.SelectedItems[0].Tag;
            var question = _questions.FirstOrDefault(q => q.Id == questionId);
            if (question == null) return;

            var result = MessageBox.Show($"Вы уверены, что хотите удалить этот вопрос?",
                "Подтверждение удаления", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                var (success, message) = await _questionService.DeleteQuestionAsync(questionId);
                MessageBox.Show(message, success ? "Успех" : "Ошибка",
                    MessageBoxButtons.OK, success ? MessageBoxIcon.Information : MessageBoxIcon.Error);

                if (success)
                    await LoadQuestionsAsync();
            }
        }

        private async void BtnMoveUp_Click(object? sender, EventArgs e)
        {
            await ChangeQuestionOrder(-1);
        }

        private async void BtnMoveDown_Click(object? sender, EventArgs e)
        {
            await ChangeQuestionOrder(1);
        }

        private async Task ChangeQuestionOrder(int direction)
        {
            var listView = this.Controls.Find("listQuestions", true).FirstOrDefault() as ListView;
            if (listView?.SelectedItems.Count == 0) return;

            var questionId = (int)listView!.SelectedItems[0].Tag;
            var question = _questions.FirstOrDefault(q => q.Id == questionId);
            if (question?.OrderIndex == null) return;

            var newIndex = question.OrderIndex.Value + direction;
            if (newIndex < 1 || newIndex > _questions.Count) return;

            // Меняем порядок
            var otherQuestion = _questions.FirstOrDefault(q => q.OrderIndex == newIndex);
            if (otherQuestion != null)
            {
                otherQuestion.OrderIndex = question.OrderIndex.Value;
                question.OrderIndex = newIndex;

                // Сохраняем изменения
                await _questionService.UpdateQuestionAsync(question);
                await _questionService.UpdateQuestionAsync(otherQuestion);

                await LoadQuestionsAsync();

                // Выделяем перемещенный вопрос
                foreach (ListViewItem item in listView.Items)
                {
                    if ((int)item.Tag == question.Id)
                    {
                        item.Selected = true;
                        item.EnsureVisible();
                        break;
                    }
                }
            }
        }
    }
}
