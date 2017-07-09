using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Apps.Models.Sys
{
    public class SysModuleOperateModel
    {
        [Display(Name = "ID")]
        public string Id { get; set; }
        [Display(Name = "操作名称")]
        public string Name { get; set; }
        [Display(Name = "操作码")]
        public string KeyCode { get; set; }
        [Display(Name = "所属模块")]
        public string ModuleId { get; set; }
        [Display(Name = "是否验证")]
        public bool IsValid { get; set; }
        [Required(ErrorMessage = "{0}必须填写")]
        [Display(Name = "排序号")]
        public int Sort { get; set; }
    }
}
