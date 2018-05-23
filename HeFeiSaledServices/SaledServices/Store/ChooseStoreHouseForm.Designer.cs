namespace SaledServices
{
    partial class ChooseStoreHouseForm
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            this.label1 = new System.Windows.Forms.Label();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.placeTextBox = new System.Windows.Forms.TextBox();
            this.choose = new System.Windows.Forms.Button();
            this.query = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.numTextBox = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.numbertextBox = new System.Windows.Forms.TextBox();
            this.mpntextBox = new System.Windows.Forms.TextBox();
            this.releasePlacebutton = new System.Windows.Forms.Button();
            this.label6 = new System.Windows.Forms.Label();
            this.houseComboBox = new System.Windows.Forms.ComboBox();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("宋体", 15F);
            this.label1.Location = new System.Drawing.Point(436, 59);
            this.label1.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(49, 20);
            this.label1.TabIndex = 0;
            this.label1.Text = "储位";
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(128)))));
            this.dataGridView1.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(109, 215);
            this.dataGridView1.Margin = new System.Windows.Forms.Padding(5);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.RowTemplate.Height = 23;
            this.dataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView1.Size = new System.Drawing.Size(618, 323);
            this.dataGridView1.TabIndex = 1;
            this.dataGridView1.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellClick);
            // 
            // placeTextBox
            // 
            this.placeTextBox.Location = new System.Drawing.Point(588, 56);
            this.placeTextBox.Margin = new System.Windows.Forms.Padding(5);
            this.placeTextBox.Name = "placeTextBox";
            this.placeTextBox.Size = new System.Drawing.Size(164, 30);
            this.placeTextBox.TabIndex = 2;
            // 
            // choose
            // 
            this.choose.Enabled = false;
            this.choose.Font = new System.Drawing.Font("宋体", 15F);
            this.choose.Location = new System.Drawing.Point(496, 150);
            this.choose.Margin = new System.Windows.Forms.Padding(5);
            this.choose.Name = "choose";
            this.choose.Size = new System.Drawing.Size(145, 55);
            this.choose.TabIndex = 3;
            this.choose.Text = "选择";
            this.choose.UseVisualStyleBackColor = true;
            this.choose.Click += new System.EventHandler(this.choose_Click);
            // 
            // query
            // 
            this.query.Font = new System.Drawing.Font("宋体", 15F);
            this.query.Location = new System.Drawing.Point(192, 150);
            this.query.Margin = new System.Windows.Forms.Padding(5);
            this.query.Name = "query";
            this.query.Size = new System.Drawing.Size(150, 55);
            this.query.TabIndex = 3;
            this.query.Text = "查询";
            this.query.UseVisualStyleBackColor = true;
            this.query.Click += new System.EventHandler(this.query_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("宋体", 15F);
            this.label2.Location = new System.Drawing.Point(95, 14);
            this.label2.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(29, 20);
            this.label2.TabIndex = 4;
            this.label2.Text = "ID";
            // 
            // numTextBox
            // 
            this.numTextBox.Location = new System.Drawing.Point(243, 14);
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
            this.label3.Location = new System.Drawing.Point(91, 56);
            this.label3.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(49, 20);
            this.label3.TabIndex = 0;
            this.label3.Text = "库房";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("宋体", 15F);
            this.label4.Location = new System.Drawing.Point(436, 110);
            this.label4.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(89, 20);
            this.label4.TabIndex = 0;
            this.label4.Text = "已存数量";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("宋体", 15F);
            this.label5.Location = new System.Drawing.Point(91, 110);
            this.label5.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(89, 20);
            this.label5.TabIndex = 0;
            this.label5.Text = "存储料号";
            // 
            // numbertextBox
            // 
            this.numbertextBox.Location = new System.Drawing.Point(588, 107);
            this.numbertextBox.Margin = new System.Windows.Forms.Padding(5);
            this.numbertextBox.Name = "numbertextBox";
            this.numbertextBox.ReadOnly = true;
            this.numbertextBox.Size = new System.Drawing.Size(164, 30);
            this.numbertextBox.TabIndex = 2;
            // 
            // mpntextBox
            // 
            this.mpntextBox.Location = new System.Drawing.Point(243, 110);
            this.mpntextBox.Margin = new System.Windows.Forms.Padding(5);
            this.mpntextBox.Name = "mpntextBox";
            this.mpntextBox.ReadOnly = true;
            this.mpntextBox.Size = new System.Drawing.Size(164, 30);
            this.mpntextBox.TabIndex = 2;
            // 
            // releasePlacebutton
            // 
            this.releasePlacebutton.Enabled = false;
            this.releasePlacebutton.Font = new System.Drawing.Font("宋体", 15F);
            this.releasePlacebutton.Location = new System.Drawing.Point(737, 333);
            this.releasePlacebutton.Margin = new System.Windows.Forms.Padding(5);
            this.releasePlacebutton.Name = "releasePlacebutton";
            this.releasePlacebutton.Size = new System.Drawing.Size(125, 55);
            this.releasePlacebutton.TabIndex = 3;
            this.releasePlacebutton.Text = "释放库位";
            this.releasePlacebutton.UseVisualStyleBackColor = true;
            this.releasePlacebutton.Click += new System.EventHandler(this.releasePlacebutton_Click);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(584, 14);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(129, 20);
            this.label6.TabIndex = 7;
            this.label6.Text = "可以模糊查询";
            // 
            // houseComboBox
            // 
            this.houseComboBox.FormattingEnabled = true;
            this.houseComboBox.Location = new System.Drawing.Point(243, 65);
            this.houseComboBox.Name = "houseComboBox";
            this.houseComboBox.Size = new System.Drawing.Size(164, 28);
            this.houseComboBox.TabIndex = 8;
            this.houseComboBox.SelectedValueChanged += new System.EventHandler(this.houseComboBox_SelectedValueChanged);
            // 
            // ChooseStoreHouseForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(918, 716);
            this.Controls.Add(this.houseComboBox);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.numTextBox);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.releasePlacebutton);
            this.Controls.Add(this.query);
            this.Controls.Add(this.choose);
            this.Controls.Add(this.mpntextBox);
            this.Controls.Add(this.numbertextBox);
            this.Controls.Add(this.placeTextBox);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label1);
            this.Font = new System.Drawing.Font("宋体", 15F);
            this.Margin = new System.Windows.Forms.Padding(5);
            this.Name = "ChooseStoreHouseForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "选择需要的库房库位";
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.TextBox placeTextBox;
        private System.Windows.Forms.Button choose;
        private System.Windows.Forms.Button query;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox numTextBox;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox numbertextBox;
        private System.Windows.Forms.TextBox mpntextBox;
        private System.Windows.Forms.Button releasePlacebutton;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ComboBox houseComboBox;
    }
}