using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace PortControllerClient.Forms
{
    public partial class WebForm : Form
    {

        public String user = "";
        public String pwd = "";
        public String userName = "";
        public String host = "http://127.0.0.1:8848";


        public WebForm()
        {
            InitializeComponent();
            //webBrowser1.Navigate("file:///C:/Pro/PortForwarding-/PortControllerClient/Debug/html/login/login/index.html");
            //webBrowser1.Navigate("https://ie.icoa.cn/");
            webBrowser1.Navigate(host+"/login/index.html");
            webBrowser1.ObjectForScripting = this;
        }

        Point mouseOff;//鼠标移动位置变量
        bool leftFlag;//标签是否为左键

        private void WebForm_Load_1(object sender, EventArgs e)
        {
            this.MouseDown += new MouseEventHandler(Form1_MouseDown);
            this.MouseMove += new MouseEventHandler(Form1_MouseMove);
            this.MouseUp += new MouseEventHandler(Form1_MouseUp);

            //webBrowser1.E
        }


        private void Form1_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                mouseOff = new Point(-e.X, -e.Y); //得到变量的值
                leftFlag = true;                  //点击左键按下时标注为true;
            }
        }


        private void Form1_MouseMove(object sender, MouseEventArgs e)
        {
            if (leftFlag)
            {
                Point mouseSet = Control.MousePosition;
                mouseSet.Offset(mouseOff.X, mouseOff.Y);  //设置移动后的位置
                Location = mouseSet;
            }
        }

        private void Form1_MouseUp(object sender, MouseEventArgs e)
        {
            if (leftFlag)
            {
                leftFlag = false;//释放鼠标后标注为false;
            }
        }

        private void small_Click(object sender, EventArgs e)
        {
            this.mainLogo.Visible = true;    //显示托盘图标
            this.Hide();    //隐藏窗口
        }

        private void exit_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("确认退出？\n可以点击右上角最小化按钮隐藏到系统托盘！", "", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
            {
                Application.Exit();
            }
        }

        private void mainLogo_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            this.Show();
        }

        public void saveUserMessage(String username)
        {
            //MessageBox.Show("hhh");
            user = webBrowser1.Document.GetElementById("username").GetAttribute("value");
            pwd = webBrowser1.Document.GetElementById("password").GetAttribute("value");
            userName = username;
            webBrowser1.Navigate(host + "/zwtp_1_dj/index.html");
            //MessageBox.Show(userName);
        }
        public void saveUser()
        {
            MessageBox.Show(userName);
        }
    }
}
