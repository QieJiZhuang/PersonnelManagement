using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web;

namespace Traffic.Utility
{
    /// <summary>
    /// Description: 针对String的操作类
    /// Version: 1.0
    /// Created: 2014/12/17
    /// Author:  zlf
    /// Company: 北京博奥中成信息科技有限责任公司
    /// 
    /// ModifyEditTime: 修改时间
    /// ModifyContent:  修改内容
    /// ModifyPerson :  修改人
    /// </summary>
    public static class StringHelper
    {
        /// <summary>
        /// 返回缩略图路径
        /// </summary>
        /// <param name="filePath">原图路径</param>
        /// <param name="width">缩略图宽</param>
        /// <param name="height">缩略图高</param>
        /// <returns></returns>
        public static string GetThumbSize(string filePath, int width, int height)
        {
            if (!string.IsNullOrEmpty(filePath))
            {
                if (filePath.ToLower().Contains("http"))
                {
                    return filePath;
                }
                var match = Regex.Match(filePath, @"(\S+)\.([gif|bmp|jpg|jpeg|png]+)", RegexOptions.IgnoreCase);
                return string.Format("{0}_{1}_{2}.{3}", match.Groups[1].Value, width, height, match.Groups[2].Value);
            }
            return string.Empty;
        }

        /// <summary>
        /// 必须为数字 
        /// </summary>
        /// <param name="strNum"></param>
        /// <returns></returns>
        public static bool IsNum(string strNum)
        {
            return strNum != null && Regex.IsMatch(strNum, @"^(-)?[0-9]+$");
        }

        /// <summary>
        /// 此方法用于判断是否存在特殊字符
        /// </summary>
        /// <param name="strNum">要验证的内容</param>
        /// <returns></returns>
        public static bool IsExistsSpecialChar(string strNum)
        {
            return !(strNum != null && Regex.IsMatch(strNum, @"^[A-Za-z0-9\u4e00-\u9fa5]+$"));
        }

        /// <summary>
        /// 验证只能输入汉子或字母
        /// </summary>
        /// <param name="strNum">要验证的内容</param>
        /// <returns></returns>
        public static bool IsExistsCharOrNum(string strNum)
        {
            return !(strNum != null && Regex.IsMatch(strNum, @"^[A-Za-z\u4e00-\u9fa5]+$"));
        }

        /// <summary>
        /// 验证昵称（不能重复，2-16位字符，包括字母、数字、下划线或中文）
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static bool IsExistsCharOrLetterOrNumOrUnderline(string str)
        {
            return !(str != null && Regex.IsMatch(str, @"^[_\u4E00-\u9FA5a-zA-Z0-9]{2,16}$"));
        }

        /// <summary>
        /// 验证名称（4-10位，中文、字母，中文占2位）
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static bool IsExistsCharOrLetterOrNum(string str)
        {
            return !(str != null && Regex.IsMatch(str, @"^[\u4E00-\u9FA5a-zA-Z]{4,10}$"));
        }

        /// <summary>
        /// 证密码格式（密码由6-16位数字、字母、或下划线组成，区分全角/半角，字母区分大小写）
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static bool IsExistsLetterOrNumOrUnderline(string str)
        {
            return !(str != null && Regex.IsMatch(str, @"^[_a-zA-Z0-9]{6,16}$"));
        }

        /// <summary>
        /// 验证IP地址
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static bool IsIp(string str)
        {
            return !(str != null && Regex.IsMatch(str, @"\b(([01]?\d?\d|2[0-4]\d|25[0-5])\.){3}([01]?\d?\d|2[0-4]\d|25[0-5])\b"));
        }

        /// <summary>
        /// 截字符串
        /// </summary>
        /// <param name="content">字符串</param>
        /// <param name="length">截几个字(两个字符 = 一个中文字)</param>
        /// <param name="isFilterHtml">是否需要过滤掉HTML标签</param>
        /// <param name="isFlag">是否追加...</param>
        /// <returns>截好的字符串</returns>
        public static string CutStr(string content, int length, bool isFilterHtml, bool isFlag = true)
        {
            if (string.IsNullOrEmpty(content)) return string.Empty;
            if (isFilterHtml) content = FilterHtml(content);//过滤掉html代码
            if (string.IsNullOrEmpty(content) || content.Length <= length) return content;

            if (isFlag)
            {
                content = content.Substring(0, length) + "...";
            }
            else
            {
                content = content.Substring(0, length);
            }
            return content;
        }

