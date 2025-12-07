namespace TestingSystem.WindowsForms
{
    partial class QuestionManagementForm
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
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.btnAddQuestionTool = new System.Windows.Forms.ToolStripButton();
            this.btnRefreshTool = new System.Windows.Forms.ToolStripButton();
            this.listViewQuestions = new System.Windows.Forms.ListView();
            this.columnHeader1 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader2 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader3 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader4 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader5 = new System.Windows.Forms.ColumnHeader();
            this.btnEdit = new System.Windows.Forms.Button();
            this.btnDelete = new System.Windows.Forms.Button();
            this.btnMoveUp = new System.Windows.Forms.Button();
            this.btnMoveDown = new System.Windows.Forms.Button();
            this.txtDebug = new System.Windows.Forms.TextBox();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();

            // toolStrip1
            this.toolStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnAddQuestionTool,
            this.btnRefreshTool});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(900, 27);
            this.toolStrip1.TabIndex = 0;
            this.toolStrip1.Text = "toolStrip1";

            // btnAddQuestionTool
            this.btnAddQuestionTool.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.btnAddQuestionTool.Name = "btnAddQuestionTool";
            this.btnAddQuestionTool.Size = new System.Drawing.Size(120, 24);
            this.btnAddQuestionTool.Text = "Добавить вопрос";
            this.btnAddQuestionTool.Click += new System.EventHandler(this.BtnAddQuestion_Click);

            // btnRefreshTool
            this.btnRefreshTool.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.btnRefreshTool.Name = "btnRefreshTool";
            this.btnRefreshTool.Size = new System.Drawing.Size(85, 24);
            this.btnRefreshTool.Text = "Обновить";
            this.btnRefreshTool.Click += new System.EventHandler(this.BtnRefresh_Click);

            // listViewQuestions
            this.listViewQuestions.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2,
            this.columnHeader3,
            this.columnHeader4,
            this.columnHeader5});
            this.listViewQuestions.FullRowSelect = true;
            this.listViewQuestions.GridLines = true;
            this.listViewQuestions.Location = new System.Drawing.Point(20, 40);
            this.listViewQuestions.Name = "listViewQuestions";
            this.listViewQuestions.Size = new System.Drawing.Size(700, 400);
            this.listViewQuestions.TabIndex = 1;
            this.listViewQuestions.UseCompatibleStateImageBehavior = false;
            this.listViewQuestions.View = System.Windows.Forms.View.Details;

            // columnHeader1
            this.columnHeader1.Text = "ID";
            this.columnHeader1.Width = 50;

            // columnHeader2
            this.columnHeader2.Text = "Текст вопроса";
            this.columnHeader2.Width = 400;

            // columnHeader3
            this.columnHeader3.Text = "Тип";
            this.columnHeader3.Width = 120;

            // columnHeader4
            this.columnHeader4.Text = "Вариантов";
            this.columnHeader4.Width = 80;

            // columnHeader5
            this.columnHeader5.Text = "Порядок";
            this.columnHeader5.Width = 60;

            // btnEdit
            this.btnEdit.Location = new System.Drawing.Point(730, 40);
            this.btnEdit.Name = "btnEdit";
            this.btnEdit.Size = new System.Drawing.Size(140, 30);
            this.btnEdit.TabIndex = 2;
            this.btnEdit.Text = "Редактировать";
            this.btnEdit.UseVisualStyleBackColor = true;
            this.btnEdit.Click += new System.EventHandler(this.BtnEdit_Click);

            // btnDelete
            this.btnDelete.Location = new System.Drawing.Point(730, 80);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(140, 30);
            this.btnDelete.TabIndex = 3;
            this.btnDelete.Text = "Удалить";
            this.btnDelete.UseVisualStyleBackColor = true;
            this.btnDelete.Click += new System.EventHandler(this.BtnDelete_Click);

            // btnMoveUp
            this.btnMoveUp.Location = new System.Drawing.Point(730, 120);
            this.btnMoveUp.Name = "btnMoveUp";
            this.btnMoveUp.Size = new System.Drawing.Size(140, 30);
            this.btnMoveUp.TabIndex = 4;
            this.btnMoveUp.Text = "↑ Выше";
            this.btnMoveUp.UseVisualStyleBackColor = true;
            this.btnMoveUp.Click += new System.EventHandler(this.BtnMoveUp_Click);

            // btnMoveDown
            this.btnMoveDown.Location = new System.Drawing.Point(730, 160);
            this.btnMoveDown.Name = "btnMoveDown";
            this.btnMoveDown.Size = new System.Drawing.Size(140, 30);
            this.btnMoveDown.TabIndex = 5;
            this.btnMoveDown.Text = "↓ Ниже";
            this.btnMoveDown.UseVisualStyleBackColor = true;
            this.btnMoveDown.Click += new System.EventHandler(this.BtnMoveDown_Click);

            // txtDebug
            this.txtDebug.Location = new System.Drawing.Point(20, 450);
            this.txtDebug.Multiline = true;
            this.txtDebug.Name = "txtDebug";
            this.txtDebug.ReadOnly = true;
            this.txtDebug.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtDebug.Size = new System.Drawing.Size(700, 100);
            this.txtDebug.TabIndex = 6;

            // QuestionManagementForm
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(900, 600);
            this.Controls.Add(this.txtDebug);
            this.Controls.Add(this.btnMoveDown);
            this.Controls.Add(this.btnMoveUp);
            this.Controls.Add(this.btnDelete);
            this.Controls.Add(this.btnEdit);
            this.Controls.Add(this.listViewQuestions);
            this.Controls.Add(this.toolStrip1);
            this.Name = "QuestionManagementForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Управление вопросами";
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        #endregion

        private ToolStrip toolStrip1;
        private ToolStripButton btnAddQuestionTool;
        private ToolStripButton btnRefreshTool;
        private ListView listViewQuestions;
        private ColumnHeader columnHeader1;
        private ColumnHeader columnHeader2;
        private ColumnHeader columnHeader3;
        private ColumnHeader columnHeader4;
        private ColumnHeader columnHeader5;
        private Button btnEdit;
        private Button btnDelete;
        private Button btnMoveUp;
        private Button btnMoveDown;
        private TextBox txtDebug;
    }
}