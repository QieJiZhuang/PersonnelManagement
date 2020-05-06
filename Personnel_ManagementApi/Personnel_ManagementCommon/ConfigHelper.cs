using System;
using System.Configuration;
using System.IO;

namespace Traffic.Utility
{
    /// <summary>
    /// Description: 获取配置文件信息操作类
    /// Version: 1.0
    /// Created: 2014/12/17
    /// Author:  zlf
    /// Company: 北京博奥中成信息科技有限责任公司
    /// 
    /// ModifyEditTime: 修改时间
    /// ModifyContent:  修改内容
    /// ModifyPerson :  修改人
    /// </summary>
    public static class ConfigHelper
    {
        #region 获取自定义返回值信息

        /// <summary>
        /// 自定义获取配置文件中的值
        /// </summary>
        /// <param name="strConfigKey">节点名称</param>
        /// <returns>节点值</returns>
        private static string GetConfigValue(string strConfigKey)
        {
            return ConfigurationManager.AppSettings[strConfigKey];
        }

        #endregion

        #region 邮件发送限制次数

        private static int _sendEmailCount;

        /// <summary>
        /// 邮件发送限制次数
        /// </summary>
        public static int SendEmailCount
        {
            get
            {
                if (_sendEmailCount == 0)
                {
                    _sendEmailCount = int.Parse(GetConfigValue("EmailSendCount"));
                }
                return _sendEmailCount;
            }
        }

        #endregion

        #region 不受限制的用户ID集合

        private static string _mangerUserIds;

        /// <summary>
        /// 不受限制的用户ID集合
        /// </summary>
        public static string MangerUserIds
        {
            get
            {
                if (string.IsNullOrEmpty(_mangerUserIds))
                {
                    _mangerUserIds = GetConfigValue("MangerUserIds");
                }
                return _mangerUserIds;
            }
        }

        #endregion

        #region 当天登录次数限制

        private static int _logonCounts;

        /// <summary>
        /// 当天登录次数限制 超过限制会认为是恶意操作 冻结账户
        /// </summary>
        public static int LogonCounts
        {
            get
            {
                if (_logonCounts == 0)
                {
                    _logonCounts = int.Parse(GetConfigValue("LogonCounts"));
                }
                return _logonCounts;
            }
        }

        #endregion

        #region 邮件发送帐号配置

        private static string _sendMethod = string.Empty;
        /// <summary>
        /// 网站邮件服务器链接串
        /// </summary>
        public static string SendMethod
        {
            get
            {
                if (_sendMethod == string.Empty)
                {
                    _sendMethod = GetConfigValue("SendMethod");
                }
                return _sendMethod;
            }
        }

        private static string _smtpServerName = string.Empty;
        /// <summary>
        /// 网站邮件服务器链接串 
        /// </summary>
        public static string SmtpServerName
        {
            get
            {
                if (_smtpServerName == string.Empty)
                {
                    _smtpServerName = GetConfigValue("SMTPServerName");
                }
                return _smtpServerName;
            }
        }

        private static string _smtpServerNameCloud = string.Empty;
        /// <summary>
        /// 网站邮件服务器链接串 
        /// </summary>
        public static string SMTPServerNameCloud
        {
            get
            {
                if (_smtpServerNameCloud == string.Empty)
                {
                    _smtpServerNameCloud = GetConfigValue("SMTPServerNameCloud");
                }
                return _smtpServerNameCloud;
            }
        }

        private static string _fromEmail = string.Empty;
        /// <summary>
        /// 发送方邮件地址 
        /// </summary>
        public static string FromEmail
        {
            get
            {
                if (_fromEmail == string.Empty)
                {
                    _fromEmail = GetConfigValue("FromEmail");
                }
                return _fromEmail;
            }
        }

        private static string _fromEmailCloud = string.Empty;
        /// <summary>
        /// 发送方邮件地址 
        /// </summary>
        public static string FromEmailCloud
        {
            get
            {
                if (_fromEmailCloud == string.Empty)
                {
                    _fromEmailCloud = GetConfigValue("FromEmailCloud");
                }
                return _fromEmailCloud;
            }
        }

        private static string _fromEmailPwd = string.Empty;
        /// <summary>
        /// 发送方邮件密码    
        /// </summary>
        public static string FromEmailPwd
        {
            get
            {
                if (_fromEmailPwd == string.Empty)
                {
                    _fromEmailPwd = GetConfigValue("FromEmailPwd");
                }
                return _fromEmailPwd;
            }
        }

        private static string _fromEmailPwdCloud = string.Empty;
        /// <summary>
        /// 发送方邮件密码    
        /// </summary>
        public static string FromEmailPwdCloud
        {
            get
            {
                if (_fromEmailPwdCloud == string.Empty)
                {
                    _fromEmailPwdCloud = GetConfigValue("FromEmailPwdCloud");
                }
                return _fromEmailPwdCloud;
            }
        }

