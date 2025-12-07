namespace TestingSystem.WindowsForms
{
    partial class TestManagementForm
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
            toolStrip1 = new ToolStrip();
            btnCreateTestTool = new ToolStripButton();
            btnRefreshTool = new ToolStripButton();
            listViewTests = new ListView();
            columnHeader1 = new ColumnHeader();
            columnHeader2 = new ColumnHeader();
            columnHeader3 = new ColumnHeader();
            columnHeader4 = new ColumnHeader();
            columnHeader5 = new ColumnHeader();
            columnHeader6 = new ColumnHeader();
            btnEdit = new Button();
            btnDelete = new Button();
            btnToggleActive = new Button();
            btnManageQuestions = new Button();
            lblStats = new Label();
            toolStrip1.SuspendLayout();
            SuspendLayout();
            // 
            // toolStrip1
            // 
            toolStrip1.ImageScalingSize = new Size(20, 20);
            toolStrip1.Items.AddRange(new ToolStripItem[] { btnCreateTestTool, btnRefreshTool });
            toolStrip1.Location = new Point(0, 0);
            toolStrip1.Name = "toolStrip1";
            toolStrip1.Size = new Size(875, 25);
            toolStrip1.TabIndex = 0;
            toolStrip1.Text = "toolStrip1";
            // 
            // btnCreateTestTool
            // 
            btnCreateTestTool.DisplayStyle = ToolStripItemDisplayStyle.Text;
            btnCreateTestTool.Name = "btnCreateTestTool";
            btnCreateTestTool.Size = new Size(79, 22);
            btnCreateTestTool.Text = "Создать тест";
            btnCreateTestTool.Click += BtnCreateTest_Click;
            // 
            // btnRefreshTool
            // 
            btnRefreshTool.DisplayStyle = ToolStripItemDisplayStyle.Text;
            btnRefreshTool.Name = "btnRefreshTool";
            btnRefreshTool.Size = new Size(65, 22);
            btnRefreshTool.Text = "Обновить";
            btnRefreshTool.Click += BtnRefresh_Click;
            // 
            // listViewTests
            // 
            listViewTests.Columns.AddRange(new ColumnHeader[] { columnHeader1, columnHeader2, columnHeader3, columnHeader4, columnHeader5, columnHeader6 });
            listViewTests.FullRowSelect = true;
            listViewTests.GridLines = true;
            listViewTests.Location = new Point(18, 30);
            listViewTests.Margin = new Padding(3, 2, 3, 2);
            listViewTests.Name = "listViewTests";
            listViewTests.Size = new Size(613, 301);
            listViewTests.TabIndex = 1;
            listViewTests.UseCompatibleStateImageBehavior = false;
            listViewTests.View = View.Details;
            // 
            // columnHeader1
            // 
            columnHeader1.Text = "ID";
            columnHeader1.Width = 50;
            // 
            // columnHeader2
            // 
            columnHeader2.Text = "Название";
            columnHeader2.Width = 200;
            // 
            // columnHeader3
            // 
            columnHeader3.Text = "Описание";
            columnHeader3.Width = 250;
            // 
            // columnHeader4
            // 
            columnHeader4.Text = "Вопросов";
            columnHeader4.Width = 80;
            // 
            // columnHeader5
            // 
            columnHeader5.Text = "Статус";
            columnHeader5.Width = 80;
            // 
            // columnHeader6
            // 
            columnHeader6.Text = "Дата создания";
            columnHeader6.Width = 120;
            // 
            // btnEdit
            // 
            btnEdit.Location = new Point(639, 30);
            btnEdit.Margin = new Padding(3, 2, 3, 2);
            btnEdit.Name = "btnEdit";
            btnEdit.Size = new Size(131, 22);
            btnEdit.TabIndex = 2;
            btnEdit.Text = "Редактировать";
            btnEdit.UseVisualStyleBackColor = true;
            btnEdit.Click += BtnEdit_Click;
            // 
            // btnDelete
            // 
            btnDelete.Location = new Point(639, 60);
            btnDelete.Margin = new Padding(3, 2, 3, 2);
            btnDelete.Name = "btnDelete";
            btnDelete.Size = new Size(131, 22);
            btnDelete.TabIndex = 3;
            btnDelete.Text = "Удалить";
            btnDelete.UseVisualStyleBackColor = true;
            btnDelete.Click += BtnDelete_Click;
            // 
            // btnToggleActive
            // 
            btnToggleActive.Location = new Point(639, 90);
            btnToggleActive.Margin = new Padding(3, 2, 3, 2);
            btnToggleActive.Name = "btnToggleActive";
            btnToggleActive.Size = new Size(131, 22);
            btnToggleActive.TabIndex = 4;
            btnToggleActive.Text = "Активировать";
            btnToggleActive.UseVisualStyleBackColor = true;
            btnToggleActive.Click += BtnToggleActive_Click;
            // 
            // btnManageQuestions
            // 
            btnManageQuestions.Location = new Point(639, 120);
            btnManageQuestions.Margin = new Padding(3, 2, 3, 2);
            btnManageQuestions.Name = "btnManageQuestions";
            btnManageQuestions.Size = new Size(131, 39);
            btnManageQuestions.TabIndex = 5;
            btnManageQuestions.Text = "Управление вопросами";
            btnManageQuestions.UseVisualStyleBackColor = true;
            btnManageQuestions.Click += BtnManageQuestions_Click;
            // 
            // lblStats
            // 
            lblStats.AutoSize = true;
            lblStats.Font = new Font("Arial", 9F);
            lblStats.Location = new Point(18, 338);
            lblStats.Name = "lblStats";
            lblStats.Size = new Size(275, 15);
            lblStats.TabIndex = 6;
            lblStats.Text = "Всего тестов: 0 | Активных: 0 | Всего вопросов: 0";
            // 
            // TestManagementForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(875, 450);
            Controls.Add(lblStats);
            Controls.Add(btnManageQuestions);
            Controls.Add(btnToggleActive);
            Controls.Add(btnDelete);
            Controls.Add(btnEdit);
            Controls.Add(listViewTests);
            Controls.Add(toolStrip1);
            Margin = new Padding(3, 2, 3, 2);
            Name = "TestManagementForm";
            StartPosition = FormStartPosition.CenterParent;
            Text = "Управление тестами";
            toolStrip1.ResumeLayout(false);
            toolStrip1.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private ToolStrip toolStrip1;
        private ToolStripButton btnCreateTestTool;
        private ToolStripButton btnRefreshTool;
        private ListView listViewTests;
        private ColumnHeader columnHeader1;
        private ColumnHeader columnHeader2;
        private ColumnHeader columnHeader3;
        private ColumnHeader columnHeader4;
        private ColumnHeader columnHeader5;
        private ColumnHeader columnHeader6;
        private Button btnEdit;
        private Button btnDelete;
        private Button btnToggleActive;
        private Button btnManageQuestions;
        private Label lblStats;
    }
}