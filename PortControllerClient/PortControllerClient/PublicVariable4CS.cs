using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Windows.Forms;

namespace PortControllerClient
{
    public static class PublicVariable4CS
    {
        public static Boolean login=false;

        public static String UserName;
        public static String UserDesc;
        public static String PassWord;
        public static String INHERIT;//身份（是否是管理员）
        public static Boolean UAC =true;//是否获取到了管理员权限
        public static String ServerIP;
        public static double ver;
        public static int ServerPort;
        public static Boolean SaveUser;
        public static Socket serverSocket;
        public static Boolean portOpen = false;


        public static List<Socket> clientSocketsForUser = new List<Socket>();
        public static List<Socket> clientSocketsForServer = new List<Socket>();


        /// <summary>
        /// 请求服务时用的socket
        /// </summary>
        public static Socket clientSocketForUser;
        public static String targetAddr;
        public static Boolean clientSocketLocked = false;

        /// <summary>
        /// 子线程用的本地与服务器的socket
        /// </summary>
        public static Socket clientSocketForServerSon;
        public static Socket clientSocketForUserSon;
        public static int threadType = -1;
        public static Boolean serverSocketLocked = false;
        public static Boolean sonSocketLocked0 = false;
        public static Boolean sonSocketLocked1 = false;



        public static Socket servertest;
        public static Socket clienttest;

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

        public static void errorMessage(String message)
        {
            MessageBox.Show("异常："+ message);
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

    }

    
}
