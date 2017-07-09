using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Apps.Models;
using Apps.Models.Sys;

namespace Apps.IDAL
{
    public interface ISysUserRepository
    {
        IQueryable<SysUser> GetList(DBContainer db);
        int Create(SysUser entity);
        int Delete(string id);
        int Edit(SysUser entity);
        SysUser GetById(string id);
        bool IsExist(string id);
        List<UserRoleInfoModel> GetRoleByUserId(DBContainer db,string id);
        void UpdateSysRoleSysUser(string userId, string[] roleIds);
    }
}
