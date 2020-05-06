using System;
using System.Globalization;
using System.Collections.Generic;

namespace Traffic.Utility
{
    /// <summary>
    /// Description: 与日期、时间有关操作类
    /// Version: 1.0
    /// Created: 2014/12/17
    /// Author:  zlf
    /// Company: 北京博奥中成信息科技有限责任公司
    /// 
    /// ModifyEditTime: 修改时间
    /// ModifyContent:  修改内容
    /// ModifyPerson :  修改人
    /// </summary>
    public static class DateHelper
    {
        /// <summary>
        /// 获取天数
        /// </summary>
        private static int GetDay(int month, int year)
        {
            var day = 0;
            switch (month)
            {
                case 1:
                    day = 31;
                    break;
                case 2:
                    //闰年天，平年天
                    if ((year % 4 == 0 && year % 100 != 0) || year % 400 == 0)
                    {
                        day = 29;
                    }
                    else
                    {
                        day = 28;
                    }
                    break;
                case 3:
                    day = 31;
                    break;
                case 4:
                    day = 30;
                    break;
                case 5:
                    day = 31;
                    break;
                case 6:
                    day = 30;
                    break;
                case 7:
                    day = 31;
                    break;
                case 8:
                    day = 31;
                    break;
                case 9:
                    day = 30;
                    break;
                case 10:
                    day = 31;
                    break;
                case 11:
                    day = 30;
                    break;
                case 12:
                    day = 31;
                    break;
            }
            return day;
        }

        /// <summary>
        /// 返回与当前时间的差
        /// </summary>
        /// <param name="dt">输入的时间字符串</param>
        /// <param name="format"></param>
        /// <returns></returns>
        public static string GetTopicTimeSpan(DateTime dt, string format = "MM-dd HH:mm")
        {
            string result;
            var curDt = DateTime.Now;
            var ts1 = new TimeSpan(dt.Ticks);
            var ts2 = new TimeSpan(curDt.Ticks);
            var ts = ts1.Subtract(ts2).Duration();
            if (ts.Minutes < 1 && ts.Hours < 1 && ts.Days < 1)
            {
                result = "刚刚";
            }
            else if (ts.Minutes <= 59 && ts.Hours < 1 && ts.Days < 1)
            {
                result = ts.Minutes + "分钟前";
            }
            else if (ts.Hours < 23 && ts.Days < 1)
            {
                result = ts.Hours + "小时前";
            }
            else if (ts.Days < 2)
            {
                result = "1天前";
            }
            else if (ts.Days < 3)
            {
                result = "2天前";
            }
            else
            {
                result = dt.ToString(format);
            }

            return result;
        }

        /// <summary> 
        /// 判断两个日期是否在同一周 
        /// </summary> 
        /// <param name="dtmS">开始日期</param> 
        /// <param name="dtmE">结束日期</param>
        /// <returns></returns> 
        public static bool IsInSameWeek(DateTime dtmS, DateTime dtmE)
        {
            var ts = dtmE - dtmS;
            var dbl = ts.TotalDays;
            var intDow = Convert.ToInt32(dtmE.DayOfWeek);
            if (intDow == 0) intDow = 7;
            return !(dbl >= 7) && !(dbl >= intDow);
        }

        /// <summary>
        /// 判断是否再当天天之前的30天
        /// </summary>
        /// <param name="dtmS">开始日期</param> 
        /// <param name="dtmE">结束日期</param>
        /// <returns></returns>
        public static bool IsInSameMonth(DateTime dtmS, DateTime dtmE)
        {
            var ts = dtmE - dtmS;
            var dbl = ts.TotalDays;
            return dbl <= 30;
        }

        /// <summary>
        /// 获取星座
        /// </summary>
        /// <param name="birthday">出生日期 年-月-日</param>
        /// <returns></returns>
        public static string GetConstellation(string birthday)
        {
            if (string.IsNullOrEmpty(birthday))
            {
                return "";
            }
            DateTime d;
            var date = DateTime.Parse(birthday);
            if (date.Month == 2 && date.Day == 29)
            {
                birthday = date.ToString("yyyy-MM") + "-28";
            }
            if (!DateTime.TryParse(birthday, out d)) return "日期格式错误";

            var day = d.Day;
            d = DateTime.Parse(d.Month + "-" + day);
            if (d >= DateTime.Parse("03-21") && d <= DateTime.Parse("04-19"))
            {
                return "白羊座";
            }
            if (d >= DateTime.Parse("04-20") && d <= DateTime.Parse("05-20"))
            {
                return "金牛座";
            }
            if (d >= DateTime.Parse("05-21") && d <= DateTime.Parse("06-21"))
            {
                return "双子座";
            }
            if (d >= DateTime.Parse("06-22") && d <= DateTime.Parse("07-22"))
            {
                return "巨蟹座";
            }
            if (d >= DateTime.Parse("07-23") && d <= DateTime.Parse("08-22"))
            {
                return "狮子座";
            }
            if (d >= DateTime.Parse("08-23") && d <= DateTime.Parse("09-22"))
            {
                return "处女座";
            }
            if (d >= DateTime.Parse("09-23") && d <= DateTime.Parse("10-23"))
            {
                return "天秤座";
            }
            if (d >= DateTime.Parse("10-24") && d <= DateTime.Parse("11-22"))
            {
                return "天蝎座";
            }
            if (d >= DateTime.Parse("11-23") && d <= DateTime.Parse("12-21"))
            {
                return "射手座";
            }
            if ((d >= DateTime.Parse("12-22") && d <= DateTime.Parse("12-31")) ||
                (d >= DateTime.Parse("01-01") && d <= DateTime.Parse("01-19")))
            {
                return "摩羯座";
            }
            if (d >= DateTime.Parse("01-20") && d <= DateTime.Parse("02-18"))
            {
                return "水瓶座";
            }
            if (d >= DateTime.Parse("02-19") && d <= DateTime.Parse("03-20"))
            {
                return "双鱼座";
            }
            return "未知日期";
        }

