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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
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
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(128)))));
            this.dataGridView1.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dataGridView1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.dataGridView1.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(32, 155);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.RowTemplate.Height = 23;
            this.dataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView1.Size = new System.Drawing.Size(722, 365);
            this.dataGridView1.TabIndex = 0;
            this.dataGridView1.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellClick);
            // 
            // choosebutton
            // 
            this.choosebutton.Enabled = false;
            this.choosebutton.Location = new System.Drawing.Point(763, 377);
            this.choosebutton.Name = "choosebutton";
            this.choosebutton.Size = new System.Drawing.Size(75, 23);
            this.choosebutton.TabIndex = 1;
            this.choosebutton.Text = "使用";
            this.choosebutton.UseVisualStyleBackColor = true;
            this.choosebutton.Click += new System.EventHandler(this.choosebutton_Click);
            // 
            // refreshbutton
            // 
            this.refreshbutton.Location = new System.Drawing.Point(763, 406);
            this.refreshbutton.Name = "refreshbutton";
            this.refreshbutton.Size = new System.Drawing.Size(75, 23);
            this.refreshbutton.TabIndex = 1;
            this.refreshbutton.Text = "刷新";
            this.refreshbutton.UseVisualStyleBackColor = true;
            this.refreshbutton.Click += new System.EventHandler(this.refreshbutton_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(31, 24);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(29, 12);
            this.label1.TabIndex = 2;
            this.label1.Text = "机型";
            // 
            // mb_brieftextBox
            // 
            this.mb_brieftextBox.Location = new System.Drawing.Point(95, 21);
            this.mb_brieftextBox.Name = "mb_brieftextBox";
            this.mb_brieftextBox.Size = new System.Drawing.Size(100, 21);
            this.mb_brieftextBox.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(224, 24);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(47, 12);
            this.label2.TabIndex = 2;
            this.label2.Text = "物料MPN";
            // 
            // material_mpntextBox
            // 
            this.material_mpntextBox.Location = new System.Drawing.Point(307, 24);
            this.material_mpntextBox.Name = "material_mpntextBox";
            this.material_mpntextBox.Size = new System.Drawing.Size(100, 21);
            this.material_mpntextBox.TabIndex = 3;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(29, 72);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(53, 12);
            this.label3.TabIndex = 2;
            this.label3.Text = "已有数量";
            // 
            // realNumbertextBox
            // 
            this.realNumbertextBox.Location = new System.Drawing.Point(95, 66);
            this.realNumbertextBox.Name = "realNumbertextBox";
            this.realNumbertextBox.ReadOnly = true;
            this.realNumbertextBox.Size = new System.Drawing.Size(100, 21);
            this.realNumbertextBox.TabIndex = 3;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(224, 124);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(77, 12);
            this.label4.TabIndex = 2;
            this.label4.Text = "本次使用数量";
            // 
            // thisNumbertextBox
            // 
            this.thisNumbertextBox.Location = new System.Drawing.Point(307, 118);
            this.thisNumbertextBox.Name = "thisNumbertextBox";
            this.thisNumbertextBox.Size = new System.Drawing.Size(100, 21);
            this.thisNumbertextBox.TabIndex = 3;
            this.thisNumbertextBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.thisNumbertextBox_KeyPress);
            this.thisNumbertextBox.Leave += new System.EventHandler(this.thisNumbertextBox_Leave);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(30, 121);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(53, 12);
            this.label5.TabIndex = 2;
            this.label5.Text = "不良位置";
            // 
            // notgood_placetextBox
            // 
            this.notgood_placetextBox.Location = new System.Drawing.Point(95, 115);
            this.notgood_placetextBox.Name = "notgood_placetextBox";
            this.notgood_placetextBox.ReadOnly = true;
            this.notgood_placetextBox.Size = new System.Drawing.Size(100, 21);
            this.notgood_placetextBox.TabIndex = 3;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(224, 75);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(77, 12);
            this.label6.TabIndex = 2;
            this.label6.Text = "使用过的数量";
            // 
            // usedNumbertextBox
            // 
            this.usedNumbertextBox.Location = new System.Drawing.Point(307, 72);
            this.usedNumbertextBox.Name = "usedNumbertextBox";
            this.usedNumbertextBox.ReadOnly = true;
            this.usedNumbertextBox.Size = new System.Drawing.Size(100, 21);
            this.usedNumbertextBox.TabIndex = 3;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(484, 124);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(17, 12);
            this.label7.TabIndex = 2;
            this.label7.Text = "Id";
            // 
            // idTextBox
            // 
            this.idTextBox.Location = new System.Drawing.Point(531, 118);
            this.idTextBox.Name = "idTextBox";
            this.idTextBox.ReadOnly = true;
            this.idTextBox.Size = new System.Drawing.Size(100, 21);
            this.idTextBox.TabIndex = 3;
            // 
            // returnMaterialbutton
            // 
            this.returnMaterialbutton.Location = new System.Drawing.Point(763, 435);
            this.returnMaterialbutton.Name = "returnMaterialbutton";
            this.returnMaterialbutton.Size = new System.Drawing.Size(75, 23);
            this.returnMaterialbutton.TabIndex = 1;
            this.returnMaterialbutton.Text = "归还到库存";
            this.returnMaterialbutton.UseVisualStyleBackColor = true;
            this.returnMaterialbutton.Click += new System.EventHandler(this.returnMaterialbutton_Click);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(448, 24);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(53, 12);
            this.label8.TabIndex = 2;
            this.label8.Text = "物料描述";
            // 
            // materialdescribetextBox
            // 
            this.materialdescribetextBox.Location = new System.Drawing.Point(531, 21);
            this.materialdescribetextBox.Name = "materialdescribetextBox";
            this.materialdescribetextBox.ReadOnly = true;
            this.materialdescribetextBox.Size = new System.Drawing.Size(100, 21);
            this.materialdescribetextBox.TabIndex = 3;
            // 
            // requestQuerybutton
            // 
            this.requestQuerybutton.Location = new System.Drawing.Point(763, 155);
            this.requestQuerybutton.Name = "requestQuerybutton";
            this.requestQuerybutton.Size = new System.Drawing.Size(85, 56);
            this.requestQuerybutton.TabIndex = 1;
            this.requestQuerybutton.Text = "申请查询";
            this.requestQuerybutton.UseVisualStyleBackColor = true;
            this.requestQuerybutton.Click += new System.EventHandler(this.requestQuerybutton_Click);
            // 
            // havedbutton
            // 
            this.havedbutton.Location = new System.Drawing.Point(760, 301);
            this.havedbutton.Name = "havedbutton";
            this.havedbutton.Size = new System.Drawing.Size(102, 46);
            this.havedbutton.TabIndex = 1;
            this.havedbutton.Text = "已有材料查询";
            this.havedbutton.UseVisualStyleBackColor = true;
            this.havedbutton.Click += new System.EventHandler(this.havedbutton_Click);
            // 
            // statusbutton
            // 
            this.statusbutton.Location = new System.Drawing.Point(763, 230);
            this.statusbutton.Name = "statusbutton";
            this.statusbutton.Size = new System.Drawing.Size(85, 56);
            this.statusbutton.TabIndex = 1;
            this.statusbutton.Text = "状态查询";
            this.statusbutton.UseVisualStyleBackColor = true;
            this.statusbutton.Click += new System.EventHandler(this.statusbutton_Click);
            // 
            // RrepareUseListForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(891, 637);
            this.Controls.Add(this.thisNumbertextBox);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.notgood_placetextBox);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.idTextBox);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.materialdescribetextBox);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.usedNumbertextBox);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.realNumbertextBox);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.material_mpntextBox);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.mb_brieftextBox);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.refreshbutton);
            this.Controls.Add(this.returnMaterialbutton);
            this.Controls.Add(this.havedbutton);
            this.Controls.Add(this.statusbutton);
            this.Controls.Add(this.requestQuerybutton);
            this.Controls.Add(this.choosebutton);
            this.Controls.Add(this.dataGridView1);
            this.Name = "RrepareUseListForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "预领料列表";
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

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
    }
}