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
    public partial class changePWD : Form
    {
        public changePWD()
        {
            InitializeComponent();
        }

        private void changPwdButton_Click(object sender, EventArgs e)
        {
            try { 
                if (this.oldpwd.Text == "")
                {
                    MessageBox.Show("原密码不能为空！");
                    return;
                }
                else if (this.newpwd.Text == "")
                {
                    MessageBox.Show("新密码不能为空！");
                    return;
                }
                else if (this.newpwd2.Text == "")
                {
                    MessageBox.Show("确认密码不能为空！");
                    return;
                }
                else if (this.newpwd2.Text != this.newpwd.Text)
                {
                    MessageBox.Show("两次输入的密码不同！");
                    return;
                }


                Hashtable sendTable = new Hashtable();
                sendTable.Add("VER",PublicVariable4CS.ver);
                sendTable.Add("TYPE", "CHANGPWD");
                sendTable.Add("USER", PublicVariable4CS.UserName);
                sendTable.Add("PWD", this.oldpwd.Text);
                sendTable.Add("NEWPWD", this.newpwd.Text);

                TcpClient tc2 = new TcpClient(PublicVariable4CS.ServerIP, PublicVariable4CS.ServerPort);
                NetworkStream ns2 = tc2.GetStream();

                byte[] byteArray = System.Text.Encoding.UTF8.GetBytes(PublicVariable4CS.setMessages(sendTable));
                ns2.Write(byteArray, 0, byteArray.Length);

                byte[] bt = new byte[10240];
                int count = ns2.Read(bt, 0, bt.Length);

                Hashtable messageTable = PublicVariable4CS.getMessages(System.Text.Encoding.UTF8.GetString(bt));
                if (messageTable.Contains("TYPE"))
                {
                    if ((string)messageTable["TYPE"] == "CHANGPWD-RE")
                    {
                        if (messageTable.Contains("RE_ANSWER"))
                        {
                            if ((string)messageTable["RE_ANSWER"] == "TRUE")
                            {
                                MessageBox.Show("密码修改成功");
                            }
                            else if((string)messageTable["RE_ANSWER"] == "FALSE")
                            {
                                MessageBox.Show("密码修改失败："+ (string)messageTable["RE_MESSAGE"]);
                            }
                        }
                        else
                        {
                            PublicVariable4CS.errorMessage("服务器答复异常-无答复");
                        }
                    }
                    else
                    {
                        PublicVariable4CS.errorMessage("服务器响应异常-"+messageTable["TYPE"]);
                    }
                }
                else
                {
                    PublicVariable4CS.errorMessage("服务器响应异常-无答复类型");
                }
            }catch(Exception ex)
            {
                PublicVariable4CS.errorMessage("内部异常："+ex);
            }

        }
           
    }
}
