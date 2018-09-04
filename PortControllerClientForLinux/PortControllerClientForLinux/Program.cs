using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Runtime.InteropServices;
using System.Threading;
using fileTools;
using PortControllerClient;

namespace PortControllerClientForLinux
{
    class Program
    {
        
        static List<Hashtable> ipconfigList = new List<Hashtable>();

        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            String user = "";
            String pwd = "";
            PublicVariable4CS.ServerIP = INIhelp.GetValue("serverip");
            PublicVariable4CS.ServerPort = int.Parse(INIhelp.GetValue("serverport"));

            Console.WriteLine("请输入用户名!");
            PublicVariable4CS.UserName = Console.ReadLine();
            Console.WriteLine("请输入密码!");
            PublicVariable4CS.PassWord = Console.ReadLine();

            getLink();

            Console.WriteLine("请输入指令！输入 help 查看指令表");
            while (true)
            {
                String command = "";
                Console.Write("PCC>>:");
                command = Console.ReadLine();
                commandTable(command);
            }
        }


        static void commandTable(String command)
        {
            switch (command)
            {
                case "help":
                    help();
                    break;
                case  "h":
                    help();
                    break;
                case "list":
                    list();
                    break;
                case "add":
                    add();
                    break;
                case "delete":
                    delete();
                    break;
                case "start":
                    start();
                    break;
                case "":
                    break;
                default:
                    Console.WriteLine("无法识别为："+command+"的指令！");
                    break;

            }
        }

        static void help()
        {
            Console.WriteLine("");
            Console.WriteLine("help:\t\t显示所有指令");
            Console.WriteLine("h:\t\t显示所有指令");
            Console.WriteLine("list:\t\t显示所有转发规则");
            Console.WriteLine("add:\t\t新增转发规则");
            Console.WriteLine("delete:\t\t删除转发规则");
            Console.WriteLine("start:\t\t开启转发");
            Console.WriteLine("stop:\t\t关闭转发");
            Console.WriteLine("restart:\t重启转发");
            Console.WriteLine("");
        }

        static void list()
        {
            getLink();
            Console.WriteLine("");
            Console.WriteLine("代理地址\t代理端口\t目的地址\t目的端口");
            for (int i=0;i< ipconfigList.Count; i++)
            {
                Console.WriteLine(ipconfigList[i]["AG_IP"]+ "\t\t" + ipconfigList[i]["AG_PORT"] + "\t\t" + ipconfigList[i]["RM_IP"] + "\t" + ipconfigList[i]["RM_PORT"] + "");
            }
            Console.WriteLine("");
        }

        static void add()
        {
            String[] con = new String[4];
            Console.WriteLine("输入代理地址：");
            con[0] = Console.ReadLine();
            Console.WriteLine("输入代理端口：");
            con[1] = Console.ReadLine();
            Console.WriteLine("输入目的地址：");
            con[2] = Console.ReadLine();
            Console.WriteLine("输入目的端口：");
            con[3] = Console.ReadLine();

            Hashtable hashtable = new Hashtable();
            hashtable.Add("AG_IP", con[0]);
            hashtable.Add("AG_PORT", con[1]);
            hashtable.Add("RM_IP", con[2]);
            hashtable.Add("RM_PORT", con[3]);
            ipconfigList.Add(hashtable);
            saveConfig();
        }

        static void delete()
        {
            String con = "";
            Console.WriteLine("输入要删除的代理地址：");
            con = Console.ReadLine();
            for (int i=0;i< ipconfigList.Count; i++)
            {
                if ((string)ipconfigList[i]["AG_IP"]== con)
                {
                    ipconfigList.Remove(ipconfigList[i]);
                    saveConfig();
                    Console.WriteLine("删除成功");
                    return;
                }
            }
            Console.WriteLine("改转发关系不存在！");
           
        }


        static void start()
        {
            for (int i = 0; i < ipconfigList.Count; i++)
            {
                try
                {
                    object listenPort = (object)(new String[] { ipconfigList[i]["AG_PORT"] + "", ipconfigList[i]["RM_IP"] + ":" + ipconfigList[i]["RM_PORT"] });
                    ThreadPool.QueueUserWorkItem(new WaitCallback(localListener1), listenPort);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("启动监听程序时发生错误：" + ex.ToString());
                }

            }

        }

