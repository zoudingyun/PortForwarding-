using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;

namespace PortControllerClient
{
    public static class PublicVariable4CS
    {
        public static Boolean login=false;

        public static String UserName;
        public static String UserDesc;
        public static String PassWord;
        public static String INHERIT;//身份（是否是管理员）
        public static String ServerIP;
        public static double ver = 2.0;
        public static int ServerPort;
        public static Boolean SaveUser;
        public static Socket serverSocket;
        public static Boolean portOpen = false;


        public static List<Socket> clientSocketsForUser = new List<Socket>();
        public static List<Socket> clientSocketsForServer = new List<Socket>();



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
            Console.WriteLine("异常："+ message);
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
