namespace SaledServices.CustomsExport
{
    partial class RealStockForm
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
            this.SuspendLayout();
            // 
            // exportxmlbutton
            // 
            this.exportxmlbutton.Location = new System.Drawing.Point(273, 235);
            this.exportxmlbutton.Name = "exportxmlbutton";
            this.exportxmlbutton.Size = new System.Drawing.Size(92, 25);
            this.exportxmlbutton.TabIndex = 1;
            this.exportxmlbutton.Text = "导出实盘库存";
            this.exportxmlbutton.UseVisualStyleBackColor = true;
            this.exportxmlbutton.Click += new System.EventHandler(this.exportxmlbutton_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(94, 194);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 12);
            this.label2.TabIndex = 6;
            this.label2.Text = "结束日期";
            this.label2.Visible = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(94, 146);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 12);
            this.label1.TabIndex = 7;
            this.label1.Text = "开始日期";
            this.label1.Visible = false;
            // 
            // dateTimePickerend
            // 
            this.dateTimePickerend.Location = new System.Drawing.Point(165, 188);
            this.dateTimePickerend.Name = "dateTimePickerend";
            this.dateTimePickerend.Size = new System.Drawing.Size(200, 21);
            this.dateTimePickerend.TabIndex = 5;
            this.dateTimePickerend.Visible = false;
            // 
            // dateTimePickerstart
            // 
            this.dateTimePickerstart.Location = new System.Drawing.Point(165, 140);
            this.dateTimePickerstart.Name = "dateTimePickerstart";
            this.dateTimePickerstart.Size = new System.Drawing.Size(200, 21);
            this.dateTimePickerstart.TabIndex = 4;
            this.dateTimePickerstart.Visible = false;
            // 
            // label3
            // 
            this.label3.Font = new System.Drawing.Font("宋体", 15F);
            this.label3.ForeColor = System.Drawing.Color.Red;
            this.label3.Location = new System.Drawing.Point(62, 38);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(373, 85);
            this.label3.TabIndex = 8;
            this.label3.Text = "在点击下面的导出按钮之前，请确认所有的材料都已经归还到库中";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(94, 356);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(95, 12);
            this.label4.TabIndex = 14;
            this.label4.Text = "默认输出路径D:\\";
            // 
            // newBankNo
            // 
            this.newBankNo.AutoSize = true;
            this.newBankNo.Location = new System.Drawing.Point(462, 142);
            this.newBankNo.Name = "newBankNo";
            this.newBankNo.Size = new System.Drawing.Size(72, 16);
            this.newBankNo.TabIndex = 16;
            this.newBankNo.Text = "新账册号";
            this.newBankNo.UseVisualStyleBackColor = true;
            // 
            // RealStockForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(593, 450);
            this.Controls.Add(this.newBankNo);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.dateTimePickerend);
            this.Controls.Add(this.dateTimePickerstart);
            this.Controls.Add(this.exportxmlbutton);
            this.Name = "RealStockForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "实盘库存";
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
        private System.Windows.Forms.CheckBox newBankNo;
    }
}