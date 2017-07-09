using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Apps.Models.Sys;
using Apps.Common;

namespace Apps.IBLL
{
    public interface ISysUserBLL
    {
        List<PermModel> GetPermission(string accountId, string controller);
        List<SysUserModel> GetList(ref GridPager pager, string queryStr);
        bool Create(ref ValidationErrors errors, SysUserModel model);
        bool Delete(ref ValidationErrors errors, string id);
        bool Edit(ref ValidationErrors errors, SysUserModel model);
        SysUserModel GetById(string id);
        bool IsExist(string id);
        List<UserRoleInfoModel> GetRoleByUserId(ref GridPager pager,string id);
        bool UpdateSysRoleSysUser(ref ValidationErrors errors ,string userId, string[] roleIds);
    }
}
