namespace SaledServices.CustomsExport
{
    partial class OpeningStockForm
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
            this.exportxmlbutton = new System.Windows.Forms.Button();
            this.dateTimePickerstart = new System.Windows.Forms.DateTimePicker();
            this.dateTimePickerend = new System.Windows.Forms.DateTimePicker();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.uploadExcelButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // exportxmlbutton
            // 
            this.exportxmlbutton.Location = new System.Drawing.Point(297, 175);
            this.exportxmlbutton.Name = "exportxmlbutton";
            this.exportxmlbutton.Size = new System.Drawing.Size(133, 25);
            this.exportxmlbutton.TabIndex = 0;
            this.exportxmlbutton.Text = "导出期初库存Excel";
            this.exportxmlbutton.UseVisualStyleBackColor = true;
            this.exportxmlbutton.Click += new System.EventHandler(this.exportxmlbutton_Click);
            // 
            // dateTimePickerstart
            // 
            this.dateTimePickerstart.Location = new System.Drawing.Point(125, 100);
            this.dateTimePickerstart.Name = "dateTimePickerstart";
            this.dateTimePickerstart.Size = new System.Drawing.Size(187, 21);
            this.dateTimePickerstart.TabIndex = 1;
            this.dateTimePickerstart.Visible = false;
            // 
            // dateTimePickerend
            // 
            this.dateTimePickerend.Location = new System.Drawing.Point(125, 148);
            this.dateTimePickerend.Name = "dateTimePickerend";
            this.dateTimePickerend.Size = new System.Drawing.Size(187, 21);
            this.dateTimePickerend.TabIndex = 2;
            this.dateTimePickerend.Visible = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(54, 106);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 12);
            this.label1.TabIndex = 3;
            this.label1.Text = "开始日期";
            this.label1.Visible = false;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(54, 154);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 12);
            this.label2.TabIndex = 3;
            this.label2.Text = "结束日期";
            this.label2.Visible = false;
            // 
            // label3
            // 
            this.label3.Font = new System.Drawing.Font("宋体", 15F);
            this.label3.ForeColor = System.Drawing.Color.Red;
            this.label3.Location = new System.Drawing.Point(43, 50);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(360, 47);
            this.label3.TabIndex = 9;
            this.label3.Text = "在点击下面的导出按钮之前，请确认所有的材料都已经归还到库中";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(295, 223);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(95, 12);
            this.label4.TabIndex = 14;
            this.label4.Text = "默认输出路径D:\\";
            // 
            // uploadExcelButton
            // 
            this.uploadExcelButton.Location = new System.Drawing.Point(297, 395);
            this.uploadExcelButton.Name = "uploadExcelButton";
            this.uploadExcelButton.Size = new System.Drawing.Size(133, 25);
            this.uploadExcelButton.TabIndex = 0;
            this.uploadExcelButton.Text = "导出期初库存";
            this.uploadExcelButton.UseVisualStyleBackColor = true;
            this.uploadExcelButton.Click += new System.EventHandler(this.uploadExcelButton_Click);
            // 
            // OpeningStockForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(648, 500);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.dateTimePickerend);
            this.Controls.Add(this.dateTimePickerstart);
            this.Controls.Add(this.uploadExcelButton);
            this.Controls.Add(this.exportxmlbutton);
            this.Name = "OpeningStockForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "期初库存";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button exportxmlbutton;
        private System.Windows.Forms.DateTimePicker dateTimePickerstart;
        private System.Windows.Forms.DateTimePicker dateTimePickerend;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button uploadExcelButton;
    }
}