namespace SaledServices
{
    partial class DPKForm
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
            this.add = new System.Windows.Forms.Button();
            this.query = new System.Windows.Forms.Button();
            this.modify = new System.Windows.Forms.Button();
            this.delete = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.dpk_order_noTextBox = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.dpk_typeTextBox = new System.Windows.Forms.TextBox();
            this.statusTextBox = new System.Windows.Forms.TextBox();
            this.KEYIDTextBox = new System.Windows.Forms.TextBox();
            this.KEYSERIALTextBox = new System.Windows.Forms.TextBox();
            this.upload_dateTextBox = new System.Windows.Forms.TextBox();
            this.KEYPNTextBox = new System.Windows.Forms.TextBox();
            this.burn_dateTextBox = new System.Windows.Forms.TextBox();
            this.custom_serial_noTextBox = new System.Windows.Forms.TextBox();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.label14 = new System.Windows.Forms.Label();
            this.idTextBox = new System.Windows.Forms.TextBox();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.tableLayoutPanel1.SuspendLayout();
            this.tableLayoutPanel3.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // add
            // 
            this.add.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.add.Location = new System.Drawing.Point(125, 15);
            this.add.Margin = new System.Windows.Forms.Padding(4);
            this.add.Name = "add";
            this.add.Size = new System.Drawing.Size(100, 31);
            this.add.TabIndex = 0;
            this.add.Text = "新增";
            this.add.UseVisualStyleBackColor = true;
            this.add.Click += new System.EventHandler(this.add_Click);
            // 
            // query
            // 
            this.query.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.query.Location = new System.Drawing.Point(475, 15);
            this.query.Margin = new System.Windows.Forms.Padding(4);
            this.query.Name = "query";
            this.query.Size = new System.Drawing.Size(100, 31);
            this.query.TabIndex = 1;
            this.query.Text = "查询";
            this.query.UseVisualStyleBackColor = true;
            this.query.Click += new System.EventHandler(this.query_Click);
            // 
            // modify
            // 
            this.modify.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.modify.Location = new System.Drawing.Point(825, 15);
            this.modify.Margin = new System.Windows.Forms.Padding(4);
            this.modify.Name = "modify";
            this.modify.Size = new System.Drawing.Size(100, 31);
            this.modify.TabIndex = 2;
            this.modify.Text = "修改";
            this.modify.UseVisualStyleBackColor = true;
            this.modify.Click += new System.EventHandler(this.modify_Click);
            // 
            // delete
            // 
            this.delete.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.delete.Location = new System.Drawing.Point(1175, 15);
            this.delete.Margin = new System.Windows.Forms.Padding(4);
            this.delete.Name = "delete";
            this.delete.Size = new System.Drawing.Size(100, 31);
            this.delete.TabIndex = 3;
            this.delete.Text = "删除";
            this.delete.UseVisualStyleBackColor = true;
            this.delete.Click += new System.EventHandler(this.delete_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(7, 3);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(96, 16);
            this.label1.TabIndex = 4;
            this.label1.Text = "DPK订单编号";
            // 
            // dpk_order_noTextBox
            // 
            this.dpk_order_noTextBox.Location = new System.Drawing.Point(181, 7);
            this.dpk_order_noTextBox.Margin = new System.Windows.Forms.Padding(4);
            this.dpk_order_noTextBox.Name = "dpk_order_noTextBox";
            this.dpk_order_noTextBox.Size = new System.Drawing.Size(163, 26);
            this.dpk_order_noTextBox.TabIndex = 5;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(7, 64);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(64, 16);
            this.label2.TabIndex = 4;
            this.label2.Text = "DPK类别";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(355, 64);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(40, 16);
            this.label3.TabIndex = 4;
            this.label3.Text = "状态";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(355, 186);
            this.label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(72, 16);
            this.label5.TabIndex = 4;
            this.label5.Text = "客户序号";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(7, 247);
            this.label6.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(80, 16);
            this.label6.TabIndex = 4;
            this.label6.Text = "KEYSERIAL";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(355, 125);
            this.label9.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(72, 16);
            this.label9.TabIndex = 4;
            this.label9.Text = "烧录日期";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(7, 125);
            this.label10.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(48, 16);
            this.label10.TabIndex = 4;
            this.label10.Text = "KEYPN";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(355, 3);
            this.label11.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(72, 16);
            this.label11.TabIndex = 4;
            this.label11.Text = "上传日期";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(7, 186);
            this.label12.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(48, 16);
            this.label12.TabIndex = 4;
            this.label12.Text = "KEYID";
            // 
            // dpk_typeTextBox
            // 
            this.dpk_typeTextBox.Location = new System.Drawing.Point(181, 68);
            this.dpk_typeTextBox.Margin = new System.Windows.Forms.Padding(4);
            this.dpk_typeTextBox.Name = "dpk_typeTextBox";
            this.dpk_typeTextBox.Size = new System.Drawing.Size(163, 26);
            this.dpk_typeTextBox.TabIndex = 5;
            // 
            // statusTextBox
            // 
            this.statusTextBox.Location = new System.Drawing.Point(529, 68);
            this.statusTextBox.Margin = new System.Windows.Forms.Padding(4);
            this.statusTextBox.Name = "statusTextBox";
            this.statusTextBox.Size = new System.Drawing.Size(163, 26);
            this.statusTextBox.TabIndex = 5;
            // 
            // KEYIDTextBox
            // 
            this.KEYIDTextBox.Location = new System.Drawing.Point(181, 190);
            this.KEYIDTextBox.Margin = new System.Windows.Forms.Padding(4);
            this.KEYIDTextBox.Name = "KEYIDTextBox";
            this.KEYIDTextBox.Size = new System.Drawing.Size(163, 26);
            this.KEYIDTextBox.TabIndex = 5;
            // 
            // KEYSERIALTextBox
            // 
            this.KEYSERIALTextBox.Location = new System.Drawing.Point(181, 251);
            this.KEYSERIALTextBox.Margin = new System.Windows.Forms.Padding(4);
            this.KEYSERIALTextBox.Name = "KEYSERIALTextBox";
            this.KEYSERIALTextBox.Size = new System.Drawing.Size(163, 26);
            this.KEYSERIALTextBox.TabIndex = 5;
            // 
            // upload_dateTextBox
            // 
            this.upload_dateTextBox.Location = new System.Drawing.Point(529, 7);
            this.upload_dateTextBox.Margin = new System.Windows.Forms.Padding(4);
            this.upload_dateTextBox.Name = "upload_dateTextBox";
            this.upload_dateTextBox.Size = new System.Drawing.Size(163, 26);
            this.upload_dateTextBox.TabIndex = 5;
            // 
            // KEYPNTextBox
            // 
            this.KEYPNTextBox.Location = new System.Drawing.Point(181, 129);
            this.KEYPNTextBox.Margin = new System.Windows.Forms.Padding(4);
            this.KEYPNTextBox.Name = "KEYPNTextBox";
            this.KEYPNTextBox.Size = new System.Drawing.Size(163, 26);
            this.KEYPNTextBox.TabIndex = 5;
            // 
            // burn_dateTextBox
            // 
            this.burn_dateTextBox.Location = new System.Drawing.Point(529, 129);
            this.burn_dateTextBox.Margin = new System.Windows.Forms.Padding(4);
            this.burn_dateTextBox.Name = "burn_dateTextBox";
            this.burn_dateTextBox.Size = new System.Drawing.Size(163, 26);
            this.burn_dateTextBox.TabIndex = 5;
            // 
            // custom_serial_noTextBox
            // 
            this.custom_serial_noTextBox.Location = new System.Drawing.Point(529, 190);
            this.custom_serial_noTextBox.Margin = new System.Windows.Forms.Padding(4);
            this.custom_serial_noTextBox.Name = "custom_serial_noTextBox";
            this.custom_serial_noTextBox.Size = new System.Drawing.Size(163, 26);
            this.custom_serial_noTextBox.TabIndex = 5;
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(128)))));
            this.dataGridView1.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dataGridView1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView1.Location = new System.Drawing.Point(4, 393);
            this.dataGridView1.Margin = new System.Windows.Forms.Padding(4);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.RowTemplate.Height = 23;
            this.dataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView1.Size = new System.Drawing.Size(1400, 564);
            this.dataGridView1.TabIndex = 6;
            this.dataGridView1.VirtualMode = true;
            this.dataGridView1.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellClick);
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(355, 247);
            this.label14.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(24, 16);
            this.label14.TabIndex = 4;
            this.label14.Text = "ID";
            // 
            // idTextBox
            // 
            this.idTextBox.Enabled = false;
            this.idTextBox.Location = new System.Drawing.Point(529, 251);
            this.idTextBox.Margin = new System.Windows.Forms.Padding(4);
            this.idTextBox.Name = "idTextBox";
            this.idTextBox.ReadOnly = true;
            this.idTextBox.Size = new System.Drawing.Size(163, 26);
            this.idTextBox.TabIndex = 5;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel3, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel2, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.dataGridView1, 0, 2);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(4);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 3;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 7.284079F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 59.41727F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(1408, 961);
            this.tableLayoutPanel1.TabIndex = 8;
            // 
            // tableLayoutPanel3
            // 
            this.tableLayoutPanel3.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.InsetDouble;
            this.tableLayoutPanel3.ColumnCount = 8;
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 12.5F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 12.5F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 12.5F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 12.5F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 12.5F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 12.5F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 12.5F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 12.5F));
            this.tableLayoutPanel3.Controls.Add(this.label1, 0, 0);
            this.tableLayoutPanel3.Controls.Add(this.dpk_order_noTextBox, 1, 0);
            this.tableLayoutPanel3.Controls.Add(this.label2, 0, 1);
            this.tableLayoutPanel3.Controls.Add(this.KEYSERIALTextBox, 1, 4);
            this.tableLayoutPanel3.Controls.Add(this.custom_serial_noTextBox, 3, 3);
            this.tableLayoutPanel3.Controls.Add(this.dpk_typeTextBox, 1, 1);
            this.tableLayoutPanel3.Controls.Add(this.burn_dateTextBox, 3, 2);
            this.tableLayoutPanel3.Controls.Add(this.KEYIDTextBox, 1, 3);
            this.tableLayoutPanel3.Controls.Add(this.label12, 0, 3);
            this.tableLayoutPanel3.Controls.Add(this.label6, 0, 4);
            this.tableLayoutPanel3.Controls.Add(this.label11, 2, 0);
            this.tableLayoutPanel3.Controls.Add(this.upload_dateTextBox, 3, 0);
            this.tableLayoutPanel3.Controls.Add(this.label9, 2, 2);
            this.tableLayoutPanel3.Controls.Add(this.label5, 2, 3);
            this.tableLayoutPanel3.Controls.Add(this.label3, 2, 1);
            this.tableLayoutPanel3.Controls.Add(this.label10, 0, 2);
            this.tableLayoutPanel3.Controls.Add(this.statusTextBox, 3, 1);
            this.tableLayoutPanel3.Controls.Add(this.KEYPNTextBox, 1, 2);
            this.tableLayoutPanel3.Controls.Add(this.label14, 2, 4);
            this.tableLayoutPanel3.Controls.Add(this.idTextBox, 3, 4);
            this.tableLayoutPanel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel3.Location = new System.Drawing.Point(4, 4);
            this.tableLayoutPanel3.Margin = new System.Windows.Forms.Padding(4);
            this.tableLayoutPanel3.Name = "tableLayoutPanel3";
            this.tableLayoutPanel3.RowCount = 5;
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel3.Size = new System.Drawing.Size(1400, 312);
            this.tableLayoutPanel3.TabIndex = 9;
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 4;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel2.Controls.Add(this.add, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.query, 1, 0);
            this.tableLayoutPanel2.Controls.Add(this.modify, 2, 0);
            this.tableLayoutPanel2.Controls.Add(this.delete, 3, 0);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(4, 324);
            this.tableLayoutPanel2.Margin = new System.Windows.Forms.Padding(4);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 1;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(1400, 61);
            this.tableLayoutPanel2.TabIndex = 9;
            // 
            // DPKForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(1408, 961);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Font = new System.Drawing.Font("SimSun", 12F);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "DPKForm";
            this.Text = "导入的DPK表";
            this.Load += new System.EventHandler(this.ReceiveOrderForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel3.ResumeLayout(false);
            this.tableLayoutPanel3.PerformLayout();
            this.tableLayoutPanel2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button add;
        private System.Windows.Forms.Button query;
        private System.Windows.Forms.Button modify;
        private System.Windows.Forms.Button delete;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox dpk_order_noTextBox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.TextBox dpk_typeTextBox;
        private System.Windows.Forms.TextBox statusTextBox;
        private System.Windows.Forms.TextBox KEYIDTextBox;
        private System.Windows.Forms.TextBox KEYSERIALTextBox;
        private System.Windows.Forms.TextBox upload_dateTextBox;
        private System.Windows.Forms.TextBox KEYPNTextBox;
        private System.Windows.Forms.TextBox burn_dateTextBox;
        private System.Windows.Forms.TextBox custom_serial_noTextBox;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.TextBox idTextBox;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel3;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
    }
}