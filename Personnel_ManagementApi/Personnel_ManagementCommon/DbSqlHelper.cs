using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace Traffic.Utility
{
    /// <summary>
    /// 数据操作类
    /// </summary>
    public class DbSqlHelper
    {
       // public static readonly string conStr = ConfigurationManager.ConnectionStrings["ConnString"].ConnectionString;
        /// <summary>
        /// 获取新版交通数据库连接字符串
        /// </summary>
        /// <returns></returns>
        public static string GetTrafficEduConString()
        {
            return ConfigurationManager.ConnectionStrings["ConnString"].ConnectionString;
        }

        /// <summary>
        /// 获取新版安全教育数据库连接字符串
        /// </summary>
        /// <returns></returns>
        public static string GetSecurityEduConString()
        {
            return ConfigurationManager.ConnectionStrings["SecurityEdu"].ConnectionString;
        }

        /// <summary>
        /// 获取新版交通数据库连接字符串-读写分离-2016-07-12-hu
        /// </summary>
        /// <returns></returns>
        public static string GetTrafficEduReadConString()
        {
            return ConfigurationManager.ConnectionStrings["WWWRead"].ConnectionString;
        }

        /// <summary>
        /// 执行sql语句，返回影响的行数
        /// </summary>
        /// <param name="sqlText"></param>
        /// <param name="conStr"></param>
        /// <returns></returns>
        public static int ExecuteNonQuery(String sqlText, string conStr)
        {
            var cmd = new SqlCommand();
            using (var conn = new SqlConnection(conStr))
            {
                conn.Open();
                cmd.Connection = conn;
                cmd.CommandText = sqlText;
                var value = cmd.ExecuteNonQuery();
                return value;
            }
        }

        /// <summary>
        /// 查询数据，并返回DataSet
        /// </summary>
        /// <param name="sqlText"></param>
        /// <param name="conStr"></param>
        /// <returns></returns>
        public static DataSet ExecuteDataSet(String sqlText, string conStr)
        {
            var cmd = new SqlCommand();
            var ds = new DataSet();
            using (var conn = new SqlConnection(conStr))
            {
                conn.Open();
                cmd.Connection = conn;
                cmd.CommandText = sqlText;
                using (var da = new SqlDataAdapter())
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
            var cmd = new SqlCommand();
            var dt = new DataTable();
            using (var conn = new SqlConnection(conStr))
            {
                conn.Open();
                cmd.Connection = conn;
                cmd.CommandText = sqlText;
                using (var da = new SqlDataAdapter())
                {
                    da.SelectCommand = cmd;
                    da.Fill(dt);
                    return dt;
                }
            }
        }

        /// <summary>
        /// 获取DataReader的第一个值,如果为Null，则返回0
        /// </summary>
        /// <param name="sqlText"></param>
        /// <param name="conStr"></param>
        /// <returns></returns>
        public static int GetSingleInt(String sqlText, string conStr)
        {
            var cmd = new SqlCommand();
            var conn = new SqlConnection(conStr);
            try
            {
                conn.Open();
                cmd.Connection = conn;
                cmd.CommandText = sqlText;
                var reader = cmd.ExecuteReader();
                reader.Read();
                var result = string.IsNullOrEmpty(reader[0].ToString()) ? 0 : Int32.Parse(reader[0].ToString());
                reader.Close();
                return result;
            }
            catch (Exception)
            {
                conn.Close();
                return 0;
            }
            finally
            {
                conn.Close();
            }
        }

        /// <summary>
        /// 获取DataReader的第一个值 bool，如果返回值为Null，则返回false
        /// </summary>
        /// <param name="sqlText"></param>
        /// <param name="conStr"></param>
        /// <returns></returns>
        public static bool GetSingleBool(String sqlText, string conStr)
        {
            var cmd = new SqlCommand();
            var conn = new SqlConnection(conStr);
            try
            {
                conn.Open();
                cmd.Connection = conn;
                cmd.CommandText = sqlText;
                var reader = cmd.ExecuteReader();
                reader.Read();
                var result = !string.IsNullOrEmpty(reader[0].ToString()) && bool.Parse(reader[0].ToString());
                reader.Close();
                return result;
            }
            catch (Exception)
            {
                conn.Close();
                return false;
            }
            finally
            {
                conn.Close();
            }
        }

        /// <summary>
        /// 获取DataReader的第一个值 String,如果没有值，则返回 ""
        /// </summary>
        /// <param name="sqlText"></param>
        /// <param name="conStr"></param>
        /// <returns></returns>
        public static string GetSingleString(String sqlText, string conStr)
        {
            var cmd = new SqlCommand();
            var conn = new SqlConnection(conStr);
            try
            {
                conn.Open();
                cmd.Connection = conn;
                cmd.CommandText = sqlText;
                var reader = cmd.ExecuteReader();
                reader.Read();
                var result = string.IsNullOrEmpty(reader[0].ToString()) ? "" : reader[0].ToString();
                reader.Close();
                return result;
            }
            catch (Exception)
            {
                conn.Close();
                return "";
            }
            finally
            {
                conn.Close();
            }
        }

        /// <summary>
        /// 获取DataReader的第一个值Double,如果为Null，则返回0
        /// </summary>
        /// <param name="sqlText"></param>
        /// <param name="conStr"></param>
        /// <returns></returns>
        public static double GetSingleDouble(String sqlText, string conStr)
        {
            var cmd = new SqlCommand();
            var conn = new SqlConnection(conStr);
            try
            {
                conn.Open();
                cmd.Connection = conn;
                cmd.CommandText = sqlText;
                var reader = cmd.ExecuteReader();
                reader.Read();
                var result = string.IsNullOrEmpty(reader[0].ToString()) ? 0 : double.Parse(reader[0].ToString());
                reader.Close();
                return result;
            }
            catch (Exception)
            {
                conn.Close();
                return 0;
            }
            finally
            {
                conn.Close();
            }
        }


        /// <summary>
        /// 获取DataReader的第一个值Double,如果为Null，则返回null
        /// </summary>
        /// <param name="sqlText"></param>
        /// <param name="conStr"></param>
        /// <returns></returns>
        public static double? GetSingleDoubleNull(String sqlText, string conStr)
        {
            var cmd = new SqlCommand();
            var conn = new SqlConnection(conStr);
            try
            {
                conn.Open();
                cmd.Connection = conn;
                cmd.CommandText = sqlText;
                var reader = cmd.ExecuteReader();
                reader.Read();
                var result = string.IsNullOrEmpty(reader[0].ToString()) ? 0 : double.Parse(reader[0].ToString());
                reader.Close();
                return result;
            }
            catch (Exception)
            {
                conn.Close();
                return null;
            }
            finally
            {
                conn.Close();
            }
        }


        /// <summary>
        /// 获取DataReader的第一个值 String,如果没有值，则返回 ""
        /// </summary>
        /// <param name="sqlText"></param>
        /// <param name="conStr"></param>
        /// <returns></returns>
        public static string GetSingleDateTime(String sqlText, string conStr)
        {
            var cmd = new SqlCommand();
            var conn = new SqlConnection(conStr);
            try
            {
                conn.Open();
                cmd.Connection = conn;
                cmd.CommandText = sqlText;
                var reader = cmd.ExecuteReader();
                reader.Read();
                var result = string.IsNullOrEmpty(reader[0].ToString()) ? "" : reader[0].ToString();
                reader.Close();
                return result;
            }
            catch (Exception)
            {
                conn.Close();
                return "";
            }
            finally
            {
                conn.Close();
            }
        }
    }
}

