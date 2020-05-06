using System;
using System.IO;
using System.Runtime.Remoting.Channels;
using System.Text;
using System.Net.Mail;
using System.Net;
using System.Text.RegularExpressions;

namespace Traffic.Utility
{
    /// <summary>
    /// Description: 邮件操作类
    /// Version: 1.0
    /// Created: 2014/12/17
    /// Author:  zlf
    /// Company: 北京博奥中成信息科技有限责任公司
    /// 
    /// ModifyEditTime: 修改时间
    /// ModifyContent:  修改内容
    /// ModifyPerson :  修改人
    /// </summary>
    public static class EmailHelper
    {
        /// <summary>
        /// 邮件发送方法
        /// </summary>
        /// <param name="email">email地址</param>
        /// <param name="subject">邮件标题</param>
        /// <param name="content">邮件内容</param>
        /// <returns></returns>
        public static bool Send163(string email, string subject, string content)
        {
            var fromSmtp = ConfigHelper.SmtpServerName;        //邮件服务器
            var fromEmail = ConfigHelper.FromEmail;            //发送方邮件地址
            var fromEmailPwd = ConfigHelper.FromEmailPwd;      //发送方邮件地址密码
            var fromEmailName = ConfigHelper.EmailDisplayName; //发送方称呼

            //创建邮件内容
            var aMessage = new MailMessage();
            aMessage.From = new MailAddress(fromEmail, fromEmailName);
            aMessage.To.Add(email);
            aMessage.Subject = subject;
            aMessage.Body = content;
            aMessage.BodyEncoding = Encoding.GetEncoding("gb2312");
            aMessage.IsBodyHtml = true;
            aMessage.Priority = MailPriority.High;
            aMessage.ReplyToList.Add(new MailAddress(fromEmail, fromEmailName));

            //创建发送对象
            var smtp = new SmtpClient();
            smtp.Host = fromSmtp;
            smtp.Timeout = 20000;
            smtp.UseDefaultCredentials = false;
            smtp.EnableSsl = false;
            smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
            smtp.Credentials = new NetworkCredential(fromEmail, fromEmailPwd); //发邮件的Email和密码
            smtp.Port = 25;

            try
            {
                smtp.Send(aMessage);
                return true;
            }
            catch(Exception ex)
            {
                //调试时执行
                using (var dout = new StreamWriter(System.Web.HttpContext.Current.Server.MapPath("../error.txt"), true))
                {
                    dout.Write("/r/n事件：" + ex.Message + "_" + email + "/r/n操作时间：" 
                        + DateTime.Now.ToString("yyy-MM-dd HH:mm:ss") + "&&&&&&&");
                    dout.Close();
                }
                return false;
            }
        }

        /// <summary>
        /// 返回激活邮箱邮件验证内容
        /// </summary>
        /// <param name="userId">接收邮件的用户ID</param>
        /// <param name="nickName">接收邮件的用户昵称</param>
        /// <param name="key">生成的随机字符串</param>
        /// <returns></returns>
        public static string GetActivationAccountEmailContent(int userId, string nickName, string key)
        {
            var str = new StringBuilder();
            return str.ToString();
        }

        /// <summary>
        /// 验证是否是邮箱
        /// </summary>
        /// <param name="source">邮件地址</param>
        /// <returns></returns>
        public static bool IsEmail(string source)
        {
            return Regex.IsMatch(source, @"^[A-Za-z0-9](([_\.\-]?[a-zA-Z0-9]+)*)@([A-Za-z0-9]+)(([\.\-]?[a-zA-Z0-9]+)*)\.([A-Za-z]{2,})$", RegexOptions.IgnoreCase);
        }
    }
}
