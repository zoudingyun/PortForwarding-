using PortControllerClient.Forms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using PortControllerClient.Threads;
using fileTools;
using System.IO;
using System.Diagnostics;

namespace PortControllerClient
{
    public partial class mainForm : Form
    {
        public List<TcpListener> tcpListeners = new List<TcpListener>();

        public mainForm()
        {
            InitializeComponent();
            Login login = new Login();
            login.Visible = false;
            login.ShowDialog();
            this.userDesc.Text = PublicVariable4CS.UserDesc;
            if (!PublicVariable4CS.login)
            {
                Environment.Exit(0);
            }

            string link_str = INIhelp.GetValue("link");
            if (link_str.Length > 0)
            {
                string[] links = link_str.Split('|');
                for (int i=0;i< links.Length; i++)
                {
                    this.portList.Rows.Add();
                    DataGridViewRow dgvr = this.portList.Rows[i];
                    string[] tmp = links[i].Split(';');
                    string[] tmptmp = tmp[0].Split(':');
                    
                    dgvr.Cells[0].Value = tmptmp[0];
                    dgvr.Cells[1].Value = tmptmp[1];

                    tmptmp = tmp[1].Split(':');

                    dgvr.Cells[2].Value = tmptmp[0];
                    dgvr.Cells[3].Value = tmptmp[1];
                }

            }

            /*this.portList.Rows.Add();


            DataGridViewRow dgvr = this.portList.Rows[0];
            dgvr.Cells[0].Value = 3;
            dgvr.Cells[1].Value = "直接添加一行";
            dgvr.Cells[2].Value = 3;
            dgvr.Cells[3].Value = "直接添加一行";*/
        }


        private void startPort_Click(object sender, EventArgs e)
        {
            if (PublicVariable4CS.portOpen)
            {
                PublicVariable4CS.portOpen = !PublicVariable4CS.portOpen;
                this.startPort.Visible = false;
            }
            else
            {
                PublicVariable4CS.portOpen = !PublicVariable4CS.portOpen;
                this.startPort.Visible = false;


                int ret = this.portList.Rows.Count;
                if (ret > 0)
                {
                    for(int i=0; i < ret-1; i++)
                    {
                        DataGridViewRow tmpRow = this.portList.Rows[i];
                        int[] tmpPorts =new int[2];
                        String[] tmpIPs = new String[2];
                        try
                        {
                            tmpIPs[0] = tmpRow.Cells[0].Value.ToString();
                            tmpIPs[1] = tmpRow.Cells[2].Value.ToString();

                            tmpPorts[0] = int.Parse(tmpRow.Cells[1].Value.ToString());
                            tmpPorts[1] = int.Parse(tmpRow.Cells[3].Value.ToString());
                        }catch(Exception ex)
                        {
                            MessageBox.Show("第"+ (i+1) +"行不符合规范！");
                            return;
                        }

                        try {
                            object listenPort = (object)(new String[] { tmpPorts[0]+"", tmpIPs[1] +":"+ tmpPorts[1] });
                            ThreadPool.QueueUserWorkItem(new WaitCallback(localListener1), listenPort);
                        }
                        catch(Exception ex)
                        {
                            MessageBox.Show("启动监听程序时发生错误："+ex.ToString());
                        }
                        
                    }
                    string links = "";
                    string host = "";
                    for (int i = 0; i < ret - 1; i++)
                    {
                        try
                        {
                            DataGridViewRow tmpRow = this.portList.Rows[i];
                            links += tmpRow.Cells[0].Value.ToString();
                            links += ":";
                            links += tmpRow.Cells[1].Value.ToString();
                            links += ";";
                            links += tmpRow.Cells[2].Value.ToString();
                            links += ":";
                            links += tmpRow.Cells[3].Value.ToString();
                            if(i!=(ret-2))
                                links += "|";

                            host +=("127.0.0.1:"+ tmpRow.Cells[0].Value.ToString()+"|");
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("第" + (i + 1) + "行不符合规范！");
                            return;
                        }

                    }
                    updateHosts(host);
                    INIhelp.SetValue("link", links);

                }

            }
        }


