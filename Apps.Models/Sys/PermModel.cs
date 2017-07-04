using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Apps.Models.Sys
{
    public class PermModel
    {
        /// <summary>
        /// 操作码
        /// </summary>
        public string KeyCode { get; set; }
        /// <summary>
        /// 是否验证
        /// </summary>
        public bool IsValid { get; set; }
    }
}
