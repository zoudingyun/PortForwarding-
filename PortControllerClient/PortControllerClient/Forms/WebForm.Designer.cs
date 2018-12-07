namespace PortControllerClient.Forms
{
    partial class WebForm
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(WebForm));
            this.webBrowser1 = new System.Windows.Forms.WebBrowser();
            this.small = new System.Windows.Forms.Button();
            this.exit = new System.Windows.Forms.Button();
            this.mainLogo = new System.Windows.Forms.NotifyIcon(this.components);
            this.SuspendLayout();
            // 
            // webBrowser1
            // 
            this.webBrowser1.Location = new System.Drawing.Point(0, 27);
            this.webBrowser1.MinimumSize = new System.Drawing.Size(20, 20);
            this.webBrowser1.Name = "webBrowser1";
            this.webBrowser1.ScriptErrorsSuppressed = true;
            this.webBrowser1.ScrollBarsEnabled = false;
            this.webBrowser1.Size = new System.Drawing.Size(1113, 647);
            this.webBrowser1.TabIndex = 0;
            // 
            // small
            // 
            this.small.BackColor = System.Drawing.Color.Transparent;
            this.small.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.small.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.small.Location = new System.Drawing.Point(1030, 4);
            this.small.Margin = new System.Windows.Forms.Padding(8, 4, 4, 4);
            this.small.Name = "small";
            this.small.Size = new System.Drawing.Size(20, 19);
            this.small.TabIndex = 6;
            this.small.Text = "-";
            this.small.UseVisualStyleBackColor = false;
            this.small.Click += new System.EventHandler(this.small_Click);
            // 
            // exit
            // 
            this.exit.BackColor = System.Drawing.Color.Transparent;
            this.exit.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.exit.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.exit.Location = new System.Drawing.Point(1062, 4);
            this.exit.Margin = new System.Windows.Forms.Padding(8, 4, 4, 4);
            this.exit.Name = "exit";
            this.exit.Padding = new System.Windows.Forms.Padding(2, 0, 0, 0);
            this.exit.Size = new System.Drawing.Size(20, 19);
            this.exit.TabIndex = 8;
            this.exit.Text = "×";
            this.exit.UseVisualStyleBackColor = false;
            this.exit.Click += new System.EventHandler(this.exit_Click);
            // 
            // mainLogo
            // 
            this.mainLogo.Icon = ((System.Drawing.Icon)(resources.GetObject("mainLogo.Icon")));
            this.mainLogo.Text = "端口转发控制台";
            this.mainLogo.Visible = true;
            this.mainLogo.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.mainLogo_MouseDoubleClick);
            // 
            // WebForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1113, 673);
            this.Controls.Add(this.exit);
            this.Controls.Add(this.small);
            this.Controls.Add(this.webBrowser1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "WebForm";
            this.Text = "WebForm";
            this.Load += new System.EventHandler(this.WebForm_Load_1);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.WebBrowser webBrowser1;
        private System.Windows.Forms.Button small;
        private System.Windows.Forms.Button exit;
        private System.Windows.Forms.NotifyIcon mainLogo;
    }
}