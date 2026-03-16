namespace TestingSystem.WindowsForms
{
    partial class EditQuestionForm
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
            lblTitle = new Label();
            lblType = new Label();
            lblText = new Label();
            txtQuestion = new TextBox();
            lblPoints = new Label();
            numPoints = new NumericUpDown();
            lblPointsInfo = new Label();
            lblOrder = new Label();
            txtOrder = new TextBox();
            lblImage = new Label();
            btnLoadImage = new Button();
            btnRemoveImage = new Button();
            pictureBox = new PictureBox();
            lblAnswers = new Label();
            pnlAnswers = new Panel();
            btnAddOption = new Button();
            btnSave = new Button();
            btnCancel = new Button();
            lblMessage = new Label();
            cmbType = new ComboBox();
            txtTypeDisplay = new TextBox();
            scrollPanel.SuspendLayout();
            contentPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)numPoints).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox).BeginInit();
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
            scrollPanel.Size = new Size(700, 440);
            scrollPanel.TabIndex = 0;
            // 
            // contentPanel
            // 
            contentPanel.Controls.Add(lblTitle);
            contentPanel.Controls.Add(lblType);
            contentPanel.Controls.Add(lblText);
            contentPanel.Controls.Add(txtQuestion);
            contentPanel.Controls.Add(lblPoints);
            contentPanel.Controls.Add(numPoints);
            contentPanel.Controls.Add(lblPointsInfo);
            contentPanel.Controls.Add(lblOrder);
            contentPanel.Controls.Add(txtOrder);
            contentPanel.Controls.Add(lblImage);
            contentPanel.Controls.Add(btnLoadImage);
            contentPanel.Controls.Add(btnRemoveImage);
            contentPanel.Controls.Add(pictureBox);
            contentPanel.Controls.Add(lblAnswers);
            contentPanel.Controls.Add(pnlAnswers);
            contentPanel.Controls.Add(btnAddOption);
            contentPanel.Controls.Add(btnSave);
            contentPanel.Controls.Add(btnCancel);
            contentPanel.Controls.Add(lblMessage);
            contentPanel.Controls.Add(cmbType);
            contentPanel.Controls.Add(txtTypeDisplay);
            contentPanel.Location = new Point(0, 0);
            contentPanel.Margin = new Padding(3, 2, 3, 2);
            contentPanel.Name = "contentPanel";
            contentPanel.Size = new Size(662, 531);
            contentPanel.TabIndex = 0;
            // 
            // lblTitle
            // 
            lblTitle.AutoSize = true;
            lblTitle.Font = new Font("Arial", 12F, FontStyle.Bold);
            lblTitle.Location = new Point(16, 11);
            lblTitle.Name = "lblTitle";
            lblTitle.Size = new Size(214, 19);
            lblTitle.TabIndex = 0;
            lblTitle.Text = "Редактирование вопроса";
            // 
            // lblType
            // 
            lblType.AutoSize = true;
            lblType.Location = new Point(18, 45);
            lblType.Name = "lblType";
            lblType.Size = new Size(79, 15);
            lblType.TabIndex = 1;
            lblType.Text = "Тип вопроса:";
            // 
            // lblText
            // 
            lblText.AutoSize = true;
            lblText.Location = new Point(16, 72);
            lblText.Name = "lblText";
            lblText.Size = new Size(88, 15);
            lblText.TabIndex = 3;
            lblText.Text = "Текст вопроса:";
            // 
            // txtQuestion
            // 
            txtQuestion.Location = new Point(105, 74);
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
            lblPoints.Location = new Point(16, 140);
            lblPoints.Name = "lblPoints";
            lblPoints.Size = new Size(80, 15);
            lblPoints.TabIndex = 5;
            lblPoints.Text = "Баллы (1-10):";
            // 
            // numPoints
            // 
            numPoints.Location = new Point(105, 138);
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
            lblPointsInfo.Location = new Point(163, 141);
            lblPointsInfo.Name = "lblPointsInfo";
            lblPointsInfo.Size = new Size(223, 14);
            lblPointsInfo.TabIndex = 7;
            lblPointsInfo.Text = "Количество баллов за правильный ответ";
            // 
            // lblOrder
            // 
            lblOrder.AutoSize = true;
            lblOrder.Location = new Point(16, 168);
            lblOrder.Name = "lblOrder";
            lblOrder.Size = new Size(119, 15);
            lblOrder.TabIndex = 8;
            lblOrder.Text = "Порядковый номер:";
            // 
            // txtOrder
            // 
            txtOrder.BackColor = SystemColors.Control;
            txtOrder.Location = new Point(141, 165);
            txtOrder.Margin = new Padding(3, 2, 3, 2);
            txtOrder.Name = "txtOrder";
            txtOrder.ReadOnly = true;
            txtOrder.Size = new Size(78, 23);
            txtOrder.TabIndex = 9;
            // 
            // lblImage
            // 
            lblImage.AutoSize = true;
            lblImage.Location = new Point(16, 197);
            lblImage.Name = "lblImage";
            lblImage.Size = new Size(86, 15);
            lblImage.TabIndex = 10;
            lblImage.Text = "Изображение:";
            // 
            // btnLoadImage
            // 
            btnLoadImage.Location = new Point(107, 193);
            btnLoadImage.Margin = new Padding(3, 2, 3, 2);
            btnLoadImage.Name = "btnLoadImage";
            btnLoadImage.Size = new Size(131, 22);
            btnLoadImage.TabIndex = 11;
            btnLoadImage.Text = "Загрузить изображение";
            btnLoadImage.UseVisualStyleBackColor = true;
            btnLoadImage.Click += BtnLoadImage_Click;
            // 
            // btnRemoveImage
            // 
            btnRemoveImage.Enabled = false;
            btnRemoveImage.Location = new Point(243, 193);
            btnRemoveImage.Margin = new Padding(3, 2, 3, 2);
            btnRemoveImage.Name = "btnRemoveImage";
            btnRemoveImage.Size = new Size(70, 22);
            btnRemoveImage.TabIndex = 12;
            btnRemoveImage.Text = "Удалить";
            btnRemoveImage.UseVisualStyleBackColor = true;
            btnRemoveImage.Click += BtnRemoveImage_Click;
            // 
            // pictureBox
            // 
            pictureBox.BorderStyle = BorderStyle.FixedSingle;
            pictureBox.Location = new Point(107, 219);
            pictureBox.Margin = new Padding(3, 2, 3, 2);
            pictureBox.Name = "pictureBox";
            pictureBox.Size = new Size(306, 113);
            pictureBox.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox.TabIndex = 13;
            pictureBox.TabStop = false;
            pictureBox.Visible = false;
            // 
            // lblAnswers
            // 
            lblAnswers.AutoSize = true;
            lblAnswers.Font = new Font("Arial", 9F, FontStyle.Bold);
            lblAnswers.Location = new Point(16, 339);
            lblAnswers.Name = "lblAnswers";
            lblAnswers.Size = new Size(116, 15);
            lblAnswers.TabIndex = 14;
            lblAnswers.Text = "Варианты ответов:";
            // 
            // pnlAnswers
            // 
            pnlAnswers.AutoScroll = true;
            pnlAnswers.BorderStyle = BorderStyle.FixedSingle;
            pnlAnswers.Location = new Point(16, 354);
            pnlAnswers.Margin = new Padding(3, 2, 3, 2);
            pnlAnswers.Name = "pnlAnswers";
            pnlAnswers.Size = new Size(630, 113);
            pnlAnswers.TabIndex = 15;
            pnlAnswers.Visible = false;
            // 
            // btnAddOption
            // 
            btnAddOption.Location = new Point(16, 474);
            btnAddOption.Margin = new Padding(3, 2, 3, 2);
            btnAddOption.Name = "btnAddOption";
            btnAddOption.Size = new Size(131, 22);
            btnAddOption.TabIndex = 16;
            btnAddOption.Text = "Добавить вариант";
            btnAddOption.UseVisualStyleBackColor = true;
            btnAddOption.Visible = false;
            btnAddOption.Click += BtnAddOption_Click;
            // 
            // btnSave
            // 
            btnSave.Location = new Point(350, 474);
            btnSave.Margin = new Padding(3, 2, 3, 2);
            btnSave.Name = "btnSave";
            btnSave.Size = new Size(105, 22);
            btnSave.TabIndex = 17;
            btnSave.Text = "Сохранить";
            btnSave.UseVisualStyleBackColor = true;
            btnSave.Click += BtnSave_Click;
            // 
            // btnCancel
            // 
            btnCancel.Location = new Point(464, 474);
            btnCancel.Margin = new Padding(3, 2, 3, 2);
            btnCancel.Name = "btnCancel";
            btnCancel.Size = new Size(105, 22);
            btnCancel.TabIndex = 18;
            btnCancel.Text = "Отмена";
            btnCancel.UseVisualStyleBackColor = true;
            btnCancel.Click += BtnCancel_Click;
            // 
            // lblMessage
            // 
            lblMessage.ForeColor = Color.Red;
            lblMessage.Location = new Point(16, 496);
            lblMessage.Name = "lblMessage";
            lblMessage.Size = new Size(630, 22);
            lblMessage.TabIndex = 19;
            lblMessage.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // cmbType
            // 
            cmbType.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbType.FormattingEnabled = true;
            cmbType.Location = new Point(105, 45);
            cmbType.Margin = new Padding(3, 2, 3, 2);
            cmbType.Name = "cmbType";
            cmbType.Size = new Size(200, 23);
            cmbType.TabIndex = 20;
            // 
            // txtTypeDisplay
            // 
            txtTypeDisplay.BackColor = SystemColors.Control;
            txtTypeDisplay.Location = new Point(114, 45);
            txtTypeDisplay.Margin = new Padding(3, 2, 3, 2);
            txtTypeDisplay.Name = "txtTypeDisplay";
            txtTypeDisplay.ReadOnly = true;
            txtTypeDisplay.Size = new Size(200, 23);
            txtTypeDisplay.TabIndex = 2;
            txtTypeDisplay.Visible = false;
            // 
            // EditQuestionForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(700, 440);
            Controls.Add(scrollPanel);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            Margin = new Padding(3, 2, 3, 2);
            MaximizeBox = false;
            Name = "EditQuestionForm";
            StartPosition = FormStartPosition.CenterParent;
            Text = "Редактирование вопроса";
            scrollPanel.ResumeLayout(false);
            contentPanel.ResumeLayout(false);
            contentPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)numPoints).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private Panel scrollPanel;
        private Panel contentPanel;
        private Label lblTitle;
        private Label lblType;
        private TextBox txtType;
        private Label lblText;
        private TextBox txtQuestion;
        private Label lblPoints;
        private NumericUpDown numPoints;
        private Label lblPointsInfo;
        private Label lblOrder;
        private TextBox txtOrder;
        private Label lblImage;
        private Button btnLoadImage;
        private Button btnRemoveImage;
        private PictureBox pictureBox;
        private Label lblAnswers;
        private Panel pnlAnswers;
        private Button btnAddOption;
        private Button btnSave;
        private Button btnCancel;
        private Label lblMessage;
        private TextBox txtTypeDisplay;
        private ComboBox cmbType;
    }
}