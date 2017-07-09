using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Apps.Models.Sys
{
    public class SysModuleModel
    {
        [Display(Name = "ID")]
        public string Id { get; set; }
        [Display(Name = "名称")]
        public string Name { get; set; }

        [Display(Name = "别名")]
        public string EnglishName { get; set; }
        [Display(Name = "上级ID")]
        public string ParentId { get; set; }
        [Display(Name = "链接")]
        public string Url { get; set; }
        [Display(Name = "图标")]
        public string Iconic { get; set; }
        [Display(Name = "排序号")]
        public int? Sort { get; set; }
        [Display(Name = "说明")]
        public string Remark { get; set; }
        [Display(Name = "状态")]
        public bool Enable { get; set; }
        [Display(Name = "创建人")]
        public string CreatePerson { get; set; }
        [Display(Name = "创建时间")]
        public DateTime? CreateTime { get; set; }
        [Display(Name = "是否最后一项")]
        public bool IsLast { get; set; }

        public string state { get; set; }//treegrid

    }
}
