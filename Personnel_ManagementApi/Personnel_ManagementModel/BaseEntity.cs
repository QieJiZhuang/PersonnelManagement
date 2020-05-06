using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Personnel_ManagementModel
{
    public class BaseEntity
    {
        public bool is_del { get; set; }
        public string description { get; set; }
        public int creator { get; set; }
        public DateTime createtime { get; set; }
    }
}
