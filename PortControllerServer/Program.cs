using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Data.OleDb;
using OracleDemo;
using Oracle.ManagedDataAccess.Client;
using System.Data;
using fileTools;
using System.Collections;

namespace PortControllerServer
{
    class Program
    {
        static int threadCount = 0;
        static long bits = 0;
        static void Main(string[] args)
        {
            int serverPort = int.Parse(INIhelp.GetValue("port"));
            Boolean offline = false;
            if (INIhelp.GetValue("offline") == "true")
            {
                offline = true;
            }

            TcpListener tl = new TcpListener(serverPort);
            tl.Start();
            
            while (true)
            {
                try
                {
                    TcpClient tc1 = tl.AcceptTcpClient();//这里是等待数据再执行下边，不会100%占用cpu
                    Console.WriteLine("线程数："+ threadCount);

                    NetworkStream ns1 = tc1.GetStream();
                    byte[] bt = new byte[10240];
                    int count = ns1.Read(bt, 0, bt.Length);
                    string str = System.Text.Encoding.UTF8.GetString(bt);

                    Hashtable message = getMessages(str);
                    Hashtable reMessage = new Hashtable();
                    if ((string)message["TYPE"] == "LOGIN")
                    {
                        float ver = float.Parse((string)message["VER"]);
                        if(ver< float.Parse(INIhelp.GetValue("ver")))
                        {
                            reMessage.Add("TYPE", "LOGIN-RE");
                            reMessage.Add("RE_ANSWER", "FALSE");
                            reMessage.Add("RE_MESSAGE", "客户端版本过低！当前版本 v"+ ver + "，请更新至 v"+ INIhelp.GetValue("ver"));
                            byte[] byteArray = System.Text.Encoding.UTF8.GetBytes(setMessages(reMessage));
                            ns1.Write(byteArray, 0, byteArray.Length);
                            continue;
                        }

                        if (!offline)
                        {
                            DataTable dt = queryUserLoginMessage((string)message["USER"]);
                            if (dt.Rows.Count <= 0)
                            {
                                reMessage.Add("TYPE", "LOGIN-RE");
                                reMessage.Add("RE_ANSWER", "FALSE");
                                reMessage.Add("RE_MESSAGE", "用户不存在！");
                                byte[] byteArray = System.Text.Encoding.UTF8.GetBytes(setMessages(reMessage));
                                ns1.Write(byteArray, 0, byteArray.Length);
                                continue;
                            }
                            String pwd = dt.Rows[0]["PASSWORD"].ToString();
                            String username = dt.Rows[0]["userdesc"].ToString();
                            String inherit = dt.Rows[0]["inherit"].ToString();
                            if ((string)message["PWD"] == pwd)
                            {
                                reMessage.Add("TYPE", "LOGIN-RE");
                                reMessage.Add("RE_ANSWER", "TRUE");
                                reMessage.Add("USERDESC", username);
                                if (inherit == "admin")
                                {
                                    reMessage.Add("INHERIT", "ADMIN");
                                }
                                else
                                {
                                    reMessage.Add("INHERIT", "USER");
                                }
                                byte[] byteArray = System.Text.Encoding.UTF8.GetBytes(setMessages(reMessage));
                                ns1.Write(byteArray, 0, byteArray.Length);
                            }
                            else
                            {
                                reMessage.Add("TYPE", "LOGIN-RE");
                                reMessage.Add("RE_ANSWER", "FALSE");
                                reMessage.Add("RE_MESSAGE", "密码错误！");
                                byte[] byteArray = System.Text.Encoding.UTF8.GetBytes(setMessages(reMessage));
                                ns1.Write(byteArray, 0, byteArray.Length);
                            }
                            continue;
                        }
                        else
                        {
                            if ((string)message["USER"] == "zdkj")
                            {
                                if ((string)message["PWD"] == "scyy@68669820")
                                {
                                    reMessage.Add("TYPE", "LOGIN-RE");
                                    reMessage.Add("RE_ANSWER", "TRUE");
                                    reMessage.Add("USERDESC", "离线管理账户");
                                    reMessage.Add("INHERIT", "ADMIN");
                                    byte[] byteArray = System.Text.Encoding.UTF8.GetBytes(setMessages(reMessage));
                                    ns1.Write(byteArray, 0, byteArray.Length);
                                }
                            }
                            else
                            {
                                reMessage.Add("TYPE", "LOGIN-RE");
                                reMessage.Add("RE_ANSWER", "FALSE");
                                reMessage.Add("RE_MESSAGE", "用户不存在！");
                                byte[] byteArray = System.Text.Encoding.UTF8.GetBytes(setMessages(reMessage));
                                ns1.Write(byteArray, 0, byteArray.Length);
                                continue;
                            }
                            continue;
                        }
                    }
                    else if ((string)message["TYPE"] == "CONNECT")
                    {
                        float ver = float.Parse((string)message["VER"]);
                        if (ver < float.Parse(INIhelp.GetValue("ver")))
                        {
                            reMessage.Add("TYPE", "CONNECT-RE");
                            reMessage.Add("RE_ANSWER", "FALSE");
                            reMessage.Add("RE_MESSAGE", "客户端版本过低！当前版本 v" + ver + "，请更新至 v" + INIhelp.GetValue("ver"));
                            byte[] byteArray = System.Text.Encoding.UTF8.GetBytes(setMessages(reMessage));
                            ns1.Write(byteArray, 0, byteArray.Length);
                            continue;
                        }

                        if (!offline)
                        {
                            DataTable dt = queryUserLicense((string)message["USER"], (string)message["PWD"], (string)message["TOTAL_IP"], (string)message["TOTAL_PORT"]);
                            if (int.Parse(dt.Rows[0]["License"].ToString()) <= 0)
                            {
                                reMessage.Add("TYPE", "CONNECT-RE");
                                reMessage.Add("RE_ANSWER", "FALSE");
                                reMessage.Add("RE_MESSAGE", "权限不足");
                                byte[] byteArray = System.Text.Encoding.UTF8.GetBytes(setMessages(reMessage));
                                ns1.Write(byteArray, 0, byteArray.Length);
                            }
                            else
                            {
                                try
                                {
                                    TcpClient tc2 = new TcpClient((string)message["TOTAL_IP"], int.Parse((string)message["TOTAL_PORT"]));
                                    ThreadMessage threadMessage1 = new ThreadMessage();
                                    ThreadMessage threadMessage2 = new ThreadMessage();
                                    threadMessage1.TOTAL_IP = (string)message["TOTAL_IP"];
                                    threadMessage2.TOTAL_IP = (string)message["TOTAL_IP"];
                                    threadMessage1.TOTAL_PORT = (string)message["TOTAL_PORT"];
                                    threadMessage2.TOTAL_PORT = (string)message["TOTAL_PORT"];

                                    reMessage.Add("TYPE", "CONNECT-RE");
                                    reMessage.Add("RE_ANSWER", "TRUE");
                                    byte[] byteArray = System.Text.Encoding.UTF8.GetBytes(setMessages(reMessage));
                                    ns1.Write(byteArray, 0, byteArray.Length);

                                    
                                    threadMessage1.tc1 = tc1;
                                    threadMessage1.tc2 = tc2;
                                    threadMessage2.tc1 = tc2;
                                    threadMessage2.tc2 = tc1;
                                    threadMessage1.type = "user";
                                    threadMessage2.type = "target";
                                    //object obj1 = (object)(new TcpClient[] { tc1, tc2 });
                                    //object obj2 = (object)(new TcpClient[] { tc2, tc1 });
                                    object obj1 = (object)threadMessage1;
                                    object obj2 = (object)threadMessage2;
                                    ThreadPool.QueueUserWorkItem(new WaitCallback(transfer), obj1);
                                    ThreadPool.QueueUserWorkItem(new WaitCallback(transfer), obj2);
                                }
                                catch (Exception ex)
                                {
                                    reMessage.Add("TYPE", "CONNECT-RE");
                                    reMessage.Add("RE_ANSWER", "FALSE");
                                    reMessage.Add("RE_MESSAGE", ex.Message);
                                    byte[] byteArray1 = System.Text.Encoding.UTF8.GetBytes(setMessages(reMessage));
                                    ns1.Write(byteArray1, 0, byteArray1.Length);
                                    continue;
                                }
                                //TcpClient tc2 = new TcpClient("172.30.200.50", 3389);
                            }

                            continue;
                        }
                        else
                        {
                            try
                            {
                                TcpClient tc2 = new TcpClient((string)message["TOTAL_IP"], int.Parse((string)message["TOTAL_PORT"]));

                                reMessage.Add("TYPE", "CONNECT-RE");
                                reMessage.Add("RE_ANSWER", "TRUE");
                                byte[] byteArray = System.Text.Encoding.UTF8.GetBytes(setMessages(reMessage));
                                ns1.Write(byteArray, 0, byteArray.Length);

                                object obj1 = (object)(new TcpClient[] { tc1, tc2 });
                                object obj2 = (object)(new TcpClient[] { tc2, tc1 });
                                ThreadPool.QueueUserWorkItem(new WaitCallback(transfer), obj1);
                                ThreadPool.QueueUserWorkItem(new WaitCallback(transfer), obj2);
                            }
                            catch (Exception ex)
                            {
                                reMessage.Add("TYPE", "CONNECT-RE");
                                reMessage.Add("RE_ANSWER", "FALSE");
                                reMessage.Add("RE_MESSAGE", ex.Message);
                                byte[] byteArray1 = System.Text.Encoding.UTF8.GetBytes(setMessages(reMessage));
                                ns1.Write(byteArray1, 0, byteArray1.Length);
                                continue;
                            }
                            continue;
                        }
                    }
                    else if ((string)message["TYPE"] == "CHANGPWD")
                    {
                        float ver = float.Parse((string)message["VER"]);
                        if (ver < float.Parse(INIhelp.GetValue("ver")))
                        {
                            reMessage.Add("TYPE", "CHANGPWD-RE");
                            reMessage.Add("RE_ANSWER", "FALSE");
                            reMessage.Add("RE_MESSAGE", "客户端版本过低！当前版本 v" + ver + "，请更新至 v" + INIhelp.GetValue("ver"));
                            byte[] byteArray = System.Text.Encoding.UTF8.GetBytes(setMessages(reMessage));
                            ns1.Write(byteArray, 0, byteArray.Length);
                            continue;
                        }

                        DataTable dt = queryUserLoginMessage((string)message["USER"]);
                        if (dt.Rows.Count <= 0)
                        {
                            reMessage.Add("TYPE", "CHANGPWD-RE");
                            reMessage.Add("RE_ANSWER", "FALSE");
                            reMessage.Add("RE_MESSAGE", "用户不存在！");
                            byte[] byteArray = System.Text.Encoding.UTF8.GetBytes(setMessages(reMessage));
                            ns1.Write(byteArray, 0, byteArray.Length);
                            continue;
                        }
                        String pwd = dt.Rows[0]["PASSWORD"].ToString();
                        String username = dt.Rows[0]["userdesc"].ToString();
                        String inherit = dt.Rows[0]["inherit"].ToString();
                        if ((string)message["PWD"] == pwd)
                        {
                            updateUserPwd((string)message["USER"], (string)message["NEWPWD"]);
                            reMessage.Add("TYPE", "CHANGPWD-RE");
                            reMessage.Add("RE_ANSWER", "TRUE");
                            byte[] byteArray = System.Text.Encoding.UTF8.GetBytes(setMessages(reMessage));
                            ns1.Write(byteArray, 0, byteArray.Length);
                        }
                        else
                        {
                            reMessage.Add("TYPE", "CHANGPWD-RE");
                            reMessage.Add("RE_ANSWER", "FALSE");
                            reMessage.Add("RE_MESSAGE", "原密码错误！");
                            byte[] byteArray = System.Text.Encoding.UTF8.GetBytes(setMessages(reMessage));
                            ns1.Write(byteArray, 0, byteArray.Length);
                        }
                    }
                    else if ((string)message["TYPE"] == "CHANGENAME")
                    {
                        float ver = float.Parse((string)message["VER"]);
                        if (ver < float.Parse(INIhelp.GetValue("ver")))
                        {
                            reMessage.Add("TYPE", "CHANGENAME-RE");
                            reMessage.Add("RE_ANSWER", "FALSE");
                            reMessage.Add("RE_MESSAGE", "客户端版本过低！当前版本 v" + ver + "，请更新至 v" + INIhelp.GetValue("ver"));
                            byte[] byteArray = System.Text.Encoding.UTF8.GetBytes(setMessages(reMessage));
                            ns1.Write(byteArray, 0, byteArray.Length);
                            continue;
                        }

                        DataTable dt = queryUserLoginMessage((string)message["USER"]);
                        if (dt.Rows.Count <= 0)
                        {
                            reMessage.Add("TYPE", "CHANGENAME-RE");
                            reMessage.Add("RE_ANSWER", "FALSE");
                            reMessage.Add("RE_MESSAGE", "用户不存在！");
                            byte[] byteArray = System.Text.Encoding.UTF8.GetBytes(setMessages(reMessage));
                            ns1.Write(byteArray, 0, byteArray.Length);
                            continue;
                        }
                        String pwd = dt.Rows[0]["PASSWORD"].ToString();
                        String username = dt.Rows[0]["userdesc"].ToString();
                        String inherit = dt.Rows[0]["inherit"].ToString();
                        if ((string)message["PWD"] == pwd)
                        {
                            updateUserName((string)message["USER"], (string)message["NEWDESC"]);
                            reMessage.Add("TYPE", "CHANGENAME-RE");
                            reMessage.Add("RE_ANSWER", "TRUE");
                            byte[] byteArray = System.Text.Encoding.UTF8.GetBytes(setMessages(reMessage));
                            ns1.Write(byteArray, 0, byteArray.Length);
                        }
                        else
                        {
                            reMessage.Add("TYPE", "CHANGENAME-RE");
                            reMessage.Add("RE_ANSWER", "FALSE");
                            reMessage.Add("RE_MESSAGE", "原密码错误！");
                            byte[] byteArray = System.Text.Encoding.UTF8.GetBytes(setMessages(reMessage));
                            ns1.Write(byteArray, 0, byteArray.Length);
                        }
                    }
                    else if ((string)message["TYPE"] == "RESET-USER-PWD")
                    {
                        float ver = float.Parse((string)message["VER"]);
                        if (ver < float.Parse(INIhelp.GetValue("ver")))
                        {
                            reMessage.Add("TYPE", "RESET-USER-PWD-RE");
                            reMessage.Add("RE_ANSWER", "FALSE");
                            reMessage.Add("RE_MESSAGE", "客户端版本过低！当前版本 v" + ver + "，请更新至 v" + INIhelp.GetValue("ver"));
                            byte[] byteArray = System.Text.Encoding.UTF8.GetBytes(setMessages(reMessage));
                            ns1.Write(byteArray, 0, byteArray.Length);
                            continue;
                        }

                        DataTable dt = queryUserLoginMessage((string)message["USER"]);
                        if (dt.Rows.Count <= 0)
                        {
                            reMessage.Add("TYPE", "RESET-USER-PWD-RE");
                            reMessage.Add("RE_ANSWER", "FALSE");
                            reMessage.Add("RE_MESSAGE", "用户不存在！");
                            byte[] byteArray = System.Text.Encoding.UTF8.GetBytes(setMessages(reMessage));
                            ns1.Write(byteArray, 0, byteArray.Length);
                            continue;
                        }
                        String pwd = dt.Rows[0]["PASSWORD"].ToString();
                        String username = dt.Rows[0]["userdesc"].ToString();
                        String inherit = dt.Rows[0]["inherit"].ToString();
                        if ((string)message["PWD"] == pwd)
                        {
                            updateTargetUserPwd((string)message["TARGET-USER"]);
                            reMessage.Add("TYPE", "RESET-USER-PWD-RE");
                            reMessage.Add("RE_ANSWER", "TRUE");
                            byte[] byteArray = System.Text.Encoding.UTF8.GetBytes(setMessages(reMessage));
                            ns1.Write(byteArray, 0, byteArray.Length);
                        }
                        else
                        {
                            reMessage.Add("TYPE", "CHANGENAME-RE");
                            reMessage.Add("RE_ANSWER", "FALSE");
                            reMessage.Add("RE_MESSAGE", "你的密码错误！");
                            byte[] byteArray = System.Text.Encoding.UTF8.GetBytes(setMessages(reMessage));
                            ns1.Write(byteArray, 0, byteArray.Length);
                        }
                    }
                    else
                    {
                        continue;
                    }


                }
                catch (Exception ex)
                {
                    errorMessage(ex);
                }
            }


        }



