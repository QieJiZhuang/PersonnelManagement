using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WEB.Common
{
   public class FormVerify
    {
        /// <summary>
        /// 判断是否被未空
        /// </summary>
        /// <returns>为空返回false</returns>
        public static bool IsNull(string syllable)
        {
            if (syllable.Trim() == "") //判断是否为空输入
            {
                return false;
            }

            return true;
        }

        /// <summary>
        /// 判断非特殊字符
        /// </summary>
        /// <param name="syllable">验证字符串</param>
        /// <returns>为空返回false</returns>
        public static bool IsEspecial(string syllable)
        {
            string regex = "[\u4e00-\u9fa5]";

            return RegexOperation(syllable, regex);

        }
        
        /// <summary>
        /// 验证字符串是否合法
        /// </summary>
        /// <param name="syllable">需验证字符串</param>
        /// <param name="regex">正则表达式</param>
        /// <returns></returns>
        private static bool RegexOperation(string syllable, string regex)
        {
            //正则表达式的枚举类型
            System.Text.RegularExpressions.RegexOptions options = (
                (System.Text.RegularExpressions.RegexOptions.IgnorePatternWhitespace |
                System.Text.RegularExpressions.RegexOptions.Multiline) |
                System.Text.RegularExpressions.RegexOptions.IgnoreCase);

            //加载正则表达式到枚举类型上
            System.Text.RegularExpressions.Regex reg = new System.Text.RegularExpressions.Regex(regex, options);

            //返回验证结果
            return reg.IsMatch(syllable);
        }

        /// <summary>
        /// 验证loginname
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        
        public static bool IsLoginName(string str) {
            if (str.IndexOf(" ") >= 0)
                return false;
            string regex = "^[A-Za-z0-9]{6,20}$";//只能输入数字字母
            return RegexOperation(str, regex);
        }
        public static bool IsPassWord(string str)
        {
            if (str.IndexOf(" ") >= 0)
                return false;
            string regex = "^[A-Za-z0-9]{6,20}$";//只能输入数字字母
            return RegexOperation(str, regex);
        }
        public static bool IsEmail(string str)
        {
            if (str.IndexOf(" ") >= 0)
                return false;
            string regex = @"[\w!#$%&'*+/=?^_`{|}~-]+(?:\.[\w!#$%&'*+/=?^_`{|}~-]+)*@(?:[\w](?:[\w-]*[\w])?\.)+[\w](?:[\w-]*[\w])?";//邮箱
            return RegexOperation(str, regex);
        }
        public static bool IsTelephone(string str)
        {
            string regex = @"^\d{10,12}$";//电话
            return RegexOperation(str, regex);
        }
        public static bool IsMobilePhone(string str)
        {
            string regex = @"^(13[0-9]|14[5|7]|15[0|1|2|3|5|6|7|8|9]|18[0|1|2|3|5|6|7|8|9])\d{8}$";//手机
            return RegexOperation(str, regex);
        }
        public static bool IsRoleID(string str)
        {
            
            string regex = @"^\d{1,1}$";//1位数字
            return RegexOperation(str, regex);
        }
        public static bool IsSMSCode(string str)
        {

            string regex = @"^\d{4,4}$";//1位数字
            return RegexOperation(str, regex);
        }
        public static bool IsLegal(string str)
        {
            if (str.IndexOf(" ") >= 0)
                return false;
            string regex = @"[-|;|,|\/|\(|\)|\[|\]|\}|\{|%|@|\*|!|\']";//特殊字符、防注入
            return !RegexOperation(str, regex);
        }
    }
}
