namespace SaledServices
{
    partial class FaultSMTStoreForm
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
            this.numberTextBox = new System.Windows.Forms.TextBox();
            this.add = new System.Windows.Forms.Button();
            this.query = new System.Windows.Forms.Button();
            this.modify = new System.Windows.Forms.Button();
            this.delete = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.idTextBox = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.mpnTextBox = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.storehoustTextBox = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.housetextBox = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.placetextBox = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("宋体", 15F);
            this.label1.Location = new System.Drawing.Point(51, 101);
            this.label1.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(49, 20);
            this.label1.TabIndex = 0;
            this.label1.Text = "数量";
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(52, 298);
            this.dataGridView1.Margin = new System.Windows.Forms.Padding(5);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.RowTemplate.Height = 23;
            this.dataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView1.Size = new System.Drawing.Size(618, 256);
            this.dataGridView1.TabIndex = 1;
            this.dataGridView1.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellClick);
            // 
            // numberTextBox
            // 
            this.numberTextBox.Location = new System.Drawing.Point(203, 98);
            this.numberTextBox.Margin = new System.Windows.Forms.Padding(5);
            this.numberTextBox.Name = "numberTextBox";
            this.numberTextBox.Size = new System.Drawing.Size(164, 30);
            this.numberTextBox.TabIndex = 2;
            // 
            // add
            // 
            this.add.Font = new System.Drawing.Font("宋体", 15F);
            this.add.Location = new System.Drawing.Point(52, 221);
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
            this.query.Font = new System.Drawing.Font("宋体", 15F);
            this.query.Location = new System.Drawing.Point(217, 221);
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
            this.modify.Font = new System.Drawing.Font("宋体", 15F);
            this.modify.Location = new System.Drawing.Point(379, 221);
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
            this.delete.Font = new System.Drawing.Font("宋体", 15F);
            this.delete.Location = new System.Drawing.Point(545, 221);
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
            this.label2.Font = new System.Drawing.Font("宋体", 15F);
            this.label2.Location = new System.Drawing.Point(55, 8);
            this.label2.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(29, 20);
            this.label2.TabIndex = 4;
            this.label2.Text = "ID";
            // 
            // idTextBox
            // 
            this.idTextBox.Location = new System.Drawing.Point(203, 8);
            this.idTextBox.Margin = new System.Windows.Forms.Padding(5);
            this.idTextBox.Name = "idTextBox";
            this.idTextBox.ReadOnly = true;
            this.idTextBox.Size = new System.Drawing.Size(164, 30);
            this.idTextBox.TabIndex = 5;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("宋体", 15F);
            this.label3.Location = new System.Drawing.Point(51, 50);
            this.label3.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(99, 20);
            this.label3.TabIndex = 0;
            this.label3.Text = "MPN(回车)";
            // 
            // mpnTextBox
            // 
            this.mpnTextBox.Location = new System.Drawing.Point(203, 50);
            this.mpnTextBox.Margin = new System.Windows.Forms.Padding(5);
            this.mpnTextBox.Name = "mpnTextBox";
            this.mpnTextBox.Size = new System.Drawing.Size(164, 30);
            this.mpnTextBox.TabIndex = 2;
            this.mpnTextBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.mpnTextBox_KeyPress);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("宋体", 15F);
            this.label4.Location = new System.Drawing.Point(51, 150);
            this.label4.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(49, 20);
            this.label4.TabIndex = 0;
            this.label4.Text = "库位";
            // 
            // storehoustTextBox
            // 
            this.storehoustTextBox.Enabled = false;
            this.storehoustTextBox.Location = new System.Drawing.Point(203, 147);
            this.storehoustTextBox.Margin = new System.Windows.Forms.Padding(5);
            this.storehoustTextBox.Name = "storehoustTextBox";
            this.storehoustTextBox.Size = new System.Drawing.Size(164, 30);
            this.storehoustTextBox.TabIndex = 2;
            this.storehoustTextBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.storehoustTextBox_KeyPress);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("宋体", 15F);
            this.label5.Location = new System.Drawing.Point(424, 27);
            this.label5.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(49, 20);
            this.label5.TabIndex = 0;
            this.label5.Text = "库房";
            // 
            // housetextBox
            // 
            this.housetextBox.Location = new System.Drawing.Point(576, 24);
            this.housetextBox.Margin = new System.Windows.Forms.Padding(5);
            this.housetextBox.Name = "housetextBox";
            this.housetextBox.ReadOnly = true;
            this.housetextBox.Size = new System.Drawing.Size(164, 30);
            this.housetextBox.TabIndex = 2;
            this.housetextBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.storehoustTextBox_KeyPress);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("宋体", 15F);
            this.label6.Location = new System.Drawing.Point(424, 67);
            this.label6.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(49, 20);
            this.label6.TabIndex = 0;
            this.label6.Text = "储位";
            // 
            // placetextBox
            // 
            this.placetextBox.Location = new System.Drawing.Point(576, 64);
            this.placetextBox.Margin = new System.Windows.Forms.Padding(5);
            this.placetextBox.Name = "placetextBox";
            this.placetextBox.ReadOnly = true;
            this.placetextBox.Size = new System.Drawing.Size(164, 30);
            this.placetextBox.TabIndex = 2;
            this.placetextBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.storehoustTextBox_KeyPress);
            // 
            // FaultSMTStoreForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(928, 749);
            this.Controls.Add(this.idTextBox);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.delete);
            this.Controls.Add(this.modify);
            this.Controls.Add(this.query);
            this.Controls.Add(this.add);
            this.Controls.Add(this.mpnTextBox);
            this.Controls.Add(this.placetextBox);
            this.Controls.Add(this.housetextBox);
            this.Controls.Add(this.storehoustTextBox);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.numberTextBox);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label1);
            this.Font = new System.Drawing.Font("宋体", 15F);
            this.Margin = new System.Windows.Forms.Padding(5);
            this.Name = "FaultSMTStoreForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "SMT不良品入库";
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.TextBox numberTextBox;
        private System.Windows.Forms.Button add;
        private System.Windows.Forms.Button query;
        private System.Windows.Forms.Button modify;
        private System.Windows.Forms.Button delete;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox idTextBox;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox mpnTextBox;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox storehoustTextBox;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox housetextBox;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox placetextBox;
    }
}