using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace LandMarkApply.Common
{
   public class SendEmail
    {

        /// <summary>
        /// 发送邮件
        /// </summary>
        /// <param name="mailTo">要发送的邮箱</param>
        /// <param name="mailSubject">邮箱主题</param>
        /// <param name="mailContent">邮箱内容</param>
        /// <returns>返回发送邮箱的结果</returns>
        public static bool SendEmails(string mailTo, string mailSubject, string mailContent)
        {
            // 设置发送方的邮件信息,例如使用网易的smtp
            string smtpServer = System.Web.Configuration.WebConfigurationManager.AppSettings["SendEmialServerv"]; //SMTP服务器
            string mailFrom = System.Web.Configuration.WebConfigurationManager.AppSettings["SendEmialAccount"] ; //登陆用户名
            string SendDisplayName = "地理标志产品保护申请电子受理平台";
            string SendDisplayUser = "平台用户";
            string userPassword = System.Web.Configuration.WebConfigurationManager.AppSettings["SendEmialPassword"]; //登陆密码

            // 邮件服务设置
            SmtpClient smtpClient = new SmtpClient();
            smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;//指定电子邮件发送方式
            smtpClient.Host = smtpServer; //指定SMTP服务器
            smtpClient.Credentials = new System.Net.NetworkCredential(mailFrom, userPassword);//用户名和密码

            // 发送邮件设置       
            MailAddress mailAddress = new MailAddress(mailFrom, SendDisplayName,Encoding.UTF8);
            MailAddress mailAddressuser = new MailAddress(mailTo, SendDisplayUser, Encoding.UTF8);
            MailMessage mailMessage = new MailMessage(mailAddress, mailAddressuser); // 发送人和收件人
            mailMessage.Subject = mailSubject;//主题
            mailMessage.Body = mailContent;//内容
            mailMessage.BodyEncoding = Encoding.UTF8;//正文编码
            mailMessage.IsBodyHtml = true;//设置为HTML格式
            mailMessage.Priority = MailPriority.Low;//优先级

            try
            {
                smtpClient.Send(mailMessage); // 发送邮件
                return true;
            }
            catch (Exception ex)
            {
                //LogHelper.AddError(ex);
                return false;
            }
        }

    }
}
