namespace Draw
{
	partial class MainForm
	{
		/// <summary>
		/// Designer variable used to keep track of non-visual components.
		/// </summary>
		private System.ComponentModel.IContainer components = null;
		
		/// <summary>
		/// Disposes resources used by the form.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing) {
				if (components != null) {
					components.Dispose();
				}
			}
			base.Dispose(disposing);
		}
		
		/// <summary>
		/// This method is required for Windows Forms designer support.
		/// Do not change the method contents inside the source code editor. The Forms designer might
		/// not be able to load this method if it was changed manually.
		/// </summary>
		private void InitializeComponent()
		{
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
			this.mainMenu = new System.Windows.Forms.MenuStrip();
			this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.saveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.loadToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
			this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.editToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.deleteSelectedToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
			this.changeFillColorToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.changeStrokeColorToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
			this.rotateLeftToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.rotateRightToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.scaleUpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.scaleDownToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
			this.groupSelectedToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.ungroupSelectedToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.imageToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.addRectangleToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.addEllipseToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.addLineToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.addTriangleToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.statusBar = new System.Windows.Forms.StatusStrip();
			this.currentStatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
			this.speedMenu = new System.Windows.Forms.ToolStrip();
			this.drawRectangleSpeedButton = new System.Windows.Forms.ToolStripButton();
			this.pickUpSpeedButton = new System.Windows.Forms.ToolStripButton();
			this.viewPort = new Draw.DoubleBufferedPanel();
			this.mainMenu.SuspendLayout();
			this.statusBar.SuspendLayout();
			this.speedMenu.SuspendLayout();
			this.SuspendLayout();
			// 
			// mainMenu
			// 
			this.mainMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
									this.fileToolStripMenuItem,
									this.editToolStripMenuItem,
									this.imageToolStripMenuItem,
									this.helpToolStripMenuItem});
			this.mainMenu.Location = new System.Drawing.Point(0, 0);
			this.mainMenu.Name = "mainMenu";
			this.mainMenu.Size = new System.Drawing.Size(693, 24);
			this.mainMenu.TabIndex = 1;
			this.mainMenu.Text = "menuStrip1";
			// 
			// fileToolStripMenuItem
			// 
			this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
									this.saveToolStripMenuItem,
									this.loadToolStripMenuItem,
									this.toolStripSeparator4,
									this.exitToolStripMenuItem});
			this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
			this.fileToolStripMenuItem.Size = new System.Drawing.Size(35, 20);
			this.fileToolStripMenuItem.Text = "File";
			// 
			// saveToolStripMenuItem
			// 
			this.saveToolStripMenuItem.Name = "saveToolStripMenuItem";
			this.saveToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
			this.saveToolStripMenuItem.Text = "Save";
			this.saveToolStripMenuItem.Click += new System.EventHandler(this.SaveToolStripMenuItemClick);
			// 
			// loadToolStripMenuItem
			// 
			this.loadToolStripMenuItem.Name = "loadToolStripMenuItem";
			this.loadToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
			this.loadToolStripMenuItem.Text = "Load";
			this.loadToolStripMenuItem.Click += new System.EventHandler(this.LoadToolStripMenuItemClick);
			// 
			// toolStripSeparator4
			// 
			this.toolStripSeparator4.Name = "toolStripSeparator4";
			this.toolStripSeparator4.Size = new System.Drawing.Size(149, 6);
			// 
			// exitToolStripMenuItem
			// 
			this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
			this.exitToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
			this.exitToolStripMenuItem.Text = "Exit";
			this.exitToolStripMenuItem.Click += new System.EventHandler(this.ExitToolStripMenuItemClick);
			// 
			// editToolStripMenuItem
			// 
			this.editToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
									this.deleteSelectedToolStripMenuItem,
									this.toolStripSeparator1,
									this.changeFillColorToolStripMenuItem,
									this.changeStrokeColorToolStripMenuItem,
									this.toolStripSeparator2,
									this.rotateLeftToolStripMenuItem,
									this.rotateRightToolStripMenuItem,
									this.scaleUpToolStripMenuItem,
									this.scaleDownToolStripMenuItem,
									this.toolStripSeparator3,
									this.groupSelectedToolStripMenuItem,
									this.ungroupSelectedToolStripMenuItem});
			this.editToolStripMenuItem.Name = "editToolStripMenuItem";
			this.editToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
			this.editToolStripMenuItem.Text = "Edit";
			// 
			// deleteSelectedToolStripMenuItem
			// 
			this.deleteSelectedToolStripMenuItem.Name = "deleteSelectedToolStripMenuItem";
			this.deleteSelectedToolStripMenuItem.Size = new System.Drawing.Size(177, 22);
			this.deleteSelectedToolStripMenuItem.Text = "Delete Selected";
			this.deleteSelectedToolStripMenuItem.Click += new System.EventHandler(this.DeleteSelectedToolStripMenuItemClick);
			// 
			// toolStripSeparator1
			// 
			this.toolStripSeparator1.Name = "toolStripSeparator1";
			this.toolStripSeparator1.Size = new System.Drawing.Size(174, 6);
			// 
			// changeFillColorToolStripMenuItem
			// 
			this.changeFillColorToolStripMenuItem.Name = "changeFillColorToolStripMenuItem";
			this.changeFillColorToolStripMenuItem.Size = new System.Drawing.Size(177, 22);
			this.changeFillColorToolStripMenuItem.Text = "Change Fill Color";
			this.changeFillColorToolStripMenuItem.Click += new System.EventHandler(this.ChangeFillColorToolStripMenuItemClick);
			// 
			// changeStrokeColorToolStripMenuItem
			// 
			this.changeStrokeColorToolStripMenuItem.Name = "changeStrokeColorToolStripMenuItem";
			this.changeStrokeColorToolStripMenuItem.Size = new System.Drawing.Size(177, 22);
			this.changeStrokeColorToolStripMenuItem.Text = "Change Stroke Color";
			this.changeStrokeColorToolStripMenuItem.Click += new System.EventHandler(this.ChangeStrokeColorToolStripMenuItemClick);
			// 
			// toolStripSeparator2
			// 
			this.toolStripSeparator2.Name = "toolStripSeparator2";
			this.toolStripSeparator2.Size = new System.Drawing.Size(174, 6);
			// 
			// rotateLeftToolStripMenuItem
			// 
			this.rotateLeftToolStripMenuItem.Name = "rotateLeftToolStripMenuItem";
			this.rotateLeftToolStripMenuItem.Size = new System.Drawing.Size(177, 22);
			this.rotateLeftToolStripMenuItem.Text = "Rotate Left";
			this.rotateLeftToolStripMenuItem.Click += new System.EventHandler(this.RotateLeftToolStripMenuItemClick);
			// 
			// rotateRightToolStripMenuItem
			// 
			this.rotateRightToolStripMenuItem.Name = "rotateRightToolStripMenuItem";
			this.rotateRightToolStripMenuItem.Size = new System.Drawing.Size(177, 22);
			this.rotateRightToolStripMenuItem.Text = "Rotate Right";
			this.rotateRightToolStripMenuItem.Click += new System.EventHandler(this.RotateRightToolStripMenuItemClick);
			// 
			// scaleUpToolStripMenuItem
			// 
			this.scaleUpToolStripMenuItem.Name = "scaleUpToolStripMenuItem";
			this.scaleUpToolStripMenuItem.Size = new System.Drawing.Size(177, 22);
			this.scaleUpToolStripMenuItem.Text = "Scale Up";
			this.scaleUpToolStripMenuItem.Click += new System.EventHandler(this.ScaleUpToolStripMenuItemClick);
			// 
			// scaleDownToolStripMenuItem
			// 
			this.scaleDownToolStripMenuItem.Name = "scaleDownToolStripMenuItem";
			this.scaleDownToolStripMenuItem.Size = new System.Drawing.Size(177, 22);
			this.scaleDownToolStripMenuItem.Text = "Scale Down";
			this.scaleDownToolStripMenuItem.Click += new System.EventHandler(this.ScaleDownToolStripMenuItemClick);
			// 
			// toolStripSeparator3
			// 
			this.toolStripSeparator3.Name = "toolStripSeparator3";
			this.toolStripSeparator3.Size = new System.Drawing.Size(174, 6);
			// 
			// groupSelectedToolStripMenuItem
			// 
			this.groupSelectedToolStripMenuItem.Name = "groupSelectedToolStripMenuItem";
			this.groupSelectedToolStripMenuItem.Size = new System.Drawing.Size(177, 22);
			this.groupSelectedToolStripMenuItem.Text = "Group Selected";
			this.groupSelectedToolStripMenuItem.Click += new System.EventHandler(this.GroupSelectedToolStripMenuItemClick);
			// 
			// ungroupSelectedToolStripMenuItem
			// 
			this.ungroupSelectedToolStripMenuItem.Name = "ungroupSelectedToolStripMenuItem";
			this.ungroupSelectedToolStripMenuItem.Size = new System.Drawing.Size(177, 22);
			this.ungroupSelectedToolStripMenuItem.Text = "Ungroup Selected";
			this.ungroupSelectedToolStripMenuItem.Click += new System.EventHandler(this.UngroupSelectedToolStripMenuItemClick);
			// 
			// imageToolStripMenuItem
			// 
			this.imageToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
									this.addRectangleToolStripMenuItem,
									this.addEllipseToolStripMenuItem,
									this.addLineToolStripMenuItem,
									this.addTriangleToolStripMenuItem});
			this.imageToolStripMenuItem.Name = "imageToolStripMenuItem";
			this.imageToolStripMenuItem.Size = new System.Drawing.Size(49, 20);
			this.imageToolStripMenuItem.Text = "Image";
			// 
			// addRectangleToolStripMenuItem
			// 
			this.addRectangleToolStripMenuItem.Name = "addRectangleToolStripMenuItem";
			this.addRectangleToolStripMenuItem.Size = new System.Drawing.Size(150, 22);
			this.addRectangleToolStripMenuItem.Text = "Add Rectangle";
			this.addRectangleToolStripMenuItem.Click += new System.EventHandler(this.AddRectangleToolStripMenuItemClick);
			// 
			// addEllipseToolStripMenuItem
			// 
			this.addEllipseToolStripMenuItem.Name = "addEllipseToolStripMenuItem";
			this.addEllipseToolStripMenuItem.Size = new System.Drawing.Size(150, 22);
			this.addEllipseToolStripMenuItem.Text = "Add Ellipse";
			this.addEllipseToolStripMenuItem.Click += new System.EventHandler(this.AddEllipseToolStripMenuItemClick);
			// 
			// addLineToolStripMenuItem
			// 
			this.addLineToolStripMenuItem.Name = "addLineToolStripMenuItem";
			this.addLineToolStripMenuItem.Size = new System.Drawing.Size(150, 22);
			this.addLineToolStripMenuItem.Text = "Add Line";
			this.addLineToolStripMenuItem.Click += new System.EventHandler(this.AddLineToolStripMenuItemClick);
			this.addTriangleToolStripMenuItem.Name = "addTriangleToolStripMenuItem";
			this.addTriangleToolStripMenuItem.Size = new System.Drawing.Size(150, 22);
			this.addTriangleToolStripMenuItem.Text = "Add Triangle";
			this.addTriangleToolStripMenuItem.Click += new System.EventHandler(this.AddTriangleToolStripMenuItemClick);
			// 
			// helpToolStripMenuItem
			// 
			this.helpToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
									this.aboutToolStripMenuItem});
			this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
			this.helpToolStripMenuItem.Size = new System.Drawing.Size(40, 20);
			this.helpToolStripMenuItem.Text = "Help";
			// 
			// aboutToolStripMenuItem
			// 
			this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
			this.aboutToolStripMenuItem.Size = new System.Drawing.Size(115, 22);
			this.aboutToolStripMenuItem.Text = "About...";
			// 
			// statusBar
			// 
			this.statusBar.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
									this.currentStatusLabel});
			this.statusBar.Location = new System.Drawing.Point(0, 401);
			this.statusBar.Name = "statusBar";
			this.statusBar.Size = new System.Drawing.Size(693, 22);
			this.statusBar.TabIndex = 2;
			this.statusBar.Text = "statusStrip1";
			// 
			// currentStatusLabel
			// 
			this.currentStatusLabel.Name = "currentStatusLabel";
			this.currentStatusLabel.Size = new System.Drawing.Size(0, 17);
			// 
			// speedMenu
			// 
			this.speedMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
									this.drawRectangleSpeedButton,
									this.pickUpSpeedButton});
			this.speedMenu.Location = new System.Drawing.Point(0, 24);
			this.speedMenu.Name = "speedMenu";
			this.speedMenu.Size = new System.Drawing.Size(693, 25);
			this.speedMenu.TabIndex = 3;
			this.speedMenu.Text = "toolStrip1";
			// 
			// drawRectangleSpeedButton
			// 
			this.drawRectangleSpeedButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.drawRectangleSpeedButton.Image = ((System.Drawing.Image)(resources.GetObject("drawRectangleSpeedButton.Image")));
			this.drawRectangleSpeedButton.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.drawRectangleSpeedButton.Name = "drawRectangleSpeedButton";
			this.drawRectangleSpeedButton.Size = new System.Drawing.Size(23, 22);
			this.drawRectangleSpeedButton.Text = "DrawRectangleButton";
			this.drawRectangleSpeedButton.Click += new System.EventHandler(this.DrawRectangleSpeedButtonClick);
			// 
			// pickUpSpeedButton
			// 
			this.pickUpSpeedButton.CheckOnClick = true;
			this.pickUpSpeedButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.pickUpSpeedButton.Image = ((System.Drawing.Image)(resources.GetObject("pickUpSpeedButton.Image")));
			this.pickUpSpeedButton.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.pickUpSpeedButton.Name = "pickUpSpeedButton";
			this.pickUpSpeedButton.Size = new System.Drawing.Size(23, 22);
			this.pickUpSpeedButton.Text = "toolStripButton1";
			// 
			// viewPort
			// 
			this.viewPort.Dock = System.Windows.Forms.DockStyle.Fill;
			this.viewPort.Location = new System.Drawing.Point(0, 49);
			this.viewPort.Name = "viewPort";
			this.viewPort.Size = new System.Drawing.Size(693, 352);
			this.viewPort.TabIndex = 4;
			this.viewPort.Paint += new System.Windows.Forms.PaintEventHandler(this.ViewPortPaint);
			this.viewPort.MouseMove += new System.Windows.Forms.MouseEventHandler(this.ViewPortMouseMove);
			this.viewPort.MouseDown += new System.Windows.Forms.MouseEventHandler(this.ViewPortMouseDown);
			this.viewPort.MouseUp += new System.Windows.Forms.MouseEventHandler(this.ViewPortMouseUp);
			// 
			// MainForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(693, 423);
			this.Controls.Add(this.viewPort);
			this.Controls.Add(this.speedMenu);
			this.Controls.Add(this.statusBar);
			this.Controls.Add(this.mainMenu);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MainMenuStrip = this.mainMenu;
			this.Name = "MainForm";
			this.Text = "Draw";
			this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
			this.mainMenu.ResumeLayout(false);
			this.mainMenu.PerformLayout();
			this.statusBar.ResumeLayout(false);
			this.statusBar.PerformLayout();
			this.speedMenu.ResumeLayout(false);
			this.speedMenu.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();
		}
		
		private System.Windows.Forms.ToolStripMenuItem addEllipseToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem addLineToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem addRectangleToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem addTriangleToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem changeFillColorToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem changeStrokeColorToolStripMenuItem;
		private System.Windows.Forms.ToolStripStatusLabel currentStatusLabel;
		private System.Windows.Forms.ToolStripMenuItem deleteSelectedToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem groupSelectedToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem rotateLeftToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem rotateRightToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem scaleDownToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem scaleUpToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem ungroupSelectedToolStripMenuItem;
		private Draw.DoubleBufferedPanel viewPort;
		private System.Windows.Forms.ToolStripButton pickUpSpeedButton;
		private System.Windows.Forms.ToolStripButton drawRectangleSpeedButton;
		private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem imageToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem loadToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem editToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
		private System.Windows.Forms.ToolStrip speedMenu;
		private System.Windows.Forms.StatusStrip statusBar;
		private System.Windows.Forms.MenuStrip mainMenu;
		private System.Windows.Forms.ToolStripMenuItem saveToolStripMenuItem;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
	}
}
