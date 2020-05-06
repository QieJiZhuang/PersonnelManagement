using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Personnel_ManagementModel
{
    /// <summary>
    /// 组织表
    /// </summary>
    public class Organization_Entity
    {
        public int Id { get; set; }

        /// <summary>
        /// 父组Id
        /// </summary>
        public int parent_to_id { get; set; }

        /// <summary>
        /// 组织名称
        /// </summary>
        public int org_id { get; set; }
    }
}
