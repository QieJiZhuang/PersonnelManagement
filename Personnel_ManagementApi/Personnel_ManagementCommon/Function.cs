using System;
using System.IO;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;
using System.Drawing;
using System.Text.RegularExpressions;
using System.Web;
using System.Data;
using System.Net;
using System.Web.Security;


namespace DTcms.Common
{
    public class Function
    {
        /// <summary>
        /// 检测是否为英文字母 
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static bool CheckEnglish(string input)
        {
            if (string.IsNullOrEmpty(input)) { return false; }
            return Regex.IsMatch(input, @"^\w+$");
        }
        /// <summary>
        /// 检测是否为整数数字 
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static bool CheckInteger(string input)
        {
            if (string.IsNullOrEmpty(input)) { return false; }
            return Regex.IsMatch(input, @"^-?\d+$");
        }
        /// <summary>
        /// 检测是否为11数字（手机号） 
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static bool CheckIsMobilePhone(string input)
        {
            if (string.IsNullOrEmpty(input)) { return false; }
            return Regex.IsMatch(input, @"^(13|15|18)\d{9}$");
        }
        /// <summary>
        /// 检测用户名是否合法（汉字、大小写字母、数字、下划线）
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static bool CheckUserName(string input)
        {
            if (string.IsNullOrEmpty(input)) { return false; }
            return Regex.IsMatch(input, @"(^[a-zA-Z\u4e00-\u9fa5]{1}[\w]{1,29}$|^[a-zA-Z]$)");
        }

        /// <summary>
        /// 检测是否为整数数字 
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static bool IsNumeric(string str)
        {
            if (string.IsNullOrEmpty(str)) { return false; }
            try
            {
                int i = Convert.ToInt32(str);
                return true;
            }
            catch
            {
                return false;
            }
        }
        /// <summary>
        /// 检测是否为合法的网站地址URL格式，必须以http://或者https://开头 
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static bool CheckWebUrl(string input)
        {
            if (string.IsNullOrEmpty(input)) { return false; }
            return Regex.IsMatch(input, @"^(http|https)\://([a-zA-Z0-9\.\-]+(\:[a-zA-Z0-9\.&%\$\-]+)*@)*((25[0-5]|2[0-4][0-9]|[0-1]{1}[0-9]{2}|[1-9]{1}[0-9]{1}|[1-9])\.(25[0-5]|2[0-4][0-9]|[0-1]{1}[0-9]{2}|[1-9]{1}[0-9]{1}|[1-9]|0)\.(25[0-5]|2[0-4][0-9]|[0-1]{1}[0-9]{2}|[1-9]{1}[0-9]{1}|[1-9]|0)\.(25[0-5]|2[0-4][0-9]|[0-1]{1}[0-9]{2}|[1-9]{1}[0-9]{1}|[0-9])|localhost|([a-zA-Z0-9\-]+\.)*[a-zA-Z0-9\-]+\.(com|edu|gov|int|mil|net|org|biz|arpa|info|name|pro|aero|coop|museum|[a-zA-Z]{1,10}))(\:[0-9]+)*(/($|[a-zA-Z0-9\.\,\?\'\\\+&%\$#\=~_\-]+))*$");
        }
        /// <summary>
        /// 是否为邮箱地址 
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        public static bool IsEmail(string email)
        {
            if (string.IsNullOrEmpty(email)) { return false; }
            return Regex.IsMatch(email, @"^([a-zA-Z0-9_\-]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$");
        }


        /// <summary>
        /// 是否为ip 
        /// </summary>
        /// <param name="ip"></param>
        /// <returns></returns>
        public static bool IsIP(string ip)
        {
            if (string.IsNullOrEmpty(ip)) { return false; }
            return Regex.IsMatch(ip, @"^((2[0-4]\d|25[0-5]|[01]?\d\d?)\.){3}(2[0-4]\d|25[0-5]|[01]?\d\d?)$");
        }


