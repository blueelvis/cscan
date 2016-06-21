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
            this.Scan = new System.Windows.Forms.Button();
            this.Fix = new System.Windows.Forms.Button();
            this.statusText = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // Scan
            // 
            this.Scan.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F);
            this.Scan.Location = new System.Drawing.Point(75, 100);
            this.Scan.Name = "Scan";
            this.Scan.Size = new System.Drawing.Size(150, 100);
            this.Scan.TabIndex = 0;
            this.Scan.Text = "Scan";
            this.Scan.UseVisualStyleBackColor = true;
            this.Scan.Click += new System.EventHandler(this.Scan_Click);
            // 
            // Fix
            // 
            this.Fix.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F);
            this.Fix.Location = new System.Drawing.Point(350, 100);
            this.Fix.Name = "Fix";
            this.Fix.Size = new System.Drawing.Size(150, 100);
            this.Fix.TabIndex = 1;
            this.Fix.Text = "Fix";
            this.Fix.UseVisualStyleBackColor = true;
            // 
            // statusText
            // 
            this.statusText.Location = new System.Drawing.Point(149, 44);
            this.statusText.Name = "statusText";
            this.statusText.Size = new System.Drawing.Size(281, 20);
            this.statusText.TabIndex = 2;
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(584, 261);
            this.Controls.Add(this.statusText);
            this.Controls.Add(this.Fix);
            this.Controls.Add(this.Scan);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Main";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "CScan";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button Scan;
        private System.Windows.Forms.Button Fix;
        private System.Windows.Forms.TextBox statusText;
    }
}

