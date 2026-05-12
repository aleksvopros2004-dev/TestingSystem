namespace TestingSystem.WindowsForms
{
    partial class CreateQuestionForm
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
            lblTitle = new Label();
            lblType = new Label();
            cmbType = new ComboBox();
            lblText = new Label();
            txtQuestion = new TextBox();
            lblPoints = new Label();
            numPoints = new NumericUpDown();
            lblPointsInfo = new Label();

            lblRecommendation = new Label();
            txtRecommendation = new TextBox();
            lblRecommendationInfo = new Label();

            lblImage = new Label();
            btnLoadImage = new Button();
            btnRemoveImage = new Button();
            lblImageInfo = new Label();
            pictureBox = new PictureBox();
            lblAnswers = new Label();
            pnlAnswers = new Panel();
            lblOptionsCounter = new Label();
            buttonPanel = new FlowLayoutPanel();
            btnAddOption = new Button();
            btnSave = new Button();
            btnCancel = new Button();
            lblMessage = new Label();

            tableLayout.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)numPoints).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox).BeginInit();
            buttonPanel.SuspendLayout();
            SuspendLayout();

            // 
            // tableLayout
            // 
            tableLayout.ColumnCount = 3;
            tableLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 130F));
            tableLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tableLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 160F));
            tableLayout.Controls.Add(lblTitle, 0, 0);
            tableLayout.Controls.Add(lblType, 0, 1);
            tableLayout.Controls.Add(cmbType, 1, 1);
            tableLayout.Controls.Add(lblText, 0, 2);
            tableLayout.Controls.Add(txtQuestion, 1, 2);
            tableLayout.Controls.Add(lblPoints, 0, 3);
            tableLayout.Controls.Add(numPoints, 1, 3);
            tableLayout.Controls.Add(lblPointsInfo, 2, 3);
            tableLayout.Controls.Add(lblRecommendation, 0, 4);
            tableLayout.Controls.Add(txtRecommendation, 1, 4);
            tableLayout.Controls.Add(lblRecommendationInfo, 2, 4);
            tableLayout.Controls.Add(lblImage, 0, 5);
            tableLayout.Controls.Add(btnLoadImage, 1, 5);
            tableLayout.Controls.Add(btnRemoveImage, 2, 5);
            tableLayout.Controls.Add(lblImageInfo, 2, 6);
            tableLayout.Controls.Add(pictureBox, 1, 6);
            tableLayout.Controls.Add(lblAnswers, 0, 7);
            tableLayout.Controls.Add(pnlAnswers, 1, 7);
            tableLayout.Controls.Add(lblOptionsCounter, 2, 7);
            tableLayout.Controls.Add(buttonPanel, 0, 8);
            tableLayout.Controls.Add(lblMessage, 0, 9);
            tableLayout.Dock = DockStyle.Fill;
            tableLayout.Location = new Point(0, 0);
            tableLayout.Name = "tableLayout";
            tableLayout.Padding = new Padding(20);
            tableLayout.RowCount = 10;
            tableLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 50F));  
            tableLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 40F));  
            tableLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 100F)); 
            tableLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 40F));  
            tableLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 90F));  
            tableLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 40F));  
            tableLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 160F)); 
            tableLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 220F)); 
            tableLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 50F));  
            tableLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 80F));  
            tableLayout.Size = new Size(880, 900);
            tableLayout.TabIndex = 0;

            // 
            // lblTitle
            // 
            tableLayout.SetColumnSpan(lblTitle, 3);
            lblTitle.Dock = DockStyle.Fill;
            lblTitle.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            lblTitle.Location = new Point(23, 20);
            lblTitle.Name = "lblTitle";
            lblTitle.Size = new Size(834, 50);
            lblTitle.TabIndex = 0;
            lblTitle.Text = "Добавление вопроса";
            lblTitle.TextAlign = ContentAlignment.MiddleLeft;

            // 
            // lblType
            // 
            lblType.Dock = DockStyle.Fill;
            lblType.Location = new Point(23, 70);
            lblType.Name = "lblType";
            lblType.Size = new Size(124, 40);
            lblType.TabIndex = 1;
            lblType.Text = "Тип вопроса:";
            lblType.TextAlign = ContentAlignment.MiddleLeft;

            // 
            // cmbType
            // 
            tableLayout.SetColumnSpan(cmbType, 2);
            cmbType.Dock = DockStyle.Fill;
            cmbType.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbType.FormattingEnabled = true;
            cmbType.Items.AddRange(new object[] { "Один вариант", "Несколько вариантов", "Текстовый ответ" });
            cmbType.Location = new Point(153, 73);
            cmbType.Name = "cmbType";
            cmbType.Size = new Size(704, 25);
            cmbType.TabIndex = 2;
            cmbType.SelectedIndexChanged += CmbType_SelectedIndexChanged;

            // 
            // lblText
            // 
            lblText.Dock = DockStyle.Fill;
            lblText.Location = new Point(23, 110);
            lblText.Name = "lblText";
            lblText.Size = new Size(124, 100);
            lblText.TabIndex = 3;
            lblText.Text = "Текст вопроса:";
            lblText.TextAlign = ContentAlignment.MiddleLeft;

            // 
            // txtQuestion
            // 
            tableLayout.SetColumnSpan(txtQuestion, 2);
            txtQuestion.Dock = DockStyle.Fill;
            txtQuestion.Location = new Point(153, 113);
            txtQuestion.Multiline = true;
            txtQuestion.Name = "txtQuestion";
            txtQuestion.ScrollBars = ScrollBars.Vertical;
            txtQuestion.Size = new Size(704, 94);
            txtQuestion.TabIndex = 4;

            // 
            // lblPoints
            // 
            lblPoints.Dock = DockStyle.Fill;
            lblPoints.Location = new Point(23, 210);
            lblPoints.Name = "lblPoints";
            lblPoints.Size = new Size(124, 40);
            lblPoints.TabIndex = 5;
            lblPoints.Text = "Баллы (1-10):";
            lblPoints.TextAlign = ContentAlignment.MiddleLeft;

            // 
            // numPoints
            // 
            numPoints.Dock = DockStyle.Fill;
            numPoints.Location = new Point(153, 213);
            numPoints.Maximum = new decimal(new int[] { 10, 0, 0, 0 });
            numPoints.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            numPoints.Name = "numPoints";
            numPoints.Size = new Size(544, 25);
            numPoints.TabIndex = 6;
            numPoints.Value = new decimal(new int[] { 1, 0, 0, 0 });

            // 
            // lblPointsInfo
            // 
            lblPointsInfo.Dock = DockStyle.Fill;
            lblPointsInfo.ForeColor = Color.Gray;
            lblPointsInfo.Location = new Point(703, 210);
            lblPointsInfo.Name = "lblPointsInfo";
            lblPointsInfo.Size = new Size(154, 40);
            lblPointsInfo.TabIndex = 7;
            lblPointsInfo.Text = "Кол-во баллов";
            lblPointsInfo.TextAlign = ContentAlignment.MiddleLeft;
            // 
            //  lblRecommendation
            // 
            lblRecommendation.Dock = DockStyle.Fill;
            lblRecommendation.Location = new Point(23, 250);
            lblRecommendation.Name = "lblRecommendation";
            lblRecommendation.Size = new Size(124, 90);
            lblRecommendation.TabIndex = 20;
            lblRecommendation.Text = "Рекомендация при ошибке:";
            lblRecommendation.TextAlign = ContentAlignment.MiddleLeft;
            // 
            //  txtRecommendation
            // 
            tableLayout.SetColumnSpan(txtRecommendation, 2);
            txtRecommendation.Dock = DockStyle.Fill;
            txtRecommendation.Location = new Point(153, 253);
            txtRecommendation.Multiline = true;
            txtRecommendation.Name = "txtRecommendation";
            txtRecommendation.ScrollBars = ScrollBars.Vertical;
            txtRecommendation.Size = new Size(704, 84);
            txtRecommendation.TabIndex = 21;
            txtRecommendation.Text = "";

            // 
            // lblImage
            // 
            lblImage.Dock = DockStyle.Fill;
            lblImage.Location = new Point(23, 340);
            lblImage.Name = "lblImage";
            lblImage.Size = new Size(124, 40);
            lblImage.TabIndex = 8;
            lblImage.Text = "Изображение:";
            lblImage.TextAlign = ContentAlignment.MiddleLeft;

            // 
            // btnLoadImage
            // 
            btnLoadImage.Dock = DockStyle.Fill;
            btnLoadImage.Location = new Point(153, 343);
            btnLoadImage.Name = "btnLoadImage";
            btnLoadImage.Size = new Size(544, 34);
            btnLoadImage.TabIndex = 9;
            btnLoadImage.Text = "Загрузить изображение";
            btnLoadImage.UseVisualStyleBackColor = true;
            btnLoadImage.Click += BtnLoadImage_Click;

            // 
            // btnRemoveImage
            // 
            btnRemoveImage.Dock = DockStyle.Fill;
            btnRemoveImage.Enabled = false;
            btnRemoveImage.Location = new Point(703, 343);
            btnRemoveImage.Name = "btnRemoveImage";
            btnRemoveImage.Size = new Size(154, 34);
            btnRemoveImage.TabIndex = 10;
            btnRemoveImage.Text = "Удалить";
            btnRemoveImage.UseVisualStyleBackColor = true;
            btnRemoveImage.Click += BtnRemoveImage_Click;

            // 
            // lblImageInfo
            // 
            lblImageInfo.Dock = DockStyle.Fill;
            lblImageInfo.ForeColor = Color.Gray;
            lblImageInfo.Location = new Point(703, 380);
            lblImageInfo.Name = "lblImageInfo";
            lblImageInfo.Size = new Size(154, 160);
            lblImageInfo.TabIndex = 11;
            lblImageInfo.Text = "Максимальный размер: 5 MB";
            lblImageInfo.TextAlign = ContentAlignment.MiddleLeft;

            // 
            // pictureBox
            // 
            pictureBox.BorderStyle = BorderStyle.FixedSingle;
            pictureBox.Dock = DockStyle.Fill;
            pictureBox.Location = new Point(153, 383);
            pictureBox.Name = "pictureBox";
            pictureBox.Size = new Size(544, 154);
            pictureBox.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox.TabIndex = 12;
            pictureBox.TabStop = false;
            pictureBox.Visible = false;

            // 
            // lblAnswers
            // 
            lblAnswers.Dock = DockStyle.Fill;
            lblAnswers.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblAnswers.Location = new Point(23, 540);
            lblAnswers.Name = "lblAnswers";
            lblAnswers.Size = new Size(124, 220);
            lblAnswers.TabIndex = 13;
            lblAnswers.Text = "Варианты ответов:";
            lblAnswers.TextAlign = ContentAlignment.MiddleLeft;

            // 
            // pnlAnswers
            // 
            pnlAnswers.AutoScroll = true;
            pnlAnswers.BorderStyle = BorderStyle.FixedSingle;
            pnlAnswers.Dock = DockStyle.Fill;
            pnlAnswers.Location = new Point(153, 543);
            pnlAnswers.Name = "pnlAnswers";
            pnlAnswers.Size = new Size(544, 214);
            pnlAnswers.TabIndex = 14;

            // 
            // lblOptionsCounter
            // 
            lblOptionsCounter.Dock = DockStyle.Fill;
            lblOptionsCounter.ForeColor = Color.Gray;
            lblOptionsCounter.Location = new Point(703, 540);
            lblOptionsCounter.Name = "lblOptionsCounter";
            lblOptionsCounter.Size = new Size(154, 220);
            lblOptionsCounter.TabIndex = 15;
            lblOptionsCounter.Text = "Вариантов: 0/10";
            lblOptionsCounter.TextAlign = ContentAlignment.MiddleLeft;

            // 
            // buttonPanel
            // 
            tableLayout.SetColumnSpan(buttonPanel, 3);
            buttonPanel.Controls.Add(btnAddOption);
            buttonPanel.Controls.Add(btnSave);
            buttonPanel.Controls.Add(btnCancel);
            buttonPanel.Dock = DockStyle.Fill;
            buttonPanel.FlowDirection = FlowDirection.RightToLeft;
            buttonPanel.Location = new Point(23, 763);
            buttonPanel.Name = "buttonPanel";
            buttonPanel.Size = new Size(834, 44);
            buttonPanel.TabIndex = 16;

            // 
            // btnAddOption
            // 
            btnAddOption.AutoSize = true;
            btnAddOption.Location = new Point(696, 3);
            btnAddOption.Name = "btnAddOption";
            btnAddOption.Size = new Size(135, 29);
            btnAddOption.TabIndex = 2;
            btnAddOption.Text = "Добавить вариант";
            btnAddOption.UseVisualStyleBackColor = true;
            btnAddOption.Click += BtnAddOption_Click;

            // 
            // btnSave
            // 
            btnSave.AutoSize = true;
            btnSave.BackColor = Color.FromArgb(0, 120, 215);
            btnSave.FlatAppearance.BorderSize = 0;
            btnSave.FlatStyle = FlatStyle.Flat;
            btnSave.ForeColor = Color.White;
            btnSave.Location = new Point(604, 3);
            btnSave.Name = "btnSave";
            btnSave.Size = new Size(86, 29);
            btnSave.TabIndex = 0;
            btnSave.Text = "Сохранить";
            btnSave.UseVisualStyleBackColor = false;
            btnSave.Click += BtnSave_Click;

            // 
            // btnCancel
            // 
            btnCancel.AutoSize = true;
            btnCancel.Location = new Point(523, 3);
            btnCancel.Name = "btnCancel";
            btnCancel.Size = new Size(75, 29);
            btnCancel.TabIndex = 1;
            btnCancel.Text = "Отмена";
            btnCancel.Click += BtnCancel_Click;

            // 
            // lblMessage
            // 
            tableLayout.SetColumnSpan(lblMessage, 3);
            lblMessage.Dock = DockStyle.Fill;
            lblMessage.ForeColor = Color.Red;
            lblMessage.Location = new Point(23, 810);
            lblMessage.Name = "lblMessage";
            lblMessage.Size = new Size(834, 70);
            lblMessage.TabIndex = 17;
            lblMessage.TextAlign = ContentAlignment.MiddleCenter;

            // 
            // CreateQuestionForm
            // 
            BackColor = Color.White;
            ClientSize = new Size(880, 900);
            Controls.Add(tableLayout);
            Font = new Font("Segoe UI", 10F);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimumSize = new Size(880, 900);
            Name = "CreateQuestionForm";
            StartPosition = FormStartPosition.CenterParent;
            Text = "Создание вопроса";

            tableLayout.ResumeLayout(false);
            tableLayout.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)numPoints).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox).EndInit();
            buttonPanel.ResumeLayout(false);
            buttonPanel.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private TableLayoutPanel tableLayout;
        private Label lblTitle;
        private Label lblType;
        private ComboBox cmbType;
        private Label lblText;
        private TextBox txtQuestion;
        private Label lblPoints;
        private NumericUpDown numPoints;
        private Label lblPointsInfo;
        private Label lblRecommendation;
        private TextBox txtRecommendation;
        private Label lblRecommendationInfo;
        private Label lblImage;
        private Button btnLoadImage;
        private Button btnRemoveImage;
        private Label lblImageInfo;
        private PictureBox pictureBox;
        private Panel pnlAnswers;
        private Label lblAnswers;
        private Label lblOptionsCounter;
        private FlowLayoutPanel buttonPanel;
        private Button btnAddOption;
        private Button btnSave;
        private Button btnCancel;
        private Label lblMessage;
    }
}