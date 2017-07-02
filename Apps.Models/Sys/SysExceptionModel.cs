using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Apps.Models.Sys
{
    /// <summary>
    /// 异常处理类
    /// </summary>
    public class SysExceptionModel
    {

        [Display(Name = "ID")]
        public string Id { get; set; }

        [Display(Name = "帮助链接")]
        public string HelpLink { get; set; }

        [Display(Name = "错误信息")]
        public string Message { get; set; }

        [Display(Name = "来源")]
        public string Source { get; set; }

        [Display(Name = "堆栈")]
        public string StackTrace { get; set; }

        [Display(Name = "目标页")]
        public string TargetSite { get; set; }

        [Display(Name = "程序集")]
        public string Data { get; set; }

        [Display(Name = "发生时间")]
        public DateTime? CreateTime { get; set; }
    }
}
