namespace TestingSystem.WindowsForms
{
    partial class CreateTestForm
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
            lblTitle = new Label();
            lblTitleField = new Label();
            txtTitle = new TextBox();
            lblDescription = new Label();
            txtDescription = new TextBox();
            lblTimeLimit = new Label();
            numHours = new NumericUpDown();
            lblHours = new Label();
            numMinutes = new NumericUpDown();
            lblMinutes = new Label();
            chkRandomQuestions = new CheckBox();
            chkRandomAnswers = new CheckBox();
            chkActive = new CheckBox();
            lblLimitsInfo = new Label();
            btnCreate = new Button();
            btnCancel = new Button();
            lblMessage = new Label();
            ((System.ComponentModel.ISupportInitialize)numHours).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numMinutes).BeginInit();
            SuspendLayout();
            // 
            // lblTitle
            // 
            lblTitle.AutoSize = true;
            lblTitle.Font = new Font("Arial", 12F, FontStyle.Bold);
            lblTitle.Location = new Point(18, 15);
            lblTitle.Name = "lblTitle";
            lblTitle.Size = new Size(195, 19);
            lblTitle.TabIndex = 0;
            lblTitle.Text = "Создание нового теста";
            lblTitle.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // lblTitleField
            // 
            lblTitleField.AutoSize = true;
            lblTitleField.Location = new Point(18, 45);
            lblTitleField.Name = "lblTitleField";
            lblTitleField.Size = new Size(93, 15);
            lblTitleField.TabIndex = 1;
            lblTitleField.Text = "Название теста:";
            // 
            // txtTitle
            // 
            txtTitle.Location = new Point(158, 45);
            txtTitle.Margin = new Padding(3, 2, 3, 2);
            txtTitle.Name = "txtTitle";
            txtTitle.Size = new Size(246, 23);
            txtTitle.TabIndex = 2;
            // 
            // lblDescription
            // 
            lblDescription.AutoSize = true;
            lblDescription.Location = new Point(18, 68);
            lblDescription.Name = "lblDescription";
            lblDescription.Size = new Size(65, 15);
            lblDescription.TabIndex = 3;
            lblDescription.Text = "Описание:";
            // 
            // txtDescription
            // 
            txtDescription.Location = new Point(158, 68);
            txtDescription.Margin = new Padding(3, 2, 3, 2);
            txtDescription.Multiline = true;
            txtDescription.Name = "txtDescription";
            txtDescription.ScrollBars = ScrollBars.Vertical;
            txtDescription.Size = new Size(246, 46);
            txtDescription.TabIndex = 4;
            // 
            // lblTimeLimit
            // 
            lblTimeLimit.AutoSize = true;
            lblTimeLimit.Location = new Point(18, 120);
            lblTimeLimit.Name = "lblTimeLimit";
            lblTimeLimit.Size = new Size(152, 15);
            lblTimeLimit.TabIndex = 5;
            lblTimeLimit.Text = "Ограничение по времени:";
            // 
            // numHours
            // 
            numHours.Location = new Point(158, 119);
            numHours.Margin = new Padding(3, 2, 3, 2);
            numHours.Maximum = new decimal(new int[] { 5, 0, 0, 0 });
            numHours.Name = "numHours";
            numHours.Size = new Size(44, 23);
            numHours.TabIndex = 6;
            // 
            // lblHours
            // 
            lblHours.AutoSize = true;
            lblHours.Location = new Point(206, 122);
            lblHours.Name = "lblHours";
            lblHours.Size = new Size(39, 15);
            lblHours.TabIndex = 7;
            lblHours.Text = "часов";
            // 
            // numMinutes
            // 
            numMinutes.Location = new Point(245, 120);
            numMinutes.Margin = new Padding(3, 2, 3, 2);
            numMinutes.Maximum = new decimal(new int[] { 59, 0, 0, 0 });
            numMinutes.Name = "numMinutes";
            numMinutes.Size = new Size(44, 23);
            numMinutes.TabIndex = 8;
            numMinutes.Value = new decimal(new int[] { 30, 0, 0, 0 });
            // 
            // lblMinutes
            // 
            lblMinutes.AutoSize = true;
            lblMinutes.Location = new Point(293, 122);
            lblMinutes.Name = "lblMinutes";
            lblMinutes.Size = new Size(41, 15);
            lblMinutes.TabIndex = 9;
            lblMinutes.Text = "минут";
            // 
            // chkRandomQuestions
            // 
            chkRandomQuestions.AutoSize = true;
            chkRandomQuestions.Checked = true;
            chkRandomQuestions.CheckState = CheckState.Checked;
            chkRandomQuestions.Location = new Point(18, 142);
            chkRandomQuestions.Margin = new Padding(3, 2, 3, 2);
            chkRandomQuestions.Name = "chkRandomQuestions";
            chkRandomQuestions.Size = new Size(195, 19);
            chkRandomQuestions.TabIndex = 10;
            chkRandomQuestions.Text = "Случайный порядок вопросов";
            chkRandomQuestions.UseVisualStyleBackColor = true;
            // 
            // chkRandomAnswers
            // 
            chkRandomAnswers.AutoSize = true;
            chkRandomAnswers.Checked = true;
            chkRandomAnswers.CheckState = CheckState.Checked;
            chkRandomAnswers.Location = new Point(18, 161);
            chkRandomAnswers.Margin = new Padding(3, 2, 3, 2);
            chkRandomAnswers.Name = "chkRandomAnswers";
            chkRandomAnswers.Size = new Size(184, 19);
            chkRandomAnswers.TabIndex = 11;
            chkRandomAnswers.Text = "Случайный порядок ответов";
            chkRandomAnswers.UseVisualStyleBackColor = true;
            // 
            // chkActive
            // 
            chkActive.AutoSize = true;
            chkActive.Location = new Point(18, 180);
            chkActive.Margin = new Padding(3, 2, 3, 2);
            chkActive.Name = "chkActive";
            chkActive.Size = new Size(160, 19);
            chkActive.TabIndex = 12;
            chkActive.Text = "Активировать тест сразу";
            chkActive.UseVisualStyleBackColor = true;
            // 
            // lblLimitsInfo
            // 
            lblLimitsInfo.AutoSize = true;
            lblLimitsInfo.Font = new Font("Arial", 8F);
            lblLimitsInfo.ForeColor = Color.Gray;
            lblLimitsInfo.Location = new Point(18, 202);
            lblLimitsInfo.Name = "lblLimitsInfo";
            lblLimitsInfo.Size = new Size(351, 14);
            lblLimitsInfo.TabIndex = 13;
            lblLimitsInfo.Text = "Лимиты: максимум 50 вопросов в тесте, 10 вариантов в вопросе";
            // 
            // btnCreate
            // 
            btnCreate.Location = new Point(158, 225);
            btnCreate.Margin = new Padding(3, 2, 3, 2);
            btnCreate.Name = "btnCreate";
            btnCreate.Size = new Size(88, 22);
            btnCreate.TabIndex = 14;
            btnCreate.Text = "Создать";
            btnCreate.UseVisualStyleBackColor = true;
            btnCreate.Click += BtnCreate_Click;
            // 
            // btnCancel
            // 
            btnCancel.Location = new Point(254, 225);
            btnCancel.Margin = new Padding(3, 2, 3, 2);
            btnCancel.Name = "btnCancel";
            btnCancel.Size = new Size(88, 22);
            btnCancel.TabIndex = 15;
            btnCancel.Text = "Отмена";
            btnCancel.UseVisualStyleBackColor = true;
            btnCancel.Click += BtnCancel_Click;
            // 
            // lblMessage
            // 
            lblMessage.ForeColor = Color.Red;
            lblMessage.Location = new Point(18, 255);
            lblMessage.Name = "lblMessage";
            lblMessage.Size = new Size(394, 30);
            lblMessage.TabIndex = 16;
            lblMessage.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // CreateTestForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(438, 300);
            Controls.Add(lblMessage);
            Controls.Add(btnCancel);
            Controls.Add(btnCreate);
            Controls.Add(lblLimitsInfo);
            Controls.Add(chkActive);
            Controls.Add(chkRandomAnswers);
            Controls.Add(chkRandomQuestions);
            Controls.Add(lblMinutes);
            Controls.Add(numMinutes);
            Controls.Add(lblHours);
            Controls.Add(numHours);
            Controls.Add(lblTimeLimit);
            Controls.Add(txtDescription);
            Controls.Add(lblDescription);
            Controls.Add(txtTitle);
            Controls.Add(lblTitleField);
            Controls.Add(lblTitle);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            Margin = new Padding(3, 2, 3, 2);
            MaximizeBox = false;
            Name = "CreateTestForm";
            StartPosition = FormStartPosition.CenterParent;
            Text = "Создание нового теста";
            ((System.ComponentModel.ISupportInitialize)numHours).EndInit();
            ((System.ComponentModel.ISupportInitialize)numMinutes).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label lblTitle;
        private Label lblTitleField;
        private TextBox txtTitle;
        private Label lblDescription;
        private TextBox txtDescription;
        private Label lblTimeLimit;
        private NumericUpDown numHours;
        private Label lblHours;
        private NumericUpDown numMinutes;
        private Label lblMinutes;
        private CheckBox chkRandomQuestions;
        private CheckBox chkRandomAnswers;
        private CheckBox chkActive;
        private Label lblLimitsInfo;
        private Button btnCreate;
        private Button btnCancel;
        private Label lblMessage;
    }
}