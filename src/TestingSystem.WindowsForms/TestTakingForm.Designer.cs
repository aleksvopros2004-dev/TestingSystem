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
            panelTop = new Panel();
            lblTestTitle = new Label();
            lblTestDescription = new Label();
            panelQuestion = new Panel();
            lblQuestionCounter = new Label();
            lblQuestionText = new Label();
            pictureBoxQuestion = new PictureBox();
            pnlAnswers = new Panel();
            panelBottom = new Panel();
            btnPrevious = new Button();
            btnNext = new Button();
            btnCancel = new Button();
            panelTop.SuspendLayout();
            panelQuestion.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBoxQuestion).BeginInit();
            panelBottom.SuspendLayout();
            SuspendLayout();

            // panelTop
            panelTop.BackColor = Color.FromArgb(76, 175, 80);
            panelTop.Controls.Add(lblTestTitle);
            panelTop.Controls.Add(lblTestDescription);
            panelTop.Dock = DockStyle.Top;
            panelTop.Location = new Point(0, 0);
            panelTop.Name = "panelTop";
            panelTop.Size = new Size(800, 80);
            panelTop.TabIndex = 0;

            // lblTestTitle
            lblTestTitle.AutoSize = true;
            lblTestTitle.Font = new Font("Arial", 16F, FontStyle.Bold);
            lblTestTitle.ForeColor = Color.White;
            lblTestTitle.Location = new Point(12, 15);
            lblTestTitle.Name = "lblTestTitle";
            lblTestTitle.Size = new Size(120, 26);
            lblTestTitle.TabIndex = 0;
            lblTestTitle.Text = "Название";

            // lblTestDescription
            lblTestDescription.AutoSize = true;
            lblTestDescription.Font = new Font("Arial", 10F);
            lblTestDescription.ForeColor = Color.White;
            lblTestDescription.Location = new Point(12, 45);
            lblTestDescription.Name = "lblTestDescription";
            lblTestDescription.Size = new Size(80, 16);
            lblTestDescription.TabIndex = 1;
            lblTestDescription.Text = "Описание";

            // panelQuestion
            panelQuestion.Controls.Add(lblQuestionCounter);
            panelQuestion.Controls.Add(lblQuestionText);
            panelQuestion.Controls.Add(pictureBoxQuestion);
            panelQuestion.Controls.Add(pnlAnswers);
            panelQuestion.Dock = DockStyle.Fill;
            panelQuestion.Location = new Point(0, 80);
            panelQuestion.Name = "panelQuestion";
            panelQuestion.Padding = new Padding(10);
            panelQuestion.Size = new Size(800, 400);
            panelQuestion.TabIndex = 1;

            // lblQuestionCounter
            lblQuestionCounter.AutoSize = true;
            lblQuestionCounter.Font = new Font("Arial", 10F, FontStyle.Bold);
            lblQuestionCounter.ForeColor = Color.Gray;
            lblQuestionCounter.Location = new Point(12, 10);
            lblQuestionCounter.Name = "lblQuestionCounter";
            lblQuestionCounter.Size = new Size(75, 16);
            lblQuestionCounter.TabIndex = 0;
            lblQuestionCounter.Text = "Вопрос 0 из 0";

            // lblQuestionText
            lblQuestionText.Font = new Font("Arial", 12F);
            lblQuestionText.Location = new Point(12, 30);
            lblQuestionText.Name = "lblQuestionText";
            lblQuestionText.Size = new Size(776, 50);
            lblQuestionText.TabIndex = 1;
            lblQuestionText.Text = "Текст вопроса";

            // pictureBoxQuestion
            pictureBoxQuestion.BorderStyle = BorderStyle.FixedSingle;
            pictureBoxQuestion.Location = new Point(12, 90);
            pictureBoxQuestion.Name = "pictureBoxQuestion";
            pictureBoxQuestion.Size = new Size(200, 150);
            pictureBoxQuestion.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBoxQuestion.TabIndex = 2;
            pictureBoxQuestion.TabStop = false;
            pictureBoxQuestion.Visible = false;

            // pnlAnswers
            pnlAnswers.AutoScroll = true;
            pnlAnswers.BorderStyle = BorderStyle.FixedSingle;
            pnlAnswers.Location = new Point(12, 250);
            pnlAnswers.Name = "pnlAnswers";
            pnlAnswers.Size = new Size(776, 138);
            pnlAnswers.TabIndex = 3;

            // panelBottom
            panelBottom.BackColor = Color.FromArgb(240, 240, 240);
            panelBottom.Controls.Add(btnPrevious);
            panelBottom.Controls.Add(btnNext);
            panelBottom.Controls.Add(btnCancel);
            panelBottom.Dock = DockStyle.Bottom;
            panelBottom.Location = new Point(0, 480);
            panelBottom.Name = "panelBottom";
            panelBottom.Size = new Size(800, 60);
            panelBottom.TabIndex = 2;

            // btnPrevious
            btnPrevious.Location = new Point(12, 15);
            btnPrevious.Name = "btnPrevious";
            btnPrevious.Size = new Size(100, 30);
            btnPrevious.TabIndex = 0;
            btnPrevious.Text = "← Назад";
            btnPrevious.UseVisualStyleBackColor = true;
            btnPrevious.Click += BtnPrevious_Click;

            // btnNext
            btnNext.BackColor = Color.FromArgb(76, 175, 80);
            btnNext.ForeColor = Color.White;
            btnNext.Location = new Point(688, 15);
            btnNext.Name = "btnNext";
            btnNext.Size = new Size(100, 30);
            btnNext.TabIndex = 1;
            btnNext.Text = "Далее →";
            btnNext.UseVisualStyleBackColor = false;
            btnNext.Click += BtnNext_Click;

            // btnCancel
            btnCancel.Location = new Point(350, 15);
            btnCancel.Name = "btnCancel";
            btnCancel.Size = new Size(100, 30);
            btnCancel.TabIndex = 2;
            btnCancel.Text = "Прервать";
            btnCancel.UseVisualStyleBackColor = true;
            btnCancel.Click += BtnCancel_Click;

            // TestTakingForm
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 540);
            Controls.Add(panelQuestion);
            Controls.Add(panelBottom);
            Controls.Add(panelTop);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            Name = "TestTakingForm";
            StartPosition = FormStartPosition.CenterParent;
            Text = "Прохождение теста";
            panelTop.ResumeLayout(false);
            panelTop.PerformLayout();
            panelQuestion.ResumeLayout(false);
            panelQuestion.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBoxQuestion).EndInit();
            panelBottom.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private Panel panelTop;
        private Label lblTestTitle;
        private Label lblTestDescription;
        private Panel panelQuestion;
        private Label lblQuestionCounter;
        private Label lblQuestionText;
        private PictureBox pictureBoxQuestion;
        private Panel pnlAnswers;
        private Panel panelBottom;
        private Button btnPrevious;
        private Button btnNext;
        private Button btnCancel;
    }
}