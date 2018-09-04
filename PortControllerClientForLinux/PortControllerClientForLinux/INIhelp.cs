using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;
using System.IO;

namespace fileTools
{
    class INIhelp
    {
        [DllImport("kernel32")]
        private static extern long WritePrivateProfileString(string section, string key, string val, string filepath);
        [DllImport("kernel32")]
        private static extern int GetPrivateProfileString(string section, string key, string def, StringBuilder retval, int size, string filePath);

        //ini文件名称
        private static string inifilename = "portConfig.ini";
        //获取ini文件路径
        //private static string inifilepath = Directory.GetCurrentDirectory() + "\\" + inifilename;
        private static string inifilepath = "./" + inifilename;

            //1.创建文件流
        

        public static string GetValue(string key)
        {
            FileStream fs111 = new FileStream(inifilepath, FileMode.OpenOrCreate);
            fs111.Close();

            StringBuilder s = new StringBuilder(1024);
            GetPrivateProfileString("CONFIG", key, "", s, 1024, inifilepath);
            
            return s.ToString();
        }


        public static void SetValue(string key, string value)
        {
            FileStream fs111 = new FileStream(inifilepath, FileMode.OpenOrCreate);
            fs111.Close();
            try
            {
                WritePrivateProfileString("CONFIG", key, value, inifilepath);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }

}
