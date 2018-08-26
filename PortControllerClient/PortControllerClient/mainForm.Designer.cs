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
            this.startPort = new System.Windows.Forms.Button();
            this.addPort = new System.Windows.Forms.Button();
            this.deletePort = new System.Windows.Forms.Button();
            this.mainLogo = new System.Windows.Forms.NotifyIcon(this.components);
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.contextMenuStrip2 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem3 = new System.Windows.Forms.ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)(this.portList)).BeginInit();
            this.contextMenuStrip1.SuspendLayout();
            this.contextMenuStrip2.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.BackColor = System.Drawing.Color.AliceBlue;
            this.label1.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.label1.Name = "label1";
            // 
            // userDesc
            // 
            resources.ApplyResources(this.userDesc, "userDesc");
            this.userDesc.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.userDesc.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.userDesc.Name = "userDesc";
            // 
            // portList
            // 
            this.portList.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.portList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.portList.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.localAddress,
            this.localPort,
            this.targetAddress,
            this.targetPort});
            this.portList.ContextMenuStrip = this.contextMenuStrip1;
            resources.ApplyResources(this.portList, "portList");
            this.portList.Name = "portList";
            this.portList.RowTemplate.Height = 27;
            // 
            // localAddress
            // 
            resources.ApplyResources(this.localAddress, "localAddress");
            this.localAddress.Name = "localAddress";
            // 
            // localPort
            // 
            resources.ApplyResources(this.localPort, "localPort");
            this.localPort.Name = "localPort";
            // 
            // targetAddress
            // 
            resources.ApplyResources(this.targetAddress, "targetAddress");
            this.targetAddress.Name = "targetAddress";
            // 
            // targetPort
            // 
            resources.ApplyResources(this.targetPort, "targetPort");
            this.targetPort.Name = "targetPort";
            // 
            // startPort
            // 
            resources.ApplyResources(this.startPort, "startPort");
            this.startPort.Name = "startPort";
            this.startPort.UseVisualStyleBackColor = true;
            this.startPort.Click += new System.EventHandler(this.startPort_Click);
            // 
            // addPort
            // 
            this.addPort.BackColor = System.Drawing.Color.Transparent;
            resources.ApplyResources(this.addPort, "addPort");
            this.addPort.Name = "addPort";
            this.addPort.UseVisualStyleBackColor = false;
            this.addPort.Click += new System.EventHandler(this.addPort_Click);
            // 
            // deletePort
            // 
            this.deletePort.BackColor = System.Drawing.Color.Transparent;
            resources.ApplyResources(this.deletePort, "deletePort");
            this.deletePort.Name = "deletePort";
            this.deletePort.UseVisualStyleBackColor = false;
            this.deletePort.Click += new System.EventHandler(this.deletePort_Click);
            // 
            // mainLogo
            // 
            this.mainLogo.ContextMenuStrip = this.contextMenuStrip2;
            resources.ApplyResources(this.mainLogo, "mainLogo");
            this.mainLogo.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.mainLogo_MouseDoubleClick);
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            resources.ApplyResources(this.contextMenuStrip1, "contextMenuStrip1");
            this.contextMenuStrip1.Opening += new System.ComponentModel.CancelEventHandler(this.contextMenuStrip1_Opening);
            // 
            // ToolStripMenuItem
            // 
            this.ToolStripMenuItem.Name = "ToolStripMenuItem";
            resources.ApplyResources(this.ToolStripMenuItem, "ToolStripMenuItem");
            this.ToolStripMenuItem.Click += new System.EventHandler(this.删除ToolStripMenuItem_Click);
            // 
            // contextMenuStrip2
            // 
            this.contextMenuStrip2.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem1,
            this.toolStripMenuItem2,
            this.toolStripMenuItem3});
            this.contextMenuStrip2.Name = "contextMenuStrip2";
            resources.ApplyResources(this.contextMenuStrip2, "contextMenuStrip2");
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            resources.ApplyResources(this.toolStripMenuItem1, "toolStripMenuItem1");
            this.toolStripMenuItem1.Click += new System.EventHandler(this.toolStripMenuItem1_Click);
            // 
            // toolStripMenuItem2
            // 
            this.toolStripMenuItem2.Name = "toolStripMenuItem2";
            resources.ApplyResources(this.toolStripMenuItem2, "toolStripMenuItem2");
            // 
            // toolStripMenuItem3
            // 
            this.toolStripMenuItem3.Name = "toolStripMenuItem3";
            resources.ApplyResources(this.toolStripMenuItem3, "toolStripMenuItem3");
            this.toolStripMenuItem3.Click += new System.EventHandler(this.toolStripMenuItem3_Click);
            // 
            // mainForm
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.Controls.Add(this.deletePort);
            this.Controls.Add(this.addPort);
            this.Controls.Add(this.startPort);
            this.Controls.Add(this.portList);
            this.Controls.Add(this.userDesc);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "mainForm";
            this.Load += new System.EventHandler(this.mainForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.portList)).EndInit();
            this.contextMenuStrip1.ResumeLayout(false);
            this.contextMenuStrip2.ResumeLayout(false);
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
    }
}

