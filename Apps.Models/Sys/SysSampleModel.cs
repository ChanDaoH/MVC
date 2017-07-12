using System;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
namespace Apps.Models.Sys
{
    public partial class SysSampleModel
    {
        /*
        [Display(Name = "ID")]
        public string Id { get; set; }


        [Display(Name = "名称")]
        public string Name { get; set; }


        [Display(Name = "年龄")]
        [Range(0, 10000)]
        public int? Age { get; set; }

        [Display(Name = "生日")]
        public DateTime? Bir { get; set; }

        [Display(Name = "照片")]
        public string Photo { get; set; }

        */
        //[Required(ErrorMessage = "不能为空")]
        [Display(Name = "简介")]
        public override string Note { get; set; }
        /*
        [Display(Name = "创建时间")]
        public DateTime? CreateTime { get; set; }
        */

    }
}