        private static string _emailUrl = string.Empty;
        /// <summary>
        /// 邮件地址特殊处理串
        /// </summary>
        public static string EmailUrl
        {
            get
            {
                if (_emailUrl == string.Empty)
                {
                    _emailUrl = GetConfigValue("EmailUrl");
                }
                return _emailUrl;
            }
        }

        private static string _emailDisplayName = string.Empty;
        /// <summary>
        /// 邮件发送时显示的名称   
        /// </summary>
        public static string EmailDisplayName
        {
            get
            {
                if (_emailDisplayName == string.Empty)
                {
                    _emailDisplayName = GetConfigValue("EmailDisplayName");
                }
                return _emailDisplayName;
            }
        }

        private static string _fromEmailName = string.Empty;
        /// <summary>
        /// 邮件发送时显示的名称   
        /// </summary>
        public static string FromEmailNameCloud
        {
            get
            {
                if (_fromEmailName == string.Empty)
                {
                    _fromEmailName = GetConfigValue("FromEmailNameCloud");
                }
                return _fromEmailName;
            }
        }
        #endregion

        #region 文件、图片上传目录、类型、大小配置
        private static string _uploadPath = string.Empty;
        /// <summary>
        /// 网站文件上传物理地址 如：D:\Work\images\
        /// </summary>
        public static string UploadPath
        {
            get
            {
                if (_uploadPath == string.Empty)
                {
                    _uploadPath = GetConfigValue("UploadPath");

                }
                return _uploadPath;
            }
        }
        private static string _cdnDomain = string.Empty;
        /// <summary>
        /// 读取图片的域名 如： http://www.baidu.com/
        /// </summary>
        public static string CdnDomain
        {
            get
            {
                if (_cdnDomain == string.Empty)
                {
                    _cdnDomain = GetConfigValue("CdnDomain");
                }
                return _cdnDomain;
            }
        }

        /// <summary>
        /// 允许上传的图片类型
        /// </summary>
        private static string _uploadImgType = string.Empty;
        /// <summary>
        /// 允许上传的图片类型 image/bmp,image/gif,image/jpeg,image/pjpeg
        /// </summary>
        public static string UploadImgType
        {
            get
            {
                if (_uploadImgType == string.Empty)
                {
                    _uploadImgType = GetConfigValue("UploadImgType");
                }
                return _uploadImgType;
            }
        }

        /// <summary>
        /// 允许上传的图片大小
        /// </summary>
        private static int _uploadImgSize;

        /// <summary>
        /// 允许上传的图片大小（K）
        /// </summary>
        public static int UploadImgSize
        {
            get
            {
                if (_uploadImgSize == 0)
                {
                    _uploadImgSize = int.Parse(GetConfigValue("UploadImgSize"));
                }
                return _uploadImgSize;
            }
        }

        /// <summary>
        /// 允许上传的文件类型
        /// </summary>
        private static string _uploadFileType = string.Empty;

        /// <summary>
        /// 允许上传的文件类型
        /// </summary>
        public static string UploadFileType
        {
            get
            {
                if (_uploadFileType == string.Empty)
                {
                    _uploadFileType = GetConfigValue("UploadFileType");
                }
                return _uploadFileType;
            }
        }

        /// <summary>
        /// 允许上传的文件大小
        /// </summary>
        private static int _uploadFileSize;

        /// <summary>
        /// 允许上传的文件大小（K）
        /// </summary>
        public static int UploadFileSize
        {
            get
            {
                if (_uploadFileSize == 0)
                {
                    _uploadFileSize = int.Parse(GetConfigValue("UploadFileSize"));
                }
                return _uploadFileSize;
            }
        }

        private static string _waterImgPath = string.Empty;
        /// <summary>
        /// 网站上传图片的服务器端带水印图路径
        /// </summary>
        public static string WaterImgPath
        {
            get
            {
                if (_waterImgPath == string.Empty)
                {
                    _waterImgPath = GetConfigValue("WaterImgPath");
                }
                return _waterImgPath;
            }
            set
            {
                _waterImgPath = value;
            }
        }

        #endregion

        #region 获取指定用户头像

        /// <summary>
        /// 获取指定用户头像（默认50×50）
        /// </summary>
        /// <param name="userId">用户ID</param>
        /// <param name="size">头像尺寸</param>
        /// <returns></returns>
        public static string GetUserProfileImage(int userId, int size = 50)
        {
            return string.Format("{0}/files/{1}/avatar/{1}_{2}_{2}.png", CdnDomain, userId, size);
        }

        #endregion

        #region 获取CDN图片地址

