using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Traffic.Utility
{

    /// <summary>
    /// Description: 获取客户端Id
    /// Version: 1.0
    /// Created: 2015/4/23
    /// Author:  yyb
    /// Company: 北京博奥中成信息科技有限责任公司
    /// 
    /// ModifyEditTime: 修改时间
    /// ModifyContent:  修改内容
    /// ModifyPerson :  修改人
    /// </summary>
    public class IpHelper
    {
        public static string GetWebClientIp()
        {
            try
            {
                if (System.Web.HttpContext.Current == null) return "";
                string customerIp = "";
                //CDN加速后取到的IP 
                customerIp = System.Web.HttpContext.Current.Request.Headers["Cdn-Src-Ip"];
                if (!string.IsNullOrEmpty(customerIp))
                {
                    return customerIp;
                }
                customerIp = System.Web.HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
                if (!String.IsNullOrEmpty(customerIp))
                    return customerIp;
                if (System.Web.HttpContext.Current.Request.ServerVariables["HTTP_VIA"] != null)
                {
                    customerIp = System.Web.HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
                    if (customerIp == null)
                        customerIp = System.Web.HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"];
                }
                else
                {
                    customerIp = System.Web.HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"];

                }

                if (System.String.Compare(customerIp, "unknown", System.StringComparison.OrdinalIgnoreCase) == 0)
                    return System.Web.HttpContext.Current.Request.UserHostAddress;
                return customerIp;
            }
            catch (Exception)
            {
                return "";
            }
        }
    }
}
