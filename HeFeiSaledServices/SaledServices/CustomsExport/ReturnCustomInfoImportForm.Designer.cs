namespace SaledServices
{
    partial class ReturnCustomInfoImportForm
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
            this.importButton = new System.Windows.Forms.Button();
            this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.filePath = new System.Windows.Forms.TextBox();
            this.findFile = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // importButton
            // 
            this.importButton.Enabled = false;
            this.importButton.Location = new System.Drawing.Point(457, 226);
            this.importButton.Margin = new System.Windows.Forms.Padding(5);
            this.importButton.Name = "importButton";
            this.importButton.Size = new System.Drawing.Size(138, 39);
            this.importButton.TabIndex = 0;
            this.importButton.Text = "导入报关单号";
            this.importButton.UseVisualStyleBackColor = true;
            this.importButton.Click += new System.EventHandler(this.importButton_Click);
            // 
            // filePath
            // 
            this.filePath.Location = new System.Drawing.Point(20, 82);
            this.filePath.Margin = new System.Windows.Forms.Padding(5);
            this.filePath.Name = "filePath";
            this.filePath.Size = new System.Drawing.Size(419, 30);
            this.filePath.TabIndex = 1;
            // 
            // findFile
            // 
            this.findFile.Location = new System.Drawing.Point(457, 82);
            this.findFile.Margin = new System.Windows.Forms.Padding(5);
            this.findFile.Name = "findFile";
            this.findFile.Size = new System.Drawing.Size(138, 38);
            this.findFile.TabIndex = 2;
            this.findFile.Text = "文件";
            this.findFile.UseVisualStyleBackColor = true;
            this.findFile.Click += new System.EventHandler(this.findFile_Click);
            // 
            // button1
            // 
            this.button1.Enabled = false;
            this.button1.Location = new System.Drawing.Point(46, 213);
            this.button1.Margin = new System.Windows.Forms.Padding(5);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(125, 38);
            this.button1.TabIndex = 0;
            this.button1.Text = "导出出货表";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // ReturnCustomInfoImportForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(612, 294);
            this.Controls.Add(this.findFile);
            this.Controls.Add(this.filePath);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.importButton);
            this.Font = new System.Drawing.Font("宋体", 15F);
            this.Margin = new System.Windows.Forms.Padding(5);
            this.Name = "ReturnCustomInfoImportForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "出货海关信息导入";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button importButton;
        private System.Windows.Forms.OpenFileDialog openFileDialog;
        private System.Windows.Forms.TextBox filePath;
        private System.Windows.Forms.Button findFile;
        private System.Windows.Forms.Button button1;
    }
}