namespace SaledServices
{
    partial class UserDetailForm
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
            this.idTextBox = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.userNameTextBox = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.passwordTextBox = new System.Windows.Forms.TextBox();
            this.add = new System.Windows.Forms.Button();
            this.query = new System.Windows.Forms.Button();
            this.modify = new System.Windows.Forms.Button();
            this.delete = new System.Windows.Forms.Button();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.super_checkBox = new System.Windows.Forms.CheckBox();
            this.permissionPanel = new System.Windows.Forms.Panel();
            this.storeCheckBox = new System.Windows.Forms.CheckBox();
            this.obecheckBox = new System.Windows.Forms.CheckBox();
            this.runningcheckBox = new System.Windows.Forms.CheckBox();
            this.outlookCheckBox = new System.Windows.Forms.CheckBox();
            this.receive_returnCheckBox = new System.Windows.Forms.CheckBox();
            this.test2CheckBox = new System.Windows.Forms.CheckBox();
            this.test1CheckBox = new System.Windows.Forms.CheckBox();
            this.test_allCheckBox = new System.Windows.Forms.CheckBox();
            this.repairCheckBox = new System.Windows.Forms.CheckBox();
            this.bgaCheckBox = new System.Windows.Forms.CheckBox();
            this.label4 = new System.Windows.Forms.Label();
            this.workIdTextBox = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.permissionPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(60, 14);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(29, 20);
            this.label1.TabIndex = 0;
            this.label1.Text = "ID";
            // 
            // idTextBox
            // 
            this.idTextBox.Location = new System.Drawing.Point(185, 11);
            this.idTextBox.Name = "idTextBox";
            this.idTextBox.ReadOnly = true;
            this.idTextBox.Size = new System.Drawing.Size(100, 30);
            this.idTextBox.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(51, 78);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(69, 20);
            this.label2.TabIndex = 2;
            this.label2.Text = "用户名";
            // 
            // userNameTextBox
            // 
            this.userNameTextBox.Location = new System.Drawing.Point(185, 68);
            this.userNameTextBox.Name = "userNameTextBox";
            this.userNameTextBox.Size = new System.Drawing.Size(145, 30);
            this.userNameTextBox.TabIndex = 3;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(51, 154);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(49, 20);
            this.label3.TabIndex = 2;
            this.label3.Text = "密码";
            // 
            // passwordTextBox
            // 
            this.passwordTextBox.Location = new System.Drawing.Point(185, 144);
            this.passwordTextBox.Name = "passwordTextBox";
            this.passwordTextBox.Size = new System.Drawing.Size(145, 30);
            this.passwordTextBox.TabIndex = 3;
            // 
            // add
            // 
            this.add.Location = new System.Drawing.Point(41, 264);
            this.add.Name = "add";
            this.add.Size = new System.Drawing.Size(118, 63);
            this.add.TabIndex = 4;
            this.add.Text = "添加用户";
            this.add.UseVisualStyleBackColor = true;
            this.add.Click += new System.EventHandler(this.add_Click);
            // 
            // query
            // 
            this.query.Location = new System.Drawing.Point(212, 264);
            this.query.Name = "query";
            this.query.Size = new System.Drawing.Size(118, 63);
            this.query.TabIndex = 4;
            this.query.Text = "查询用户";
            this.query.UseVisualStyleBackColor = true;
            this.query.Click += new System.EventHandler(this.query_Click);
            // 
            // modify
            // 
            this.modify.Location = new System.Drawing.Point(367, 264);
            this.modify.Name = "modify";
            this.modify.Size = new System.Drawing.Size(118, 63);
            this.modify.TabIndex = 4;
            this.modify.Text = "修改用户";
            this.modify.UseVisualStyleBackColor = true;
            this.modify.Click += new System.EventHandler(this.modify_Click);
            // 
            // delete
            // 
            this.delete.Location = new System.Drawing.Point(522, 264);
            this.delete.Name = "delete";
            this.delete.Size = new System.Drawing.Size(118, 63);
            this.delete.TabIndex = 4;
            this.delete.Text = "删除用户";
            this.delete.UseVisualStyleBackColor = true;
            this.delete.Click += new System.EventHandler(this.delete_Click);
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(41, 350);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.RowTemplate.Height = 23;
            this.dataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView1.Size = new System.Drawing.Size(599, 277);
            this.dataGridView1.TabIndex = 5;
            this.dataGridView1.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellClick);
            // 
            // super_checkBox
            // 
            this.super_checkBox.AutoSize = true;
            this.super_checkBox.Location = new System.Drawing.Point(16, 15);
            this.super_checkBox.Name = "super_checkBox";
            this.super_checkBox.Size = new System.Drawing.Size(128, 24);
            this.super_checkBox.TabIndex = 6;
            this.super_checkBox.Text = "超级管理员";
            this.super_checkBox.UseVisualStyleBackColor = true;
            // 
            // permissionPanel
            // 
            this.permissionPanel.Controls.Add(this.storeCheckBox);
            this.permissionPanel.Controls.Add(this.obecheckBox);
            this.permissionPanel.Controls.Add(this.runningcheckBox);
            this.permissionPanel.Controls.Add(this.outlookCheckBox);
            this.permissionPanel.Controls.Add(this.receive_returnCheckBox);
            this.permissionPanel.Controls.Add(this.test2CheckBox);
            this.permissionPanel.Controls.Add(this.test1CheckBox);
            this.permissionPanel.Controls.Add(this.test_allCheckBox);
            this.permissionPanel.Controls.Add(this.repairCheckBox);
            this.permissionPanel.Controls.Add(this.bgaCheckBox);
            this.permissionPanel.Controls.Add(this.super_checkBox);
            this.permissionPanel.Location = new System.Drawing.Point(367, 11);
            this.permissionPanel.Name = "permissionPanel";
            this.permissionPanel.Size = new System.Drawing.Size(292, 247);
            this.permissionPanel.TabIndex = 7;
            // 
            // storeCheckBox
            // 
            this.storeCheckBox.AutoSize = true;
            this.storeCheckBox.Location = new System.Drawing.Point(16, 212);
            this.storeCheckBox.Name = "storeCheckBox";
            this.storeCheckBox.Size = new System.Drawing.Size(68, 24);
            this.storeCheckBox.TabIndex = 6;
            this.storeCheckBox.Text = "库存";
            this.storeCheckBox.UseVisualStyleBackColor = true;
            // 
            // obecheckBox
            // 
            this.obecheckBox.AutoSize = true;
            this.obecheckBox.Location = new System.Drawing.Point(215, 182);
            this.obecheckBox.Name = "obecheckBox";
            this.obecheckBox.Size = new System.Drawing.Size(58, 24);
            this.obecheckBox.TabIndex = 6;
            this.obecheckBox.Text = "OBE";
            this.obecheckBox.UseVisualStyleBackColor = true;
            // 
            // runningcheckBox
            // 
            this.runningcheckBox.AutoSize = true;
            this.runningcheckBox.Location = new System.Drawing.Point(107, 182);
            this.runningcheckBox.Name = "runningcheckBox";
            this.runningcheckBox.Size = new System.Drawing.Size(98, 24);
            this.runningcheckBox.TabIndex = 6;
            this.runningcheckBox.Text = "Running";
            this.runningcheckBox.UseVisualStyleBackColor = true;
            // 
            // outlookCheckBox
            // 
            this.outlookCheckBox.AutoSize = true;
            this.outlookCheckBox.Location = new System.Drawing.Point(16, 182);
            this.outlookCheckBox.Name = "outlookCheckBox";
            this.outlookCheckBox.Size = new System.Drawing.Size(68, 24);
            this.outlookCheckBox.TabIndex = 6;
            this.outlookCheckBox.Text = "外观";
            this.outlookCheckBox.UseVisualStyleBackColor = true;
            // 
            // receive_returnCheckBox
            // 
            this.receive_returnCheckBox.AutoSize = true;
            this.receive_returnCheckBox.Location = new System.Drawing.Point(16, 150);
            this.receive_returnCheckBox.Name = "receive_returnCheckBox";
            this.receive_returnCheckBox.Size = new System.Drawing.Size(88, 24);
            this.receive_returnCheckBox.TabIndex = 6;
            this.receive_returnCheckBox.Text = "收还货";
            this.receive_returnCheckBox.UseVisualStyleBackColor = true;
            // 
            // test2CheckBox
            // 
            this.test2CheckBox.AutoSize = true;
            this.test2CheckBox.Location = new System.Drawing.Point(204, 110);
            this.test2CheckBox.Name = "test2CheckBox";
            this.test2CheckBox.Size = new System.Drawing.Size(78, 24);
            this.test2CheckBox.TabIndex = 6;
            this.test2CheckBox.Text = "测试2";
            this.test2CheckBox.UseVisualStyleBackColor = true;
            // 
            // test1CheckBox
            // 
            this.test1CheckBox.AutoSize = true;
            this.test1CheckBox.Location = new System.Drawing.Point(120, 110);
            this.test1CheckBox.Name = "test1CheckBox";
            this.test1CheckBox.Size = new System.Drawing.Size(78, 24);
            this.test1CheckBox.TabIndex = 6;
            this.test1CheckBox.Text = "测试1";
            this.test1CheckBox.UseVisualStyleBackColor = true;
            // 
            // test_allCheckBox
            // 
            this.test_allCheckBox.AutoSize = true;
            this.test_allCheckBox.Location = new System.Drawing.Point(16, 110);
            this.test_allCheckBox.Name = "test_allCheckBox";
            this.test_allCheckBox.Size = new System.Drawing.Size(98, 24);
            this.test_allCheckBox.TabIndex = 6;
            this.test_allCheckBox.Text = "测试1&&2";
            this.test_allCheckBox.UseVisualStyleBackColor = true;
            // 
            // repairCheckBox
            // 
            this.repairCheckBox.AutoSize = true;
            this.repairCheckBox.Location = new System.Drawing.Point(16, 80);
            this.repairCheckBox.Name = "repairCheckBox";
            this.repairCheckBox.Size = new System.Drawing.Size(68, 24);
            this.repairCheckBox.TabIndex = 6;
            this.repairCheckBox.Text = "维修";
            this.repairCheckBox.UseVisualStyleBackColor = true;
            // 
            // bgaCheckBox
            // 
            this.bgaCheckBox.AutoSize = true;
            this.bgaCheckBox.Location = new System.Drawing.Point(16, 45);
            this.bgaCheckBox.Name = "bgaCheckBox";
            this.bgaCheckBox.Size = new System.Drawing.Size(98, 24);
            this.bgaCheckBox.TabIndex = 6;
            this.bgaCheckBox.Text = "BGA维修";
            this.bgaCheckBox.UseVisualStyleBackColor = true;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(51, 118);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(49, 20);
            this.label4.TabIndex = 2;
            this.label4.Text = "工号";
            // 
            // workIdTextBox
            // 
            this.workIdTextBox.Location = new System.Drawing.Point(185, 108);
            this.workIdTextBox.Name = "workIdTextBox";
            this.workIdTextBox.Size = new System.Drawing.Size(145, 30);
            this.workIdTextBox.TabIndex = 3;
            // 
            // UserDetailForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(830, 693);
            this.Controls.Add(this.permissionPanel);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.delete);
            this.Controls.Add(this.modify);
            this.Controls.Add(this.query);
            this.Controls.Add(this.add);
            this.Controls.Add(this.passwordTextBox);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.workIdTextBox);
            this.Controls.Add(this.userNameTextBox);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.idTextBox);
            this.Controls.Add(this.label1);
            this.Font = new System.Drawing.Font("SimSun", 15F);
            this.Margin = new System.Windows.Forms.Padding(5);
            this.Name = "UserDetailForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "用户信息";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.UserDetailForm_FormClosed);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.permissionPanel.ResumeLayout(false);
            this.permissionPanel.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox idTextBox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox userNameTextBox;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox passwordTextBox;
        private System.Windows.Forms.Button add;
        private System.Windows.Forms.Button query;
        private System.Windows.Forms.Button modify;
        private System.Windows.Forms.Button delete;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.CheckBox super_checkBox;
        private System.Windows.Forms.Panel permissionPanel;
        private System.Windows.Forms.CheckBox test1CheckBox;
        private System.Windows.Forms.CheckBox test_allCheckBox;
        private System.Windows.Forms.CheckBox repairCheckBox;
        private System.Windows.Forms.CheckBox bgaCheckBox;
        private System.Windows.Forms.CheckBox receive_returnCheckBox;
        private System.Windows.Forms.CheckBox test2CheckBox;
        private System.Windows.Forms.CheckBox outlookCheckBox;
        private System.Windows.Forms.CheckBox storeCheckBox;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox workIdTextBox;
        private System.Windows.Forms.CheckBox obecheckBox;
        private System.Windows.Forms.CheckBox runningcheckBox;

    }
}