        /// <summary>
        /// 返回文章中带有图片的第一条数据
        /// </summary>
        /// <param name="content">文章内容</param>
        /// <returns></returns>
        public static string GetTxtFirstImgSrc(string content)
        {
            var imgSrc = string.Empty;
            if (string.IsNullOrWhiteSpace(content)) return imgSrc;

            const string pattern = @"<img\b[^<>]*?\bsrc[\s\t\r\n]*=[\s\t\r\n]*[""']?[\s\t\r\n]*(?<imgUrl>([^\s\t\r\n""'<>])*)[^<>]*?/?[\s\t\r\n]*>";
            var matchs = Regex.Matches(content, pattern);
            foreach (Match match in matchs)
            {
                var url = match.Groups["imgUrl"].Value;
                if (url.ToLower().IndexOf("themes", StringComparison.Ordinal) > 0)
                    continue;

                if (url.ToLower().IndexOf("emotion", StringComparison.Ordinal) > 0)
                    continue;

                imgSrc = Regex.Replace(url, @"_\d+_\d+", "", RegexOptions.IgnoreCase);
                break;
            }
            return imgSrc;
        }

        /// <summary>
        /// 过滤词实体对象
        /// </summary>
        public class FilterWords
        {
            /// <summary>
            /// 要过滤的词
            /// </summary>
            public string WordPattern { get; set; }
            /// <summary>
            /// 替换词
            /// </summary>
            public string RepalceWord { get; set; }
        }

        /// <summary>
        /// 返回替换过后的文字内容
        /// </summary>
        /// <param name="strContext">文字内容</param>
        /// <param name="listFilterWords">替换词</param>
        /// <returns></returns>
        private static string ReplaceModWords(string strContext, IEnumerable<FilterWords> listFilterWords)
        {
            return strContext == null ? null : listFilterWords.Aggregate(strContext, (current, filterWords) => current.Replace(filterWords.WordPattern.Trim(), filterWords.RepalceWord));
        }

        /// <summary>
        /// 判断是否含有禁止词
        /// </summary>
        /// <param name="strContext">文字内容</param>
        /// <param name="listFilterWords">禁止词集合</param>
        /// <param name="strSensitiveWord">输出内容中存在的禁止词字符串 返回true代表有禁止词 false代表没有</param>
        /// <returns></returns>
        private static bool IsContainsForbidWords(string strContext, List<string> listFilterWords, out string strSensitiveWord)
        {
            if (string.IsNullOrEmpty(strContext))
            {
                strSensitiveWord = null;
                return false;
            }
            strSensitiveWord = "";  //初始化一下
            var regex = string.Join("|", listFilterWords.ToArray());
            var isPass = Regex.IsMatch(strContext, regex, RegexOptions.IgnoreCase);
            if (!isPass) return false;

            var matchCollection = Regex.Matches(strContext, regex, RegexOptions.IgnoreCase);
            strSensitiveWord = matchCollection.Cast<Match>().Aggregate(strSensitiveWord, (current, match) => current + (match.Value + ","));
            strSensitiveWord = strSensitiveWord.Substring(0, strSensitiveWord.Length - 1);
            return true;
        }

        /// <summary>
        /// 过滤html标签以及JS脚本
        /// </summary>
        /// <param name="content">待过滤的文本信息</param>
        /// <returns></returns>
        public static string FilterHtml(string content)
        {
            if (string.IsNullOrEmpty(content))
            {
                return string.Empty;
            }
            content = Regex.Replace(content, @"<([\u0000-\uffff]+?)>", "");
            return Regex.Replace(content, @"&(nbsp|#160);", "   ", RegexOptions.IgnoreCase); ;
        }

