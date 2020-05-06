using System;
using System.IO;
using System.Text;
using System.Web.Security;
using System.Xml;
using System.Xml.Serialization;

namespace Traffic.Utility
{
    /// <summary>
    /// Description: 泛型序列号化操作类
    /// Version: 1.0
    /// Created: 2014/12/17
    /// Author:  zlf
    /// Company: 北京博奥中成信息科技有限责任公司
    /// 
    /// ModifyEditTime: 修改时间
    /// ModifyContent:  修改内容
    /// ModifyPerson :  修改人
    /// </summary>
    public  class SerializableHelper
    {
        /// <summary>
        /// 序列化成一个字符串
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="t"></param>
        /// <returns>序列化代码</returns>
        public static string Encrypt<T>(T t)
        {
            try
            {
                var s = new XmlSerializer(typeof(T));
                Stream stream = new MemoryStream();
                s.Serialize(stream, t);

                stream.Seek(0, SeekOrigin.Begin);
                string strSource;
                using (var reader = new StreamReader(stream))
                {
                    strSource = reader.ReadToEnd();
                }
                return strSource;
            }
            catch(Exception ex)
            {
                throw ex.InnerException;
            }
        }

        /// <summary>
        /// 字符串反序列化成一个类
        /// </summary>
        /// <param name="xmlSource"></param>
        /// <returns></returns>
        public static T Decrypt<T>(string xmlSource)
        {
            if (string.IsNullOrEmpty(xmlSource)) return default(T);
            try
            {

                var x = new XmlSerializer(typeof(T));                
                Stream stream = new MemoryStream(Encoding.UTF8.GetBytes(xmlSource));
                stream.Seek(0, SeekOrigin.Begin);
                var obj = x.Deserialize(stream);
                stream.Close();
                return (T)obj;
            }
            catch
            {
                CookiesHelper.RemoveCookie(FormsAuthentication.FormsCookieName);
                return default(T);
            }
        }
    }
}
