namespace SaledServices
{
    partial class EcoForm
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
            this.label3 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.currentVersionTextBox = new System.Windows.Forms.TextBox();
            this.custom_material_noTextBox = new System.Windows.Forms.TextBox();
            this.mpnTextBox = new System.Windows.Forms.TextBox();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.label14 = new System.Windows.Forms.Label();
            this.newVersionTextBox = new System.Windows.Forms.TextBox();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
            this.label18 = new System.Windows.Forms.Label();
            this.track_serial_no = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.inputerTextBox = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.inputDateTextBox = new System.Windows.Forms.TextBox();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.linkLabel1 = new System.Windows.Forms.LinkLabel();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.tableLayoutPanel1.SuspendLayout();
            this.tableLayoutPanel3.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // add
            // 
            this.add.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.add.Location = new System.Drawing.Point(89, 6);
            this.add.Margin = new System.Windows.Forms.Padding(4);
            this.add.Name = "add";
            this.add.Size = new System.Drawing.Size(100, 27);
            this.add.TabIndex = 0;
            this.add.Text = "新增";
            this.add.UseVisualStyleBackColor = true;
            this.add.Click += new System.EventHandler(this.add_Click);
            // 
            // query
            // 
            this.query.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.query.Location = new System.Drawing.Point(365, 6);
            this.query.Margin = new System.Windows.Forms.Padding(4);
            this.query.Name = "query";
            this.query.Size = new System.Drawing.Size(100, 27);
            this.query.TabIndex = 1;
            this.query.Text = "查询";
            this.query.UseVisualStyleBackColor = true;
            this.query.Click += new System.EventHandler(this.query_Click);
            // 
            // modify
            // 
            this.modify.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.modify.Location = new System.Drawing.Point(641, 6);
            this.modify.Margin = new System.Windows.Forms.Padding(4);
            this.modify.Name = "modify";
            this.modify.Size = new System.Drawing.Size(100, 27);
            this.modify.TabIndex = 2;
            this.modify.Text = "修改";
            this.modify.UseVisualStyleBackColor = true;
            this.modify.Click += new System.EventHandler(this.modify_Click);
            // 
            // delete
            // 
            this.delete.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.delete.Location = new System.Drawing.Point(918, 6);
            this.delete.Margin = new System.Windows.Forms.Padding(4);
            this.delete.Name = "delete";
            this.delete.Size = new System.Drawing.Size(100, 27);
            this.delete.TabIndex = 3;
            this.delete.Text = "删除";
            this.delete.UseVisualStyleBackColor = true;
            this.delete.Click += new System.EventHandler(this.delete_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(375, 3);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(72, 16);
            this.label3.TabIndex = 4;
            this.label3.Text = "当前版本";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(7, 38);
            this.label10.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(32, 16);
            this.label10.TabIndex = 4;
            this.label10.Text = "MPN";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(7, 73);
            this.label12.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(72, 16);
            this.label12.TabIndex = 4;
            this.label12.Text = "客户料号";
            // 
            // currentVersionTextBox
            // 
            this.currentVersionTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.currentVersionTextBox.Location = new System.Drawing.Point(559, 7);
            this.currentVersionTextBox.Margin = new System.Windows.Forms.Padding(4);
            this.currentVersionTextBox.Name = "currentVersionTextBox";
            this.currentVersionTextBox.Size = new System.Drawing.Size(173, 26);
            this.currentVersionTextBox.TabIndex = 5;
            // 
            // custom_material_noTextBox
            // 
            this.custom_material_noTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.custom_material_noTextBox.Location = new System.Drawing.Point(191, 77);
            this.custom_material_noTextBox.Margin = new System.Windows.Forms.Padding(4);
            this.custom_material_noTextBox.Name = "custom_material_noTextBox";
            this.custom_material_noTextBox.ReadOnly = true;
            this.custom_material_noTextBox.Size = new System.Drawing.Size(173, 26);
            this.custom_material_noTextBox.TabIndex = 5;
            // 
            // mpnTextBox
            // 
            this.mpnTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mpnTextBox.Location = new System.Drawing.Point(191, 42);
            this.mpnTextBox.Margin = new System.Windows.Forms.Padding(4);
            this.mpnTextBox.Name = "mpnTextBox";
            this.mpnTextBox.ReadOnly = true;
            this.mpnTextBox.Size = new System.Drawing.Size(173, 26);
            this.mpnTextBox.TabIndex = 5;
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(128)))));
            this.dataGridView1.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dataGridView1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView1.Location = new System.Drawing.Point(6, 270);
            this.dataGridView1.Margin = new System.Windows.Forms.Padding(4);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.RowTemplate.Height = 23;
            this.dataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView1.Size = new System.Drawing.Size(1109, 165);
            this.dataGridView1.TabIndex = 6;
            this.dataGridView1.VirtualMode = true;
            this.dataGridView1.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellClick);
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(375, 38);
            this.label14.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(56, 16);
            this.label14.TabIndex = 4;
            this.label14.Text = "新版本";
            // 
            // newVersionTextBox
            // 
            this.newVersionTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.newVersionTextBox.Location = new System.Drawing.Point(559, 42);
            this.newVersionTextBox.Margin = new System.Windows.Forms.Padding(4);
            this.newVersionTextBox.Name = "newVersionTextBox";
            this.newVersionTextBox.Size = new System.Drawing.Size(173, 26);
            this.newVersionTextBox.TabIndex = 5;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.tableLayoutPanel1.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.Outset;
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel3, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel2, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.dataGridView1, 0, 3);
            this.tableLayoutPanel1.Controls.Add(this.linkLabel1, 0, 1);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(4);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 4;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 26.87927F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 22.09567F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 11.09947F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 39.68518F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(1121, 441);
            this.tableLayoutPanel1.TabIndex = 8;
            // 
            // tableLayoutPanel3
            // 
            this.tableLayoutPanel3.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.InsetDouble;
            this.tableLayoutPanel3.ColumnCount = 6;
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 12.5F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 12.5F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 12.5F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 12.5F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 12.5F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 12.5F));
            this.tableLayoutPanel3.Controls.Add(this.label18, 0, 0);
            this.tableLayoutPanel3.Controls.Add(this.track_serial_no, 1, 0);
            this.tableLayoutPanel3.Controls.Add(this.label10, 0, 1);
            this.tableLayoutPanel3.Controls.Add(this.mpnTextBox, 1, 1);
            this.tableLayoutPanel3.Controls.Add(this.label12, 0, 2);
            this.tableLayoutPanel3.Controls.Add(this.custom_material_noTextBox, 1, 2);
            this.tableLayoutPanel3.Controls.Add(this.label3, 2, 0);
            this.tableLayoutPanel3.Controls.Add(this.currentVersionTextBox, 3, 0);
            this.tableLayoutPanel3.Controls.Add(this.label14, 2, 1);
            this.tableLayoutPanel3.Controls.Add(this.newVersionTextBox, 3, 1);
            this.tableLayoutPanel3.Controls.Add(this.label1, 2, 2);
            this.tableLayoutPanel3.Controls.Add(this.inputerTextBox, 3, 2);
            this.tableLayoutPanel3.Controls.Add(this.label2, 4, 2);
            this.tableLayoutPanel3.Controls.Add(this.inputDateTextBox, 5, 2);
            this.tableLayoutPanel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel3.Location = new System.Drawing.Point(6, 6);
            this.tableLayoutPanel3.Margin = new System.Windows.Forms.Padding(4);
            this.tableLayoutPanel3.Name = "tableLayoutPanel3";
            this.tableLayoutPanel3.RowCount = 3;
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel3.Size = new System.Drawing.Size(1109, 108);
            this.tableLayoutPanel3.TabIndex = 9;
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Location = new System.Drawing.Point(7, 3);
            this.label18.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(72, 16);
            this.label18.TabIndex = 4;
            this.label18.Text = "跟踪条码";
            // 
            // track_serial_no
            // 
            this.track_serial_no.Dock = System.Windows.Forms.DockStyle.Fill;
            this.track_serial_no.Location = new System.Drawing.Point(191, 7);
            this.track_serial_no.Margin = new System.Windows.Forms.Padding(4);
            this.track_serial_no.Name = "track_serial_no";
            this.track_serial_no.Size = new System.Drawing.Size(173, 26);
            this.track_serial_no.TabIndex = 5;
            this.track_serial_no.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.track_serial_no_KeyPress);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(375, 73);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(56, 16);
            this.label1.TabIndex = 4;
            this.label1.Text = "输入人";
            // 
            // inputerTextBox
            // 
            this.inputerTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.inputerTextBox.Enabled = false;
            this.inputerTextBox.Location = new System.Drawing.Point(559, 77);
            this.inputerTextBox.Margin = new System.Windows.Forms.Padding(4);
            this.inputerTextBox.Name = "inputerTextBox";
            this.inputerTextBox.ReadOnly = true;
            this.inputerTextBox.Size = new System.Drawing.Size(173, 26);
            this.inputerTextBox.TabIndex = 5;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(743, 73);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(72, 16);
            this.label2.TabIndex = 4;
            this.label2.Text = "输入时间";
            // 
            // inputDateTextBox
            // 
            this.inputDateTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.inputDateTextBox.Enabled = false;
            this.inputDateTextBox.Location = new System.Drawing.Point(927, 77);
            this.inputDateTextBox.Margin = new System.Windows.Forms.Padding(4);
            this.inputDateTextBox.Name = "inputDateTextBox";
            this.inputDateTextBox.ReadOnly = true;
            this.inputDateTextBox.Size = new System.Drawing.Size(175, 26);
            this.inputDateTextBox.TabIndex = 5;
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.Outset;
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
            this.tableLayoutPanel2.Location = new System.Drawing.Point(6, 221);
            this.tableLayoutPanel2.Margin = new System.Windows.Forms.Padding(4);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 1;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(1109, 39);
            this.tableLayoutPanel2.TabIndex = 9;
            // 
            // linkLabel1
            // 
            this.linkLabel1.AutoSize = true;
            this.linkLabel1.Location = new System.Drawing.Point(5, 120);
            this.linkLabel1.Name = "linkLabel1";
            this.linkLabel1.Size = new System.Drawing.Size(88, 16);
            this.linkLabel1.TabIndex = 10;
            this.linkLabel1.TabStop = true;
            this.linkLabel1.Text = "操作说明书";
            this.linkLabel1.Click += new System.EventHandler(this.linkLabel1_Click);
            // 
            // EcoForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(1121, 441);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Font = new System.Drawing.Font("宋体", 12F);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "EcoForm";
            this.Text = "ECO";
            this.Load += new System.EventHandler(this.ReceiveOrderForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
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
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.TextBox currentVersionTextBox;
        private System.Windows.Forms.TextBox custom_material_noTextBox;
        private System.Windows.Forms.TextBox mpnTextBox;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.TextBox newVersionTextBox;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel3;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.TextBox track_serial_no;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox inputerTextBox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox inputDateTextBox;
        private System.Windows.Forms.LinkLabel linkLabel1;
    }
}