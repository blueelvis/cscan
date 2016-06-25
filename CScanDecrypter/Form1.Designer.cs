namespace CScanDecrypter
{
    partial class Form1
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
            this.generateKey = new System.Windows.Forms.Button();
            this.log = new System.Windows.Forms.RichTextBox();
            this.decryptLog = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // generateKey
            // 
            this.generateKey.Location = new System.Drawing.Point(90, 10);
            this.generateKey.Name = "generateKey";
            this.generateKey.Size = new System.Drawing.Size(100, 25);
            this.generateKey.TabIndex = 0;
            this.generateKey.Text = "Generate Key";
            this.generateKey.UseVisualStyleBackColor = true;
            this.generateKey.Click += new System.EventHandler(this.generateKey_Click);
            // 
            // log
            // 
            this.log.AcceptsTab = true;
            this.log.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.log.DetectUrls = false;
            this.log.HideSelection = false;
            this.log.Location = new System.Drawing.Point(0, 80);
            this.log.Name = "log";
            this.log.Size = new System.Drawing.Size(280, 180);
            this.log.TabIndex = 1;
            this.log.Text = "Paste the log here.";
            // 
            // decryptLog
            // 
            this.decryptLog.Location = new System.Drawing.Point(90, 43);
            this.decryptLog.Name = "decryptLog";
            this.decryptLog.Size = new System.Drawing.Size(100, 25);
            this.decryptLog.TabIndex = 2;
            this.decryptLog.Text = "Decrypt Log";
            this.decryptLog.UseVisualStyleBackColor = true;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 261);
            this.Controls.Add(this.decryptLog);
            this.Controls.Add(this.log);
            this.Controls.Add(this.generateKey);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "Form1";
            this.Text = "Log Decrypter";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button generateKey;
        private System.Windows.Forms.RichTextBox log;
        private System.Windows.Forms.Button decryptLog;
    }
}

