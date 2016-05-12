namespace Dynamo.Forms
{
    partial class CodeGeneratorForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CodeGeneratorForm));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            this.mainMenuStrip = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.newToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator = new System.Windows.Forms.ToolStripSeparator();
            this.saveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveAsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.recentFilesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.generateCodeFilesToolStripMenuItem = new Dynamo.Forms.BindableToolStripMenuItem();
            this.refreshToolStripMenuItem = new Dynamo.Forms.BindableToolStripMenuItem();
            this.shortFuzeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.solutionLinkLabel = new System.Windows.Forms.LinkLabel();
            this.viewModelBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.templateLinkLabel = new System.Windows.Forms.LinkLabel();
            this.generateButton = new System.Windows.Forms.Button();
            this.mainTabControl = new System.Windows.Forms.TabControl();
            this.tableTabPage = new System.Windows.Forms.TabPage();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.tablesDataGridView = new System.Windows.Forms.DataGridView();
            this.displayDataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.templatesDataGridView = new System.Windows.Forms.DataGridView();
            this.displayDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.fileNameDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.fileInfoDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.linesOfCodeTabPage = new System.Windows.Forms.TabPage();
            this.linesOfCodeStatusLabel = new System.Windows.Forms.Label();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.extensionDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.commentsDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.blankDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.codeDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.totalDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.linesOfCodeBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.connectionStringTextBox = new System.Windows.Forms.TextBox();
            this.dataProviderComboBox = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.connectionStringButton = new System.Windows.Forms.Button();
            this.mainToolTip = new System.Windows.Forms.ToolTip(this.components);
            this.progressBar = new System.Windows.Forms.ProgressBar();
            this.mainStatusStrip = new System.Windows.Forms.StatusStrip();
            this.generatorStatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.resultsTextBox = new System.Windows.Forms.TextBox();
            this.mainMenuStrip.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.viewModelBindingSource)).BeginInit();
            this.mainTabControl.SuspendLayout();
            this.tableTabPage.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tablesDataGridView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.templatesDataGridView)).BeginInit();
            this.linesOfCodeTabPage.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.linesOfCodeBindingSource)).BeginInit();
            this.mainStatusStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // mainMenuStrip
            // 
            this.mainMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.toolsToolStripMenuItem});
            this.mainMenuStrip.Location = new System.Drawing.Point(0, 0);
            this.mainMenuStrip.Name = "mainMenuStrip";
            this.mainMenuStrip.Size = new System.Drawing.Size(784, 24);
            this.mainMenuStrip.TabIndex = 0;
            this.mainMenuStrip.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.newToolStripMenuItem,
            this.openToolStripMenuItem,
            this.toolStripSeparator,
            this.saveToolStripMenuItem,
            this.saveAsToolStripMenuItem,
            this.toolStripSeparator1,
            this.recentFilesToolStripMenuItem,
            this.exitToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "&File";
            // 
            // newToolStripMenuItem
            // 
            this.newToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("newToolStripMenuItem.Image")));
            this.newToolStripMenuItem.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.newToolStripMenuItem.Name = "newToolStripMenuItem";
            this.newToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.N)));
            this.newToolStripMenuItem.Size = new System.Drawing.Size(146, 22);
            this.newToolStripMenuItem.Text = "&New";
            this.newToolStripMenuItem.Click += new System.EventHandler(this.newToolStripMenuItem_Click);
            // 
            // openToolStripMenuItem
            // 
            this.openToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("openToolStripMenuItem.Image")));
            this.openToolStripMenuItem.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.openToolStripMenuItem.Name = "openToolStripMenuItem";
            this.openToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.O)));
            this.openToolStripMenuItem.Size = new System.Drawing.Size(146, 22);
            this.openToolStripMenuItem.Text = "&Open";
            this.openToolStripMenuItem.Click += new System.EventHandler(this.openToolStripMenuItem_Click);
            // 
            // toolStripSeparator
            // 
            this.toolStripSeparator.Name = "toolStripSeparator";
            this.toolStripSeparator.Size = new System.Drawing.Size(143, 6);
            // 
            // saveToolStripMenuItem
            // 
            this.saveToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("saveToolStripMenuItem.Image")));
            this.saveToolStripMenuItem.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.saveToolStripMenuItem.Name = "saveToolStripMenuItem";
            this.saveToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.S)));
            this.saveToolStripMenuItem.Size = new System.Drawing.Size(146, 22);
            this.saveToolStripMenuItem.Text = "&Save";
            this.saveToolStripMenuItem.Click += new System.EventHandler(this.saveToolStripMenuItem_Click);
            // 
            // saveAsToolStripMenuItem
            // 
            this.saveAsToolStripMenuItem.Name = "saveAsToolStripMenuItem";
            this.saveAsToolStripMenuItem.Size = new System.Drawing.Size(146, 22);
            this.saveAsToolStripMenuItem.Text = "Save &As";
            this.saveAsToolStripMenuItem.Click += new System.EventHandler(this.saveAsToolStripMenuItem_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(143, 6);
            // 
            // recentFilesToolStripMenuItem
            // 
            this.recentFilesToolStripMenuItem.Name = "recentFilesToolStripMenuItem";
            this.recentFilesToolStripMenuItem.Size = new System.Drawing.Size(146, 22);
            this.recentFilesToolStripMenuItem.Text = "&Recent Files";
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(146, 22);
            this.exitToolStripMenuItem.Text = "E&xit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // toolsToolStripMenuItem
            // 
            this.toolsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.generateCodeFilesToolStripMenuItem,
            this.refreshToolStripMenuItem,
            this.shortFuzeToolStripMenuItem});
            this.toolsToolStripMenuItem.Name = "toolsToolStripMenuItem";
            this.toolsToolStripMenuItem.Size = new System.Drawing.Size(47, 20);
            this.toolsToolStripMenuItem.Text = "&Tools";
            // 
            // generateCodeFilesToolStripMenuItem
            // 
            this.generateCodeFilesToolStripMenuItem.Name = "generateCodeFilesToolStripMenuItem";
            this.generateCodeFilesToolStripMenuItem.ShortcutKeys = System.Windows.Forms.Keys.F5;
            this.generateCodeFilesToolStripMenuItem.Size = new System.Drawing.Size(233, 22);
            this.generateCodeFilesToolStripMenuItem.Text = "&Generate Code Files...";
            this.generateCodeFilesToolStripMenuItem.Click += new System.EventHandler(this.generateCodeFilesToolStripMenuItem_Click);
            // 
            // refreshToolStripMenuItem
            // 
            this.refreshToolStripMenuItem.Name = "refreshToolStripMenuItem";
            this.refreshToolStripMenuItem.ShortcutKeys = System.Windows.Forms.Keys.F7;
            this.refreshToolStripMenuItem.Size = new System.Drawing.Size(233, 22);
            this.refreshToolStripMenuItem.Text = "&Refresh Tables / Templates";
            this.refreshToolStripMenuItem.Click += new System.EventHandler(this.refreshToolStripMenuItem_Click);
            // 
            // shortFuzeToolStripMenuItem
            // 
            this.shortFuzeToolStripMenuItem.Name = "shortFuzeToolStripMenuItem";
            this.shortFuzeToolStripMenuItem.Size = new System.Drawing.Size(233, 22);
            this.shortFuzeToolStripMenuItem.Text = "Short Fuze...";
            this.shortFuzeToolStripMenuItem.Click += new System.EventHandler(this.shortFuzeToolStripMenuItem_Click);
            // 
            // solutionLinkLabel
            // 
            this.solutionLinkLabel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.solutionLinkLabel.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.solutionLinkLabel.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.viewModelBindingSource, "SolutionFile", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.solutionLinkLabel.Location = new System.Drawing.Point(116, 82);
            this.solutionLinkLabel.Name = "solutionLinkLabel";
            this.solutionLinkLabel.Size = new System.Drawing.Size(656, 23);
            this.solutionLinkLabel.TabIndex = 2;
            this.solutionLinkLabel.TabStop = true;
            this.solutionLinkLabel.Text = "linkLabel1";
            this.solutionLinkLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.solutionLinkLabel.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.solutionLinkLabel_LinkClicked);
            // 
            // viewModelBindingSource
            // 
            this.viewModelBindingSource.DataSource = typeof(Dynamo.Core.GeneratorViewModel);
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(10, 82);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(100, 23);
            this.label1.TabIndex = 3;
            this.label1.Text = "Solution File";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.mainToolTip.SetToolTip(this.label1, "The Solution File serves as the generator\'s root");
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(10, 114);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(100, 23);
            this.label2.TabIndex = 4;
            this.label2.Text = "Template Folder";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.mainToolTip.SetToolTip(this.label2, "Folder where templates reside");
            // 
            // templateLinkLabel
            // 
            this.templateLinkLabel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.templateLinkLabel.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.templateLinkLabel.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.viewModelBindingSource, "TemplateFolder", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.templateLinkLabel.Location = new System.Drawing.Point(116, 114);
            this.templateLinkLabel.Name = "templateLinkLabel";
            this.templateLinkLabel.Size = new System.Drawing.Size(656, 23);
            this.templateLinkLabel.TabIndex = 5;
            this.templateLinkLabel.TabStop = true;
            this.templateLinkLabel.Text = "linkLabel1";
            this.templateLinkLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.templateLinkLabel.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.templateLinkLabel_LinkClicked);
            // 
            // generateButton
            // 
            this.generateButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.generateButton.DataBindings.Add(new System.Windows.Forms.Binding("Enabled", this.viewModelBindingSource, "CanGenerate", true));
            this.generateButton.Location = new System.Drawing.Point(618, 157);
            this.generateButton.Name = "generateButton";
            this.generateButton.Size = new System.Drawing.Size(154, 23);
            this.generateButton.TabIndex = 6;
            this.generateButton.Text = "Generate Code Files ...";
            this.generateButton.UseVisualStyleBackColor = true;
            this.generateButton.Click += new System.EventHandler(this.generateButton_Click);
            // 
            // mainTabControl
            // 
            this.mainTabControl.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.mainTabControl.Controls.Add(this.tableTabPage);
            this.mainTabControl.Controls.Add(this.linesOfCodeTabPage);
            this.mainTabControl.Location = new System.Drawing.Point(13, 186);
            this.mainTabControl.Name = "mainTabControl";
            this.mainTabControl.SelectedIndex = 0;
            this.mainTabControl.Size = new System.Drawing.Size(759, 178);
            this.mainTabControl.TabIndex = 8;
            // 
            // tableTabPage
            // 
            this.tableTabPage.Controls.Add(this.splitContainer1);
            this.tableTabPage.Location = new System.Drawing.Point(4, 22);
            this.tableTabPage.Name = "tableTabPage";
            this.tableTabPage.Padding = new System.Windows.Forms.Padding(3);
            this.tableTabPage.Size = new System.Drawing.Size(751, 152);
            this.tableTabPage.TabIndex = 0;
            this.tableTabPage.Text = "Generator";
            this.tableTabPage.UseVisualStyleBackColor = true;
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(3, 3);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.tablesDataGridView);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.templatesDataGridView);
            this.splitContainer1.Size = new System.Drawing.Size(745, 146);
            this.splitContainer1.SplitterDistance = 392;
            this.splitContainer1.TabIndex = 1;
            // 
            // tablesDataGridView
            // 
            this.tablesDataGridView.AllowUserToAddRows = false;
            this.tablesDataGridView.AllowUserToDeleteRows = false;
            this.tablesDataGridView.AutoGenerateColumns = false;
            this.tablesDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.tablesDataGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.displayDataGridViewTextBoxColumn1});
            this.tablesDataGridView.DataMember = "Tables";
            this.tablesDataGridView.DataSource = this.viewModelBindingSource;
            this.tablesDataGridView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tablesDataGridView.Location = new System.Drawing.Point(0, 0);
            this.tablesDataGridView.Name = "tablesDataGridView";
            this.tablesDataGridView.ReadOnly = true;
            this.tablesDataGridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.tablesDataGridView.Size = new System.Drawing.Size(392, 146);
            this.tablesDataGridView.TabIndex = 0;
            // 
            // displayDataGridViewTextBoxColumn1
            // 
            this.displayDataGridViewTextBoxColumn1.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.displayDataGridViewTextBoxColumn1.DataPropertyName = "Name";
            this.displayDataGridViewTextBoxColumn1.HeaderText = "Select Tables to Use When Generating";
            this.displayDataGridViewTextBoxColumn1.Name = "displayDataGridViewTextBoxColumn1";
            this.displayDataGridViewTextBoxColumn1.ReadOnly = true;
            // 
            // templatesDataGridView
            // 
            this.templatesDataGridView.AllowUserToAddRows = false;
            this.templatesDataGridView.AllowUserToDeleteRows = false;
            this.templatesDataGridView.AutoGenerateColumns = false;
            this.templatesDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.templatesDataGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.displayDataGridViewTextBoxColumn,
            this.fileNameDataGridViewTextBoxColumn,
            this.fileInfoDataGridViewTextBoxColumn});
            this.templatesDataGridView.DataMember = "Templates";
            this.templatesDataGridView.DataSource = this.viewModelBindingSource;
            this.templatesDataGridView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.templatesDataGridView.Location = new System.Drawing.Point(0, 0);
            this.templatesDataGridView.Name = "templatesDataGridView";
            this.templatesDataGridView.ReadOnly = true;
            this.templatesDataGridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.templatesDataGridView.Size = new System.Drawing.Size(349, 146);
            this.templatesDataGridView.TabIndex = 3;
            // 
            // displayDataGridViewTextBoxColumn
            // 
            this.displayDataGridViewTextBoxColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.displayDataGridViewTextBoxColumn.DataPropertyName = "Display";
            this.displayDataGridViewTextBoxColumn.HeaderText = "Select Template Files to Use";
            this.displayDataGridViewTextBoxColumn.Name = "displayDataGridViewTextBoxColumn";
            this.displayDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // fileNameDataGridViewTextBoxColumn
            // 
            this.fileNameDataGridViewTextBoxColumn.DataPropertyName = "FileName";
            this.fileNameDataGridViewTextBoxColumn.HeaderText = "FileName";
            this.fileNameDataGridViewTextBoxColumn.Name = "fileNameDataGridViewTextBoxColumn";
            this.fileNameDataGridViewTextBoxColumn.ReadOnly = true;
            this.fileNameDataGridViewTextBoxColumn.Visible = false;
            // 
            // fileInfoDataGridViewTextBoxColumn
            // 
            this.fileInfoDataGridViewTextBoxColumn.DataPropertyName = "FileInfo";
            this.fileInfoDataGridViewTextBoxColumn.HeaderText = "FileInfo";
            this.fileInfoDataGridViewTextBoxColumn.Name = "fileInfoDataGridViewTextBoxColumn";
            this.fileInfoDataGridViewTextBoxColumn.ReadOnly = true;
            this.fileInfoDataGridViewTextBoxColumn.Visible = false;
            // 
            // linesOfCodeTabPage
            // 
            this.linesOfCodeTabPage.Controls.Add(this.linesOfCodeStatusLabel);
            this.linesOfCodeTabPage.Controls.Add(this.dataGridView1);
            this.linesOfCodeTabPage.Location = new System.Drawing.Point(4, 22);
            this.linesOfCodeTabPage.Name = "linesOfCodeTabPage";
            this.linesOfCodeTabPage.Size = new System.Drawing.Size(751, 152);
            this.linesOfCodeTabPage.TabIndex = 2;
            this.linesOfCodeTabPage.Text = "Lines of Code";
            this.linesOfCodeTabPage.UseVisualStyleBackColor = true;
            // 
            // linesOfCodeStatusLabel
            // 
            this.linesOfCodeStatusLabel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.linesOfCodeStatusLabel.Location = new System.Drawing.Point(3, 186);
            this.linesOfCodeStatusLabel.Name = "linesOfCodeStatusLabel";
            this.linesOfCodeStatusLabel.Size = new System.Drawing.Size(742, 23);
            this.linesOfCodeStatusLabel.TabIndex = 14;
            this.linesOfCodeStatusLabel.Text = "...";
            this.linesOfCodeStatusLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.mainToolTip.SetToolTip(this.linesOfCodeStatusLabel, "Connection String to connect to your ADO.NET data source");
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridView1.AutoGenerateColumns = false;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.extensionDataGridViewTextBoxColumn,
            this.commentsDataGridViewTextBoxColumn,
            this.blankDataGridViewTextBoxColumn,
            this.codeDataGridViewTextBoxColumn,
            this.totalDataGridViewTextBoxColumn});
            this.dataGridView1.DataMember = "LinesOfCodeMetrics";
            this.dataGridView1.DataSource = this.linesOfCodeBindingSource;
            this.dataGridView1.Location = new System.Drawing.Point(6, 6);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.Size = new System.Drawing.Size(739, 177);
            this.dataGridView1.TabIndex = 0;
            // 
            // extensionDataGridViewTextBoxColumn
            // 
            this.extensionDataGridViewTextBoxColumn.DataPropertyName = "Extension";
            this.extensionDataGridViewTextBoxColumn.HeaderText = "Extension";
            this.extensionDataGridViewTextBoxColumn.Name = "extensionDataGridViewTextBoxColumn";
            this.extensionDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // commentsDataGridViewTextBoxColumn
            // 
            this.commentsDataGridViewTextBoxColumn.DataPropertyName = "Comments";
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle1.Format = "N0";
            this.commentsDataGridViewTextBoxColumn.DefaultCellStyle = dataGridViewCellStyle1;
            this.commentsDataGridViewTextBoxColumn.HeaderText = "Comments";
            this.commentsDataGridViewTextBoxColumn.Name = "commentsDataGridViewTextBoxColumn";
            this.commentsDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // blankDataGridViewTextBoxColumn
            // 
            this.blankDataGridViewTextBoxColumn.DataPropertyName = "Blank";
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle2.Format = "N0";
            this.blankDataGridViewTextBoxColumn.DefaultCellStyle = dataGridViewCellStyle2;
            this.blankDataGridViewTextBoxColumn.HeaderText = "Blank";
            this.blankDataGridViewTextBoxColumn.Name = "blankDataGridViewTextBoxColumn";
            this.blankDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // codeDataGridViewTextBoxColumn
            // 
            this.codeDataGridViewTextBoxColumn.DataPropertyName = "Code";
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle3.Format = "N0";
            this.codeDataGridViewTextBoxColumn.DefaultCellStyle = dataGridViewCellStyle3;
            this.codeDataGridViewTextBoxColumn.HeaderText = "Code";
            this.codeDataGridViewTextBoxColumn.Name = "codeDataGridViewTextBoxColumn";
            this.codeDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // totalDataGridViewTextBoxColumn
            // 
            this.totalDataGridViewTextBoxColumn.DataPropertyName = "Total";
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle4.Format = "N0";
            this.totalDataGridViewTextBoxColumn.DefaultCellStyle = dataGridViewCellStyle4;
            this.totalDataGridViewTextBoxColumn.HeaderText = "Total";
            this.totalDataGridViewTextBoxColumn.Name = "totalDataGridViewTextBoxColumn";
            this.totalDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // linesOfCodeBindingSource
            // 
            this.linesOfCodeBindingSource.DataSource = typeof(Dynamo.Core.LinesOfCodeViewModel);
            // 
            // connectionStringTextBox
            // 
            this.connectionStringTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.connectionStringTextBox.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.viewModelBindingSource, "ConnectionString", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.connectionStringTextBox.Location = new System.Drawing.Point(116, 54);
            this.connectionStringTextBox.Name = "connectionStringTextBox";
            this.connectionStringTextBox.Size = new System.Drawing.Size(620, 20);
            this.connectionStringTextBox.TabIndex = 12;
            // 
            // dataProviderComboBox
            // 
            this.dataProviderComboBox.DataBindings.Add(new System.Windows.Forms.Binding("SelectedItem", this.viewModelBindingSource, "DataProvider", true));
            this.dataProviderComboBox.DisplayMember = "dbo";
            this.dataProviderComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.dataProviderComboBox.FormattingEnabled = true;
            this.dataProviderComboBox.Location = new System.Drawing.Point(116, 27);
            this.dataProviderComboBox.Name = "dataProviderComboBox";
            this.dataProviderComboBox.Size = new System.Drawing.Size(305, 21);
            this.dataProviderComboBox.TabIndex = 9;
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(10, 52);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(100, 23);
            this.label3.TabIndex = 13;
            this.label3.Text = "Connection String";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.mainToolTip.SetToolTip(this.label3, "Connection String to connect to your ADO.NET data source");
            // 
            // label4
            // 
            this.label4.Location = new System.Drawing.Point(10, 25);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(100, 23);
            this.label4.TabIndex = 14;
            this.label4.Text = "Data Provider";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.mainToolTip.SetToolTip(this.label4, "The Provider to use with your connection string");
            // 
            // connectionStringButton
            // 
            this.connectionStringButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.connectionStringButton.Location = new System.Drawing.Point(742, 52);
            this.connectionStringButton.Name = "connectionStringButton";
            this.connectionStringButton.Size = new System.Drawing.Size(30, 23);
            this.connectionStringButton.TabIndex = 15;
            this.mainToolTip.SetToolTip(this.connectionStringButton, "Show Connection String Builder");
            this.connectionStringButton.UseVisualStyleBackColor = true;
            this.connectionStringButton.Click += new System.EventHandler(this.connectionStringButton_Click);
            // 
            // progressBar
            // 
            this.progressBar.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.progressBar.DataBindings.Add(new System.Windows.Forms.Binding("Value", this.viewModelBindingSource, "Current", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.progressBar.DataBindings.Add(new System.Windows.Forms.Binding("Maximum", this.viewModelBindingSource, "Total", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.progressBar.Location = new System.Drawing.Point(116, 163);
            this.progressBar.Name = "progressBar";
            this.progressBar.Size = new System.Drawing.Size(481, 10);
            this.progressBar.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
            this.progressBar.TabIndex = 7;
            // 
            // mainStatusStrip
            // 
            this.mainStatusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.generatorStatusLabel});
            this.mainStatusStrip.Location = new System.Drawing.Point(0, 440);
            this.mainStatusStrip.Name = "mainStatusStrip";
            this.mainStatusStrip.Size = new System.Drawing.Size(784, 22);
            this.mainStatusStrip.TabIndex = 16;
            this.mainStatusStrip.Text = "statusStrip1";
            // 
            // generatorStatusLabel
            // 
            this.generatorStatusLabel.AutoSize = false;
            this.generatorStatusLabel.Name = "generatorStatusLabel";
            this.generatorStatusLabel.Size = new System.Drawing.Size(769, 17);
            this.generatorStatusLabel.Spring = true;
            this.generatorStatusLabel.Text = "...";
            this.generatorStatusLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // resultsTextBox
            // 
            this.resultsTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.resultsTextBox.Location = new System.Drawing.Point(17, 370);
            this.resultsTextBox.Multiline = true;
            this.resultsTextBox.Name = "resultsTextBox";
            this.resultsTextBox.ReadOnly = true;
            this.resultsTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.resultsTextBox.Size = new System.Drawing.Size(748, 67);
            this.resultsTextBox.TabIndex = 17;
            // 
            // CodeGeneratorForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(784, 462);
            this.Controls.Add(this.resultsTextBox);
            this.Controls.Add(this.mainStatusStrip);
            this.Controls.Add(this.connectionStringButton);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.connectionStringTextBox);
            this.Controls.Add(this.dataProviderComboBox);
            this.Controls.Add(this.mainTabControl);
            this.Controls.Add(this.progressBar);
            this.Controls.Add(this.generateButton);
            this.Controls.Add(this.templateLinkLabel);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.solutionLinkLabel);
            this.Controls.Add(this.mainMenuStrip);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.mainMenuStrip;
            this.MinimumSize = new System.Drawing.Size(800, 500);
            this.Name = "CodeGeneratorForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Code Geneator";
            this.mainMenuStrip.ResumeLayout(false);
            this.mainMenuStrip.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.viewModelBindingSource)).EndInit();
            this.mainTabControl.ResumeLayout(false);
            this.tableTabPage.ResumeLayout(false);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.tablesDataGridView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.templatesDataGridView)).EndInit();
            this.linesOfCodeTabPage.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.linesOfCodeBindingSource)).EndInit();
            this.mainStatusStrip.ResumeLayout(false);
            this.mainStatusStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip mainMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem newToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator;
        private System.Windows.Forms.ToolStripMenuItem saveToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveAsToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem toolsToolStripMenuItem;
        private System.Windows.Forms.LinkLabel solutionLinkLabel;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.LinkLabel templateLinkLabel;
        private BindableToolStripMenuItem generateCodeFilesToolStripMenuItem;
        private System.Windows.Forms.Button generateButton;
        private BindableToolStripMenuItem refreshToolStripMenuItem;
        private System.Windows.Forms.TabControl mainTabControl;
        private System.Windows.Forms.TabPage tableTabPage;
        private System.Windows.Forms.BindingSource viewModelBindingSource;
        private System.Windows.Forms.DataGridView tablesDataGridView;
        private System.Windows.Forms.DataGridView templatesDataGridView;
        private System.Windows.Forms.TextBox connectionStringTextBox;
        private System.Windows.Forms.ComboBox dataProviderComboBox;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button connectionStringButton;
        private System.Windows.Forms.DataGridViewTextBoxColumn displayDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn fileNameDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn fileInfoDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn displayDataGridViewTextBoxColumn1;
        private System.Windows.Forms.ToolTip mainToolTip;
        private System.Windows.Forms.TabPage linesOfCodeTabPage;
        private System.Windows.Forms.BindingSource linesOfCodeBindingSource;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.DataGridViewTextBoxColumn extensionDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn commentsDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn blankDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn codeDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn totalDataGridViewTextBoxColumn;
        private System.Windows.Forms.ProgressBar progressBar;
        private System.Windows.Forms.Label linesOfCodeStatusLabel;
        private System.Windows.Forms.StatusStrip mainStatusStrip;
        private System.Windows.Forms.ToolStripStatusLabel generatorStatusLabel;
        private System.Windows.Forms.ToolStripMenuItem recentFilesToolStripMenuItem;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.TextBox resultsTextBox;
        private System.Windows.Forms.ToolStripMenuItem shortFuzeToolStripMenuItem;
    }
}