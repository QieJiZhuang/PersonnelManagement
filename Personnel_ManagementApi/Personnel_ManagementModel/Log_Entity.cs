using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Personnel_ManagementModel
{

    /// <summary>
    /// 操作日志
    /// </summary>
    public class Log_Entity
    {
        public int Id { get; set; }

        /// <summary>
        /// 操作类型
        /// </summary>
        public int op_type { get; set; }

        /// <summary>
        /// 操作内容
        /// </summary>
        public string content { get; set; }

        /// <summary>
        /// 操作人
        /// </summary>
        public int tu_id { get; set; }

        /// <summary>
        /// 操作时间
        /// </summary>
        public DateTime createtime { get; set; }
    }
}
