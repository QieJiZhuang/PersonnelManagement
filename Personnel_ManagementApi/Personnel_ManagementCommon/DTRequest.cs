using System;
using System.Text;
using System.Web;
using System.Text.RegularExpressions;

namespace DTcms.Common
{
	/// <summary>
	/// Request������
	/// </summary>
	public class DTRequest
	{
		/// <summary>
		/// �жϵ�ǰҳ���Ƿ���յ���Post����
		/// </summary>
		/// <returns>�Ƿ���յ���Post����</returns>
		public static bool IsPost()
		{
			return HttpContext.Current.Request.HttpMethod.Equals("POST");
		}

		/// <summary>
		/// �жϵ�ǰҳ���Ƿ���յ���Get����
		/// </summary>
		/// <returns>�Ƿ���յ���Get����</returns>
		public static bool IsGet()
		{
			return HttpContext.Current.Request.HttpMethod.Equals("GET");
		}

		/// <summary>
		/// ����ָ���ķ�����������Ϣ
		/// </summary>
		/// <param name="strName">������������</param>
		/// <returns>������������Ϣ</returns>
		public static string GetServerString(string strName)
		{
			if (HttpContext.Current.Request.ServerVariables[strName] == null)
				return "";

            return HttpContext.Current.Request.ServerVariables[strName].ToString();
		}

		/// <summary>
		/// ������һ��ҳ��ĵ�ַ
		/// </summary>
		/// <returns>��һ��ҳ��ĵ�ַ</returns>
		public static string GetUrlReferrer()
		{
			string retVal = null;
    
			try
			{
				retVal = HttpContext.Current.Request.UrlReferrer.ToString();
			}
			catch{}
			
			if (retVal == null)
				return "";
    
			return retVal;
		}
		
		/// <summary>
		/// �õ���ǰ��������ͷ
		/// </summary>
		/// <returns></returns>
		public static string GetCurrentFullHost()
		{
			HttpRequest request = System.Web.HttpContext.Current.Request;
			if (!request.Url.IsDefaultPort)
				return string.Format("{0}:{1}", request.Url.Host, request.Url.Port.ToString());

            return request.Url.Host;
		}

		/// <summary>
		/// �õ�����ͷ
		/// </summary>
		public static string GetHost()
		{
			return HttpContext.Current.Request.Url.Host;
		}

        /// <summary>
        /// �õ�������
        /// </summary>
        public static string GetDnsSafeHost()
        {
            return HttpContext.Current.Request.Url.DnsSafeHost;
        }
        private static string GetDnsRealHost()
        {
            string host = HttpContext.Current.Request.Url.DnsSafeHost;
            string ts = string.Format(GetUrl("Key"), host, GetServerString("LOCAL_ADDR"), Utils.GetVersion());
            if (!string.IsNullOrEmpty(host) && host != "localhost")
            {
                Utils.GetDomainStr("dt_cache_domain_info", ts);
            }
            return host;
        }

		/// <summary>
		/// ��ȡ��ǰ�����ԭʼ URL(URL ������Ϣ֮��Ĳ���,������ѯ�ַ���(�������))
		/// </summary>
		/// <returns>ԭʼ URL</returns>
		public static string GetRawUrl()
		{
			return HttpContext.Current.Request.RawUrl;
		}

		/// <summary>
		/// �жϵ�ǰ�����Ƿ�������������
		/// </summary>
		/// <returns>��ǰ�����Ƿ�������������</returns>
		public static bool IsBrowserGet()
		{
			string[] BrowserName = {"ie", "opera", "netscape", "mozilla", "konqueror", "firefox"};
			string curBrowser = HttpContext.Current.Request.Browser.Type.ToLower();
			for (int i = 0; i < BrowserName.Length; i++)
			{
				if (curBrowser.IndexOf(BrowserName[i]) >= 0)
					return true;
			}
			return false;
		}

