using Apps.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Apps.Models.Sys;

namespace Apps.IDAL
{
    public interface ISysRoleRepository
    {
        IQueryable<SysRole> GetList(DBContainer db);
        int Create(SysRole entity);
        int Delete(string id);
        int Edit(SysRole entity);
        SysRole GetById(string id);
        bool IsExist(string id);
        List<RoleUserInfoModel> GetUserByRoleId(DBContainer db, string roleId);
        void UpdateSysRoleSysUser(string roleId, string[] userIds);
    }
}
