using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Apps.Models.Sys;
using Apps.Common;

namespace Apps.IBLL
{
    public partial interface ISysRoleBLL
    {
     //   List<SysRoleModel> GetList(ref GridPager pager, string queryStr);
     //   bool Create(ref ValidationErrors errors, SysRoleModel model);
     //   bool Edit(ref ValidationErrors errors, SysRoleModel model);
      //  bool Delete(ref ValidationErrors errors, string id);
     //   SysRoleModel GetById(string id);
     //   bool IsExist(string id);
        List<RoleUserInfoModel> GetUserByRoleId(ref GridPager pager ,string roleId);
        bool UpdateSysRoleSysUser(ref ValidationErrors errors, string roleId, string[] userIds);

    }
}
