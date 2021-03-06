﻿namespace SaledServices
{
    partial class BGA_InSheetForm
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
            this.vendorTextBox = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.productTextBox = new System.Windows.Forms.TextBox();
            this.vendormaterialNoTextBox = new System.Windows.Forms.TextBox();
            this.mpnTextBox = new System.Windows.Forms.TextBox();
            this.material_typeTextBox = new System.Windows.Forms.TextBox();
            this.buy_typeTextBox = new System.Windows.Forms.TextBox();
            this.describeTextBox = new System.Windows.Forms.TextBox();
            this.pricePerTextBox = new System.Windows.Forms.TextBox();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
            this.label7 = new System.Windows.Forms.Label();
            this.material_nameTextBox = new System.Windows.Forms.TextBox();
            this.label13 = new System.Windows.Forms.Label();
            this.stock_in_numTextBox = new System.Windows.Forms.TextBox();
            this.buy_order_serial_noComboBox = new System.Windows.Forms.ComboBox();
            this.label17 = new System.Windows.Forms.Label();
            this.isDeclareTextBox = new System.Windows.Forms.TextBox();
            this.label14 = new System.Windows.Forms.Label();
            this.label18 = new System.Windows.Forms.Label();
            this.idTextBox = new System.Windows.Forms.TextBox();
            this.bga_brieftextBox = new System.Windows.Forms.TextBox();
            this.label15 = new System.Windows.Forms.Label();
            this.label19 = new System.Windows.Forms.Label();
            this.inputerTextBox = new System.Windows.Forms.TextBox();
            this.stock_placetextBox = new System.Windows.Forms.TextBox();
            this.label16 = new System.Windows.Forms.Label();
            this.label20 = new System.Windows.Forms.Label();
            this.input_dateTextBox = new System.Windows.Forms.TextBox();
            this.notetextBox = new System.Windows.Forms.TextBox();
            this.label21 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.VGA = new System.Windows.Forms.RadioButton();
            this.PCH = new System.Windows.Forms.RadioButton();
            this.CPU = new System.Windows.Forms.RadioButton();
            this.label22 = new System.Windows.Forms.Label();
            this.bgaSnTextBox = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.orderNumberTextBox = new System.Windows.Forms.TextBox();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel4 = new System.Windows.Forms.TableLayoutPanel();
            this.dataGridViewToReturn = new System.Windows.Forms.DataGridView();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.tableLayoutPanel1.SuspendLayout();
            this.tableLayoutPanel3.SuspendLayout();
            this.panel1.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.tableLayoutPanel4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewToReturn)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // add
            // 
            this.add.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.add.Location = new System.Drawing.Point(120, 6);
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
            this.query.Location = new System.Drawing.Point(460, 6);
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
            this.modify.Location = new System.Drawing.Point(800, 6);
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
            this.delete.Location = new System.Drawing.Point(1141, 6);
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
            this.label1.Location = new System.Drawing.Point(7, 134);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(40, 16);
            this.label1.TabIndex = 4;
            this.label1.Text = "厂商";
            // 
            // vendorTextBox
            // 
            this.vendorTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.vendorTextBox.Location = new System.Drawing.Point(176, 138);
            this.vendorTextBox.Margin = new System.Windows.Forms.Padding(4);
            this.vendorTextBox.Name = "vendorTextBox";
            this.vendorTextBox.ReadOnly = true;
            this.vendorTextBox.Size = new System.Drawing.Size(158, 26);
            this.vendorTextBox.TabIndex = 5;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(7, 174);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(56, 16);
            this.label2.TabIndex = 4;
            this.label2.Text = "客户别";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(345, 42);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(72, 16);
            this.label3.TabIndex = 4;
            this.label3.Text = "厂商料号";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(345, 174);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(40, 16);
            this.label4.TabIndex = 4;
            this.label4.Text = "单价";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(7, 214);
            this.label6.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(72, 16);
            this.label6.TabIndex = 4;
            this.label6.Text = "材料类别";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(345, 81);
            this.label9.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(40, 16);
            this.label9.TabIndex = 4;
            this.label9.Text = "描述";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(7, 3);
            this.label10.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(104, 16);
            this.label10.TabIndex = 4;
            this.label10.Text = "采购订单编号";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(345, 3);
            this.label11.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(72, 16);
            this.label11.TabIndex = 4;
            this.label11.Text = "采购类别";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(7, 42);
            this.label12.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(32, 16);
            this.label12.TabIndex = 4;
            this.label12.Text = "MPN";
            // 
            // productTextBox
            // 
            this.productTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.productTextBox.Location = new System.Drawing.Point(176, 178);
            this.productTextBox.Margin = new System.Windows.Forms.Padding(4);
            this.productTextBox.Name = "productTextBox";
            this.productTextBox.ReadOnly = true;
            this.productTextBox.Size = new System.Drawing.Size(158, 26);
            this.productTextBox.TabIndex = 5;
            // 
            // vendormaterialNoTextBox
            // 
            this.vendormaterialNoTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.vendormaterialNoTextBox.Location = new System.Drawing.Point(514, 46);
            this.vendormaterialNoTextBox.Margin = new System.Windows.Forms.Padding(4);
            this.vendormaterialNoTextBox.Name = "vendormaterialNoTextBox";
            this.vendormaterialNoTextBox.ReadOnly = true;
            this.vendormaterialNoTextBox.Size = new System.Drawing.Size(158, 26);
            this.vendormaterialNoTextBox.TabIndex = 5;
            // 
            // mpnTextBox
            // 
            this.mpnTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mpnTextBox.Location = new System.Drawing.Point(176, 46);
            this.mpnTextBox.Margin = new System.Windows.Forms.Padding(4);
            this.mpnTextBox.Name = "mpnTextBox";
            this.mpnTextBox.ReadOnly = true;
            this.mpnTextBox.Size = new System.Drawing.Size(158, 26);
            this.mpnTextBox.TabIndex = 5;
            this.mpnTextBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.mpnTextBox_KeyPress);
            // 
            // material_typeTextBox
            // 
            this.material_typeTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.material_typeTextBox.Location = new System.Drawing.Point(176, 218);
            this.material_typeTextBox.Margin = new System.Windows.Forms.Padding(4);
            this.material_typeTextBox.Name = "material_typeTextBox";
            this.material_typeTextBox.ReadOnly = true;
            this.material_typeTextBox.Size = new System.Drawing.Size(158, 26);
            this.material_typeTextBox.TabIndex = 5;
            // 
            // buy_typeTextBox
            // 
            this.buy_typeTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.buy_typeTextBox.Location = new System.Drawing.Point(514, 7);
            this.buy_typeTextBox.Margin = new System.Windows.Forms.Padding(4);
            this.buy_typeTextBox.Name = "buy_typeTextBox";
            this.buy_typeTextBox.ReadOnly = true;
            this.buy_typeTextBox.Size = new System.Drawing.Size(158, 26);
            this.buy_typeTextBox.TabIndex = 5;
            // 
            // describeTextBox
            // 
            this.describeTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.describeTextBox.Location = new System.Drawing.Point(514, 85);
            this.describeTextBox.Margin = new System.Windows.Forms.Padding(4);
            this.describeTextBox.Multiline = true;
            this.describeTextBox.Name = "describeTextBox";
            this.describeTextBox.ReadOnly = true;
            this.describeTextBox.Size = new System.Drawing.Size(158, 42);
            this.describeTextBox.TabIndex = 5;
            // 
            // pricePerTextBox
            // 
            this.pricePerTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pricePerTextBox.Location = new System.Drawing.Point(514, 178);
            this.pricePerTextBox.Margin = new System.Windows.Forms.Padding(4);
            this.pricePerTextBox.Name = "pricePerTextBox";
            this.pricePerTextBox.ReadOnly = true;
            this.pricePerTextBox.Size = new System.Drawing.Size(158, 26);
            this.pricePerTextBox.TabIndex = 5;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel3, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel2, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel4, 0, 2);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(4);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 3;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 35.64753F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 6.94259F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 57.27637F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(1370, 749);
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
            this.tableLayoutPanel3.Controls.Add(this.label7, 4, 2);
            this.tableLayoutPanel3.Controls.Add(this.material_nameTextBox, 5, 2);
            this.tableLayoutPanel3.Controls.Add(this.label13, 4, 3);
            this.tableLayoutPanel3.Controls.Add(this.stock_in_numTextBox, 5, 3);
            this.tableLayoutPanel3.Controls.Add(this.pricePerTextBox, 3, 4);
            this.tableLayoutPanel3.Controls.Add(this.describeTextBox, 3, 2);
            this.tableLayoutPanel3.Controls.Add(this.label11, 2, 0);
            this.tableLayoutPanel3.Controls.Add(this.buy_typeTextBox, 3, 0);
            this.tableLayoutPanel3.Controls.Add(this.label4, 2, 4);
            this.tableLayoutPanel3.Controls.Add(this.label9, 2, 2);
            this.tableLayoutPanel3.Controls.Add(this.label3, 2, 1);
            this.tableLayoutPanel3.Controls.Add(this.vendormaterialNoTextBox, 3, 1);
            this.tableLayoutPanel3.Controls.Add(this.label10, 0, 0);
            this.tableLayoutPanel3.Controls.Add(this.buy_order_serial_noComboBox, 1, 0);
            this.tableLayoutPanel3.Controls.Add(this.label17, 4, 0);
            this.tableLayoutPanel3.Controls.Add(this.isDeclareTextBox, 5, 0);
            this.tableLayoutPanel3.Controls.Add(this.label14, 6, 2);
            this.tableLayoutPanel3.Controls.Add(this.label18, 4, 1);
            this.tableLayoutPanel3.Controls.Add(this.idTextBox, 7, 2);
            this.tableLayoutPanel3.Controls.Add(this.bga_brieftextBox, 5, 1);
            this.tableLayoutPanel3.Controls.Add(this.label15, 6, 3);
            this.tableLayoutPanel3.Controls.Add(this.label19, 6, 0);
            this.tableLayoutPanel3.Controls.Add(this.inputerTextBox, 7, 3);
            this.tableLayoutPanel3.Controls.Add(this.stock_placetextBox, 7, 0);
            this.tableLayoutPanel3.Controls.Add(this.label16, 6, 4);
            this.tableLayoutPanel3.Controls.Add(this.label20, 6, 1);
            this.tableLayoutPanel3.Controls.Add(this.input_dateTextBox, 7, 4);
            this.tableLayoutPanel3.Controls.Add(this.notetextBox, 7, 1);
            this.tableLayoutPanel3.Controls.Add(this.label12, 0, 1);
            this.tableLayoutPanel3.Controls.Add(this.mpnTextBox, 1, 1);
            this.tableLayoutPanel3.Controls.Add(this.label6, 0, 5);
            this.tableLayoutPanel3.Controls.Add(this.material_typeTextBox, 1, 5);
            this.tableLayoutPanel3.Controls.Add(this.label2, 0, 4);
            this.tableLayoutPanel3.Controls.Add(this.productTextBox, 1, 4);
            this.tableLayoutPanel3.Controls.Add(this.label1, 0, 3);
            this.tableLayoutPanel3.Controls.Add(this.vendorTextBox, 1, 3);
            this.tableLayoutPanel3.Controls.Add(this.label21, 0, 2);
            this.tableLayoutPanel3.Controls.Add(this.panel1, 1, 2);
            this.tableLayoutPanel3.Controls.Add(this.label22, 4, 4);
            this.tableLayoutPanel3.Controls.Add(this.bgaSnTextBox, 5, 4);
            this.tableLayoutPanel3.Controls.Add(this.label5, 2, 3);
            this.tableLayoutPanel3.Controls.Add(this.orderNumberTextBox, 3, 3);
            this.tableLayoutPanel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel3.Location = new System.Drawing.Point(4, 4);
            this.tableLayoutPanel3.Margin = new System.Windows.Forms.Padding(4);
            this.tableLayoutPanel3.Name = "tableLayoutPanel3";
            this.tableLayoutPanel3.RowCount = 6;
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 15.38461F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 15.38462F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 21.48438F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 16.01563F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 16.01563F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 17.1875F));
            this.tableLayoutPanel3.Size = new System.Drawing.Size(1362, 259);
            this.tableLayoutPanel3.TabIndex = 9;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(683, 81);
            this.label7.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(72, 16);
            this.label7.TabIndex = 9;
            this.label7.Text = "材料名称";
            // 
            // material_nameTextBox
            // 
            this.material_nameTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.material_nameTextBox.Location = new System.Drawing.Point(852, 85);
            this.material_nameTextBox.Margin = new System.Windows.Forms.Padding(4);
            this.material_nameTextBox.Name = "material_nameTextBox";
            this.material_nameTextBox.ReadOnly = true;
            this.material_nameTextBox.Size = new System.Drawing.Size(158, 26);
            this.material_nameTextBox.TabIndex = 25;
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(683, 134);
            this.label13.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(72, 16);
            this.label13.TabIndex = 8;
            this.label13.Text = "入库数量";
            // 
            // stock_in_numTextBox
            // 
            this.stock_in_numTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.stock_in_numTextBox.Enabled = false;
            this.stock_in_numTextBox.Location = new System.Drawing.Point(852, 138);
            this.stock_in_numTextBox.Margin = new System.Windows.Forms.Padding(4);
            this.stock_in_numTextBox.Name = "stock_in_numTextBox";
            this.stock_in_numTextBox.Size = new System.Drawing.Size(158, 26);
            this.stock_in_numTextBox.TabIndex = 20;
            this.stock_in_numTextBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.stock_in_numTextBox_KeyPress);
            // 
            // buy_order_serial_noComboBox
            // 
            this.buy_order_serial_noComboBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.buy_order_serial_noComboBox.FormattingEnabled = true;
            this.buy_order_serial_noComboBox.Location = new System.Drawing.Point(175, 6);
            this.buy_order_serial_noComboBox.Name = "buy_order_serial_noComboBox";
            this.buy_order_serial_noComboBox.Size = new System.Drawing.Size(160, 24);
            this.buy_order_serial_noComboBox.TabIndex = 6;
            this.buy_order_serial_noComboBox.SelectedValueChanged += new System.EventHandler(this.buy_order_serial_noComboBox_SelectedValueChanged);
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Location = new System.Drawing.Point(683, 3);
            this.label17.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(72, 16);
            this.label17.TabIndex = 12;
            this.label17.Text = "是否报关";
            // 
            // isDeclareTextBox
            // 
            this.isDeclareTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.isDeclareTextBox.Location = new System.Drawing.Point(852, 7);
            this.isDeclareTextBox.Margin = new System.Windows.Forms.Padding(4);
            this.isDeclareTextBox.Name = "isDeclareTextBox";
            this.isDeclareTextBox.ReadOnly = true;
            this.isDeclareTextBox.Size = new System.Drawing.Size(158, 26);
            this.isDeclareTextBox.TabIndex = 26;
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(1021, 81);
            this.label14.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(24, 16);
            this.label14.TabIndex = 7;
            this.label14.Text = "ID";
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Location = new System.Drawing.Point(683, 42);
            this.label18.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(64, 16);
            this.label18.TabIndex = 11;
            this.label18.Text = "BGA简述";
            // 
            // idTextBox
            // 
            this.idTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.idTextBox.Enabled = false;
            this.idTextBox.Location = new System.Drawing.Point(1190, 85);
            this.idTextBox.Margin = new System.Windows.Forms.Padding(4);
            this.idTextBox.Name = "idTextBox";
            this.idTextBox.ReadOnly = true;
            this.idTextBox.Size = new System.Drawing.Size(165, 26);
            this.idTextBox.TabIndex = 18;
            // 
            // bga_brieftextBox
            // 
            this.bga_brieftextBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.bga_brieftextBox.Location = new System.Drawing.Point(852, 46);
            this.bga_brieftextBox.Margin = new System.Windows.Forms.Padding(4);
            this.bga_brieftextBox.Name = "bga_brieftextBox";
            this.bga_brieftextBox.ReadOnly = true;
            this.bga_brieftextBox.Size = new System.Drawing.Size(158, 26);
            this.bga_brieftextBox.TabIndex = 24;
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(1021, 134);
            this.label15.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(56, 16);
            this.label15.TabIndex = 14;
            this.label15.Text = "输入人";
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.Location = new System.Drawing.Point(1021, 3);
            this.label19.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(40, 16);
            this.label19.TabIndex = 10;
            this.label19.Text = "库位";
            // 
            // inputerTextBox
            // 
            this.inputerTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.inputerTextBox.Location = new System.Drawing.Point(1190, 138);
            this.inputerTextBox.Margin = new System.Windows.Forms.Padding(4);
            this.inputerTextBox.Name = "inputerTextBox";
            this.inputerTextBox.ReadOnly = true;
            this.inputerTextBox.Size = new System.Drawing.Size(165, 26);
            this.inputerTextBox.TabIndex = 22;
            // 
            // stock_placetextBox
            // 
            this.stock_placetextBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.stock_placetextBox.Location = new System.Drawing.Point(1190, 7);
            this.stock_placetextBox.Margin = new System.Windows.Forms.Padding(4);
            this.stock_placetextBox.Name = "stock_placetextBox";
            this.stock_placetextBox.Size = new System.Drawing.Size(165, 26);
            this.stock_placetextBox.TabIndex = 19;
            this.stock_placetextBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.stock_placetextBox_KeyPress);
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(1021, 174);
            this.label16.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(40, 16);
            this.label16.TabIndex = 13;
            this.label16.Text = "日期";
            // 
            // label20
            // 
            this.label20.AutoSize = true;
            this.label20.Location = new System.Drawing.Point(1021, 42);
            this.label20.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(40, 16);
            this.label20.TabIndex = 15;
            this.label20.Text = "备注";
            // 
            // input_dateTextBox
            // 
            this.input_dateTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.input_dateTextBox.Enabled = false;
            this.input_dateTextBox.Location = new System.Drawing.Point(1190, 178);
            this.input_dateTextBox.Margin = new System.Windows.Forms.Padding(4);
            this.input_dateTextBox.Name = "input_dateTextBox";
            this.input_dateTextBox.ReadOnly = true;
            this.input_dateTextBox.Size = new System.Drawing.Size(165, 26);
            this.input_dateTextBox.TabIndex = 23;
            // 
            // notetextBox
            // 
            this.notetextBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.notetextBox.Location = new System.Drawing.Point(1190, 46);
            this.notetextBox.Margin = new System.Windows.Forms.Padding(4);
            this.notetextBox.Name = "notetextBox";
            this.notetextBox.Size = new System.Drawing.Size(165, 26);
            this.notetextBox.TabIndex = 17;
            // 
            // label21
            // 
            this.label21.AutoSize = true;
            this.label21.Location = new System.Drawing.Point(7, 81);
            this.label21.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(64, 16);
            this.label21.TabIndex = 4;
            this.label21.Text = "BGA类型";
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.VGA);
            this.panel1.Controls.Add(this.PCH);
            this.panel1.Controls.Add(this.CPU);
            this.panel1.Location = new System.Drawing.Point(175, 84);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(160, 30);
            this.panel1.TabIndex = 27;
            // 
            // VGA
            // 
            this.VGA.AutoSize = true;
            this.VGA.Location = new System.Drawing.Point(107, 7);
            this.VGA.Name = "VGA";
            this.VGA.Size = new System.Drawing.Size(50, 20);
            this.VGA.TabIndex = 0;
            this.VGA.TabStop = true;
            this.VGA.Text = "VGA";
            this.VGA.UseVisualStyleBackColor = true;
            this.VGA.CheckedChanged += new System.EventHandler(this.CPU_CheckedChanged);
            // 
            // PCH
            // 
            this.PCH.AutoSize = true;
            this.PCH.Location = new System.Drawing.Point(55, 7);
            this.PCH.Name = "PCH";
            this.PCH.Size = new System.Drawing.Size(50, 20);
            this.PCH.TabIndex = 0;
            this.PCH.TabStop = true;
            this.PCH.Text = "PCH";
            this.PCH.UseVisualStyleBackColor = true;
            this.PCH.CheckedChanged += new System.EventHandler(this.CPU_CheckedChanged);
            // 
            // CPU
            // 
            this.CPU.AutoSize = true;
            this.CPU.Location = new System.Drawing.Point(3, 7);
            this.CPU.Name = "CPU";
            this.CPU.Size = new System.Drawing.Size(50, 20);
            this.CPU.TabIndex = 0;
            this.CPU.TabStop = true;
            this.CPU.Text = "CPU";
            this.CPU.UseVisualStyleBackColor = true;
            this.CPU.CheckedChanged += new System.EventHandler(this.CPU_CheckedChanged);
            // 
            // label22
            // 
            this.label22.AutoSize = true;
            this.label22.Location = new System.Drawing.Point(683, 174);
            this.label22.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label22.Name = "label22";
            this.label22.Size = new System.Drawing.Size(56, 16);
            this.label22.TabIndex = 16;
            this.label22.Text = "BGA SN";
            // 
            // bgaSnTextBox
            // 
            this.bgaSnTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.bgaSnTextBox.Enabled = false;
            this.bgaSnTextBox.Location = new System.Drawing.Point(852, 178);
            this.bgaSnTextBox.Margin = new System.Windows.Forms.Padding(4);
            this.bgaSnTextBox.Name = "bgaSnTextBox";
            this.bgaSnTextBox.Size = new System.Drawing.Size(158, 26);
            this.bgaSnTextBox.TabIndex = 20;
            this.bgaSnTextBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.stock_in_numTextBox_KeyPress);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(345, 134);
            this.label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(72, 16);
            this.label5.TabIndex = 4;
            this.label5.Text = "订单数量";
            // 
            // orderNumberTextBox
            // 
            this.orderNumberTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.orderNumberTextBox.Location = new System.Drawing.Point(514, 138);
            this.orderNumberTextBox.Margin = new System.Windows.Forms.Padding(4);
            this.orderNumberTextBox.Name = "orderNumberTextBox";
            this.orderNumberTextBox.ReadOnly = true;
            this.orderNumberTextBox.Size = new System.Drawing.Size(158, 26);
            this.orderNumberTextBox.TabIndex = 5;
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
            this.tableLayoutPanel2.Location = new System.Drawing.Point(4, 271);
            this.tableLayoutPanel2.Margin = new System.Windows.Forms.Padding(4);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 1;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(1362, 44);
            this.tableLayoutPanel2.TabIndex = 9;
            // 
            // tableLayoutPanel4
            // 
            this.tableLayoutPanel4.ColumnCount = 2;
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 43.91496F));
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 56.08504F));
            this.tableLayoutPanel4.Controls.Add(this.dataGridViewToReturn, 0, 0);
            this.tableLayoutPanel4.Controls.Add(this.dataGridView1, 1, 0);
            this.tableLayoutPanel4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel4.Location = new System.Drawing.Point(3, 322);
            this.tableLayoutPanel4.Name = "tableLayoutPanel4";
            this.tableLayoutPanel4.RowCount = 1;
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel4.Size = new System.Drawing.Size(1364, 424);
            this.tableLayoutPanel4.TabIndex = 10;
            // 
            // dataGridViewToReturn
            // 
            this.dataGridViewToReturn.AllowUserToAddRows = false;
            this.dataGridViewToReturn.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.dataGridViewToReturn.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewToReturn.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridViewToReturn.Location = new System.Drawing.Point(4, 4);
            this.dataGridViewToReturn.Margin = new System.Windows.Forms.Padding(4);
            this.dataGridViewToReturn.Name = "dataGridViewToReturn";
            this.dataGridViewToReturn.ReadOnly = true;
            this.dataGridViewToReturn.RowTemplate.Height = 23;
            this.dataGridViewToReturn.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridViewToReturn.Size = new System.Drawing.Size(591, 416);
            this.dataGridViewToReturn.TabIndex = 8;
            this.dataGridViewToReturn.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridViewToReturn_CellClick);
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(128)))));
            this.dataGridView1.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dataGridView1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView1.Location = new System.Drawing.Point(603, 4);
            this.dataGridView1.Margin = new System.Windows.Forms.Padding(4);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.RowTemplate.Height = 23;
            this.dataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView1.Size = new System.Drawing.Size(757, 416);
            this.dataGridView1.TabIndex = 7;
            this.dataGridView1.VirtualMode = true;
            this.dataGridView1.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellClick);
            // 
            // BGA_InSheetForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(1370, 749);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Font = new System.Drawing.Font("宋体", 12F);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "BGA_InSheetForm";
            this.Text = "BGA入库表";
            this.Load += new System.EventHandler(this.ReceiveOrderForm_Load);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel3.ResumeLayout(false);
            this.tableLayoutPanel3.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel4.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewToReturn)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button add;
        private System.Windows.Forms.Button query;
        private System.Windows.Forms.Button modify;
        private System.Windows.Forms.Button delete;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox vendorTextBox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.TextBox productTextBox;
        private System.Windows.Forms.TextBox vendormaterialNoTextBox;
        private System.Windows.Forms.TextBox mpnTextBox;
        private System.Windows.Forms.TextBox material_typeTextBox;
        private System.Windows.Forms.TextBox buy_typeTextBox;
        private System.Windows.Forms.TextBox describeTextBox;
        private System.Windows.Forms.TextBox pricePerTextBox;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel3;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel4;
        private System.Windows.Forms.DataGridView dataGridViewToReturn;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.ComboBox buy_order_serial_noComboBox;
        private System.Windows.Forms.TextBox input_dateTextBox;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.TextBox inputerTextBox;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.TextBox isDeclareTextBox;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox material_nameTextBox;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.TextBox bga_brieftextBox;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.TextBox stock_in_numTextBox;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.TextBox stock_placetextBox;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.TextBox idTextBox;
        private System.Windows.Forms.Label label20;
        private System.Windows.Forms.TextBox notetextBox;
        private System.Windows.Forms.Label label21;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.RadioButton VGA;
        private System.Windows.Forms.RadioButton PCH;
        private System.Windows.Forms.RadioButton CPU;
        private System.Windows.Forms.Label label22;
        private System.Windows.Forms.TextBox bgaSnTextBox;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox orderNumberTextBox;
    }
}