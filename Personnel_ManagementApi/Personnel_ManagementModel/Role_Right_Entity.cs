using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Personnel_ManagementModel
{
    /// <summary>
    /// 权限角色表
    /// </summary>
    public class Role_Right_Entity : BaseEntity
    {
        public int Id { get; set; }

        /// <summary>
        /// 角色ID
        /// </summary>
        public int Role_id { get; set; }

        /// <summary>
        /// 权限ID
        /// </summary>
        public int Right_id { get; set; }

        /// <summary>
        /// 权限类型
        /// </summary>
        public int Right_type { get; set; }
    }
}