        /// <summary>
        /// 本地监听线程（监听本地请求并发送建立端口转发申请）
        /// </summary>
        public void localListener1(object obj)
        {
            try {
                String[] mes = (String[])obj;
                int port = int.Parse(mes[0]);
                String remoteAddress = mes[1];
                TcpListener tl = new TcpListener(port);
                while (true)
                {
                    try
                    {
                        tl.Start();
                        TcpClient tc1 = tl.AcceptTcpClient();//这里是等待数据再执行下边，不会100%占用cpu
                        TcpClient tc2 = new TcpClient(PublicVariable4CS.ServerIP, PublicVariable4CS.ServerPort);

                        NetworkStream ns2 = tc2.GetStream();
                        String sendStr = "CONNECT|" + PublicVariable4CS.UserName + "|" + PublicVariable4CS.PassWord + "|" + remoteAddress + "\n";

                        byte[] byteArray = System.Text.Encoding.UTF8.GetBytes(sendStr);
                        ns2.Write(byteArray, 0, byteArray.Length);

                        byte[] bt = new byte[10240];
                        int count = ns2.Read(bt, 0, bt.Length);

                        string reMessage = System.Text.Encoding.UTF8.GetString(bt);

                        if(reMessage.IndexOf(@"CONNECT-RE|TRUE") >= 0)
                        {
                            object obj1 = (object)(new TcpClient[] { tc1, tc2 });
                            object obj2 = (object)(new TcpClient[] { tc2, tc1 });
                            ThreadPool.QueueUserWorkItem(new WaitCallback(transfer), obj1);
                            ThreadPool.QueueUserWorkItem(new WaitCallback(transfer), obj2);
                        }
                        else if(reMessage.IndexOf(@"CONNECT-RE|FALSE") >= 0)
                        {
                            String[] tmpDatas = reMessage.Split('|');
                            MessageBox.Show("端口转发被拒绝：" + tmpDatas[2]);
                        }
                        else
                        {
                            throw new Exception("服务器反馈数据异常！");
                        }
                    }catch(Exception ex)
                    {
                        MessageBox.Show(ex.ToString());
                    }


                }
            }catch(Exception ex)
            {
                Console.WriteLine(ex);
            }
        }

        
        