        /// <summary>
        /// 获取CDN图片地址
        /// </summary>
        /// <param name="imagePath"></param>
        /// <returns></returns>
        public static string GetImage(string imagePath)
        {
            return CdnDomain + imagePath;
        }

        #endregion

        #region 加密密钥配置
        private static string _descKey = string.Empty;
        /// <summary>
        /// 双向加密解密密钥
        /// </summary>
        public static string DescKey
        {
            get
            {
                if (_descKey == string.Empty)
                {
                    _descKey = GetConfigValue("DescKey");
                }
                return _descKey;
            }
        }
        #endregion

        #region 缓存键名称配置
        /// <summary>
        /// [脏词系统]过滤类关键词缓存键名称
        /// </summary>
        public static string IsAllowKey
        {
            get
            {
                return "IsAllowKey";
            }
        }
        /// <summary>
        /// [脏词系统]禁止类关键词缓存键名称
        /// </summary>
        public static string FilterKey
        {
            get
            {
                return "IsFilterKey";
            }
        }
        #endregion

        #region Cookie信息配置
        private static string _cookiesKey = string.Empty;
        /// <summary>
        /// Cookies加密密钥
        /// </summary>
        public static string CookiesKey
        {
            get
            {
                if (_cookiesKey == string.Empty)
                {
                    _cookiesKey = GetConfigValue("CookiesKey");
                }
                return _cookiesKey;
            }
        }
        #endregion

        #region 控制是否记录日志

        private static bool _isAddLogBack = false;
        /// <summary>
        /// 后台是否记录日志
        /// </summary>
        public static bool IsAddLogBack
        {
            get
            {
                _isAddLogBack = bool.Parse(GetConfigValue("IsAddLogBack"));

                return _isAddLogBack;
            }
        }

        private static bool _isAddLogFront = false;
        /// <summary>
        /// 前台是否记录日志
        /// </summary>
        public static bool IsAddLogFront
        {
            get
            {
                _isAddLogFront = bool.Parse(GetConfigValue("IsAddLogFront"));

                return _isAddLogFront;
            }
        }

        #endregion

        #region /// 人脸识别服务器配置地址

        private static string _aForgeServiceUrl = string.Empty;
        /// <summary>
        /// 人脸识别服务器配置地址
        /// </summary>
        public static string AForgeServiceUrl
        {
            get
            {
                _aForgeServiceUrl = GetConfigValue("AForgeServiceUrl");

                return _aForgeServiceUrl;
            }
        }

        #endregion

        #region /// 人脸识别比对照片服务器存放地址

        private static string _faceImagDefaultUrl = string.Empty;
        /// <summary>
        /// 人脸识别比对照片服务器存放地址
        /// </summary>
        public static string FaceImagDefaultUrl
        {
            get
            {
                _faceImagDefaultUrl = GetConfigValue("FaceImagDefaultUrl");

                return _faceImagDefaultUrl;
            }
        }

        #endregion

        #region /// 主域名

        private static string _wWwDomain = string.Empty;
        /// <summary>
        /// 人脸识别服务器配置地址
        /// </summary>
        public static string WwwDomain
        {
            get
            {
                _wWwDomain = GetConfigValue("WWWDomain");

                return _wWwDomain;
            }
        }

        #endregion

        #region /// 人脸识别比对照片存放地址

        private static string _clientFaceImageServiceUrl = string.Empty;
        /// <summary>
        /// 加密字符串(请勿修改)
        /// </summary>
        public static string ClientFaceImageServiceUrl
        {
            get
            {
                _clientFaceImageServiceUrl = GetConfigValue("ClientFaceImageServiceUrl");

                return _clientFaceImageServiceUrl;
            }
        }

        #endregion

        #region /// 加密字符串

        private static string _encryptKey = string.Empty;
        /// <summary>
        /// 加密字符串(请勿修改)
        /// </summary>
        public static string EncryptKey
        {
            get
            {
                _encryptKey = "QWEDSioc";

                return _encryptKey;
            }
        }

        #endregion

        #region /// Memcached服务器地址

        private static string _memcachedServiceUrl = string.Empty;
        /// <summary>
        /// Memcached服务器地址
        /// </summary>
        public static string MemcachedServiceUrl
        {
            get
            {
                _memcachedServiceUrl = GetConfigValue("MemcachedServiceUrl");

                return _memcachedServiceUrl;
            }
        }

        #endregion

        #region /// IP限制白名单

        private static string _iPstr = string.Empty;
        /// <summary>
        /// IP限制白名单
        /// </summary>
        public static string IPstr
        {
            get
            {
                _iPstr = GetConfigValue("IPstr");

                return _iPstr;
            }
        }

        #endregion

        #region /// 第三方系统对接接口开关
       
