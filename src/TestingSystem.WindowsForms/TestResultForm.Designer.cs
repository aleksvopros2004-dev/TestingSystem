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
            tableLayout = new TableLayoutPanel();
            panelTop = new Panel();
            flowLayoutTop = new FlowLayoutPanel();
            lblTitle = new Label();
            lblTestTitle = new Label();
            panelCenter = new Panel();
            tableLayoutCenter = new TableLayoutPanel();
            lblResultLabel = new Label();
            lblTotalLabel = new Label();
            lblTotalQuestions = new Label();
            lblPointsLabel = new Label();
            lblPointsValue = new Label();
            lblPercentageLabel = new Label();
            lblPercentage = new Label();
            lblTimeLabel = new Label();
            lblTimeSpent = new Label();
            lblGradeText = new Label();
            panelBottom = new Panel();
            btnOk = new Button();
            tableLayout.SuspendLayout();
            panelTop.SuspendLayout();
            flowLayoutTop.SuspendLayout();
            panelCenter.SuspendLayout();
            tableLayoutCenter.SuspendLayout();
            panelBottom.SuspendLayout();
            SuspendLayout();
            // 
            // tableLayout
            // 
            tableLayout.ColumnCount = 1;
            tableLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tableLayout.Controls.Add(panelTop, 0, 0);
            tableLayout.Controls.Add(panelCenter, 0, 1);
            tableLayout.Controls.Add(panelBottom, 0, 2);
            tableLayout.Dock = DockStyle.Fill;
            tableLayout.Location = new Point(0, 0);
            tableLayout.Name = "tableLayout";
            tableLayout.RowCount = 3;
            tableLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 85F));
            tableLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tableLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 70F));
            tableLayout.Size = new Size(600, 480);
            tableLayout.TabIndex = 0;
            // 
            // panelTop
            // 
            panelTop.BackColor = Color.FromArgb(76, 175, 80);
            panelTop.Controls.Add(flowLayoutTop);
            panelTop.Dock = DockStyle.Fill;
            panelTop.Location = new Point(0, 0);
            panelTop.Name = "panelTop";
            panelTop.Padding = new Padding(20, 10, 20, 10);
            panelTop.Size = new Size(600, 85);
            panelTop.TabIndex = 0;
            // 
            // flowLayoutTop
            // 
            flowLayoutTop.Controls.Add(lblTitle);
            flowLayoutTop.Controls.Add(lblTestTitle);
            flowLayoutTop.Dock = DockStyle.Fill;
            flowLayoutTop.FlowDirection = FlowDirection.TopDown;
            flowLayoutTop.Location = new Point(20, 10);
            flowLayoutTop.Name = "flowLayoutTop";
            flowLayoutTop.Size = new Size(560, 65);
            flowLayoutTop.TabIndex = 0;
            // 
            // lblTitle
            // 
            lblTitle.AutoSize = true;
            lblTitle.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            lblTitle.ForeColor = Color.White;
            lblTitle.Location = new Point(3, 0);
            lblTitle.Name = "lblTitle";
            lblTitle.Size = new Size(100, 21);
            lblTitle.TabIndex = 0;
            lblTitle.Text = "Результаты:";
            // 
            // lblTestTitle
            // 
            lblTestTitle.AutoSize = true;
            lblTestTitle.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
            lblTestTitle.ForeColor = Color.White;
            lblTestTitle.Location = new Point(3, 21);
            lblTestTitle.Name = "lblTestTitle";
            lblTestTitle.Size = new Size(135, 25);
            lblTestTitle.TabIndex = 1;
            lblTestTitle.Text = "Название теста";
            // 
            // panelCenter
            // 
            panelCenter.BackColor = Color.White;
            panelCenter.Controls.Add(tableLayoutCenter);
            panelCenter.Dock = DockStyle.Fill;
            panelCenter.Location = new Point(0, 85);
            panelCenter.Name = "panelCenter";
            panelCenter.Padding = new Padding(30, 20, 30, 20);
            panelCenter.Size = new Size(600, 325);
            panelCenter.TabIndex = 1;
            // 
            // tableLayoutCenter
            // 
            tableLayoutCenter.ColumnCount = 2;
            tableLayoutCenter.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 160F));
            tableLayoutCenter.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tableLayoutCenter.Controls.Add(lblResultLabel, 0, 0);
            tableLayoutCenter.Controls.Add(lblTotalLabel, 0, 1);
            tableLayoutCenter.Controls.Add(lblTotalQuestions, 1, 1);
            tableLayoutCenter.Controls.Add(lblPointsLabel, 0, 2);
            tableLayoutCenter.Controls.Add(lblPointsValue, 1, 2);
            tableLayoutCenter.Controls.Add(lblPercentageLabel, 0, 3);
            tableLayoutCenter.Controls.Add(lblPercentage, 1, 3);
            tableLayoutCenter.Controls.Add(lblTimeLabel, 0, 4);
            tableLayoutCenter.Controls.Add(lblTimeSpent, 1, 4);
            tableLayoutCenter.Controls.Add(lblGradeText, 0, 5);
            tableLayoutCenter.Dock = DockStyle.Fill;
            tableLayoutCenter.Location = new Point(30, 20);
            tableLayoutCenter.Name = "tableLayoutCenter";
            tableLayoutCenter.RowCount = 6;
            tableLayoutCenter.RowStyles.Add(new RowStyle(SizeType.Absolute, 40F));
            tableLayoutCenter.RowStyles.Add(new RowStyle(SizeType.Absolute, 40F));
            tableLayoutCenter.RowStyles.Add(new RowStyle(SizeType.Absolute, 40F));
            tableLayoutCenter.RowStyles.Add(new RowStyle(SizeType.Absolute, 50F));
            tableLayoutCenter.RowStyles.Add(new RowStyle(SizeType.Absolute, 40F));
            tableLayoutCenter.RowStyles.Add(new RowStyle(SizeType.Absolute, 70F));
            tableLayoutCenter.Size = new Size(540, 280);
            tableLayoutCenter.TabIndex = 0;
            // 
            // lblResultLabel
            // 
            tableLayoutCenter.SetColumnSpan(lblResultLabel, 2);
            lblResultLabel.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            lblResultLabel.Location = new Point(3, 0);
            lblResultLabel.Name = "lblResultLabel";
            lblResultLabel.Size = new Size(534, 40);
            lblResultLabel.TabIndex = 0;
            lblResultLabel.Text = "Результат:";
            lblResultLabel.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // lblTotalLabel
            // 
            lblTotalLabel.Location = new Point(3, 40);
            lblTotalLabel.Name = "lblTotalLabel";
            lblTotalLabel.Size = new Size(154, 40);
            lblTotalLabel.TabIndex = 1;
            lblTotalLabel.Text = "Всего вопросов:";
            lblTotalLabel.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // lblTotalQuestions
            // 
            lblTotalQuestions.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lblTotalQuestions.Location = new Point(163, 40);
            lblTotalQuestions.Name = "lblTotalQuestions";
            lblTotalQuestions.Size = new Size(374, 40);
            lblTotalQuestions.TabIndex = 2;
            lblTotalQuestions.Text = "0";
            lblTotalQuestions.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // lblPointsLabel
            // 
            lblPointsLabel.Location = new Point(3, 80);
            lblPointsLabel.Name = "lblPointsLabel";
            lblPointsLabel.Size = new Size(154, 40);
            lblPointsLabel.TabIndex = 3;
            lblPointsLabel.Text = "Набрано баллов:";
            lblPointsLabel.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // lblPointsValue
            // 
            lblPointsValue.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lblPointsValue.Location = new Point(163, 80);
            lblPointsValue.Name = "lblPointsValue";
            lblPointsValue.Size = new Size(374, 40);
            lblPointsValue.TabIndex = 4;
            lblPointsValue.Text = "0 из 0";
            lblPointsValue.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // lblPercentageLabel
            // 
            lblPercentageLabel.Location = new Point(3, 120);
            lblPercentageLabel.Name = "lblPercentageLabel";
            lblPercentageLabel.Size = new Size(154, 50);
            lblPercentageLabel.TabIndex = 5;
            lblPercentageLabel.Text = "Итого:";
            lblPercentageLabel.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // lblPercentage
            // 
            lblPercentage.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
            lblPercentage.Location = new Point(163, 120);
            lblPercentage.Name = "lblPercentage";
            lblPercentage.Size = new Size(374, 50);
            lblPercentage.TabIndex = 6;
            lblPercentage.Text = "0%";
            lblPercentage.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // lblTimeLabel
            // 
            lblTimeLabel.Location = new Point(3, 170);
            lblTimeLabel.Name = "lblTimeLabel";
            lblTimeLabel.Size = new Size(154, 40);
            lblTimeLabel.TabIndex = 7;
            lblTimeLabel.Text = "Время:";
            lblTimeLabel.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // lblTimeSpent
            // 
            lblTimeSpent.Font = new Font("Segoe UI", 10F);
            lblTimeSpent.Location = new Point(163, 170);
            lblTimeSpent.Name = "lblTimeSpent";
            lblTimeSpent.Size = new Size(374, 40);
            lblTimeSpent.TabIndex = 8;
            lblTimeSpent.Text = "0 мин";
            lblTimeSpent.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // lblGradeText
            // 
            tableLayoutCenter.SetColumnSpan(lblGradeText, 2);
            lblGradeText.Font = new Font("Segoe UI", 18F, FontStyle.Bold);
            lblGradeText.Location = new Point(3, 210);
            lblGradeText.Name = "lblGradeText";
            lblGradeText.Size = new Size(534, 70);
            lblGradeText.TabIndex = 9;
            lblGradeText.Text = "Оценка";
            lblGradeText.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // panelBottom
            // 
            panelBottom.BackColor = Color.FromArgb(240, 240, 240);
            panelBottom.Controls.Add(btnOk);
            panelBottom.Dock = DockStyle.Fill;
            panelBottom.Location = new Point(0, 410);
            panelBottom.Name = "panelBottom";
            panelBottom.Size = new Size(600, 70);
            panelBottom.TabIndex = 2;
            // 
            // btnOk
            // 
            btnOk.BackColor = Color.FromArgb(0, 120, 215);
            btnOk.FlatAppearance.BorderSize = 0;
            btnOk.FlatStyle = FlatStyle.Flat;
            btnOk.ForeColor = Color.White;
            btnOk.Location = new Point(250, 18);
            btnOk.Name = "btnOk";
            btnOk.Size = new Size(100, 35);
            btnOk.TabIndex = 0;
            btnOk.Text = "OK";
            btnOk.UseVisualStyleBackColor = false;
            btnOk.Click += BtnOk_Click;
            // 
            // TestResultForm
            // 
            BackColor = Color.White;
            ClientSize = new Size(600, 480);
            Controls.Add(tableLayout);
            Font = new Font("Segoe UI", 10F);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            Name = "TestResultForm";
            StartPosition = FormStartPosition.CenterParent;
            Text = "Результаты тестирования";
            tableLayout.ResumeLayout(false);
            panelTop.ResumeLayout(false);
            flowLayoutTop.ResumeLayout(false);
            flowLayoutTop.PerformLayout();
            panelCenter.ResumeLayout(false);
            tableLayoutCenter.ResumeLayout(false);
            panelBottom.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private TableLayoutPanel tableLayout;
        private Panel panelTop;
        private FlowLayoutPanel flowLayoutTop;
        private Label lblTitle;
        private Label lblTestTitle;
        private Panel panelCenter;
        private TableLayoutPanel tableLayoutCenter;
        private Label lblResultLabel;
        private Label lblTotalLabel;
        private Label lblTotalQuestions;
        private Label lblPointsLabel;
        private Label lblPointsValue;
        private Label lblPercentageLabel;
        private Label lblPercentage;
        private Label lblTimeLabel;
        private Label lblTimeSpent;
        private Label lblGradeText;
        private Panel panelBottom;
        private Button btnOk;
    }
}