namespace TestingSystem.WindowsForms
{
    partial class StatisticsForm
    {
        private System.ComponentModel.IContainer components = null;

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
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Series series1 = new System.Windows.Forms.DataVisualization.Charting.Series();
            tableLayout = new System.Windows.Forms.TableLayoutPanel();
            panelTop = new System.Windows.Forms.Panel();
            labelTestSelect = new System.Windows.Forms.Label();
            comboBoxTests = new System.Windows.Forms.ComboBox();
            buttonRefresh = new System.Windows.Forms.Button();
            buttonExportExcel = new System.Windows.Forms.Button();
            panelGeneral = new System.Windows.Forms.Panel();
            labelGeneralTitle = new System.Windows.Forms.Label();
            labelAttempts = new System.Windows.Forms.Label();
            labelTotalAttempts = new System.Windows.Forms.Label();
            labelScore = new System.Windows.Forms.Label();
            labelAverageScore = new System.Windows.Forms.Label();
            labelMedian = new System.Windows.Forms.Label();
            labelMedianScore = new System.Windows.Forms.Label();
            labelMax = new System.Windows.Forms.Label();
            labelMaxScore = new System.Windows.Forms.Label();
            labelMin = new System.Windows.Forms.Label();
            labelMinScore = new System.Windows.Forms.Label();
            labelTime = new System.Windows.Forms.Label();
            labelAverageTime = new System.Windows.Forms.Label();
            progressAverage = new System.Windows.Forms.ProgressBar();
            panelCharts = new System.Windows.Forms.Panel();
            chartScoreDistribution = new System.Windows.Forms.DataVisualization.Charting.Chart();
            panelQuestions = new System.Windows.Forms.Panel();
            labelQuestionsTitle = new System.Windows.Forms.Label();
            listViewQuestions = new System.Windows.Forms.ListView();
            columnHeaderId = new System.Windows.Forms.ColumnHeader();
            columnHeaderText = new System.Windows.Forms.ColumnHeader();
            columnHeaderPercent = new System.Windows.Forms.ColumnHeader();
            columnHeaderCount = new System.Windows.Forms.ColumnHeader();
            panelAttempts = new System.Windows.Forms.Panel();
            labelAttemptsTitle = new System.Windows.Forms.Label();
            listViewAttempts = new System.Windows.Forms.ListView();
            columnHeaderUserName = new System.Windows.Forms.ColumnHeader();
            columnHeaderDate = new System.Windows.Forms.ColumnHeader();
            columnHeaderPoints = new System.Windows.Forms.ColumnHeader();
            columnHeaderPercentResult = new System.Windows.Forms.ColumnHeader();
            columnHeaderTime = new System.Windows.Forms.ColumnHeader();
            labelMessage = new System.Windows.Forms.Label();
            tableLayout.SuspendLayout();
            panelTop.SuspendLayout();
            panelGeneral.SuspendLayout();
            panelCharts.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)chartScoreDistribution).BeginInit();
            panelQuestions.SuspendLayout();
            panelAttempts.SuspendLayout();
            SuspendLayout();
            // 
            // tableLayout
            // 
            tableLayout.ColumnCount = 2;
            tableLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            tableLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            tableLayout.Controls.Add(panelTop, 0, 0);
            tableLayout.Controls.Add(panelGeneral, 0, 1);
            tableLayout.Controls.Add(panelCharts, 1, 1);
            tableLayout.Controls.Add(panelQuestions, 0, 2);
            tableLayout.Controls.Add(panelAttempts, 1, 2);
            tableLayout.Controls.Add(labelMessage, 0, 3);
            tableLayout.Dock = System.Windows.Forms.DockStyle.Fill;
            tableLayout.Location = new System.Drawing.Point(0, 0);
            tableLayout.Name = "tableLayout";
            tableLayout.Padding = new System.Windows.Forms.Padding(10);
            tableLayout.RowCount = 4;
            tableLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 50F));
            tableLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 35F));
            tableLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 55F));
            tableLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            tableLayout.Size = new System.Drawing.Size(1200, 700);
            tableLayout.TabIndex = 0;
            // 
            // panelTop
            // 
            panelTop.Controls.Add(labelTestSelect);
            panelTop.Controls.Add(comboBoxTests);
            panelTop.Controls.Add(buttonRefresh);
            panelTop.Controls.Add(buttonExportExcel);
            panelTop.Dock = System.Windows.Forms.DockStyle.Fill;
            panelTop.Location = new System.Drawing.Point(13, 13);
            panelTop.Name = "panelTop";
            panelTop.Size = new System.Drawing.Size(1174, 44);
            panelTop.TabIndex = 0;
            // 
            // labelTestSelect
            // 
            labelTestSelect.AutoSize = true;
            labelTestSelect.Location = new System.Drawing.Point(10, 12);
            labelTestSelect.Name = "labelTestSelect";
            labelTestSelect.Size = new System.Drawing.Size(43, 20);
            labelTestSelect.TabIndex = 0;
            labelTestSelect.Text = "Тест:";
            // 
            // comboBoxTests
            // 
            comboBoxTests.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            comboBoxTests.Location = new System.Drawing.Point(60, 9);
            comboBoxTests.Name = "comboBoxTests";
            comboBoxTests.Size = new System.Drawing.Size(300, 28);
            comboBoxTests.TabIndex = 1;
            // 
            // buttonRefresh
            // 
            buttonRefresh.Location = new System.Drawing.Point(380, 8);
            buttonRefresh.Name = "buttonRefresh";
            buttonRefresh.Size = new System.Drawing.Size(100, 30);
            buttonRefresh.TabIndex = 2;
            buttonRefresh.Text = "Обновить";
            buttonRefresh.UseVisualStyleBackColor = true;
            // 
            // buttonExportExcel
            // 
            buttonExportExcel.BackColor = System.Drawing.Color.FromArgb(0, 120, 215);
            buttonExportExcel.FlatAppearance.BorderSize = 0;
            buttonExportExcel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            buttonExportExcel.ForeColor = System.Drawing.Color.White;
            buttonExportExcel.Location = new System.Drawing.Point(490, 8);
            buttonExportExcel.Name = "buttonExportExcel";
            buttonExportExcel.Size = new System.Drawing.Size(80, 30);
            buttonExportExcel.TabIndex = 3;
            buttonExportExcel.Text = "Экспорт";
            buttonExportExcel.UseVisualStyleBackColor = false;
            // 
            // panelGeneral
            // 
            panelGeneral.BackColor = System.Drawing.Color.White;
            panelGeneral.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
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
            panelGeneral.Dock = System.Windows.Forms.DockStyle.Fill;
            panelGeneral.Location = new System.Drawing.Point(13, 63);
            panelGeneral.Name = "panelGeneral";
            panelGeneral.Size = new System.Drawing.Size(574, 199);
            panelGeneral.TabIndex = 1;
            // 
            // labelGeneralTitle
            // 
            labelGeneralTitle.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            labelGeneralTitle.Location = new System.Drawing.Point(10, 10);
            labelGeneralTitle.Name = "labelGeneralTitle";
            labelGeneralTitle.Size = new System.Drawing.Size(200, 25);
            labelGeneralTitle.TabIndex = 0;
            labelGeneralTitle.Text = "Общая статистика";
            // 
            // labelAttempts
            // 
            labelAttempts.Location = new System.Drawing.Point(10, 45);
            labelAttempts.Name = "labelAttempts";
            labelAttempts.Size = new System.Drawing.Size(150, 25);
            labelAttempts.TabIndex = 1;
            labelAttempts.Text = "Попыток (всего/завершено):";
            // 
            // labelTotalAttempts
            // 
            labelTotalAttempts.Location = new System.Drawing.Point(170, 45);
            labelTotalAttempts.Name = "labelTotalAttempts";
            labelTotalAttempts.Size = new System.Drawing.Size(100, 25);
            labelTotalAttempts.TabIndex = 2;
            labelTotalAttempts.Text = "0 / 0";
            // 
            // labelScore
            // 
            labelScore.Location = new System.Drawing.Point(10, 75);
            labelScore.Name = "labelScore";
            labelScore.Size = new System.Drawing.Size(100, 25);
            labelScore.TabIndex = 3;
            labelScore.Text = "Средний балл:";
            // 
            // labelAverageScore
            // 
            labelAverageScore.Location = new System.Drawing.Point(120, 75);
            labelAverageScore.Name = "labelAverageScore";
            labelAverageScore.Size = new System.Drawing.Size(80, 25);
            labelAverageScore.TabIndex = 4;
            labelAverageScore.Text = "0%";
            // 
            // labelMedian
            // 
            labelMedian.Location = new System.Drawing.Point(10, 105);
            labelMedian.Name = "labelMedian";
            labelMedian.Size = new System.Drawing.Size(110, 25);
            labelMedian.TabIndex = 5;
            labelMedian.Text = "Медианный балл:";
            // 
            // labelMedianScore
            // 
            labelMedianScore.Location = new System.Drawing.Point(130, 105);
            labelMedianScore.Name = "labelMedianScore";
            labelMedianScore.Size = new System.Drawing.Size(80, 25);
            labelMedianScore.TabIndex = 6;
            labelMedianScore.Text = "0%";
            // 
            // labelMax
            // 
            labelMax.Location = new System.Drawing.Point(300, 45);
            labelMax.Name = "labelMax";
            labelMax.Size = new System.Drawing.Size(100, 25);
            labelMax.TabIndex = 7;
            labelMax.Text = "Максимум:";
            // 
            // labelMaxScore
            // 
            labelMaxScore.Location = new System.Drawing.Point(410, 45);
            labelMaxScore.Name = "labelMaxScore";
            labelMaxScore.Size = new System.Drawing.Size(80, 25);
            labelMaxScore.TabIndex = 8;
            labelMaxScore.Text = "0%";
            // 
            // labelMin
            // 
            labelMin.Location = new System.Drawing.Point(300, 75);
            labelMin.Name = "labelMin";
            labelMin.Size = new System.Drawing.Size(100, 25);
            labelMin.TabIndex = 9;
            labelMin.Text = "Минимум:";
            // 
            // labelMinScore
            // 
            labelMinScore.Location = new System.Drawing.Point(410, 75);
            labelMinScore.Name = "labelMinScore";
            labelMinScore.Size = new System.Drawing.Size(80, 25);
            labelMinScore.TabIndex = 10;
            labelMinScore.Text = "0%";
            // 
            // labelTime
            // 
            labelTime.Location = new System.Drawing.Point(300, 105);
            labelTime.Name = "labelTime";
            labelTime.Size = new System.Drawing.Size(100, 25);
            labelTime.TabIndex = 11;
            labelTime.Text = "Среднее время:";
            // 
            // labelAverageTime
            // 
            labelAverageTime.Location = new System.Drawing.Point(410, 105);
            labelAverageTime.Name = "labelAverageTime";
            labelAverageTime.Size = new System.Drawing.Size(100, 25);
            labelAverageTime.TabIndex = 12;
            labelAverageTime.Text = "0 мин";
            // 
            // progressAverage
            // 
            progressAverage.Location = new System.Drawing.Point(10, 145);
            progressAverage.Name = "progressAverage";
            progressAverage.Size = new System.Drawing.Size(550, 25);
            progressAverage.TabIndex = 13;
            progressAverage.Maximum = 100;
            // 
            // panelCharts
            // 
            panelCharts.Controls.Add(chartScoreDistribution);
            panelCharts.Dock = System.Windows.Forms.DockStyle.Fill;
            panelCharts.Location = new System.Drawing.Point(593, 63);
            panelCharts.Name = "panelCharts";
            panelCharts.Size = new System.Drawing.Size(594, 199);
            panelCharts.TabIndex = 2;
            // 
            // chartScoreDistribution
            // 
            chartScoreDistribution.Dock = System.Windows.Forms.DockStyle.Fill;
            chartArea1.Name = "ChartArea1";
            chartScoreDistribution.ChartAreas.Add(chartArea1);
            chartScoreDistribution.Location = new System.Drawing.Point(0, 0);
            chartScoreDistribution.Name = "chartScoreDistribution";
            series1.ChartArea = "ChartArea1";
            series1.Name = "Распределение оценок";
            chartScoreDistribution.Series.Add(series1);
            chartScoreDistribution.Size = new System.Drawing.Size(594, 199);
            chartScoreDistribution.TabIndex = 0;
            // 
            // panelQuestions
            // 
            panelQuestions.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            panelQuestions.Controls.Add(labelQuestionsTitle);
            panelQuestions.Controls.Add(listViewQuestions);
            panelQuestions.Dock = System.Windows.Forms.DockStyle.Fill;
            panelQuestions.Location = new System.Drawing.Point(13, 268);
            panelQuestions.Name = "panelQuestions";
            panelQuestions.Size = new System.Drawing.Size(574, 371);
            panelQuestions.TabIndex = 3;
            // 
            // labelQuestionsTitle
            // 
            labelQuestionsTitle.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            labelQuestionsTitle.Location = new System.Drawing.Point(10, 10);
            labelQuestionsTitle.Name = "labelQuestionsTitle";
            labelQuestionsTitle.Size = new System.Drawing.Size(200, 25);
            labelQuestionsTitle.TabIndex = 0;
            labelQuestionsTitle.Text = "Самые сложные вопросы";
            // 
            // listViewQuestions
            // 
            listViewQuestions.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            columnHeaderId,
            columnHeaderText,
            columnHeaderPercent,
            columnHeaderCount});
            listViewQuestions.Dock = System.Windows.Forms.DockStyle.Fill;
            listViewQuestions.FullRowSelect = true;
            listViewQuestions.GridLines = true;
            listViewQuestions.Location = new System.Drawing.Point(0, 35);
            listViewQuestions.Name = "listViewQuestions";
            listViewQuestions.Size = new System.Drawing.Size(572, 334);
            listViewQuestions.TabIndex = 1;
            listViewQuestions.UseCompatibleStateImageBehavior = false;
            listViewQuestions.View = System.Windows.Forms.View.Details;
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
            panelAttempts.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            panelAttempts.Controls.Add(labelAttemptsTitle);
            panelAttempts.Controls.Add(listViewAttempts);
            panelAttempts.Dock = System.Windows.Forms.DockStyle.Fill;
            panelAttempts.Location = new System.Drawing.Point(593, 268);
            panelAttempts.Name = "panelAttempts";
            panelAttempts.Size = new System.Drawing.Size(594, 371);
            panelAttempts.TabIndex = 4;
            // 
            // labelAttemptsTitle
            // 
            labelAttemptsTitle.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            labelAttemptsTitle.Location = new System.Drawing.Point(10, 10);
            labelAttemptsTitle.Name = "labelAttemptsTitle";
            labelAttemptsTitle.Size = new System.Drawing.Size(200, 25);
            labelAttemptsTitle.TabIndex = 0;
            labelAttemptsTitle.Text = "Последние попытки";
            // 
            // listViewAttempts
            // 
            listViewAttempts.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            columnHeaderUserName,
            columnHeaderDate,
            columnHeaderPoints,
            columnHeaderPercentResult,
            columnHeaderTime});
            listViewAttempts.Dock = System.Windows.Forms.DockStyle.Fill;
            listViewAttempts.FullRowSelect = true;
            listViewAttempts.GridLines = true;
            listViewAttempts.Location = new System.Drawing.Point(0, 35);
            listViewAttempts.Name = "listViewAttempts";
            listViewAttempts.Size = new System.Drawing.Size(592, 334);
            listViewAttempts.TabIndex = 2;
            listViewAttempts.UseCompatibleStateImageBehavior = false;
            listViewAttempts.View = System.Windows.Forms.View.Details;
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
            labelMessage.AutoSize = false;
            labelMessage.Location = new System.Drawing.Point(13, 645);
            labelMessage.Name = "labelMessage";
            labelMessage.Size = new System.Drawing.Size(1174, 30);
            labelMessage.TabIndex = 5;
            labelMessage.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            labelMessage.ForeColor = System.Drawing.Color.Blue;
            labelMessage.Visible = false;
            // 
            // StatisticsForm
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            BackColor = System.Drawing.Color.FromArgb(240, 240, 240);
            ClientSize = new System.Drawing.Size(1200, 700);
            Controls.Add(tableLayout);
            Name = "StatisticsForm";
            StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            Text = "Статистика и аналитика";
            tableLayout.ResumeLayout(false);
            panelTop.ResumeLayout(false);
            panelTop.PerformLayout();
            panelGeneral.ResumeLayout(false);
            panelCharts.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)chartScoreDistribution).EndInit();
            panelQuestions.ResumeLayout(false);
            panelAttempts.ResumeLayout(false);
            ResumeLayout(false);
        }
    }
}