        /// <summary>
        /// 工作子线程高级版（端口转发建立后，实际进行数据转发的线程）
        /// </summary>
        public static void transfer(object obj)
        {
            ThreadMessage threadMessage = (ThreadMessage)obj;
            try
            {
                threadCount++;
                //TcpClient tc1 = ((TcpClient[])obj)[0];
                //TcpClient tc2 = ((TcpClient[])obj)[1];
                TcpClient tc1 = threadMessage.tc1;
                TcpClient tc2 = threadMessage.tc2;
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
                    byte[] bt = new byte[3000000];

                    System.DateTime currentTime = new System.DateTime();
                    currentTime = System.DateTime.Now;
                    string strY = currentTime.ToString();

                    try
                    {
                        
                        //=============
                        //这里必须try catch，否则连接一旦中断程序就崩溃了，要是弹出错误提示让机主看见那就囧了
                        count = ns1.Read(bt, 0, bt.Length);
                        ns2.Write(bt, 0, count);
                        //==============
                        if(bits< count)
                        {
                            bits = count;
                        }
                        Console.WriteLine(strY + " : " + threadMessage.type + "  length：" + bits + "  tc1:"+ tc1.Connected + "  tc2:" + tc2.Connected);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(threadMessage.type+"线程<<<<<<<<<<<<<<" +ex.ToString());
                        Console.WriteLine(strY + " : " + threadMessage.type + "  length：" + bits + "  tc1:" + tc1.Connected + "  tc2:" + tc2.Connected);
                        threadCount--;
                        break;
                    }
                    end = DateTime.Now.Subtract(DateTime.Parse("1970-1-1")).TotalMilliseconds;
                    if ((end - start) <= 1 || count == 0)
                    {
                        if (overSpeedCount >= 50)
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
                threadCount--;
            }
            catch(Exception ex)
            {
                Console.WriteLine(threadMessage.type + "线程无法修复的错误<<<<<<<<<<<<<<" + ex.ToString());
            }
        }


        public static DataTable queryUserLoginMessage(String user)
        {
            String sql = @"select a.userid as username
            , a.username as userdesc
            , a.password as password
            , a.inherit as inherit
              from t_base_op_user a
             where a.userid = '" + user+ @"'
             group by a.userid, a.username, a.password,a.inherit";

            return OracleHelper.ExecuteDataTable(sql, new OracleParameter(":id", 1));
        }


        public static DataTable queryUserLicense(String user,String pwd,String ip,String port)
        {
            String sql = @"select count(*) as License
                              from 
                              (
                             select t2.userid, t2.username,t2.password,t1.ip,t1.port
                              from (select tt.userid, tt.inherit, tt.password, tt.ip, tt.port
                                      from t_base_op_user tt
                                      left join t_base_op_user tt2
                                        on tt.inherit = tt2.userid
                                       and tt.type = 'user'
                                       and tt2.type = 'group') t1,
                                   (select * from t_base_op_user userlist where userlist.type = 'user') t2
                             where t2.userid = t1.userid or t2.inherit = t1.userid
    
                              )
                               t
                             where t.userid = '" + user + @"'
                               and t.password = '"+ pwd + @"'
                               and (t.ip = '" + ip + @"' or t.ip = '*')
                               and (t.port = '" + port + @"'
                                 or t.port = '*')";

            return OracleHelper.ExecuteDataTable(sql, new OracleParameter(":id", 1));
        }

        /// <summary>
        /// 更新密码
        /// </summary>
        /// <param name="user"></param>
        /// <param name="pwd"></param>
        /// <returns></returns>
        public static int updateUserPwd(String user, String pwd)
        {
            String sql = @"update t_base_op_user t
                               set t.password = '"+ pwd + @"'
                             where t.userid = '"+ user + @"'
                               and t.type = 'user'";

            return OracleHelper.ExecuteNonQuery(sql, new OracleParameter(":id", 1));
        }

        /// <summary>
        /// 更新昵称
        /// </summary>
        /// <param name="user"></param>
        /// <param name="pwd"></param>
        /// <returns></returns>
        public static int updateUserName(String user, String name)
        {
            String sql = @"update t_base_op_user t
                               set t.username = '" + name + @"'
                             where t.userid = '" + user + @"'
                               and t.type = 'user'";

            return OracleHelper.ExecuteNonQuery(sql, new OracleParameter(":id", 1));
        }

        /// <summary>
        /// 重置用户密码
        /// </summary>
        /// <param name="user"></param>
        /// <param name="pwd"></param>
        /// <returns></returns>
        public static int updateTargetUserPwd(String user)
        {
            String sql = @"update t_base_op_user t
                               set t.password = '123456'
                             where t.userid = '" + user + @"'
                               and t.type = 'user'";

            return OracleHelper.ExecuteNonQuery(sql, new OracleParameter(":id", 1));
        }


        /// <summary>
        /// 增加用户
        /// </summary>
        /// <param name="user"></param>
        /// <param name="pwd"></param>
        /// <returns></returns>
        public static int addTargetUserPwd(String user)
        {
            String sql = @"update t_base_op_user t
                               set t.password = '123456'
                             where t.userid = '" + user + @"'
                               and t.type = 'user'";

            return OracleHelper.ExecuteNonQuery(sql, new OracleParameter(":id", 1));
        }


        public static Hashtable getMessages(String message)
        {
            Hashtable hashtable = new Hashtable();
            String[] messsages = message.Split('\n');
            try
            {
                String[] messages = messsages[0].Split('|');
                for (int i = 0; i < messages.Length; i++)
                {
                    String[] tmp = messages[i].Split(':');
                    hashtable.Add(tmp[0], tmp[1]);
                }
            }
            catch
            {

            }
            return hashtable;
        }

        public static string setMessages(Hashtable message)
        {
            string str = "";
            foreach (DictionaryEntry de in message)
            {
                str += (de.Key + ":" + de.Value + "|");
            }
            str = str.Substring(0, str.Length - 1);
            str += "\n";
            return str;
        }

        public static void errorMessage(String message)
        {
            Console.Write("异常：" + message+"\n");
        }
        public static void errorMessage(Exception message)
        {
            Console.Write("异常：" + message + "\n");
        }


        public static void logger(String str)
        {
            Console.WriteLine(str);
        }

        public static void logger(Exception str)
        {
            Console.WriteLine(str+"");
        }
    }
}

