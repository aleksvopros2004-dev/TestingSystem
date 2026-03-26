namespace TestingSystem.WindowsForms
{
    partial class QuestionStatisticsForm
    {
        private System.ComponentModel.IContainer components = null;

        private System.Windows.Forms.TableLayoutPanel tableLayout;
        private System.Windows.Forms.Label lblQuestionTitle;
        private System.Windows.Forms.Label lblQuestionText;
        private System.Windows.Forms.Label lblTypeTitle;
        private System.Windows.Forms.Label lblType;
        private System.Windows.Forms.Label lblStatsTitle;
        private System.Windows.Forms.Label lblStats;
        private System.Windows.Forms.Label lblOptionsTitle;
        private System.Windows.Forms.ListView listViewOptions;
        private System.Windows.Forms.ColumnHeader colOptionText;
        private System.Windows.Forms.ColumnHeader colCount;
        private System.Windows.Forms.ColumnHeader colPercent;
        private System.Windows.Forms.ColumnHeader colCorrect;
        private System.Windows.Forms.ListView listViewWords;
        private System.Windows.Forms.ColumnHeader colWord;
        private System.Windows.Forms.ColumnHeader colWordCount;
        private System.Windows.Forms.Label lblNoData;
        private System.Windows.Forms.Button btnClose;

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
            // 
            // tableLayout
            // 
            tableLayout.ColumnCount = 2;
            tableLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 105F));
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
            tableLayout.Margin = new Padding(3, 2, 3, 2);
            tableLayout.Name = "tableLayout";
            tableLayout.Padding = new Padding(18, 15, 18, 15);
            tableLayout.RowCount = 5;
            tableLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 38F));
            tableLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 30F));
            tableLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 45F));
            tableLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tableLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 38F));
            tableLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            tableLayout.Size = new Size(700, 375);
            tableLayout.TabIndex = 0;
            // 
            // lblQuestionTitle
            // 
            lblQuestionTitle.Dock = DockStyle.Fill;
            lblQuestionTitle.Location = new Point(21, 15);
            lblQuestionTitle.Name = "lblQuestionTitle";
            lblQuestionTitle.Size = new Size(99, 38);
            lblQuestionTitle.TabIndex = 0;
            lblQuestionTitle.Text = "Вопрос:";
            lblQuestionTitle.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // lblQuestionText
            // 
            lblQuestionText.Dock = DockStyle.Fill;
            lblQuestionText.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lblQuestionText.Location = new Point(126, 15);
            lblQuestionText.Name = "lblQuestionText";
            lblQuestionText.Size = new Size(553, 38);
            lblQuestionText.TabIndex = 1;
            lblQuestionText.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // lblTypeTitle
            // 
            lblTypeTitle.Dock = DockStyle.Fill;
            lblTypeTitle.Location = new Point(21, 53);
            lblTypeTitle.Name = "lblTypeTitle";
            lblTypeTitle.Size = new Size(99, 30);
            lblTypeTitle.TabIndex = 2;
            lblTypeTitle.Text = "Тип:";
            lblTypeTitle.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // lblType
            // 
            lblType.Dock = DockStyle.Fill;
            lblType.Location = new Point(126, 53);
            lblType.Name = "lblType";
            lblType.Size = new Size(553, 30);
            lblType.TabIndex = 3;
            lblType.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // lblStatsTitle
            // 
            lblStatsTitle.Dock = DockStyle.Fill;
            lblStatsTitle.Location = new Point(21, 83);
            lblStatsTitle.Name = "lblStatsTitle";
            lblStatsTitle.Size = new Size(99, 45);
            lblStatsTitle.TabIndex = 4;
            lblStatsTitle.Text = "Статистика:";
            lblStatsTitle.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // lblStats
            // 
            lblStats.Dock = DockStyle.Fill;
            lblStats.Location = new Point(126, 83);
            lblStats.Name = "lblStats";
            lblStats.Size = new Size(553, 45);
            lblStats.TabIndex = 5;
            lblStats.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // lblOptionsTitle
            // 
            lblOptionsTitle.Dock = DockStyle.Fill;
            lblOptionsTitle.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lblOptionsTitle.Location = new Point(21, 128);
            lblOptionsTitle.Name = "lblOptionsTitle";
            lblOptionsTitle.Size = new Size(99, 174);
            lblOptionsTitle.TabIndex = 6;
            lblOptionsTitle.Text = "Детальный анализ:";
            lblOptionsTitle.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // listViewOptions
            // 
            listViewOptions.Columns.AddRange(new ColumnHeader[] { colOptionText, colCount, colPercent, colCorrect });
            listViewOptions.Dock = DockStyle.Fill;
            listViewOptions.FullRowSelect = true;
            listViewOptions.GridLines = true;
            listViewOptions.Location = new Point(126, 304);
            listViewOptions.Margin = new Padding(3, 2, 3, 2);
            listViewOptions.Name = "listViewOptions";
            listViewOptions.Size = new Size(553, 34);
            listViewOptions.TabIndex = 7;
            listViewOptions.UseCompatibleStateImageBehavior = false;
            listViewOptions.View = View.Details;
            listViewOptions.Visible = false;
            // 
            // colOptionText
            // 
            colOptionText.Text = "Вариант ответа";
            colOptionText.Width = 350;
            // 
            // colCount
            // 
            colCount.Text = "Выбрано";
            colCount.Width = 80;
            // 
            // colPercent
            // 
            colPercent.Text = "%";
            colPercent.Width = 80;
            // 
            // colCorrect
            // 
            colCorrect.Text = "Правильный";
            colCorrect.Width = 80;
            // 
            // listViewWords
            // 
            listViewWords.Columns.AddRange(new ColumnHeader[] { colWord, colWordCount });
            listViewWords.Dock = DockStyle.Fill;
            listViewWords.FullRowSelect = true;
            listViewWords.GridLines = true;
            listViewWords.Location = new Point(21, 304);
            listViewWords.Margin = new Padding(3, 2, 3, 2);
            listViewWords.Name = "listViewWords";
            listViewWords.Size = new Size(99, 34);
            listViewWords.TabIndex = 8;
            listViewWords.UseCompatibleStateImageBehavior = false;
            listViewWords.View = View.Details;
            listViewWords.Visible = false;
            // 
            // colWord
            // 
            colWord.Text = "Слово";
            colWord.Width = 400;
            // 
            // colWordCount
            // 
            colWordCount.Text = "Частота";
            colWordCount.Width = 100;
            // 
            // lblNoData
            // 
            lblNoData.Dock = DockStyle.Fill;
            lblNoData.ForeColor = Color.Gray;
            lblNoData.Location = new Point(126, 128);
            lblNoData.Name = "lblNoData";
            lblNoData.Size = new Size(553, 174);
            lblNoData.TabIndex = 9;
            lblNoData.Text = "Нет данных для анализа";
            lblNoData.TextAlign = ContentAlignment.MiddleCenter;
            lblNoData.Visible = false;
            // 
            // btnClose
            // 
            btnClose.Location = new Point(21, 342);
            btnClose.Margin = new Padding(3, 2, 3, 2);
            btnClose.Name = "btnClose";
            btnClose.Size = new Size(88, 16);
            btnClose.TabIndex = 10;
            btnClose.Text = "Закрыть";
            btnClose.UseVisualStyleBackColor = true;
            // 
            // QuestionStatisticsForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.White;
            ClientSize = new Size(700, 375);
            Controls.Add(tableLayout);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            Margin = new Padding(3, 2, 3, 2);
            MaximizeBox = false;
            Name = "QuestionStatisticsForm";
            StartPosition = FormStartPosition.CenterParent;
            Text = "Детальная статистика вопроса";
            tableLayout.ResumeLayout(false);
            ResumeLayout(false);
        }
    }
}