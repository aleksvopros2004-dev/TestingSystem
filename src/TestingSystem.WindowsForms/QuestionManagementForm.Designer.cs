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
            toolStrip = new ToolStrip();
            btnAddQuestionTool = new ToolStripButton();
            btnRefreshTool = new ToolStripButton();
            tableLayout = new TableLayoutPanel();
            listViewQuestions = new ListView();
            columnHeader1 = new ColumnHeader();
            columnHeader2 = new ColumnHeader();
            columnHeader3 = new ColumnHeader();
            columnHeader4 = new ColumnHeader();
            columnHeader5 = new ColumnHeader();
            buttonPanel = new FlowLayoutPanel();
            btnEdit = new Button();
            btnDelete = new Button();
            btnMoveUp = new Button();
            btnMoveDown = new Button();
            txtDebug = new TextBox();
            toolStrip.SuspendLayout();
            tableLayout.SuspendLayout();
            buttonPanel.SuspendLayout();
            SuspendLayout();
            // 
            // toolStrip
            // 
            toolStrip.ImageScalingSize = new Size(20, 20);
            toolStrip.Items.AddRange(new ToolStripItem[] { btnAddQuestionTool, btnRefreshTool });
            toolStrip.Location = new Point(0, 0);
            toolStrip.Name = "toolStrip";
            toolStrip.Size = new Size(900, 27);
            toolStrip.TabIndex = 0;
            // 
            // btnAddQuestionTool
            // 
            btnAddQuestionTool.DisplayStyle = ToolStripItemDisplayStyle.Text;
            btnAddQuestionTool.Name = "btnAddQuestionTool";
            btnAddQuestionTool.Size = new Size(95, 24);
            btnAddQuestionTool.Text = "Добавить вопрос";
            btnAddQuestionTool.Click += BtnAddQuestion_Click;
            // 
            // btnRefreshTool
            // 
            btnRefreshTool.DisplayStyle = ToolStripItemDisplayStyle.Text;
            btnRefreshTool.Name = "btnRefreshTool";
            btnRefreshTool.Size = new Size(65, 24);
            btnRefreshTool.Text = "Обновить";
            btnRefreshTool.Click += BtnRefresh_Click;
            // 
            // tableLayout
            // 
            tableLayout.ColumnCount = 2;
            tableLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 75F));
            tableLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25F));
            tableLayout.Controls.Add(listViewQuestions, 0, 0);
            tableLayout.Controls.Add(buttonPanel, 1, 0);
            tableLayout.Controls.Add(txtDebug, 0, 1);
            tableLayout.Dock = DockStyle.Fill;
            tableLayout.Location = new Point(0, 27);
            tableLayout.Name = "tableLayout";
            tableLayout.Padding = new Padding(20);
            tableLayout.RowCount = 2;
            tableLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 70F));
            tableLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 30F));
            tableLayout.Size = new Size(900, 573);
            tableLayout.TabIndex = 1;
            // 
            // listViewQuestions
            // 
            listViewQuestions.Columns.AddRange(new ColumnHeader[] { columnHeader1, columnHeader2, columnHeader3, columnHeader4, columnHeader5 });
            listViewQuestions.Dock = DockStyle.Fill;
            listViewQuestions.FullRowSelect = true;
            listViewQuestions.GridLines = true;
            listViewQuestions.Location = new Point(23, 23);
            listViewQuestions.Name = "listViewQuestions";
            listViewQuestions.Size = new Size(629, 379);
            listViewQuestions.TabIndex = 0;
            listViewQuestions.UseCompatibleStateImageBehavior = false;
            listViewQuestions.View = View.Details;
            // 
            // columnHeader1
            // 
            columnHeader1.Text = "ID";
            columnHeader1.Width = 50;
            // 
            // columnHeader2
            // 
            columnHeader2.Text = "Текст вопроса";
            columnHeader2.Width = 400;
            // 
            // columnHeader3
            // 
            columnHeader3.Text = "Тип";
            columnHeader3.Width = 120;
            // 
            // columnHeader4
            // 
            columnHeader4.Text = "Вариантов";
            columnHeader4.Width = 80;
            // 
            // columnHeader5
            // 
            columnHeader5.Text = "Порядок";
            columnHeader5.Width = 80;
            // 
            // buttonPanel
            // 
            buttonPanel.Controls.Add(btnEdit);
            buttonPanel.Controls.Add(btnDelete);
            buttonPanel.Controls.Add(btnMoveUp);
            buttonPanel.Controls.Add(btnMoveDown);
            buttonPanel.Dock = DockStyle.Top;
            buttonPanel.FlowDirection = FlowDirection.TopDown;
            buttonPanel.Location = new Point(658, 23);
            buttonPanel.Name = "buttonPanel";
            buttonPanel.Size = new Size(219, 150);
            buttonPanel.TabIndex = 1;
            // 
            // btnEdit
            // 
            btnEdit.AutoSize = true;
            btnEdit.Location = new Point(3, 3);
            btnEdit.Name = "btnEdit";
            btnEdit.Size = new Size(130, 30);
            btnEdit.TabIndex = 0;
            btnEdit.Text = "Редактировать";
            btnEdit.UseVisualStyleBackColor = true;
            btnEdit.Click += BtnEdit_Click;
            // 
            // btnDelete
            // 
            btnDelete.AutoSize = true;
            btnDelete.Location = new Point(3, 39);
            btnDelete.Name = "btnDelete";
            btnDelete.Size = new Size(130, 30);
            btnDelete.TabIndex = 1;
            btnDelete.Text = "Удалить";
            btnDelete.UseVisualStyleBackColor = true;
            btnDelete.Click += BtnDelete_Click;
            // 
            // btnMoveUp
            // 
            btnMoveUp.AutoSize = true;
            btnMoveUp.Location = new Point(3, 75);
            btnMoveUp.Name = "btnMoveUp";
            btnMoveUp.Size = new Size(130, 30);
            btnMoveUp.TabIndex = 2;
            btnMoveUp.Text = "↑ Выше";
            btnMoveUp.UseVisualStyleBackColor = true;
            btnMoveUp.Click += BtnMoveUp_Click;
            // 
            // btnMoveDown
            // 
            btnMoveDown.AutoSize = true;
            btnMoveDown.Location = new Point(3, 111);
            btnMoveDown.Name = "btnMoveDown";
            btnMoveDown.Size = new Size(130, 30);
            btnMoveDown.TabIndex = 3;
            btnMoveDown.Text = "↓ Ниже";
            btnMoveDown.UseVisualStyleBackColor = true;
            btnMoveDown.Click += BtnMoveDown_Click;
            // 
            // txtDebug
            // 
            txtDebug.Dock = DockStyle.Fill;
            txtDebug.Location = new Point(23, 405);
            txtDebug.Multiline = true;
            txtDebug.Name = "txtDebug";
            txtDebug.ReadOnly = true;
            txtDebug.ScrollBars = ScrollBars.Vertical;
            txtDebug.Size = new Size(854, 145);
            txtDebug.TabIndex = 2;
            // 
            // QuestionManagementForm
            // 
            BackColor = Color.White;
            ClientSize = new Size(900, 600);
            Controls.Add(tableLayout);
            Controls.Add(toolStrip);
            Font = new Font("Segoe UI", 10F);
            Name = "QuestionManagementForm";
            StartPosition = FormStartPosition.CenterParent;
            Text = "Управление вопросами";
            toolStrip.ResumeLayout(false);
            toolStrip.PerformLayout();
            tableLayout.ResumeLayout(false);
            tableLayout.PerformLayout();
            buttonPanel.ResumeLayout(false);
            buttonPanel.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private ToolStrip toolStrip;
        private ToolStripButton btnAddQuestionTool;
        private ToolStripButton btnRefreshTool;
        private TableLayoutPanel tableLayout;
        private ListView listViewQuestions;
        private ColumnHeader columnHeader1;
        private ColumnHeader columnHeader2;
        private ColumnHeader columnHeader3;
        private ColumnHeader columnHeader4;
        private ColumnHeader columnHeader5;
        private FlowLayoutPanel buttonPanel;
        private Button btnEdit;
        private Button btnDelete;
        private Button btnMoveUp;
        private Button btnMoveDown;
        private TextBox txtDebug;
    }
}