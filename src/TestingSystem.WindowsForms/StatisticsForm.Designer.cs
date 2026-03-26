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
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Series series1 = new System.Windows.Forms.DataVisualization.Charting.Series();
            this.tableLayout = new System.Windows.Forms.TableLayoutPanel();
            this.panelTop = new System.Windows.Forms.Panel();
            this.labelTestSelect = new System.Windows.Forms.Label();
            this.comboBoxTests = new System.Windows.Forms.ComboBox();
            this.buttonRefresh = new System.Windows.Forms.Button();
            this.buttonExportExcel = new System.Windows.Forms.Button();
            this.panelGeneral = new System.Windows.Forms.Panel();
            this.labelGeneralTitle = new System.Windows.Forms.Label();
            this.labelAttempts = new System.Windows.Forms.Label();
            this.labelTotalAttempts = new System.Windows.Forms.Label();
            this.labelScore = new System.Windows.Forms.Label();
            this.labelAverageScore = new System.Windows.Forms.Label();
            this.labelMedian = new System.Windows.Forms.Label();
            this.labelMedianScore = new System.Windows.Forms.Label();
            this.labelMax = new System.Windows.Forms.Label();
            this.labelMaxScore = new System.Windows.Forms.Label();
            this.labelMin = new System.Windows.Forms.Label();
            this.labelMinScore = new System.Windows.Forms.Label();
            this.labelTime = new System.Windows.Forms.Label();
            this.labelAverageTime = new System.Windows.Forms.Label();
            this.progressAverage = new System.Windows.Forms.ProgressBar();
            this.panelCharts = new System.Windows.Forms.Panel();
            this.chartScoreDistribution = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.panelQuestions = new System.Windows.Forms.Panel();
            this.labelQuestionsTitle = new System.Windows.Forms.Label();
            this.listViewQuestions = new System.Windows.Forms.ListView();
            this.columnHeaderId = new System.Windows.Forms.ColumnHeader();
            this.columnHeaderText = new System.Windows.Forms.ColumnHeader();
            this.columnHeaderPercent = new System.Windows.Forms.ColumnHeader();
            this.columnHeaderCount = new System.Windows.Forms.ColumnHeader();
            this.panelAttempts = new System.Windows.Forms.Panel();
            this.labelAttemptsTitle = new System.Windows.Forms.Label();
            this.listViewAttempts = new System.Windows.Forms.ListView();
            this.columnHeaderUserName = new System.Windows.Forms.ColumnHeader();
            this.columnHeaderDate = new System.Windows.Forms.ColumnHeader();
            this.columnHeaderPoints = new System.Windows.Forms.ColumnHeader();
            this.columnHeaderPercentResult = new System.Windows.Forms.ColumnHeader();
            this.columnHeaderTime = new System.Windows.Forms.ColumnHeader();
            this.labelMessage = new System.Windows.Forms.Label();
            this.tableLayout.SuspendLayout();
            this.panelTop.SuspendLayout();
            this.panelGeneral.SuspendLayout();
            this.panelCharts.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chartScoreDistribution)).BeginInit();
            this.panelQuestions.SuspendLayout();
            this.panelAttempts.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayout
            // 
            this.tableLayout.ColumnCount = 2;
            this.tableLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayout.Controls.Add(this.panelTop, 0, 0);
            this.tableLayout.Controls.Add(this.panelGeneral, 0, 1);
            this.tableLayout.Controls.Add(this.panelCharts, 1, 1);
            this.tableLayout.Controls.Add(this.panelQuestions, 0, 2);
            this.tableLayout.Controls.Add(this.panelAttempts, 1, 2);
            this.tableLayout.Controls.Add(this.labelMessage, 0, 3);
            this.tableLayout.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayout.Location = new System.Drawing.Point(0, 0);
            this.tableLayout.Name = "tableLayout";
            this.tableLayout.Padding = new System.Windows.Forms.Padding(10);
            this.tableLayout.RowCount = 4;
            this.tableLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 50F));
            this.tableLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 35F));
            this.tableLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 55F));
            this.tableLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.tableLayout.Size = new System.Drawing.Size(1200, 700);
            this.tableLayout.TabIndex = 0;
            // 
            // panelTop
            // 
            this.panelTop.Controls.Add(this.labelTestSelect);
            this.panelTop.Controls.Add(this.comboBoxTests);
            this.panelTop.Controls.Add(this.buttonRefresh);
            this.panelTop.Controls.Add(this.buttonExportExcel);
            this.panelTop.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelTop.Location = new System.Drawing.Point(13, 13);
            this.panelTop.Name = "panelTop";
            this.panelTop.Size = new System.Drawing.Size(1174, 44);
            this.panelTop.TabIndex = 0;
            // 
            // labelTestSelect
            // 
            this.labelTestSelect.AutoSize = true;
            this.labelTestSelect.Location = new System.Drawing.Point(10, 12);
            this.labelTestSelect.Name = "labelTestSelect";
            this.labelTestSelect.Size = new System.Drawing.Size(43, 20);
            this.labelTestSelect.TabIndex = 0;
            this.labelTestSelect.Text = "Тест:";
            // 
            // comboBoxTests
            // 
            this.comboBoxTests.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxTests.Location = new System.Drawing.Point(60, 9);
            this.comboBoxTests.Name = "comboBoxTests";
            this.comboBoxTests.Size = new System.Drawing.Size(300, 28);
            this.comboBoxTests.TabIndex = 1;
            // 
            // buttonRefresh
            // 
            this.buttonRefresh.Location = new System.Drawing.Point(380, 8);
            this.buttonRefresh.Name = "buttonRefresh";
            this.buttonRefresh.Size = new System.Drawing.Size(100, 30);
            this.buttonRefresh.TabIndex = 2;
            this.buttonRefresh.Text = "Обновить";
            this.buttonRefresh.UseVisualStyleBackColor = true;
            // 
            // buttonExportExcel
            // 
            this.buttonExportExcel.BackColor = System.Drawing.Color.FromArgb(0, 120, 215);
            this.buttonExportExcel.FlatAppearance.BorderSize = 0;
            this.buttonExportExcel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonExportExcel.ForeColor = System.Drawing.Color.White;
            this.buttonExportExcel.Location = new System.Drawing.Point(500, 8);
            this.buttonExportExcel.Name = "buttonExportExcel";
            this.buttonExportExcel.Size = new System.Drawing.Size(120, 30);
            this.buttonExportExcel.TabIndex = 3;
            this.buttonExportExcel.Text = "Экспорт Excel";
            this.buttonExportExcel.UseVisualStyleBackColor = false;
            // 
            // panelGeneral
            // 
            this.panelGeneral.BackColor = System.Drawing.Color.White;
            this.panelGeneral.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelGeneral.Controls.Add(this.labelGeneralTitle);
            this.panelGeneral.Controls.Add(this.labelAttempts);
            this.panelGeneral.Controls.Add(this.labelTotalAttempts);
            this.panelGeneral.Controls.Add(this.labelScore);
            this.panelGeneral.Controls.Add(this.labelAverageScore);
            this.panelGeneral.Controls.Add(this.labelMedian);
            this.panelGeneral.Controls.Add(this.labelMedianScore);
            this.panelGeneral.Controls.Add(this.labelMax);
            this.panelGeneral.Controls.Add(this.labelMaxScore);
            this.panelGeneral.Controls.Add(this.labelMin);
            this.panelGeneral.Controls.Add(this.labelMinScore);
            this.panelGeneral.Controls.Add(this.labelTime);
            this.panelGeneral.Controls.Add(this.labelAverageTime);
            this.panelGeneral.Controls.Add(this.progressAverage);
            this.panelGeneral.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelGeneral.Location = new System.Drawing.Point(13, 63);
            this.panelGeneral.Name = "panelGeneral";
            this.panelGeneral.Size = new System.Drawing.Size(574, 199);
            this.panelGeneral.TabIndex = 1;
            // 
            // labelGeneralTitle
            // 
            this.labelGeneralTitle.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.labelGeneralTitle.Location = new System.Drawing.Point(10, 10);
            this.labelGeneralTitle.Name = "labelGeneralTitle";
            this.labelGeneralTitle.Size = new System.Drawing.Size(200, 25);
            this.labelGeneralTitle.TabIndex = 0;
            this.labelGeneralTitle.Text = "Общая статистика";
            // 
            // labelAttempts
            // 
            this.labelAttempts.Location = new System.Drawing.Point(10, 45);
            this.labelAttempts.Name = "labelAttempts";
            this.labelAttempts.Size = new System.Drawing.Size(150, 25);
            this.labelAttempts.TabIndex = 1;
            this.labelAttempts.Text = "Попыток (всего/завершено):";
            // 
            // labelTotalAttempts
            // 
            this.labelTotalAttempts.Location = new System.Drawing.Point(170, 45);
            this.labelTotalAttempts.Name = "labelTotalAttempts";
            this.labelTotalAttempts.Size = new System.Drawing.Size(100, 25);
            this.labelTotalAttempts.TabIndex = 2;
            this.labelTotalAttempts.Text = "0 / 0";
            // 
            // labelScore
            // 
            this.labelScore.Location = new System.Drawing.Point(10, 75);
            this.labelScore.Name = "labelScore";
            this.labelScore.Size = new System.Drawing.Size(100, 25);
            this.labelScore.TabIndex = 3;
            this.labelScore.Text = "Средний балл:";
            // 
            // labelAverageScore
            // 
            this.labelAverageScore.Location = new System.Drawing.Point(120, 75);
            this.labelAverageScore.Name = "labelAverageScore";
            this.labelAverageScore.Size = new System.Drawing.Size(80, 25);
            this.labelAverageScore.TabIndex = 4;
            this.labelAverageScore.Text = "0%";
            // 
            // labelMedian
            // 
            this.labelMedian.Location = new System.Drawing.Point(10, 105);
            this.labelMedian.Name = "labelMedian";
            this.labelMedian.Size = new System.Drawing.Size(110, 25);
            this.labelMedian.TabIndex = 5;
            this.labelMedian.Text = "Медианный балл:";
            // 
            // labelMedianScore
            // 
            this.labelMedianScore.Location = new System.Drawing.Point(130, 105);
            this.labelMedianScore.Name = "labelMedianScore";
            this.labelMedianScore.Size = new System.Drawing.Size(80, 25);
            this.labelMedianScore.TabIndex = 6;
            this.labelMedianScore.Text = "0%";
            // 
            // labelMax
            // 
            this.labelMax.Location = new System.Drawing.Point(300, 45);
            this.labelMax.Name = "labelMax";
            this.labelMax.Size = new System.Drawing.Size(100, 25);
            this.labelMax.TabIndex = 7;
            this.labelMax.Text = "Максимум:";
            // 
            // labelMaxScore
            // 
            this.labelMaxScore.Location = new System.Drawing.Point(410, 45);
            this.labelMaxScore.Name = "labelMaxScore";
            this.labelMaxScore.Size = new System.Drawing.Size(80, 25);
            this.labelMaxScore.TabIndex = 8;
            this.labelMaxScore.Text = "0%";
            // 
            // labelMin
            // 
            this.labelMin.Location = new System.Drawing.Point(300, 75);
            this.labelMin.Name = "labelMin";
            this.labelMin.Size = new System.Drawing.Size(100, 25);
            this.labelMin.TabIndex = 9;
            this.labelMin.Text = "Минимум:";
            // 
            // labelMinScore
            // 
            this.labelMinScore.Location = new System.Drawing.Point(410, 75);
            this.labelMinScore.Name = "labelMinScore";
            this.labelMinScore.Size = new System.Drawing.Size(80, 25);
            this.labelMinScore.TabIndex = 10;
            this.labelMinScore.Text = "0%";
            // 
            // labelTime
            // 
            this.labelTime.Location = new System.Drawing.Point(300, 105);
            this.labelTime.Name = "labelTime";
            this.labelTime.Size = new System.Drawing.Size(100, 25);
            this.labelTime.TabIndex = 11;
            this.labelTime.Text = "Среднее время:";
            // 
            // labelAverageTime
            // 
            this.labelAverageTime.Location = new System.Drawing.Point(410, 105);
            this.labelAverageTime.Name = "labelAverageTime";
            this.labelAverageTime.Size = new System.Drawing.Size(100, 25);
            this.labelAverageTime.TabIndex = 12;
            this.labelAverageTime.Text = "0 мин";
            // 
            // progressAverage
            // 
            this.progressAverage.Location = new System.Drawing.Point(10, 145);
            this.progressAverage.Name = "progressAverage";
            this.progressAverage.Size = new System.Drawing.Size(550, 25);
            this.progressAverage.TabIndex = 13;
            this.progressAverage.Maximum = 100;
            // 
            // panelCharts
            // 
            this.panelCharts.Controls.Add(this.chartScoreDistribution);
            this.panelCharts.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelCharts.Location = new System.Drawing.Point(593, 63);
            this.panelCharts.Name = "panelCharts";
            this.panelCharts.Size = new System.Drawing.Size(594, 199);
            this.panelCharts.TabIndex = 2;
            // 
            // chartScoreDistribution
            // 
            this.chartScoreDistribution.Dock = System.Windows.Forms.DockStyle.Fill;
            chartArea1.Name = "ChartArea1";
            this.chartScoreDistribution.ChartAreas.Add(chartArea1);
            this.chartScoreDistribution.Location = new System.Drawing.Point(0, 0);
            this.chartScoreDistribution.Name = "chartScoreDistribution";
            series1.ChartArea = "ChartArea1";
            series1.Name = "Распределение оценок";
            this.chartScoreDistribution.Series.Add(series1);
            this.chartScoreDistribution.Size = new System.Drawing.Size(594, 199);
            this.chartScoreDistribution.TabIndex = 0;
            // 
            // panelQuestions
            // 
            this.panelQuestions.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelQuestions.Controls.Add(this.labelQuestionsTitle);
            this.panelQuestions.Controls.Add(this.listViewQuestions);
            this.panelQuestions.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelQuestions.Location = new System.Drawing.Point(13, 268);
            this.panelQuestions.Name = "panelQuestions";
            this.panelQuestions.Size = new System.Drawing.Size(574, 371);
            this.panelQuestions.TabIndex = 3;
            // 
            // labelQuestionsTitle
            // 
            this.labelQuestionsTitle.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.labelQuestionsTitle.Location = new System.Drawing.Point(10, 10);
            this.labelQuestionsTitle.Name = "labelQuestionsTitle";
            this.labelQuestionsTitle.Size = new System.Drawing.Size(200, 25);
            this.labelQuestionsTitle.TabIndex = 0;
            this.labelQuestionsTitle.Text = "Самые сложные вопросы";
            // 
            // listViewQuestions
            // 
            this.listViewQuestions.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeaderId,
            this.columnHeaderText,
            this.columnHeaderPercent,
            this.columnHeaderCount});
            this.listViewQuestions.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listViewQuestions.FullRowSelect = true;
            this.listViewQuestions.GridLines = true;
            this.listViewQuestions.Location = new System.Drawing.Point(0, 35);
            this.listViewQuestions.Name = "listViewQuestions";
            this.listViewQuestions.Size = new System.Drawing.Size(572, 334);
            this.listViewQuestions.TabIndex = 1;
            this.listViewQuestions.UseCompatibleStateImageBehavior = false;
            this.listViewQuestions.View = System.Windows.Forms.View.Details;
            // 
            // columnHeaderId
            // 
            this.columnHeaderId.Text = "ID";
            this.columnHeaderId.Width = 50;
            // 
            // columnHeaderText
            // 
            this.columnHeaderText.Text = "Текст вопроса";
            this.columnHeaderText.Width = 350;
            // 
            // columnHeaderPercent
            // 
            this.columnHeaderPercent.Text = "Правильно (%)";
            this.columnHeaderPercent.Width = 100;
            // 
            // columnHeaderCount
            // 
            this.columnHeaderCount.Text = "Правильно/Всего";
            this.columnHeaderCount.Width = 100;
            // 
            // panelAttempts
            // 
            this.panelAttempts.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelAttempts.Controls.Add(this.labelAttemptsTitle);
            this.panelAttempts.Controls.Add(this.listViewAttempts);
            this.panelAttempts.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelAttempts.Location = new System.Drawing.Point(593, 268);
            this.panelAttempts.Name = "panelAttempts";
            this.panelAttempts.Size = new System.Drawing.Size(594, 371);
            this.panelAttempts.TabIndex = 4;
            // 
            // labelAttemptsTitle
            // 
            this.labelAttemptsTitle.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.labelAttemptsTitle.Location = new System.Drawing.Point(10, 10);
            this.labelAttemptsTitle.Name = "labelAttemptsTitle";
            this.labelAttemptsTitle.Size = new System.Drawing.Size(200, 25);
            this.labelAttemptsTitle.TabIndex = 0;
            this.labelAttemptsTitle.Text = "Последние попытки";
            // 
            // listViewAttempts
            // 
            this.listViewAttempts.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeaderUserName,
            this.columnHeaderDate,
            this.columnHeaderPoints,
            this.columnHeaderPercentResult,
            this.columnHeaderTime});
            this.listViewAttempts.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listViewAttempts.FullRowSelect = true;
            this.listViewAttempts.GridLines = true;
            this.listViewAttempts.Location = new System.Drawing.Point(0, 35);
            this.listViewAttempts.Name = "listViewAttempts";
            this.listViewAttempts.Size = new System.Drawing.Size(592, 334);
            this.listViewAttempts.TabIndex = 2;
            this.listViewAttempts.UseCompatibleStateImageBehavior = false;
            this.listViewAttempts.View = System.Windows.Forms.View.Details;
            // 
            // columnHeaderUserName
            // 
            this.columnHeaderUserName.Text = "Пользователь";
            this.columnHeaderUserName.Width = 150;
            // 
            // columnHeaderDate
            // 
            this.columnHeaderDate.Text = "Дата";
            this.columnHeaderDate.Width = 120;
            // 
            // columnHeaderPoints
            // 
            this.columnHeaderPoints.Text = "Баллы";
            this.columnHeaderPoints.Width = 80;
            // 
            // columnHeaderPercentResult
            // 
            this.columnHeaderPercentResult.Text = "%";
            this.columnHeaderPercentResult.Width = 80;
            // 
            // columnHeaderTime
            // 
            this.columnHeaderTime.Text = "Время";
            this.columnHeaderTime.Width = 80;
            // 
            // labelMessage
            // 
            this.tableLayout.SetColumnSpan(this.labelMessage, 2);
            this.labelMessage.AutoSize = false;
            this.labelMessage.Location = new System.Drawing.Point(13, 645);
            this.labelMessage.Name = "labelMessage";
            this.labelMessage.Size = new System.Drawing.Size(1174, 30);
            this.labelMessage.TabIndex = 5;
            this.labelMessage.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.labelMessage.ForeColor = System.Drawing.Color.Blue;
            this.labelMessage.Visible = false;
            // 
            // StatisticsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(240, 240, 240);
            this.ClientSize = new System.Drawing.Size(1200, 700);
            this.Controls.Add(this.tableLayout);
            this.Name = "StatisticsForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Статистика и аналитика";
            this.tableLayout.ResumeLayout(false);
            this.panelTop.ResumeLayout(false);
            this.panelTop.PerformLayout();
            this.panelGeneral.ResumeLayout(false);
            this.panelCharts.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.chartScoreDistribution)).EndInit();
            this.panelQuestions.ResumeLayout(false);
            this.panelAttempts.ResumeLayout(false);
            this.ResumeLayout(false);
        }
    }
}