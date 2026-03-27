namespace TestingSystem.WindowsForms
{
    partial class TextAnswersViewForm
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.ListView listViewAnswers;
        private System.Windows.Forms.ColumnHeader colUserName;
        private System.Windows.Forms.ColumnHeader colDate;
        private System.Windows.Forms.ColumnHeader colAnswerText;
        private System.Windows.Forms.ColumnHeader colPoints;
        private System.Windows.Forms.Button btnExport;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Panel panelBottom;
        private System.Windows.Forms.Label lblCount;
        private System.Windows.Forms.Panel panelTop;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            listViewAnswers = new ListView();
            colUserName = new ColumnHeader();
            colDate = new ColumnHeader();
            colAnswerText = new ColumnHeader();
            colPoints = new ColumnHeader();
            panelBottom = new Panel();
            btnExport = new Button();
            btnClose = new Button();
            lblCount = new Label();
            panelTop = new Panel();
            panelBottom.SuspendLayout();
            panelTop.SuspendLayout();
            SuspendLayout();

            // listViewAnswers
            listViewAnswers.Columns.AddRange(new ColumnHeader[] { colUserName, colDate, colAnswerText, colPoints });
            listViewAnswers.Dock = DockStyle.Fill;
            listViewAnswers.FullRowSelect = true;
            listViewAnswers.GridLines = true;
            listViewAnswers.Location = new Point(0, 40);
            listViewAnswers.Name = "listViewAnswers";
            listViewAnswers.Size = new Size(800, 410);
            listViewAnswers.TabIndex = 0;
            listViewAnswers.UseCompatibleStateImageBehavior = false;
            listViewAnswers.View = View.Details;

            // Column Headers
            colUserName.Text = "Пользователь";
            colUserName.Width = 120;
            colDate.Text = "Дата";
            colDate.Width = 120;
            colAnswerText.Text = "Ответ";
            colAnswerText.Width = 450;
            colPoints.Text = "Баллы";
            colPoints.Width = 60;

            // panelTop
            panelTop.BackColor = Color.FromArgb(0, 120, 215);
            panelTop.Controls.Add(lblCount);
            panelTop.Dock = DockStyle.Top;
            panelTop.Location = new Point(0, 0);
            panelTop.Size = new Size(800, 40);

            // lblCount
            lblCount.AutoSize = true;
            lblCount.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lblCount.ForeColor = Color.White;
            lblCount.Location = new Point(12, 10);
            lblCount.Size = new Size(100, 19);
            lblCount.Text = "Всего: 0 ответов";

            // panelBottom
            panelBottom.BackColor = Color.FromArgb(240, 240, 240);
            panelBottom.Controls.Add(btnExport);
            panelBottom.Controls.Add(btnClose);
            panelBottom.Dock = DockStyle.Bottom;
            panelBottom.Location = new Point(0, 450);
            panelBottom.Size = new Size(800, 50);

            // btnExport
            btnExport.BackColor = Color.FromArgb(0, 120, 215);
            btnExport.FlatAppearance.BorderSize = 0;
            btnExport.FlatStyle = FlatStyle.Flat;
            btnExport.ForeColor = Color.White;
            btnExport.Location = new Point(12, 10);
            btnExport.Size = new Size(100, 30);
            btnExport.Text = "Экспорт";
            btnExport.UseVisualStyleBackColor = false;
            btnExport.Click += BtnExport_Click;

            // btnClose
            btnClose.BackColor = Color.FromArgb(100, 100, 100);
            btnClose.FlatAppearance.BorderSize = 0;
            btnClose.FlatStyle = FlatStyle.Flat;
            btnClose.ForeColor = Color.White;
            btnClose.Location = new Point(688, 10);
            btnClose.Size = new Size(100, 30);
            btnClose.Text = "Закрыть";
            btnClose.UseVisualStyleBackColor = false;
            btnClose.Click += BtnClose_Click;

            // Form
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 500);
            Controls.Add(listViewAnswers);
            Controls.Add(panelTop);
            Controls.Add(panelBottom);
            Name = "TextAnswersViewForm";
            StartPosition = FormStartPosition.CenterParent;
            Text = "Текстовые ответы";
            panelBottom.ResumeLayout(false);
            panelTop.ResumeLayout(false);
            panelTop.PerformLayout();
            ResumeLayout(false);
        }
    }
}