        /// <summary>
        /// 判断文本框混合输入长度
        /// </summary>
        /// <param name="str">要判断的字符串</param>
        /// <param name="i">长度</param>
        /// <returns></returns>
        public static bool CheckByteLength(string str, int i)
        {
            var b = Encoding.Default.GetBytes(str);
            var m = b.Length;
            return m <= i;
        }

        /// <summary>
        /// 判断文本框混合输入长度
        /// </summary>
        /// <param name="str">要判断的字符串</param>
        /// <param name="min"></param>
        /// <param name="max"></param>
        /// <returns></returns>
        public static bool CheckByteLength(string str, int min, int max)
        {
            var b = Encoding.Default.GetBytes(str);
            var m = b.Length;
            return m >= min && m <= max;
        }

        /// <summary>
        /// 组装26个英文字母到集合中
        /// </summary>
        /// <returns>返回字母集合</returns>
        public static List<string> GetLetterList()
        {
            var letterList = new List<string> { "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z" };
            return letterList;
        }

        /// <summary>
        /// 取html中所有图片地址
        /// </summary>
        /// <param name="strHtml">html代码</param>
        /// <returns>图片列表</returns>
        public static List<string> GetPicUrls(string strHtml)
        {
            var arrOutPut = new List<string>();
            if (string.IsNullOrWhiteSpace(strHtml)) return arrOutPut;

            const string url = @"((http|https):(\/\/|\\\\){1}((\w)+[.]){1,}(net|com|cn|org|cc|tv|[0-9]{1,3})(\S*\/)((\S)+[.]{1}(gif|jpg|jpeg|png|bmp)))";
            var r = new Regex(url, RegexOptions.IgnoreCase | RegexOptions.Compiled);
            var match1 = r.Match(strHtml);

            while (match1.Success)
            {
                var tmpUrl = match1.Groups[0].Value;

                if (tmpUrl.ToLower().IndexOf("themes", StringComparison.Ordinal) > 0)
                {
                    match1 = match1.NextMatch();
                    continue;
                }

                if (tmpUrl.ToLower().IndexOf("emotion", StringComparison.Ordinal) > 0)
                {
                    match1 = match1.NextMatch();
                    continue;
                }

                if (tmpUrl.ToLower().IndexOf("avatar", StringComparison.Ordinal) > 0)
                {
                    match1 = match1.NextMatch();
                    continue;
                }

                arrOutPut.Add(tmpUrl);
                match1 = match1.NextMatch();
            }
            return arrOutPut;
        }

       
        /// <summary>
        /// 返回指定IP是否在指定的IP数组所限定的范围内, IP数组内的IP地址可以使用*表示该IP段任意, 例如192.168.1.*
        /// </summary>
        /// <param name="ip"></param>
        /// <param name="iparray"></param>
        /// <returns></returns>
        public static bool InIpArray(string ip, string[] iparray)
        {
            var userip = SplitString(ip, @".");

            foreach (var t in iparray)
            {
                var tmpip = SplitString(t, @".");
                var r = 0;
                for (var i = 0; i < tmpip.Length; i++)
                {
                    if (tmpip[i] == "*")
                        return true;

                    if (userip.Length > i)
                    {
                        if (tmpip[i] == userip[i])
                            r++;
                        else
                            break;
                    }
                    else
                        break;
                }

                if (r == 4)
                    return true;
            }
            return false;
        }

        /// <summary>
        /// 分割字符串
        /// </summary>
        public static string[] SplitString(string strContent, string strSplit)
        {
            if (!StrIsNullOrEmpty(strContent))
            {
                if (strContent.IndexOf(strSplit, System.StringComparison.Ordinal) < 0)
                    return new string[] { strContent };

                return Regex.Split(strContent, Regex.Escape(strSplit), RegexOptions.IgnoreCase);
            }
            else
                return new string[0] { };
        }

        /// <summary>
        /// 字段串是否为Null或为""(空)
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static bool StrIsNullOrEmpty(string str)
        {
            if (str == null || str.Trim() == string.Empty)
                return true;

            return false;
        }

