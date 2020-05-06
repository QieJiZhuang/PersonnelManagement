using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Personnel_ManagementModel
{
   public class ResponseModel
    {
        public int Code { get; set; }
        public string Msg { get; set; }
        public object Data { get; set; }

        public int Count { get; set; }
    }
}
