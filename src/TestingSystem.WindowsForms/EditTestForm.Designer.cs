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
            chkIsScored = new CheckBox();
            lblSettings = new Label();
            tableLayout = new TableLayoutPanel();
            timePanel = new FlowLayoutPanel();
            buttonPanel = new FlowLayoutPanel();
            btnCancel = new Button();
            btnSave = new Button();
            lblMessage = new Label();
            settingsPanel = new FlowLayoutPanel();
            ((System.ComponentModel.ISupportInitialize)numHours).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numMinutes).BeginInit();
            tableLayout.SuspendLayout();
            timePanel.SuspendLayout();
            buttonPanel.SuspendLayout();
            settingsPanel.SuspendLayout();
            SuspendLayout();
            // 
            // lblTitle
            // 
            tableLayout.SetColumnSpan(lblTitle, 2);
            lblTitle.Dock = DockStyle.Fill;
            lblTitle.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            lblTitle.Location = new Point(23, 20);
            lblTitle.Name = "lblTitle";
            lblTitle.Size = new Size(454, 40);
            lblTitle.TabIndex = 0;
            lblTitle.Text = "Редактирование теста";
            lblTitle.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // lblTitleField
            // 
            lblTitleField.Location = new Point(23, 60);
            lblTitleField.Name = "lblTitleField";
            lblTitleField.Size = new Size(100, 23);
            lblTitleField.TabIndex = 1;
            lblTitleField.Text = "Название:";
            lblTitleField.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // txtTitle
            // 
            txtTitle.Dock = DockStyle.Fill;
            txtTitle.Location = new Point(184, 63);
            txtTitle.Name = "txtTitle";
            txtTitle.Size = new Size(293, 25);
            txtTitle.TabIndex = 2;
            // 
            // lblDescription
            // 
            lblDescription.Location = new Point(23, 100);
            lblDescription.Name = "lblDescription";
            lblDescription.Size = new Size(100, 23);
            lblDescription.TabIndex = 3;
            lblDescription.Text = "Описание:";
            lblDescription.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // txtDescription
            // 
            txtDescription.Dock = DockStyle.Fill;
            txtDescription.Location = new Point(184, 103);
            txtDescription.Multiline = true;
            txtDescription.Name = "txtDescription";
            txtDescription.Size = new Size(293, 74);
            txtDescription.TabIndex = 4;
            // 
            // lblTimeLimit
            // 
            lblTimeLimit.Location = new Point(23, 180);
            lblTimeLimit.Name = "lblTimeLimit";
            lblTimeLimit.Size = new Size(100, 23);
            lblTimeLimit.TabIndex = 5;
            lblTimeLimit.Text = "Время:";
            lblTimeLimit.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // numHours
            // 
            numHours.Location = new Point(3, 3);
            numHours.Maximum = new decimal(new int[] { 5, 0, 0, 0 });
            numHours.Name = "numHours";
            numHours.Size = new Size(50, 25);
            numHours.TabIndex = 0;
            // 
            // lblHours
            // 
            lblHours.Location = new Point(59, 0);
            lblHours.Name = "lblHours";
            lblHours.Size = new Size(100, 23);
            lblHours.TabIndex = 1;
            lblHours.Text = "ч";
            // 
            // numMinutes
            // 
            numMinutes.Location = new Point(165, 3);
            numMinutes.Maximum = new decimal(new int[] { 59, 0, 0, 0 });
            numMinutes.Name = "numMinutes";
            numMinutes.Size = new Size(50, 25);
            numMinutes.TabIndex = 2;
            numMinutes.Value = new decimal(new int[] { 30, 0, 0, 0 });
            // 
            // lblMinutes
            // 
            lblMinutes.Location = new Point(3, 31);
            lblMinutes.Name = "lblMinutes";
            lblMinutes.Size = new Size(100, 23);
            lblMinutes.TabIndex = 3;
            lblMinutes.Text = "мин";
            // 
            // chkRandomQuestions
            // 
            chkRandomQuestions.AutoSize = true;
            chkRandomQuestions.Checked = true;
            chkRandomQuestions.CheckState = CheckState.Checked;
            chkRandomQuestions.Location = new Point(13, 3);
            chkRandomQuestions.Name = "chkRandomQuestions";
            chkRandomQuestions.Size = new Size(222, 23);
            chkRandomQuestions.TabIndex = 0;
            chkRandomQuestions.Text = "Случайный порядок вопросов";
            // 
            // chkRandomAnswers
            // 
            chkRandomAnswers.AutoSize = true;
            chkRandomAnswers.Checked = true;
            chkRandomAnswers.CheckState = CheckState.Checked;
            chkRandomAnswers.Location = new Point(13, 32);
            chkRandomAnswers.Name = "chkRandomAnswers";
            chkRandomAnswers.Size = new Size(211, 23);
            chkRandomAnswers.TabIndex = 1;
            chkRandomAnswers.Text = "Случайный порядок ответов";
            // 
            // chkActive
            // 
            chkActive.AutoSize = true;
            chkActive.Location = new Point(13, 61);
            chkActive.Name = "chkActive";
            chkActive.Size = new Size(154, 23);
            chkActive.TabIndex = 2;
            chkActive.Text = "Активировать";
            // 
            // chkIsScored
            // 
            chkIsScored.AutoSize = true;
            chkIsScored.Checked = true;
            chkIsScored.CheckState = CheckState.Checked;
            chkIsScored.Location = new Point(13, 90);
            chkIsScored.Name = "chkIsScored";
            chkIsScored.Size = new Size(187, 23);
            chkIsScored.TabIndex = 3;
            chkIsScored.Text = "С оценкой (иначе опрос)";
            // 
            // lblSettings
            // 
            tableLayout.SetColumnSpan(lblSettings, 2);
            lblSettings.Dock = DockStyle.Fill;
            lblSettings.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lblSettings.Location = new Point(23, 220);
            lblSettings.Name = "lblSettings";
            lblSettings.Size = new Size(454, 30);
            lblSettings.TabIndex = 11;
            lblSettings.Text = "Настройки теста";
            lblSettings.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // tableLayout
            // 
            tableLayout.ColumnCount = 2;
            tableLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 35F));
            tableLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 65F));
            tableLayout.Controls.Add(lblTitle, 0, 0);
            tableLayout.Controls.Add(lblTitleField, 0, 1);
            tableLayout.Controls.Add(txtTitle, 1, 1);
            tableLayout.Controls.Add(lblDescription, 0, 2);
            tableLayout.Controls.Add(txtDescription, 1, 2);
            tableLayout.Controls.Add(lblTimeLimit, 0, 3);
            tableLayout.Controls.Add(timePanel, 1, 3);
            tableLayout.Controls.Add(buttonPanel, 0, 6);
            tableLayout.Controls.Add(lblMessage, 0, 7);
            tableLayout.Controls.Add(lblSettings, 0, 4);
            tableLayout.Controls.Add(settingsPanel, 0, 5);
            tableLayout.Dock = DockStyle.Fill;
            tableLayout.Location = new Point(0, 0);
            tableLayout.Name = "tableLayout";
            tableLayout.Padding = new Padding(20);
            tableLayout.RowCount = 7;
            tableLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 40F));
            tableLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 40F));
            tableLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 80F));
            tableLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 40F));
            tableLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 30F));
            tableLayout.RowStyles.Add(new RowStyle());
            tableLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 50F));
            tableLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 30F));
            tableLayout.Size = new Size(500, 493);
            tableLayout.TabIndex = 0;
            // 
            // timePanel
            // 
            timePanel.Controls.Add(numHours);
            timePanel.Controls.Add(lblHours);
            timePanel.Controls.Add(numMinutes);
            timePanel.Controls.Add(lblMinutes);
            timePanel.Dock = DockStyle.Fill;
            timePanel.Location = new Point(184, 183);
            timePanel.Name = "timePanel";
            timePanel.Size = new Size(293, 34);
            timePanel.TabIndex = 6;
            // 
            // buttonPanel
            // 
            tableLayout.SetColumnSpan(buttonPanel, 2);
            buttonPanel.Controls.Add(btnCancel);
            buttonPanel.Controls.Add(btnSave);
            buttonPanel.Dock = DockStyle.Fill;
            buttonPanel.FlowDirection = FlowDirection.RightToLeft;
            buttonPanel.Location = new Point(23, 412);
            buttonPanel.Name = "buttonPanel";
            buttonPanel.Size = new Size(454, 44);
            buttonPanel.TabIndex = 9;
            // 
            // btnCancel
            // 
            btnCancel.AutoSize = true;
            btnCancel.Location = new Point(376, 3);
            btnCancel.Name = "btnCancel";
            btnCancel.Size = new Size(75, 29);
            btnCancel.TabIndex = 0;
            btnCancel.Text = "Отмена";
            btnCancel.Click += BtnCancel_Click;
            // 
            // btnSave
            // 
            btnSave.AutoSize = true;
            btnSave.BackColor = Color.FromArgb(0, 120, 215);
            btnSave.FlatAppearance.BorderSize = 0;
            btnSave.FlatStyle = FlatStyle.Flat;
            btnSave.ForeColor = Color.White;
            btnSave.Location = new Point(295, 3);
            btnSave.Name = "btnSave";
            btnSave.Size = new Size(75, 29);
            btnSave.TabIndex = 1;
            btnSave.Text = "Сохранить";
            btnSave.UseVisualStyleBackColor = false;
            btnSave.Click += BtnSave_Click;
            // 
            // lblMessage
            // 
            tableLayout.SetColumnSpan(lblMessage, 2);
            lblMessage.Dock = DockStyle.Fill;
            lblMessage.ForeColor = Color.Red;
            lblMessage.Location = new Point(23, 459);
            lblMessage.Name = "lblMessage";
            lblMessage.Size = new Size(454, 30);
            lblMessage.TabIndex = 10;
            lblMessage.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // settingsPanel
            // 
            settingsPanel.AutoSize = true;
            settingsPanel.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            tableLayout.SetColumnSpan(settingsPanel, 2);
            settingsPanel.Controls.Add(chkRandomQuestions);
            settingsPanel.Controls.Add(chkRandomAnswers);
            settingsPanel.Controls.Add(chkActive);
            settingsPanel.Controls.Add(chkIsScored);
            settingsPanel.Dock = DockStyle.Fill;
            settingsPanel.FlowDirection = FlowDirection.TopDown;
            settingsPanel.Location = new Point(23, 253);
            settingsPanel.Name = "settingsPanel";
            settingsPanel.Padding = new Padding(10, 0, 0, 0);
            settingsPanel.Size = new Size(454, 156);
            settingsPanel.TabIndex = 7;
            settingsPanel.WrapContents = false;
            // 
            // EditTestForm
            // 
            BackColor = Color.White;
            ClientSize = new Size(500, 493);
            Controls.Add(tableLayout);
            Font = new Font("Segoe UI", 10F);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            Name = "EditTestForm";
            StartPosition = FormStartPosition.CenterParent;
            Text = "Редактирование теста";
            ((System.ComponentModel.ISupportInitialize)numHours).EndInit();
            ((System.ComponentModel.ISupportInitialize)numMinutes).EndInit();
            tableLayout.ResumeLayout(false);
            tableLayout.PerformLayout();
            timePanel.ResumeLayout(false);
            buttonPanel.ResumeLayout(false);
            buttonPanel.PerformLayout();
            settingsPanel.ResumeLayout(false);
            settingsPanel.PerformLayout();
            ResumeLayout(false);
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
        private CheckBox chkIsScored;
        private TableLayoutPanel tableLayout;
        private FlowLayoutPanel settingsPanel;
        private FlowLayoutPanel timePanel;
        private Label lblSettings;
        private FlowLayoutPanel buttonPanel;
        private Button btnCancel;
        private Button btnSave;
        private Label lblMessage;
    }
}