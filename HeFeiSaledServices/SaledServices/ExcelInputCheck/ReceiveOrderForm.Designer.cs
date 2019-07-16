namespace SaledServices
{
    partial class ReceiveOrderForm
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
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.productTextBox = new System.Windows.Forms.TextBox();
            this.ordernoTextBox = new System.Windows.Forms.TextBox();
            this.custom_materialNoTextBox = new System.Windows.Forms.TextBox();
            this.custom_material_describeTextBox = new System.Windows.Forms.TextBox();
            this.ordernumTextBox = new System.Windows.Forms.TextBox();
            this.mb_briefTextBox = new System.Windows.Forms.TextBox();
            this.vendor_materialNoTextBox = new System.Windows.Forms.TextBox();
            this.usernameTextBox = new System.Windows.Forms.TextBox();
            this.ordertimeTextBox = new System.Windows.Forms.TextBox();
            this.receivedNumTextBox = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.receivedateTextBox = new System.Windows.Forms.TextBox();
            this.label13 = new System.Windows.Forms.Label();
            this.statusTextBox = new System.Windows.Forms.TextBox();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.label14 = new System.Windows.Forms.Label();
            this.idTextBox = new System.Windows.Forms.TextBox();
            this.label15 = new System.Windows.Forms.Label();
            this.storeHouseComboBox = new System.Windows.Forms.ComboBox();
            this.label16 = new System.Windows.Forms.Label();
            this.returnNumTextBox = new System.Windows.Forms.TextBox();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
            this.label17 = new System.Windows.Forms.Label();
            this.label18 = new System.Windows.Forms.Label();
            this.label19 = new System.Windows.Forms.Label();
            this.declare_unittextBox = new System.Windows.Forms.TextBox();
            this.declare_numbertextBox = new System.Windows.Forms.TextBox();
            this.custom_request_numbertextBox = new System.Windows.Forms.TextBox();
            this.label20 = new System.Windows.Forms.Label();
            this.cidNumberTextBox = new System.Windows.Forms.TextBox();
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
            this.add.Location = new System.Drawing.Point(120, 11);
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
            this.query.Location = new System.Drawing.Point(460, 11);
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
            this.modify.Location = new System.Drawing.Point(800, 11);
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
            this.delete.Enabled = false;
            this.delete.Location = new System.Drawing.Point(1141, 11);
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
            this.label1.Size = new System.Drawing.Size(40, 16);
            this.label1.TabIndex = 4;
            this.label1.Text = "厂商";
            // 
            // vendorTextBox
            // 
            this.vendorTextBox.BackColor = System.Drawing.Color.LightSalmon;
            this.vendorTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.vendorTextBox.Location = new System.Drawing.Point(176, 7);
            this.vendorTextBox.Margin = new System.Windows.Forms.Padding(4);
            this.vendorTextBox.Name = "vendorTextBox";
            this.vendorTextBox.Size = new System.Drawing.Size(158, 26);
            this.vendorTextBox.TabIndex = 5;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(7, 50);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(56, 16);
            this.label2.TabIndex = 4;
            this.label2.Text = "客户别";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(7, 97);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(72, 16);
            this.label3.TabIndex = 4;
            this.label3.Text = "订单编号";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(345, 191);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(72, 16);
            this.label4.TabIndex = 4;
            this.label4.Text = "制单时间";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(345, 144);
            this.label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(56, 16);
            this.label5.TabIndex = 4;
            this.label5.Text = "制单人";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(7, 191);
            this.label6.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(104, 16);
            this.label6.TabIndex = 4;
            this.label6.Text = "客户物料描述";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(683, 3);
            this.label7.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(72, 16);
            this.label7.TabIndex = 4;
            this.label7.Text = "收货数量";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(345, 97);
            this.label9.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(72, 16);
            this.label9.TabIndex = 4;
            this.label9.Text = "厂商料号";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(345, 50);
            this.label10.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(56, 16);
            this.label10.TabIndex = 4;
            this.label10.Text = "MB简称";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(345, 3);
            this.label11.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(72, 16);
            this.label11.TabIndex = 4;
            this.label11.Text = "订单数量";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(7, 144);
            this.label12.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(72, 16);
            this.label12.TabIndex = 4;
            this.label12.Text = "客户料号";
            // 
            // productTextBox
            // 
            this.productTextBox.BackColor = System.Drawing.Color.LightSalmon;
            this.productTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.productTextBox.Location = new System.Drawing.Point(176, 54);
            this.productTextBox.Margin = new System.Windows.Forms.Padding(4);
            this.productTextBox.Name = "productTextBox";
            this.productTextBox.Size = new System.Drawing.Size(158, 26);
            this.productTextBox.TabIndex = 5;
            // 
            // ordernoTextBox
            // 
            this.ordernoTextBox.BackColor = System.Drawing.Color.LightSalmon;
            this.ordernoTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ordernoTextBox.Location = new System.Drawing.Point(176, 101);
            this.ordernoTextBox.Margin = new System.Windows.Forms.Padding(4);
            this.ordernoTextBox.Name = "ordernoTextBox";
            this.ordernoTextBox.Size = new System.Drawing.Size(158, 26);
            this.ordernoTextBox.TabIndex = 5;
            // 
            // custom_materialNoTextBox
            // 
            this.custom_materialNoTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.custom_materialNoTextBox.Location = new System.Drawing.Point(176, 148);
            this.custom_materialNoTextBox.Margin = new System.Windows.Forms.Padding(4);
            this.custom_materialNoTextBox.Name = "custom_materialNoTextBox";
            this.custom_materialNoTextBox.Size = new System.Drawing.Size(158, 26);
            this.custom_materialNoTextBox.TabIndex = 5;
            // 
            // custom_material_describeTextBox
            // 
            this.custom_material_describeTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.custom_material_describeTextBox.Location = new System.Drawing.Point(176, 195);
            this.custom_material_describeTextBox.Margin = new System.Windows.Forms.Padding(4);
            this.custom_material_describeTextBox.Name = "custom_material_describeTextBox";
            this.custom_material_describeTextBox.Size = new System.Drawing.Size(158, 26);
            this.custom_material_describeTextBox.TabIndex = 5;
            // 
            // ordernumTextBox
            // 
            this.ordernumTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ordernumTextBox.Location = new System.Drawing.Point(514, 7);
            this.ordernumTextBox.Margin = new System.Windows.Forms.Padding(4);
            this.ordernumTextBox.Name = "ordernumTextBox";
            this.ordernumTextBox.Size = new System.Drawing.Size(158, 26);
            this.ordernumTextBox.TabIndex = 5;
            // 
            // mb_briefTextBox
            // 
            this.mb_briefTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mb_briefTextBox.Location = new System.Drawing.Point(514, 54);
            this.mb_briefTextBox.Margin = new System.Windows.Forms.Padding(4);
            this.mb_briefTextBox.Name = "mb_briefTextBox";
            this.mb_briefTextBox.Size = new System.Drawing.Size(158, 26);
            this.mb_briefTextBox.TabIndex = 5;
            // 
            // vendor_materialNoTextBox
            // 
            this.vendor_materialNoTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.vendor_materialNoTextBox.Location = new System.Drawing.Point(514, 101);
            this.vendor_materialNoTextBox.Margin = new System.Windows.Forms.Padding(4);
            this.vendor_materialNoTextBox.Name = "vendor_materialNoTextBox";
            this.vendor_materialNoTextBox.Size = new System.Drawing.Size(158, 26);
            this.vendor_materialNoTextBox.TabIndex = 5;
            // 
            // usernameTextBox
            // 
            this.usernameTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.usernameTextBox.Location = new System.Drawing.Point(514, 148);
            this.usernameTextBox.Margin = new System.Windows.Forms.Padding(4);
            this.usernameTextBox.Name = "usernameTextBox";
            this.usernameTextBox.Size = new System.Drawing.Size(158, 26);
            this.usernameTextBox.TabIndex = 5;
            // 
            // ordertimeTextBox
            // 
            this.ordertimeTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ordertimeTextBox.Location = new System.Drawing.Point(514, 195);
            this.ordertimeTextBox.Margin = new System.Windows.Forms.Padding(4);
            this.ordertimeTextBox.Name = "ordertimeTextBox";
            this.ordertimeTextBox.Size = new System.Drawing.Size(158, 26);
            this.ordertimeTextBox.TabIndex = 5;
            // 
            // receivedNumTextBox
            // 
            this.receivedNumTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.receivedNumTextBox.Location = new System.Drawing.Point(852, 7);
            this.receivedNumTextBox.Margin = new System.Windows.Forms.Padding(4);
            this.receivedNumTextBox.Name = "receivedNumTextBox";
            this.receivedNumTextBox.Size = new System.Drawing.Size(158, 26);
            this.receivedNumTextBox.TabIndex = 5;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(683, 50);
            this.label8.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(72, 16);
            this.label8.TabIndex = 4;
            this.label8.Text = "收货日期";
            // 
            // receivedateTextBox
            // 
            this.receivedateTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.receivedateTextBox.Location = new System.Drawing.Point(852, 54);
            this.receivedateTextBox.Margin = new System.Windows.Forms.Padding(4);
            this.receivedateTextBox.Name = "receivedateTextBox";
            this.receivedateTextBox.Size = new System.Drawing.Size(158, 26);
            this.receivedateTextBox.TabIndex = 5;
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(683, 97);
            this.label13.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(72, 16);
            this.label13.TabIndex = 4;
            this.label13.Text = "订单状态";
            // 
            // statusTextBox
            // 
            this.statusTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.statusTextBox.Location = new System.Drawing.Point(852, 101);
            this.statusTextBox.Margin = new System.Windows.Forms.Padding(4);
            this.statusTextBox.Name = "statusTextBox";
            this.statusTextBox.Size = new System.Drawing.Size(158, 26);
            this.statusTextBox.TabIndex = 5;
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(128)))));
            this.dataGridView1.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dataGridView1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView1.Location = new System.Drawing.Point(4, 314);
            this.dataGridView1.Margin = new System.Windows.Forms.Padding(4);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.RowTemplate.Height = 23;
            this.dataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView1.Size = new System.Drawing.Size(1362, 431);
            this.dataGridView1.TabIndex = 6;
            this.dataGridView1.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellClick);
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(1021, 191);
            this.label14.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(24, 16);
            this.label14.TabIndex = 4;
            this.label14.Text = "ID";
            // 
            // idTextBox
            // 
            this.idTextBox.Enabled = false;
            this.idTextBox.Location = new System.Drawing.Point(1190, 195);
            this.idTextBox.Margin = new System.Windows.Forms.Padding(4);
            this.idTextBox.Name = "idTextBox";
            this.idTextBox.ReadOnly = true;
            this.idTextBox.Size = new System.Drawing.Size(165, 26);
            this.idTextBox.TabIndex = 5;
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(683, 144);
            this.label15.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(56, 16);
            this.label15.TabIndex = 4;
            this.label15.Text = "仓库别";
            // 
            // storeHouseComboBox
            // 
            this.storeHouseComboBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.storeHouseComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.storeHouseComboBox.FormattingEnabled = true;
            this.storeHouseComboBox.Location = new System.Drawing.Point(852, 148);
            this.storeHouseComboBox.Margin = new System.Windows.Forms.Padding(4);
            this.storeHouseComboBox.Name = "storeHouseComboBox";
            this.storeHouseComboBox.Size = new System.Drawing.Size(158, 24);
            this.storeHouseComboBox.TabIndex = 7;
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(683, 191);
            this.label16.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(72, 16);
            this.label16.TabIndex = 4;
            this.label16.Text = "还货数量";
            // 
            // returnNumTextBox
            // 
            this.returnNumTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.returnNumTextBox.Enabled = false;
            this.returnNumTextBox.Location = new System.Drawing.Point(852, 195);
            this.returnNumTextBox.Margin = new System.Windows.Forms.Padding(4);
            this.returnNumTextBox.Name = "returnNumTextBox";
            this.returnNumTextBox.ReadOnly = true;
            this.returnNumTextBox.Size = new System.Drawing.Size(158, 26);
            this.returnNumTextBox.TabIndex = 5;
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
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 8.220604F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 58.48075F));
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
            this.tableLayoutPanel3.Controls.Add(this.label1, 0, 0);
            this.tableLayoutPanel3.Controls.Add(this.vendorTextBox, 1, 0);
            this.tableLayoutPanel3.Controls.Add(this.returnNumTextBox, 5, 4);
            this.tableLayoutPanel3.Controls.Add(this.storeHouseComboBox, 5, 3);
            this.tableLayoutPanel3.Controls.Add(this.label2, 0, 1);
            this.tableLayoutPanel3.Controls.Add(this.label16, 4, 4);
            this.tableLayoutPanel3.Controls.Add(this.ordertimeTextBox, 3, 4);
            this.tableLayoutPanel3.Controls.Add(this.custom_material_describeTextBox, 1, 4);
            this.tableLayoutPanel3.Controls.Add(this.statusTextBox, 5, 2);
            this.tableLayoutPanel3.Controls.Add(this.label15, 4, 3);
            this.tableLayoutPanel3.Controls.Add(this.usernameTextBox, 3, 3);
            this.tableLayoutPanel3.Controls.Add(this.receivedateTextBox, 5, 1);
            this.tableLayoutPanel3.Controls.Add(this.productTextBox, 1, 1);
            this.tableLayoutPanel3.Controls.Add(this.receivedNumTextBox, 5, 0);
            this.tableLayoutPanel3.Controls.Add(this.label13, 4, 2);
            this.tableLayoutPanel3.Controls.Add(this.vendor_materialNoTextBox, 3, 2);
            this.tableLayoutPanel3.Controls.Add(this.custom_materialNoTextBox, 1, 3);
            this.tableLayoutPanel3.Controls.Add(this.mb_briefTextBox, 3, 1);
            this.tableLayoutPanel3.Controls.Add(this.label8, 4, 1);
            this.tableLayoutPanel3.Controls.Add(this.label3, 0, 2);
            this.tableLayoutPanel3.Controls.Add(this.ordernoTextBox, 1, 2);
            this.tableLayoutPanel3.Controls.Add(this.label12, 0, 3);
            this.tableLayoutPanel3.Controls.Add(this.label7, 4, 0);
            this.tableLayoutPanel3.Controls.Add(this.label6, 0, 4);
            this.tableLayoutPanel3.Controls.Add(this.label11, 2, 0);
            this.tableLayoutPanel3.Controls.Add(this.ordernumTextBox, 3, 0);
            this.tableLayoutPanel3.Controls.Add(this.label4, 2, 4);
            this.tableLayoutPanel3.Controls.Add(this.label10, 2, 1);
            this.tableLayoutPanel3.Controls.Add(this.label9, 2, 2);
            this.tableLayoutPanel3.Controls.Add(this.label5, 2, 3);
            this.tableLayoutPanel3.Controls.Add(this.label17, 6, 0);
            this.tableLayoutPanel3.Controls.Add(this.label18, 6, 1);
            this.tableLayoutPanel3.Controls.Add(this.label19, 6, 2);
            this.tableLayoutPanel3.Controls.Add(this.declare_unittextBox, 7, 0);
            this.tableLayoutPanel3.Controls.Add(this.declare_numbertextBox, 7, 1);
            this.tableLayoutPanel3.Controls.Add(this.custom_request_numbertextBox, 7, 2);
            this.tableLayoutPanel3.Controls.Add(this.label14, 6, 4);
            this.tableLayoutPanel3.Controls.Add(this.idTextBox, 7, 4);
            this.tableLayoutPanel3.Controls.Add(this.label20, 6, 3);
            this.tableLayoutPanel3.Controls.Add(this.cidNumberTextBox, 7, 3);
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
            this.tableLayoutPanel3.Size = new System.Drawing.Size(1362, 241);
            this.tableLayoutPanel3.TabIndex = 9;
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Location = new System.Drawing.Point(1021, 3);
            this.label17.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(72, 16);
            this.label17.TabIndex = 4;
            this.label17.Text = "申报单位";
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Location = new System.Drawing.Point(1021, 50);
            this.label18.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(72, 16);
            this.label18.TabIndex = 4;
            this.label18.Text = "报关单号";
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.Location = new System.Drawing.Point(1021, 97);
            this.label19.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(72, 16);
            this.label19.TabIndex = 4;
            this.label19.Text = "申请单号";
            // 
            // declare_unittextBox
            // 
            this.declare_unittextBox.Enabled = false;
            this.declare_unittextBox.Location = new System.Drawing.Point(1190, 7);
            this.declare_unittextBox.Margin = new System.Windows.Forms.Padding(4);
            this.declare_unittextBox.Name = "declare_unittextBox";
            this.declare_unittextBox.ReadOnly = true;
            this.declare_unittextBox.Size = new System.Drawing.Size(165, 26);
            this.declare_unittextBox.TabIndex = 5;
            // 
            // declare_numbertextBox
            // 
            this.declare_numbertextBox.Location = new System.Drawing.Point(1190, 54);
            this.declare_numbertextBox.Margin = new System.Windows.Forms.Padding(4);
            this.declare_numbertextBox.Name = "declare_numbertextBox";
            this.declare_numbertextBox.Size = new System.Drawing.Size(165, 26);
            this.declare_numbertextBox.TabIndex = 5;
            // 
            // custom_request_numbertextBox
            // 
            this.custom_request_numbertextBox.Location = new System.Drawing.Point(1190, 101);
            this.custom_request_numbertextBox.Margin = new System.Windows.Forms.Padding(4);
            this.custom_request_numbertextBox.Name = "custom_request_numbertextBox";
            this.custom_request_numbertextBox.Size = new System.Drawing.Size(165, 26);
            this.custom_request_numbertextBox.TabIndex = 5;
            // 
            // label20
            // 
            this.label20.AutoSize = true;
            this.label20.Location = new System.Drawing.Point(1021, 144);
            this.label20.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(64, 16);
            this.label20.TabIndex = 4;
            this.label20.Text = "Cid数量";
            // 
            // cidNumberTextBox
            // 
            this.cidNumberTextBox.Enabled = false;
            this.cidNumberTextBox.Location = new System.Drawing.Point(1190, 148);
            this.cidNumberTextBox.Margin = new System.Windows.Forms.Padding(4);
            this.cidNumberTextBox.Name = "cidNumberTextBox";
            this.cidNumberTextBox.ReadOnly = true;
            this.cidNumberTextBox.Size = new System.Drawing.Size(165, 26);
            this.cidNumberTextBox.TabIndex = 5;
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
            this.tableLayoutPanel2.Location = new System.Drawing.Point(4, 253);
            this.tableLayoutPanel2.Margin = new System.Windows.Forms.Padding(4);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 1;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(1362, 53);
            this.tableLayoutPanel2.TabIndex = 9;
            // 
            // ReceiveOrderForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ClientSize = new System.Drawing.Size(1370, 749);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Font = new System.Drawing.Font("宋体", 12F);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "ReceiveOrderForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "收还货";
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
        private System.Windows.Forms.TextBox vendorTextBox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.TextBox productTextBox;
        private System.Windows.Forms.TextBox ordernoTextBox;
        private System.Windows.Forms.TextBox custom_materialNoTextBox;
        private System.Windows.Forms.TextBox custom_material_describeTextBox;
        private System.Windows.Forms.TextBox ordernumTextBox;
        private System.Windows.Forms.TextBox mb_briefTextBox;
        private System.Windows.Forms.TextBox vendor_materialNoTextBox;
        private System.Windows.Forms.TextBox usernameTextBox;
        private System.Windows.Forms.TextBox ordertimeTextBox;
        private System.Windows.Forms.TextBox receivedNumTextBox;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox receivedateTextBox;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.TextBox statusTextBox;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.TextBox idTextBox;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.ComboBox storeHouseComboBox;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.TextBox returnNumTextBox;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel3;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.TextBox declare_unittextBox;
        private System.Windows.Forms.TextBox declare_numbertextBox;
        private System.Windows.Forms.TextBox custom_request_numbertextBox;
        private System.Windows.Forms.Label label20;
        private System.Windows.Forms.TextBox cidNumberTextBox;
    }
}