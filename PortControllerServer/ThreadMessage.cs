using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;

namespace PortControllerServer
{
    class ThreadMessage
    {
        public TcpClient tc1;
        public TcpClient tc2;
        public String type;
        public String TOTAL_IP;
        public String TOTAL_PORT;
    }
}
