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
            ((System.ComponentModel.ISupportInitialize)(this.portList)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.label1.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.label1.Location = new System.Drawing.Point(12, 24);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "欢迎你：";
            // 
            // userDesc
            // 
            this.userDesc.AutoSize = true;
            this.userDesc.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.userDesc.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.userDesc.Location = new System.Drawing.Point(76, 24);
            this.userDesc.Name = "userDesc";
            this.userDesc.Size = new System.Drawing.Size(17, 12);
            this.userDesc.TabIndex = 1;
            this.userDesc.Text = "<>";
            // 
            // portList
            // 
            this.portList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.portList.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.localAddress,
            this.localPort,
            this.targetAddress,
            this.targetPort});
            this.portList.Location = new System.Drawing.Point(15, 52);
            this.portList.Name = "portList";
            this.portList.RowTemplate.Height = 27;
            this.portList.Size = new System.Drawing.Size(445, 488);
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
            // startPort
            // 
            this.startPort.Location = new System.Drawing.Point(466, 508);
            this.startPort.Name = "startPort";
            this.startPort.Size = new System.Drawing.Size(100, 32);
            this.startPort.TabIndex = 3;
            this.startPort.Text = "启动";
            this.startPort.UseVisualStyleBackColor = true;
            this.startPort.Click += new System.EventHandler(this.startPort_Click);
            // 
            // addPort
            // 
            this.addPort.Location = new System.Drawing.Point(466, 52);
            this.addPort.Name = "addPort";
            this.addPort.Size = new System.Drawing.Size(100, 32);
            this.addPort.TabIndex = 4;
            this.addPort.Text = "增加";
            this.addPort.UseVisualStyleBackColor = true;
            this.addPort.Visible = false;
            this.addPort.Click += new System.EventHandler(this.addPort_Click);
            // 
            // deletePort
            // 
            this.deletePort.Location = new System.Drawing.Point(466, 99);
            this.deletePort.Name = "deletePort";
            this.deletePort.Size = new System.Drawing.Size(100, 32);
            this.deletePort.TabIndex = 5;
            this.deletePort.Text = "删除";
            this.deletePort.UseVisualStyleBackColor = true;
            this.deletePort.Visible = false;
            // 
            // mainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.ClientSize = new System.Drawing.Size(570, 552);
            this.Controls.Add(this.deletePort);
            this.Controls.Add(this.addPort);
            this.Controls.Add(this.startPort);
            this.Controls.Add(this.portList);
            this.Controls.Add(this.userDesc);
            this.Controls.Add(this.label1);
            this.Font = new System.Drawing.Font("仿宋", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "mainForm";
            this.Text = "端口转发控制台";
            this.Load += new System.EventHandler(this.mainForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.portList)).EndInit();
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
    }
}

