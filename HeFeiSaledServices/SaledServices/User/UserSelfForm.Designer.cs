namespace SaledServices.User
{
    partial class UserSelfForm
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
            this.usernameTextBox = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.oripasswordTextBox = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.newpassTextBox = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.confirmPassTextBox = new System.Windows.Forms.TextBox();
            this.modifyPassButton = new System.Windows.Forms.Button();
            this.permissionPanel = new System.Windows.Forms.Panel();
            this.obecheckBox = new System.Windows.Forms.CheckBox();
            this.storeCheckBox = new System.Windows.Forms.CheckBox();
            this.runningcheckBox = new System.Windows.Forms.CheckBox();
            this.outlookCheckBox = new System.Windows.Forms.CheckBox();
            this.receive_returnCheckBox = new System.Windows.Forms.CheckBox();
            this.test2CheckBox = new System.Windows.Forms.CheckBox();
            this.test1CheckBox = new System.Windows.Forms.CheckBox();
            this.test_allCheckBox = new System.Windows.Forms.CheckBox();
            this.repairCheckBox = new System.Windows.Forms.CheckBox();
            this.bgaCheckBox = new System.Windows.Forms.CheckBox();
            this.super_checkBox = new System.Windows.Forms.CheckBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label6 = new System.Windows.Forms.Label();
            this.workIdtextBox = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.permissionPanel.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(17, 46);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(41, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "用户名";
            // 
            // usernameTextBox
            // 
            this.usernameTextBox.Location = new System.Drawing.Point(118, 43);
            this.usernameTextBox.Name = "usernameTextBox";
            this.usernameTextBox.ReadOnly = true;
            this.usernameTextBox.Size = new System.Drawing.Size(100, 21);
            this.usernameTextBox.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(17, 107);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(41, 12);
            this.label2.TabIndex = 0;
            this.label2.Text = "原密码";
            // 
            // oripasswordTextBox
            // 
            this.oripasswordTextBox.Location = new System.Drawing.Point(118, 104);
            this.oripasswordTextBox.Name = "oripasswordTextBox";
            this.oripasswordTextBox.Size = new System.Drawing.Size(100, 21);
            this.oripasswordTextBox.TabIndex = 1;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(17, 144);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(41, 12);
            this.label3.TabIndex = 0;
            this.label3.Text = "新密码";
            // 
            // newpassTextBox
            // 
            this.newpassTextBox.Location = new System.Drawing.Point(118, 141);
            this.newpassTextBox.Name = "newpassTextBox";
            this.newpassTextBox.Size = new System.Drawing.Size(100, 21);
            this.newpassTextBox.TabIndex = 1;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(17, 183);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(65, 12);
            this.label4.TabIndex = 0;
            this.label4.Text = "确认新密码";
            // 
            // confirmPassTextBox
            // 
            this.confirmPassTextBox.Location = new System.Drawing.Point(118, 180);
            this.confirmPassTextBox.Name = "confirmPassTextBox";
            this.confirmPassTextBox.Size = new System.Drawing.Size(100, 21);
            this.confirmPassTextBox.TabIndex = 1;
            // 
            // modifyPassButton
            // 
            this.modifyPassButton.Location = new System.Drawing.Point(236, 193);
            this.modifyPassButton.Name = "modifyPassButton";
            this.modifyPassButton.Size = new System.Drawing.Size(75, 23);
            this.modifyPassButton.TabIndex = 2;
            this.modifyPassButton.Text = "修改密码";
            this.modifyPassButton.UseVisualStyleBackColor = true;
            this.modifyPassButton.Click += new System.EventHandler(this.modifyPassButton_Click);
            // 
            // permissionPanel
            // 
            this.permissionPanel.Controls.Add(this.obecheckBox);
            this.permissionPanel.Controls.Add(this.storeCheckBox);
            this.permissionPanel.Controls.Add(this.runningcheckBox);
            this.permissionPanel.Controls.Add(this.outlookCheckBox);
            this.permissionPanel.Controls.Add(this.receive_returnCheckBox);
            this.permissionPanel.Controls.Add(this.test2CheckBox);
            this.permissionPanel.Controls.Add(this.test1CheckBox);
            this.permissionPanel.Controls.Add(this.test_allCheckBox);
            this.permissionPanel.Controls.Add(this.repairCheckBox);
            this.permissionPanel.Controls.Add(this.bgaCheckBox);
            this.permissionPanel.Controls.Add(this.super_checkBox);
            this.permissionPanel.Location = new System.Drawing.Point(494, 12);
            this.permissionPanel.Name = "permissionPanel";
            this.permissionPanel.Size = new System.Drawing.Size(292, 245);
            this.permissionPanel.TabIndex = 8;
            // 
            // obecheckBox
            // 
            this.obecheckBox.AutoSize = true;
            this.obecheckBox.Enabled = false;
            this.obecheckBox.Location = new System.Drawing.Point(216, 182);
            this.obecheckBox.Name = "obecheckBox";
            this.obecheckBox.Size = new System.Drawing.Size(42, 16);
            this.obecheckBox.TabIndex = 12;
            this.obecheckBox.Text = "OBE";
            this.obecheckBox.UseVisualStyleBackColor = true;
            // 
            // storeCheckBox
            // 
            this.storeCheckBox.AutoSize = true;
            this.storeCheckBox.Enabled = false;
            this.storeCheckBox.Location = new System.Drawing.Point(16, 212);
            this.storeCheckBox.Name = "storeCheckBox";
            this.storeCheckBox.Size = new System.Drawing.Size(48, 16);
            this.storeCheckBox.TabIndex = 6;
            this.storeCheckBox.Text = "库存";
            this.storeCheckBox.UseVisualStyleBackColor = true;
            // 
            // runningcheckBox
            // 
            this.runningcheckBox.AutoSize = true;
            this.runningcheckBox.Enabled = false;
            this.runningcheckBox.Location = new System.Drawing.Point(120, 182);
            this.runningcheckBox.Name = "runningcheckBox";
            this.runningcheckBox.Size = new System.Drawing.Size(66, 16);
            this.runningcheckBox.TabIndex = 11;
            this.runningcheckBox.Text = "Running";
            this.runningcheckBox.UseVisualStyleBackColor = true;
            // 
            // outlookCheckBox
            // 
            this.outlookCheckBox.AutoSize = true;
            this.outlookCheckBox.Enabled = false;
            this.outlookCheckBox.Location = new System.Drawing.Point(16, 182);
            this.outlookCheckBox.Name = "outlookCheckBox";
            this.outlookCheckBox.Size = new System.Drawing.Size(48, 16);
            this.outlookCheckBox.TabIndex = 6;
            this.outlookCheckBox.Text = "外观";
            this.outlookCheckBox.UseVisualStyleBackColor = true;
            // 
            // receive_returnCheckBox
            // 
            this.receive_returnCheckBox.AutoSize = true;
            this.receive_returnCheckBox.Enabled = false;
            this.receive_returnCheckBox.Location = new System.Drawing.Point(16, 150);
            this.receive_returnCheckBox.Name = "receive_returnCheckBox";
            this.receive_returnCheckBox.Size = new System.Drawing.Size(60, 16);
            this.receive_returnCheckBox.TabIndex = 6;
            this.receive_returnCheckBox.Text = "收还货";
            this.receive_returnCheckBox.UseVisualStyleBackColor = true;
            // 
            // test2CheckBox
            // 
            this.test2CheckBox.AutoSize = true;
            this.test2CheckBox.Enabled = false;
            this.test2CheckBox.Location = new System.Drawing.Point(204, 110);
            this.test2CheckBox.Name = "test2CheckBox";
            this.test2CheckBox.Size = new System.Drawing.Size(54, 16);
            this.test2CheckBox.TabIndex = 6;
            this.test2CheckBox.Text = "测试2";
            this.test2CheckBox.UseVisualStyleBackColor = true;
            // 
            // test1CheckBox
            // 
            this.test1CheckBox.AutoSize = true;
            this.test1CheckBox.Enabled = false;
            this.test1CheckBox.Location = new System.Drawing.Point(120, 110);
            this.test1CheckBox.Name = "test1CheckBox";
            this.test1CheckBox.Size = new System.Drawing.Size(54, 16);
            this.test1CheckBox.TabIndex = 6;
            this.test1CheckBox.Text = "测试1";
            this.test1CheckBox.UseVisualStyleBackColor = true;
            // 
            // test_allCheckBox
            // 
            this.test_allCheckBox.AutoSize = true;
            this.test_allCheckBox.Enabled = false;
            this.test_allCheckBox.Location = new System.Drawing.Point(16, 110);
            this.test_allCheckBox.Name = "test_allCheckBox";
            this.test_allCheckBox.Size = new System.Drawing.Size(66, 16);
            this.test_allCheckBox.TabIndex = 6;
            this.test_allCheckBox.Text = "测试1&&2";
            this.test_allCheckBox.UseVisualStyleBackColor = true;
            // 
            // repairCheckBox
            // 
            this.repairCheckBox.AutoSize = true;
            this.repairCheckBox.Enabled = false;
            this.repairCheckBox.Location = new System.Drawing.Point(16, 80);
            this.repairCheckBox.Name = "repairCheckBox";
            this.repairCheckBox.Size = new System.Drawing.Size(48, 16);
            this.repairCheckBox.TabIndex = 6;
            this.repairCheckBox.Text = "维修";
            this.repairCheckBox.UseVisualStyleBackColor = true;
            // 
            // bgaCheckBox
            // 
            this.bgaCheckBox.AutoSize = true;
            this.bgaCheckBox.Enabled = false;
            this.bgaCheckBox.Location = new System.Drawing.Point(16, 45);
            this.bgaCheckBox.Name = "bgaCheckBox";
            this.bgaCheckBox.Size = new System.Drawing.Size(42, 16);
            this.bgaCheckBox.TabIndex = 6;
            this.bgaCheckBox.Text = "BGA";
            this.bgaCheckBox.UseVisualStyleBackColor = true;
            // 
            // super_checkBox
            // 
            this.super_checkBox.AutoSize = true;
            this.super_checkBox.Enabled = false;
            this.super_checkBox.Location = new System.Drawing.Point(16, 15);
            this.super_checkBox.Name = "super_checkBox";
            this.super_checkBox.Size = new System.Drawing.Size(84, 16);
            this.super_checkBox.TabIndex = 6;
            this.super_checkBox.Text = "超级管理员";
            this.super_checkBox.UseVisualStyleBackColor = true;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.modifyPassButton);
            this.panel1.Controls.Add(this.oripasswordTextBox);
            this.panel1.Controls.Add(this.confirmPassTextBox);
            this.panel1.Controls.Add(this.label6);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.workIdtextBox);
            this.panel1.Controls.Add(this.usernameTextBox);
            this.panel1.Controls.Add(this.newpassTextBox);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Location = new System.Drawing.Point(12, 21);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(363, 236);
            this.panel1.TabIndex = 9;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(17, 80);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(29, 12);
            this.label6.TabIndex = 0;
            this.label6.Text = "工号";
            // 
            // workIdtextBox
            // 
            this.workIdtextBox.Location = new System.Drawing.Point(118, 77);
            this.workIdtextBox.Name = "workIdtextBox";
            this.workIdtextBox.ReadOnly = true;
            this.workIdtextBox.Size = new System.Drawing.Size(100, 21);
            this.workIdtextBox.TabIndex = 1;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(406, 47);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(65, 12);
            this.label5.TabIndex = 10;
            this.label5.Text = "当前的权限";
            // 
            // UserSelfForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(846, 555);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.permissionPanel);
            this.Name = "UserSelfForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "用户信息界面";
            this.permissionPanel.ResumeLayout(false);
            this.permissionPanel.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox usernameTextBox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox oripasswordTextBox;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox newpassTextBox;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox confirmPassTextBox;
        private System.Windows.Forms.Button modifyPassButton;
        private System.Windows.Forms.Panel permissionPanel;
        private System.Windows.Forms.CheckBox storeCheckBox;
        private System.Windows.Forms.CheckBox outlookCheckBox;
        private System.Windows.Forms.CheckBox receive_returnCheckBox;
        private System.Windows.Forms.CheckBox test2CheckBox;
        private System.Windows.Forms.CheckBox test1CheckBox;
        private System.Windows.Forms.CheckBox test_allCheckBox;
        private System.Windows.Forms.CheckBox repairCheckBox;
        private System.Windows.Forms.CheckBox bgaCheckBox;
        private System.Windows.Forms.CheckBox super_checkBox;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox workIdtextBox;
        private System.Windows.Forms.CheckBox obecheckBox;
        private System.Windows.Forms.CheckBox runningcheckBox;
    }
}