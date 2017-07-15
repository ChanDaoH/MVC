using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Apps.Models;
using Apps.Models.Sys;
using Apps.Common;

namespace Apps.IBLL
{
    public partial interface ISysRightBLL
    {
        bool UpdateRight(ValidationErrors errors, SysRightOperateModel model);
        List<RightModuleOperateModel> GetRightByRoleAndModule(string roleId, string moduleId);
    }
}
