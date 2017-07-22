using System;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
namespace Apps.Models.Sys
{
    public partial class SysSampleModel
    {
        [Required(ErrorMessage = "不能为空")]
        [Display(Name = "ID")]
        public override string Id { get; set; }

        [Required(ErrorMessage = "不能为空")]
        [Display(Name = "名称")]
        public override string Name { get; set; }


        [Display(Name = "年龄")]
        [Range(0, 10000)]
        public override int? Age { get; set; }

        [Display(Name = "生日")]
        public override DateTime? Bir { get; set; }

        [Display(Name = "照片")]
        public override string Photo { get; set; }

        
        [Required(ErrorMessage = "不能为空")]
        [Display(Name = "简介")]
        public override string Note { get; set; }
        
       // [Display(Name = "创建时间")]
        //public DateTime? CreateTime { get; set; }
        

    }
}