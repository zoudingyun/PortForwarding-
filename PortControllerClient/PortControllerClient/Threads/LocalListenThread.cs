using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace PortControllerClient.Threads
{
    class LocalListenThread
    {
        public static void localListener()
        {
            Socket clientSocketForUser = PublicVariable4CS.clientSocketForUser;
            String clientSocketForServer = PublicVariable4CS.targetAddr;
            PublicVariable4CS.clientSocketLocked = false;
            while (true) {
                try { 
                    Socket localsocket = clientSocketForUser.Accept();
                    Socket serverSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                    IPAddress ip = IPAddress.Parse(PublicVariable4CS.ServerIP);
                    IPEndPoint point = new IPEndPoint(ip, PublicVariable4CS.ServerPort);
                    serverSocket.Connect(point);

                    String s = "CONNECT|" + PublicVariable4CS.UserName + "|" + PublicVariable4CS.PassWord +"|"+ clientSocketForServer + "\n";
                    serverSocket.Send(Encoding.UTF8.GetBytes(s));

                    byte[] dat = new byte[2048];

                    String data = System.Text.Encoding.UTF8.GetString(dat, 0, serverSocket.Receive(dat));
                    String[] datas = data.Split('★');
                    data = datas[1];

                    if (data.IndexOf(@"CONNECT-RE|TRUE") >= 0)
                    {
                        MessageBox.Show("ok");
                        while (true)
                        {
                            if (PublicVariable4CS.serverSocketLocked)
                            {
                                Thread.Sleep(30);
                                continue;
                            }
                            PublicVariable4CS.serverSocketLocked = true;
                            PublicVariable4CS.clientSocketForServerSon = serverSocket;
                            PublicVariable4CS.clientSocketForUserSon = localsocket;
                        }


                    }else if(data.IndexOf(@"CONNECT-RE|FALSE") >= 0)
                    {
                        String[] tmpDatas = data.Split('|');
                        MessageBox.Show("端口转发被拒绝："+tmpDatas[2]);
                        serverSocket.Close();
                        localsocket.Close();
                    }
                    else
                    {
                        serverSocket.Close();
                        localsocket.Close();
                        throw new Exception("服务器反馈数据异常！");
                    }

                }
                catch(Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
            }
        }
    }
}
