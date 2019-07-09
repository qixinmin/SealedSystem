namespace SaledServices.CustomsExport
{
    partial class StockInOutForm
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
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.dateTimePickerend = new System.Windows.Forms.DateTimePicker();
            this.dateTimePickerstart = new System.Windows.Forms.DateTimePicker();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.newBankNo = new System.Windows.Forms.CheckBox();
            this.excelExport = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // exportxmlbutton
            // 
            this.exportxmlbutton.Location = new System.Drawing.Point(177, 193);
            this.exportxmlbutton.Name = "exportxmlbutton";
            this.exportxmlbutton.Size = new System.Drawing.Size(75, 23);
            this.exportxmlbutton.TabIndex = 2;
            this.exportxmlbutton.Text = "导出";
            this.exportxmlbutton.UseVisualStyleBackColor = true;
            this.exportxmlbutton.Click += new System.EventHandler(this.exportxmlbutton_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(28, 141);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 12);
            this.label2.TabIndex = 10;
            this.label2.Text = "结束日期";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(28, 93);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 12);
            this.label1.TabIndex = 11;
            this.label1.Text = "开始日期";
            // 
            // dateTimePickerend
            // 
            this.dateTimePickerend.Location = new System.Drawing.Point(99, 135);
            this.dateTimePickerend.Name = "dateTimePickerend";
            this.dateTimePickerend.Size = new System.Drawing.Size(200, 21);
            this.dateTimePickerend.TabIndex = 9;
            // 
            // dateTimePickerstart
            // 
            this.dateTimePickerstart.Location = new System.Drawing.Point(99, 87);
            this.dateTimePickerstart.Name = "dateTimePickerstart";
            this.dateTimePickerstart.Size = new System.Drawing.Size(200, 21);
            this.dateTimePickerstart.TabIndex = 8;
            // 
            // label3
            // 
            this.label3.Font = new System.Drawing.Font("宋体", 15F);
            this.label3.ForeColor = System.Drawing.Color.Red;
            this.label3.Location = new System.Drawing.Point(12, 9);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(333, 75);
            this.label3.TabIndex = 12;
            this.label3.Text = "在点击下面的导出按钮之前，请确认之前上传的日期与本次导出的日期不要重复！";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(28, 257);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(95, 12);
            this.label4.TabIndex = 13;
            this.label4.Text = "默认输出路径D:\\";
            // 
            // newBankNo
            // 
            this.newBankNo.AutoSize = true;
            this.newBankNo.Location = new System.Drawing.Point(381, 87);
            this.newBankNo.Name = "newBankNo";
            this.newBankNo.Size = new System.Drawing.Size(72, 16);
            this.newBankNo.TabIndex = 16;
            this.newBankNo.Text = "新账册号";
            this.newBankNo.UseVisualStyleBackColor = true;
            this.newBankNo.Visible = false;
            // 
            // excelExport
            // 
            this.excelExport.AutoSize = true;
            this.excelExport.Location = new System.Drawing.Point(381, 137);
            this.excelExport.Name = "excelExport";
            this.excelExport.Size = new System.Drawing.Size(102, 16);
            this.excelExport.TabIndex = 16;
            this.excelExport.Text = "导出Excel文件";
            this.excelExport.UseVisualStyleBackColor = true;
            // 
            // StockInOutForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(553, 334);
            this.Controls.Add(this.excelExport);
            this.Controls.Add(this.newBankNo);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.dateTimePickerend);
            this.Controls.Add(this.dateTimePickerstart);
            this.Controls.Add(this.exportxmlbutton);
            this.Name = "StockInOutForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "出库入库(原来的界面）";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button exportxmlbutton;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DateTimePicker dateTimePickerend;
        private System.Windows.Forms.DateTimePicker dateTimePickerstart;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        public System.Windows.Forms.CheckBox newBankNo;
        public System.Windows.Forms.CheckBox excelExport;
    }
}