        /// <summary>
        /// 测试方法
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void addPort_Click(object sender, EventArgs e)
        {
            Socket clientSocketForUser = PublicVariable4CS.clientSocketForUser;
            String clientSocketForServer = PublicVariable4CS.targetAddr;
            PublicVariable4CS.clientSocketLocked = false;

                try
                {


                    TcpListener tl = new TcpListener(10000);
                    tl.Start();
                    while (true) { 


                   
                        TcpClient tc1 = tl.AcceptTcpClient();//这里是等待数据再执行下边，不会100%占用cpu
                        //TcpClient tc2 = new TcpClient("172.30.200.50", 3389);
                        TcpClient tc2 = new TcpClient("127.0.0.1", 10002);
                        String s = "CONNECT|" + "zdy" + "|" + "dlam520313" + "|" + "172.30.200.51:3389" + "\n";
                        byte[] byteArray = System.Text.Encoding.UTF8.GetBytes(s);
                        NetworkStream ns2 = tc2.GetStream();
                        ns2.Write(byteArray, 0, byteArray.Length);

                        byte[] bt = new byte[10240];
                        int count = ns2.Read(bt, 0, bt.Length);

                        string str = System.Text.Encoding.UTF8.GetString(bt);


                        object obj1 = (object)(new TcpClient[] { tc1, tc2 });
                        object obj2 = (object)(new TcpClient[] { tc2, tc1 });
                        ThreadPool.QueueUserWorkItem(new WaitCallback(transfer), obj1);
                        ThreadPool.QueueUserWorkItem(new WaitCallback(transfer), obj2);


                    }


                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
            

        }




        /// <summary>
        /// 工作子线程高级版（端口转发建立后，实际进行数据转发的线程）
        /// </summary>
        public static void transfer(object obj)
        {
            TcpClient tc1 = ((TcpClient[])obj)[0];
            TcpClient tc2 = ((TcpClient[])obj)[1];
            NetworkStream ns1 = tc1.GetStream();
            NetworkStream ns2 = tc2.GetStream();
            double start = 0;
            double end = 0;
            Boolean overSpeed = false;
            int overSpeedCount = 0;
            while (true)
            {
                start = DateTime.Now.Subtract(DateTime.Parse("1970-1-1")).TotalMilliseconds;
                try
                {
                    //这里必须try catch，否则连接一旦中断程序就崩溃了，要是弹出错误提示让机主看见那就囧了
                    byte[] bt = new byte[30720000];
                    int count = ns1.Read(bt, 0, bt.Length);
                    ns2.Write(bt, 0, count);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("子线程异常："+ex.ToString());
                    ns1.Dispose();
                    ns2.Dispose();
                    tc1.Close();
                    tc2.Close();
                    break;
                }
                end = DateTime.Now.Subtract(DateTime.Parse("1970-1-1")).TotalMilliseconds;
                if ((end - start) <= 1)
                {
                    if (overSpeedCount >= 500)
                    {
                        return;//无效线程死循环达到500次自动退出
                    }
                    else
                    {
                        if (!overSpeed)
                        {
                            overSpeed = true;
                            overSpeedCount = 1;
                        }
                        else
                        {
                            overSpeedCount++;
                        }
                    }
                }
            }
        }

        private static int updateHosts(string key)
        {
            string path = @"C:\WINDOWS\system32\drivers\etc\hosts";
            //通常情况下这个文件是只读的，所以写入之前要取消只读
            //File.SetAttributes(path, File.GetAttributes(path) & (~FileAttributes.ReadOnly));//取消只读
            if (key.Length <= 0)
            {
                return 0;
            }
            //1.创建文件流
            FileStream fs = new FileStream(path, FileMode.OpenOrCreate);
            //2.创建写入器
            StreamWriter sw = new StreamWriter(fs, Encoding.UTF8);
            //3.开始写入
            bool result = false;//标识是否写入成功

            string[] hosts = key.Split('|');
            List<string[]> hosts_list = new List<string[]>();
            for(int i=0;i<hosts.Length; i++)
            {
                string[] tmp = hosts[i].Split(':');
                if (tmp.Length!=2)
                {
                    continue;
                }
                hosts_list.Add(tmp);
            }

            StreamReader streamReader = new StreamReader(fs);
            string line = "";
            while ((line = streamReader.ReadLine()) != null)
            {
                

                for(int i=0;i< hosts_list.Count; i++)
                {
                    //line就是一行一行的文本
                    if (line.IndexOf(hosts_list[i][1]) >= 0)
                    {
                        hosts_list.Remove(hosts_list[i]);
                    }
                }
                if (hosts_list.Count<=0)
                {
                    return 0;
                }
            }


            try
            {
                //StringBuilder sb = new StringBuilder();
                //sb.Append(hosts_list[i][0] + " ");//域名
                sw.WriteLine("");
                for (int i = 0; i < hosts_list.Count; i++)
                {
                    sw.WriteLine(hosts_list[i][0]+" "+ hosts_list[i][1]);
                }
                
                
                result = true;
            }
            catch (Exception ex)
            {
                result = false;
            }
            finally
            {
                //4.关闭写入器
                if (sw != null)
                {
                    sw.Close();
                }
                //5.关闭文件流
                if (fs != null)
                {
                    fs.Close();
                }

                Process p = new Process();
                            //设置要启动的应用程序
                 p.StartInfo.FileName = "cmd.exe";
                                //是否使用操作系统shell启动
                 p.StartInfo.UseShellExecute = false;
                                 // 接受来自调用程序的输入信息
                 p.StartInfo.RedirectStandardInput = true;
                                 //输出信息
                 p.StartInfo.RedirectStandardOutput = true;
                                 // 输出错误
                p.StartInfo.RedirectStandardError = true;
                                 //不显示程序窗口
                 p.StartInfo.CreateNoWindow = true;
                                //启动程序
                 p.Start();

                    //向cmd窗口发送输入信息             
                 p.StandardInput.WriteLine("ipconfig /flushdns&exit");
                
                 p.StandardInput.AutoFlush = true;
                p.WaitForExit();
                p.Close();



            }
            if (result == true)
            {
                
                //File.SetAttributes(path, File.GetAttributes(path) | FileAttributes.ReadOnly);//设置只读
                return 0;
            }
            else
            {
                return -1;
            }
        }




        private void login_Load(object sender, EventArgs e)
        {

        }

        private void login_Load_1(object sender, EventArgs e)
        {

        }

        private void mainForm_Load(object sender, EventArgs e)
        {

        }

       
    }
}
