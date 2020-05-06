using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Personnel_ManagementModel
{
    /// <summary>
    /// </summary>
    /// </summary>
    public class User_Right_Entity : BaseEntity
    {
        public int Id { get; set; }

        /// <summary>
        /// 用户ID
        /// </summary>
        public int tu_id { get; set; }

        /// <summary>
        /// 权限ID
        /// </summary>
        public int tr_id { get; set; }

        /// <summary>
        /// 权限类型
        /// </summary>
        public int right_type { get; set; }
    }
}
