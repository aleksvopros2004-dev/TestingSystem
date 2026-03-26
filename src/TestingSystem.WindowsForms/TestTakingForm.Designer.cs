namespace TestingSystem.WindowsForms
{
    partial class TestTakingForm
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
            lblTestTitle = new Label();
            lblTestDescription = new Label();
            panelQuestion = new Panel();
            lblQuestionCounter = new Label();
            lblQuestionText = new Label();
            pictureBoxQuestion = new PictureBox();
            pnlAnswers = new Panel();
            panelBottom = new Panel();
            buttonPanel = new FlowLayoutPanel();
            btnPrevious = new Button();
            btnNext = new Button();
            btnCancel = new Button();
            tableLayout.SuspendLayout();
            panelTop.SuspendLayout();
            flowLayoutTop.SuspendLayout();
            panelQuestion.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBoxQuestion).BeginInit();
            panelBottom.SuspendLayout();
            buttonPanel.SuspendLayout();
            SuspendLayout();
            // 
            // tableLayout
            // 
            tableLayout.ColumnCount = 1;
            tableLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tableLayout.Controls.Add(panelTop, 0, 0);
            tableLayout.Controls.Add(panelQuestion, 0, 1);
            tableLayout.Controls.Add(panelBottom, 0, 2);
            tableLayout.Dock = DockStyle.Fill;
            tableLayout.Location = new Point(0, 0);
            tableLayout.Name = "tableLayout";
            tableLayout.RowCount = 3;
            tableLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 90F));
            tableLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tableLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 70F));
            tableLayout.Size = new Size(900, 650);
            tableLayout.TabIndex = 0;
            // 
            // panelTop
            // 
            panelTop.BackColor = SystemColors.Highlight;
            panelTop.Controls.Add(flowLayoutTop);
            panelTop.Dock = DockStyle.Fill;
            panelTop.Location = new Point(3, 3);
            panelTop.Name = "panelTop";
            panelTop.Padding = new Padding(20, 10, 20, 10);
            panelTop.Size = new Size(894, 84);
            panelTop.TabIndex = 0;
            // 
            // flowLayoutTop
            // 
            flowLayoutTop.Controls.Add(lblTestTitle);
            flowLayoutTop.Controls.Add(lblTestDescription);
            flowLayoutTop.Dock = DockStyle.Fill;
            flowLayoutTop.FlowDirection = FlowDirection.TopDown;
            flowLayoutTop.Location = new Point(20, 10);
            flowLayoutTop.Name = "flowLayoutTop";
            flowLayoutTop.Size = new Size(854, 64);
            flowLayoutTop.TabIndex = 0;
            // 
            // lblTestTitle
            // 
            lblTestTitle.AutoSize = true;
            lblTestTitle.Font = new Font("Segoe UI", 16F, FontStyle.Bold);
            lblTestTitle.ForeColor = Color.White;
            lblTestTitle.Location = new Point(3, 0);
            lblTestTitle.Name = "lblTestTitle";
            lblTestTitle.Size = new Size(178, 30);
            lblTestTitle.TabIndex = 0;
            lblTestTitle.Text = "Название теста";
            // 
            // lblTestDescription
            // 
            lblTestDescription.AutoSize = true;
            lblTestDescription.Font = new Font("Segoe UI", 10F);
            lblTestDescription.ForeColor = Color.White;
            lblTestDescription.Location = new Point(3, 30);
            lblTestDescription.Name = "lblTestDescription";
            lblTestDescription.Size = new Size(108, 19);
            lblTestDescription.TabIndex = 1;
            lblTestDescription.Text = "Описание теста";
            // 
            // panelQuestion
            // 
            panelQuestion.AutoScroll = true;
            panelQuestion.BackColor = Color.White;
            panelQuestion.Controls.Add(lblQuestionCounter);
            panelQuestion.Controls.Add(lblQuestionText);
            panelQuestion.Controls.Add(pictureBoxQuestion);
            panelQuestion.Controls.Add(pnlAnswers);
            panelQuestion.Dock = DockStyle.Fill;
            panelQuestion.Location = new Point(3, 93);
            panelQuestion.Name = "panelQuestion";
            panelQuestion.Padding = new Padding(20);
            panelQuestion.Size = new Size(894, 484);
            panelQuestion.TabIndex = 1;
            // 
            // lblQuestionCounter
            // 
            lblQuestionCounter.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lblQuestionCounter.ForeColor = Color.Gray;
            lblQuestionCounter.Location = new Point(20, 20);
            lblQuestionCounter.Name = "lblQuestionCounter";
            lblQuestionCounter.Size = new Size(860, 25);
            lblQuestionCounter.TabIndex = 0;
            lblQuestionCounter.Text = "Вопрос 0 из 0";
            lblQuestionCounter.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // lblQuestionText
            // 
            lblQuestionText.Font = new Font("Segoe UI", 12F);
            lblQuestionText.Location = new Point(20, 55);
            lblQuestionText.Name = "lblQuestionText";
            lblQuestionText.Size = new Size(860, 70);
            lblQuestionText.TabIndex = 1;
            lblQuestionText.Text = "Текст вопроса";
            // 
            // pictureBoxQuestion
            // 
            pictureBoxQuestion.BorderStyle = BorderStyle.FixedSingle;
            pictureBoxQuestion.Location = new Point(20, 135);
            pictureBoxQuestion.Name = "pictureBoxQuestion";
            pictureBoxQuestion.Size = new Size(300, 200);
            pictureBoxQuestion.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBoxQuestion.TabIndex = 2;
            pictureBoxQuestion.TabStop = false;
            pictureBoxQuestion.Visible = false;
            // 
            // pnlAnswers
            // 
            pnlAnswers.AutoScroll = true;
            pnlAnswers.BorderStyle = BorderStyle.FixedSingle;
            pnlAnswers.Location = new Point(20, 360);
            pnlAnswers.Name = "pnlAnswers";
            pnlAnswers.Size = new Size(800, 280);
            pnlAnswers.TabIndex = 3;
            // 
            // panelBottom
            // 
            panelBottom.BackColor = Color.FromArgb(240, 240, 240);
            panelBottom.Controls.Add(buttonPanel);
            panelBottom.Dock = DockStyle.Fill;
            panelBottom.Location = new Point(3, 583);
            panelBottom.Name = "panelBottom";
            panelBottom.Padding = new Padding(20, 15, 20, 15);
            panelBottom.Size = new Size(894, 64);
            panelBottom.TabIndex = 2;
            // 
            // buttonPanel
            // 
            buttonPanel.Controls.Add(btnPrevious);
            buttonPanel.Controls.Add(btnNext);
            buttonPanel.Controls.Add(btnCancel);
            buttonPanel.Dock = DockStyle.Fill;
            buttonPanel.FlowDirection = FlowDirection.RightToLeft;
            buttonPanel.Location = new Point(20, 15);
            buttonPanel.Name = "buttonPanel";
            buttonPanel.Size = new Size(854, 34);
            buttonPanel.TabIndex = 0;
            // 
            // btnPrevious
            // 
            btnPrevious.AutoSize = true;
            btnPrevious.Location = new Point(776, 3);
            btnPrevious.Name = "btnPrevious";
            btnPrevious.Size = new Size(75, 29);
            btnPrevious.TabIndex = 0;
            btnPrevious.Text = "← Назад";
            btnPrevious.UseVisualStyleBackColor = true;
            btnPrevious.Click += BtnPrevious_Click;
            // 
            // btnNext
            // 
            btnNext.AutoSize = true;
            btnNext.BackColor = SystemColors.Highlight;
            btnNext.FlatAppearance.BorderSize = 0;
            btnNext.FlatStyle = FlatStyle.Flat;
            btnNext.ForeColor = Color.White;
            btnNext.Location = new Point(695, 3);
            btnNext.Name = "btnNext";
            btnNext.Size = new Size(75, 29);
            btnNext.TabIndex = 1;
            btnNext.Text = "Далее →";
            btnNext.UseVisualStyleBackColor = false;
            btnNext.Click += BtnNext_Click;
            // 
            // btnCancel
            // 
            btnCancel.AutoSize = true;
            btnCancel.Location = new Point(610, 3);
            btnCancel.Name = "btnCancel";
            btnCancel.Size = new Size(79, 29);
            btnCancel.TabIndex = 2;
            btnCancel.Text = "Прервать";
            btnCancel.UseVisualStyleBackColor = true;
            btnCancel.Click += BtnCancel_Click;
            // 
            // TestTakingForm
            // 
            BackColor = Color.White;
            ClientSize = new Size(900, 650);
            Controls.Add(tableLayout);
            Font = new Font("Segoe UI", 10F);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            Name = "TestTakingForm";
            StartPosition = FormStartPosition.CenterParent;
            Text = "Прохождение теста";
            tableLayout.ResumeLayout(false);
            panelTop.ResumeLayout(false);
            flowLayoutTop.ResumeLayout(false);
            flowLayoutTop.PerformLayout();
            panelQuestion.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)pictureBoxQuestion).EndInit();
            panelBottom.ResumeLayout(false);
            buttonPanel.ResumeLayout(false);
            buttonPanel.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private TableLayoutPanel tableLayout;
        private Panel panelTop;
        private FlowLayoutPanel flowLayoutTop;
        private Label lblTestTitle;
        private Label lblTestDescription;
        private Panel panelQuestion;
        private Label lblQuestionCounter;
        private Label lblQuestionText;
        private PictureBox pictureBoxQuestion;
        private Panel pnlAnswers;
        private Panel panelBottom;
        private FlowLayoutPanel buttonPanel;
        private Button btnPrevious;
        private Button btnNext;
        private Button btnCancel;
    }
}