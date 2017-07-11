using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Apps.Models.Sys;
using Apps.Models;

namespace Apps.IDAL
{
    public partial interface ISysRightRepository
    {
        List<PermModel> GetPermission(string accountId, string controller);

        //更新
        bool UpdateRight(SysRightOperate model);
        //按选择的角色及模块加载模块的权限项
        List<RightModuleOperateModel> GetRightByRoleAndModule(string roleId, string moduleId);
    }
}
