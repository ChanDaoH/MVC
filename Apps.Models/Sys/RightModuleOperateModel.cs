using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Apps.Models.Sys
{
    public partial class RightModuleOperateModel
    {
        /// <summary>
        /// 操作码Id
        /// </summary>
        public string Id { get; set; }
        /// <summary>
        /// 操作码名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 操作码代码
        /// </summary>
        public string KeyCode { get; set; }
        /// <summary>
        /// 模块Id
        /// </summary>
        public string ModuleId { get; set; }
        /// <summary>
        /// 是否有效
        /// </summary>
        public bool isvalid { get; set; }
        /// <summary>
        /// 排序字段
        /// </summary>
        public int Sort { get; set; }
        /// <summary>
        /// 权限ID
        /// </summary>
        public string RightId { get; set; }
    }
}
