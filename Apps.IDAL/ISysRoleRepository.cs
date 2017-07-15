using Apps.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Apps.Models.Sys;

namespace Apps.IDAL
{
    public partial interface ISysRoleRepository
    {
        List<RoleUserInfoModel> GetUserByRoleId(DBContainer db, string roleId);
        void UpdateSysRoleSysUser(string roleId, string[] userIds);
    }
}
