using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Web.Security;

namespace Traffic.Utility
{
    /// <summary>
    /// Description: 安全相关操作类
    /// Version: 1.0
    /// Created: 2014/12/17
    /// Author:  zlf
    /// Company: 北京博奥中成信息科技有限责任公司
    /// 
    /// ModifyEditTime: 修改时间
    /// ModifyContent:  修改内容
    /// ModifyPerson :  修改人
    /// </summary>
    public static class SecurityHelper
    {
        /// <summary>
        /// MD5加密
        /// </summary>
        /// <param name="str">被加密的字符串</param>
        /// <returns>返回加密后的字符串大写</returns>
        public static string Md5(string str)
        {
            var md5Hash = MD5.Create();
            var data = md5Hash.ComputeHash(Encoding.UTF8.GetBytes(str));
            var sBuilder = new StringBuilder();
            for (var i = 0; i < data.Length; i++)
            {
                sBuilder.Append(data[i].ToString("x2"));
            }
            return sBuilder.ToString();
        }

        /// <summary>
        /// 创建双向加密键值（密钥）
        /// </summary>
        /// <returns></returns>
        public static string GenerateKey()
        {
            var desCrypto = (DESCryptoServiceProvider)DES.Create();
            return Encoding.ASCII.GetString(desCrypto.Key);
        }

        /// <summary>
        /// 双向加密方法 — 加密
        /// </summary>
        /// <param name="pToEncrypt">加密字符串</param>
        /// <param name="sKey">密钥（长度为8）</param>
        /// <returns></returns>
        public static string Encrypt(string pToEncrypt, string sKey)
        {
            try
            {
                var des = new DESCryptoServiceProvider();
                var inputByteArray = Encoding.Default.GetBytes(pToEncrypt);

                //建立加密对象的密钥和偏移量
                //原文使用ASCIIEncoding.ASCII方法的GetBytes方法
                //使得输入密码必须输入英文文本
                des.Key = Encoding.ASCII.GetBytes(sKey);
                des.IV = Encoding.ASCII.GetBytes(sKey);
                var ms = new MemoryStream();
                var cs = new CryptoStream(ms, des.CreateEncryptor(), CryptoStreamMode.Write);
                cs.Write(inputByteArray, 0,
                    inputByteArray.Length);
                cs.FlushFinalBlock();
                var ret = new StringBuilder();
                foreach (byte b in ms.ToArray())
                {
                    ret.AppendFormat("{0:X2}", b);
                }
                return ret.ToString();
            }
            catch
            {
                return "";
            }
        }

        /// <summary>
        /// 双向加密方法 — 解密
        /// </summary>
        /// <param name="pToDecrypt">解密字符串</param>
        /// <param name="sKey">密钥（长度为8）</param>
        /// <returns></returns>
        public static string Decrypt(string pToDecrypt, string sKey)
        {
            try
            {
                var des = new DESCryptoServiceProvider();
                var inputByteArray = new byte
                    [pToDecrypt.Length / 2];
                for (var x = 0; x < pToDecrypt.Length / 2; x++)
                {
                    var i = (Convert.ToInt32(pToDecrypt.Substring(x * 2, 2), 16));
                    inputByteArray[x] = (byte)i;
                }

                //建立加密对象的密钥和偏移量，此值重要，不能修改
                des.Key = Encoding.ASCII.GetBytes(sKey);
                des.IV = Encoding.ASCII.GetBytes(sKey);
                var ms = new MemoryStream();
                var cs = new CryptoStream(ms, des.CreateDecryptor(), CryptoStreamMode.Write);
                cs.Write(inputByteArray, 0, inputByteArray.Length);
                cs.FlushFinalBlock();

                //建立StringBuild对象，CreateDecrypt使用的是流对象，必须把解密后的文本变成流对象
                return Encoding.Default.GetString(ms.ToArray());
            }
            catch
            {
                return string.Empty;
            }
        }

        /// <summary>
        /// 转换危险字符串（常用语扰乱页面代码的html标签的处理，主要针对尖括号等可执行符号）
        /// </summary>
        /// <param name="content">要过滤的字符串</param>
        /// <returns></returns>
        public static string FilterChars(string content)
        {
            content = content.Replace("'", "&#39"); //过滤单引号
            content = content.Replace("\"", "&#34"); //过滤双引号
            content = content.Replace("<", "&#60"); //过滤左尖括号
            content = content.Replace(">", "&#62"); //过滤右尖括号
            content = content.Replace(";", "&#59"); //过滤分号
            content = Regex.Replace(content, @"net user", "", RegexOptions.IgnoreCase);
            content = Regex.Replace(content, @"administrators", "", RegexOptions.IgnoreCase);
            content = Regex.Replace(content, @"master", "", RegexOptions.IgnoreCase);
            content = Regex.Replace(content, @"exec", "", RegexOptions.IgnoreCase);
            content = Regex.Replace(content, @"select", "", RegexOptions.IgnoreCase);
            content = Regex.Replace(content, @"drop", "", RegexOptions.IgnoreCase);
            content = Regex.Replace(content, @"insert", "", RegexOptions.IgnoreCase);
            content = Regex.Replace(content, @"delete", "", RegexOptions.IgnoreCase);
            content = Regex.Replace(content, @"update", "", RegexOptions.IgnoreCase);
            return content.Trim();
        }

