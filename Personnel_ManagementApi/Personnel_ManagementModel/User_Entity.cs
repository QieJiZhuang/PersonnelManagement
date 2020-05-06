using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Personnel_ManagementModel
{
    /// <summary>
    /// 用户表
    /// </summary>
    public class User_Entity: BaseEntity
    {
        /// <summary>
        /// 编号
        /// </summary>
        public int tu_id { get; set; }

        /// <summary>
        /// 所属组织
        /// </summary>
        public int to_id { get; set; }

        /// <summary>
        /// 登录账号
        /// </summary>
        public string login_name { get; set; }

        /// <summary>
        /// 用户密码
        /// </summary>
        public string password { get; set; }

        /// <summary>
        /// 用户姓名
        /// </summary>
        public string username { get; set; }

        /// <summary>
        /// 手机号
        /// </summary>
        public string mobile { get; set; }

        /// <summary>
        /// 电子邮箱
        /// </summary>
        public string email { get; set; }

        /// <summary>
        /// 上次登录时间
        /// </summary>
        public DateTime login_time { get; set; }
    }
}