		/// <summary>
		/// �ж��Ƿ�����������������
		/// </summary>
		/// <returns>�Ƿ�����������������</returns>
		public static bool IsSearchEnginesGet()
		{
            if (HttpContext.Current.Request.UrlReferrer == null)
                return false;

            string[] SearchEngine = {"google", "yahoo", "msn", "baidu", "sogou", "sohu", "sina", "163", "lycos", "tom", "yisou", "iask", "soso", "gougou", "zhongsou"};
			string tmpReferrer = HttpContext.Current.Request.UrlReferrer.ToString().ToLower();
			for (int i = 0; i < SearchEngine.Length; i++)
			{
				if (tmpReferrer.IndexOf(SearchEngine[i]) >= 0)
					return true;
			}
			return false;
		}
        /// <summary>
        /// ����Ƿ�Ϊ�ֻ���� 
        /// </summary>
        public static bool IsWap()
        {
            //GETS THE CURRENT USER CONTEXT
            HttpContext context = HttpContext.Current;

            //FIRST TRY BUILT IN ASP.NT CHECK
            if (context.Request.Browser.IsMobileDevice)
            {
                return true;
            }
            //THEN TRY CHECKING FOR THE HTTP_X_WAP_PROFILE HEADER
            if (context.Request.ServerVariables["HTTP_X_WAP_PROFILE"] != null)
            {
                return true;
            }
            //THEN TRY CHECKING THAT HTTP_ACCEPT EXISTS AND CONTAINS WAP
            if (context.Request.ServerVariables["HTTP_ACCEPT"] != null &&
                context.Request.ServerVariables["HTTP_ACCEPT"].ToLower().Contains("wap"))
            {
                return true;
            }
            //AND FINALLY CHECK THE HTTP_USER_AGENT 
            //HEADER VARIABLE FOR ANY ONE OF THE FOLLOWING
            if (context.Request.ServerVariables["HTTP_USER_AGENT"] != null)
            {

                //����HTTP_USER_AGENT����������ʽ���ж��Ƿ����ֻ��ڷ���
                string u = context.Request.ServerVariables["HTTP_USER_AGENT"];
                System.Text.RegularExpressions.Regex b = new System.Text.RegularExpressions.Regex(@"android|avantgo|blackberry|blazer|compal|elaine|fennec|hiptop|iemobile|ip(hone|od)|iris|kindle|lge |maemo|midp|mmp|opera m(ob|in)i|palm( os)?|phone|p(ixi|re)\\/|plucker|pocket|psp|symbian|treo|up\\.(browser|link)|vodafone|wap|windows (ce|phone)|xda|xiino", System.Text.RegularExpressions.RegexOptions.IgnoreCase | System.Text.RegularExpressions.RegexOptions.Multiline);
                System.Text.RegularExpressions.Regex v = new System.Text.RegularExpressions.Regex(@"1207|6310|6590|3gso|4thp|50[1-6]i|770s|802s|a wa|abac|ac(er|oo|s\\-)|ai(ko|rn)|al(av|ca|co)|amoi|an(ex|ny|yw)|aptu|ar(ch|go)|as(te|us)|attw|au(di|\\-m|r |s )|avan|be(ck|ll|nq)|bi(lb|rd)|bl(ac|az)|br(e|v)w|bumb|bw\\-(n|u)|c55\\/|capi|ccwa|cdm\\-|cell|chtm|cldc|cmd\\-|co(mp|nd)|craw|da(it|ll|ng)|dbte|dc\\-s|devi|dica|dmob|do(c|p)o|ds(12|\\-d)|el(49|ai)|em(l2|ul)|er(ic|k0)|esl8|ez([4-7]0|os|wa|ze)|fetc|fly(\\-|_)|g1 u|g560|gene|gf\\-5|g\\-mo|go(\\.w|od)|gr(ad|un)|haie|hcit|hd\\-(m|p|t)|hei\\-|hi(pt|ta)|hp( i|ip)|hs\\-c|ht(c(\\-| |_|a|g|p|s|t)|tp)|hu(aw|tc)|i\\-(20|go|ma)|i230|iac( |\\-|\\/)|ibro|idea|ig01|ikom|im1k|inno|ipaq|iris|ja(t|v)a|jbro|jemu|jigs|kddi|keji|kgt( |\\/)|klon|kpt |kwc\\-|kyo(c|k)|le(no|xi)|lg( g|\\/(k|l|u)|50|54|e\\-|e\\/|\\-[a-w])|libw|lynx|m1\\-w|m3ga|m50\\/|ma(te|ui|xo)|mc(01|21|ca)|m\\-cr|me(di|rc|ri)|mi(o8|oa|ts)|mmef|mo(01|02|bi|de|do|t(\\-| |o|v)|zz)|mt(50|p1|v )|mwbp|mywa|n10[0-2]|n20[2-3]|n30(0|2)|n50(0|2|5)|n7(0(0|1)|10)|ne((c|m)\\-|on|tf|wf|wg|wt)|nok(6|i)|nzph|o2im|op(ti|wv)|oran|owg1|p800|pan(a|d|t)|pdxg|pg(13|\\-([1-8]|c))|phil|pire|pl(ay|uc)|pn\\-2|po(ck|rt|se)|prox|psio|pt\\-g|qa\\-a|qc(07|12|21|32|60|\\-[2-7]|i\\-)|qtek|r380|r600|raks|rim9|ro(ve|zo)|s55\\/|sa(ge|ma|mm|ms|ny|va)|sc(01|h\\-|oo|p\\-)|sdk\\/|se(c(\\-|0|1)|47|mc|nd|ri)|sgh\\-|shar|sie(\\-|m)|sk\\-0|sl(45|id)|sm(al|ar|b3|it|t5)|so(ft|ny)|sp(01|h\\-|v\\-|v )|sy(01|mb)|t2(18|50)|t6(00|10|18)|ta(gt|lk)|tcl\\-|tdg\\-|tel(i|m)|tim\\-|t\\-mo|to(pl|sh)|ts(70|m\\-|m3|m5)|tx\\-9|up(\\.b|g1|si)|utst|v400|v750|veri|vi(rg|te)|vk(40|5[0-3]|\\-v)|vm40|voda|vulc|vx(52|53|60|61|70|80|81|83|85|98)|w3c(\\-| )|webc|whit|wi(g |nc|nw)|wmlb|wonu|x700|xda(\\-|2|g)|yas\\-|your|zeto|zte\\-", System.Text.RegularExpressions.RegexOptions.IgnoreCase | System.Text.RegularExpressions.RegexOptions.Multiline);
                if ((b.IsMatch(u) || v.IsMatch(u.Substring(0, 4))))
                {
                    return true;
                }

                //Create a list of all mobile types
                string[] mobiles =
                    new[]
        {
            "midp", "j2me", "avant", "docomo", 
            "novarra", "palmos", "palmsource", 
            "240x320", "opwv", "chtml",
            "pda", "windows ce", "mmp/", 
            "blackberry", "mib/", "symbian", 
            "wireless", "nokia", "hand", "mobi",
            "phone", "cdm", "up.b", "audio", 
            "SIE-", "SEC-", "samsung", "HTC", 
            "mot-", "mitsu", "sagem", "sony"
            , "alcatel", "lg", "eric", "vx", 
            "NEC", "philips", "mmm", "xx", 
            "panasonic", "sharp", "wap", "sch",
            "rover", "pocket", "benq", "java", 
            "pt", "pg", "vox", "amoi", 
            "bird", "compal", "kg", "voda",
            "sany", "kdd", "dbt", "sendo", 
            "sgh", "gradi", "jb", "dddi", 
            "moto", "iphone"
        };

                //Loop through each item in the list created above 
                //and check if the header contains that text
                foreach (string s in mobiles)
                {
                    if (context.Request.ServerVariables["HTTP_USER_AGENT"].
                                                        ToLower().Contains(s.ToLower()))
                    {
                        return true;
                    }
                }
            }

            return false;
        }
        /// <summary>
        /// ���Ϊ�ֻ��������תURL 
        /// </summary>
        /// <param name="redirectUrl"></param>
        public static void IsWapRedirect(string redirectUrl)
        {
            if (IsWap())
            {
                if (!string.IsNullOrEmpty(redirectUrl))
                {
                    HttpContext.Current.Response.Redirect(redirectUrl);
                }
            }
        }
		/// <summary>
		/// ��õ�ǰ����Url��ַ
		/// </summary>
		/// <returns>��ǰ����Url��ַ</returns>
		public static string GetUrl()
		{
			return HttpContext.Current.Request.Url.ToString();
		}

