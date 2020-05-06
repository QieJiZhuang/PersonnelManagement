using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Personnel_ManagementModel
{
    /// <summary>
    /// 
    /// 组权限表
    /// </summary>
    public class Group_Right_Entity:BaseEntity
    {
        public int Id { get; set; }

        /// <summary>
        /// 组ID
        /// </summary>
        public int tg_id { get; set; }

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
