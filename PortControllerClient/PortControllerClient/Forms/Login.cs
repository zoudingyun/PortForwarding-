using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Windows.Forms;
using fileTools;

namespace PortControllerClient.Forms
{
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
            string userid = INIhelp.GetValue("userid");
            string userpwd = INIhelp.GetValue("userpwd");
            string serverIp = INIhelp.GetValue("serverip");
            string serverPort = INIhelp.GetValue("serverport");
            string saveUser = INIhelp.GetValue("saveuser");

            this.serverIP.Text = serverIp;
            this.serverPort.Text = serverPort;
            this.userName.Text = userid;
            this.passWord.Text = userpwd;
            if ("true" == saveUser)
                this.saveUsername.Checked = true;
            else
                this.saveUsername.Checked = false;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (this.saveUsername.Checked)
            {
                INIhelp.SetValue("userid", this.userName.Text);
                INIhelp.SetValue("userpwd", this.passWord.Text);
                INIhelp.SetValue("serverip", this.serverIP.Text);
                INIhelp.SetValue("serverport", this.serverPort.Text);
                INIhelp.SetValue("saveuser","true");
            }
            else
            {
                INIhelp.SetValue("userid", "");
                INIhelp.SetValue("userpwd", "");
                INIhelp.SetValue("saveuser", "false");
            }
            PublicVariable4CS.UserName = this.userName.Text;
            PublicVariable4CS.PassWord = this.passWord.Text;
            PublicVariable4CS.ServerIP = this.serverIP.Text;
            PublicVariable4CS.ServerPort = int.Parse(this.serverPort.Text);
            PublicVariable4CS.SaveUser = this.saveUsername.Checked;

            PublicVariable4CS.serverSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            IPAddress ip = IPAddress.Parse(PublicVariable4CS.ServerIP);
            IPEndPoint point = new IPEndPoint(ip, PublicVariable4CS.ServerPort);
            
            try {
                //绑定IP地址和端口号
                PublicVariable4CS.serverSocket.Connect(point);
                String s = "LOGIN|"+PublicVariable4CS.UserName + "|" + PublicVariable4CS.PassWord+"|"+"ver:1.2"+"\n";

                //发送登录信息
                PublicVariable4CS.serverSocket.Send(Encoding.UTF8.GetBytes(s));
                byte[] dat = new byte[2048];

                String data = System.Text.Encoding.UTF8.GetString(dat, 0, PublicVariable4CS.serverSocket.Receive(dat));
                //String[] datas = data.Split('★');
                //data = datas[1];
                if(data.IndexOf( @"LOGIN-RE|TRUE")>=0)
                {
                    String[] datas = data.Split('|');
                    String[] userDesc = datas[2].Split(':');
                    PublicVariable4CS.UserDesc = userDesc[1];
                    PublicVariable4CS.login = true;
                    this.Close();
                }else if (data.IndexOf(@"LOGIN-RE|FALSE") >= 0)
                {
                    MessageBox.Show("登录失败，请核对密码！\n");
                }
                else if (data.IndexOf(@"LOGIN-RE|ERROR_USERNAME")>= 0)
                {
                    MessageBox.Show("用户不存在！\n");
                }
                else
                {
                    String a = "服务器响应异常！" + data;
                    MessageBox.Show(a);
                }

            }
            catch(Exception ex)
            {
                MessageBox.Show("服务器连接错误,请检查服务器地址是否正确，并保证网络连接正常！\n" + ex);
            }
            ;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            String s = PublicVariable4CS.UserName + "|" + PublicVariable4CS.PassWord+"\n";
            PublicVariable4CS.serverSocket.Send(Encoding.UTF8.GetBytes(s));
        }

        private void Login_Load(object sender, EventArgs e)
        {

        }
    }
}
