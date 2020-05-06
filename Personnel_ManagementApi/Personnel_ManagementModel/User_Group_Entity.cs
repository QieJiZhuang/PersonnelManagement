using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Personnel_ManagementModel
{
    /// <summary>
    /// 用户组
    /// </summary>
    public class User_Group_Entity : BaseEntity
    {
        public int Id { get; set; }

        /// <summary>
        /// 用户Id
        /// </summary>
        public int tu_id { get; set; }

        /// <summary>
        /// 组Id
        /// </summary>
        public int tg_id { get; set; }
    }
}
