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

namespace PortControllerServer
{
    class Program
    {
        static int threadCount = 0;
        static void Main(string[] args)
        {
            int serverPort = int.Parse(INIhelp.GetValue("port"));


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
                    string[] userMessage = str.Split('\n');
                    userMessage = userMessage[0].Split('|');
                    if (userMessage[0] == "LOGIN")
                    {
                        DataTable dt = queryUserLoginMessage(userMessage[1]);
                        if (dt.Rows.Count <= 0)
                        {
                            String tmp = "LOGIN-RE|ERROR_USERNAME";
                            byte[] byteArray = System.Text.Encoding.UTF8.GetBytes(tmp);
                            ns1.Write(byteArray, 0, byteArray.Length);
                            continue;
                        }
                        String pwd = dt.Rows[0]["PASSWORD"].ToString();
                        String username = dt.Rows[0]["userdesc"].ToString();
                        if (pwd == userMessage[2])
                        {
                            String tmp = "LOGIN-RE|TRUE|USERDESC:" + username;
                            byte[] byteArray = System.Text.Encoding.UTF8.GetBytes(tmp);
                            ns1.Write(byteArray, 0, byteArray.Length);
                        }
                        else
                        {
                            String tmp = "LOGIN-RE|FALSE|USERDESC:";
                            byte[] byteArray = System.Text.Encoding.UTF8.GetBytes(tmp);
                            ns1.Write(byteArray, 0, byteArray.Length);
                        }
                        continue;
                    }
                    else if (userMessage[0] == "CONNECT")
                    {
                        String[] address = userMessage[3].Split(':');
                        DataTable dt = queryUserLicense(userMessage[1], userMessage[2], address[0], address[1]);
                        if (int.Parse(dt.Rows[0]["License"].ToString()) <= 0)
                        {
                            String tmp = "CONNECT-RE|FALSE|PERMISSION DENIED:";
                            byte[] byteArray = System.Text.Encoding.UTF8.GetBytes(tmp);
                            ns1.Write(byteArray, 0, byteArray.Length);
                        }
                        else
                        {
                            try
                            {
                                TcpClient tc2 = new TcpClient(address[0], int.Parse(address[1]));

                                String tmp = "CONNECT-RE|TRUE";
                                byte[] byteArray = System.Text.Encoding.UTF8.GetBytes(tmp);
                                ns1.Write(byteArray, 0, byteArray.Length);

                                object obj1 = (object)(new TcpClient[] { tc1, tc2 });
                                object obj2 = (object)(new TcpClient[] { tc2, tc1 });
                                ThreadPool.QueueUserWorkItem(new WaitCallback(transfer), obj1);
                                ThreadPool.QueueUserWorkItem(new WaitCallback(transfer), obj2);
                            }
                            catch(Exception ex)
                            {
                                String tmp1 = "CONNECT-RE|FALSE|"+ex.Message;
                                byte[] byteArray1 = System.Text.Encoding.UTF8.GetBytes(tmp1);
                                ns1.Write(byteArray1, 0, byteArray1.Length);
                                continue;
                            }
                            //TcpClient tc2 = new TcpClient("172.30.200.50", 3389);


                           


                        }

                        continue;
                    }
                    else
                    {
                        continue;
                    }


                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                }
            }


        }



        /// <summary>
        /// 工作子线程高级版（端口转发建立后，实际进行数据转发的线程）
        /// </summary>
        public static void transfer(object obj)
        {
            threadCount++;
            TcpClient tc1 = ((TcpClient[])obj)[0];
            TcpClient tc2 = ((TcpClient[])obj)[1];
            NetworkStream ns1 = tc1.GetStream();
            NetworkStream ns2 = tc2.GetStream();
            double start=0;
            double end = 0;
            Boolean overSpeed = false;
            int overSpeedCount = 0;
            while (true)
            {
                start = DateTime.Now.Subtract(DateTime.Parse("1970-1-1")).TotalMilliseconds;
                try
                {
                    //这里必须try catch，否则连接一旦中断程序就崩溃了，要是弹出错误提示让机主看见那就囧了
                    byte[] bt = new byte[2048];
                    int count = ns1.Read(bt, 0, bt.Length);
                    ns2.Write(bt, 0, count);
                }
                catch(Exception ex)
                {
                    ns1.Dispose();
                    ns2.Dispose();
                    tc1.Close();
                    tc2.Close();
                    break;
                }
                end = DateTime.Now.Subtract(DateTime.Parse("1970-1-1")).TotalMilliseconds;
                if ((end-start)<=1)
                {
                    if (overSpeedCount >= 500) {
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
            threadCount--;
        }


        public static DataTable queryUserLoginMessage(String user)
        {
            String sql = @"select a.userid as username
            , a.username as userdesc
            , a.password as password
              from t_base_op_user a
             where a.userid = '"+user+@"'
             group by a.userid, a.username, a.password";

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
       and (t.port = " + port + @"
         or t.port = '*')";

            return OracleHelper.ExecuteDataTable(sql, new OracleParameter(":id", 1));
        }
    }
}
