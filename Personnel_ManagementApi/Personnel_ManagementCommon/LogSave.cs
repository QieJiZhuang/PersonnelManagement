using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Web;

namespace DTcms.Common
{
    public static class LogSave
    {
        /// <summary>
        /// 获取日志目录 
        /// </summary>
        /// <returns></returns>
        public static string GetLogDirectory()
        {
            string baseDirectory = string.Empty;
            if ((HttpContext.Current != null) && (HttpContext.Current.Server != null))
            {
                baseDirectory = HttpContext.Current.Server.MapPath("~");
            }
            else
            {
                baseDirectory = AppDomain.CurrentDomain.BaseDirectory;
            }
            if ((baseDirectory[baseDirectory.Length - 1] != '/') && (baseDirectory[baseDirectory.Length - 1] != '\\'))
            {
                baseDirectory = baseDirectory + @"\";
            }
            baseDirectory = string.Format(@"{0}Log\", baseDirectory);
            if (!Directory.Exists(baseDirectory))
            {
                Directory.CreateDirectory(baseDirectory);
            }
            return baseDirectory;
        }

        /// <summary>
        /// 保存异常记录 
        /// </summary>
        /// <param name="e"></param>
        public static void SaveException(Exception e, string filename = "")
        {
            SaveException(e, string.Empty, filename);
        }

        public static void SaveException(Exception e, string memo, string filename = "")
        {
            FileStream stream = new FileStream(GetLogDirectory() + filename + DateTime.Now.ToString("yyyy-MM-dd") + ".txt", FileMode.Append, FileAccess.Write, FileShare.Delete | FileShare.ReadWrite);
            StreamWriter writer = new StreamWriter(stream);
            writer.WriteLine("================================================================");
            writer.WriteLine(string.Format("Memo:\t{0}", memo));
            writer.WriteLine(string.Format("DateTime:\t{0}", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")));
            writer.WriteLine(string.Format("Message:\t{0}", e.Message));
            writer.WriteLine(string.Format("StackTrace:\r\n----------\r\n{0}\r\n----------\r\n\r\n\r\n", e.StackTrace));
            stream.Flush();
            writer.Close();
            stream.Close();
            stream.Dispose();
            writer.Dispose();
        }


        /// <summary>
        /// 保存正常执行记录 
        /// </summary>
        /// <param name="note"></param>
        public static void SaveNote(string note, string filename = "")
        {
            FileStream stream = new FileStream(GetLogDirectory() + filename + DateTime.Now.ToString("yyyy-MM-dd") + ".txt", FileMode.Append, FileAccess.Write, FileShare.Delete | FileShare.ReadWrite);
            StreamWriter writer = new StreamWriter(stream);
            writer.WriteLine("================================================================");
            writer.WriteLine(string.Format("Note:\t{0}", note));
            writer.WriteLine(string.Format("DateTime:\t{0}\r\n\r\n", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")));
            stream.Flush();
            writer.Close();
            stream.Close();
            stream.Dispose();
            writer.Dispose();
        }
    }
}
