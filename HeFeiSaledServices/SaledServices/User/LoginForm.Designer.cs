namespace SaledServices
{
    partial class LoginForm
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.passwordInput = new System.Windows.Forms.TextBox();
            this.password = new System.Windows.Forms.Label();
            this.workid = new System.Windows.Forms.Label();
            this.workIdInput = new System.Windows.Forms.TextBox();
            this.login = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.passwordInput);
            this.panel1.Controls.Add(this.password);
            this.panel1.Controls.Add(this.workid);
            this.panel1.Controls.Add(this.workIdInput);
            this.panel1.Controls.Add(this.login);
            this.panel1.Location = new System.Drawing.Point(45, 48);
            this.panel1.Margin = new System.Windows.Forms.Padding(5);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(655, 453);
            this.panel1.TabIndex = 0;
            // 
            // passwordInput
            // 
            this.passwordInput.Location = new System.Drawing.Point(280, 188);
            this.passwordInput.Margin = new System.Windows.Forms.Padding(5);
            this.passwordInput.Name = "passwordInput";
            this.passwordInput.PasswordChar = '*';
            this.passwordInput.Size = new System.Drawing.Size(164, 30);
            this.passwordInput.TabIndex = 1;
            this.passwordInput.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.passwordInput_KeyPress);
            // 
            // password
            // 
            this.password.AutoSize = true;
            this.password.Location = new System.Drawing.Point(100, 203);
            this.password.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.password.Name = "password";
            this.password.Size = new System.Drawing.Size(49, 20);
            this.password.TabIndex = 3;
            this.password.Text = "密码";
            // 
            // workid
            // 
            this.workid.AutoSize = true;
            this.workid.Location = new System.Drawing.Point(100, 58);
            this.workid.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.workid.Name = "workid";
            this.workid.Size = new System.Drawing.Size(49, 20);
            this.workid.TabIndex = 2;
            this.workid.Text = "工号";
            // 
            // workIdInput
            // 
            this.workIdInput.Location = new System.Drawing.Point(280, 45);
            this.workIdInput.Margin = new System.Windows.Forms.Padding(5);
            this.workIdInput.Name = "workIdInput";
            this.workIdInput.Size = new System.Drawing.Size(164, 30);
            this.workIdInput.TabIndex = 0;
            this.workIdInput.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.workIdInput_KeyPress);
            // 
            // login
            // 
            this.login.Location = new System.Drawing.Point(280, 312);
            this.login.Margin = new System.Windows.Forms.Padding(5);
            this.login.Name = "login";
            this.login.Size = new System.Drawing.Size(125, 38);
            this.login.TabIndex = 2;
            this.login.Text = "登录";
            this.login.UseVisualStyleBackColor = true;
            this.login.Click += new System.EventHandler(this.login_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(627, 537);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(93, 44);
            this.button1.TabIndex = 5;
            this.button1.Text = "test";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // LoginForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(765, 615);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.panel1);
            this.Font = new System.Drawing.Font("SimSun", 15F);
            this.Margin = new System.Windows.Forms.Padding(5);
            this.Name = "LoginForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "登录界面";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TextBox passwordInput;
        private System.Windows.Forms.Label password;
        private System.Windows.Forms.Label workid;
        private System.Windows.Forms.TextBox workIdInput;
        private System.Windows.Forms.Button login;
        private System.Windows.Forms.Button button1;
    }
}