        /// <summary>
        /// 检测是否为字母或者数字 
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static bool CheckLetterAndNumber(string input)
        {
            if (string.IsNullOrEmpty(input)) { return false; }
            return Regex.IsMatch(input, "^[A-Za-z0-9_]+$");
        }

        /// <summary>
        /// 检测是否为数字 
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static bool CheckNumber(string input)
        {
            if (string.IsNullOrEmpty(input)) { return false; }
            return Regex.IsMatch(input, @"^\d+$");
        }

        /// <summary>
        /// 检测是否为浮点数 
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static bool CheckFloat(string input)
        {
            if (string.IsNullOrEmpty(input)) { return false; }
            bool flag = Regex.IsMatch(input, @"^\d+[.]?\d*$");
            if (input.EndsWith("."))
            {
                flag = false;
            }
            return flag;
        }
        //D++++++++++++

        /// <summary>
        /// 检测非零数字 
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static bool CheckNumberNotZero(string input)
        {
            if (string.IsNullOrEmpty(input)) { return false; }
            return Regex.IsMatch(input, @"^[^0]\d*$");
        }

        /// <summary>
        /// 过滤html代码 
        /// </summary>
        /// <param name="Htmlstring"></param>
        /// <returns></returns>
        public static string LoseHtml(string Htmlstring)
        {
            //return Regex.Replace(Htmlstring, "<[^>]+>", "");
            //删除脚本   
            Htmlstring = Regex.Replace(Htmlstring, @"<script[^>]*?>.*?</script>", "", RegexOptions.IgnoreCase);
            //删除HTML   
            Htmlstring = Regex.Replace(Htmlstring, @"<(.[^>]*)>", "", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"([\r\n])[\s]+", "", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"-->", "", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"<!--.*", "", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"&(quot|#34);", "\"", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"&(amp|#38);", "&", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"&(lt|#60);", "<", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"&(gt|#62);", ">", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"&(nbsp|#160);", "   ", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"&(iexcl|#161);", "\xa1", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"&(cent|#162);", "\xa2", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"&(pound|#163);", "\xa3", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"&(copy|#169);", "\xa9", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"&#(\d+);", "", RegexOptions.IgnoreCase);
            Htmlstring.Replace("&ldquo;", "“").Replace("&rdquo;", "”");
            Htmlstring.Replace("&mdash;", "—");
            Htmlstring.Replace("&gt;", ">");
            Htmlstring.Replace("&lt;", "<");
            Htmlstring.Replace("&nbsp;", " ");
            Htmlstring.Replace("&quot;", "\"");
            Htmlstring.Replace("<", "");
            Htmlstring.Replace(">", "");
            Htmlstring.Replace("\r\n", "");
            //Htmlstring = HttpContext.Current.Server.HtmlEncode(Htmlstring).Trim();
            return Htmlstring;
        }


        public static string Decode(string strHtml)
        {
            strHtml = strHtml.Replace("<br>", "\n");
            strHtml = strHtml.Replace("&gt;", ">");
            strHtml = strHtml.Replace("&lt;", "<");
            strHtml = strHtml.Replace("&nbsp;", " ");
            strHtml = strHtml.Replace("&quot;", "\"");
            return strHtml;
        }
        public static string Encode(string strHtml)
        {
            strHtml = strHtml.Replace("\"", "&quot;");
            strHtml = strHtml.Replace("  ", " &nbsp;");
            strHtml = strHtml.Replace("<", "&lt;");
            strHtml = strHtml.Replace(">", "&gt;");
            strHtml = strHtml.Replace("\n", "<br>");
            return strHtml;
        }


