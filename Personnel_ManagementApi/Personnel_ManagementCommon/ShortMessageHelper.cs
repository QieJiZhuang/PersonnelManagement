using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Traffic.Utility
{
    /// <summary>
    /// Description: 发送短信
    /// Version: 1.0
    /// Created: 2015/4/27
    /// Author:  jyc
    /// Company: 北京博奥中成信息科技有限责任公司
    /// 
    /// ModifyEditTime: 修改时间
    /// ModifyContent:  修改内容
    /// ModifyPerson :  修改人
    /// </summary>
    public static class ShortMessageHelper
    {
        //------------------------------------------------
        //功能:	美联软通HTTP接口C#调用说明
        //日期:	2013-05-08
        //说明:	http://m.5c.com.cn/api/send/?apikey=32位加密码&username=用户名&password=密码&mobile=手机号&content=内容
        //状态:


        //返回值										说明
        //success:msgid								提交成功，发送状态请见4.1
        //error:msgid								提交失败
        //error:Missing username					用户名为空
        //error:Missing password					密码为空
        //error:Missing apikey						APIKEY为空
        //error:Missing recipient					手机号码为空
        //error:Missing message content				短信内容为空
        //error:Account is blocked					帐号被禁用
        //error:Unrecognized encoding				编码未能识别
        //error:APIKEY or password error			APIKEY 或密码错误
        //error:Unauthorized IP address				未授权 IP 地址
        //error:Account balance is insufficient		余额不足
        //error:Black keywords is:党中央			屏蔽词

        /// <summary>
        /// 发送短信--美联软通
        /// </summary>
        /// <param name="phoneNumber">手机号码</param>
        /// <param name="content">短信内容</param>
        public static string SendMessage(string phoneNumber, string content)
        {
            const string userName = "bjba";
            const string password = "Hhx3434x2014";
            const string apikey = "c846a0ce1e3434db61158b270cf7b3d766b5";
            //POST
            var sbTemp = new StringBuilder();
            sbTemp.Append("apikey=" + apikey + "&username=" + userName + "&password=" + password + "&mobile=" + phoneNumber + "&content=" + content);
            var bData = System.Text.Encoding.GetEncoding("GBK").GetBytes(sbTemp.ToString());
            var result = PostRequest("http://m.5c.com.cn/api/send/?", bData);
            return result;
        }



        //1	    操作成功
        //0	    帐户格式不正确(正确的格式为:员工编号@企业编号)
        //-1	服务器拒绝(速度过快、限时或绑定IP不对等)如遇速度过快可延时再发
        //-2	密钥不正确
        //-3	密钥已锁定
        //-4	参数不正确(内容和号码不能为空，手机号码数过多，发送时间错误等)
        //-5	无此帐户
        //-6	帐户已锁定或已过期
        //-7	帐户未开启接口发送
        //-8	不可使用该通道组
        //-9	帐户余额不足
        //-10	内部错误
        //-11	扣费失败
        /// <summary>
        /// 发送短信--莫名
        /// </summary>
        /// <param name="phoneNumber">手机号码</param>
        /// <param name="content">短信内容</param>
        public static string SendMessageNew(string phoneNumber, string content)
        {
            const string ac = "1001@800003390001";
            const string key = "E94B09FDD332F6121CB61498981998B1";
            //POST
            var sbTemp = new StringBuilder();
            sbTemp.Append("action=sendOnce&ac=" + ac + "&authkey=" + key + "&cgid=31&c=" + content + "&m=" + phoneNumber);
            var result = postSend("http://180.97.163.89:8012/OpenPlatform/OpenApi?", sbTemp.ToString());
            return result;
        }

        /// <summary>
        /// 发送短信--华兴软通
        /// </summary>
        /// <param name="phoneNumber"></param>
        /// <param name="content"></param>
        /// <returns></returns>
        public static string SendMessageNewHX(string phoneNumber, string content)
        {
            var myEncode = System.Text.Encoding.GetEncoding("UTF-8");

            //以下参数为所需要的参数，测试时请修改
            var strReg = "10113400-rtWEB-HUeeeeAX-604828";   //注册号（由华兴软通提供）
            var strPwd = "ZQOAKSFE";                 //密码（由华兴软通提供）
            var strSourceAdd = "";                   //子通道号，可为空（预留参数）
            var strPhone = phoneNumber;//手机号码，多个手机号用半角逗号分开，最多1000个
            var strContent = System.Web.HttpUtility.UrlEncode(content, myEncode);
            //短信内容
            //华兴软通发送短信地址
            var url = "http://www.stongnet.com/sdkhttp/sendsms.aspx";

            //要发送的内容
            var strSend = "reg=" + strReg + "&pwd=" + strPwd + "&sourceadd=" + strSourceAdd +
                             "&phone=" + strPhone + "&content=" + strContent;

            //发送
            var strRes = postSendHX(url, strSend);

            return strRes;
        }

        /// <summary>
        /// POST方式发送得结果--美联软通
        /// </summary>
        /// <param name="url"></param>
        /// <param name="bData"></param>
        /// <returns></returns>
        private static String PostRequest(string url, byte[] bData)
        {
            System.Net.HttpWebRequest hwRequest;
            System.Net.HttpWebResponse hwResponse;

            var strResult = string.Empty;
            try
            {
                hwRequest = (System.Net.HttpWebRequest)System.Net.WebRequest.Create(url);
                hwRequest.Timeout = 5000;
                hwRequest.Method = "POST";
                hwRequest.ContentType = "application/x-www-form-urlencoded";
                hwRequest.ContentLength = bData.Length;

                var smWrite = hwRequest.GetRequestStream();
                smWrite.Write(bData, 0, bData.Length);
                smWrite.Close();
            }
            catch (System.Exception err)
            {
                WriteErrLog(err.ToString());
                return strResult;
            }

            //get response
            try
            {
                hwResponse = (HttpWebResponse)hwRequest.GetResponse();
                var srReader = new StreamReader(hwResponse.GetResponseStream(), Encoding.ASCII);
                strResult = srReader.ReadToEnd();
                srReader.Close();
                hwResponse.Close();
            }
            catch (System.Exception err)
            {
                WriteErrLog(err.ToString());
            }

            return strResult;
        }

        /// <summary>
        /// 错误
        /// </summary>
        /// <param name="strErr"></param>
        private static void WriteErrLog(string strErr)
        {
            Console.WriteLine(strErr);
            System.Diagnostics.Trace.WriteLine(strErr);
        }

        /// <summary>
        /// /POST方式发送得结果---莫名
        /// </summary>
        /// <param name="url"></param>
        /// <param name="param"></param>
        /// <returns></returns>
        public static string postSend(string url, string param)
        {
            var myEncode = System.Text.Encoding.GetEncoding("UTF-8");
            var postBytes = System.Text.Encoding.UTF8.GetBytes(param);

            var req = (HttpWebRequest)HttpWebRequest.Create(url);
            req.Method = "POST";
            req.ContentType = "application/x-www-form-urlencoded;charset=UTF-8";
            req.ContentLength = postBytes.Length;

            try
            {
                using (var reqStream = req.GetRequestStream())
                {
                    reqStream.Write(postBytes, 0, postBytes.Length);
                }
                using (var res = req.GetResponse())
                {
                    using (var sr = new StreamReader(res.GetResponseStream(), myEncode))
                    {
                        var strResult = sr.ReadToEnd();
                        return strResult;
                    }
                }
            }
            catch (WebException ex)
            {
                return "无法连接到服务器\r\n错误信息：" + ex.Message;
            }

        }

        /// <summary>
        /// POST方式发送得结果--华兴软通
        /// </summary>
        /// <param name="url">服务器URL</param>
        /// <param name="param">要发送的参数字符串</param>
        /// <returns>服务器返回字符串</returns>
        public static string postSendHX(string url, string param)
        {
            var myEncode = System.Text.Encoding.GetEncoding("UTF-8");
            var postBytes = System.Text.Encoding.ASCII.GetBytes(param);

            var req = (HttpWebRequest)HttpWebRequest.Create(url);
            req.Method = "POST";
            req.ContentType = "application/x-www-form-urlencoded;charset=UTF-8";
            req.ContentLength = postBytes.Length;

            try
            {
                using (var reqStream = req.GetRequestStream())
                {
                    reqStream.Write(postBytes, 0, postBytes.Length);
                }
                using (var res = req.GetResponse())
                {
                    using (var sr = new StreamReader(res.GetResponseStream(), myEncode))
                    {
                        var strResult = sr.ReadToEnd();
                        return strResult;
                    }
                }
            }
            catch (WebException ex)
            {
                return "无法连接到服务器\r\n错误信息：" + ex.Message;
            }
        }
    }
}
