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
            txtType = new TextBox();
            lblText = new Label();
            txtQuestion = new TextBox();
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
            scrollPanel.SuspendLayout();
            contentPanel.SuspendLayout();
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
            scrollPanel.Size = new Size(700, 525);
            scrollPanel.TabIndex = 0;
            // 
            // contentPanel
            // 
            contentPanel.Controls.Add(lblTitle);
            contentPanel.Controls.Add(lblType);
            contentPanel.Controls.Add(txtType);
            contentPanel.Controls.Add(lblText);
            contentPanel.Controls.Add(txtQuestion);
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
            contentPanel.Location = new Point(0, 0);
            contentPanel.Margin = new Padding(3, 2, 3, 2);
            contentPanel.Name = "contentPanel";
            contentPanel.Size = new Size(656, 582);
            contentPanel.TabIndex = 0;
            // 
            // lblTitle
            // 
            lblTitle.AutoSize = true;
            lblTitle.Font = new Font("Arial", 12F, FontStyle.Bold);
            lblTitle.Location = new Point(18, 15);
            lblTitle.Name = "lblTitle";
            lblTitle.Size = new Size(214, 19);
            lblTitle.TabIndex = 0;
            lblTitle.Text = "Редактирование вопроса";
            lblTitle.TextAlign = ContentAlignment.MiddleCenter;
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
            // txtType
            // 
            txtType.BackColor = SystemColors.Control;
            txtType.Location = new Point(114, 45);
            txtType.Margin = new Padding(3, 2, 3, 2);
            txtType.Name = "txtType";
            txtType.ReadOnly = true;
            txtType.Size = new Size(176, 23);
            txtType.TabIndex = 2;
            // 
            // lblText
            // 
            lblText.AutoSize = true;
            lblText.Location = new Point(18, 75);
            lblText.Name = "lblText";
            lblText.Size = new Size(88, 15);
            lblText.TabIndex = 3;
            lblText.Text = "Текст вопроса:";
            // 
            // txtQuestion
            // 
            txtQuestion.Location = new Point(114, 75);
            txtQuestion.Margin = new Padding(3, 2, 3, 2);
            txtQuestion.Multiline = true;
            txtQuestion.Name = "txtQuestion";
            txtQuestion.ScrollBars = ScrollBars.Vertical;
            txtQuestion.Size = new Size(482, 61);
            txtQuestion.TabIndex = 4;
            // 
            // lblOrder
            // 
            lblOrder.AutoSize = true;
            lblOrder.Location = new Point(18, 146);
            lblOrder.Name = "lblOrder";
            lblOrder.Size = new Size(119, 15);
            lblOrder.TabIndex = 5;
            lblOrder.Text = "Порядковый номер:";
            // 
            // txtOrder
            // 
            txtOrder.BackColor = SystemColors.Control;
            txtOrder.Location = new Point(138, 146);
            txtOrder.Margin = new Padding(3, 2, 3, 2);
            txtOrder.Name = "txtOrder";
            txtOrder.ReadOnly = true;
            txtOrder.Size = new Size(88, 23);
            txtOrder.TabIndex = 6;
            // 
            // lblImage
            // 
            lblImage.AutoSize = true;
            lblImage.Location = new Point(18, 172);
            lblImage.Name = "lblImage";
            lblImage.Size = new Size(86, 15);
            lblImage.TabIndex = 7;
            lblImage.Text = "Изображение:";
            // 
            // btnLoadImage
            // 
            btnLoadImage.Location = new Point(114, 172);
            btnLoadImage.Margin = new Padding(3, 2, 3, 2);
            btnLoadImage.Name = "btnLoadImage";
            btnLoadImage.Size = new Size(131, 23);
            btnLoadImage.TabIndex = 8;
            btnLoadImage.Text = "Загрузить изображение";
            btnLoadImage.UseVisualStyleBackColor = true;
            btnLoadImage.Click += BtnLoadImage_Click;
            // 
            // btnRemoveImage
            // 
            btnRemoveImage.Enabled = false;
            btnRemoveImage.Location = new Point(254, 172);
            btnRemoveImage.Margin = new Padding(3, 2, 3, 2);
            btnRemoveImage.Name = "btnRemoveImage";
            btnRemoveImage.Size = new Size(70, 23);
            btnRemoveImage.TabIndex = 9;
            btnRemoveImage.Text = "Удалить";
            btnRemoveImage.UseVisualStyleBackColor = true;
            btnRemoveImage.Click += BtnRemoveImage_Click;
            // 
            // pictureBox
            // 
            pictureBox.BorderStyle = BorderStyle.FixedSingle;
            pictureBox.Location = new Point(114, 199);
            pictureBox.Margin = new Padding(3, 2, 3, 2);
            pictureBox.Name = "pictureBox";
            pictureBox.Size = new Size(350, 150);
            pictureBox.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox.TabIndex = 10;
            pictureBox.TabStop = false;
            pictureBox.Visible = false;
            // 
            // lblAnswers
            // 
            lblAnswers.AutoSize = true;
            lblAnswers.Font = new Font("Arial", 9F, FontStyle.Bold);
            lblAnswers.Location = new Point(18, 360);
            lblAnswers.Name = "lblAnswers";
            lblAnswers.Size = new Size(116, 15);
            lblAnswers.TabIndex = 11;
            lblAnswers.Text = "Варианты ответов:";
            lblAnswers.Visible = false;
            // 
            // pnlAnswers
            // 
            pnlAnswers.AutoScroll = true;
            pnlAnswers.BorderStyle = BorderStyle.FixedSingle;
            pnlAnswers.Location = new Point(18, 382);
            pnlAnswers.Margin = new Padding(3, 2, 3, 2);
            pnlAnswers.Name = "pnlAnswers";
            pnlAnswers.Size = new Size(622, 150);
            pnlAnswers.TabIndex = 12;
            pnlAnswers.Visible = false;
            // 
            // btnAddOption
            // 
            btnAddOption.Location = new Point(18, 540);
            btnAddOption.Margin = new Padding(3, 2, 3, 2);
            btnAddOption.Name = "btnAddOption";
            btnAddOption.Size = new Size(119, 26);
            btnAddOption.TabIndex = 13;
            btnAddOption.Text = "Добавить вариант";
            btnAddOption.UseVisualStyleBackColor = true;
            btnAddOption.Visible = false;
            btnAddOption.Click += BtnAddOption_Click;
            // 
            // btnSave
            // 
            btnSave.Location = new Point(350, 540);
            btnSave.Margin = new Padding(3, 2, 3, 2);
            btnSave.Name = "btnSave";
            btnSave.Size = new Size(105, 26);
            btnSave.TabIndex = 14;
            btnSave.Text = "Сохранить";
            btnSave.UseVisualStyleBackColor = true;
            btnSave.Click += BtnSave_Click;
            // 
            // btnCancel
            // 
            btnCancel.Location = new Point(464, 540);
            btnCancel.Margin = new Padding(3, 2, 3, 2);
            btnCancel.Name = "btnCancel";
            btnCancel.Size = new Size(105, 26);
            btnCancel.TabIndex = 15;
            btnCancel.Text = "Отмена";
            btnCancel.UseVisualStyleBackColor = true;
            btnCancel.Click += BtnCancel_Click;
            // 
            // lblMessage
            // 
            lblMessage.ForeColor = Color.Red;
            lblMessage.Location = new Point(18, 578);
            lblMessage.Name = "lblMessage";
            lblMessage.Size = new Size(612, 30);
            lblMessage.TabIndex = 16;
            lblMessage.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // EditQuestionForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(700, 525);
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
    }
}