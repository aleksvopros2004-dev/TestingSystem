namespace TestingSystem.WindowsForms
{
    partial class EditTestForm
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
            this.lblTitle = new System.Windows.Forms.Label();
            this.txtTitle = new System.Windows.Forms.TextBox();
            this.lblDescription = new System.Windows.Forms.Label();
            this.txtDescription = new System.Windows.Forms.TextBox();
            this.lblTimeLimit = new System.Windows.Forms.Label();
            this.numHours = new System.Windows.Forms.NumericUpDown();
            this.lblHours = new System.Windows.Forms.Label();
            this.numMinutes = new System.Windows.Forms.NumericUpDown();
            this.lblMinutes = new System.Windows.Forms.Label();
            this.chkRandomQuestions = new System.Windows.Forms.CheckBox();
            this.chkRandomAnswers = new System.Windows.Forms.CheckBox();
            this.chkActive = new System.Windows.Forms.CheckBox();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.lblMessage = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.numHours)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numMinutes)).BeginInit();
            this.SuspendLayout();

            // lblTitle
            this.lblTitle.AutoSize = true;
            this.lblTitle.Location = new System.Drawing.Point(20, 20);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(150, 20);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "Название теста:";

            // txtTitle
            this.txtTitle.Location = new System.Drawing.Point(180, 20);
            this.txtTitle.Name = "txtTitle";
            this.txtTitle.Size = new System.Drawing.Size(280, 27);
            this.txtTitle.TabIndex = 1;

            // lblDescription
            this.lblDescription.AutoSize = true;
            this.lblDescription.Location = new System.Drawing.Point(20, 60);
            this.lblDescription.Name = "lblDescription";
            this.lblDescription.Size = new System.Drawing.Size(150, 20);
            this.lblDescription.TabIndex = 2;
            this.lblDescription.Text = "Описание:";

            // txtDescription
            this.txtDescription.Location = new System.Drawing.Point(180, 60);
            this.txtDescription.Multiline = true;
            this.txtDescription.Name = "txtDescription";
            this.txtDescription.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtDescription.Size = new System.Drawing.Size(280, 60);
            this.txtDescription.TabIndex = 3;

            // lblTimeLimit
            this.lblTimeLimit.AutoSize = true;
            this.lblTimeLimit.Location = new System.Drawing.Point(20, 140);
            this.lblTimeLimit.Name = "lblTimeLimit";
            this.lblTimeLimit.Size = new System.Drawing.Size(150, 20);
            this.lblTimeLimit.TabIndex = 4;
            this.lblTimeLimit.Text = "Ограничение по времени:";

            // numHours
            this.numHours.Location = new System.Drawing.Point(180, 140);
            this.numHours.Maximum = new decimal(new int[] { 5, 0, 0, 0 });
            this.numHours.Name = "numHours";
            this.numHours.Size = new System.Drawing.Size(50, 27);
            this.numHours.TabIndex = 5;

            // lblHours
            this.lblHours.AutoSize = true;
            this.lblHours.Location = new System.Drawing.Point(235, 143);
            this.lblHours.Name = "lblHours";
            this.lblHours.Size = new System.Drawing.Size(40, 20);
            this.lblHours.TabIndex = 6;
            this.lblHours.Text = "часов";

            // numMinutes
            this.numMinutes.Location = new System.Drawing.Point(280, 140);
            this.numMinutes.Maximum = new decimal(new int[] { 59, 0, 0, 0 });
            this.numMinutes.Name = "numMinutes";
            this.numMinutes.Size = new System.Drawing.Size(50, 27);
            this.numMinutes.TabIndex = 7;

            // lblMinutes
            this.lblMinutes.AutoSize = true;
            this.lblMinutes.Location = new System.Drawing.Point(335, 143);
            this.lblMinutes.Name = "lblMinutes";
            this.lblMinutes.Size = new System.Drawing.Size(40, 20);
            this.lblMinutes.TabIndex = 8;
            this.lblMinutes.Text = "минут";

            // chkRandomQuestions
            this.chkRandomQuestions.AutoSize = true;
            this.chkRandomQuestions.Location = new System.Drawing.Point(20, 180);
            this.chkRandomQuestions.Name = "chkRandomQuestions";
            this.chkRandomQuestions.Size = new System.Drawing.Size(250, 24);
            this.chkRandomQuestions.TabIndex = 9;
            this.chkRandomQuestions.Text = "Случайный порядок вопросов";
            this.chkRandomQuestions.UseVisualStyleBackColor = true;

            // chkRandomAnswers
            this.chkRandomAnswers.AutoSize = true;
            this.chkRandomAnswers.Location = new System.Drawing.Point(20, 210);
            this.chkRandomAnswers.Name = "chkRandomAnswers";
            this.chkRandomAnswers.Size = new System.Drawing.Size(250, 24);
            this.chkRandomAnswers.TabIndex = 10;
            this.chkRandomAnswers.Text = "Случайный порядок ответов";
            this.chkRandomAnswers.UseVisualStyleBackColor = true;

            // chkActive
            this.chkActive.AutoSize = true;
            this.chkActive.Location = new System.Drawing.Point(20, 240);
            this.chkActive.Name = "chkActive";
            this.chkActive.Size = new System.Drawing.Size(250, 24);
            this.chkActive.TabIndex = 11;
            this.chkActive.Text = "Активировать тест";
            this.chkActive.UseVisualStyleBackColor = true;

            // btnSave
            this.btnSave.Location = new System.Drawing.Point(180, 290);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(100, 30);
            this.btnSave.TabIndex = 12;
            this.btnSave.Text = "Сохранить";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.BtnSave_Click);

            // btnCancel
            this.btnCancel.Location = new System.Drawing.Point(290, 290);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(100, 30);
            this.btnCancel.TabIndex = 13;
            this.btnCancel.Text = "Отмена";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.BtnCancel_Click);

            // lblMessage
            this.lblMessage.AutoSize = false;
            this.lblMessage.Location = new System.Drawing.Point(20, 330);
            this.lblMessage.Name = "lblMessage";
            this.lblMessage.Size = new System.Drawing.Size(440, 20);
            this.lblMessage.TabIndex = 14;
            this.lblMessage.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblMessage.ForeColor = System.Drawing.Color.Red;

            // EditTestForm
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(500, 400);
            this.Controls.Add(this.lblMessage);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.chkActive);
            this.Controls.Add(this.chkRandomAnswers);
            this.Controls.Add(this.chkRandomQuestions);
            this.Controls.Add(this.lblMinutes);
            this.Controls.Add(this.numMinutes);
            this.Controls.Add(this.lblHours);
            this.Controls.Add(this.numHours);
            this.Controls.Add(this.lblTimeLimit);
            this.Controls.Add(this.txtDescription);
            this.Controls.Add(this.lblDescription);
            this.Controls.Add(this.txtTitle);
            this.Controls.Add(this.lblTitle);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.Name = "EditTestForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Редактирование теста";
            ((System.ComponentModel.ISupportInitialize)(this.numHours)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numMinutes)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        #endregion

        private Label lblTitle;
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
        private Button btnSave;
        private Button btnCancel;
        private Label lblMessage;
    }
}