        /// <summary>
        /// 判断指定字符串在字符串中出现的次数 
        /// </summary>
        /// <param name="ResourceString">源字符串</param>
        /// <param name="objectString">要判断的字符串</param>
        /// <returns></returns>
        public static int GetStringInString(string ResourceString, string objectString)
        {
            return ResourceString.Split(new string[] { objectString }, StringSplitOptions.None).Length - 1;
        }
        /// <summary>
        /// 获取日期格式名称，返回类似200905121613223 
        /// </summary>
        /// <returns></returns>
        public static string GetFileName()
        {
            Random random = new Random();
            StringBuilder builder = new StringBuilder();
            builder.Append(DateTime.Now.ToString("yyyyMMddHHmmss"));
            builder.Append(random.Next(0x186a0, 0xf423f).ToString());
            return builder.ToString();
        }

        /// <summary>
        /// 将对象转换为Int32类型
        /// </summary>
        /// <param name="strValue">要转换的字符串</param>
        /// <param name="defValue">缺省值</param>
        /// <returns>转换后的int类型结果</returns>
        public static int StrToInt(object Expression, int defValue)
        {

            if (Expression != null)
            {
                string str = Expression.ToString();
                if (str.Length > 0 && str.Length <= 11 && Regex.IsMatch(str, @"^[-]?[0-9]*$"))
                {
                    if ((str.Length < 10) || (str.Length == 10 && str[0] == '1') || (str.Length == 11 && str[0] == '-' && str[1] == '1'))
                    {
                        return Convert.ToInt32(str);
                    }
                }
            }
            return defValue;
        }

        
        /// <summary>
        /// 获取数字字母随机数 
        /// </summary>
        /// <param name="length"></param>
        /// <returns></returns>
        public static string GetRndNum(int length)
        {
            Random random = new Random();
            string[] strArray = "0,1,2,3,4,5,6,7,8,9,a,b,c,d,e,f,g,h,i,j,k,l,m,n,o,p,q,r,s,t,u,v,w,x,y,z".Split(new char[] { ',' });
            StringBuilder builder = new StringBuilder();
            for (int i = 0; i < length; i++)
            {
                builder.Append(strArray[random.Next(strArray.Length)]);
            }
            return builder.ToString();
        }

        /// <summary>
        /// 获取数字随机数 
        /// </summary>
        /// <param name="length"></param>
        /// <returns></returns>
        public static string GetRndNumber(int length)
        {
            return GetRndNumber("0,1,2,3,4,5,6,7,8,9", length);
        }
        /// <summary>
        /// 获取数字随机数 
        /// </summary>
        /// <param name="numbers">如0,1,2,3,4,5,6,7,8,9</param>
        /// <param name="length">长度</param>
        /// <returns></returns>
        public static string GetRndNumber(string numbers, int length)
        {
            Random random = new Random();
            string[] strArray = numbers.Split(new char[] { ',', '，', '|', ' ', '_' });
            StringBuilder builder = new StringBuilder();
            for (int i = 0; i < length; i++)
            {
                builder.Append(strArray[random.Next(strArray.Length)]);
            }
            return builder.ToString();
        }
        /// <summary>
        /// 获取一个小于指定值的非负数随机数 
        /// </summary>
        /// <param name="maxValue"></param>
        /// <returns></returns>
        public static int GetRandNumber(int maxValue)
        {
            Random random = new Random();
            return random.Next(maxValue);
        }


        /// <summary>
        /// 获取随机时间
        /// <remarks>
        /// 由于Random 以当前系统时间做为种值,所以当快速运行多次该方法所得到的结果可能相同,
        /// 这时,你应该在外部初始化 Random 实例并调用 GetRandomTime(DateTime time1, DateTime time2, Random random)
        /// </remarks>
        /// </summary>
        /// <param name="time1"></param>
        /// <param name="time2"></param>
        /// <returns></returns>
        public static DateTime GetRandomTime(DateTime time1, DateTime time2)
        {
            Random random = new Random();
            return GetRandomTime(time1, time2, random);
        }

