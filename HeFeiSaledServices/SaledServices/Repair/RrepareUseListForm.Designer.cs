namespace SaledServices.Repair
{
    partial class RrepareUseListForm
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.choosebutton = new System.Windows.Forms.Button();
            this.refreshbutton = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.mb_brieftextBox = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.material_mpntextBox = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.realNumbertextBox = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.thisNumbertextBox = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.notgood_placetextBox = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.usedNumbertextBox = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.idTextBox = new System.Windows.Forms.TextBox();
            this.returnMaterialbutton = new System.Windows.Forms.Button();
            this.label8 = new System.Windows.Forms.Label();
            this.materialdescribetextBox = new System.Windows.Forms.TextBox();
            this.requestQuerybutton = new System.Windows.Forms.Button();
            this.havedbutton = new System.Windows.Forms.Button();
            this.statusbutton = new System.Windows.Forms.Button();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.tableLayoutPanel1.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.tableLayoutPanel3.SuspendLayout();
            this.SuspendLayout();
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            dataGridViewCellStyle3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(128)))));
            this.dataGridView1.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle3;
            this.dataGridView1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.dataGridView1.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.dataGridView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView1.Location = new System.Drawing.Point(5, 111);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.RowTemplate.Height = 23;
            this.dataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView1.Size = new System.Drawing.Size(674, 424);
            this.dataGridView1.TabIndex = 0;
            this.dataGridView1.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellClick);
            // 
            // choosebutton
            // 
            this.choosebutton.Dock = System.Windows.Forms.DockStyle.Fill;
            this.choosebutton.Enabled = false;
            this.choosebutton.Location = new System.Drawing.Point(5, 215);
            this.choosebutton.Name = "choosebutton";
            this.choosebutton.Size = new System.Drawing.Size(100, 62);
            this.choosebutton.TabIndex = 1;
            this.choosebutton.Text = "使用";
            this.choosebutton.UseVisualStyleBackColor = true;
            this.choosebutton.Click += new System.EventHandler(this.choosebutton_Click);
            // 
            // refreshbutton
            // 
            this.refreshbutton.Dock = System.Windows.Forms.DockStyle.Fill;
            this.refreshbutton.Location = new System.Drawing.Point(5, 285);
            this.refreshbutton.Name = "refreshbutton";
            this.refreshbutton.Size = new System.Drawing.Size(100, 62);
            this.refreshbutton.TabIndex = 1;
            this.refreshbutton.Text = "刷新";
            this.refreshbutton.UseVisualStyleBackColor = true;
            this.refreshbutton.Click += new System.EventHandler(this.refreshbutton_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(5, 2);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(29, 12);
            this.label1.TabIndex = 2;
            this.label1.Text = "机型";
            // 
            // mb_brieftextBox
            // 
            this.mb_brieftextBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mb_brieftextBox.Location = new System.Drawing.Point(97, 5);
            this.mb_brieftextBox.Name = "mb_brieftextBox";
            this.mb_brieftextBox.Size = new System.Drawing.Size(122, 21);
            this.mb_brieftextBox.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(227, 2);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(47, 12);
            this.label2.TabIndex = 2;
            this.label2.Text = "物料MPN";
            // 
            // material_mpntextBox
            // 
            this.material_mpntextBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.material_mpntextBox.Location = new System.Drawing.Point(339, 5);
            this.material_mpntextBox.Name = "material_mpntextBox";
            this.material_mpntextBox.Size = new System.Drawing.Size(104, 21);
            this.material_mpntextBox.TabIndex = 3;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(5, 34);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(53, 12);
            this.label3.TabIndex = 2;
            this.label3.Text = "已有数量";
            // 
            // realNumbertextBox
            // 
            this.realNumbertextBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.realNumbertextBox.Location = new System.Drawing.Point(97, 37);
            this.realNumbertextBox.Name = "realNumbertextBox";
            this.realNumbertextBox.ReadOnly = true;
            this.realNumbertextBox.Size = new System.Drawing.Size(122, 21);
            this.realNumbertextBox.TabIndex = 3;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(227, 66);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(77, 12);
            this.label4.TabIndex = 2;
            this.label4.Text = "本次使用数量";
            // 
            // thisNumbertextBox
            // 
            this.thisNumbertextBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.thisNumbertextBox.Location = new System.Drawing.Point(339, 69);
            this.thisNumbertextBox.Name = "thisNumbertextBox";
            this.thisNumbertextBox.Size = new System.Drawing.Size(104, 21);
            this.thisNumbertextBox.TabIndex = 3;
            this.thisNumbertextBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.thisNumbertextBox_KeyPress);
            this.thisNumbertextBox.Leave += new System.EventHandler(this.thisNumbertextBox_Leave);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(5, 66);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(53, 12);
            this.label5.TabIndex = 2;
            this.label5.Text = "不良位置";
            // 
            // notgood_placetextBox
            // 
            this.notgood_placetextBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.notgood_placetextBox.Location = new System.Drawing.Point(97, 69);
            this.notgood_placetextBox.Name = "notgood_placetextBox";
            this.notgood_placetextBox.ReadOnly = true;
            this.notgood_placetextBox.Size = new System.Drawing.Size(122, 21);
            this.notgood_placetextBox.TabIndex = 3;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(227, 34);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(77, 12);
            this.label6.TabIndex = 2;
            this.label6.Text = "使用过的数量";
            // 
            // usedNumbertextBox
            // 
            this.usedNumbertextBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.usedNumbertextBox.Location = new System.Drawing.Point(339, 37);
            this.usedNumbertextBox.Name = "usedNumbertextBox";
            this.usedNumbertextBox.ReadOnly = true;
            this.usedNumbertextBox.Size = new System.Drawing.Size(104, 21);
            this.usedNumbertextBox.TabIndex = 3;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(451, 66);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(17, 12);
            this.label7.TabIndex = 2;
            this.label7.Text = "Id";
            // 
            // idTextBox
            // 
            this.idTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.idTextBox.Location = new System.Drawing.Point(563, 69);
            this.idTextBox.Name = "idTextBox";
            this.idTextBox.ReadOnly = true;
            this.idTextBox.Size = new System.Drawing.Size(106, 21);
            this.idTextBox.TabIndex = 3;
            // 
            // returnMaterialbutton
            // 
            this.returnMaterialbutton.Dock = System.Windows.Forms.DockStyle.Fill;
            this.returnMaterialbutton.Location = new System.Drawing.Point(5, 355);
            this.returnMaterialbutton.Name = "returnMaterialbutton";
            this.returnMaterialbutton.Size = new System.Drawing.Size(100, 64);
            this.returnMaterialbutton.TabIndex = 1;
            this.returnMaterialbutton.Text = "归还到库存";
            this.returnMaterialbutton.UseVisualStyleBackColor = true;
            this.returnMaterialbutton.Click += new System.EventHandler(this.returnMaterialbutton_Click);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(451, 2);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(53, 12);
            this.label8.TabIndex = 2;
            this.label8.Text = "物料描述";
            // 
            // materialdescribetextBox
            // 
            this.materialdescribetextBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.materialdescribetextBox.Location = new System.Drawing.Point(563, 5);
            this.materialdescribetextBox.Name = "materialdescribetextBox";
            this.materialdescribetextBox.ReadOnly = true;
            this.materialdescribetextBox.Size = new System.Drawing.Size(106, 21);
            this.materialdescribetextBox.TabIndex = 3;
            // 
            // requestQuerybutton
            // 
            this.requestQuerybutton.Dock = System.Windows.Forms.DockStyle.Fill;
            this.requestQuerybutton.Location = new System.Drawing.Point(5, 5);
            this.requestQuerybutton.Name = "requestQuerybutton";
            this.requestQuerybutton.Size = new System.Drawing.Size(100, 62);
            this.requestQuerybutton.TabIndex = 1;
            this.requestQuerybutton.Text = "申请查询";
            this.requestQuerybutton.UseVisualStyleBackColor = true;
            this.requestQuerybutton.Click += new System.EventHandler(this.requestQuerybutton_Click);
            // 
            // havedbutton
            // 
            this.havedbutton.Dock = System.Windows.Forms.DockStyle.Fill;
            this.havedbutton.Location = new System.Drawing.Point(5, 145);
            this.havedbutton.Name = "havedbutton";
            this.havedbutton.Size = new System.Drawing.Size(100, 62);
            this.havedbutton.TabIndex = 1;
            this.havedbutton.Text = "已有材料查询";
            this.havedbutton.UseVisualStyleBackColor = true;
            this.havedbutton.Click += new System.EventHandler(this.havedbutton_Click);
            // 
            // statusbutton
            // 
            this.statusbutton.Dock = System.Windows.Forms.DockStyle.Fill;
            this.statusbutton.Location = new System.Drawing.Point(5, 75);
            this.statusbutton.Name = "statusbutton";
            this.statusbutton.Size = new System.Drawing.Size(100, 62);
            this.statusbutton.TabIndex = 1;
            this.statusbutton.Text = "状态查询";
            this.statusbutton.UseVisualStyleBackColor = true;
            this.statusbutton.Click += new System.EventHandler(this.statusbutton_Click);
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.Inset;
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 85.50725F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 14.49275F));
            this.tableLayoutPanel1.Controls.Add(this.dataGridView1, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel2, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel3, 0, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 19.52F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 80.48F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(802, 540);
            this.tableLayoutPanel1.TabIndex = 4;
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.Inset;
            this.tableLayoutPanel2.ColumnCount = 1;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.Controls.Add(this.requestQuerybutton, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.statusbutton, 0, 1);
            this.tableLayoutPanel2.Controls.Add(this.havedbutton, 0, 2);
            this.tableLayoutPanel2.Controls.Add(this.choosebutton, 0, 3);
            this.tableLayoutPanel2.Controls.Add(this.refreshbutton, 0, 4);
            this.tableLayoutPanel2.Controls.Add(this.returnMaterialbutton, 0, 5);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(687, 111);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 6;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(110, 424);
            this.tableLayoutPanel2.TabIndex = 1;
            // 
            // tableLayoutPanel3
            // 
            this.tableLayoutPanel3.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.Inset;
            this.tableLayoutPanel3.ColumnCount = 6;
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 13.66623F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 19.4481F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
            this.tableLayoutPanel3.Controls.Add(this.label1, 0, 0);
            this.tableLayoutPanel3.Controls.Add(this.idTextBox, 5, 2);
            this.tableLayoutPanel3.Controls.Add(this.thisNumbertextBox, 3, 2);
            this.tableLayoutPanel3.Controls.Add(this.label7, 4, 2);
            this.tableLayoutPanel3.Controls.Add(this.mb_brieftextBox, 1, 0);
            this.tableLayoutPanel3.Controls.Add(this.label4, 2, 2);
            this.tableLayoutPanel3.Controls.Add(this.label2, 2, 0);
            this.tableLayoutPanel3.Controls.Add(this.notgood_placetextBox, 1, 2);
            this.tableLayoutPanel3.Controls.Add(this.material_mpntextBox, 3, 0);
            this.tableLayoutPanel3.Controls.Add(this.label5, 0, 2);
            this.tableLayoutPanel3.Controls.Add(this.label8, 4, 0);
            this.tableLayoutPanel3.Controls.Add(this.materialdescribetextBox, 5, 0);
            this.tableLayoutPanel3.Controls.Add(this.label3, 0, 1);
            this.tableLayoutPanel3.Controls.Add(this.usedNumbertextBox, 3, 1);
            this.tableLayoutPanel3.Controls.Add(this.realNumbertextBox, 1, 1);
            this.tableLayoutPanel3.Controls.Add(this.label6, 2, 1);
            this.tableLayoutPanel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel3.Location = new System.Drawing.Point(5, 5);
            this.tableLayoutPanel3.Name = "tableLayoutPanel3";
            this.tableLayoutPanel3.RowCount = 3;
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel3.Size = new System.Drawing.Size(674, 98);
            this.tableLayoutPanel3.TabIndex = 2;
            // 
            // RrepareUseListForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(802, 540);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "RrepareUseListForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "预领料列表";
            this.Load += new System.EventHandler(this.RrepareUseListForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel3.ResumeLayout(false);
            this.tableLayoutPanel3.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Button choosebutton;
        private System.Windows.Forms.Button refreshbutton;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox mb_brieftextBox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox material_mpntextBox;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox realNumbertextBox;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox thisNumbertextBox;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox notgood_placetextBox;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox usedNumbertextBox;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox idTextBox;
        private System.Windows.Forms.Button returnMaterialbutton;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox materialdescribetextBox;
        private System.Windows.Forms.Button requestQuerybutton;
        private System.Windows.Forms.Button havedbutton;
        private System.Windows.Forms.Button statusbutton;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel3;
    }
}