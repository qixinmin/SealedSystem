namespace SaledServices.Export
{
    partial class ExportDatabaseToExcel
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
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.dateTimePickerend = new System.Windows.Forms.DateTimePicker();
            this.dateTimePickerstart = new System.Windows.Forms.DateTimePicker();
            this.chooseFolder = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.pathTextBox = new System.Windows.Forms.TextBox();
            this.datasourceComboBox = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.exportToExcel = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(82, 178);
            this.label2.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(89, 20);
            this.label2.TabIndex = 6;
            this.label2.Text = "结束日期";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(82, 98);
            this.label1.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(89, 20);
            this.label1.TabIndex = 7;
            this.label1.Text = "开始日期";
            // 
            // dateTimePickerend
            // 
            this.dateTimePickerend.Location = new System.Drawing.Point(200, 168);
            this.dateTimePickerend.Margin = new System.Windows.Forms.Padding(5, 5, 5, 5);
            this.dateTimePickerend.Name = "dateTimePickerend";
            this.dateTimePickerend.Size = new System.Drawing.Size(331, 30);
            this.dateTimePickerend.TabIndex = 5;
            // 
            // dateTimePickerstart
            // 
            this.dateTimePickerstart.Location = new System.Drawing.Point(200, 88);
            this.dateTimePickerstart.Margin = new System.Windows.Forms.Padding(5, 5, 5, 5);
            this.dateTimePickerstart.Name = "dateTimePickerstart";
            this.dateTimePickerstart.Size = new System.Drawing.Size(331, 30);
            this.dateTimePickerstart.TabIndex = 4;
            // 
            // chooseFolder
            // 
            this.chooseFolder.Location = new System.Drawing.Point(575, 277);
            this.chooseFolder.Margin = new System.Windows.Forms.Padding(5, 5, 5, 5);
            this.chooseFolder.Name = "chooseFolder";
            this.chooseFolder.Size = new System.Drawing.Size(125, 38);
            this.chooseFolder.TabIndex = 8;
            this.chooseFolder.Text = "选择";
            this.chooseFolder.UseVisualStyleBackColor = true;
            this.chooseFolder.Click += new System.EventHandler(this.chooseFolder_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(82, 285);
            this.label3.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(109, 20);
            this.label3.TabIndex = 6;
            this.label3.Text = "目的文件夹";
            // 
            // pathTextBox
            // 
            this.pathTextBox.Location = new System.Drawing.Point(200, 280);
            this.pathTextBox.Margin = new System.Windows.Forms.Padding(5, 5, 5, 5);
            this.pathTextBox.Name = "pathTextBox";
            this.pathTextBox.ReadOnly = true;
            this.pathTextBox.Size = new System.Drawing.Size(331, 30);
            this.pathTextBox.TabIndex = 9;
            this.pathTextBox.Text = "D:\\";
            // 
            // datasourceComboBox
            // 
            this.datasourceComboBox.FormattingEnabled = true;
            this.datasourceComboBox.Items.AddRange(new object[] {
            "收货单",
            "CID记录",
            "Flexid对应表",
            "MB物料对照表",
            "用户表",
            "收货表",
            "还货表",
            "LCFC_MBBOM",
            "LCFC71BOM",
            "DPK表",
            "良品库房",
            "MB良品入库表",
            "MB良品出库表"});
            this.datasourceComboBox.Location = new System.Drawing.Point(200, 20);
            this.datasourceComboBox.Margin = new System.Windows.Forms.Padding(5, 5, 5, 5);
            this.datasourceComboBox.Name = "datasourceComboBox";
            this.datasourceComboBox.Size = new System.Drawing.Size(331, 28);
            this.datasourceComboBox.TabIndex = 10;
            this.datasourceComboBox.SelectedIndexChanged += new System.EventHandler(this.datasourceComboBox_SelectedIndexChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(82, 33);
            this.label4.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(69, 20);
            this.label4.TabIndex = 7;
            this.label4.Text = "数据源";
            // 
            // exportToExcel
            // 
            this.exportToExcel.Font = new System.Drawing.Font("SimSun", 15F);
            this.exportToExcel.Location = new System.Drawing.Point(768, 395);
            this.exportToExcel.Margin = new System.Windows.Forms.Padding(5, 5, 5, 5);
            this.exportToExcel.Name = "exportToExcel";
            this.exportToExcel.Size = new System.Drawing.Size(323, 152);
            this.exportToExcel.TabIndex = 8;
            this.exportToExcel.Text = "导出Excel";
            this.exportToExcel.UseVisualStyleBackColor = true;
            this.exportToExcel.Click += new System.EventHandler(this.exportToExcel_Click);
            // 
            // ExportDatabaseToExcel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1193, 625);
            this.Controls.Add(this.datasourceComboBox);
            this.Controls.Add(this.pathTextBox);
            this.Controls.Add(this.exportToExcel);
            this.Controls.Add(this.chooseFolder);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.dateTimePickerend);
            this.Controls.Add(this.dateTimePickerstart);
            this.Font = new System.Drawing.Font("SimSun", 15F);
            this.Margin = new System.Windows.Forms.Padding(5, 5, 5, 5);
            this.Name = "ExportDatabaseToExcel";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "选择";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DateTimePicker dateTimePickerend;
        private System.Windows.Forms.DateTimePicker dateTimePickerstart;
        private System.Windows.Forms.Button chooseFolder;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox pathTextBox;
        private System.Windows.Forms.ComboBox datasourceComboBox;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button exportToExcel;
    }
}