        /// <summary>
        /// 获取随机时间
        /// </summary>
        /// <param name="time1"></param>
        /// <param name="time2"></param>
        /// <param name="random"></param>
        /// <returns></returns>
        public static DateTime GetRandomTime(DateTime time1, DateTime time2, Random random)
        {
            DateTime minTime = new DateTime();
            DateTime maxTime = new DateTime();
            System.TimeSpan ts = new System.TimeSpan(time1.Ticks - time2.Ticks);
            // 获取两个时间相隔的秒数
            double dTotalSecontds = ts.TotalSeconds;
            int iTotalSecontds = 0;
            if (dTotalSecontds > System.Int32.MaxValue)
            {
                iTotalSecontds = System.Int32.MaxValue;
            }
            else if (dTotalSecontds < System.Int32.MinValue)
            {
                iTotalSecontds = System.Int32.MinValue;
            }
            else
            {
                iTotalSecontds = (int)dTotalSecontds;
            }
            if (iTotalSecontds > 0)
            {
                minTime = time2;
                maxTime = time1;
            }
            else if (iTotalSecontds < 0)
            {
                minTime = time1;
                maxTime = time2;
            }
            else
            {
                return time1;
            }
            int maxValue = iTotalSecontds;
            if (iTotalSecontds <= System.Int32.MinValue)
            { maxValue = System.Int32.MinValue + 1; }
            int i = random.Next(System.Math.Abs(maxValue));
            return minTime.AddSeconds(i);
        }


        public static string[] GetSplit(string strOriginal, string strSymbol)
        {
            return strOriginal.Split(new char[] { Convert.ToChar(strSymbol) });
        }


        /// <summary>
        /// 根据一个字节数返回返回xx Byte或者xx K 或者xx M  
        /// </summary>
        /// <param name="bytes"></param>
        /// <returns></returns>
        public static string GetStrByByte(string bytes)
        {
            double num3;
            double num = 1024.0;
            double length = double.Parse(bytes);
            if (length < 1024.0)
            {
                return (length.ToString() + " Byte");
            }
            if ((length / num) < 1024.0)
            {
                num3 = length / num;
                return (num3.ToString("0.00") + " K");
            }
            num3 = (length / num) / num;
            return (num3.ToString("0.00") + " M");
        }


        public static string HtmlEncode(object s)
        {
            if ((s != null) && (s.ToString() != ""))
            {
                return HtmlEncode(s.ToString());
            }
            return "";
        }

