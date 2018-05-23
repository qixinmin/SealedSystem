namespace SaledServices
{
    partial class CustomFaultForm
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
            this.label1 = new System.Windows.Forms.Label();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.fault_describeTextBox = new System.Windows.Forms.TextBox();
            this.add = new System.Windows.Forms.Button();
            this.query = new System.Windows.Forms.Button();
            this.modify = new System.Windows.Forms.Button();
            this.delete = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.numTextBox = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.faultIndexTextBox = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("SimSun", 15F);
            this.label1.Location = new System.Drawing.Point(51, 101);
            this.label1.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(89, 20);
            this.label1.TabIndex = 0;
            this.label1.Text = "客户故障";
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(52, 231);
            this.dataGridView1.Margin = new System.Windows.Forms.Padding(5);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.RowTemplate.Height = 23;
            this.dataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView1.Size = new System.Drawing.Size(618, 323);
            this.dataGridView1.TabIndex = 1;
            this.dataGridView1.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellClick);
            // 
            // fault_describeTextBox
            // 
            this.fault_describeTextBox.Location = new System.Drawing.Point(203, 98);
            this.fault_describeTextBox.Margin = new System.Windows.Forms.Padding(5);
            this.fault_describeTextBox.Name = "fault_describeTextBox";
            this.fault_describeTextBox.Size = new System.Drawing.Size(164, 30);
            this.fault_describeTextBox.TabIndex = 2;
            // 
            // add
            // 
            this.add.Font = new System.Drawing.Font("SimSun", 15F);
            this.add.Location = new System.Drawing.Point(52, 156);
            this.add.Margin = new System.Windows.Forms.Padding(5);
            this.add.Name = "add";
            this.add.Size = new System.Drawing.Size(145, 55);
            this.add.TabIndex = 3;
            this.add.Text = "新增";
            this.add.UseVisualStyleBackColor = true;
            this.add.Click += new System.EventHandler(this.add_Click);
            // 
            // query
            // 
            this.query.Font = new System.Drawing.Font("SimSun", 15F);
            this.query.Location = new System.Drawing.Point(217, 156);
            this.query.Margin = new System.Windows.Forms.Padding(5);
            this.query.Name = "query";
            this.query.Size = new System.Drawing.Size(150, 55);
            this.query.TabIndex = 3;
            this.query.Text = "查询";
            this.query.UseVisualStyleBackColor = true;
            this.query.Click += new System.EventHandler(this.query_Click);
            // 
            // modify
            // 
            this.modify.Font = new System.Drawing.Font("SimSun", 15F);
            this.modify.Location = new System.Drawing.Point(379, 156);
            this.modify.Margin = new System.Windows.Forms.Padding(5);
            this.modify.Name = "modify";
            this.modify.Size = new System.Drawing.Size(142, 55);
            this.modify.TabIndex = 3;
            this.modify.Text = "修改";
            this.modify.UseVisualStyleBackColor = true;
            this.modify.Click += new System.EventHandler(this.modify_Click);
            // 
            // delete
            // 
            this.delete.Font = new System.Drawing.Font("SimSun", 15F);
            this.delete.Location = new System.Drawing.Point(545, 156);
            this.delete.Margin = new System.Windows.Forms.Padding(5);
            this.delete.Name = "delete";
            this.delete.Size = new System.Drawing.Size(125, 55);
            this.delete.TabIndex = 3;
            this.delete.Text = "删除";
            this.delete.UseVisualStyleBackColor = true;
            this.delete.Click += new System.EventHandler(this.delete_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("SimSun", 15F);
            this.label2.Location = new System.Drawing.Point(55, 8);
            this.label2.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(29, 20);
            this.label2.TabIndex = 4;
            this.label2.Text = "ID";
            // 
            // numTextBox
            // 
            this.numTextBox.Location = new System.Drawing.Point(203, 8);
            this.numTextBox.Margin = new System.Windows.Forms.Padding(5);
            this.numTextBox.Name = "numTextBox";
            this.numTextBox.ReadOnly = true;
            this.numTextBox.Size = new System.Drawing.Size(164, 30);
            this.numTextBox.TabIndex = 5;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("SimSun", 15F);
            this.label3.Location = new System.Drawing.Point(51, 50);
            this.label3.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(89, 20);
            this.label3.TabIndex = 0;
            this.label3.Text = "故障编号";
            // 
            // faultIndexTextBox
            // 
            this.faultIndexTextBox.Location = new System.Drawing.Point(203, 50);
            this.faultIndexTextBox.Margin = new System.Windows.Forms.Padding(5);
            this.faultIndexTextBox.Name = "faultIndexTextBox";
            this.faultIndexTextBox.Size = new System.Drawing.Size(164, 30);
            this.faultIndexTextBox.TabIndex = 2;
            // 
            // CustomFaultForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(928, 749);
            this.Controls.Add(this.numTextBox);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.delete);
            this.Controls.Add(this.modify);
            this.Controls.Add(this.query);
            this.Controls.Add(this.add);
            this.Controls.Add(this.faultIndexTextBox);
            this.Controls.Add(this.fault_describeTextBox);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label1);
            this.Font = new System.Drawing.Font("SimSun", 15F);
            this.Margin = new System.Windows.Forms.Padding(5);
            this.Name = "CustomFaultForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "客户故障";
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.TextBox fault_describeTextBox;
        private System.Windows.Forms.Button add;
        private System.Windows.Forms.Button query;
        private System.Windows.Forms.Button modify;
        private System.Windows.Forms.Button delete;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox numTextBox;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox faultIndexTextBox;
    }
}