using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LandMarkApply.Common
{
    /// <summary>
    /// 统一Ajax返回的对象
    /// </summary>
    public class AjaxResult
    {
        /// <summary>
        /// 执行结果
        /// </summary>
        public string Status { get; set; }
        /// <summary>
        /// 错误信息
        /// </summary>
        public string ErrorMsg { get; set; }
        /// <summary>
        /// 返回数据
        /// </summary>
        public object Data { get; set; }
    }
}
