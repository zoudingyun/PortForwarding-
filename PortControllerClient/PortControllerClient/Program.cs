using PortControllerClient.Forms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace PortControllerClient
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main(string[] Args)
        {

            System.Diagnostics.Process[] myProcesses = System.Diagnostics.Process.GetProcessesByName("PortControllerClient");//获取指定的进程名   
            if (myProcesses.Length > 1) //如果可以获取到知道的进程名则说明已经启动
            {
                MessageBox.Show("程序已启动！请不要重复打开！");
                Application.Exit();              //关闭系统
                return;
            }


            //获得当前登录的Windows用户标示
            System.Security.Principal.WindowsIdentity identity = System.Security.Principal.WindowsIdentity.GetCurrent();


            System.Security.Principal.WindowsPrincipal principal = new System.Security.Principal.WindowsPrincipal(identity);
            //判断当前登录用户是否为管理员
            if (principal.IsInRole(System.Security.Principal.WindowsBuiltInRole.Administrator))
            {
                //如果是管理员，则直接运行
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                //Application.Run(new mainForm());
                Application.Run(new WebForm());

            }
            else
            {
                //创建启动对象
                System.Diagnostics.ProcessStartInfo startInfo = new System.Diagnostics.ProcessStartInfo();
                //设置运行文件
                startInfo.FileName = System.Windows.Forms.Application.ExecutablePath;


                String[] values = new String[1];

                if (Args.Length==0)
                {
                    values[0] = "0";
                    //设置启动参数
                    startInfo.Arguments = String.Join(" ", values);
                    //设置启动动作,确保以管理员身份运行
                    startInfo.Verb = "runas";
                    //如果不是管理员，则启动UAC
                    System.Diagnostics.Process.Start(startInfo);
                    //退出
                    System.Windows.Forms.Application.Exit();
                }
                else
                {
                    values[0] = ""+(int.Parse(Args[0])+1);
                    if (int.Parse(Args[0]) >= 0)
                    {
                        MessageBox.Show("无法以管理员权限启动，功能将受到限制！");
                        PublicVariable4CS.UAC = false;
                        Application.EnableVisualStyles();
                        Application.SetCompatibleTextRenderingDefault(false);
                        Application.Run(new mainForm());
                        return;
                    }
                }
              
            }

        }
    }
}
