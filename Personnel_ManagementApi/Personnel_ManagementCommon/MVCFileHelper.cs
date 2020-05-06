using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Web;

namespace LandMarkApply.Common
{
    public class MVCFileHelper
    {
        /// <summary>
        /// 文件阅读
        /// </summary>
        /// <returns></returns>
        public static string FileRead(string filePath)
        {
            if (string.IsNullOrWhiteSpace(filePath))
            {
                throw new NullReferenceException("filePath为空，请确认");
            }
            if (!filePath.Trim().StartsWith(@"~/UpLoadFile/"))
            {
                filePath = @"~/UpLoadFile/" + filePath;
            }
            string temporaryFilePath = "~/temporaryfile/";
            //检测临时文件夹是否存在
            if (!Directory.Exists(HttpContext.Current.Server.MapPath(temporaryFilePath)))
            {
                Directory.CreateDirectory(HttpContext.Current.Server.MapPath(temporaryFilePath));
            }
            temporaryFilePath += FileHelper.GetFileName(HttpContext.Current.Server.MapPath(filePath));
            File.Copy(HttpContext.Current.Server.MapPath(filePath), HttpContext.Current.Server.MapPath(temporaryFilePath), true);
            return temporaryFilePath;
        }
    }
}
