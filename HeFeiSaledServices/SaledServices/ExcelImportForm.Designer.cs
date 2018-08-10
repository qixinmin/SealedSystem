namespace SaledServices
{
    partial class ExcelImportForm
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.test = new System.Windows.Forms.RadioButton();
            this.userInputRadioButton = new System.Windows.Forms.RadioButton();
            this.storeInfoImportradioButton = new System.Windows.Forms.RadioButton();
            this.stock_in_sheetradioButton = new System.Windows.Forms.RadioButton();
            this.faultTableRadioButton = new System.Windows.Forms.RadioButton();
            this.DPKradioButton = new System.Windows.Forms.RadioButton();
            this.LCFC71BOMRadioButton = new System.Windows.Forms.RadioButton();
            this.LCFC_MBBOMradioButton = new System.Windows.Forms.RadioButton();
            this.receiveOrder = new System.Windows.Forms.RadioButton();
            this.mbmaterial = new System.Windows.Forms.RadioButton();
            this.flexid8scheck = new System.Windows.Forms.RadioButton();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // importButton
            // 
            this.importButton.Enabled = false;
            this.importButton.Location = new System.Drawing.Point(479, 448);
            this.importButton.Margin = new System.Windows.Forms.Padding(5);
            this.importButton.Name = "importButton";
            this.importButton.Size = new System.Drawing.Size(125, 38);
            this.importButton.TabIndex = 0;
            this.importButton.Text = "开始导入";
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
            this.findFile.Location = new System.Drawing.Point(470, 82);
            this.findFile.Margin = new System.Windows.Forms.Padding(5);
            this.findFile.Name = "findFile";
            this.findFile.Size = new System.Drawing.Size(125, 38);
            this.findFile.TabIndex = 2;
            this.findFile.Text = "文件";
            this.findFile.UseVisualStyleBackColor = true;
            this.findFile.Click += new System.EventHandler(this.findFile_Click);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.test);
            this.panel1.Controls.Add(this.userInputRadioButton);
            this.panel1.Controls.Add(this.storeInfoImportradioButton);
            this.panel1.Controls.Add(this.stock_in_sheetradioButton);
            this.panel1.Controls.Add(this.faultTableRadioButton);
            this.panel1.Controls.Add(this.DPKradioButton);
            this.panel1.Controls.Add(this.LCFC71BOMRadioButton);
            this.panel1.Controls.Add(this.LCFC_MBBOMradioButton);
            this.panel1.Controls.Add(this.flexid8scheck);
            this.panel1.Controls.Add(this.receiveOrder);
            this.panel1.Controls.Add(this.mbmaterial);
            this.panel1.Location = new System.Drawing.Point(20, 149);
            this.panel1.Margin = new System.Windows.Forms.Padding(5);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(430, 495);
            this.panel1.TabIndex = 4;
            // 
            // test
            // 
            this.test.AutoSize = true;
            this.test.Location = new System.Drawing.Point(22, 450);
            this.test.Name = "test";
            this.test.Size = new System.Drawing.Size(67, 24);
            this.test.TabIndex = 4;
            this.test.Text = "test";
            this.test.UseVisualStyleBackColor = true;
            this.test.Visible = false;
            // 
            // userInputRadioButton
            // 
            this.userInputRadioButton.AutoSize = true;
            this.userInputRadioButton.Location = new System.Drawing.Point(22, 397);
            this.userInputRadioButton.Name = "userInputRadioButton";
            this.userInputRadioButton.Size = new System.Drawing.Size(147, 24);
            this.userInputRadioButton.TabIndex = 4;
            this.userInputRadioButton.Text = "用户资料导入";
            this.userInputRadioButton.UseVisualStyleBackColor = true;
            // 
            // storeInfoImportradioButton
            // 
            this.storeInfoImportradioButton.AutoSize = true;
            this.storeInfoImportradioButton.Location = new System.Drawing.Point(22, 347);
            this.storeInfoImportradioButton.Name = "storeInfoImportradioButton";
            this.storeInfoImportradioButton.Size = new System.Drawing.Size(147, 24);
            this.storeInfoImportradioButton.TabIndex = 4;
            this.storeInfoImportradioButton.Text = "库房储位导入";
            this.storeInfoImportradioButton.UseVisualStyleBackColor = true;
            // 
            // stock_in_sheetradioButton
            // 
            this.stock_in_sheetradioButton.AutoSize = true;
            this.stock_in_sheetradioButton.Location = new System.Drawing.Point(22, 307);
            this.stock_in_sheetradioButton.Name = "stock_in_sheetradioButton";
            this.stock_in_sheetradioButton.Size = new System.Drawing.Size(127, 24);
            this.stock_in_sheetradioButton.TabIndex = 4;
            this.stock_in_sheetradioButton.Text = "材料入库单";
            this.stock_in_sheetradioButton.UseVisualStyleBackColor = true;
            // 
            // faultTableRadioButton
            // 
            this.faultTableRadioButton.AutoSize = true;
            this.faultTableRadioButton.Location = new System.Drawing.Point(22, 265);
            this.faultTableRadioButton.Name = "faultTableRadioButton";
            this.faultTableRadioButton.Size = new System.Drawing.Size(127, 24);
            this.faultTableRadioButton.TabIndex = 4;
            this.faultTableRadioButton.Text = "故障代码表";
            this.faultTableRadioButton.UseVisualStyleBackColor = true;
            // 
            // DPKradioButton
            // 
            this.DPKradioButton.AutoSize = true;
            this.DPKradioButton.Location = new System.Drawing.Point(22, 222);
            this.DPKradioButton.Name = "DPKradioButton";
            this.DPKradioButton.Size = new System.Drawing.Size(57, 24);
            this.DPKradioButton.TabIndex = 4;
            this.DPKradioButton.Text = "DPK";
            this.DPKradioButton.UseVisualStyleBackColor = true;
            // 
            // LCFC71BOMRadioButton
            // 
            this.LCFC71BOMRadioButton.AutoSize = true;
            this.LCFC71BOMRadioButton.Location = new System.Drawing.Point(22, 180);
            this.LCFC71BOMRadioButton.Name = "LCFC71BOMRadioButton";
            this.LCFC71BOMRadioButton.Size = new System.Drawing.Size(117, 24);
            this.LCFC71BOMRadioButton.TabIndex = 4;
            this.LCFC71BOMRadioButton.Text = "LCFC71BOM";
            this.LCFC71BOMRadioButton.UseVisualStyleBackColor = true;
            // 
            // LCFC_MBBOMradioButton
            // 
            this.LCFC_MBBOMradioButton.AutoSize = true;
            this.LCFC_MBBOMradioButton.Location = new System.Drawing.Point(22, 141);
            this.LCFC_MBBOMradioButton.Name = "LCFC_MBBOMradioButton";
            this.LCFC_MBBOMradioButton.Size = new System.Drawing.Size(257, 24);
            this.LCFC_MBBOMradioButton.TabIndex = 2;
            this.LCFC_MBBOMradioButton.Text = "LCFC_MBBOM&&COMPAL_MBBOM";
            this.LCFC_MBBOMradioButton.UseVisualStyleBackColor = true;
            // 
            // receiveOrder
            // 
            this.receiveOrder.AutoSize = true;
            this.receiveOrder.Checked = true;
            this.receiveOrder.Location = new System.Drawing.Point(22, 48);
            this.receiveOrder.Margin = new System.Windows.Forms.Padding(5);
            this.receiveOrder.Name = "receiveOrder";
            this.receiveOrder.Size = new System.Drawing.Size(87, 24);
            this.receiveOrder.TabIndex = 1;
            this.receiveOrder.Text = "收货单";
            this.receiveOrder.UseVisualStyleBackColor = true;
            // 
            // mbmaterial
            // 
            this.mbmaterial.AutoSize = true;
            this.mbmaterial.Location = new System.Drawing.Point(22, 14);
            this.mbmaterial.Margin = new System.Windows.Forms.Padding(5);
            this.mbmaterial.Name = "mbmaterial";
            this.mbmaterial.Size = new System.Drawing.Size(147, 24);
            this.mbmaterial.TabIndex = 0;
            this.mbmaterial.Text = "MB物料对照表";
            this.mbmaterial.UseVisualStyleBackColor = true;
            // 
            // flexid8scheck
            // 
            this.flexid8scheck.AutoSize = true;
            this.flexid8scheck.Location = new System.Drawing.Point(22, 101);
            this.flexid8scheck.Margin = new System.Windows.Forms.Padding(5);
            this.flexid8scheck.Name = "flexid8scheck";
            this.flexid8scheck.Size = new System.Drawing.Size(167, 24);
            this.flexid8scheck.TabIndex = 1;
            this.flexid8scheck.Text = "FlexId8S对照表";
            this.flexid8scheck.UseVisualStyleBackColor = true;
            // 
            // ExcelImportForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(925, 658);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.findFile);
            this.Controls.Add(this.filePath);
            this.Controls.Add(this.importButton);
            this.Font = new System.Drawing.Font("宋体", 15F);
            this.Margin = new System.Windows.Forms.Padding(5);
            this.Name = "ExcelImportForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Excel表格导入";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button importButton;
        private System.Windows.Forms.OpenFileDialog openFileDialog;
        private System.Windows.Forms.TextBox filePath;
        private System.Windows.Forms.Button findFile;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.RadioButton receiveOrder;
        private System.Windows.Forms.RadioButton mbmaterial;
        private System.Windows.Forms.RadioButton LCFC71BOMRadioButton;
        private System.Windows.Forms.RadioButton LCFC_MBBOMradioButton;
        private System.Windows.Forms.RadioButton DPKradioButton;
        private System.Windows.Forms.RadioButton faultTableRadioButton;
        private System.Windows.Forms.RadioButton stock_in_sheetradioButton;
        private System.Windows.Forms.RadioButton storeInfoImportradioButton;
        private System.Windows.Forms.RadioButton userInputRadioButton;
        private System.Windows.Forms.RadioButton test;
        private System.Windows.Forms.RadioButton flexid8scheck;
    }
}