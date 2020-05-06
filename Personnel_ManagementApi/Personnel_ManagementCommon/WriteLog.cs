using System;
using System.IO;
using System.Text;
using System.Web;

namespace LandMarkApply.Common
{
    public class WriteLog
    {
        /// <summary>
        /// 记录日志
        /// </summary>
        /// <param name="text"></param>
        public void WriteLogs(string text)
        {
            string path = AppDomain.CurrentDomain.BaseDirectory;
            path = Path.Combine(path, "Log\\");

            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            string fileFullName = Path.Combine(path, string.Format("{0}.txt", DateTime.Now.ToString("yyyyMMdd")));

            using (StreamWriter output = File.AppendText(fileFullName))
            {
                output.WriteLine(text);
                output.Close();
            }
        }

        /// <summary> 
        /// 创建日志文件 
        /// </summary> 
        /// <param name="ex">异常类</param> 
        public void CreateLog(Exception ex)
        {
            string path = AppDomain.CurrentDomain.BaseDirectory;
            path = Path.Combine(path, "Log\\");
            if (!Directory.Exists(path))
            {
                //创建日志文件夹 
                Directory.CreateDirectory(path);
            }
            //发生异常每天都创建一个单独的日子文件[*.log],每天的错误信息都在这一个文件里。方便查找 
            path += "\\" + DateTime.Now.ToString("yyyyMMdd") + ".log";
            WriteLogInfo(ex, path);
        }
        /// <summary> 
        /// 写日志信息 
        /// </summary> 
        /// <param name="ex">异常类</param> 
        /// <param name="path">日志文件存放路径</param> 
        public void WriteLogInfo(Exception ex, string path)
        {
            using (StreamWriter sw = new StreamWriter(path, true, Encoding.Default))
            {
                sw.WriteLine("*****************************************【"
                               + DateTime.Now.ToString()
                               + "】*****************************************");
                if (ex != null)
                {
                    //把异常信息输出到文件
                    sw.WriteLine("异常信息：" + ex.Message);
                    sw.WriteLine("异常对象：" + ex.Source);
                    sw.WriteLine("调用堆栈：/n" + ex.StackTrace.Trim());
                    sw.WriteLine("触发方法：" + ex.TargetSite);                 
                }
                else
                {
                    sw.WriteLine("Exception is NULL");
                }
                sw.WriteLine();
                sw.Close();
            }
        }    
    }
}
