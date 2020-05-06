using System.Configuration;
using System.Runtime.CompilerServices;

namespace LandMarkApply.Common
{
    public class WebSettingsConfig
    { 
        public static string PubAcceptNotice
        {
            get
            {
                return AppSettingValue();
            }
        }

        public static string PubApproveNotice
        {
            get
            {
                return AppSettingValue();
            }
        }

        public static string PubAcceptFailNotice
        {
            get
            {
                return AppSettingValue();
            }
        }

        public static string PubApproveFailNotice
        {
            get
            {
                return AppSettingValue();
            }
        }

        private static string AppSettingValue([CallerMemberName] string key = null)
        {
            return ConfigurationManager.AppSettings[key];
        }
    }
}
