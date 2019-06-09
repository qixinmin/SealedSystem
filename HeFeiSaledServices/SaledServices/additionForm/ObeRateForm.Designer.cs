namespace SaledServices
{
    partial class ObeRateForm
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
            this.dataGridViewinsert = new System.Windows.Forms.DataGridView();
            this.checkrateTextBox = new System.Windows.Forms.TextBox();
            this.add = new System.Windows.Forms.Button();
            this.query = new System.Windows.Forms.Button();
            this.modify = new System.Windows.Forms.Button();
            this.delete = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.numTextBox = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.ordernoTextBox = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.inputertextBox = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.inputdatetextBox = new System.Windows.Forms.TextBox();
            this.dataGridViewquery = new System.Windows.Forms.DataGridView();
            this.label6 = new System.Windows.Forms.Label();
            this.ordernoquerytextBox = new System.Windows.Forms.TextBox();
            this.button_queryorder = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.label7 = new System.Windows.Forms.Label();
            this.matertialNotextBox = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewinsert)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewquery)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("宋体", 15F);
            this.label1.Location = new System.Drawing.Point(707, 123);
            this.label1.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(149, 20);
            this.label1.TabIndex = 0;
            this.label1.Text = "抽查比例(小数)";
            // 
            // dataGridViewinsert
            // 
            this.dataGridViewinsert.AllowUserToAddRows = false;
            this.dataGridViewinsert.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.dataGridViewinsert.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewinsert.Location = new System.Drawing.Point(379, 263);
            this.dataGridViewinsert.Margin = new System.Windows.Forms.Padding(5);
            this.dataGridViewinsert.Name = "dataGridViewinsert";
            this.dataGridViewinsert.ReadOnly = true;
            this.dataGridViewinsert.RowTemplate.Height = 23;
            this.dataGridViewinsert.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridViewinsert.Size = new System.Drawing.Size(655, 358);
            this.dataGridViewinsert.TabIndex = 1;
            this.dataGridViewinsert.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellClick);
            // 
            // checkrateTextBox
            // 
            this.checkrateTextBox.Location = new System.Drawing.Point(859, 120);
            this.checkrateTextBox.Margin = new System.Windows.Forms.Padding(5);
            this.checkrateTextBox.Name = "checkrateTextBox";
            this.checkrateTextBox.Size = new System.Drawing.Size(164, 30);
            this.checkrateTextBox.TabIndex = 2;
            // 
            // add
            // 
            this.add.Font = new System.Drawing.Font("宋体", 15F);
            this.add.Location = new System.Drawing.Point(390, 175);
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
            this.query.Location = new System.Drawing.Point(555, 175);
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
            this.modify.Location = new System.Drawing.Point(726, 175);
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
            this.delete.Location = new System.Drawing.Point(909, 175);
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
            this.label2.Location = new System.Drawing.Point(380, 29);
            this.label2.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(29, 20);
            this.label2.TabIndex = 4;
            this.label2.Text = "ID";
            // 
            // numTextBox
            // 
            this.numTextBox.Location = new System.Drawing.Point(528, 29);
            this.numTextBox.Margin = new System.Windows.Forms.Padding(5);
            this.numTextBox.Name = "numTextBox";
            this.numTextBox.ReadOnly = true;
            this.numTextBox.Size = new System.Drawing.Size(164, 30);
            this.numTextBox.TabIndex = 5;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("宋体", 15F);
            this.label3.Location = new System.Drawing.Point(376, 71);
            this.label3.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(89, 20);
            this.label3.TabIndex = 0;
            this.label3.Text = "订单编号";
            // 
            // ordernoTextBox
            // 
            this.ordernoTextBox.Location = new System.Drawing.Point(528, 71);
            this.ordernoTextBox.Margin = new System.Windows.Forms.Padding(5);
            this.ordernoTextBox.Name = "ordernoTextBox";
            this.ordernoTextBox.Size = new System.Drawing.Size(164, 30);
            this.ordernoTextBox.TabIndex = 2;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("宋体", 15F);
            this.label4.Location = new System.Drawing.Point(711, 29);
            this.label4.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(69, 20);
            this.label4.TabIndex = 4;
            this.label4.Text = "输入人";
            // 
            // inputertextBox
            // 
            this.inputertextBox.Location = new System.Drawing.Point(859, 29);
            this.inputertextBox.Margin = new System.Windows.Forms.Padding(5);
            this.inputertextBox.Name = "inputertextBox";
            this.inputertextBox.ReadOnly = true;
            this.inputertextBox.Size = new System.Drawing.Size(164, 30);
            this.inputertextBox.TabIndex = 5;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("宋体", 15F);
            this.label5.Location = new System.Drawing.Point(711, 80);
            this.label5.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(89, 20);
            this.label5.TabIndex = 4;
            this.label5.Text = "输入时间";
            // 
            // inputdatetextBox
            // 
            this.inputdatetextBox.Location = new System.Drawing.Point(859, 80);
            this.inputdatetextBox.Margin = new System.Windows.Forms.Padding(5);
            this.inputdatetextBox.Name = "inputdatetextBox";
            this.inputdatetextBox.ReadOnly = true;
            this.inputdatetextBox.Size = new System.Drawing.Size(164, 30);
            this.inputdatetextBox.TabIndex = 5;
            // 
            // dataGridViewquery
            // 
            this.dataGridViewquery.AllowUserToAddRows = false;
            this.dataGridViewquery.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.dataGridViewquery.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewquery.Location = new System.Drawing.Point(14, 263);
            this.dataGridViewquery.Margin = new System.Windows.Forms.Padding(5);
            this.dataGridViewquery.Name = "dataGridViewquery";
            this.dataGridViewquery.ReadOnly = true;
            this.dataGridViewquery.RowTemplate.Height = 23;
            this.dataGridViewquery.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridViewquery.Size = new System.Drawing.Size(334, 358);
            this.dataGridViewquery.TabIndex = 1;
            this.dataGridViewquery.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridViewquery_CellClick);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("宋体", 15F);
            this.label6.Location = new System.Drawing.Point(14, 42);
            this.label6.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(129, 20);
            this.label6.TabIndex = 0;
            this.label6.Text = "订单编号查询";
            // 
            // ordernoquerytextBox
            // 
            this.ordernoquerytextBox.Location = new System.Drawing.Point(153, 39);
            this.ordernoquerytextBox.Margin = new System.Windows.Forms.Padding(5);
            this.ordernoquerytextBox.Name = "ordernoquerytextBox";
            this.ordernoquerytextBox.Size = new System.Drawing.Size(164, 30);
            this.ordernoquerytextBox.TabIndex = 2;
            // 
            // button_queryorder
            // 
            this.button_queryorder.Font = new System.Drawing.Font("宋体", 15F);
            this.button_queryorder.Location = new System.Drawing.Point(18, 84);
            this.button_queryorder.Margin = new System.Windows.Forms.Padding(5);
            this.button_queryorder.Name = "button_queryorder";
            this.button_queryorder.Size = new System.Drawing.Size(299, 76);
            this.button_queryorder.TabIndex = 3;
            this.button_queryorder.Text = "查询可设置obe比例的订单";
            this.button_queryorder.UseVisualStyleBackColor = true;
            this.button_queryorder.Click += new System.EventHandler(this.button_queryorder_Click);
            // 
            // button1
            // 
            this.button1.Font = new System.Drawing.Font("宋体", 15F);
            this.button1.Location = new System.Drawing.Point(47, 177);
            this.button1.Margin = new System.Windows.Forms.Padding(5);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(226, 76);
            this.button1.TabIndex = 3;
            this.button1.Text = "批量修改Obe抽检比例Todo";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button_queryorder_Click);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("宋体", 15F);
            this.label7.Location = new System.Drawing.Point(380, 136);
            this.label7.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(49, 20);
            this.label7.TabIndex = 0;
            this.label7.Text = "料号";
            // 
            // matertialNotextBox
            // 
            this.matertialNotextBox.Location = new System.Drawing.Point(528, 126);
            this.matertialNotextBox.Margin = new System.Windows.Forms.Padding(5);
            this.matertialNotextBox.Name = "matertialNotextBox";
            this.matertialNotextBox.Size = new System.Drawing.Size(164, 30);
            this.matertialNotextBox.TabIndex = 2;
            // 
            // ObeRateForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1273, 751);
            this.Controls.Add(this.inputdatetextBox);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.inputertextBox);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.numTextBox);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.delete);
            this.Controls.Add(this.modify);
            this.Controls.Add(this.query);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.button_queryorder);
            this.Controls.Add(this.add);
            this.Controls.Add(this.ordernoquerytextBox);
            this.Controls.Add(this.matertialNotextBox);
            this.Controls.Add(this.ordernoTextBox);
            this.Controls.Add(this.checkrateTextBox);
            this.Controls.Add(this.dataGridViewquery);
            this.Controls.Add(this.dataGridViewinsert);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label1);
            this.Font = new System.Drawing.Font("宋体", 15F);
            this.Margin = new System.Windows.Forms.Padding(5);
            this.Name = "ObeRateForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "OBE抽查比率查看与修改";
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewinsert)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewquery)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGridView dataGridViewinsert;
        private System.Windows.Forms.TextBox checkrateTextBox;
        private System.Windows.Forms.Button add;
        private System.Windows.Forms.Button query;
        private System.Windows.Forms.Button modify;
        private System.Windows.Forms.Button delete;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox numTextBox;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox ordernoTextBox;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox inputertextBox;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox inputdatetextBox;
        private System.Windows.Forms.DataGridView dataGridViewquery;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox ordernoquerytextBox;
        private System.Windows.Forms.Button button_queryorder;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox matertialNotextBox;
    }
}