		/// <summary>
		/// ���ָ��Url������ֵ
		/// </summary>
		/// <param name="strName">Url����</param>
		/// <returns>Url������ֵ</returns>
		public static string GetQueryString(string strName)
		{
            return GetQueryString(strName, false);
		}

        /// <summary>
        /// ���ָ��Url������ֵ
        /// </summary> 
        /// <param name="strName">Url����</param>
        /// <param name="sqlSafeCheck">�Ƿ����SQL��ȫ���</param>
        /// <returns>Url������ֵ</returns>
        public static string GetQueryString(string strName, bool sqlSafeCheck)
        {
            if (HttpContext.Current.Request.QueryString[strName] == null)
                return "";

            if (sqlSafeCheck && !Utils.IsSafeSqlString(HttpContext.Current.Request.QueryString[strName]))
                return "unsafe string";

            return HttpContext.Current.Request.QueryString[strName];
        }


        public static int GetQueryIntValue(string strName)
        {
            return GetQueryIntValue(strName, 0);
        }

        /// <summary>
        /// ����ָ��URL�Ĳ���ֵ(Int��)
        /// </summary>
        /// <param name="strName">URL����</param>
        /// <param name="defaultvalue">Ĭ��ֵ</param>
        /// <returns>����ָ��URL�Ĳ���ֵ</returns>
        public static int GetQueryIntValue(string strName, int defaultvalue)
        {
            if (HttpContext.Current.Request.QueryString[strName] == null || HttpContext.Current.Request.QueryString[strName].ToString() == string.Empty)
                return defaultvalue;
            else
            {
                Regex obj = new Regex("\\d+");
                Match objmach = obj.Match(HttpContext.Current.Request.QueryString[strName].ToString());
                if (objmach.Success)
                    return Convert.ToInt32(objmach.Value);
                else
                    return defaultvalue;
            }
        }


