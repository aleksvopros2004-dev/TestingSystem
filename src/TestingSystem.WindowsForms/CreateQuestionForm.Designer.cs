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
            scrollPanel = new Panel();
            contentPanel = new Panel();
            lblOptionsCounter = new Label();
            lblTitle = new Label();
            lblType = new Label();
            cmbType = new ComboBox();
            lblText = new Label();
            txtQuestion = new TextBox();
            lblPoints = new Label();
            numPoints = new NumericUpDown();
            lblPointsInfo = new Label();
            lblImage = new Label();
            btnLoadImage = new Button();
            btnRemoveImage = new Button();
            lblImageInfo = new Label();
            pictureBox = new PictureBox();
            pnlAnswers = new Panel();
            buttonPanel = new Panel();
            btnAddOption = new Button();
            btnSave = new Button();
            btnCancel = new Button();
            lblMessage = new Label();
            scrollPanel.SuspendLayout();
            contentPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)numPoints).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox).BeginInit();
            buttonPanel.SuspendLayout();
            SuspendLayout();
            // 
            // scrollPanel
            // 
            scrollPanel.AutoScroll = true;
            scrollPanel.Controls.Add(contentPanel);
            scrollPanel.Dock = DockStyle.Fill;
            scrollPanel.Location = new Point(0, 0);
            scrollPanel.Margin = new Padding(3, 2, 3, 2);
            scrollPanel.Name = "scrollPanel";
            scrollPanel.Size = new Size(700, 450);
            scrollPanel.TabIndex = 0;
            // 
            // contentPanel
            // 
            contentPanel.Controls.Add(lblOptionsCounter);
            contentPanel.Controls.Add(lblTitle);
            contentPanel.Controls.Add(lblType);
            contentPanel.Controls.Add(cmbType);
            contentPanel.Controls.Add(lblText);
            contentPanel.Controls.Add(txtQuestion);
            contentPanel.Controls.Add(lblPoints);
            contentPanel.Controls.Add(numPoints);
            contentPanel.Controls.Add(lblPointsInfo);
            contentPanel.Controls.Add(lblImage);
            contentPanel.Controls.Add(btnLoadImage);
            contentPanel.Controls.Add(btnRemoveImage);
            contentPanel.Controls.Add(lblImageInfo);
            contentPanel.Controls.Add(pictureBox);
            contentPanel.Controls.Add(pnlAnswers);
            contentPanel.Controls.Add(buttonPanel);
            contentPanel.Controls.Add(lblMessage);
            contentPanel.Location = new Point(0, 0);
            contentPanel.Margin = new Padding(3, 2, 3, 2);
            contentPanel.Name = "contentPanel";
            contentPanel.Size = new Size(662, 594);
            contentPanel.TabIndex = 0;
            // 
            // lblOptionsCounter
            // 
            lblOptionsCounter.AutoSize = true;
            lblOptionsCounter.Font = new Font("Arial", 9F);
            lblOptionsCounter.ForeColor = Color.Gray;
            lblOptionsCounter.Location = new Point(500, 299);
            lblOptionsCounter.Name = "lblOptionsCounter";
            lblOptionsCounter.Size = new Size(98, 15);
            lblOptionsCounter.TabIndex = 17;
            lblOptionsCounter.Text = "Вариантов: 0/10";
            // 
            // lblTitle
            // 
            lblTitle.AutoSize = true;
            lblTitle.Font = new Font("Arial", 12F, FontStyle.Bold);
            lblTitle.Location = new Point(16, 11);
            lblTitle.Name = "lblTitle";
            lblTitle.Size = new Size(324, 19);
            lblTitle.TabIndex = 0;
            lblTitle.Text = "Добавление вопроса к тесту: {TestTitle}";
            // 
            // lblType
            // 
            lblType.AutoSize = true;
            lblType.Location = new Point(16, 34);
            lblType.Name = "lblType";
            lblType.Size = new Size(79, 15);
            lblType.TabIndex = 1;
            lblType.Text = "Тип вопроса:";
            // 
            // cmbType
            // 
            cmbType.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbType.FormattingEnabled = true;
            cmbType.Items.AddRange(new object[] { "Один вариант", "Несколько вариантов", "Текстовый ответ" });
            cmbType.Location = new Point(105, 34);
            cmbType.Margin = new Padding(3, 2, 3, 2);
            cmbType.Name = "cmbType";
            cmbType.Size = new Size(176, 23);
            cmbType.TabIndex = 2;
            cmbType.SelectedIndexChanged += CmbType_SelectedIndexChanged;
            // 
            // lblText
            // 
            lblText.AutoSize = true;
            lblText.Location = new Point(16, 56);
            lblText.Name = "lblText";
            lblText.Size = new Size(88, 15);
            lblText.TabIndex = 3;
            lblText.Text = "Текст вопроса:";
            // 
            // txtQuestion
            // 
            txtQuestion.Location = new Point(105, 61);
            txtQuestion.Margin = new Padding(3, 2, 3, 2);
            txtQuestion.Multiline = true;
            txtQuestion.Name = "txtQuestion";
            txtQuestion.ScrollBars = ScrollBars.Vertical;
            txtQuestion.Size = new Size(526, 61);
            txtQuestion.TabIndex = 4;
            // 
            // lblPoints
            // 
            lblPoints.AutoSize = true;
            lblPoints.Location = new Point(16, 128);
            lblPoints.Name = "lblPoints";
            lblPoints.Size = new Size(80, 15);
            lblPoints.TabIndex = 5;
            lblPoints.Text = "Баллы (1-10):";
            // 
            // numPoints
            // 
            numPoints.Location = new Point(105, 126);
            numPoints.Margin = new Padding(3, 2, 3, 2);
            numPoints.Maximum = new decimal(new int[] { 10, 0, 0, 0 });
            numPoints.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            numPoints.Name = "numPoints";
            numPoints.Size = new Size(52, 23);
            numPoints.TabIndex = 6;
            numPoints.Value = new decimal(new int[] { 1, 0, 0, 0 });
            // 
            // lblPointsInfo
            // 
            lblPointsInfo.AutoSize = true;
            lblPointsInfo.Font = new Font("Arial", 8F);
            lblPointsInfo.ForeColor = Color.Gray;
            lblPointsInfo.Location = new Point(163, 129);
            lblPointsInfo.Name = "lblPointsInfo";
            lblPointsInfo.Size = new Size(223, 14);
            lblPointsInfo.TabIndex = 7;
            lblPointsInfo.Text = "Количество баллов за правильный ответ";
            // 
            // lblImage
            // 
            lblImage.AutoSize = true;
            lblImage.Location = new Point(16, 159);
            lblImage.Name = "lblImage";
            lblImage.Size = new Size(86, 15);
            lblImage.TabIndex = 8;
            lblImage.Text = "Изображение:";
            // 
            // btnLoadImage
            // 
            btnLoadImage.Location = new Point(105, 153);
            btnLoadImage.Margin = new Padding(3, 2, 3, 2);
            btnLoadImage.Name = "btnLoadImage";
            btnLoadImage.Size = new Size(131, 26);
            btnLoadImage.TabIndex = 9;
            btnLoadImage.Text = "Загрузить изображение";
            btnLoadImage.UseVisualStyleBackColor = true;
            btnLoadImage.Click += BtnLoadImage_Click;
            // 
            // btnRemoveImage
            // 
            btnRemoveImage.Enabled = false;
            btnRemoveImage.Location = new Point(241, 153);
            btnRemoveImage.Margin = new Padding(3, 2, 3, 2);
            btnRemoveImage.Name = "btnRemoveImage";
            btnRemoveImage.Size = new Size(70, 26);
            btnRemoveImage.TabIndex = 10;
            btnRemoveImage.Text = "Удалить";
            btnRemoveImage.UseVisualStyleBackColor = true;
            btnRemoveImage.Click += BtnRemoveImage_Click;
            // 
            // lblImageInfo
            // 
            lblImageInfo.AutoSize = true;
            lblImageInfo.Font = new Font("Arial", 8F);
            lblImageInfo.ForeColor = Color.Gray;
            lblImageInfo.Location = new Point(317, 160);
            lblImageInfo.Name = "lblImageInfo";
            lblImageInfo.Size = new Size(154, 14);
            lblImageInfo.TabIndex = 11;
            lblImageInfo.Text = "Максимальный размер: 5 MB";
            lblImageInfo.Click += lblImageInfo_Click;
            // 
            // pictureBox
            // 
            pictureBox.BorderStyle = BorderStyle.FixedSingle;
            pictureBox.Location = new Point(105, 183);
            pictureBox.Margin = new Padding(3, 2, 3, 2);
            pictureBox.Name = "pictureBox";
            pictureBox.Size = new Size(306, 114);
            pictureBox.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox.TabIndex = 12;
            pictureBox.TabStop = false;
            pictureBox.Visible = false;
            // 
            // pnlAnswers
            // 
            pnlAnswers.AutoScroll = true;
            pnlAnswers.BorderStyle = BorderStyle.FixedSingle;
            pnlAnswers.Location = new Point(16, 320);
            pnlAnswers.Margin = new Padding(3, 2, 3, 2);
            pnlAnswers.Name = "pnlAnswers";
            pnlAnswers.Size = new Size(630, 150);
            pnlAnswers.TabIndex = 13;
            // 
            // buttonPanel
            // 
            buttonPanel.BackColor = SystemColors.Control;
            buttonPanel.Controls.Add(btnAddOption);
            buttonPanel.Controls.Add(btnSave);
            buttonPanel.Controls.Add(btnCancel);
            buttonPanel.Location = new Point(0, 483);
            buttonPanel.Margin = new Padding(3, 2, 3, 2);
            buttonPanel.Name = "buttonPanel";
            buttonPanel.Size = new Size(662, 38);
            buttonPanel.TabIndex = 14;
            // 
            // btnAddOption
            // 
            btnAddOption.Location = new Point(16, 9);
            btnAddOption.Margin = new Padding(3, 2, 3, 2);
            btnAddOption.Name = "btnAddOption";
            btnAddOption.Size = new Size(131, 22);
            btnAddOption.TabIndex = 0;
            btnAddOption.Text = "Добавить вариант";
            btnAddOption.UseVisualStyleBackColor = true;
            btnAddOption.Click += BtnAddOption_Click;
            // 
            // btnSave
            // 
            btnSave.Location = new Point(350, 9);
            btnSave.Margin = new Padding(3, 2, 3, 2);
            btnSave.Name = "btnSave";
            btnSave.Size = new Size(105, 22);
            btnSave.TabIndex = 1;
            btnSave.Text = "Сохранить";
            btnSave.UseVisualStyleBackColor = true;
            btnSave.Click += BtnSave_Click;
            // 
            // btnCancel
            // 
            btnCancel.Location = new Point(464, 9);
            btnCancel.Margin = new Padding(3, 2, 3, 2);
            btnCancel.Name = "btnCancel";
            btnCancel.Size = new Size(105, 22);
            btnCancel.TabIndex = 2;
            btnCancel.Text = "Отмена";
            btnCancel.UseVisualStyleBackColor = true;
            btnCancel.Click += BtnCancel_Click;
            // 
            // lblMessage
            // 
            lblMessage.ForeColor = Color.Red;
            lblMessage.Location = new Point(16, 533);
            lblMessage.Name = "lblMessage";
            lblMessage.Size = new Size(630, 22);
            lblMessage.TabIndex = 15;
            lblMessage.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // CreateQuestionForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(700, 450);
            Controls.Add(scrollPanel);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            Margin = new Padding(3, 2, 3, 2);
            MaximizeBox = false;
            Name = "CreateQuestionForm";
            StartPosition = FormStartPosition.CenterParent;
            Text = "Добавление вопроса";
            scrollPanel.ResumeLayout(false);
            contentPanel.ResumeLayout(false);
            contentPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)numPoints).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox).EndInit();
            buttonPanel.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private Panel scrollPanel;
        private Panel contentPanel;
        private Label lblTitle;
        private Label lblType;
        private ComboBox cmbType;
        private Label lblText;
        private TextBox txtQuestion;
        private Label lblPoints;
        private NumericUpDown numPoints;
        private Label lblPointsInfo;
        private Label lblImage;
        private Button btnLoadImage;
        private Button btnRemoveImage;
        private Label lblImageInfo;
        private PictureBox pictureBox;
        private Panel pnlAnswers;
        private Button btnAddOption;
        private Button btnSave;
        private Button btnCancel;
        private Label lblMessage;
        private Panel buttonPanel;
        private Label lblOptionsCounter;
    }
}