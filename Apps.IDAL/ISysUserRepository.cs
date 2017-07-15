using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Apps.Models;
using Apps.Models.Sys;

namespace Apps.IDAL
{
    public partial interface ISysUserRepository
    {
        List<UserRoleInfoModel> GetRoleByUserId(DBContainer db,string id);
        void UpdateSysRoleSysUser(string userId, string[] roleIds);
    }
}
