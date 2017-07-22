using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Apps.Models.Sys
{
    /// <summary>
    /// 文件检查返回类
    /// </summary>
    public class FileCheckModel
    {
        /// <summary>
        /// 成功条数
        /// </summary>
        public int Success { get; set; }
        /// <summary>
        /// 失败条数
        /// </summary>
        public int Fail { get; set; }
        /// <summary>
        /// 保存中是否有异常
        /// </summary>
        public bool State { get; set; }
    }
}
