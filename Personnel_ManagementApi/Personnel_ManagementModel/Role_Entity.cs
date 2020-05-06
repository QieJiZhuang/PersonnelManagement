using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Personnel_ManagementModel
{
    /// <summary>
    /// 角色表
    /// </summary>
    public class Role_Entity:BaseEntity
    {
        /// <summary>
        /// 编号
        /// </summary>
        public int tr_id { get; set; }

        /// <summary>
        /// 父级角色ID
        /// </summary>
        public int parent_tr_id { get; set; }

        /// <summary>
        /// 角色名称
        /// </summary>
        public string role_name { get; set; }

    }
}
