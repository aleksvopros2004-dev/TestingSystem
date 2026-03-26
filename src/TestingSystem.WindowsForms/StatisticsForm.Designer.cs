namespace TestingSystem.WindowsForms
{
    partial class StatisticsForm
    {
        private System.ComponentModel.IContainer components = null;

        // Объявляем все контролы
        private System.Windows.Forms.TableLayoutPanel tableLayout;
        private System.Windows.Forms.Panel panelTop;
        private System.Windows.Forms.Label labelTestSelect;
        private System.Windows.Forms.ComboBox comboBoxTests;
        private System.Windows.Forms.Button buttonRefresh;
        private System.Windows.Forms.Button buttonExportExcel;
        private System.Windows.Forms.Panel panelGeneral;
        private System.Windows.Forms.Label labelGeneralTitle;
        private System.Windows.Forms.Label labelAttempts;
        private System.Windows.Forms.Label labelTotalAttempts;
        private System.Windows.Forms.Label labelScore;
        private System.Windows.Forms.Label labelAverageScore;
        private System.Windows.Forms.Label labelMedian;
        private System.Windows.Forms.Label labelMedianScore;
        private System.Windows.Forms.Label labelMax;
        private System.Windows.Forms.Label labelMaxScore;
        private System.Windows.Forms.Label labelMin;
        private System.Windows.Forms.Label labelMinScore;
        private System.Windows.Forms.Label labelTime;
        private System.Windows.Forms.Label labelAverageTime;
        private System.Windows.Forms.ProgressBar progressAverage;
        private System.Windows.Forms.Panel panelCharts;
        private System.Windows.Forms.DataVisualization.Charting.Chart chartScoreDistribution;
        private System.Windows.Forms.Panel panelQuestions;
        private System.Windows.Forms.Label labelQuestionsTitle;
        private System.Windows.Forms.ListView listViewQuestions;
        private System.Windows.Forms.ColumnHeader columnHeaderId;
        private System.Windows.Forms.ColumnHeader columnHeaderText;
        private System.Windows.Forms.ColumnHeader columnHeaderPercent;
        private System.Windows.Forms.ColumnHeader columnHeaderCount;
        private System.Windows.Forms.Panel panelAttempts;
        private System.Windows.Forms.Label labelAttemptsTitle;
        private System.Windows.Forms.ListView listViewAttempts;
        private System.Windows.Forms.ColumnHeader columnHeaderUserName;
        private System.Windows.Forms.ColumnHeader columnHeaderDate;
        private System.Windows.Forms.ColumnHeader columnHeaderPoints;
        private System.Windows.Forms.ColumnHeader columnHeaderPercentResult;
        private System.Windows.Forms.ColumnHeader columnHeaderTime;
        private System.Windows.Forms.Label labelMessage;

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
            panelTop = new Panel();
            labelTestSelect = new Label();
            comboBoxTests = new ComboBox();
            buttonRefresh = new Button();
            buttonExportExcel = new Button();
            panelGeneral = new Panel();
            labelGeneralTitle = new Label();
            labelAttempts = new Label();
            labelTotalAttempts = new Label();
            labelScore = new Label();
            labelAverageScore = new Label();
            labelMedian = new Label();
            labelMedianScore = new Label();
            labelMax = new Label();
            labelMaxScore = new Label();
            labelMin = new Label();
            labelMinScore = new Label();
            labelTime = new Label();
            labelAverageTime = new Label();
            progressAverage = new ProgressBar();
            panelCharts = new Panel();
            panelQuestions = new Panel();
            labelQuestionsTitle = new Label();
            listViewQuestions = new ListView();
            columnHeaderId = new ColumnHeader();
            columnHeaderText = new ColumnHeader();
            columnHeaderPercent = new ColumnHeader();
            columnHeaderCount = new ColumnHeader();
            panelAttempts = new Panel();
            labelAttemptsTitle = new Label();
            listViewAttempts = new ListView();
            columnHeaderUserName = new ColumnHeader();
            columnHeaderDate = new ColumnHeader();
            columnHeaderPoints = new ColumnHeader();
            columnHeaderPercentResult = new ColumnHeader();
            columnHeaderTime = new ColumnHeader();
            labelMessage = new Label();
            tableLayout.SuspendLayout();
            panelTop.SuspendLayout();
            panelGeneral.SuspendLayout();
            panelQuestions.SuspendLayout();
            panelAttempts.SuspendLayout();
            SuspendLayout();
            // 
            // tableLayout
            // 
            tableLayout.ColumnCount = 2;
            tableLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tableLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tableLayout.Controls.Add(panelTop, 0, 0);
            tableLayout.Controls.Add(panelGeneral, 0, 1);
            tableLayout.Controls.Add(panelCharts, 1, 1);
            tableLayout.Controls.Add(panelQuestions, 0, 2);
            tableLayout.Controls.Add(panelAttempts, 1, 2);
            tableLayout.Controls.Add(labelMessage, 0, 3);
            tableLayout.Dock = DockStyle.Fill;
            tableLayout.Location = new Point(0, 0);
            tableLayout.Margin = new Padding(3, 2, 3, 2);
            tableLayout.Name = "tableLayout";
            tableLayout.Padding = new Padding(9, 8, 9, 8);
            tableLayout.RowCount = 4;
            tableLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 38F));
            tableLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 35F));
            tableLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 55F));
            tableLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 30F));
            tableLayout.Size = new Size(1050, 525);
            tableLayout.TabIndex = 0;
            // 
            // panelTop
            // 
            panelTop.Controls.Add(labelTestSelect);
            panelTop.Controls.Add(comboBoxTests);
            panelTop.Controls.Add(buttonRefresh);
            panelTop.Controls.Add(buttonExportExcel);
            panelTop.Dock = DockStyle.Fill;
            panelTop.Location = new Point(12, 10);
            panelTop.Margin = new Padding(3, 2, 3, 2);
            panelTop.Name = "panelTop";
            panelTop.Size = new Size(510, 34);
            panelTop.TabIndex = 0;
            // 
            // labelTestSelect
            // 
            labelTestSelect.AutoSize = true;
            labelTestSelect.Location = new Point(9, 9);
            labelTestSelect.Name = "labelTestSelect";
            labelTestSelect.Size = new Size(33, 15);
            labelTestSelect.TabIndex = 0;
            labelTestSelect.Text = "Тест:";
            // 
            // comboBoxTests
            // 
            comboBoxTests.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBoxTests.Location = new Point(52, 7);
            comboBoxTests.Margin = new Padding(3, 2, 3, 2);
            comboBoxTests.Name = "comboBoxTests";
            comboBoxTests.Size = new Size(263, 23);
            comboBoxTests.TabIndex = 1;
            // 
            // buttonRefresh
            // 
            buttonRefresh.Location = new Point(332, 6);
            buttonRefresh.Margin = new Padding(3, 2, 3, 2);
            buttonRefresh.Name = "buttonRefresh";
            buttonRefresh.Size = new Size(88, 22);
            buttonRefresh.TabIndex = 2;
            buttonRefresh.Text = "Обновить";
            buttonRefresh.UseVisualStyleBackColor = true;
            // 
            // buttonExportExcel
            // 
            buttonExportExcel.BackColor = Color.FromArgb(0, 120, 215);
            buttonExportExcel.FlatAppearance.BorderSize = 0;
            buttonExportExcel.FlatStyle = FlatStyle.Flat;
            buttonExportExcel.ForeColor = Color.White;
            buttonExportExcel.Location = new Point(423, 6);
            buttonExportExcel.Margin = new Padding(3, 2, 3, 2);
            buttonExportExcel.Name = "buttonExportExcel";
            buttonExportExcel.Size = new Size(84, 22);
            buttonExportExcel.TabIndex = 3;
            buttonExportExcel.Text = "Экспорт Excel";
            buttonExportExcel.UseVisualStyleBackColor = false;
            // 
            // panelGeneral
            // 
            panelGeneral.BackColor = Color.White;
            panelGeneral.BorderStyle = BorderStyle.FixedSingle;
            panelGeneral.Controls.Add(labelGeneralTitle);
            panelGeneral.Controls.Add(labelAttempts);
            panelGeneral.Controls.Add(labelTotalAttempts);
            panelGeneral.Controls.Add(labelScore);
            panelGeneral.Controls.Add(labelAverageScore);
            panelGeneral.Controls.Add(labelMedian);
            panelGeneral.Controls.Add(labelMedianScore);
            panelGeneral.Controls.Add(labelMax);
            panelGeneral.Controls.Add(labelMaxScore);
            panelGeneral.Controls.Add(labelMin);
            panelGeneral.Controls.Add(labelMinScore);
            panelGeneral.Controls.Add(labelTime);
            panelGeneral.Controls.Add(labelAverageTime);
            panelGeneral.Controls.Add(progressAverage);
            panelGeneral.Dock = DockStyle.Fill;
            panelGeneral.Location = new Point(12, 48);
            panelGeneral.Margin = new Padding(3, 2, 3, 2);
            panelGeneral.Name = "panelGeneral";
            panelGeneral.Size = new Size(510, 167);
            panelGeneral.TabIndex = 1;
            // 
            // labelGeneralTitle
            // 
            labelGeneralTitle.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            labelGeneralTitle.Location = new Point(9, 8);
            labelGeneralTitle.Name = "labelGeneralTitle";
            labelGeneralTitle.Size = new Size(175, 19);
            labelGeneralTitle.TabIndex = 0;
            labelGeneralTitle.Text = "Общая статистика";
            // 
            // labelAttempts
            // 
            labelAttempts.Location = new Point(9, 34);
            labelAttempts.Name = "labelAttempts";
            labelAttempts.Size = new Size(131, 19);
            labelAttempts.TabIndex = 1;
            labelAttempts.Text = "Попыток (всего/завершено):";
            // 
            // labelTotalAttempts
            // 
            labelTotalAttempts.Location = new Point(149, 34);
            labelTotalAttempts.Name = "labelTotalAttempts";
            labelTotalAttempts.Size = new Size(88, 19);
            labelTotalAttempts.TabIndex = 2;
            labelTotalAttempts.Text = "0 / 0";
            // 
            // labelScore
            // 
            labelScore.Location = new Point(9, 56);
            labelScore.Name = "labelScore";
            labelScore.Size = new Size(88, 19);
            labelScore.TabIndex = 3;
            labelScore.Text = "Средний балл:";
            // 
            // labelAverageScore
            // 
            labelAverageScore.Location = new Point(105, 56);
            labelAverageScore.Name = "labelAverageScore";
            labelAverageScore.Size = new Size(70, 19);
            labelAverageScore.TabIndex = 4;
            labelAverageScore.Text = "0%";
            // 
            // labelMedian
            // 
            labelMedian.Location = new Point(9, 79);
            labelMedian.Name = "labelMedian";
            labelMedian.Size = new Size(96, 19);
            labelMedian.TabIndex = 5;
            labelMedian.Text = "Медианный балл:";
            // 
            // labelMedianScore
            // 
            labelMedianScore.Location = new Point(114, 79);
            labelMedianScore.Name = "labelMedianScore";
            labelMedianScore.Size = new Size(70, 19);
            labelMedianScore.TabIndex = 6;
            labelMedianScore.Text = "0%";
            // 
            // labelMax
            // 
            labelMax.Location = new Point(262, 34);
            labelMax.Name = "labelMax";
            labelMax.Size = new Size(88, 19);
            labelMax.TabIndex = 7;
            labelMax.Text = "Максимум:";
            // 
            // labelMaxScore
            // 
            labelMaxScore.Location = new Point(359, 34);
            labelMaxScore.Name = "labelMaxScore";
            labelMaxScore.Size = new Size(70, 19);
            labelMaxScore.TabIndex = 8;
            labelMaxScore.Text = "0%";
            // 
            // labelMin
            // 
            labelMin.Location = new Point(262, 56);
            labelMin.Name = "labelMin";
            labelMin.Size = new Size(88, 19);
            labelMin.TabIndex = 9;
            labelMin.Text = "Минимум:";
            // 
            // labelMinScore
            // 
            labelMinScore.Location = new Point(359, 56);
            labelMinScore.Name = "labelMinScore";
            labelMinScore.Size = new Size(70, 19);
            labelMinScore.TabIndex = 10;
            labelMinScore.Text = "0%";
            // 
            // labelTime
            // 
            labelTime.Location = new Point(262, 79);
            labelTime.Name = "labelTime";
            labelTime.Size = new Size(88, 19);
            labelTime.TabIndex = 11;
            labelTime.Text = "Среднее время:";
            // 
            // labelAverageTime
            // 
            labelAverageTime.Location = new Point(359, 79);
            labelAverageTime.Name = "labelAverageTime";
            labelAverageTime.Size = new Size(88, 19);
            labelAverageTime.TabIndex = 12;
            labelAverageTime.Text = "0 мин";
            // 
            // progressAverage
            // 
            progressAverage.Location = new Point(9, 109);
            progressAverage.Margin = new Padding(3, 2, 3, 2);
            progressAverage.Name = "progressAverage";
            progressAverage.Size = new Size(481, 19);
            progressAverage.TabIndex = 13;
            // 
            // panelCharts
            // 
            panelCharts.Dock = DockStyle.Fill;
            panelCharts.Location = new Point(528, 48);
            panelCharts.Margin = new Padding(3, 2, 3, 2);
            panelCharts.Name = "panelCharts";
            panelCharts.Size = new Size(510, 167);
            panelCharts.TabIndex = 2;
            // 
            // panelQuestions
            // 
            panelQuestions.BorderStyle = BorderStyle.FixedSingle;
            panelQuestions.Controls.Add(labelQuestionsTitle);
            panelQuestions.Controls.Add(listViewQuestions);
            panelQuestions.Dock = DockStyle.Fill;
            panelQuestions.Location = new Point(12, 219);
            panelQuestions.Margin = new Padding(3, 2, 3, 2);
            panelQuestions.Name = "panelQuestions";
            panelQuestions.Size = new Size(510, 265);
            panelQuestions.TabIndex = 3;
            // 
            // labelQuestionsTitle
            // 
            labelQuestionsTitle.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            labelQuestionsTitle.Location = new Point(9, 8);
            labelQuestionsTitle.Name = "labelQuestionsTitle";
            labelQuestionsTitle.Size = new Size(175, 19);
            labelQuestionsTitle.TabIndex = 0;
            labelQuestionsTitle.Text = "Самые сложные вопросы";
            // 
            // listViewQuestions
            // 
            listViewQuestions.Columns.AddRange(new ColumnHeader[] { columnHeaderId, columnHeaderText, columnHeaderPercent, columnHeaderCount });
            listViewQuestions.Dock = DockStyle.Fill;
            listViewQuestions.FullRowSelect = true;
            listViewQuestions.GridLines = true;
            listViewQuestions.Location = new Point(0, 0);
            listViewQuestions.Margin = new Padding(3, 2, 3, 2);
            listViewQuestions.Name = "listViewQuestions";
            listViewQuestions.Size = new Size(508, 263);
            listViewQuestions.TabIndex = 1;
            listViewQuestions.UseCompatibleStateImageBehavior = false;
            listViewQuestions.View = View.Details;
            // 
            // columnHeaderId
            // 
            columnHeaderId.Text = "ID";
            columnHeaderId.Width = 50;
            // 
            // columnHeaderText
            // 
            columnHeaderText.Text = "Текст вопроса";
            columnHeaderText.Width = 350;
            // 
            // columnHeaderPercent
            // 
            columnHeaderPercent.Text = "Правильно (%)";
            columnHeaderPercent.Width = 100;
            // 
            // columnHeaderCount
            // 
            columnHeaderCount.Text = "Правильно/Всего";
            columnHeaderCount.Width = 100;
            // 
            // panelAttempts
            // 
            panelAttempts.BorderStyle = BorderStyle.FixedSingle;
            panelAttempts.Controls.Add(labelAttemptsTitle);
            panelAttempts.Controls.Add(listViewAttempts);
            panelAttempts.Dock = DockStyle.Fill;
            panelAttempts.Location = new Point(528, 219);
            panelAttempts.Margin = new Padding(3, 2, 3, 2);
            panelAttempts.Name = "panelAttempts";
            panelAttempts.Size = new Size(510, 265);
            panelAttempts.TabIndex = 4;
            // 
            // labelAttemptsTitle
            // 
            labelAttemptsTitle.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            labelAttemptsTitle.Location = new Point(9, 8);
            labelAttemptsTitle.Name = "labelAttemptsTitle";
            labelAttemptsTitle.Size = new Size(175, 19);
            labelAttemptsTitle.TabIndex = 0;
            labelAttemptsTitle.Text = "Последние попытки";
            // 
            // listViewAttempts
            // 
            listViewAttempts.Columns.AddRange(new ColumnHeader[] { columnHeaderUserName, columnHeaderDate, columnHeaderPoints, columnHeaderPercentResult, columnHeaderTime });
            listViewAttempts.Dock = DockStyle.Fill;
            listViewAttempts.FullRowSelect = true;
            listViewAttempts.GridLines = true;
            listViewAttempts.Location = new Point(0, 0);
            listViewAttempts.Margin = new Padding(3, 2, 3, 2);
            listViewAttempts.Name = "listViewAttempts";
            listViewAttempts.Size = new Size(508, 263);
            listViewAttempts.TabIndex = 2;
            listViewAttempts.UseCompatibleStateImageBehavior = false;
            listViewAttempts.View = View.Details;
            // 
            // columnHeaderUserName
            // 
            columnHeaderUserName.Text = "Пользователь";
            columnHeaderUserName.Width = 150;
            // 
            // columnHeaderDate
            // 
            columnHeaderDate.Text = "Дата";
            columnHeaderDate.Width = 120;
            // 
            // columnHeaderPoints
            // 
            columnHeaderPoints.Text = "Баллы";
            columnHeaderPoints.Width = 80;
            // 
            // columnHeaderPercentResult
            // 
            columnHeaderPercentResult.Text = "%";
            columnHeaderPercentResult.Width = 80;
            // 
            // columnHeaderTime
            // 
            columnHeaderTime.Text = "Время";
            columnHeaderTime.Width = 80;
            // 
            // labelMessage
            // 
            tableLayout.SetColumnSpan(labelMessage, 2);
            labelMessage.ForeColor = Color.Blue;
            labelMessage.Location = new Point(12, 486);
            labelMessage.Name = "labelMessage";
            labelMessage.Size = new Size(1026, 22);
            labelMessage.TabIndex = 5;
            labelMessage.TextAlign = ContentAlignment.MiddleCenter;
            labelMessage.Visible = false;
            // 
            // StatisticsForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(240, 240, 240);
            ClientSize = new Size(1050, 525);
            Controls.Add(tableLayout);
            Margin = new Padding(3, 2, 3, 2);
            Name = "StatisticsForm";
            StartPosition = FormStartPosition.CenterParent;
            Text = "Статистика и аналитика";
            tableLayout.ResumeLayout(false);
            panelTop.ResumeLayout(false);
            panelTop.PerformLayout();
            panelGeneral.ResumeLayout(false);
            panelQuestions.ResumeLayout(false);
            panelAttempts.ResumeLayout(false);
            ResumeLayout(false);
        }
    }
}