        private static bool _interFaceOnOff = true;
        /// <summary>
        /// 安徽运政系统接口开关 true-打开开关；false-关闭开关
        /// </summary>
        public static bool InterFaceOnOff
        {
            get
            {
                _interFaceOnOff = bool.Parse(GetConfigValue("InterFaceOnOff"));

                return _interFaceOnOff;
            }
        }
        #endregion

        #region /// 延期申请缴费,短信发送开关

        private static bool _sendMessageOnOff = true;
        /// <summary>
        /// 开启短信发送接口开关 true-打开开关；false-关闭开关
        /// </summary>
        public static bool SendMessageOnOff
        {
            get
            {
                _sendMessageOnOff = bool.Parse(GetConfigValue("SendMessageOnOff"));

                return _sendMessageOnOff;
            }
        }
        #endregion

        #region /// 缴费提示发送开关
        
        private static bool _sendArrearsPromptOff = true;
        /// <summary>
        /// 开启缴费提示发送接口开关 true-打开开关；false-关闭开关
        /// </summary>
        public static bool SendArrearsPromptOff
        {
            get
            {
                _sendArrearsPromptOff = bool.Parse(GetConfigValue("SendArrearsPromptOff"));

                return _sendArrearsPromptOff;
            }
        }
        #endregion

        #region /// 作弊邮件发送给技术部开关

        private static bool _sendCheatEmailOnOff = true;
        /// <summary>
        /// 是否开启作弊邮件发送给技术部 true-开启发送功能；false-关闭发送功能
        /// </summary>
        public static bool SendCheatEmailOnOff
        {
            get
            {
                _sendCheatEmailOnOff = bool.Parse(GetConfigValue("SendCheatEmailOnOff"));
                return _sendCheatEmailOnOff;
            }
        }
        #endregion

        #region /// 作弊发送短信给学员开关

        private static bool _sendCheatMessageOnOff = true;
        /// <summary>
        /// 是否开启作弊邮件发送给技术部 true-开启发送功能；false-关闭发送功能
        /// </summary>
        public static bool SendCheatMessageOnOff
        {
            get
            {
                _sendCheatMessageOnOff = bool.Parse(GetConfigValue("SendCheatMessageOnOff"));
                return _sendCheatMessageOnOff;
            }
        }

        #endregion

        #region /// 退费流程发送短信给学员开关

        private static bool _sendReturnCostMessageOnOff = true;
        /// <summary>
        /// 是否开启作弊邮件发送给技术部 true-开启发送功能；false-关闭发送功能
        /// </summary>
        public static bool SendReturnCostMessageOnOff
        {
            get
            {
                _sendReturnCostMessageOnOff = bool.Parse(GetConfigValue("SendReturnCostMessageOnOff"));
                return _sendReturnCostMessageOnOff;
            }
        }

        #endregion

        #region 安全教育域名

        private static string _securityDoMain = string.Empty;
        /// <summary>
        ///  安全教育域名
        /// </summary>
        public static string SecurityDoMain
        {
            get
            {
                _securityDoMain = GetConfigValue("SecurityDoMain");

                return _securityDoMain;
            }
        }
        #endregion

        #region 图片服务器地址

        private static string _saveImageServiceUrl = string.Empty;
        /// <summary>
        ///  保存
        /// </summary>
        public static string SaveImageServiceUrl
        {
            get
            {
                _saveImageServiceUrl = GetConfigValue("SaveImageServiceUrl");

                return _saveImageServiceUrl;
            }
        }

        private static string _readImageServiceUrl = string.Empty;
        /// <summary>
        ///  读取
        /// </summary>
        public static string ReadImageServiceUrl
        {
            get
            {
                _readImageServiceUrl = GetConfigValue("ReadImageServiceUrl");

                return _readImageServiceUrl;
            }
        }
        #endregion

        #region 图片服务接口地址

        private static string _imageWebServiceUrl1 = string.Empty;
        /// <summary>
        ///  保存
        /// </summary>
        public static string ImageWebServiceUrl1
        {
            get
            {
                _imageWebServiceUrl1 = GetConfigValue("ImageWebServiceUrl1");

                return _imageWebServiceUrl1;
            }
        }

        private static string _imageWebServiceUrl2 = string.Empty;
        /// <summary>
        ///  读取
        /// </summary>
        public static string ImageWebServiceUrl2
        {
            get
            {
                _imageWebServiceUrl2 = GetConfigValue("ImageWebServiceUrl2");

                return _imageWebServiceUrl2;
            }
        }
        #endregion

        #region 超市跳转参数秘钥

        private static string _marketSecretKey = string.Empty;
        public static string MarketSecretKey
        {
            get
            {
                _marketSecretKey = GetConfigValue("MarketSecretKey");

                return _marketSecretKey;
            }
        }
        #endregion

    }
}