        public static string HtmlEncode(string input)
        {
            return HttpUtility.HtmlEncode(input);
        }
        public static string HtmlDecode(string input)
        {
            return HttpUtility.HtmlDecode(input);
        }
        public static bool IsDate(string dateStr)
        {
            try
            {
                DateTime.Parse(dateStr);
                return true;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// MD5加密（32位小写） 
        /// </summary>
        /// <param name="pass"></param>
        /// <returns></returns>
        public static string MD5Encrypt(string pass)
        {
            return FormsAuthentication.HashPasswordForStoringInConfigFile(pass, "MD5").ToLower();
        }

        /// <summary>
        /// MD5加密（小写） 
        /// </summary>
        /// <param name="pass"></param>
        /// <param name="code">16为16位，否则为32位</param>
        /// <returns></returns>
        public static string MD5Encrypt(string pass, int code)
        {
            if (code == 16) //16位MD5加密（取32位加密的9~25字符）
            {
                return FormsAuthentication.HashPasswordForStoringInConfigFile(pass, "MD5").ToLower().Substring(8, 16);
            }
            else//32位加密
            {
                return FormsAuthentication.HashPasswordForStoringInConfigFile(pass, "MD5").ToLower();
            }
        }


        public static bool ReadTempCookies(string CookiesName, string CookiesValue)
        {
            return ((HttpContext.Current.Request.Cookies[CookiesName] != null) && (HttpContext.Current.Request.Cookies[CookiesName].Value == CookiesValue));
        }

        public static void SaveTempCookies(string CookiesName, string CookiesValue)
        {
            HttpCookie cookie = new HttpCookie(CookiesName, CookiesValue);
            cookie.Expires = DateTime.Now.AddHours(2.0);
            HttpContext.Current.Response.Cookies.Add(cookie);
        }

        /// <summary>
        /// 屏蔽危险sql字符 
        /// </summary>
        /// <param name="Htmlstring"></param>
        /// <returns></returns>
        public static string SqlFiltrate(string Htmlstring)
        {
            if (Htmlstring == null) { return ""; }
            //删除脚本
            Htmlstring = Regex.Replace(Htmlstring, @"<script[^>]*?>.*?</script>", "", RegexOptions.IgnoreCase);
            //删除HTML
            Htmlstring = Regex.Replace(Htmlstring, @"<(.[^>]*)>", "", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"([\r\n])[\s]+", "", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"-->", "", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"<!--.*", "", RegexOptions.IgnoreCase);

            Htmlstring = Regex.Replace(Htmlstring, @"&(quot|#34);", "\"", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"&(amp|#38);", "&", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"&(lt|#60);", "<", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"&(gt|#62);", ">", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"&(nbsp|#160);", " ", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"&(iexcl|#161);", "\xa1", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"&(cent|#162);", "\xa2", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"&(pound|#163);", "\xa3", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"&(copy|#169);", "\xa9", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"&#(\d+);", "", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, "xp_cmdshell", "", RegexOptions.IgnoreCase);

            //删除与数据库相关的词
            Htmlstring = Regex.Replace(Htmlstring, "select", "", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, "insert", "", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, "delete from", "", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, "count''", "", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, "drop table", "", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, "truncate", "", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, "asc", "", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, "mid", "", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, "char", "", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, "xp_cmdshell", "", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, "exec master", "", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, "net localgroup administrators", "", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, "and", "", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, "net user", "", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, "or", "", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, "net", "", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, "delete", "", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, "drop", "", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, "script", "", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, "%20", "", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, "%", "", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, "0x", "ox", RegexOptions.IgnoreCase);

            //特殊的字符
            Htmlstring = Htmlstring.Replace("<", "");
            Htmlstring = Htmlstring.Replace(">", "");
            Htmlstring = Htmlstring.Replace("*", "");
            Htmlstring = Htmlstring.Replace("%", "");
            Htmlstring = Htmlstring.Replace("?", "");
            Htmlstring = Htmlstring.Replace(",", "");
            Htmlstring = Htmlstring.Replace("/", "");
            Htmlstring = Htmlstring.Replace(";", "");
            Htmlstring = Htmlstring.Replace("*/", "");
            Htmlstring = Htmlstring.Replace("\r\n", "");
            Htmlstring = Htmlstring.Replace("'", "");
            Htmlstring = Htmlstring.Replace("&", "＆");
            Htmlstring = Htmlstring.Replace("'", "");
            Htmlstring = HttpContext.Current.Server.HtmlEncode(Htmlstring).Trim();
            return Htmlstring;
        }


        public static string SubStr(string input, int length)
        {
            if (input.Length > length)
            {
                return input.Substring(0, length);
            }
            return input;
        }

        /// <summary>
        /// 截取字符，如果超出指定字数，将附加超出字符(通常是省略号) 
        /// </summary>
        /// <param name="input"></param>
        /// <param name="length"></param>
        /// <param name="outString"></param>
        /// <returns></returns>
        public static string SubStr(string input, int length, string outString)
        {
            if (input.Length > length)
            {
                return input.Substring(0, length) + outString;
            }
            return input;
        }

        /// <summary>
        /// 获取字符串字节数 
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static int GetBytes(string str)
        {
            byte[] bytestr = System.Text.Encoding.Unicode.GetBytes(str);
            int j = 0;
            for (int i = 0; i < bytestr.GetLength(0); i++)
            {
                if (i % 2 == 0)
                {
                    j++;
                }
                else
                {
                    if (bytestr[i] > 0)
                    {
                        j++;
                    }
                }
            }
            return j;
        }
        ///   <summary>   
        ///   截取字符串(适用于中英文混合)    
        ///   </summary>   
        ///   <param   name="str">原字符串</param>   
        ///   <param   name="len">长度</param>   
        ///   <returns></returns>   
        public static string SubStrByByte(string str, int len)
        {
            str = str.Trim();
            byte[] myByte = System.Text.Encoding.Default.GetBytes(str);
            if (myByte.Length > len)
            {
                string result = "";
                for (int i = 0; i < str.Length; i++)
                {
                    byte[] tempByte = System.Text.Encoding.Default.GetBytes(result);
                    if (tempByte.Length < len)
                    {
                        result += str.Substring(i, 1);
                    }
                    else
                    {
                        break;
                    }
                }
                return result + "...";
            }
            else
            {
                return str;
            }
        }
        public static string UrlDecode(string input)
        {
            return HttpUtility.UrlDecode(input);
        }

        public static string UrlEncode(string input)
        {
            return HttpUtility.UrlEncode(input);
        }

        //D++++++++++++++++++++++++++数组排序
        public static void SortInt(ref int[] list)
        {
            int i, j, temp;
            bool done = false;
            j = 1;
            while ((j < list.Length) && (!done))
            {
                done = true;
                for (i = 0; i < list.Length - j; i++)
                {
                    if (list[i] > list[i + 1])
                    {
                        done = false;
                        temp = list[i];
                        list[i] = list[i + 1];
                        list[i + 1] = temp;
                    }
                }
                j++;
            }
        }

        public static void SortFloat(ref float[] list)
        {
            int i, j;
            float temp;
            bool done = false;
            j = 1;
            while ((j < list.Length) && (!done))
            {
                done = true;
                for (i = 0; i < list.Length - j; i++)
                {
                    if (list[i] > list[i + 1])
                    {
                        done = false;
                        temp = list[i];
                        list[i] = list[i + 1];
                        list[i + 1] = temp;
                    }
                }
                j++;
            }
        }


        /// <summary>
        /// 获得当前页面客户端的IP  
        /// </summary>
        /// <returns>当前页面客户端的IP</returns>
        public static string GetIP()
        {
            string result = String.Empty;
            result = HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
            if (string.IsNullOrEmpty(result))
            {
                result = HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"];
            }
            if (string.IsNullOrEmpty(result))
            {
                result = HttpContext.Current.Request.UserHostAddress;
            }
            if (string.IsNullOrEmpty(result) || !IsIP(result))
            {
                return "127.0.0.1";
            }
            return result;
        }

        /// <summary>
        /// 屏蔽IP地址最后len位为星号 
        /// </summary>
        /// <param name="ip">合法的IP地址，如：192.168.0.1</param>
        /// <param name="len">屏蔽最后几位（0到4的整数），0为不屏蔽；如果为2，则输出：192.168.*.*</param>
        /// <returns></returns>
        public static string GetIP(string ip, int len)
        {
            if (IsIP(ip))
            {
                if (len >= 4) { return "*.*.*.*"; }
                if (len <= 0) { return ip; }
                string[] IParr = ip.Split('.');
                switch (len)
                {
                    case 1: return IParr[0] + "." + IParr[1] + "." + IParr[2] + ".*";
                    case 2: return IParr[0] + "." + IParr[1] + ".*.*";
                    case 3: return IParr[0] + ".*.*.*";
                }
                return ip;
            }
            return ip;
        }


        #region 判断日期

        /// <summary>
        /// 判断日期的全部格式（yyyy-MM-dd HH:mm:ss） 
        /// </summary>
        /// <param name="dateStr">输入日期的字符串</param>
        /// <returns></returns>
        public bool isDateTimeLong(string dateStr)
        {
            return Regex.IsMatch(dateStr, @"^(((((1[6-9]|[2-9]\d)\d{2})-(0?[13578]|1[02])-(0?[1-9]|[12]\d|3[01]))|(((1[6-9]|[2-9]\d)\d{2})-(0?[13456789]|1[012])-(0?[1-9]|[12]\d|30))|(((1[6-9]|[2-9]\d)\d{2})-0?2-(0?[1-9]|1\d|2[0-8]))|(((1[6-9]|[2-9]\d)(0[48]|[2468][048]|[13579][26])|((16|[2468][048]|[3579][26])00))-0?2-29-)) (20|21|22|23|[0-1]?\d):[0-5]?\d:[0-5]?\d)$");

        }

        /// <summary>
        /// 判断日期的全部格式（不带秒）（yyyy-MM-dd HH:mm）

        /// </summary>
        /// <param name="dateStr">输入日期的字符串</param>
        /// <returns></returns>
        public bool isDateTimeShort(string dateStr)
        {
            return Regex.IsMatch(dateStr, @"^(((((1[6-9]|[2-9]\d)\d{2})-(0?[13578]|1[02])-(0?[1-9]|[12]\d|3[01]))|(((1[6-9]|[2-9]\d)\d{2})-(0?[13456789]|1[012])-(0?[1-9]|[12]\d|30))|(((1[6-9]|[2-9]\d)\d{2})-0?2-(0?[1-9]|1\d|2[0-8]))|(((1[6-9]|[2-9]\d)(0[48]|[2468][048]|[13579][26])|((16|[2468][048]|[3579][26])00))-0?2-29-)) (20|21|22|23|[0-1]?\d):[0-5]?\d)$");
        }

        /// <summary>
        /// 判断日期的日期部分格式（yyyy-MM-dd）

        /// </summary>
        /// <param name="dateStr">输入的日期的日期部分字符串</param>
        /// <returns>bool</returns>
        public bool isDateShort(string dateStr)
        {
            return Regex.IsMatch(dateStr, @"^((((1[6-9]|[2-9]\d)\d{2})-(0?[13578]|1[02])-(0?[1-9]|[12]\d|3[01]))|(((1[6-9]|[2-9]\d)\d{2})-(0?[13456789]|1[012])-(0?[1-9]|[12]\d|30))|(((1[6-9]|[2-9]\d)\d{2})-0?2-(0?[1-9]|1\d|2[0-9]))|(((1[6-9]|[2-9]\d)(0[48]|[2468][048]|[13579][26])|((16|[2468][048]|[3579][26])00))-0?2-29-))$");

        }

        /// <summary>
        /// 判断日期的时间部分格式，带秒（HH:mm:ss）  
        /// </summary>
        /// <param name="dateStr">输入日期的时间部分字符串</param>
        /// <returns>bool</returns>
        public bool isTimeLong(string dateStr)
        {
            return Regex.IsMatch(dateStr, @"^((20|21|22|23|[0-1]?\d):[0-5]?\d:[0-5]?\d)$");
        }

        /// <summary>
        /// 判断日期的时间部分格式，没有秒（HH:mm） 
        /// </summary>
        /// <param name="dateStr">输入日期的时间部分字符串</param>
        /// <returns>bool</returns>
        public bool isTimeShort(string dateStr)
        {
            return Regex.IsMatch(dateStr, @"^((20|21|22|23|[0-1]?\d):[0-5]?\d)$");
        }



        #endregion 判断日期


        /// <summary>
        /// 返回CheckBoxList的所有选中的值，返回类似1,2,3,4,5 
        /// </summary>
        /// <param name="cb">CheckBoxList控件</param>
        /// <returns></returns>
        public static string getCheckBoxListSelectValues(System.Web.UI.WebControls.CheckBoxList cb)
        {
            StringBuilder sb = new StringBuilder();
            string result = string.Empty;
            for (int i = 0; i < cb.Items.Count; i++)
            {
                if (cb.Items[i].Selected) { sb.Append(cb.Items[i].Value + ","); }
            }
            result = sb.ToString();
            return result.TrimEnd(',');
        }


        #region 格式化文件大小，输出G,M,K,bytes
        /// <summary>
        /// 格式化文件大小，输出G,M,K,bytes 
        /// </summary>
        /// <param name="fileSize"></param>
        /// <returns></returns>
        public static String FormatFileSize(Int64 fileSize)
        {
            if (fileSize < 0)
            {
                throw new ArgumentOutOfRangeException("fileSize");
            }
            else if (fileSize >= 1024 * 1024 * 1024)
            {
                return string.Format("{0:########0.00} G", ((Double)fileSize) / (1024 * 1024 * 1024));
            }
            else if (fileSize >= 1024 * 1024)
            {
                return string.Format("{0:####0.00} M", ((Double)fileSize) / (1024 * 1024));
            }
            else if (fileSize >= 1024)
            {
                return string.Format("{0:####0.00} K", ((Double)fileSize) / 1024);
            }
            else
            {
                return string.Format("{0} bytes", fileSize);
            }
        }
        #endregion

        /// <summary>
        /// 去处字符串中首尾指定的字符 
        /// </summary>
        /// <param name="str"></param>
        /// <param name="charcontent"></param>
        /// <returns></returns>
        public static string DelStartAndEndChar(string str, char charcontent)
        {
            return str.TrimEnd(charcontent);
        }


        /// <summary>
        /// 格式化日期为多少小时前等 
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public static string FormatDateString(string date)
        {
            DateTime dt = DateTime.Parse(date);
            TimeSpan span = DateTime.Now - dt;
            if (span.TotalDays > 60) { return dt.ToShortDateString(); }
            else
            {
                if (span.TotalDays > 30) { return "1个月前"; }
                else
                {
                    if (span.TotalDays > 14) { return "2周前"; }
                    else
                    {
                        if (span.TotalDays > 7) { return "1周前"; }
                        else
                        {
                            if (span.TotalDays > 1)
                            {
                                return string.Format("{0}天前", (int)Math.Floor(span.TotalDays));
                            }
                            else
                            {
                                if (span.TotalHours > 1)
                                {
                                    return string.Format("{0}小时前", (int)Math.Floor(span.TotalHours));
                                }
                                else
                                {
                                    if (span.TotalMinutes > 1)
                                    {
                                        return string.Format("{0}分钟前", (int)Math.Floor(span.TotalMinutes));
                                    }
                                    else
                                    {
                                        if (span.TotalSeconds >= 1)
                                        {
                                            return string.Format("{0}秒前", (int)Math.Floor(span.TotalSeconds));
                                        }
                                        return "1秒前";
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }
        /// <summary>
        /// 获取来源页面
        /// </summary>
        /// <returns></returns>
        public static string GetRefer()
        {
            if (HttpContext.Current.Request.UrlReferrer != null)
            {
                return HttpContext.Current.Request.UrlReferrer.ToString();
            }
            return string.Empty;
        }

        /// <summary>
        /// 删除文件
        /// </summary>
        /// <param name="filePath">物理地址</param>
        public static void DelFile(string filePath)
        {
            try { File.Delete(filePath); }
            catch { }
        }
        /// <summary>
        /// 判断链接是否来自自身服务器页面，该方法用来判断非法post\get\防盗链  
        /// </summary>
        /// <returns></returns>
        public static bool IsSelftServer()
        {
            string str1 = System.Web.HttpContext.Current.Request.ServerVariables["HTTP_REFERER"];
            string str2 = System.Web.HttpContext.Current.Request.ServerVariables["SERVER_NAME"];
            return ((str1 != null) && (str1.IndexOf(str2) == 7));
        }

    }
}