namespace PortControllerClient.Forms
{
    partial class ChangeUserPwd
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
            this.yourPwd = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.targetUser = new System.Windows.Forms.TextBox();
            this.send = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // yourPwd
            // 
            this.yourPwd.Location = new System.Drawing.Point(83, 38);
            this.yourPwd.Name = "yourPwd";
            this.yourPwd.PasswordChar = '牟';
            this.yourPwd.Size = new System.Drawing.Size(219, 21);
            this.yourPwd.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 41);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 12);
            this.label1.TabIndex = 1;
            this.label1.Text = "你的密码：";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 124);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(65, 12);
            this.label2.TabIndex = 2;
            this.label2.Text = "重置账户：";
            // 
            // targetUser
            // 
            this.targetUser.Location = new System.Drawing.Point(83, 121);
            this.targetUser.Name = "targetUser";
            this.targetUser.Size = new System.Drawing.Size(219, 21);
            this.targetUser.TabIndex = 3;
            // 
            // send
            // 
            this.send.Location = new System.Drawing.Point(227, 181);
            this.send.Name = "send";
            this.send.Size = new System.Drawing.Size(75, 23);
            this.send.TabIndex = 4;
            this.send.Text = "提交";
            this.send.UseVisualStyleBackColor = true;
            this.send.Click += new System.EventHandler(this.send_Click);
            // 
            // ChangeUserPwd
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(320, 237);
            this.Controls.Add(this.send);
            this.Controls.Add(this.targetUser);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.yourPwd);
            this.Name = "ChangeUserPwd";
            this.Text = "ChangeUserPwd";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox yourPwd;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox targetUser;
        private System.Windows.Forms.Button send;
    }
}