using PortControllerClient.Forms;

namespace PortControllerClient
{
    partial class mainForm
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(mainForm));
            this.label1 = new System.Windows.Forms.Label();
            this.userDesc = new System.Windows.Forms.Label();
            this.portList = new System.Windows.Forms.DataGridView();
            this.localAddress = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.localPort = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.targetAddress = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.targetPort = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.startPort = new System.Windows.Forms.Button();
            this.addPort = new System.Windows.Forms.Button();
            this.deletePort = new System.Windows.Forms.Button();
            this.mainLogo = new System.Windows.Forms.NotifyIcon(this.components);
            this.contextMenuStrip2 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem3 = new System.Windows.Forms.ToolStripMenuItem();
            this.button1 = new System.Windows.Forms.Button();
            this.userBox = new System.Windows.Forms.GroupBox();
            this.button4 = new System.Windows.Forms.Button();
            this.adminBox = new System.Windows.Forms.GroupBox();
            this.button2 = new System.Windows.Forms.Button();
            this.button5 = new System.Windows.Forms.Button();
            this.button6 = new System.Windows.Forms.Button();
            this.button7 = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.portList)).BeginInit();
            this.contextMenuStrip1.SuspendLayout();
            this.contextMenuStrip2.SuspendLayout();
            this.userBox.SuspendLayout();
            this.adminBox.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.AliceBlue;
            this.label1.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.label1.Location = new System.Drawing.Point(15, 28);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(63, 14);
            this.label1.TabIndex = 0;
            this.label1.Text = "欢迎你：";
            // 
            // userDesc
            // 
            this.userDesc.AutoSize = true;
            this.userDesc.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.userDesc.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.userDesc.Location = new System.Drawing.Point(108, 28);
            this.userDesc.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.userDesc.Name = "userDesc";
            this.userDesc.Size = new System.Drawing.Size(21, 14);
            this.userDesc.TabIndex = 1;
            this.userDesc.Text = "<>";
            // 
            // portList
            // 
            this.portList.BackgroundColor = System.Drawing.SystemColors.ButtonFace;
            this.portList.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.portList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.portList.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.localAddress,
            this.localPort,
            this.targetAddress,
            this.targetPort});
            this.portList.ContextMenuStrip = this.contextMenuStrip1;
            this.portList.Location = new System.Drawing.Point(18, 60);
            this.portList.Margin = new System.Windows.Forms.Padding(4);
            this.portList.Name = "portList";
            this.portList.RowTemplate.Height = 27;
            this.portList.Size = new System.Drawing.Size(441, 559);
            this.portList.TabIndex = 2;
            // 
            // localAddress
            // 
            this.localAddress.HeaderText = "代理地址";
            this.localAddress.Name = "localAddress";
            // 
            // localPort
            // 
            this.localPort.HeaderText = "代理端口";
            this.localPort.Name = "localPort";
            // 
            // targetAddress
            // 
            this.targetAddress.HeaderText = "目的地址";
            this.targetAddress.Name = "targetAddress";
            // 
            // targetPort
            // 
            this.targetPort.HeaderText = "目的端口";
            this.targetPort.Name = "targetPort";
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(101, 26);
            this.contextMenuStrip1.Opening += new System.ComponentModel.CancelEventHandler(this.contextMenuStrip1_Opening);
            // 
            // ToolStripMenuItem
            // 
            this.ToolStripMenuItem.Name = "ToolStripMenuItem";
            this.ToolStripMenuItem.Size = new System.Drawing.Size(100, 22);
            this.ToolStripMenuItem.Text = "删除";
            this.ToolStripMenuItem.Click += new System.EventHandler(this.删除ToolStripMenuItem_Click);
            // 
            // startPort
            // 
            this.startPort.Location = new System.Drawing.Point(508, 593);
            this.startPort.Margin = new System.Windows.Forms.Padding(4);
            this.startPort.Name = "startPort";
            this.startPort.Size = new System.Drawing.Size(117, 38);
            this.startPort.TabIndex = 3;
            this.startPort.Text = "启动";
            this.startPort.UseVisualStyleBackColor = true;
            this.startPort.Click += new System.EventHandler(this.startPort_Click);
            // 
            // addPort
            // 
            this.addPort.BackColor = System.Drawing.Color.Transparent;
            this.addPort.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.addPort.Location = new System.Drawing.Point(622, 13);
            this.addPort.Margin = new System.Windows.Forms.Padding(4);
            this.addPort.Name = "addPort";
            this.addPort.Size = new System.Drawing.Size(30, 30);
            this.addPort.TabIndex = 4;
            this.addPort.Text = "×";
            this.addPort.UseVisualStyleBackColor = false;
            this.addPort.Click += new System.EventHandler(this.addPort_Click);
            // 
            // deletePort
            // 
            this.deletePort.BackColor = System.Drawing.Color.Transparent;
            this.deletePort.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.deletePort.Location = new System.Drawing.Point(584, 13);
            this.deletePort.Margin = new System.Windows.Forms.Padding(4);
            this.deletePort.Name = "deletePort";
            this.deletePort.Size = new System.Drawing.Size(30, 30);
            this.deletePort.TabIndex = 5;
            this.deletePort.Text = "-";
            this.deletePort.UseVisualStyleBackColor = false;
            this.deletePort.Click += new System.EventHandler(this.deletePort_Click);
            // 
            // mainLogo
            // 
            this.mainLogo.ContextMenuStrip = this.contextMenuStrip2;
            this.mainLogo.Icon = ((System.Drawing.Icon)(resources.GetObject("mainLogo.Icon")));
            this.mainLogo.Text = "端口转发控制台";
            this.mainLogo.Visible = true;
            this.mainLogo.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.mainLogo_MouseDoubleClick);
            // 
            // contextMenuStrip2
            // 
            this.contextMenuStrip2.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem1,
            this.toolStripMenuItem2,
            this.toolStripMenuItem3});
            this.contextMenuStrip2.Name = "contextMenuStrip2";
            this.contextMenuStrip2.Size = new System.Drawing.Size(137, 70);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(136, 22);
            this.toolStripMenuItem1.Text = "显示主界面";
            this.toolStripMenuItem1.Click += new System.EventHandler(this.toolStripMenuItem1_Click);
            // 
            // toolStripMenuItem2
            // 
            this.toolStripMenuItem2.Name = "toolStripMenuItem2";
            this.toolStripMenuItem2.Size = new System.Drawing.Size(136, 22);
            this.toolStripMenuItem2.Text = "端口映射";
            // 
            // toolStripMenuItem3
            // 
            this.toolStripMenuItem3.Name = "toolStripMenuItem3";
            this.toolStripMenuItem3.Size = new System.Drawing.Size(136, 22);
            this.toolStripMenuItem3.Text = "退出";
            this.toolStripMenuItem3.Click += new System.EventHandler(this.toolStripMenuItem3_Click);
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.Color.Transparent;
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button1.Location = new System.Drawing.Point(42, 22);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(117, 23);
            this.button1.TabIndex = 6;
            this.button1.Text = "修改密码";
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // userBox
            // 
            this.userBox.BackColor = System.Drawing.Color.Transparent;
            this.userBox.Controls.Add(this.button4);
            this.userBox.Controls.Add(this.button1);
            this.userBox.Location = new System.Drawing.Point(466, 60);
            this.userBox.Name = "userBox";
            this.userBox.Size = new System.Drawing.Size(187, 93);
            this.userBox.TabIndex = 7;
            this.userBox.TabStop = false;
            this.userBox.Text = "用户功能";
            // 
            // button4
            // 
            this.button4.BackColor = System.Drawing.Color.Transparent;
            this.button4.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button4.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.button4.Location = new System.Drawing.Point(42, 51);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(117, 23);
            this.button4.TabIndex = 9;
            this.button4.Text = "修改用户名";
            this.button4.UseVisualStyleBackColor = false;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // adminBox
            // 
            this.adminBox.BackColor = System.Drawing.Color.Transparent;
            this.adminBox.Controls.Add(this.button2);
            this.adminBox.Controls.Add(this.button5);
            this.adminBox.Controls.Add(this.button6);
            this.adminBox.Controls.Add(this.button7);
            this.adminBox.Location = new System.Drawing.Point(466, 159);
            this.adminBox.Name = "adminBox";
            this.adminBox.Size = new System.Drawing.Size(187, 153);
            this.adminBox.TabIndex = 10;
            this.adminBox.TabStop = false;
            this.adminBox.Text = "管理员功能";
            // 
            // button2
            // 
            this.button2.BackColor = System.Drawing.Color.Transparent;
            this.button2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button2.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.button2.Location = new System.Drawing.Point(42, 109);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(117, 23);
            this.button2.TabIndex = 10;
            this.button2.Text = "调整权限";
            this.button2.UseVisualStyleBackColor = false;
            // 
            // button5
            // 
            this.button5.BackColor = System.Drawing.Color.Transparent;
            this.button5.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button5.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.button5.Location = new System.Drawing.Point(42, 22);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(117, 23);
            this.button5.TabIndex = 9;
            this.button5.Text = "重置用户密码";
            this.button5.UseVisualStyleBackColor = false;
            this.button5.Click += new System.EventHandler(this.button5_Click);
            // 
            // button6
            // 
            this.button6.BackColor = System.Drawing.Color.Transparent;
            this.button6.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button6.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.button6.Location = new System.Drawing.Point(42, 80);
            this.button6.Name = "button6";
            this.button6.Size = new System.Drawing.Size(117, 23);
            this.button6.TabIndex = 8;
            this.button6.Text = "删除用户";
            this.button6.UseVisualStyleBackColor = false;
            // 
            // button7
            // 
            this.button7.BackColor = System.Drawing.Color.Transparent;
            this.button7.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button7.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.button7.Location = new System.Drawing.Point(42, 51);
            this.button7.Name = "button7";
            this.button7.Size = new System.Drawing.Size(117, 23);
            this.button7.TabIndex = 7;
            this.button7.Text = "新增用户";
            this.button7.UseVisualStyleBackColor = false;
            this.button7.Click += new System.EventHandler(this.button7_Click);
            // 
            // mainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.ClientSize = new System.Drawing.Size(665, 644);
            this.Controls.Add(this.adminBox);
            this.Controls.Add(this.userBox);
            this.Controls.Add(this.deletePort);
            this.Controls.Add(this.addPort);
            this.Controls.Add(this.startPort);
            this.Controls.Add(this.portList);
            this.Controls.Add(this.userDesc);
            this.Controls.Add(this.label1);
            this.Font = new System.Drawing.Font("黑体", 10.5F);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "mainForm";
            this.Text = "端口转发控制台";
            this.Load += new System.EventHandler(this.mainForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.portList)).EndInit();
            this.contextMenuStrip1.ResumeLayout(false);
            this.contextMenuStrip2.ResumeLayout(false);
            this.userBox.ResumeLayout(false);
            this.adminBox.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Login login;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label userDesc;
        private System.Windows.Forms.DataGridView portList;
        private System.Windows.Forms.DataGridViewTextBoxColumn localAddress;
        private System.Windows.Forms.DataGridViewTextBoxColumn localPort;
        private System.Windows.Forms.DataGridViewTextBoxColumn targetAddress;
        private System.Windows.Forms.DataGridViewTextBoxColumn targetPort;
        private System.Windows.Forms.Button startPort;
        private System.Windows.Forms.Button addPort;
        private System.Windows.Forms.Button deletePort;
        private System.Windows.Forms.NotifyIcon mainLogo;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItem;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip2;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem2;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem3;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.GroupBox userBox;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.GroupBox adminBox;
        private System.Windows.Forms.Button button5;
        private System.Windows.Forms.Button button6;
        private System.Windows.Forms.Button button7;
        private System.Windows.Forms.Button button2;
    }
}

