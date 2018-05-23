namespace SaledServices
{
    partial class FilesUpdateForm
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
            this.cpupnfileName = new System.Windows.Forms.TextBox();
            this.add = new System.Windows.Forms.Button();
            this.upload = new System.Windows.Forms.Button();
            this.export = new System.Windows.Forms.Button();
            this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.chkcpufilename = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.SuspendLayout();
            // 
            // cpupnfileName
            // 
            this.cpupnfileName.Location = new System.Drawing.Point(52, 30);
            this.cpupnfileName.Margin = new System.Windows.Forms.Padding(5);
            this.cpupnfileName.Name = "cpupnfileName";
            this.cpupnfileName.Size = new System.Drawing.Size(449, 30);
            this.cpupnfileName.TabIndex = 2;
            // 
            // add
            // 
            this.add.Font = new System.Drawing.Font("SimSun", 15F);
            this.add.Location = new System.Drawing.Point(511, 30);
            this.add.Margin = new System.Windows.Forms.Padding(5);
            this.add.Name = "add";
            this.add.Size = new System.Drawing.Size(119, 39);
            this.add.TabIndex = 3;
            this.add.Text = "选择CPUPN";
            this.add.UseVisualStyleBackColor = true;
            this.add.Click += new System.EventHandler(this.add_Click);
            // 
            // upload
            // 
            this.upload.Font = new System.Drawing.Font("SimSun", 15F);
            this.upload.Location = new System.Drawing.Point(262, 137);
            this.upload.Margin = new System.Windows.Forms.Padding(5);
            this.upload.Name = "upload";
            this.upload.Size = new System.Drawing.Size(122, 58);
            this.upload.TabIndex = 3;
            this.upload.Text = "上传文件";
            this.upload.UseVisualStyleBackColor = true;
            this.upload.Click += new System.EventHandler(this.upload_Click);
            // 
            // export
            // 
            this.export.Font = new System.Drawing.Font("SimSun", 15F);
            this.export.Location = new System.Drawing.Point(474, 137);
            this.export.Margin = new System.Windows.Forms.Padding(5);
            this.export.Name = "export";
            this.export.Size = new System.Drawing.Size(162, 58);
            this.export.TabIndex = 3;
            this.export.Text = "导出";
            this.export.UseVisualStyleBackColor = true;
            this.export.Click += new System.EventHandler(this.export_Click);
            // 
            // openFileDialog
            // 
            this.openFileDialog.FileName = "openFileDialog";
            // 
            // chkcpufilename
            // 
            this.chkcpufilename.Location = new System.Drawing.Point(52, 88);
            this.chkcpufilename.Margin = new System.Windows.Forms.Padding(5);
            this.chkcpufilename.Name = "chkcpufilename";
            this.chkcpufilename.Size = new System.Drawing.Size(449, 30);
            this.chkcpufilename.TabIndex = 2;
            // 
            // button1
            // 
            this.button1.Font = new System.Drawing.Font("SimSun", 15F);
            this.button1.Location = new System.Drawing.Point(511, 82);
            this.button1.Margin = new System.Windows.Forms.Padding(5);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(119, 39);
            this.button1.TabIndex = 3;
            this.button1.Text = "选择CHKCPU";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog";
            // 
            // FilesUpdateForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(656, 236);
            this.Controls.Add(this.export);
            this.Controls.Add(this.upload);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.add);
            this.Controls.Add(this.chkcpufilename);
            this.Controls.Add(this.cpupnfileName);
            this.Font = new System.Drawing.Font("SimSun", 15F);
            this.Margin = new System.Windows.Forms.Padding(5);
            this.Name = "FilesUpdateForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "文件更新";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox cpupnfileName;
        private System.Windows.Forms.Button add;
        private System.Windows.Forms.Button upload;
        private System.Windows.Forms.Button export;
        private System.Windows.Forms.OpenFileDialog openFileDialog;
        private System.Windows.Forms.TextBox chkcpufilename;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
    }
}