namespace TestingSystem.WindowsForms;

partial class QuestionStatisticsForm
{
    private System.ComponentModel.IContainer components = null;

    protected override void Dispose(bool disposing)
    {
        if (disposing && (components != null))
        {
            components.Dispose();
        }
        base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
        tableLayout = new TableLayoutPanel();
        lblQuestionTitle = new Label();
        lblQuestionText = new Label();
        lblTypeTitle = new Label();
        lblType = new Label();
        lblStatsTitle = new Label();
        lblStats = new Label();
        lblOptionsTitle = new Label();
        listViewOptions = new ListView();
        colOptionText = new ColumnHeader();
        colCount = new ColumnHeader();
        colPercent = new ColumnHeader();
        colCorrect = new ColumnHeader();
        listViewWords = new ListView();
        colWord = new ColumnHeader();
        colWordCount = new ColumnHeader();
        lblNoData = new Label();
        btnClose = new Button();

        tableLayout.SuspendLayout();
        SuspendLayout();

        // tableLayout
        tableLayout.ColumnCount = 2;
        tableLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 120F));
        tableLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
        tableLayout.Controls.Add(lblQuestionTitle, 0, 0);
        tableLayout.Controls.Add(lblQuestionText, 1, 0);
        tableLayout.Controls.Add(lblTypeTitle, 0, 1);
        tableLayout.Controls.Add(lblType, 1, 1);
        tableLayout.Controls.Add(lblStatsTitle, 0, 2);
        tableLayout.Controls.Add(lblStats, 1, 2);
        tableLayout.Controls.Add(lblOptionsTitle, 0, 3);
        tableLayout.Controls.Add(listViewOptions, 1, 3);
        tableLayout.Controls.Add(listViewWords, 1, 3);
        tableLayout.Controls.Add(lblNoData, 1, 3);
        tableLayout.Controls.Add(btnClose, 1, 4);
        tableLayout.Dock = DockStyle.Fill;
        tableLayout.Location = new Point(0, 0);
        tableLayout.Name = "tableLayout";
        tableLayout.Padding = new Padding(20);
        tableLayout.RowCount = 5;
        tableLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 60F));
        tableLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 40F));
        tableLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 60F));
        tableLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
        tableLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 50F));
        tableLayout.Size = new Size(800, 500);
        tableLayout.TabIndex = 0;

        // lblQuestionTitle
        lblQuestionTitle.Dock = DockStyle.Fill;
        lblQuestionTitle.Location = new Point(23, 20);
        lblQuestionTitle.Name = "lblQuestionTitle";
        lblQuestionTitle.Size = new Size(114, 60);
        lblQuestionTitle.TabIndex = 0;
        lblQuestionTitle.Text = "Вопрос:";
        lblQuestionTitle.TextAlign = ContentAlignment.MiddleLeft;

        // lblQuestionText
        lblQuestionText.Dock = DockStyle.Fill;
        lblQuestionText.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
        lblQuestionText.Location = new Point(143, 20);
        lblQuestionText.Name = "lblQuestionText";
        lblQuestionText.Size = new Size(634, 60);
        lblQuestionText.TabIndex = 1;
        lblQuestionText.TextAlign = ContentAlignment.MiddleLeft;

        // lblTypeTitle
        lblTypeTitle.Dock = DockStyle.Fill;
        lblTypeTitle.Location = new Point(23, 80);
        lblTypeTitle.Name = "lblTypeTitle";
        lblTypeTitle.Size = new Size(114, 40);
        lblTypeTitle.TabIndex = 2;
        lblTypeTitle.Text = "Тип:";
        lblTypeTitle.TextAlign = ContentAlignment.MiddleLeft;

        // lblType
        lblType.Dock = DockStyle.Fill;
        lblType.Location = new Point(143, 80);
        lblType.Name = "lblType";
        lblType.Size = new Size(634, 40);
        lblType.TabIndex = 3;
        lblType.TextAlign = ContentAlignment.MiddleLeft;

        // lblStatsTitle
        lblStatsTitle.Dock = DockStyle.Fill;
        lblStatsTitle.Location = new Point(23, 120);
        lblStatsTitle.Name = "lblStatsTitle";
        lblStatsTitle.Size = new Size(114, 60);
        lblStatsTitle.TabIndex = 4;
        lblStatsTitle.Text = "Статистика:";
        lblStatsTitle.TextAlign = ContentAlignment.MiddleLeft;

        // lblStats
        lblStats.Dock = DockStyle.Fill;
        lblStats.Location = new Point(143, 120);
        lblStats.Name = "lblStats";
        lblStats.Size = new Size(634, 60);
        lblStats.TabIndex = 5;
        lblStats.TextAlign = ContentAlignment.MiddleLeft;

        // lblOptionsTitle
        lblOptionsTitle.Dock = DockStyle.Fill;
        lblOptionsTitle.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
        lblOptionsTitle.Location = new Point(23, 180);
        lblOptionsTitle.Name = "lblOptionsTitle";
        lblOptionsTitle.Size = new Size(114, 220);
        lblOptionsTitle.TabIndex = 6;
        lblOptionsTitle.Text = "Детальный анализ:";
        lblOptionsTitle.TextAlign = ContentAlignment.MiddleLeft;

        // listViewOptions
        listViewOptions.Columns.AddRange(new ColumnHeader[] { colOptionText, colCount, colPercent, colCorrect });
        listViewOptions.Dock = DockStyle.Fill;
        listViewOptions.FullRowSelect = true;
        listViewOptions.GridLines = true;
        listViewOptions.Location = new Point(143, 183);
        listViewOptions.Name = "listViewOptions";
        listViewOptions.Size = new Size(634, 214);
        listViewOptions.TabIndex = 7;
        listViewOptions.UseCompatibleStateImageBehavior = false;
        listViewOptions.View = View.Details;
        listViewOptions.Visible = false;

        colOptionText.Text = "Вариант ответа";
        colOptionText.Width = 350;
        colCount.Text = "Выбрано";
        colCount.Width = 80;
        colPercent.Text = "%";
        colPercent.Width = 80;
        colCorrect.Text = "Правильный";
        colCorrect.Width = 80;

        // listViewWords
        listViewWords.Columns.AddRange(new ColumnHeader[] { colWord, colWordCount });
        listViewWords.Dock = DockStyle.Fill;
        listViewWords.FullRowSelect = true;
        listViewWords.GridLines = true;
        listViewWords.Location = new Point(143, 183);
        listViewWords.Name = "listViewWords";
        listViewWords.Size = new Size(634, 214);
        listViewWords.TabIndex = 8;
        listViewWords.UseCompatibleStateImageBehavior = false;
        listViewWords.View = View.Details;
        listViewWords.Visible = false;

        colWord.Text = "Слово";
        colWord.Width = 400;
        colWordCount.Text = "Частота";
        colWordCount.Width = 100;

        // lblNoData
        lblNoData.Dock = DockStyle.Fill;
        lblNoData.ForeColor = Color.Gray;
        lblNoData.Location = new Point(143, 180);
        lblNoData.Name = "lblNoData";
        lblNoData.Size = new Size(634, 220);
        lblNoData.TabIndex = 9;
        lblNoData.Text = "Нет данных для анализа";
        lblNoData.TextAlign = ContentAlignment.MiddleCenter;
        lblNoData.Visible = false;

        // btnClose
        btnClose.Location = new Point(143, 430);
        btnClose.Name = "btnClose";
        btnClose.Size = new Size(100, 35);
        btnClose.TabIndex = 10;
        btnClose.Text = "Закрыть";
        btnClose.Click += (s, e) => Close();

        // QuestionStatisticsForm
        AutoScaleDimensions = new SizeF(8F, 20F);
        AutoScaleMode = AutoScaleMode.Font;
        BackColor = Color.White;
        ClientSize = new Size(800, 500);
        Controls.Add(tableLayout);
        FormBorderStyle = FormBorderStyle.FixedDialog;
        MaximizeBox = false;
        Name = "QuestionStatisticsForm";
        StartPosition = FormStartPosition.CenterParent;
        Text = "Детальная статистика вопроса";
        tableLayout.ResumeLayout(false);
        ResumeLayout(false);
    }

    private TableLayoutPanel tableLayout;
    private Label lblQuestionTitle;
    private Label lblQuestionText;
    private Label lblTypeTitle;
    private Label lblType;
    private Label lblStatsTitle;
    private Label lblStats;
    private Label lblOptionsTitle;
    private ListView listViewOptions;
    private ColumnHeader colOptionText;
    private ColumnHeader colCount;
    private ColumnHeader colPercent;
    private ColumnHeader colCorrect;
    private ListView listViewWords;
    private ColumnHeader colWord;
    private ColumnHeader colWordCount;
    private Label lblNoData;
    private Button btnClose;
}