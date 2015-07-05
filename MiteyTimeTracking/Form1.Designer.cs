using System.ComponentModel;
using System.Windows.Forms;

namespace MiteyTimeTracking
{
	partial class Form1
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private IContainer components = null;

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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
			this.mainMenu = new System.Windows.Forms.MenuStrip();
			this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.newToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.openToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator = new System.Windows.Forms.ToolStripSeparator();
			this.saveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.saveAsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
			this.printToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.printPreviewToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
			this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.editToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.undoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.redoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
			this.cutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.copyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.pasteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
			this.selectAllToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.toolsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.customizeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.optionsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.viewToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.showOrHideMenuToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.showOrHideLeftPanelToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.showhideBottomPanelToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.showOrHideRightPanelToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.contentsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.indexToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.searchToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
			this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.splitContainer1 = new System.Windows.Forms.SplitContainer();
			this.monthCalendar1 = new System.Windows.Forms.MonthCalendar();
			this.splitContainerBottom = new System.Windows.Forms.SplitContainer();
			this.splitContainerRight = new System.Windows.Forms.SplitContainer();
			this.mainTextBox = new System.Windows.Forms.RichTextBox();
			this.RichTextBox2 = new System.Windows.Forms.RichTextBox();
			this.RichTextBox3 = new System.Windows.Forms.RichTextBox();
			this.btn_mainMenu = new System.Windows.Forms.Button();
			this.btn_panelLeft = new System.Windows.Forms.Button();
			this.btn_panelRight = new System.Windows.Forms.Button();
			this.btn_reloadTodos = new System.Windows.Forms.Button();
			this.btn_panelBottom = new System.Windows.Forms.Button();
			this.showhodeButtonsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.mainMenu.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
			this.splitContainer1.Panel1.SuspendLayout();
			this.splitContainer1.Panel2.SuspendLayout();
			this.splitContainer1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.splitContainerBottom)).BeginInit();
			this.splitContainerBottom.Panel1.SuspendLayout();
			this.splitContainerBottom.Panel2.SuspendLayout();
			this.splitContainerBottom.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.splitContainerRight)).BeginInit();
			this.splitContainerRight.Panel1.SuspendLayout();
			this.splitContainerRight.Panel2.SuspendLayout();
			this.splitContainerRight.SuspendLayout();
			this.SuspendLayout();
			// 
			// mainMenu
			// 
			this.mainMenu.ImageScalingSize = new System.Drawing.Size(32, 32);
			this.mainMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.editToolStripMenuItem,
            this.toolsToolStripMenuItem,
            this.viewToolStripMenuItem,
            this.helpToolStripMenuItem});
			this.mainMenu.Location = new System.Drawing.Point(0, 0);
			this.mainMenu.Name = "mainMenu";
			this.mainMenu.Size = new System.Drawing.Size(1354, 42);
			this.mainMenu.TabIndex = 0;
			this.mainMenu.Text = "mainMenu";
			this.mainMenu.Visible = false;
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
            this.printToolStripMenuItem,
            this.printPreviewToolStripMenuItem,
            this.toolStripSeparator2,
            this.exitToolStripMenuItem});
			this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
			this.fileToolStripMenuItem.Size = new System.Drawing.Size(64, 38);
			this.fileToolStripMenuItem.Text = "&File";
			// 
			// newToolStripMenuItem
			// 
			this.newToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("newToolStripMenuItem.Image")));
			this.newToolStripMenuItem.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.newToolStripMenuItem.Name = "newToolStripMenuItem";
			this.newToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.N)));
			this.newToolStripMenuItem.Size = new System.Drawing.Size(251, 38);
			this.newToolStripMenuItem.Text = "&New";
			// 
			// openToolStripMenuItem
			// 
			this.openToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("openToolStripMenuItem.Image")));
			this.openToolStripMenuItem.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.openToolStripMenuItem.Name = "openToolStripMenuItem";
			this.openToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.O)));
			this.openToolStripMenuItem.Size = new System.Drawing.Size(251, 38);
			this.openToolStripMenuItem.Text = "&Open";
			// 
			// toolStripSeparator
			// 
			this.toolStripSeparator.Name = "toolStripSeparator";
			this.toolStripSeparator.Size = new System.Drawing.Size(248, 6);
			// 
			// saveToolStripMenuItem
			// 
			this.saveToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("saveToolStripMenuItem.Image")));
			this.saveToolStripMenuItem.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.saveToolStripMenuItem.Name = "saveToolStripMenuItem";
			this.saveToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.S)));
			this.saveToolStripMenuItem.Size = new System.Drawing.Size(251, 38);
			this.saveToolStripMenuItem.Text = "&Save";
			// 
			// saveAsToolStripMenuItem
			// 
			this.saveAsToolStripMenuItem.Name = "saveAsToolStripMenuItem";
			this.saveAsToolStripMenuItem.Size = new System.Drawing.Size(251, 38);
			this.saveAsToolStripMenuItem.Text = "Save &As";
			// 
			// toolStripSeparator1
			// 
			this.toolStripSeparator1.Name = "toolStripSeparator1";
			this.toolStripSeparator1.Size = new System.Drawing.Size(248, 6);
			// 
			// printToolStripMenuItem
			// 
			this.printToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("printToolStripMenuItem.Image")));
			this.printToolStripMenuItem.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.printToolStripMenuItem.Name = "printToolStripMenuItem";
			this.printToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.P)));
			this.printToolStripMenuItem.Size = new System.Drawing.Size(251, 38);
			this.printToolStripMenuItem.Text = "&Print";
			// 
			// printPreviewToolStripMenuItem
			// 
			this.printPreviewToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("printPreviewToolStripMenuItem.Image")));
			this.printPreviewToolStripMenuItem.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.printPreviewToolStripMenuItem.Name = "printPreviewToolStripMenuItem";
			this.printPreviewToolStripMenuItem.Size = new System.Drawing.Size(251, 38);
			this.printPreviewToolStripMenuItem.Text = "Print Pre&view";
			// 
			// toolStripSeparator2
			// 
			this.toolStripSeparator2.Name = "toolStripSeparator2";
			this.toolStripSeparator2.Size = new System.Drawing.Size(248, 6);
			// 
			// exitToolStripMenuItem
			// 
			this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
			this.exitToolStripMenuItem.Size = new System.Drawing.Size(251, 38);
			this.exitToolStripMenuItem.Text = "E&xit";
			// 
			// editToolStripMenuItem
			// 
			this.editToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.undoToolStripMenuItem,
            this.redoToolStripMenuItem,
            this.toolStripSeparator3,
            this.cutToolStripMenuItem,
            this.copyToolStripMenuItem,
            this.pasteToolStripMenuItem,
            this.toolStripSeparator4,
            this.selectAllToolStripMenuItem});
			this.editToolStripMenuItem.Name = "editToolStripMenuItem";
			this.editToolStripMenuItem.Size = new System.Drawing.Size(67, 38);
			this.editToolStripMenuItem.Text = "&Edit";
			// 
			// undoToolStripMenuItem
			// 
			this.undoToolStripMenuItem.Name = "undoToolStripMenuItem";
			this.undoToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Z)));
			this.undoToolStripMenuItem.Size = new System.Drawing.Size(246, 38);
			this.undoToolStripMenuItem.Text = "&Undo";
			this.undoToolStripMenuItem.Click += new System.EventHandler(this.undoToolStripMenuItem_Click);
			// 
			// redoToolStripMenuItem
			// 
			this.redoToolStripMenuItem.Name = "redoToolStripMenuItem";
			this.redoToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Y)));
			this.redoToolStripMenuItem.Size = new System.Drawing.Size(246, 38);
			this.redoToolStripMenuItem.Text = "&Redo";
			this.redoToolStripMenuItem.Click += new System.EventHandler(this.redoToolStripMenuItem_Click);
			// 
			// toolStripSeparator3
			// 
			this.toolStripSeparator3.Name = "toolStripSeparator3";
			this.toolStripSeparator3.Size = new System.Drawing.Size(243, 6);
			// 
			// cutToolStripMenuItem
			// 
			this.cutToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("cutToolStripMenuItem.Image")));
			this.cutToolStripMenuItem.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.cutToolStripMenuItem.Name = "cutToolStripMenuItem";
			this.cutToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.X)));
			this.cutToolStripMenuItem.Size = new System.Drawing.Size(246, 38);
			this.cutToolStripMenuItem.Text = "Cu&t";
			this.cutToolStripMenuItem.Click += new System.EventHandler(this.cutToolStripMenuItem_Click);
			// 
			// copyToolStripMenuItem
			// 
			this.copyToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("copyToolStripMenuItem.Image")));
			this.copyToolStripMenuItem.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.copyToolStripMenuItem.Name = "copyToolStripMenuItem";
			this.copyToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.C)));
			this.copyToolStripMenuItem.Size = new System.Drawing.Size(246, 38);
			this.copyToolStripMenuItem.Text = "&Copy";
			this.copyToolStripMenuItem.Click += new System.EventHandler(this.copyToolStripMenuItem_Click);
			// 
			// pasteToolStripMenuItem
			// 
			this.pasteToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("pasteToolStripMenuItem.Image")));
			this.pasteToolStripMenuItem.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.pasteToolStripMenuItem.Name = "pasteToolStripMenuItem";
			this.pasteToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.V)));
			this.pasteToolStripMenuItem.Size = new System.Drawing.Size(246, 38);
			this.pasteToolStripMenuItem.Text = "&Paste";
			this.pasteToolStripMenuItem.Click += new System.EventHandler(this.pasteToolStripMenuItem_Click);
			// 
			// toolStripSeparator4
			// 
			this.toolStripSeparator4.Name = "toolStripSeparator4";
			this.toolStripSeparator4.Size = new System.Drawing.Size(243, 6);
			// 
			// selectAllToolStripMenuItem
			// 
			this.selectAllToolStripMenuItem.Name = "selectAllToolStripMenuItem";
			this.selectAllToolStripMenuItem.Size = new System.Drawing.Size(246, 38);
			this.selectAllToolStripMenuItem.Text = "Select &All";
			this.selectAllToolStripMenuItem.Click += new System.EventHandler(this.selectAllToolStripMenuItem_Click);
			// 
			// toolsToolStripMenuItem
			// 
			this.toolsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.customizeToolStripMenuItem,
            this.optionsToolStripMenuItem});
			this.toolsToolStripMenuItem.Name = "toolsToolStripMenuItem";
			this.toolsToolStripMenuItem.Size = new System.Drawing.Size(84, 38);
			this.toolsToolStripMenuItem.Text = "&Tools";
			// 
			// customizeToolStripMenuItem
			// 
			this.customizeToolStripMenuItem.Name = "customizeToolStripMenuItem";
			this.customizeToolStripMenuItem.Size = new System.Drawing.Size(202, 36);
			this.customizeToolStripMenuItem.Text = "&Customize";
			// 
			// optionsToolStripMenuItem
			// 
			this.optionsToolStripMenuItem.Name = "optionsToolStripMenuItem";
			this.optionsToolStripMenuItem.Size = new System.Drawing.Size(202, 36);
			this.optionsToolStripMenuItem.Text = "&Options";
			// 
			// viewToolStripMenuItem
			// 
			this.viewToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.showOrHideMenuToolStripMenuItem,
            this.showOrHideLeftPanelToolStripMenuItem,
            this.showhideBottomPanelToolStripMenuItem,
            this.showOrHideRightPanelToolStripMenuItem,
            this.showhodeButtonsToolStripMenuItem});
			this.viewToolStripMenuItem.Name = "viewToolStripMenuItem";
			this.viewToolStripMenuItem.Size = new System.Drawing.Size(78, 38);
			this.viewToolStripMenuItem.Text = "View";
			// 
			// showOrHideMenuToolStripMenuItem
			// 
			this.showOrHideMenuToolStripMenuItem.Name = "showOrHideMenuToolStripMenuItem";
			this.showOrHideMenuToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.M)));
			this.showOrHideMenuToolStripMenuItem.Size = new System.Drawing.Size(429, 36);
			this.showOrHideMenuToolStripMenuItem.Text = "Show/hide menu";
			this.showOrHideMenuToolStripMenuItem.Click += new System.EventHandler(this.showOrHideMenuToolStripMenuItem_Click);
			// 
			// showOrHideLeftPanelToolStripMenuItem
			// 
			this.showOrHideLeftPanelToolStripMenuItem.Name = "showOrHideLeftPanelToolStripMenuItem";
			this.showOrHideLeftPanelToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.D1)));
			this.showOrHideLeftPanelToolStripMenuItem.Size = new System.Drawing.Size(429, 36);
			this.showOrHideLeftPanelToolStripMenuItem.Text = "Show/hide left panel";
			this.showOrHideLeftPanelToolStripMenuItem.Click += new System.EventHandler(this.showOrHideLeftPanelToolStripMenuItem_Click);
			// 
			// showhideBottomPanelToolStripMenuItem
			// 
			this.showhideBottomPanelToolStripMenuItem.Name = "showhideBottomPanelToolStripMenuItem";
			this.showhideBottomPanelToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.D2)));
			this.showhideBottomPanelToolStripMenuItem.Size = new System.Drawing.Size(429, 36);
			this.showhideBottomPanelToolStripMenuItem.Text = "Show/hide bottom panel";
			this.showhideBottomPanelToolStripMenuItem.Click += new System.EventHandler(this.showhideBottomPanelToolStripMenuItem_Click);
			// 
			// showOrHideRightPanelToolStripMenuItem
			// 
			this.showOrHideRightPanelToolStripMenuItem.Name = "showOrHideRightPanelToolStripMenuItem";
			this.showOrHideRightPanelToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.D3)));
			this.showOrHideRightPanelToolStripMenuItem.Size = new System.Drawing.Size(429, 36);
			this.showOrHideRightPanelToolStripMenuItem.Text = "Show/hide right panel";
			this.showOrHideRightPanelToolStripMenuItem.Click += new System.EventHandler(this.showOrHideRightPanelToolStripMenuItem_Click);
			// 
			// helpToolStripMenuItem
			// 
			this.helpToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.contentsToolStripMenuItem,
            this.indexToolStripMenuItem,
            this.searchToolStripMenuItem,
            this.toolStripSeparator5,
            this.aboutToolStripMenuItem});
			this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
			this.helpToolStripMenuItem.Size = new System.Drawing.Size(77, 38);
			this.helpToolStripMenuItem.Text = "&Help";
			// 
			// contentsToolStripMenuItem
			// 
			this.contentsToolStripMenuItem.Name = "contentsToolStripMenuItem";
			this.contentsToolStripMenuItem.Size = new System.Drawing.Size(186, 36);
			this.contentsToolStripMenuItem.Text = "&Contents";
			// 
			// indexToolStripMenuItem
			// 
			this.indexToolStripMenuItem.Name = "indexToolStripMenuItem";
			this.indexToolStripMenuItem.Size = new System.Drawing.Size(186, 36);
			this.indexToolStripMenuItem.Text = "&Index";
			// 
			// searchToolStripMenuItem
			// 
			this.searchToolStripMenuItem.Name = "searchToolStripMenuItem";
			this.searchToolStripMenuItem.Size = new System.Drawing.Size(186, 36);
			this.searchToolStripMenuItem.Text = "&Search";
			// 
			// toolStripSeparator5
			// 
			this.toolStripSeparator5.Name = "toolStripSeparator5";
			this.toolStripSeparator5.Size = new System.Drawing.Size(183, 6);
			// 
			// aboutToolStripMenuItem
			// 
			this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
			this.aboutToolStripMenuItem.Size = new System.Drawing.Size(186, 36);
			this.aboutToolStripMenuItem.Text = "&About...";
			// 
			// splitContainer1
			// 
			this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.splitContainer1.Location = new System.Drawing.Point(0, 0);
			this.splitContainer1.Name = "splitContainer1";
			// 
			// splitContainer1.Panel1
			// 
			this.splitContainer1.Panel1.BackColor = System.Drawing.Color.Gainsboro;
			this.splitContainer1.Panel1.Controls.Add(this.monthCalendar1);
			// 
			// splitContainer1.Panel2
			// 
			this.splitContainer1.Panel2.Controls.Add(this.splitContainerBottom);
			this.splitContainer1.Size = new System.Drawing.Size(1354, 998);
			this.splitContainer1.SplitterDistance = 322;
			this.splitContainer1.TabIndex = 1;
			// 
			// monthCalendar1
			// 
			this.monthCalendar1.Location = new System.Drawing.Point(18, 18);
			this.monthCalendar1.Name = "monthCalendar1";
			this.monthCalendar1.TabIndex = 2;
			this.monthCalendar1.TabStop = false;
			this.monthCalendar1.DateChanged += new System.Windows.Forms.DateRangeEventHandler(this.monthCalendar1_DateChanged);
			// 
			// splitContainerBottom
			// 
			this.splitContainerBottom.Dock = System.Windows.Forms.DockStyle.Fill;
			this.splitContainerBottom.Location = new System.Drawing.Point(0, 0);
			this.splitContainerBottom.Name = "splitContainerBottom";
			this.splitContainerBottom.Orientation = System.Windows.Forms.Orientation.Horizontal;
			// 
			// splitContainerBottom.Panel1
			// 
			this.splitContainerBottom.Panel1.Controls.Add(this.splitContainerRight);
			// 
			// splitContainerBottom.Panel2
			// 
			this.splitContainerBottom.Panel2.Controls.Add(this.RichTextBox3);
			this.splitContainerBottom.Size = new System.Drawing.Size(1028, 998);
			this.splitContainerBottom.SplitterDistance = 443;
			this.splitContainerBottom.TabIndex = 3;
			// 
			// splitContainerRight
			// 
			this.splitContainerRight.Dock = System.Windows.Forms.DockStyle.Fill;
			this.splitContainerRight.Location = new System.Drawing.Point(0, 0);
			this.splitContainerRight.Name = "splitContainerRight";
			// 
			// splitContainerRight.Panel1
			// 
			this.splitContainerRight.Panel1.Controls.Add(this.mainTextBox);
			// 
			// splitContainerRight.Panel2
			// 
			this.splitContainerRight.Panel2.Controls.Add(this.RichTextBox2);
			this.splitContainerRight.Size = new System.Drawing.Size(1028, 443);
			this.splitContainerRight.SplitterDistance = 584;
			this.splitContainerRight.SplitterWidth = 10;
			this.splitContainerRight.TabIndex = 0;
			// 
			// mainTextBox
			// 
			this.mainTextBox.AcceptsTab = true;
			this.mainTextBox.BackColor = System.Drawing.Color.FloralWhite;
			this.mainTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
			this.mainTextBox.Font = new System.Drawing.Font("Calibri", 10.875F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.mainTextBox.Location = new System.Drawing.Point(0, 0);
			this.mainTextBox.Name = "mainTextBox";
			this.mainTextBox.Size = new System.Drawing.Size(584, 443);
			this.mainTextBox.TabIndex = 1;
			this.mainTextBox.TabStop = false;
			this.mainTextBox.Text = "";
			this.mainTextBox.LinkClicked += new System.Windows.Forms.LinkClickedEventHandler(this.mainTextBox_LinkClicked);
			this.mainTextBox.TextChanged += new System.EventHandler(this.mainTextBox_TextChanged);
			this.mainTextBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.mainTextBox_KeyDown);
			this.mainTextBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.mainTextBox_KeyPress);
			// 
			// RichTextBox2
			// 
			this.RichTextBox2.AcceptsTab = true;
			this.RichTextBox2.BackColor = System.Drawing.Color.FloralWhite;
			this.RichTextBox2.Dock = System.Windows.Forms.DockStyle.Fill;
			this.RichTextBox2.Location = new System.Drawing.Point(0, 0);
			this.RichTextBox2.Name = "RichTextBox2";
			this.RichTextBox2.Size = new System.Drawing.Size(434, 443);
			this.RichTextBox2.TabIndex = 0;
			this.RichTextBox2.TabStop = false;
			this.RichTextBox2.Text = "";
			this.RichTextBox2.LinkClicked += new System.Windows.Forms.LinkClickedEventHandler(this.mainTextBox_LinkClicked);
			// 
			// RichTextBox3
			// 
			this.RichTextBox3.BackColor = System.Drawing.Color.FloralWhite;
			this.RichTextBox3.Dock = System.Windows.Forms.DockStyle.Fill;
			this.RichTextBox3.Location = new System.Drawing.Point(0, 0);
			this.RichTextBox3.Name = "RichTextBox3";
			this.RichTextBox3.Size = new System.Drawing.Size(1028, 551);
			this.RichTextBox3.TabIndex = 0;
			this.RichTextBox3.Text = "";
			// 
			// btn_mainMenu
			// 
			this.btn_mainMenu.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btn_mainMenu.AutoSize = true;
			this.btn_mainMenu.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btn_mainMenu.Location = new System.Drawing.Point(1261, 12);
			this.btn_mainMenu.Name = "btn_mainMenu";
			this.btn_mainMenu.Size = new System.Drawing.Size(83, 37);
			this.btn_mainMenu.TabIndex = 3;
			this.btn_mainMenu.TabStop = false;
			this.btn_mainMenu.Text = "Menu";
			this.btn_mainMenu.UseVisualStyleBackColor = true;
			this.btn_mainMenu.Click += new System.EventHandler(this.btn_mainMenu_Click);
			// 
			// btn_panelLeft
			// 
			this.btn_panelLeft.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.btn_panelLeft.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btn_panelLeft.Location = new System.Drawing.Point(13, 950);
			this.btn_panelLeft.Name = "btn_panelLeft";
			this.btn_panelLeft.Size = new System.Drawing.Size(104, 36);
			this.btn_panelLeft.TabIndex = 2;
			this.btn_panelLeft.TabStop = false;
			this.btn_panelLeft.Text = "Panel L";
			this.btn_panelLeft.UseVisualStyleBackColor = true;
			this.btn_panelLeft.Click += new System.EventHandler(this.btn_panelLeft_Click);
			// 
			// btn_panelRight
			// 
			this.btn_panelRight.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.btn_panelRight.AutoSize = true;
			this.btn_panelRight.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btn_panelRight.Location = new System.Drawing.Point(1242, 949);
			this.btn_panelRight.Name = "btn_panelRight";
			this.btn_panelRight.Size = new System.Drawing.Size(100, 37);
			this.btn_panelRight.TabIndex = 4;
			this.btn_panelRight.TabStop = false;
			this.btn_panelRight.Text = "Panel R";
			this.btn_panelRight.UseVisualStyleBackColor = true;
			this.btn_panelRight.Click += new System.EventHandler(this.btn_panelRight_Click);
			// 
			// btn_reloadTodos
			// 
			this.btn_reloadTodos.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.btn_reloadTodos.BackColor = System.Drawing.SystemColors.Control;
			this.btn_reloadTodos.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btn_reloadTodos.Location = new System.Drawing.Point(123, 950);
			this.btn_reloadTodos.Name = "btn_reloadTodos";
			this.btn_reloadTodos.Size = new System.Drawing.Size(131, 36);
			this.btn_reloadTodos.TabIndex = 5;
			this.btn_reloadTodos.Text = "reload todo";
			this.btn_reloadTodos.UseVisualStyleBackColor = true;
			this.btn_reloadTodos.Click += new System.EventHandler(this.btn_reloadTodo_Click);
			// 
			// btn_panelBottom
			// 
			this.btn_panelBottom.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.btn_panelBottom.BackColor = System.Drawing.SystemColors.Control;
			this.btn_panelBottom.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btn_panelBottom.Location = new System.Drawing.Point(260, 950);
			this.btn_panelBottom.Name = "btn_panelBottom";
			this.btn_panelBottom.Size = new System.Drawing.Size(131, 36);
			this.btn_panelBottom.TabIndex = 6;
			this.btn_panelBottom.Text = "Panel B";
			this.btn_panelBottom.UseVisualStyleBackColor = true;
			this.btn_panelBottom.Click += new System.EventHandler(this.btn_panelBottom_Click);
			// 
			// showhodeButtonsToolStripMenuItem
			// 
			this.showhodeButtonsToolStripMenuItem.Name = "showhodeButtonsToolStripMenuItem";
			this.showhodeButtonsToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.B)));
			this.showhodeButtonsToolStripMenuItem.Size = new System.Drawing.Size(429, 36);
			this.showhodeButtonsToolStripMenuItem.Text = "Show/hode buttons";
			this.showhodeButtonsToolStripMenuItem.Click += new System.EventHandler(this.showhodeButtonsToolStripMenuItem_Click);
			// 
			// Form1
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(1354, 998);
			this.Controls.Add(this.btn_panelBottom);
			this.Controls.Add(this.btn_reloadTodos);
			this.Controls.Add(this.btn_mainMenu);
			this.Controls.Add(this.btn_panelLeft);
			this.Controls.Add(this.btn_panelRight);
			this.Controls.Add(this.splitContainer1);
			this.Controls.Add(this.mainMenu);
			this.KeyPreview = true;
			this.MainMenuStrip = this.mainMenu;
			this.Name = "Form1";
			this.Text = "MiteyTimeTracking";
			this.Load += new System.EventHandler(this.Form1_Load);
			this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.Form1_KeyUp);
			this.mainMenu.ResumeLayout(false);
			this.mainMenu.PerformLayout();
			this.splitContainer1.Panel1.ResumeLayout(false);
			this.splitContainer1.Panel2.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
			this.splitContainer1.ResumeLayout(false);
			this.splitContainerBottom.Panel1.ResumeLayout(false);
			this.splitContainerBottom.Panel2.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.splitContainerBottom)).EndInit();
			this.splitContainerBottom.ResumeLayout(false);
			this.splitContainerRight.Panel1.ResumeLayout(false);
			this.splitContainerRight.Panel2.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.splitContainerRight)).EndInit();
			this.splitContainerRight.ResumeLayout(false);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		public RichTextBox mainTextBox;
		private MenuStrip mainMenu;
		private SplitContainer splitContainer1;
		private ToolStripMenuItem fileToolStripMenuItem;
		private ToolStripMenuItem newToolStripMenuItem;
		private ToolStripMenuItem openToolStripMenuItem;
		private ToolStripSeparator toolStripSeparator;
		private ToolStripMenuItem saveToolStripMenuItem;
		private ToolStripMenuItem saveAsToolStripMenuItem;
		private ToolStripSeparator toolStripSeparator1;
		private ToolStripMenuItem printToolStripMenuItem;
		private ToolStripMenuItem printPreviewToolStripMenuItem;
		private ToolStripSeparator toolStripSeparator2;
		private ToolStripMenuItem exitToolStripMenuItem;
		private ToolStripMenuItem editToolStripMenuItem;
		private ToolStripMenuItem undoToolStripMenuItem;
		private ToolStripMenuItem redoToolStripMenuItem;
		private ToolStripSeparator toolStripSeparator3;
		private ToolStripMenuItem cutToolStripMenuItem;
		private ToolStripMenuItem copyToolStripMenuItem;
		private ToolStripMenuItem pasteToolStripMenuItem;
		private ToolStripSeparator toolStripSeparator4;
		private ToolStripMenuItem selectAllToolStripMenuItem;
		private ToolStripMenuItem toolsToolStripMenuItem;
		private ToolStripMenuItem customizeToolStripMenuItem;
		private ToolStripMenuItem optionsToolStripMenuItem;
		private ToolStripMenuItem helpToolStripMenuItem;
		private ToolStripMenuItem contentsToolStripMenuItem;
		private ToolStripMenuItem indexToolStripMenuItem;
		private ToolStripMenuItem searchToolStripMenuItem;
		private ToolStripSeparator toolStripSeparator5;
		private ToolStripMenuItem aboutToolStripMenuItem;
		private MonthCalendar monthCalendar1;
		private Button btn_panelLeft;
		private Button btn_mainMenu;
		private SplitContainer splitContainerBottom;
		private Button btn_panelRight;
		public RichTextBox RichTextBox2;
		private ToolStripMenuItem viewToolStripMenuItem;
		private ToolStripMenuItem showOrHideMenuToolStripMenuItem;
		private ToolStripMenuItem showOrHideLeftPanelToolStripMenuItem;
		private ToolStripMenuItem showOrHideRightPanelToolStripMenuItem;
		public RichTextBox RichTextBox3;
		private Button btn_reloadTodos;
		private SplitContainer splitContainerRight;
		private Button btn_panelBottom;
		private ToolStripMenuItem showhideBottomPanelToolStripMenuItem;
		private ToolStripMenuItem showhodeButtonsToolStripMenuItem;

	}
}