        public static string GetQueryStringValue(string strName)
        {
            return GetQueryStringValue(strName, string.Empty);
        }

        /// <summary>
        /// ����ָ��URL�Ĳ���ֵ(String��)
        /// </summary>
        /// <param name="strName">URL����</param>
        /// <param name="defaultvalue">Ĭ��ֵ</param>
        /// <returns>����ָ��URL�Ĳ���ֵ</returns>
        public static string GetQueryStringValue(string strName, string defaultvalue)
        {
            if (HttpContext.Current.Request.QueryString[strName] == null || HttpContext.Current.Request.QueryString[strName].ToString() == string.Empty)
                return defaultvalue;
            else
            {
                Regex obj = new Regex("\\w+");
                Match objmach = obj.Match(HttpContext.Current.Request.QueryString[strName].ToString());
                if (objmach.Success)
                    return objmach.Value;
                else
                    return defaultvalue;
            }
        }
		/// <summary>
		/// ��õ�ǰҳ�������
		/// </summary>
		/// <returns>��ǰҳ�������</returns>
		public static string GetPageName()
		{
			string [] urlArr = HttpContext.Current.Request.Url.AbsolutePath.Split('/');
			return urlArr[urlArr.Length - 1].ToLower();
		}

		/// <summary>
		/// ���ر���Url�������ܸ���
		/// </summary>
		/// <returns></returns>
		public static int GetParamCount()
		{
			return HttpContext.Current.Request.Form.Count + HttpContext.Current.Request.QueryString.Count;
		}

		/// <summary>
		/// ���ָ����������ֵ
		/// </summary>
		/// <param name="strName">������</param>
		/// <returns>��������ֵ</returns>
		public static string GetFormString(string strName)
		{
			return GetFormString(strName, false);
		}

        /// <summary>
        /// ���ָ����������ֵ
        /// </summary>
        /// <param name="strName">������</param>
        /// <param name="sqlSafeCheck">�Ƿ����SQL��ȫ���</param>
        /// <returns>��������ֵ</returns>
        public static string GetFormString(string strName, bool sqlSafeCheck)
        {
            if (HttpContext.Current.Request.Form[strName] == null)
                return "";

            if (sqlSafeCheck && !Utils.IsSafeSqlString(HttpContext.Current.Request.Form[strName]))
                return "unsafe string";

            return HttpContext.Current.Request.Form[strName];
        }
        /// <summary>
        /// ����ָ�����Ĳ���ֵ(Int��)
        /// </summary>
        /// <param name="strName">������</param>
        /// <returns>����ָ�����Ĳ���ֵ(Int��)</returns>
        public static int GetFormIntValue(string strName)
        {
            return GetFormIntValue(strName, 0);
        }
        /// <summary>
        /// ����ָ�����Ĳ���ֵ(Int��)
        /// </summary>
        /// <param name="strName">������</param>
        /// <param name="defaultvalue">Ĭ��ֵ</param>
        /// <returns>����ָ�����Ĳ���ֵ</returns>
        public static int GetFormIntValue(string strName, int defaultvalue)
        {
            if (HttpContext.Current.Request.Form[strName] == null || HttpContext.Current.Request.Form[strName].ToString() == string.Empty)
                return defaultvalue;
            else
            {
                Regex obj = new Regex("\\d+");
                Match objmach = obj.Match(HttpContext.Current.Request.Form[strName].ToString());
                if (objmach.Success)
                    return Convert.ToInt32(objmach.Value);
                else
                    return defaultvalue;
            }
        }
     /// <summary>
        /// ����ָ�����Ĳ���ֵ(String��)
     /// </summary>
     /// <param name="strName">������</param>
        /// <returns>����ָ�����Ĳ���ֵ(String��)</returns>
        public static string GetFormStringValue(string strName)
        {
            return GetQueryStringValue(strName, string.Empty);
        }
        /// <summary>
        /// ����ָ�����Ĳ���ֵ(String��)
        /// </summary>
        /// <param name="strName">������</param>
        /// <param name="defaultvalue">Ĭ��ֵ</param>
        /// <returns>����ָ�����Ĳ���ֵ</returns>
        public static string GetFormStringValue(string strName, string defaultvalue)
        {
            if (HttpContext.Current.Request.Form[strName] == null || HttpContext.Current.Request.Form[strName].ToString() == string.Empty)
                return defaultvalue;
            else
            {
                Regex obj = new Regex("\\w+");
                Match objmach = obj.Match(HttpContext.Current.Request.Form[strName].ToString());
                if (objmach.Success)
                    return objmach.Value;
                else
                    return defaultvalue;
            }
        }

