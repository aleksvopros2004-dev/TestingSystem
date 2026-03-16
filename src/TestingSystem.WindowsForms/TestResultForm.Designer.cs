namespace TestingSystem.WindowsForms
{
    partial class TestResultForm
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

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            panelTop = new Panel();
            lblTitle = new Label();
            lblTestTitle = new Label();
            panelCenter = new Panel();
            lblResultLabel = new Label();
            lblTotalLabel = new Label();
            lblTotalQuestions = new Label();
            lblPointsLabel = new Label();
            lblEarnedPoints = new Label();
            lblOfTotal = new Label();
            lblTotalPoints = new Label();
            lblPercentageLabel = new Label();
            lblPercentage = new Label();
            lblTimeLabel = new Label();
            lblTimeSpent = new Label();
            lblGrade = new Label();
            panelBottom = new Panel();
            btnOk = new Button();
            panelTop.SuspendLayout();
            panelCenter.SuspendLayout();
            panelBottom.SuspendLayout();
            SuspendLayout();

            // panelTop
            panelTop.BackColor = Color.FromArgb(76, 175, 80);
            panelTop.Controls.Add(lblTitle);
            panelTop.Controls.Add(lblTestTitle);
            panelTop.Dock = DockStyle.Top;
            panelTop.Location = new Point(0, 0);
            panelTop.Name = "panelTop";
            panelTop.Size = new Size(500, 70);
            panelTop.TabIndex = 0;

            // lblTitle
            lblTitle.AutoSize = true;
            lblTitle.Font = new Font("Arial", 12F, FontStyle.Bold);
            lblTitle.ForeColor = Color.White;
            lblTitle.Location = new Point(12, 10);
            lblTitle.Name = "lblTitle";
            lblTitle.Size = new Size(113, 19);
            lblTitle.TabIndex = 0;
            lblTitle.Text = "Результаты:";

            // lblTestTitle
            lblTestTitle.AutoSize = true;
            lblTestTitle.Font = new Font("Arial", 14F, FontStyle.Bold);
            lblTestTitle.ForeColor = Color.White;
            lblTestTitle.Location = new Point(12, 35);
            lblTestTitle.Name = "lblTestTitle";
            lblTestTitle.Size = new Size(95, 22);
            lblTestTitle.TabIndex = 1;
            lblTestTitle.Text = "Название";

            // panelCenter
            panelCenter.Controls.Add(lblResultLabel);
            panelCenter.Controls.Add(lblTotalLabel);
            panelCenter.Controls.Add(lblTotalQuestions);
            panelCenter.Controls.Add(lblPointsLabel);
            panelCenter.Controls.Add(lblEarnedPoints);
            panelCenter.Controls.Add(lblOfTotal);
            panelCenter.Controls.Add(lblTotalPoints);
            panelCenter.Controls.Add(lblPercentageLabel);
            panelCenter.Controls.Add(lblPercentage);
            panelCenter.Controls.Add(lblTimeLabel);
            panelCenter.Controls.Add(lblTimeSpent);
            panelCenter.Controls.Add(lblGrade);
            panelCenter.Dock = DockStyle.Fill;
            panelCenter.Location = new Point(0, 70);
            panelCenter.Name = "panelCenter";
            panelCenter.Padding = new Padding(20);
            panelCenter.Size = new Size(500, 250);
            panelCenter.TabIndex = 1;

            // lblResultLabel
            lblResultLabel.AutoSize = true;
            lblResultLabel.Font = new Font("Arial", 10F, FontStyle.Bold);
            lblResultLabel.Location = new Point(20, 20);
            lblResultLabel.Name = "lblResultLabel";
            lblResultLabel.Size = new Size(78, 16);
            lblResultLabel.TabIndex = 0;
            lblResultLabel.Text = "Результат:";

            // lblTotalLabel
            lblTotalLabel.AutoSize = true;
            lblTotalLabel.Location = new Point(40, 50);
            lblTotalLabel.Name = "lblTotalLabel";
            lblTotalLabel.Size = new Size(111, 15);
            lblTotalLabel.TabIndex = 1;
            lblTotalLabel.Text = "Всего вопросов:";

            // lblTotalQuestions
            lblTotalQuestions.AutoSize = true;
            lblTotalQuestions.Font = new Font("Arial", 10F, FontStyle.Bold);
            lblTotalQuestions.Location = new Point(200, 50);
            lblTotalQuestions.Name = "lblTotalQuestions";
            lblTotalQuestions.Size = new Size(15, 16);
            lblTotalQuestions.TabIndex = 2;
            lblTotalQuestions.Text = "0";

            // lblPointsLabel
            lblPointsLabel.AutoSize = true;
            lblPointsLabel.Location = new Point(40, 75);
            lblPointsLabel.Name = "lblPointsLabel";
            lblPointsLabel.Size = new Size(96, 15);
            lblPointsLabel.TabIndex = 3;
            lblPointsLabel.Text = "Набрано баллов:";

            // lblEarnedPoints
            lblEarnedPoints.AutoSize = true;
            lblEarnedPoints.Font = new Font("Arial", 10F, FontStyle.Bold);
            lblEarnedPoints.Location = new Point(200, 75);
            lblEarnedPoints.Name = "lblEarnedPoints";
            lblEarnedPoints.Size = new Size(15, 16);
            lblEarnedPoints.TabIndex = 4;
            lblEarnedPoints.Text = "0";

            // lblOfTotal
            lblOfTotal.AutoSize = true;
            lblOfTotal.Location = new Point(220, 75);
            lblOfTotal.Name = "lblOfTotal";
            lblOfTotal.Size = new Size(18, 15);
            lblOfTotal.TabIndex = 5;
            lblOfTotal.Text = "из";

            // lblTotalPoints
            lblTotalPoints.AutoSize = true;
            lblTotalPoints.Font = new Font("Arial", 10F, FontStyle.Bold);
            lblTotalPoints.Location = new Point(240, 75);
            lblTotalPoints.Name = "lblTotalPoints";
            lblTotalPoints.Size = new Size(15, 16);
            lblTotalPoints.TabIndex = 6;
            lblTotalPoints.Text = "0";

            // lblPercentageLabel
            lblPercentageLabel.AutoSize = true;
            lblPercentageLabel.Location = new Point(40, 105);
            lblPercentageLabel.Name = "lblPercentageLabel";
            lblPercentageLabel.Size = new Size(45, 15);
            lblPercentageLabel.TabIndex = 7;
            lblPercentageLabel.Text = "Итого:";

            // lblPercentage
            lblPercentage.AutoSize = true;
            lblPercentage.Font = new Font("Arial", 14F, FontStyle.Bold);
            lblPercentage.Location = new Point(200, 100);
            lblPercentage.Name = "lblPercentage";
            lblPercentage.Size = new Size(35, 22);
            lblPercentage.TabIndex = 8;
            lblPercentage.Text = "0%";

            // lblTimeLabel
            lblTimeLabel.AutoSize = true;
            lblTimeLabel.Location = new Point(40, 135);
            lblTimeLabel.Name = "lblTimeLabel";
            lblTimeLabel.Size = new Size(45, 15);
            lblTimeLabel.TabIndex = 9;
            lblTimeLabel.Text = "Время:";

            // lblTimeSpent
            lblTimeSpent.AutoSize = true;
            lblTimeSpent.Font = new Font("Arial", 10F);
            lblTimeSpent.Location = new Point(200, 135);
            lblTimeSpent.Name = "lblTimeSpent";
            lblTimeSpent.Size = new Size(36, 16);
            lblTimeSpent.TabIndex = 10;
            lblTimeSpent.Text = "0 мин";

            // lblGrade
            lblGrade.AutoSize = true;
            lblGrade.Font = new Font("Arial", 18F, FontStyle.Bold);
            lblGrade.Location = new Point(200, 170);
            lblGrade.Name = "lblGrade";
            lblGrade.Size = new Size(106, 29);
            lblGrade.TabIndex = 11;
            lblGrade.Text = "Оценка";

            // panelBottom
            panelBottom.BackColor = Color.FromArgb(240, 240, 240);
            panelBottom.Controls.Add(btnOk);
            panelBottom.Dock = DockStyle.Bottom;
            panelBottom.Location = new Point(0, 320);
            panelBottom.Name = "panelBottom";
            panelBottom.Size = new Size(500, 50);
            panelBottom.TabIndex = 2;

            // btnOk
            btnOk.BackColor = Color.FromArgb(76, 175, 80);
            btnOk.ForeColor = Color.White;
            btnOk.Location = new Point(200, 10);
            btnOk.Name = "btnOk";
            btnOk.Size = new Size(100, 30);
            btnOk.TabIndex = 0;
            btnOk.Text = "OK";
            btnOk.UseVisualStyleBackColor = false;
            btnOk.Click += BtnOk_Click;

            // TestResultForm
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(500, 370);
            Controls.Add(panelCenter);
            Controls.Add(panelBottom);
            Controls.Add(panelTop);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            Name = "TestResultForm";
            StartPosition = FormStartPosition.CenterParent;
            Text = "Результаты тестирования";
            panelTop.ResumeLayout(false);
            panelTop.PerformLayout();
            panelCenter.ResumeLayout(false);
            panelCenter.PerformLayout();
            panelBottom.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private Panel panelTop;
        private Label lblTitle;
        private Label lblTestTitle;
        private Panel panelCenter;
        private Label lblResultLabel;
        private Label lblTotalLabel;
        private Label lblTotalQuestions;
        private Label lblPointsLabel;
        private Label lblEarnedPoints;
        private Label lblOfTotal;
        private Label lblTotalPoints;
        private Label lblPercentageLabel;
        private Label lblPercentage;
        private Label lblTimeLabel;
        private Label lblTimeSpent;
        private Label lblGrade;
        private Panel panelBottom;
        private Button btnOk;
    }
}