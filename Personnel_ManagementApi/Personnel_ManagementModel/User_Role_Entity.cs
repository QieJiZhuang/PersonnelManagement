using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Personnel_ManagementModel
{
    /// <summary>
    /// 用户角色
    /// </summary>
    public class User_Role_Entity : BaseEntity
    {
        public int Id { get; set; }

       /// <summary>
       /// 用户Id
       /// </summary>
        public int tu_id { get; set; }

        /// <summary>
        /// 角色ID
        /// </summary>
        public int tr_id { get; set; }
    }
}