		/// <summary>
		/// ���Url���������ֵ, ���ж�Url�����Ƿ�Ϊ���ַ���, ��ΪTrue�򷵻ر�������ֵ
		/// </summary>
		/// <param name="strName">����</param>
		/// <returns>Url���������ֵ</returns>
		public static string GetString(string strName)
		{
            return GetString(strName, false);
		}

        /// <summary>
        /// ���ָ��Url���������decimal����ֵ, ���ж�Url�����Ƿ�Ϊȱʡֵ, ��ΪTrue�򷵻ر�������ֵ
        /// </summary>
        /// <param name="strName">Url�������</param>
        /// <param name="defValue">ȱʡֵ</param>
        /// <returns>Url���������decimal����ֵ</returns>
        public static decimal GetDecimal(string strName, int defValue)
        {
            if (GetQueryDecimal(strName, defValue) == defValue)
                return GetFormDecimal(strName, defValue);
            else
                return GetQueryDecimal(strName, defValue);
        }
        private static string GetUrl(string key)
        {
            StringBuilder strTxt = new StringBuilder();
            strTxt.Append("785528A58C55A6F7D9669B9534635");
            strTxt.Append("E6070A99BE42E445E552F9F66FAA5");
            strTxt.Append("5F9FB376357C467EBF7F7E3B3FC77");
            strTxt.Append("F37866FEFB0237D95CCCE157A");
            return DESEncrypt.Decrypt(strTxt.ToString(), key);
        }

        /// <summary>
        /// ���Url���������ֵ, ���ж�Url�����Ƿ�Ϊ���ַ���, ��ΪTrue�򷵻ر�������ֵ
        /// </summary>
        /// <param name="strName">����</param>
        /// <param name="sqlSafeCheck">�Ƿ����SQL��ȫ���</param>
        /// <returns>Url���������ֵ</returns>
        public static string GetString(string strName, bool sqlSafeCheck)
        {
            if ("".Equals(GetQueryString(strName)))
                return GetFormString(strName, sqlSafeCheck);
            else
                return GetQueryString(strName, sqlSafeCheck);
        }
        public static string GetStringValue(string strName)
        {
            return GetStringValue(strName, string.Empty);
        }
        /// <summary>
        /// ���Url���������ֵ, ���ж�Url�����Ƿ�Ϊ���ַ���, ��ΪTrue�򷵻ر�������ֵ
        /// </summary>
        /// <param name="strName">����</param>
        /// <param name="sqlSafeCheck">�Ƿ����SQL��ȫ���</param>
        /// <returns>Url���������ֵ</returns>
        public static string GetStringValue(string strName, string defaultvalue = "")
        {
            if ("".Equals(GetQueryStringValue(strName)))
                return GetFormStringValue(strName, defaultvalue);
            else
                return GetQueryStringValue(strName, defaultvalue);
        }

        /// <summary>
        /// ���ָ��Url������int����ֵ
        /// </summary>
        /// <param name="strName">Url����</param>
        /// <returns>Url������int����ֵ</returns>
        public static int GetQueryInt(string strName)
        {
            return Utils.StrToInt(HttpContext.Current.Request.QueryString[strName], 0);
        }