        static void getLink()
        {
            string link_str = INIhelp.GetValue("link");//读取历史转发记录并自动填写
            ipconfigList = new List<Hashtable>();

            if (link_str.Length > 0)
            {
                string[] links = link_str.Split('|');
                for (int i = 0; i < links.Length; i++)
                {
                    Hashtable hashtable = new Hashtable();

                    string[] tmp = links[i].Split(';');
                    string[] tmptmp = tmp[0].Split(':');


                    hashtable.Add("AG_IP", tmptmp[0]);
                    hashtable.Add("AG_PORT", tmptmp[1]);

                    tmptmp = tmp[1].Split(':');

                    hashtable.Add("RM_IP", tmptmp[0]);
                    hashtable.Add("RM_PORT", tmptmp[1]);

                    ipconfigList.Add(hashtable);
                }

            }
        }

        public static void saveConfig()
        {
            String links = "";
            for (int i=0; i< ipconfigList.Count; i++)
            {
                links+=(ipconfigList[i]["AG_IP"] + ":" + ipconfigList[i]["AG_PORT"] + ";" + ipconfigList[i]["RM_IP"] + ":" + ipconfigList[i]["RM_PORT"]);
                if (i != (ipconfigList.Count - 1))
                    links += "|";
            }
            INIhelp.SetValue("link", links);
        }




        /// <summary>
        /// 本地监听线程（监听本地请求并发送建立端口转发申请）
        /// </summary>
        public static void localListener1(object obj)
        {
            try
            {
                String[] mes = (String[])obj;
                int port = int.Parse(mes[0]);
                String remoteAddress = mes[1];
                TcpListener tl = new TcpListener(port);
                while (true)
                {
                    try
                    {
                        Hashtable sendTable = new Hashtable();
                        tl.Start();
                        TcpClient tc1 = tl.AcceptTcpClient();//这里是等待数据再执行下边，不会100%占用cpu
                        TcpClient tc2 = new TcpClient(PublicVariable4CS.ServerIP, PublicVariable4CS.ServerPort);

                        NetworkStream ns2 = tc2.GetStream();
                        sendTable.Add("VER", PublicVariable4CS.ver);
                        sendTable.Add("TYPE", "CONNECT");
                        sendTable.Add("USER", PublicVariable4CS.UserName);
                        sendTable.Add("PWD", PublicVariable4CS.PassWord);

                        string[] tmp = remoteAddress.Split(':');

                        sendTable.Add("TOTAL_IP", tmp[0]);
                        sendTable.Add("TOTAL_PORT", tmp[1]);

                        byte[] byteArray = System.Text.Encoding.UTF8.GetBytes(PublicVariable4CS.setMessages(sendTable));
                        ns2.Write(byteArray, 0, byteArray.Length);

                        byte[] bt = new byte[10240];
                        int count = ns2.Read(bt, 0, bt.Length);

                        string reMessage = System.Text.Encoding.UTF8.GetString(bt);
                        Hashtable reMesTable = PublicVariable4CS.getMessages(reMessage);
                        if ((string)reMesTable["TYPE"] == "CONNECT-RE")
                        {
                            if ((string)reMesTable["RE_ANSWER"] == "TRUE")
                            {
                                object obj1 = (object)(new TcpClient[] { tc1, tc2 });
                                object obj2 = (object)(new TcpClient[] { tc2, tc1 });
                                ThreadPool.QueueUserWorkItem(new WaitCallback(transfer), obj1);
                                ThreadPool.QueueUserWorkItem(new WaitCallback(transfer), obj2);
                            }
                            else if ((string)reMesTable["RE_ANSWER"] == "FALSE")
                            {
                                PublicVariable4CS.errorMessage("端口转发被拒绝：" + (string)reMesTable["RE_MESSAGE"]);
                            }
                            else
                            {
                                throw new Exception("服务器反馈数据异常！");
                            }
                        }
                        else
                        {
                            PublicVariable4CS.errorMessage("错误的返回类型" + (string)reMesTable["TYPE"]);
                        }
                    }
                    catch (Exception ex)
                    {
                        PublicVariable4CS.errorMessage(ex.ToString());
                    }


                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
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
                int count = 0;
                try
                {
                    //这里必须try catch，否则连接一旦中断程序就崩溃了，要是弹出错误提示让机主看见那就囧了
                    byte[] bt = new byte[30720000];
                    count = ns1.Read(bt, 0, bt.Length);
                    ns2.Write(bt, 0, count);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                    ns1.Dispose();
                    ns2.Dispose();
                    tc1.Close();
                    tc2.Close();
                    break;
                }
                end = DateTime.Now.Subtract(DateTime.Parse("1970-1-1")).TotalMilliseconds;
                if ((end - start) <= 1 || count == 0)
                {
                    if (overSpeedCount >= 500)
                    {
                        break;//无效线程死循环达到500次自动退出
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
            overSpeedCount--;
        }
    }
}
