using System;
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
        public static String ServerIP;
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


    }
}
