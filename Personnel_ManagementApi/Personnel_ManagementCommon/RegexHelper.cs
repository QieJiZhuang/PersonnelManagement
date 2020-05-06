using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Traffic.Utility
{
    /// <summary>
    /// 正则表达式基类
    /// </summary>
    public static class RegexHelper
    {
        /// <summary>
        /// 返回正则对象所有结果
        /// </summary>
        /// <param name="htmlString"></param>
        /// <param name="regexString"></param>
        /// <returns></returns>
        public static MatchCollection GetMatchRegex(string htmlString, string regexString)
        {
            return Regex.Matches(htmlString, regexString, RegexOptions.IgnoreCase);
        }

        /// <summary>
        /// 返回单个正则分析结果
        /// </summary>
        /// <param name="htmlString"></param>
        /// <param name="regexString"></param>
        /// <returns></returns>
        public static string GetMatchregexString(string htmlString, string regexString)
        {
            var mc = Regex.Matches(htmlString, regexString, RegexOptions.IgnoreCase);
            if (mc.Count > 0)
            {
                return mc[0].Groups[1].ToString();
            }
            return null;
        }

        /// <summary>
        /// 根据正则删除表达式所匹配的内容
        /// </summary>
        /// <param name="htmlString"></param>
        /// <param name="regexString"></param>
        /// <returns></returns>
        public static string Clear(string htmlString, string regexString)
        {
            return Regex.Replace(htmlString, regexString, "", RegexOptions.IgnoreCase);
        }

        /// <summary>
        /// 清除HTML代码
        /// </summary>
        /// <param name="htmlString"></param>
        /// <returns>返回文字内容</returns>
        public static string ClearHtml(string htmlString)
        {
            htmlString = Regex.Replace(htmlString, "(<[^>]*?>)", "");
            return htmlString.Trim();
        }

        /// <summary>
        /// 清除HTML代码A标签
        /// </summary>
        /// <param name="htmlString"></param>
        /// <returns>返回文字内容</returns>
        public static string ClearA(string htmlString)
        {
            htmlString = Regex.Replace(htmlString, "(</?a[^>]*>)", "", RegexOptions.IgnoreCase);
            return htmlString.Trim();
        }

        /// <summary>
        /// 清除HTML代码A标签
        /// </summary>
        /// <param name="htmlString"></param>
        /// <returns>返回文字内容</returns>
        public static string ClearTitle(string htmlString)
        {
            htmlString = Regex.Replace(htmlString, "(title=\"[^\"]*\")", "");
            return htmlString.Trim();
        }

        /// <summary>
        /// 清理多余的HTML元素
        /// </summary>
        /// <param name="html"></param>
        /// <returns></returns>
        public static string ClearNone(string html)
        {
            var h = RegexHelper.Clear(html, @"(<\!doctype[^>]*?>)");
            h = RegexHelper.Clear(h, @"(<html[^>]*?>)");
            h = RegexHelper.Clear(h, @"(<head[\w\W]*?</head>)");
            h = RegexHelper.Clear(h, @"(<script[\w\W]*?</script>)");
            h = RegexHelper.Clear(h, @"(<style[\w\W]*?</style>)");
            h = RegexHelper.Clear(h, @"(<input[^>]*?>)");
            h = RegexHelper.Clear(h, @"(<!--[\w\W]*?-->)");
            h = RegexHelper.Clear(h, @"(<link[^>]*?>)");
            h = RegexHelper.Clear(h, @"(\s*alt\s*=[""'\s]*[^>]*?['""]+)");
            h = RegexHelper.Clear(h, @"(\s*style\s*=[""'\s]*[^>]*?['""]+)");
            h = RegexHelper.Clear(h, @"(\s*title\s*=[""'\s]*[^>]*?['""]+)");
            h = RegexHelper.Clear(h, @"(\s*id\s*=[""'\s]*[^>]*?['""]+)");
            h = RegexHelper.Clear(h, @"(\s*class\s*=[""'\s]*[^>]*?['""]+)");
            h = RegexHelper.Clear(h, @"(\s*align\s*=[""'\s]*[^>]*?['""]+)");
            h = h.Replace("\r\n", " ");
            h = h.Replace("\n", " ").Replace("\r", " ").Replace("\t", " ").Replace("\0", " ");
            h = h.Replace("                ", " ");
            h = h.Replace("        ", " ");
            h = h.Replace("    ", " ");
            h = h.Replace("   ", " ");
            return h.Replace("  ", " ").Replace("  ", " ");
        }

        /// <summary>
        /// 根据正则表达式获取匹配内容的个数
        /// </summary>
        /// <param name="html"></param>
        /// <param name="regex"></param>
        /// <returns></returns>
        public static int GetRegexCount(string html, string regex)
        {
            return Regex.Matches(html, regex, RegexOptions.IgnoreCase).Count;
        }

        /// <summary>
        /// 根据正则删除表达式所匹配的内容
        /// </summary>
        /// <param name="htmlString"></param>
        /// <param name="regexString"></param>
        /// <param name="newString"></param>
        /// <returns></returns>
        public static string Relpace(string htmlString, string regexString, string newString)
        {
            if (htmlString == null)
            {
                return null;
            }
            return Regex.Replace(htmlString, regexString, newString);
        }

        /// <summary>
        /// 是否是数字(包含小数)
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static bool IsNumber(string str)
        {
            var tmpRegex = new System.Text.RegularExpressions.Regex(@"^[0-9\.]+$");
            return tmpRegex.IsMatch(str);
        }

        /// <summary>
        /// 是否是字母
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static bool IsChar(string str)
        {
            var tmpRegex = new System.Text.RegularExpressions.Regex(@"^[A-Za-z]+$");
            return tmpRegex.IsMatch(str);
        }

        /// <summary>
        /// 是否是字母或数字
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static bool IsCharOrNumber(string str)
        {
            var tmpRegex = new System.Text.RegularExpressions.Regex(@"^[A-Za-z0-9]+$");
            return tmpRegex.IsMatch(str);
        }

        /// <summary>
        /// 是否是中文字符串列
        /// </summary>
        /// <param name="word"></param>
        /// <returns></returns>
        public static bool IsWordList(string word)
        {
            var xj = word.ToCharArray();
            for (var j = 0; j < xj.Length; j++)
                if (!IsWord(xj[j]))
                    return false;
            return true;
        }

        /// <summary>
        /// 是否是字符
        /// </summary>
        /// <param name="ascii"></param>
        /// <returns></returns>
        private static bool IsWord(int ascii)
        {
            if (ascii == 32 || ascii == 35) //空格
                return true;
            if (ascii < 58 && ascii > 47) //数字
                return true;
            if (ascii < 48 && ascii > 31) //符号
                return true;
            if (ascii < 91 && ascii > 64) //小写字母
                return true;
            if (ascii < 123 && ascii > 96) //大写字母
                return true;
            if (ascii < 40892 && ascii > 13311) //中文字符串
                return true;
            return false;
        }

        /// <summary>
        /// 是否为安全字符串(防注入)
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static bool IsSafetySql(string str)
        {
            var tmpRegex =
                new System.Text.RegularExpressions.Regex(
                    @"select |insert |delete from |count\(|drop table|update |truncate |asc\(|mid\(|char\(|xp_cmdshell|exec master|net localgroup administrators|:|net user|""|\'| or ");
            return tmpRegex.IsMatch(str);
        }

        /// <summary>
        /// 是否为小数
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static bool IsFlaotNum(string str)
        {
            var tmpRegex = new System.Text.RegularExpressions.Regex(@"^[0-9]+.?[0-9]+$");
            return tmpRegex.IsMatch(str);
        }

        /// <summary>
        /// 是否为整型数据(不包含小数)
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static bool IsUnsNumeric(string str)
        {
            var tmpRegex = new System.Text.RegularExpressions.Regex(@"^[0-9]+$");
            return tmpRegex.IsMatch(str);
        }

        /// <summary>
        /// 是否为IP
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static bool IsIp(string str)
        {
            var tmpRegex = new System.Text.RegularExpressions.Regex(@"^\d{1,3}\.\d{1,3}\.\d{1,3}\.\d{1,3}$");
            return tmpRegex.IsMatch(str);
        }

        /// <summary>
        /// 是否为邮箱地址
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static bool IsEmail(string str)
        {
            var tmpRegex = new System.Text.RegularExpressions.Regex(@"^[\w-]+(\.[\w-]+)*@[\w-]+(\.[\w-]+)+$");
            return tmpRegex.IsMatch(str);
        }

        public static bool HasEmail(string source)
        {
            return Regex.IsMatch(source,
                @"[A-Za-z0-9](([_\.\-]?[a-zA-Z0-9]+)*)@([A-Za-z0-9]+)(([\.\-]?[a-zA-Z0-9]+)*)\.([A-Za-z]{2,})",
                RegexOptions.IgnoreCase);
        }

        /// <summary> 
        /// 验证网址 
        /// </summary> 
        /// <param name="source"></param> 
        /// <returns></returns> 
        public static bool IsUrl(string source)
        {
            return Regex.IsMatch(source,
                @"^(((file|gopher|news|nntp|telnet|http|ftp|https|ftps|sftp)://)|(www\.))+(([a-zA-Z0-9\._-]+\.[a-zA-Z]{2,6})|([0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}))(/[a-zA-Z0-9\&amp;%_\./-~-]*)?$",
                RegexOptions.IgnoreCase);
        }

        public static bool HasUrl(string source)
        {
            return Regex.IsMatch(source,
                @"(((file|gopher|news|nntp|telnet|http|ftp|https|ftps|sftp)://)|(www\.))+(([a-zA-Z0-9\._-]+\.[a-zA-Z]{2,6})|([0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}))(/[a-zA-Z0-9\&amp;%_\./-~-]*)?",
                RegexOptions.IgnoreCase);
        }

        /// <summary> 
        /// 验证日期 
        /// </summary> 
        /// <param name="source"></param> 
        /// <returns></returns> 
        public static bool IsDateTime(string source)
        {
            try
            {
                var time = Convert.ToDateTime(source);
                return true;
            }
            catch
            {
                return false;
            }
        }

        /// <summary> 
        /// 验证手机号 
        /// </summary> 
        /// <param name="source"></param> 
        /// <returns></returns> 
        public static bool IsMobile(string source)
        {
            return Regex.IsMatch(source, @"^1[35]\d{9}$", RegexOptions.IgnoreCase);
        }

        public static bool HasMobile(string source)
        {
            return Regex.IsMatch(source, @"1[35]\d{9}", RegexOptions.IgnoreCase);
        }

        /// <summary> 
        /// 验证IP 
        /// </summary> 
        /// <param name="source"></param> 
        /// <returns></returns> 
        public static bool IsIP(string source)
        {
            return Regex.IsMatch(source,
                @"^(25[0-5]|2[0-4][0-9]|[0-1]{1}[0-9]{2}|[1-9]{1}[0-9]{1}|[1-9])\.(25[0-5]|2[0-4][0-9]|[0-1]{1}[0-9]{2}|[1-9]{1}[0-9]{1}|[1-9]|0)\.(25[0-5]|2[0-4][0-9]|[0-1]{1}[0-9]{2}|[1-9]{1}[0-9]{1}|[1-9]|0)\.(25[0-5]|2[0-4][0-9]|[0-1]{1}[0-9]{2}|[1-9]{1}[0-9]{1}|[0-9])$",
                RegexOptions.IgnoreCase);
        }

        public static bool HasIP(string source)
        {
            return Regex.IsMatch(source,
                @"(25[0-5]|2[0-4][0-9]|[0-1]{1}[0-9]{2}|[1-9]{1}[0-9]{1}|[1-9])\.(25[0-5]|2[0-4][0-9]|[0-1]{1}[0-9]{2}|[1-9]{1}[0-9]{1}|[1-9]|0)\.(25[0-5]|2[0-4][0-9]|[0-1]{1}[0-9]{2}|[1-9]{1}[0-9]{1}|[1-9]|0)\.(25[0-5]|2[0-4][0-9]|[0-1]{1}[0-9]{2}|[1-9]{1}[0-9]{1}|[0-9])",
                RegexOptions.IgnoreCase);
        }

        /// <summary> 
        /// 验证身份证是否有效 
        /// </summary> 
        /// <param name="Id"></param> 
        /// <returns></returns> 
        public static bool IsIDCard(string Id)
        {
            if (Id.Length == 18)
            {
                var check = IsIdCard18(Id);
                return check;
            }
            else if (Id.Length == 15)
            {
                var check = IsIdCard15(Id);
                return check;
            }
            else
            {
                return false;
            }
        }

        public static bool IsIdCard18(string Id)
        {
            long n = 0;
            if (long.TryParse(Id.Remove(17), out n) == false || n < Math.Pow(10, 16) ||
                long.TryParse(Id.Replace('x', '0').Replace('X', '0'), out n) == false)
            {
                return false; //数字验证 
            }
            var address =
                "11x22x35x44x53x12x23x36x45x54x13x31x37x46x61x14x32x41x50x62x15x33x42x51x63x21x34x43x52x64x65x71x81x82x91";
            if (address.IndexOf(Id.Remove(2), System.StringComparison.Ordinal) == -1)
            {
                return false; //省份验证 
            }
            var birth = Id.Substring(6, 8).Insert(6, "-").Insert(4, "-");
            var time = new DateTime();
            if (DateTime.TryParse(birth, out time) == false)
            {
                return false; //生日验证 
            }
            var arrVarifyCode = ("1,0,x,9,8,7,6,5,4,3,2").Split(',');
            var Wi = ("7,9,10,5,8,4,2,1,6,3,7,9,10,5,8,4,2").Split(',');
            var Ai = Id.Remove(17).ToCharArray();
            var sum = 0;
            for (var i = 0; i < 17; i++)
            {
                sum += int.Parse(Wi[i]) * int.Parse(Ai[i].ToString());
            }
            var y = -1;
            Math.DivRem(sum, 11, out y);
            if (arrVarifyCode[y] != Id.Substring(17, 1).ToLower())
            {
                return false; //校验码验证 
            }
            return true; //符合GB11643-1999标准 
        }

        public static bool IsIdCard15(string Id)
        {
            long n = 0;
            if (long.TryParse(Id, out n) == false || n < Math.Pow(10, 14))
            {
                return false; //数字验证 
            }
            var address =
                "11x22x35x44x53x12x23x36x45x54x13x31x37x46x61x14x32x41x50x62x15x33x42x51x63x21x34x43x52x64x65x71x81x82x91";
            if (address.IndexOf(Id.Remove(2), System.StringComparison.Ordinal) == -1)
            {
                return false; //省份验证 
            }
            var birth = Id.Substring(6, 6).Insert(4, "-").Insert(2, "-");
            var time = new DateTime();
            if (DateTime.TryParse(birth, out time) == false)
            {
                return false; //生日验证 
            }
            return true; //符合15位身份证标准 
        }

        /// <summary> 
        /// 是不是Int型的 
        /// </summary> 
        /// <param name="source"></param> 
        /// <returns></returns> 
        public static bool IsInt(string source)
        {
            var regex = new Regex(@"^(-){0,1}\d+$");
            if (regex.Match(source).Success)
            {
                if ((long.Parse(source) > 0x7fffffffL) || (long.Parse(source) < -2147483648L))
                {
                    return false;
                }
                return true;
            }
            return false;
        }

        /// <summary> 
        /// 看字符串的长度是不是在限定数之间 一个中文为两个字符 
        /// </summary> 
        /// <param name="source">字符串</param> 
        /// <param name="begin">大于等于</param> 
        /// <param name="end">小于等于</param> 
        /// <returns></returns> 
        public static bool IsLengthStr(string source, int begin, int end)
        {
            var length = Regex.Replace(source, @"[^\x00-\xff]", "OK").Length;
            if ((length <= begin) && (length >= end))
            {
                return false;
            }
            return true;
        }

        /// <summary> 
        /// 是不是中国电话，格式010-85849685 
        /// </summary> 
        /// <param name="source"></param> 
        /// <returns></returns> 
        public static bool IsTel(string source)
        {
            return Regex.IsMatch(source, @"^\d{3,4}-?\d{6,8}$", RegexOptions.IgnoreCase);
        }

        /// <summary> 
        /// 邮政编码 6个数字 
        /// </summary> 
        /// <param name="source"></param> 
        /// <returns></returns> 
        public static bool IsPostCode(string source)
        {
            return Regex.IsMatch(source, @"^\d{6}$", RegexOptions.IgnoreCase);
        }

        /// <summary> 
        /// 中文 
        /// </summary> 
        /// <param name="source"></param> 
        /// <returns></returns> 
        public static bool IsChinese(string source)
        {
            return Regex.IsMatch(source, @"^[\u4e00-\u9fa5]+$", RegexOptions.IgnoreCase);
        }

        public static bool HasChinese(string source)
        {
            return Regex.IsMatch(source, @"[\u4e00-\u9fa5]+", RegexOptions.IgnoreCase);
        }

        /// <summary> 
        /// 验证是不是正常字符 字母，数字，下划线的组合 
        /// </summary> 
        /// <param name="source"></param> 
        /// <returns></returns> 
        public static bool IsNormalChar(string source)
        {
            return Regex.IsMatch(source, @"[\w\d_]+", RegexOptions.IgnoreCase);
        }
    }
}