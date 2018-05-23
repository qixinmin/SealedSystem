namespace SaledServices
{
    partial class VendorForm
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
            this.query = new System.Windows.Forms.Button();
            this.modify = new System.Windows.Forms.Button();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.add = new System.Windows.Forms.Button();
            this.changshangTextBox = new System.Windows.Forms.TextBox();
            this.kehubieTextBox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.groupPanel = new System.Windows.Forms.Panel();
            this.numTextBox = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.delete = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.groupPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // query
            // 
            this.query.Location = new System.Drawing.Point(268, 380);
            this.query.Margin = new System.Windows.Forms.Padding(5);
            this.query.Name = "query";
            this.query.Size = new System.Drawing.Size(125, 38);
            this.query.TabIndex = 0;
            this.query.Text = "查询";
            this.query.UseVisualStyleBackColor = true;
            this.query.Click += new System.EventHandler(this.query_Click);
            // 
            // modify
            // 
            this.modify.Location = new System.Drawing.Point(453, 380);
            this.modify.Margin = new System.Windows.Forms.Padding(5);
            this.modify.Name = "modify";
            this.modify.Size = new System.Drawing.Size(125, 38);
            this.modify.TabIndex = 1;
            this.modify.Text = "修改";
            this.modify.UseVisualStyleBackColor = true;
            this.modify.Click += new System.EventHandler(this.modify_Click);
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToOrderColumns = true;
            this.dataGridView1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(48, 20);
            this.dataGridView1.Margin = new System.Windows.Forms.Padding(5);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowTemplate.Height = 23;
            this.dataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView1.Size = new System.Drawing.Size(907, 350);
            this.dataGridView1.TabIndex = 3;
            this.dataGridView1.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellClick);
            // 
            // add
            // 
            this.add.Location = new System.Drawing.Point(69, 380);
            this.add.Margin = new System.Windows.Forms.Padding(5);
            this.add.Name = "add";
            this.add.Size = new System.Drawing.Size(125, 38);
            this.add.TabIndex = 4;
            this.add.Text = "新增";
            this.add.UseVisualStyleBackColor = true;
            this.add.Click += new System.EventHandler(this.add_Click);
            // 
            // changshangTextBox
            // 
            this.changshangTextBox.Location = new System.Drawing.Point(123, 62);
            this.changshangTextBox.Margin = new System.Windows.Forms.Padding(5);
            this.changshangTextBox.Name = "changshangTextBox";
            this.changshangTextBox.Size = new System.Drawing.Size(164, 30);
            this.changshangTextBox.TabIndex = 5;
            // 
            // kehubieTextBox
            // 
            this.kehubieTextBox.Location = new System.Drawing.Point(123, 117);
            this.kehubieTextBox.Margin = new System.Windows.Forms.Padding(5);
            this.kehubieTextBox.Name = "kehubieTextBox";
            this.kehubieTextBox.Size = new System.Drawing.Size(164, 30);
            this.kehubieTextBox.TabIndex = 6;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(17, 67);
            this.label1.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(49, 20);
            this.label1.TabIndex = 7;
            this.label1.Text = "厂商";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(20, 122);
            this.label2.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(69, 20);
            this.label2.TabIndex = 8;
            this.label2.Text = "客户别";
            // 
            // groupPanel
            // 
            this.groupPanel.Controls.Add(this.numTextBox);
            this.groupPanel.Controls.Add(this.label3);
            this.groupPanel.Controls.Add(this.changshangTextBox);
            this.groupPanel.Controls.Add(this.label2);
            this.groupPanel.Controls.Add(this.kehubieTextBox);
            this.groupPanel.Controls.Add(this.label1);
            this.groupPanel.Location = new System.Drawing.Point(60, 428);
            this.groupPanel.Margin = new System.Windows.Forms.Padding(5);
            this.groupPanel.Name = "groupPanel";
            this.groupPanel.Size = new System.Drawing.Size(343, 200);
            this.groupPanel.TabIndex = 9;
            // 
            // numTextBox
            // 
            this.numTextBox.Location = new System.Drawing.Point(123, 7);
            this.numTextBox.Margin = new System.Windows.Forms.Padding(5);
            this.numTextBox.Name = "numTextBox";
            this.numTextBox.ReadOnly = true;
            this.numTextBox.Size = new System.Drawing.Size(164, 30);
            this.numTextBox.TabIndex = 10;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(20, 12);
            this.label3.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(49, 20);
            this.label3.TabIndex = 9;
            this.label3.Text = "编号";
            // 
            // delete
            // 
            this.delete.Location = new System.Drawing.Point(634, 380);
            this.delete.Margin = new System.Windows.Forms.Padding(5);
            this.delete.Name = "delete";
            this.delete.Size = new System.Drawing.Size(125, 38);
            this.delete.TabIndex = 10;
            this.delete.Text = "删除";
            this.delete.UseVisualStyleBackColor = true;
            this.delete.Click += new System.EventHandler(this.delete_Click);
            // 
            // VendorForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1078, 749);
            this.Controls.Add(this.delete);
            this.Controls.Add(this.groupPanel);
            this.Controls.Add(this.add);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.modify);
            this.Controls.Add(this.query);
            this.Font = new System.Drawing.Font("SimSun", 15F);
            this.Margin = new System.Windows.Forms.Padding(5);
            this.Name = "VendorForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "厂商与客户别";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.groupPanel.ResumeLayout(false);
            this.groupPanel.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button query;
        private System.Windows.Forms.Button modify;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Button add;
        private System.Windows.Forms.TextBox changshangTextBox;
        private System.Windows.Forms.TextBox kehubieTextBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Panel groupPanel;
        private System.Windows.Forms.TextBox numTextBox;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button delete;
    }
}