        /// <summary>
        /// 财务模块动态生成列
        /// LJQK - 累计欠款
        /// LJJY - 累计结余
        /// YJJE - 应缴欠款
        /// XZRS - 新增人数
        /// SJJE - 已交金额
        /// TFJE - 退费金额
        /// TFRS - 退人数
        /// BookBuyerNum - 教材数
        /// </summary>
        /// <param name="strName">需要转换的列名</param>
        /// <param name="strMonth">要转换的月份。默认不填写</param>
        /// <returns></returns>
        public static string GetFinancialName(string strName, string strMonth = "")
        {
            if (string.IsNullOrEmpty(strName)) return strName;

            var result = "";
            var tmpMonth = strMonth;
            if (string.IsNullOrEmpty(tmpMonth))
            {
                tmpMonth = DateTime.Now.Month.ToString("MM");
            }

            switch (strName.ToUpper())
            {
                case "LJQK":
                    result = tmpMonth + "月之前累计欠款";
                    break;

                case "LJJY":
                    result = tmpMonth + "月之前累计结余";
                    break;

                case "YJJE":
                    result = tmpMonth + "月应缴欠款";
                    break;
                case "XZRS":
                    result = tmpMonth + "月新增人数";
                    break;
                case "SJJE":
                    result = tmpMonth + "月已交金额";
                    break;
                case "TFJE":
                    result = tmpMonth + "月退费金额";
                    break;
                case "TFRS":
                    result = tmpMonth + "月退人数";
                    break;
                case "BookBuyerNum":
                    result = tmpMonth + "月教材数";
                    break;

                default:
                    return result = strName;
            }
            return result;
        }

        /// <summary>
        /// 替换第一个匹配的内容
        /// </summary>
        /// <param name="inputString">输入的字符串</param>
        /// <param name="oldValue">被替换的字符串</param>
        /// <param name="newValue">替换的字符串</param>
        /// <returns></returns>
        public static string ReplaceFirst(string inputString, string oldValue, string newValue)
        {
            oldValue = oldValue.Replace("(", "//(");
            var regEx = new Regex(oldValue, RegexOptions.Multiline);
            return regEx.Replace(inputString, newValue ?? "", 1);
        }

        /// <summary>
        /// 替换最后一个匹配的内容
        /// </summary>
        /// <param name="inputString">输入的字符串</param>
        /// <param name="oldValue">被替换的字符串</param>
        /// <param name="newValue">替换的字符串</param>
        /// <returns></returns>
        public static string ReplaceLast(string inputString, string oldValue, string newValue)
        {
            var index = inputString.LastIndexOf(oldValue, System.StringComparison.Ordinal);
            if (index < 0)
            {
                return inputString;
            }
            else
            {
                var sb = new StringBuilder(inputString.Length - oldValue.Length + newValue.Length);
                sb.Append(inputString.Substring(0, index));
                sb.Append(newValue);
                sb.Append(inputString.Substring(index + oldValue.Length,
                   inputString.Length - index - oldValue.Length));
                return sb.ToString();
            }
        }

        /// <summary>
        /// 获取订单号 小批次号 大批次号(0产品唯一单号  1 订单号 2 小批次号 3 大批次号)
        /// </summary>
        /// <param name="flag"></param>
        /// <returns></returns>
        public static string GetProductOrderNum(int flag)
        {
            var frontstr = "ETLJTKH";
            var middlestr = "";
            switch (flag)
            {
                case 0:
                    middlestr = "00";
                    break;
                case 1:
                    middlestr = "01";
                    break;
                case 2:
                    middlestr = "02";
                    break;
                case 3:
                    middlestr = "03";
                    break;
                default:
                    break;
            }
            //后四位随机数
            var dateTimeStr = DateTime.Now.AddMilliseconds(new Random().Next(1000, 10000)).ToString("yyyyMMddHHmmssffff");
            //为了避免有重复数据出现.取 GUID前几位
            var mathstr = System.Guid.NewGuid().ToString("N").Substring(1, 8);
            var affterstr = dateTimeStr + mathstr;

            return frontstr + middlestr + affterstr;
        }


        /// <summary>
        /// 获取银行验证码 10位
        /// </summary>
        /// <returns></returns>
        public static string GetBankCode()
        {
           return  DateTime.Now.ToString("yyMMddHHmmssfff") + new Random().Next(0, 9);
           
        }
    }
}
