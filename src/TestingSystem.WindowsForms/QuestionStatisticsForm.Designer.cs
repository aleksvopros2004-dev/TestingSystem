namespace TestingSystem.WindowsForms
{
    partial class QuestionStatisticsForm
    {
        private System.ComponentModel.IContainer components = null;

        private System.Windows.Forms.Panel panelTop;
        private System.Windows.Forms.Label lblQuestionTitle;
        private System.Windows.Forms.Label lblQuestionText;
        private System.Windows.Forms.Panel panelType;
        private System.Windows.Forms.Label lblTypeTitle;
        private System.Windows.Forms.Label lblType;
        private System.Windows.Forms.Panel panelStats;
        private System.Windows.Forms.Label lblStatsTitle;
        private System.Windows.Forms.Label lblStats;
        private System.Windows.Forms.SplitContainer splitContainer;
        private System.Windows.Forms.Panel panelLeft;
        private System.Windows.Forms.Label lblOptionsTitle;
        private System.Windows.Forms.ListView listViewOptions;
        private System.Windows.Forms.ColumnHeader colOptionText;
        private System.Windows.Forms.ColumnHeader colCount;
        private System.Windows.Forms.ColumnHeader colPercent;
        private System.Windows.Forms.ColumnHeader colCorrect;
        private System.Windows.Forms.Panel panelRight;
        private System.Windows.Forms.Label lblWordsTitle;
        private System.Windows.Forms.ListView listViewWords;
        private System.Windows.Forms.ColumnHeader colWord;
        private System.Windows.Forms.ColumnHeader colWordCount;
        private System.Windows.Forms.Label lblNoData;
        private System.Windows.Forms.Panel panelBottom;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Button btnViewAnswers;

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
            panelTop = new Panel();
            lblQuestionTitle = new Label();
            lblQuestionText = new Label();
            panelType = new Panel();
            lblTypeTitle = new Label();
            lblType = new Label();
            panelStats = new Panel();
            lblStatsTitle = new Label();
            lblStats = new Label();
            splitContainer = new SplitContainer();
            panelLeft = new Panel();
            lblOptionsTitle = new Label();
            listViewOptions = new ListView();
            colOptionText = new ColumnHeader();
            colCount = new ColumnHeader();
            colPercent = new ColumnHeader();
            colCorrect = new ColumnHeader();
            panelRight = new Panel();
            lblWordsTitle = new Label();
            listViewWords = new ListView();
            colWord = new ColumnHeader();
            colWordCount = new ColumnHeader();
            lblNoData = new Label();
            panelBottom = new Panel();
            btnClose = new Button();
            panelTop.SuspendLayout();
            panelType.SuspendLayout();
            panelStats.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)splitContainer).BeginInit();
            splitContainer.Panel1.SuspendLayout();
            splitContainer.Panel2.SuspendLayout();
            splitContainer.SuspendLayout();
            panelLeft.SuspendLayout();
            panelRight.SuspendLayout();
            panelBottom.SuspendLayout();
            SuspendLayout();
            // 
            // panelTop
            // 
            panelTop.BackColor = Color.FromArgb(0, 120, 215);
            panelTop.Controls.Add(lblQuestionTitle);
            panelTop.Controls.Add(lblQuestionText);
            panelTop.Dock = DockStyle.Top;
            panelTop.Location = new Point(0, 0);
            panelTop.Margin = new Padding(3, 2, 3, 2);
            panelTop.Name = "panelTop";
            panelTop.Padding = new Padding(13, 8, 13, 8);
            panelTop.Size = new Size(800, 70);
            panelTop.TabIndex = 0;
            // 
            // lblQuestionTitle
            // 
            lblQuestionTitle.AutoSize = true;
            lblQuestionTitle.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lblQuestionTitle.ForeColor = Color.White;
            lblQuestionTitle.Location = new Point(13, 9);
            lblQuestionTitle.Name = "lblQuestionTitle";
            lblQuestionTitle.Size = new Size(65, 19);
            lblQuestionTitle.TabIndex = 0;
            lblQuestionTitle.Text = "Вопрос:";
            // 
            // lblQuestionText
            // 
            lblQuestionText.AutoSize = true;
            lblQuestionText.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lblQuestionText.ForeColor = Color.White;
            lblQuestionText.Location = new Point(13, 32);
            lblQuestionText.Name = "lblQuestionText";
            lblQuestionText.Size = new Size(108, 19);
            lblQuestionText.TabIndex = 1;
            lblQuestionText.Text = "Текст вопроса";
            // 
            // panelType
            // 
            panelType.BackColor = Color.FromArgb(240, 240, 240);
            panelType.Controls.Add(lblTypeTitle);
            panelType.Controls.Add(lblType);
            panelType.Dock = DockStyle.Top;
            panelType.Location = new Point(0, 70);
            panelType.Margin = new Padding(3, 2, 3, 2);
            panelType.Name = "panelType";
            panelType.Padding = new Padding(13, 6, 13, 6);
            panelType.Size = new Size(800, 40);
            panelType.TabIndex = 1;
            // 
            // lblTypeTitle
            // 
            lblTypeTitle.AutoSize = true;
            lblTypeTitle.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblTypeTitle.Location = new Point(13, 10);
            lblTypeTitle.Name = "lblTypeTitle";
            lblTypeTitle.Size = new Size(31, 15);
            lblTypeTitle.TabIndex = 0;
            lblTypeTitle.Text = "Тип:";
            // 
            // lblType
            // 
            lblType.AutoSize = true;
            lblType.Font = new Font("Segoe UI", 9F);
            lblType.Location = new Point(54, 10);
            lblType.Name = "lblType";
            lblType.Size = new Size(76, 15);
            lblType.TabIndex = 1;
            lblType.Text = "Тип вопроса";
            // 
            // panelStats
            // 
            panelStats.BackColor = Color.FromArgb(255, 248, 225);
            panelStats.Controls.Add(lblStatsTitle);
            panelStats.Controls.Add(lblStats);
            panelStats.Dock = DockStyle.Top;
            panelStats.Location = new Point(0, 110);
            panelStats.Margin = new Padding(3, 2, 3, 2);
            panelStats.Name = "panelStats";
            panelStats.Padding = new Padding(13, 6, 13, 6);
            panelStats.Size = new Size(800, 50);
            panelStats.TabIndex = 2;
            // 
            // lblStatsTitle
            // 
            lblStatsTitle.AutoSize = true;
            lblStatsTitle.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblStatsTitle.Location = new Point(13, 12);
            lblStatsTitle.Name = "lblStatsTitle";
            lblStatsTitle.Size = new Size(73, 15);
            lblStatsTitle.TabIndex = 0;
            lblStatsTitle.Text = "Статистика:";
            // 
            // lblStats
            // 
            lblStats.AutoSize = true;
            lblStats.Font = new Font("Segoe UI", 9F);
            lblStats.Location = new Point(91, 12);
            lblStats.Name = "lblStats";
            lblStats.Size = new Size(211, 15);
            lblStats.TabIndex = 1;
            lblStats.Text = "Всего ответов: 0 | Правильных: 0 | 0%";
            // 
            // splitContainer
            // 
            splitContainer.Dock = DockStyle.Fill;
            splitContainer.Location = new Point(0, 160);
            splitContainer.Margin = new Padding(3, 2, 3, 2);
            splitContainer.Name = "splitContainer";
            splitContainer.Orientation = Orientation.Horizontal;
            // 
            // splitContainer.Panel1
            // 
            splitContainer.Panel1.Controls.Add(panelLeft);
            splitContainer.Panel1MinSize = 200;
            // 
            // splitContainer.Panel2
            // 
            splitContainer.Panel2.Controls.Add(panelRight);
            splitContainer.Panel2MinSize = 150;
            splitContainer.Size = new Size(800, 430);
            splitContainer.SplitterDistance = 250;
            splitContainer.SplitterWidth = 3;
            splitContainer.TabIndex = 3;
            // 
            // panelLeft
            // 
            panelLeft.Controls.Add(lblOptionsTitle);
            panelLeft.Controls.Add(listViewOptions);
            panelLeft.Dock = DockStyle.Fill;
            panelLeft.Location = new Point(0, 0);
            panelLeft.Margin = new Padding(3, 2, 3, 2);
            panelLeft.Name = "panelLeft";
            panelLeft.Padding = new Padding(9, 8, 9, 8);
            panelLeft.Size = new Size(800, 250);
            panelLeft.TabIndex = 0;
            // 
            // lblOptionsTitle
            // 
            lblOptionsTitle.AutoSize = true;
            lblOptionsTitle.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lblOptionsTitle.Location = new Point(9, 8);
            lblOptionsTitle.Name = "lblOptionsTitle";
            lblOptionsTitle.Size = new Size(142, 19);
            lblOptionsTitle.TabIndex = 0;
            lblOptionsTitle.Text = "Анализ вариантов:";
            // 
            // listViewOptions
            // 
            listViewOptions.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            listViewOptions.Columns.AddRange(new ColumnHeader[] { colOptionText, colCount, colPercent, colCorrect });
            listViewOptions.FullRowSelect = true;
            listViewOptions.GridLines = true;
            listViewOptions.Location = new Point(9, 35);
            listViewOptions.Margin = new Padding(3, 2, 3, 2);
            listViewOptions.Name = "listViewOptions";
            listViewOptions.Size = new Size(782, 205);
            listViewOptions.TabIndex = 1;
            listViewOptions.UseCompatibleStateImageBehavior = false;
            listViewOptions.View = View.Details;
            // 
            // colOptionText
            // 
            colOptionText.Text = "Вариант ответа";
            colOptionText.Width = 400;
            // 
            // colCount
            // 
            colCount.Text = "Выбрано";
            colCount.Width = 100;
            // 
            // colPercent
            // 
            colPercent.Text = "%";
            colPercent.Width = 80;
            // 
            // colCorrect
            // 
            colCorrect.Text = "Правильный";
            colCorrect.Width = 100;
            // 
            // panelRight
            // 
            panelRight.Controls.Add(lblWordsTitle);
            panelRight.Controls.Add(listViewWords);
            panelRight.Controls.Add(lblNoData);
            panelRight.Controls.Add(btnViewAnswers);
            panelRight.Dock = DockStyle.Fill;
            panelRight.Location = new Point(0, 0);
            panelRight.Margin = new Padding(3, 2, 3, 2);
            panelRight.Name = "panelRight";
            panelRight.Padding = new Padding(9, 8, 9, 8);
            panelRight.Size = new Size(800, 177);
            panelRight.TabIndex = 0;
            //
            // btnViewAnswers
            //
            btnViewAnswers = new Button();
            btnViewAnswers.BackColor = Color.FromArgb(0, 120, 215);
            btnViewAnswers.FlatAppearance.BorderSize = 0;
            btnViewAnswers.FlatStyle = FlatStyle.Flat;
            btnViewAnswers.ForeColor = Color.White;
            btnViewAnswers.Location = new Point(620, 8);
            btnViewAnswers.Size = new Size(150, 30);
            btnViewAnswers.Text = "📋 Все ответы";
            btnViewAnswers.UseVisualStyleBackColor = false;
            btnViewAnswers.Visible = false;
            btnViewAnswers.Click += BtnViewAnswers_Click;
            panelRight.Controls.Add(btnViewAnswers);
            // 
            // lblWordsTitle
            // 
            lblWordsTitle.AutoSize = true;
            lblWordsTitle.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lblWordsTitle.Location = new Point(9, 8);
            lblWordsTitle.Name = "lblWordsTitle";
            lblWordsTitle.Size = new Size(175, 19);
            lblWordsTitle.TabIndex = 0;
            lblWordsTitle.Text = "Анализ ключевых слов:";
            // 
            // listViewWords
            // 
            listViewWords.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            listViewWords.Columns.AddRange(new ColumnHeader[] { colWord, colWordCount });
            listViewWords.FullRowSelect = true;
            listViewWords.GridLines = true;
            listViewWords.Location = new Point(9, 35);
            listViewWords.Margin = new Padding(3, 2, 3, 2);
            listViewWords.Name = "listViewWords";
            listViewWords.Size = new Size(782, 132);
            listViewWords.TabIndex = 1;
            listViewWords.UseCompatibleStateImageBehavior = false;
            listViewWords.View = View.Details;
            listViewWords.Visible = false;
            // 
            // colWord
            // 
            colWord.Text = "Слово";
            colWord.Width = 500;
            // 
            // colWordCount
            // 
            colWordCount.Text = "Частота";
            colWordCount.Width = 100;
            // 
            // lblNoData
            // 
            lblNoData.Anchor = AnchorStyles.None;
            lblNoData.AutoSize = true;
            lblNoData.ForeColor = Color.Gray;
            lblNoData.Location = new Point(311, 80);
            lblNoData.Name = "lblNoData";
            lblNoData.Size = new Size(140, 15);
            lblNoData.TabIndex = 2;
            lblNoData.Text = "Нет данных для анализа";
            lblNoData.TextAlign = ContentAlignment.MiddleCenter;
            lblNoData.Visible = false;
            // 
            // panelBottom
            // 
            panelBottom.BackColor = Color.FromArgb(240, 240, 240);
            panelBottom.Controls.Add(btnClose);
            panelBottom.Dock = DockStyle.Bottom;
            panelBottom.Location = new Point(0, 590);
            panelBottom.Margin = new Padding(3, 2, 3, 2);
            panelBottom.Name = "panelBottom";
            panelBottom.Padding = new Padding(9, 8, 9, 8);
            panelBottom.Size = new Size(800, 50);
            panelBottom.TabIndex = 4;
            // 
            // btnClose
            // 
            btnClose.Anchor = AnchorStyles.None;
            btnClose.BackColor = Color.FromArgb(0, 120, 215);
            btnClose.FlatAppearance.BorderSize = 0;
            btnClose.FlatStyle = FlatStyle.Flat;
            btnClose.ForeColor = Color.White;
            btnClose.Location = new Point(356, 12);
            btnClose.Margin = new Padding(3, 2, 3, 2);
            btnClose.Name = "btnClose";
            btnClose.Size = new Size(88, 26);
            btnClose.TabIndex = 0;
            btnClose.Text = "Закрыть";
            btnClose.UseVisualStyleBackColor = false;
            // 
            // QuestionStatisticsForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.White;
            ClientSize = new Size(800, 640);
            Controls.Add(splitContainer);
            Controls.Add(panelStats);
            Controls.Add(panelType);
            Controls.Add(panelTop);
            Controls.Add(panelBottom);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            Margin = new Padding(3, 2, 3, 2);
            MaximizeBox = false;
            MinimumSize = new Size(800, 640);
            Name = "QuestionStatisticsForm";
            StartPosition = FormStartPosition.CenterParent;
            Text = "Детальная статистика вопроса";
            panelTop.ResumeLayout(false);
            panelTop.PerformLayout();
            panelType.ResumeLayout(false);
            panelType.PerformLayout();
            panelStats.ResumeLayout(false);
            panelStats.PerformLayout();
            splitContainer.Panel1.ResumeLayout(false);
            splitContainer.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)splitContainer).EndInit();
            splitContainer.ResumeLayout(false);
            panelLeft.ResumeLayout(false);
            panelLeft.PerformLayout();
            panelRight.ResumeLayout(false);
            panelRight.PerformLayout();
            panelBottom.ResumeLayout(false);
            ResumeLayout(false);
        }
    }
}