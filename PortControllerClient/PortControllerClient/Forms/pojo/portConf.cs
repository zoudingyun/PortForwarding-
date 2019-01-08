using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PortControllerClient.Forms.pojo
{
    class PortConf
    {
        public string forwardPassNet { get; set; }
        public string agentAdd { get; set; }
        public string agentPort { get; set; }
        public string targetAdd { get; set; }
        public string targetPort { get; set; }
        public string userId { get; set; }
        public string targetPwd { get; set; }
    }
}
