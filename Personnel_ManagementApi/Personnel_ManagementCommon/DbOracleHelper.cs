using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.OracleClient;

namespace Traffic.Utility
{
    /// <summary>
    /// 数据操作类
    /// </summary>
    public class DbOracleHelper
    {
        /// <summary>
        /// 获取新版交通数据库连接字符串
        /// </summary>
        /// <returns></returns>
        public static string GetTrafficEduConString()
        {
            return ConfigurationManager.ConnectionStrings["WWW"].ConnectionString;
        }

        /// <summary>
        /// 获取安徽宣城数据库连接字符串
        /// </summary>
        /// <returns></returns>
        public static string GetAnHuiXuanChengConString()
        {
            return string.Format(@"Data Source=(DESCRIPTION =  
                                                                    (ADDRESS_LIST =  
                                                                      (ADDRESS = (PROTOCOL = TCP)(HOST = {0})(PORT = 1521))  
                                                                    )  
                                                                    (CONNECT_DATA =  
                                                                      (SID = {1})  
                                                                      (SERVER = DEDICATED)  
                                                                    )  
                                                                  );User Id={2};Password={3}",

                "10.37.2.85", "AHXTDB", "ahedubjba", "ahedubjba_0123456");
        }

        /// <summary>
        /// 获取浙江省局运管数据库连接字符串
        /// </summary>
        /// <returns></returns>
        public static string GetZheJiangConString()
        {
            return string.Format(@"Data Source=(DESCRIPTION =  
                                                                    (ADDRESS_LIST =  
                                                                      (ADDRESS = (PROTOCOL = TCP)(HOST = {0})(PORT = 1521))  
                                                                    )  
                                                                    (CONNECT_DATA =  
                                                                      (SID = {1})  
                                                                      (SERVER = DEDICATED)  
                                                                    )  
                                                                  );User Id={2};Password={3}",
                                             "183.136.168.206", "ORCL", "JXJY_WZ", "JXJY_WZ");
        }
        /// <summary>
        /// 执行sql语句，返回影响的行数
        /// </summary>
        /// <param name="sqlText"></param>
        /// <param name="conStr"></param>
        /// <returns></returns>
        public static int ExecuteNonQuery(String sqlText, string conStr)
        {
            System.Environment.SetEnvironmentVariable("NLS_LANG", "SIMPLIFIED CHINESE_CHINA.ZHS16GBK");
            var cmd = new OracleCommand();
            using (var conn = new OracleConnection(conStr))
            {
                conn.Open();
                cmd.Connection = conn;
                cmd.CommandText = sqlText;

                var value = cmd.ExecuteNonQuery();
                return value;
            }
        }

        /// <summary>
        /// 查询数据，返回DataReader
        /// </summary>
        /// <param name="sqlText"></param>
        /// <param name="conStr"></param>
        /// <returns></returns>
        public static OracleDataReader ExecuteReader(String sqlText, string conStr)
        {
            System.Environment.SetEnvironmentVariable("NLS_LANG", "SIMPLIFIED CHINESE_CHINA.ZHS16GBK");
            var conn = new OracleConnection(conStr);
            var cmd = new OracleCommand();
            try
            {
                conn.Open();
                cmd.Connection = conn;
                cmd.CommandText = sqlText;
                var reader = cmd.ExecuteReader();
                return reader;
            }
            catch
            {
                conn.Close();
            }
            return null;
        }

        /// <summary>
        /// 查询数据，并返回DataSet
        /// </summary>
        /// <param name="sqlText"></param>
        /// <param name="conStr"></param>
        /// <returns></returns>
        public static DataSet ExecuteDataSet(String sqlText, string conStr)
        {
            System.Environment.SetEnvironmentVariable("NLS_LANG", "SIMPLIFIED CHINESE_CHINA.ZHS16GBK");
            var cmd = new OracleCommand();
            var ds = new DataSet();
            using (var conn = new OracleConnection(conStr))
            {
                conn.Open();
                cmd.Connection = conn;
                cmd.CommandText = sqlText;
                using (var da = new OracleDataAdapter())
                {
                    da.SelectCommand = cmd;
                    da.Fill(ds);
                    return ds;
                }
            }
        }

        /// <summary>
        /// 查询数据，返回DataTable
        /// </summary>
        /// <param name="sqlText"></param>
        /// <param name="conStr"></param>
        /// <returns></returns>
        public static DataTable ExecuteDataTable(String sqlText, string conStr)
        {
            System.Environment.SetEnvironmentVariable("NLS_LANG", "SIMPLIFIED CHINESE_CHINA.ZHS16GBK");
            var cmd = new OracleCommand();
            var dt = new DataTable();
            using (var conn = new OracleConnection(conStr))
            {
                conn.Open();
                cmd.Connection = conn;
                cmd.CommandText = sqlText;
                using (var da = new OracleDataAdapter())
                {
                    da.SelectCommand = cmd;
                    da.Fill(dt);
                    return dt;
                }
            }
        }

        /// <summary>
        /// 获取DataReader的第一个值
        /// </summary>
        /// <param name="sqlText"></param>
        /// <param name="conStr"></param>
        /// <returns></returns>
        public static int GetSingleInt(String sqlText, string conStr)
        {
            System.Environment.SetEnvironmentVariable("NLS_LANG", "SIMPLIFIED CHINESE_CHINA.ZHS16GBK");
            var cmd = new OracleCommand();
            var conn = new OracleConnection(conStr);
            try
            {
                conn.Open();
                cmd.Connection = conn;
                cmd.CommandText = sqlText;
                var reader = cmd.ExecuteReader();
                reader.Read();
                if (reader.IsDBNull(0))
                    return 0;
                else
                    return reader.GetInt32(0);
            }
            catch (Exception)
            {
                conn.Close();
                return -1;
            }
            finally
            {
                conn.Close();
            }
        }
    }
}
