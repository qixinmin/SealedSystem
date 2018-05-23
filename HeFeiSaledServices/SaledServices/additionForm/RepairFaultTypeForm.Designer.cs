namespace SaledServices.additionForm
{
    partial class RepairFaultTypeForm
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
            this.numTextBox = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.delete = new System.Windows.Forms.Button();
            this.modify = new System.Windows.Forms.Button();
            this.query = new System.Windows.Forms.Button();
            this.add = new System.Windows.Forms.Button();
            this.faultTypeTextBox = new System.Windows.Forms.TextBox();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.label1 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // numTextBox
            // 
            this.numTextBox.Location = new System.Drawing.Point(349, 15);
            this.numTextBox.Margin = new System.Windows.Forms.Padding(6);
            this.numTextBox.Name = "numTextBox";
            this.numTextBox.ReadOnly = true;
            this.numTextBox.Size = new System.Drawing.Size(218, 26);
            this.numTextBox.TabIndex = 20;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(185, 15);
            this.label2.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(24, 16);
            this.label2.TabIndex = 19;
            this.label2.Text = "ID";
            // 
            // delete
            // 
            this.delete.Location = new System.Drawing.Point(834, 112);
            this.delete.Margin = new System.Windows.Forms.Padding(6);
            this.delete.Name = "delete";
            this.delete.Size = new System.Drawing.Size(166, 50);
            this.delete.TabIndex = 17;
            this.delete.Text = "删除";
            this.delete.UseVisualStyleBackColor = true;
            this.delete.Click += new System.EventHandler(this.delete_Click);
            // 
            // modify
            // 
            this.modify.Location = new System.Drawing.Point(614, 112);
            this.modify.Margin = new System.Windows.Forms.Padding(6);
            this.modify.Name = "modify";
            this.modify.Size = new System.Drawing.Size(166, 50);
            this.modify.TabIndex = 18;
            this.modify.Text = "修改";
            this.modify.UseVisualStyleBackColor = true;
            this.modify.Click += new System.EventHandler(this.modify_Click);
            // 
            // query
            // 
            this.query.Location = new System.Drawing.Point(398, 112);
            this.query.Margin = new System.Windows.Forms.Padding(6);
            this.query.Name = "query";
            this.query.Size = new System.Drawing.Size(166, 50);
            this.query.TabIndex = 16;
            this.query.Text = "查询";
            this.query.UseVisualStyleBackColor = true;
            this.query.Click += new System.EventHandler(this.query_Click);
            // 
            // add
            // 
            this.add.Location = new System.Drawing.Point(178, 112);
            this.add.Margin = new System.Windows.Forms.Padding(6);
            this.add.Name = "add";
            this.add.Size = new System.Drawing.Size(166, 50);
            this.add.TabIndex = 15;
            this.add.Text = "新增";
            this.add.UseVisualStyleBackColor = true;
            this.add.Click += new System.EventHandler(this.add_Click);
            // 
            // faultTypeTextBox
            // 
            this.faultTypeTextBox.Location = new System.Drawing.Point(349, 61);
            this.faultTypeTextBox.Margin = new System.Windows.Forms.Padding(6);
            this.faultTypeTextBox.Name = "faultTypeTextBox";
            this.faultTypeTextBox.Size = new System.Drawing.Size(218, 26);
            this.faultTypeTextBox.TabIndex = 14;
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(178, 174);
            this.dataGridView1.Margin = new System.Windows.Forms.Padding(6);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowTemplate.Height = 23;
            this.dataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView1.Size = new System.Drawing.Size(824, 430);
            this.dataGridView1.TabIndex = 13;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(185, 71);
            this.label1.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(40, 16);
            this.label1.TabIndex = 12;
            this.label1.Text = "类别";
            // 
            // RepairFaultTypeForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1302, 749);
            this.Controls.Add(this.numTextBox);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.delete);
            this.Controls.Add(this.modify);
            this.Controls.Add(this.query);
            this.Controls.Add(this.add);
            this.Controls.Add(this.faultTypeTextBox);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.label1);
            this.Font = new System.Drawing.Font("SimSun", 12F);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "RepairFaultTypeForm";
            this.Text = "维修故障类别";
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox numTextBox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button delete;
        private System.Windows.Forms.Button modify;
        private System.Windows.Forms.Button query;
        private System.Windows.Forms.Button add;
        private System.Windows.Forms.TextBox faultTypeTextBox;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Label label1;
    }
}