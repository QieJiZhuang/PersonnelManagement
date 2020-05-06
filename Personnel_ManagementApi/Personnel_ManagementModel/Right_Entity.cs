using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Personnel_ManagementModel
{
    /// <summary>
    /// 权限表
    /// </summary>
    public class Right_Entity:BaseEntity
    {
        public int Id { get; set; }

        /// <summary>
        /// 父权限
        /// </summary>
        public int parent_tr_id { get; set; }

        /// <summary>
        /// 权限名称
        /// </summary>
        public string right_name { get; set; }
     
     }
}