        /// <summary>  
        /// 当前时间转化为本月的第几周  
        /// </summary>  
        /// <param name="date">指定的日期</param>
        /// <param name="sundayStart">是否从周日开始计算</param>  
        /// <returns></returns>  
        private static int WeekOfTime(DateTime? date, bool sundayStart)
        {
            var dtSel = Convert.ToDateTime(date);

            //如果要判断的日期为1号，则肯定是第一周了   
            if (dtSel.Day == 1) return 1;

            //得到本月第一天   
            var dtStart = new DateTime(dtSel.Year, dtSel.Month, 1);

            //得到本月第一天是周几   
            var dayofweek = (int)dtStart.DayOfWeek;

            //如果不是以周日开始，需要重新计算一下dayofweek，详细风DayOfWeek枚举的定义   
            if (!sundayStart)
            {
                dayofweek = dayofweek - 1;
                if (dayofweek < 0) dayofweek = 7;
            }

            //得到本月的第一周一共有几天   
            var startWeekDays = 7 - dayofweek;

            //如果要判断的日期在第一周范围内，返回1   
            if (dtSel.Day <= startWeekDays) return 1;

            var aday = dtSel.Day + 7 - startWeekDays;
            return aday / 7 + (aday % 7 > 0 ? 1 : 0);
        }

        /// <summary>
        /// 生成一个年月List，内容如：201502，201503，201504....
        /// </summary>
        /// <returns></returns>
        public static List<string> GetYearMonthList()
        {
            var nowMonth = DateTime.Now.Month;
            var tmpDateList = new List<string>();
            for (var i = 1; i <= nowMonth; i++)
            {
                var tmpYear = DateTime.Now.Year;
                var tmpVal = tmpYear + "" + DateTime.Now.AddMonths(i-nowMonth).ToString("MM");
                tmpDateList.Add(tmpVal);
            }
            return tmpDateList;
        }

        /// <summary>  
        /// 得到本周第一天(以星期一为第一天)  
        /// </summary>  
        /// <param name="datetime"></param>  
        /// <returns></returns>  
        public static string GetWeekFirstDayMon(DateTime datetime)
        {
            //星期一为第一天  
            var weeknow = Convert.ToInt32(DateTime.Now.DayOfWeek);

            //因为是以星期一为第一天，所以要判断weeknow等于0时，要向前推6天。  
            weeknow = (weeknow == 0 ? (7 - 1) : (weeknow - 1));
            var daydiff = (-1) * weeknow;

            //本周第一天  
            var firstDay = DateTime.Now.AddDays(daydiff).ToString("yyyy-MM-dd");
            return firstDay;
        }

        /// <summary>  
        /// 得到本周最后一天(以星期天为最后一天)  
        /// </summary>  
        /// <param name="datetime"></param>  
        /// <returns></returns>  
        public static string GetWeekLastDaySun(DateTime datetime)
        {
            //星期天为最后一天  
            var weeknow = Convert.ToInt32(DateTime.Now.DayOfWeek);
            weeknow = (weeknow == 0 ? 7 : weeknow);
            var daydiff = (7 - weeknow);

            //本周最后一天  
            var lastDay = DateTime.Now.AddDays(daydiff).ToString("yyyy-MM-dd");
            return lastDay;
        }

        /// <summary>
        /// 将秒转换 分-秒
        /// </summary>
        /// <param name="seconds">秒</param>
        /// <returns></returns>
        public static string GetMinuteSeconds(double? seconds)
        {
            var second = Convert.ToInt32(seconds);
            var tmpMinute = second / 60;
            var tmpSecond = second % 60;
            if (tmpMinute == 0)
            {
                return tmpSecond + "秒";
            }
            if (tmpSecond == 0)
            {
                return tmpMinute + "分";
            }
            return tmpMinute + "分" + tmpSecond + "秒";
        }

    }
}
