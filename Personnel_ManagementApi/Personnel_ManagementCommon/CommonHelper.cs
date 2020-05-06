using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LandMarkApply.Common
{
    public class CommonHelper
    {
        /// <summary>
        /// 通过key获取appsettings值
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static string GetAppSettingByKey(string key)
        {
            return  ConfigurationManager.AppSettings[key].ToString();
        }
        /// <summary>
        /// 通过name获取ConnectionString值
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public static string GetConnectionStringByName(string name)
        {
            return ConfigurationManager.ConnectionStrings[name].ConnectionString.ToString();
        }
    }
}