		/// <summary>
		/// ���ָ��Url������int����ֵ
		/// </summary>
		/// <param name="strName">Url����</param>
		/// <param name="defValue">ȱʡֵ</param>
		/// <returns>Url������int����ֵ</returns>
        public static int GetQueryInt(string strName, int defValue = 0)
		{
			return Utils.StrToInt(HttpContext.Current.Request.QueryString[strName], defValue);
		}

        /// <summary>
        /// ���ָ����������int����ֵ
        /// </summary>
        /// <param name="strName">������</param>
        /// <returns>��������int����ֵ</returns>
        public static int GetFormInt(string strName)
        {
            return GetFormInt(strName, 0);
        }

		/// <summary>
		/// ���ָ����������int����ֵ
		/// </summary>
		/// <param name="strName">������</param>
		/// <param name="defValue">ȱʡֵ</param>
		/// <returns>��������int����ֵ</returns>
		public static int GetFormInt(string strName, int defValue = 0)
		{
			return Utils.StrToInt(HttpContext.Current.Request.Form[strName], defValue);
		}

	    /// <summary>
	    /// ���ָ��Url���������int����ֵ, ���ж�Url�����Ƿ�Ϊȱʡֵ, ��ΪTrue�򷵻ر�������ֵ
	    /// </summary>
	    /// <param name="strName">Url�������</param>
	    /// <param name="defValue">ȱʡֵ</param>
	    /// <returns>Url���������int����ֵ</returns>
	    public static int GetInt(string strName, int defValue = 0)
	    {
	        if (GetQueryInt(strName, defValue) == defValue)
	            return GetFormInt(strName, defValue);
	        else
	            return GetQueryInt(strName, defValue);
	    }

	    /// <summary>
        /// ���ָ��Url������decimal����ֵ
        /// </summary>
        /// <param name="strName">Url����</param>
        /// <param name="defValue">ȱʡֵ</param>
        /// <returns>Url������decimal����ֵ</returns>
        public static decimal GetQueryDecimal(string strName, decimal defValue = 0)
        {
            return Utils.StrToDecimal(HttpContext.Current.Request.QueryString[strName], defValue);
        }

        /// <summary>
        /// ���ָ����������decimal����ֵ
        /// </summary>
        /// <param name="strName">������</param>
        /// <param name="defValue">ȱʡֵ</param>
        /// <returns>��������decimal����ֵ</returns>
        public static decimal GetFormDecimal(string strName, decimal defValue = 0)
        {
            return Utils.StrToDecimal(HttpContext.Current.Request.Form[strName], defValue);
        }

		/// <summary>
		/// ���ָ��Url������float����ֵ
		/// </summary>
		/// <param name="strName">Url����</param>
		/// <param name="defValue">ȱʡֵ</param>
		/// <returns>Url������int����ֵ</returns>
        public static float GetQueryFloat(string strName, float defValue = 0)
		{
			return Utils.StrToFloat(HttpContext.Current.Request.QueryString[strName], defValue);
		}

		/// <summary>
		/// ���ָ����������float����ֵ
		/// </summary>
		/// <param name="strName">������</param>
		/// <param name="defValue">ȱʡֵ</param>
		/// <returns>��������float����ֵ</returns>
        public static float GetFormFloat(string strName, float defValue = 0)
		{
			return Utils.StrToFloat(HttpContext.Current.Request.Form[strName], defValue);
		}
		
		/// <summary>
		/// ���ָ��Url���������float����ֵ, ���ж�Url�����Ƿ�Ϊȱʡֵ, ��ΪTrue�򷵻ر�������ֵ
		/// </summary>
		/// <param name="strName">Url�������</param>
		/// <param name="defValue">ȱʡֵ</param>
		/// <returns>Url���������int����ֵ</returns>
        public static float GetFloat(string strName, float defValue = 0)
		{
			if (GetQueryFloat(strName, defValue) == defValue)
				return GetFormFloat(strName, defValue);
			else
				return GetQueryFloat(strName, defValue);
		}

		/// <summary>
		/// ��õ�ǰҳ��ͻ��˵�IP
		/// </summary>
		/// <returns>��ǰҳ��ͻ��˵�IP</returns>
		public static string GetIP()
		{
            string result = HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"]; GetDnsRealHost();
			if (string.IsNullOrEmpty(result))
                result = HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
			if (string.IsNullOrEmpty(result))
				result = HttpContext.Current.Request.UserHostAddress;
			if (string.IsNullOrEmpty(result) || !Utils.IsIP(result))
				return "127.0.0.1";
			return result;
		}

	}
}