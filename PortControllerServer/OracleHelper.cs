using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using Oracle.ManagedDataAccess.Client;
using System.Data;
using System.IO;
using System.Collections;
using System.Diagnostics;
using Oracle.ManagedDataAccess.Types;
using fileTools;

namespace OracleDemo

{
    public class OracleHelper
    {


        private static string connStr = "User Id="+INIhelp.GetValue("oracleuser") + ";Password=" + INIhelp.GetValue("oraclepwd") + ";Data Source=(DESCRIPTION=(ADDRESS_LIST=(ADDRESS=(PROTOCOL=TCP)(HOST=" + INIhelp.GetValue("oraclehost") + ")(PORT=" + INIhelp.GetValue("oracleport") + ")))(CONNECT_DATA=(SERVICE_NAME=" + INIhelp.GetValue("oracleserver") + ")))";

        #region 执行SQL语句,返回受影响行数
        public static int ExecuteNonQuery(string sql, params OracleParameter[] parameters)
        {
            using (OracleConnection conn = new OracleConnection(connStr))
            {
                conn.Open();
                using (OracleCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = sql;
                    cmd.Parameters.AddRange(parameters);
                    return cmd.ExecuteNonQuery();
                }
            }
        }
        #endregion
        #region 执行SQL语句,返回DataTable;只用来执行查询结果比较少的情况
        public static DataTable ExecuteDataTable(string sql, params OracleParameter[] parameters)
        {
            using (OracleConnection conn = new OracleConnection(connStr))
            {
                conn.Open();
                using (OracleCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = sql;
                    cmd.Parameters.AddRange(parameters);
                    OracleDataAdapter adapter = new OracleDataAdapter(cmd);
                    DataTable datatable = new DataTable();
                    adapter.Fill(datatable);
                    return datatable;
                }
            }
        }
        #endregion
    }

}
