namespace CScan
{
    partial class Main
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Main));
            this.Scan = new System.Windows.Forms.Button();
            this.Fix = new System.Windows.Forms.Button();
            this.encryptionKey = new System.Windows.Forms.TextBox();
            this.statusText = new System.Windows.Forms.RichTextBox();
            this.enableJson = new System.Windows.Forms.CheckBox();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.advancedToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.allowUnsafeOperationsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.scanToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.enableFileEnumerationToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // Scan
            // 
            this.Scan.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F);
            this.Scan.Location = new System.Drawing.Point(24, 46);
            this.Scan.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.Scan.Name = "Scan";
            this.Scan.Size = new System.Drawing.Size(300, 96);
            this.Scan.TabIndex = 0;
            this.Scan.Text = "Scan";
            this.Scan.UseVisualStyleBackColor = true;
            this.Scan.Click += new System.EventHandler(this.Scan_Click);
            // 
            // Fix
            // 
            this.Fix.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F);
            this.Fix.Location = new System.Drawing.Point(24, 154);
            this.Fix.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.Fix.Name = "Fix";
            this.Fix.Size = new System.Drawing.Size(300, 96);
            this.Fix.TabIndex = 1;
            this.Fix.Text = "Fix";
            this.Fix.UseVisualStyleBackColor = true;
            this.Fix.Click += new System.EventHandler(this.Fix_Click);
            // 
            // encryptionKey
            // 
            this.encryptionKey.Location = new System.Drawing.Point(24, 262);
            this.encryptionKey.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.encryptionKey.Name = "encryptionKey";
            this.encryptionKey.Size = new System.Drawing.Size(296, 31);
            this.encryptionKey.TabIndex = 3;
            this.encryptionKey.Text = "Optional Encryption Key";
            this.encryptionKey.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // statusText
            // 
            this.statusText.DetectUrls = false;
            this.statusText.HideSelection = false;
            this.statusText.Location = new System.Drawing.Point(336, 46);
            this.statusText.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.statusText.Name = "statusText";
            this.statusText.ReadOnly = true;
            this.statusText.Size = new System.Drawing.Size(804, 452);
            this.statusText.TabIndex = 4;
            this.statusText.Text = "";
            // 
            // enableJson
            // 
            this.enableJson.AutoSize = true;
            this.enableJson.Location = new System.Drawing.Point(24, 302);
            this.enableJson.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.enableJson.Name = "enableJson";
            this.enableJson.Size = new System.Drawing.Size(243, 29);
            this.enableJson.TabIndex = 5;
            this.enableJson.Text = "Enable JSON Output";
            this.enableJson.UseVisualStyleBackColor = true;
            // 
            // menuStrip1
            // 
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(32, 32);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.scanToolStripMenuItem,
            this.advancedToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Padding = new System.Windows.Forms.Padding(12, 4, 0, 4);
            this.menuStrip1.Size = new System.Drawing.Size(1168, 46);
            this.menuStrip1.TabIndex = 6;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.exitToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(64, 38);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(269, 38);
            this.exitToolStripMenuItem.Text = "Exit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // advancedToolStripMenuItem
            // 
            this.advancedToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.allowUnsafeOperationsToolStripMenuItem});
            this.advancedToolStripMenuItem.Name = "advancedToolStripMenuItem";
            this.advancedToolStripMenuItem.Size = new System.Drawing.Size(132, 38);
            this.advancedToolStripMenuItem.Text = "Advanced";
            // 
            // allowUnsafeOperationsToolStripMenuItem
            // 
            this.allowUnsafeOperationsToolStripMenuItem.Name = "allowUnsafeOperationsToolStripMenuItem";
            this.allowUnsafeOperationsToolStripMenuItem.Size = new System.Drawing.Size(377, 38);
            this.allowUnsafeOperationsToolStripMenuItem.Text = "Allow Unsafe Operations";
            this.allowUnsafeOperationsToolStripMenuItem.Click += new System.EventHandler(this.allowUnsafeOperationsToolStripMenuItem_Click);
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.ImageScalingSize = new System.Drawing.Size(32, 32);
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(86, 4);
            // 
            // scanToolStripMenuItem
            // 
            this.scanToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.enableFileEnumerationToolStripMenuItem});
            this.scanToolStripMenuItem.Name = "scanToolStripMenuItem";
            this.scanToolStripMenuItem.Size = new System.Drawing.Size(77, 38);
            this.scanToolStripMenuItem.Text = "Scan";
            // 
            // enableFileEnumerationToolStripMenuItem
            // 
            this.enableFileEnumerationToolStripMenuItem.Name = "enableFileEnumerationToolStripMenuItem";
            this.enableFileEnumerationToolStripMenuItem.Size = new System.Drawing.Size(373, 38);
            this.enableFileEnumerationToolStripMenuItem.Text = "Enable File Enumeration";
            this.enableFileEnumerationToolStripMenuItem.Click += new System.EventHandler(this.enableFileEnumerationToolStripMenuItem_Click);
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1168, 502);
            this.Controls.Add(this.enableJson);
            this.Controls.Add(this.statusText);
            this.Controls.Add(this.encryptionKey);
            this.Controls.Add(this.Fix);
            this.Controls.Add(this.Scan);
            this.Controls.Add(this.menuStrip1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Main";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "CScan";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button Scan;
        private System.Windows.Forms.Button Fix;
        private System.Windows.Forms.TextBox encryptionKey;
        private System.Windows.Forms.RichTextBox statusText;
        private System.Windows.Forms.CheckBox enableJson;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem advancedToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem allowUnsafeOperationsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem scanToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem enableFileEnumerationToolStripMenuItem;
    }
}

