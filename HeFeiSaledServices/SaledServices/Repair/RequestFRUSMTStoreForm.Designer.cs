namespace SaledServices.Store
{
    partial class RequestFRUSMTStoreForm
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
            this.mb_brieftextBox = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.not_good_placeTextBox = new System.Windows.Forms.TextBox();
            this.requestbutton = new System.Windows.Forms.Button();
            this.cancel = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.numberTextBox = new System.Windows.Forms.TextBox();
            this.dateTextBox = new System.Windows.Forms.TextBox();
            this.requesterTextBox = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.checkRequestListbutton = new System.Windows.Forms.Button();
            this.dataGridView = new System.Windows.Forms.DataGridView();
            this.materialMpnTextBox = new System.Windows.Forms.TextBox();
            this.materialDescribetextBox = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(96, 72);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(77, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "机型(MB简称)";
            // 
            // mb_brieftextBox
            // 
            this.mb_brieftextBox.Location = new System.Drawing.Point(182, 69);
            this.mb_brieftextBox.Name = "mb_brieftextBox";
            this.mb_brieftextBox.Size = new System.Drawing.Size(100, 21);
            this.mb_brieftextBox.TabIndex = 6;
            this.mb_brieftextBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.mb_brieftextBox_KeyPress);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(96, 105);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(89, 12);
            this.label3.TabIndex = 0;
            this.label3.Text = "维修位置(回车)";
            // 
            // not_good_placeTextBox
            // 
            this.not_good_placeTextBox.Location = new System.Drawing.Point(182, 102);
            this.not_good_placeTextBox.Name = "not_good_placeTextBox";
            this.not_good_placeTextBox.Size = new System.Drawing.Size(100, 21);
            this.not_good_placeTextBox.TabIndex = 6;
            this.not_good_placeTextBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.not_good_placeTextBox_KeyPress);
            // 
            // requestbutton
            // 
            this.requestbutton.Location = new System.Drawing.Point(206, 314);
            this.requestbutton.Name = "requestbutton";
            this.requestbutton.Size = new System.Drawing.Size(76, 23);
            this.requestbutton.TabIndex = 8;
            this.requestbutton.Text = "发出请求";
            this.requestbutton.UseVisualStyleBackColor = true;
            this.requestbutton.Click += new System.EventHandler(this.requestbutton_Click);
            // 
            // cancel
            // 
            this.cancel.Location = new System.Drawing.Point(334, 314);
            this.cancel.Name = "cancel";
            this.cancel.Size = new System.Drawing.Size(76, 23);
            this.cancel.TabIndex = 8;
            this.cancel.Text = "取消";
            this.cancel.UseVisualStyleBackColor = true;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(96, 141);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(53, 12);
            this.label4.TabIndex = 0;
            this.label4.Text = "请求数量";
            // 
            // numberTextBox
            // 
            this.numberTextBox.Location = new System.Drawing.Point(182, 138);
            this.numberTextBox.Name = "numberTextBox";
            this.numberTextBox.Size = new System.Drawing.Size(100, 21);
            this.numberTextBox.TabIndex = 6;
            // 
            // dateTextBox
            // 
            this.dateTextBox.Location = new System.Drawing.Point(182, 215);
            this.dateTextBox.Name = "dateTextBox";
            this.dateTextBox.ReadOnly = true;
            this.dateTextBox.Size = new System.Drawing.Size(100, 21);
            this.dateTextBox.TabIndex = 17;
            // 
            // requesterTextBox
            // 
            this.requesterTextBox.Location = new System.Drawing.Point(182, 179);
            this.requesterTextBox.Name = "requesterTextBox";
            this.requesterTextBox.ReadOnly = true;
            this.requesterTextBox.Size = new System.Drawing.Size(100, 21);
            this.requesterTextBox.TabIndex = 18;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(96, 218);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(53, 12);
            this.label5.TabIndex = 15;
            this.label5.Text = "请求日期";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(96, 182);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(41, 12);
            this.label2.TabIndex = 16;
            this.label2.Text = "请求人";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(309, 40);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(47, 12);
            this.label6.TabIndex = 15;
            this.label6.Text = "材料MPN";
            // 
            // checkRequestListbutton
            // 
            this.checkRequestListbutton.Location = new System.Drawing.Point(463, 314);
            this.checkRequestListbutton.Name = "checkRequestListbutton";
            this.checkRequestListbutton.Size = new System.Drawing.Size(118, 23);
            this.checkRequestListbutton.TabIndex = 8;
            this.checkRequestListbutton.Text = "查看申请列表";
            this.checkRequestListbutton.UseVisualStyleBackColor = true;
            this.checkRequestListbutton.Click += new System.EventHandler(this.checkRequestListbutton_Click);
            // 
            // dataGridView
            // 
            this.dataGridView.AllowUserToAddRows = false;
            this.dataGridView.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.dataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView.Location = new System.Drawing.Point(299, 67);
            this.dataGridView.Margin = new System.Windows.Forms.Padding(4);
            this.dataGridView.Name = "dataGridView";
            this.dataGridView.ReadOnly = true;
            this.dataGridView.RowTemplate.Height = 23;
            this.dataGridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView.Size = new System.Drawing.Size(452, 223);
            this.dataGridView.TabIndex = 20;
            this.dataGridView.VirtualMode = true;
            this.dataGridView.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView_CellClick);
            // 
            // materialMpnTextBox
            // 
            this.materialMpnTextBox.Location = new System.Drawing.Point(362, 37);
            this.materialMpnTextBox.Name = "materialMpnTextBox";
            this.materialMpnTextBox.ReadOnly = true;
            this.materialMpnTextBox.Size = new System.Drawing.Size(116, 21);
            this.materialMpnTextBox.TabIndex = 6;
            // 
            // materialDescribetextBox
            // 
            this.materialDescribetextBox.Location = new System.Drawing.Point(543, 37);
            this.materialDescribetextBox.Name = "materialDescribetextBox";
            this.materialDescribetextBox.ReadOnly = true;
            this.materialDescribetextBox.Size = new System.Drawing.Size(208, 21);
            this.materialDescribetextBox.TabIndex = 6;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(484, 43);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(53, 12);
            this.label7.TabIndex = 15;
            this.label7.Text = "材料描述";
            // 
            // RequestFRUSMTStoreForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(784, 509);
            this.Controls.Add(this.dataGridView);
            this.Controls.Add(this.dateTextBox);
            this.Controls.Add(this.requesterTextBox);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.checkRequestListbutton);
            this.Controls.Add(this.cancel);
            this.Controls.Add(this.requestbutton);
            this.Controls.Add(this.numberTextBox);
            this.Controls.Add(this.not_good_placeTextBox);
            this.Controls.Add(this.materialDescribetextBox);
            this.Controls.Add(this.materialMpnTextBox);
            this.Controls.Add(this.mb_brieftextBox);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label1);
            this.Name = "RequestFRUSMTStoreForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "发送请求SMT";
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox mb_brieftextBox;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox not_good_placeTextBox;
        private System.Windows.Forms.Button requestbutton;
        private System.Windows.Forms.Button cancel;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox numberTextBox;
        private System.Windows.Forms.TextBox dateTextBox;
        private System.Windows.Forms.TextBox requesterTextBox;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button checkRequestListbutton;
        private System.Windows.Forms.DataGridView dataGridView;
        private System.Windows.Forms.TextBox materialMpnTextBox;
        private System.Windows.Forms.TextBox materialDescribetextBox;
        private System.Windows.Forms.Label label7;
    }
}