        /// <summary>
        /// 过滤script标记和iframe标记
        /// </summary>
        /// <param name="html">要过滤的字符串</param>
        /// <returns></returns>
        public static string FilterScript(string html)
        {
            var regex = new Regex[11];
            const RegexOptions options = RegexOptions.Singleline | RegexOptions.IgnoreCase;
            regex[0] = new Regex(@"<marquee[\s\S]+</marquee *>", options);
            regex[1] = new Regex(@"<script[\s\S]+</script *>", options);
            regex[2] = new Regex(@"href *= *[\s\S]*script *:", options);
            regex[3] = new Regex(@"<iframe[\s\S]+</iframe *>", options);
            regex[4] = new Regex(@"<frameset[\s\S]+</frameset *>", options);
            regex[5] = new Regex(@"<input[\s\S]+</input *>", options);
            regex[6] = new Regex(@"<button[\s\S]+</button *>", options);
            regex[7] = new Regex(@"<select[\s\S]+</select *>", options);
            regex[8] = new Regex(@"<textarea[\s\S]+</textarea *>", options);
            regex[9] = new Regex(@"<form[\s\S]+</form *>", options);
            regex[10] = new Regex(@"(?s)on[/s/S]*=", options);
            for (var i = 0; i < regex.Length - 1; i++)
            {
                foreach (Match match in regex[i].Matches(html))
                {
                    html = html.Replace(match.Groups[0].ToString(), "");
                }
            }
            return html;
        }

        /// <summary>
        /// 过滤HTML标签
        /// </summary>
        /// <param name="htmlstring">内容</param>
        /// <returns></returns>
        public static string FilterHtml(string htmlstring)
        {
            //删除脚本   
            htmlstring = Regex.Replace(htmlstring, @"<script[^>]*?>.*?</script>", "", RegexOptions.IgnoreCase);
            //删除样式
            htmlstring = Regex.Replace(htmlstring, @"<style[^>]*?>.*?</style>", "", RegexOptions.IgnoreCase);
            //删除HTML   
            htmlstring = Regex.Replace(htmlstring, @"<(.[^>]*)>", "", RegexOptions.IgnoreCase);
            htmlstring = Regex.Replace(htmlstring, @"([\r\n])[\s]+", "", RegexOptions.IgnoreCase);
            htmlstring = Regex.Replace(htmlstring, @"-->", "", RegexOptions.IgnoreCase);
            htmlstring = Regex.Replace(htmlstring, @"<!--.*", "", RegexOptions.IgnoreCase);
            htmlstring = Regex.Replace(htmlstring, @"&(quot|#34);", "\"", RegexOptions.IgnoreCase);
            htmlstring = Regex.Replace(htmlstring, @"&(amp|#38);", "&", RegexOptions.IgnoreCase);
            htmlstring = Regex.Replace(htmlstring, @"&(lt|#60);", "<", RegexOptions.IgnoreCase);
            htmlstring = Regex.Replace(htmlstring, @"&(gt|#62);", ">", RegexOptions.IgnoreCase);
            htmlstring = Regex.Replace(htmlstring, @"&(nbsp|#160);", " ", RegexOptions.IgnoreCase);
            htmlstring = Regex.Replace(htmlstring, @"&(iexcl|#161);", "\xa1", RegexOptions.IgnoreCase);
            htmlstring = Regex.Replace(htmlstring, @"&(cent|#162);", "\xa2", RegexOptions.IgnoreCase);
            htmlstring = Regex.Replace(htmlstring, @"&(pound|#163);", "\xa3", RegexOptions.IgnoreCase);
            htmlstring = Regex.Replace(htmlstring, @"&(copy|#169);", "\xa9", RegexOptions.IgnoreCase);
            htmlstring = Regex.Replace(htmlstring, @"&#(\d+);", "", RegexOptions.IgnoreCase);
            htmlstring = Regex.Replace(htmlstring, @"<img[^>]*>;", "", RegexOptions.IgnoreCase);
            htmlstring = htmlstring.Replace("<", "");
            htmlstring = htmlstring.Replace(">", "");
            htmlstring = htmlstring.Replace("\r\n", "");
            htmlstring = System.Web.HttpContext.Current.Server.HtmlEncode(htmlstring).Trim();
            return htmlstring;
        }

        /// <summary>
        /// 生成随机字母字符串(数字字母混和)
        /// </summary>
        /// <param name="codeCount">待生成的位数</param>
        /// <returns>生成的字母字符串</returns>
        public static string GenerateCheckCode(int codeCount)
        {
            var rep = 0;
            var str = string.Empty;
            var num2 = DateTime.Now.Ticks + rep;
            rep++;
            var random = new Random(((int)(((ulong)num2) & 0xffffffffL)) | ((int)(num2 >> rep)));
            for (var i = 0; i < codeCount; i++)
            {
                char ch;
                var num = random.Next();
                if ((num % 2) == 0)
                {
                    ch = (char)(0x30 + ((ushort)(num % 10)));
                }
                else
                {
                    ch = (char)(0x41 + ((ushort)(num % 0x1a)));
                }
                str = str + ch;
            }
            return str;
        }

        /// <summary>
        /// 过滤SQL字符。
        /// </summary>
        /// <param name="str">要过滤SQL字符的字符串。</param>
        /// <returns>已过滤掉SQL字符的字符串。</returns>
        public static string ReplaceSqlChar(string str)
        {
            if (str == String.Empty)
            {
                return String.Empty;
            }
            str = str.Replace("'", "‘");
            str = str.Replace(";", "；");
            str = str.Replace(",", ",");
            str = str.Replace("?", "?");
            str = str.Replace("<", "＜");
            str = str.Replace(">", "＞");
            str = str.Replace("(", "(");
            str = str.Replace(")", ")");
            str = str.Replace("@", "＠");
            str = str.Replace("=", "＝");
            str = str.Replace("+", "＋");
            str = str.Replace("*", "＊");
            str = str.Replace("&", "＆");
            str = str.Replace("#", "＃");
            str = str.Replace("%", "％");
            str = str.Replace("$", "￥");
            str = str.Replace("/", "／");
            return str;
        }
    }
}
