namespace PortControllerClient.Forms
{
    partial class changePWD
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(changePWD));
            this.oldpwd = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.newpwd = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.newpwd2 = new System.Windows.Forms.TextBox();
            this.changPwdButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // oldpwd
            // 
            this.oldpwd.Location = new System.Drawing.Point(73, 35);
            this.oldpwd.Name = "oldpwd";
            this.oldpwd.PasswordChar = '牟';
            this.oldpwd.Size = new System.Drawing.Size(215, 21);
            this.oldpwd.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 38);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(41, 12);
            this.label1.TabIndex = 1;
            this.label1.Text = "原密码";
            // 
            // newpwd
            // 
            this.newpwd.Location = new System.Drawing.Point(73, 97);
            this.newpwd.Name = "newpwd";
            this.newpwd.PasswordChar = '志';
            this.newpwd.Size = new System.Drawing.Size(215, 21);
            this.newpwd.TabIndex = 2;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 100);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(41, 12);
            this.label2.TabIndex = 3;
            this.label2.Text = "新密码";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 127);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(53, 12);
            this.label3.TabIndex = 5;
            this.label3.Text = "确认密码";
            // 
            // newpwd2
            // 
            this.newpwd2.Location = new System.Drawing.Point(73, 124);
            this.newpwd2.Name = "newpwd2";
            this.newpwd2.PasswordChar = '×';
            this.newpwd2.Size = new System.Drawing.Size(215, 21);
            this.newpwd2.TabIndex = 4;
            // 
            // changPwdButton
            // 
            this.changPwdButton.Location = new System.Drawing.Point(213, 198);
            this.changPwdButton.Name = "changPwdButton";
            this.changPwdButton.Size = new System.Drawing.Size(75, 23);
            this.changPwdButton.TabIndex = 6;
            this.changPwdButton.Text = "提交";
            this.changPwdButton.UseVisualStyleBackColor = true;
            this.changPwdButton.Click += new System.EventHandler(this.changPwdButton_Click);
            // 
            // changePWD
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(313, 233);
            this.Controls.Add(this.changPwdButton);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.newpwd2);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.newpwd);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.oldpwd);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "changePWD";
            this.Text = "修改密码";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox oldpwd;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox newpwd;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox newpwd2;
        private System.Windows.Forms.Button changPwdButton;
    }
}