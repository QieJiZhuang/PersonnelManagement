using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Web;

namespace LandMarkApply.Common
{
    public class SMTPHelper
    {
        public static void SMTP126(string emailaddress,string title,string text)
        {
            try
            {
                string userName = ConfigurationManager.AppSettings["SendEmialAccount"].ToString();
                string password = ConfigurationManager.AppSettings["SendEmialPassword"].ToString();
                string host = ConfigurationManager.AppSettings["SendEmialServerv"].ToString();
                SmtpClient client = new SmtpClient(host, 25)
                {
                    Credentials = new NetworkCredential(userName, password),
                    EnableSsl = true
                };

                MailAddress from = new MailAddress(@userName, "");
                MailAddress to = new MailAddress(@emailaddress, "");
                MailMessage myMail = new MailMessage(from, to);

                myMail.Subject = title;
                myMail.SubjectEncoding = System.Text.Encoding.UTF8;
                myMail.Body = text;

                myMail.BodyEncoding = System.Text.Encoding.UTF8;
                client.Send(myMail);

            }
            catch (SmtpException ex)
            {
                throw new ApplicationException
                  ("SmtpException has occured: " + ex.Message);
            }
        }

        /// <summary>
        /// 验证码
        /// </summary>
        /// <param name="len"></param>
        /// <returns></returns>
        public static string CreateVerifyCode(int len)
        {
            char[] date = { 'a','b', 'c', 'd', 'e', 'f','g', 'h','i','j', 'k','l', 'm', 'n','o','p','q', 'r', 's', 't',
                'u','v', 'w', 'x', 'y','z','0','1','2','3','4','5','6','7','8','9' };
            StringBuilder sb = new StringBuilder();
            Random random = new Random();
            for (int i = 0; i < len; i++)
            {
                int index = random.Next(date.Length);
                char ch = date[index];
                sb.Append(ch);
            }
            return sb.ToString();
        }
    }
}