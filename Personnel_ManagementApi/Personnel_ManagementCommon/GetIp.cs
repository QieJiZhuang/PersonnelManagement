using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web;

namespace LandMarkApply.Common
{
   public class GetIp
    {
        /// <summary>
        /// 获取本地ip地址
        /// </summary>
        /// <returns></returns>       
        public static string GetIP(HttpContextBase req)
        {
            // 针对于代理器（获取的不一定为真实的，可修改）
            string result = req.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
            if (string.IsNullOrEmpty(result))
            {
                result = req.Request.ServerVariables["REMOTE_ADDR"];
            }
            if (string.IsNullOrEmpty(result))
            {
                result = req.Request.UserHostAddress;
            }
            if (string.IsNullOrEmpty(result))
            {
                result = "0.0.0.0";
            }
            if (result == "::1")
            {
                result = "127.0.0.1";
            }
            return result;
        }




    }
}
