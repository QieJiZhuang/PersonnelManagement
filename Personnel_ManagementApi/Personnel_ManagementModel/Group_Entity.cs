using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Personnel_ManagementModel
{
    /// <summary>
    /// 组表
    /// </summary>
    public class Group_Entity : BaseEntity
    {
        public int Id { get; set; }

        /// <summary>
        /// 组名称
        /// </summary>
        public string group_name { get; set; }

        /// <summary>
        /// 父组ID
        /// </summary>
        public int parent_tg_id { get; set; }
    }
}
