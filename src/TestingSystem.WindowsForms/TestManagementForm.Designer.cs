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
            components = new System.ComponentModel.Container();
            toolStrip = new ToolStrip();
            btnCreateTestTool = new ToolStripButton();
            btnRefreshTool = new ToolStripButton();
            tableLayout = new TableLayoutPanel();
            listViewTests = new ListView();
            columnHeader1 = new ColumnHeader();
            columnHeader2 = new ColumnHeader();
            columnHeader3 = new ColumnHeader();
            columnHeader4 = new ColumnHeader();
            columnHeader5 = new ColumnHeader();
            columnHeader6 = new ColumnHeader();
            buttonPanel = new FlowLayoutPanel();
            btnEdit = new Button();
            btnDelete = new Button();
            btnToggleActive = new Button();
            btnManageQuestions = new Button();
            btnStartTest = new Button();
            lblStats = new Label();
            toolTip1 = new ToolTip(components);
            toolStrip.SuspendLayout();
            tableLayout.SuspendLayout();
            buttonPanel.SuspendLayout();
            SuspendLayout();
            // 
            // toolStrip
            // 
            toolStrip.ImageScalingSize = new Size(20, 20);
            toolStrip.Items.AddRange(new ToolStripItem[] { btnCreateTestTool, btnRefreshTool });
            toolStrip.Location = new Point(0, 0);
            toolStrip.Name = "toolStrip";
            toolStrip.Size = new Size(900, 25);
            toolStrip.TabIndex = 0;
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
            // tableLayout
            // 
            tableLayout.ColumnCount = 2;
            tableLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 75F));
            tableLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25F));
            tableLayout.Controls.Add(listViewTests, 0, 0);
            tableLayout.Controls.Add(buttonPanel, 1, 0);
            tableLayout.Controls.Add(lblStats, 0, 1);
            tableLayout.Dock = DockStyle.Fill;
            tableLayout.Location = new Point(0, 25);
            tableLayout.Name = "tableLayout";
            tableLayout.Padding = new Padding(20);
            tableLayout.RowCount = 2;
            tableLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 85F));
            tableLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 15F));
            tableLayout.Size = new Size(900, 575);
            tableLayout.TabIndex = 1;
            // 
            // listViewTests
            // 
            listViewTests.Columns.AddRange(new ColumnHeader[] { columnHeader1, columnHeader2, columnHeader3, columnHeader4, columnHeader5, columnHeader6 });
            listViewTests.Dock = DockStyle.Fill;
            listViewTests.FullRowSelect = true;
            listViewTests.GridLines = true;
            listViewTests.Location = new Point(23, 23);
            listViewTests.Name = "listViewTests";
            listViewTests.Size = new Size(639, 448);
            listViewTests.TabIndex = 0;
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
            columnHeader5.Width = 120;
            // 
            // columnHeader6
            // 
            columnHeader6.Text = "Дата создания";
            columnHeader6.Width = 120;
            // 
            // buttonPanel
            // 
            buttonPanel.Controls.Add(btnEdit);
            buttonPanel.Controls.Add(btnDelete);
            buttonPanel.Controls.Add(btnToggleActive);
            buttonPanel.Controls.Add(btnManageQuestions);
            buttonPanel.Controls.Add(btnStartTest);
            buttonPanel.Dock = DockStyle.Top;
            buttonPanel.FlowDirection = FlowDirection.TopDown;
            buttonPanel.Location = new Point(668, 23);
            buttonPanel.Name = "buttonPanel";
            buttonPanel.Size = new Size(209, 200);
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
            // btnToggleActive
            // 
            btnToggleActive.AutoSize = true;
            btnToggleActive.Location = new Point(3, 75);
            btnToggleActive.Name = "btnToggleActive";
            btnToggleActive.Size = new Size(130, 30);
            btnToggleActive.TabIndex = 2;
            btnToggleActive.Text = "Активировать";
            btnToggleActive.UseVisualStyleBackColor = true;
            btnToggleActive.Click += BtnToggleActive_Click;
            // 
            // btnManageQuestions
            // 
            btnManageQuestions.AutoSize = true;
            btnManageQuestions.Location = new Point(3, 111);
            btnManageQuestions.Name = "btnManageQuestions";
            btnManageQuestions.Size = new Size(168, 30);
            btnManageQuestions.TabIndex = 3;
            btnManageQuestions.Text = "Управление вопросами";
            btnManageQuestions.UseVisualStyleBackColor = true;
            btnManageQuestions.Click += BtnManageQuestions_Click;
            // 
            // btnStartTest
            // 
            btnStartTest.AutoSize = true;
            btnStartTest.BackColor = SystemColors.Highlight;
            btnStartTest.FlatAppearance.BorderSize = 0;
            btnStartTest.FlatStyle = FlatStyle.Flat;
            btnStartTest.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            btnStartTest.ForeColor = Color.White;
            btnStartTest.Location = new Point(3, 147);
            btnStartTest.Name = "btnStartTest";
            btnStartTest.Size = new Size(130, 30);
            btnStartTest.TabIndex = 4;
            btnStartTest.Text = "НАЧАТЬ ТЕСТ";
            btnStartTest.UseVisualStyleBackColor = false;
            btnStartTest.Visible = false;
            btnStartTest.Click += BtnStartTest_Click;
            // 
            // lblStats
            // 
            lblStats.Dock = DockStyle.Fill;
            lblStats.Location = new Point(23, 474);
            lblStats.Name = "lblStats";
            lblStats.Size = new Size(639, 81);
            lblStats.TabIndex = 2;
            lblStats.Text = "Всего тестов: 0 | Активных: 0 | Всего вопросов: 0";
            lblStats.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // TestManagementForm
            // 
            BackColor = Color.White;
            ClientSize = new Size(900, 600);
            Controls.Add(tableLayout);
            Controls.Add(toolStrip);
            Font = new Font("Segoe UI", 10F);
            Name = "TestManagementForm";
            StartPosition = FormStartPosition.CenterParent;
            Text = "Управление тестами";
            toolStrip.ResumeLayout(false);
            toolStrip.PerformLayout();
            tableLayout.ResumeLayout(false);
            buttonPanel.ResumeLayout(false);
            buttonPanel.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private ToolStrip toolStrip;
        private ToolStripButton btnCreateTestTool;
        private ToolStripButton btnRefreshTool;
        private TableLayoutPanel tableLayout;
        private ListView listViewTests;
        private ColumnHeader columnHeader1;
        private ColumnHeader columnHeader2;
        private ColumnHeader columnHeader3;
        private ColumnHeader columnHeader4;
        private ColumnHeader columnHeader5;
        private ColumnHeader columnHeader6;
        private FlowLayoutPanel buttonPanel;
        private Button btnEdit;
        private Button btnDelete;
        private Button btnToggleActive;
        private Button btnManageQuestions;
        private Button btnStartTest;
        private Label lblStats;
        private ToolTip toolTip1;
    }
}