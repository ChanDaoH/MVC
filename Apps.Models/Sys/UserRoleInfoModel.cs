using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Apps.Models.Sys
{
    /// <summary>
    /// 用户针对角色的拥有状态类
    /// </summary>
    public partial class UserRoleInfoModel
    {
        /// <summary>
        /// 角色Id
        /// </summary>
        public string Id { get; set; }
        /// <summary>
        /// 角色名
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 角色描述
        /// </summary>
        public string Description { get; set; }
        /// <summary>
        /// 角色创建时间
        /// </summary>
        public DateTime CreateTime { get; set; }
        /// <summary>
        /// 角色创建者
        /// </summary>
        public string CreatePerson { get; set; }
        /// <summary>
        /// 有无该角色 为"0"代表无
        /// </summary>
        public string Flag { get; set; }    //为0代表该用户无该角色
    }
}
