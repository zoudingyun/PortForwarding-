using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Windows.Forms;

namespace PortControllerClient.Forms
{
    public partial class ChangeUserPwd : Form
    {
        public ChangeUserPwd()
        {
            InitializeComponent();
        }

        private void send_Click(object sender, EventArgs e)
        {
            Hashtable send = new Hashtable();
            send.Add("VER", PublicVariable4CS.ver);
            send.Add("TYPE", "RESET-USER-PWD");
            send.Add("USER", PublicVariable4CS.UserName);
            send.Add("PWD", this.yourPwd.Text);
            send.Add("TARGET-USER", this.targetUser.Text);

            TcpClient tc2 = new TcpClient(PublicVariable4CS.ServerIP, PublicVariable4CS.ServerPort);
            NetworkStream ns2 = tc2.GetStream();

            byte[] byteArray = System.Text.Encoding.UTF8.GetBytes(PublicVariable4CS.setMessages(send));
            ns2.Write(byteArray, 0, byteArray.Length);

            byte[] bt = new byte[10240];
            int count = ns2.Read(bt, 0, bt.Length);

            Hashtable messageTable = PublicVariable4CS.getMessages(System.Text.Encoding.UTF8.GetString(bt));

            if (messageTable.Contains("TYPE"))
            {
                if ((string)messageTable["TYPE"] == "RESET-USER-PWD-RE")
                {
                    if (messageTable.Contains("RE_ANSWER"))
                    {
                        if ((string)messageTable["RE_ANSWER"] == "TRUE")
                        {
                            MessageBox.Show("重置密码成功");
                        }
                        else if ((string)messageTable["RE_ANSWER"] == "FALSE")
                        {
                            MessageBox.Show("重置密码失败：" + (string)messageTable["RE_MESSAGE"]);
                        }
                    }
                    else
                    {
                        PublicVariable4CS.errorMessage("服务器答复异常-无答复");
                    }
                }
                else
                {
                    PublicVariable4CS.errorMessage("服务器响应异常-" + messageTable["TYPE"]);
                }
            }
            else
            {
                PublicVariable4CS.errorMessage("服务器响应异常-无答复类型");
            }
        }
    }
}
