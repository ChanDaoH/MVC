using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Apps.IDAL;
using Apps.Models;
using Apps.Models.Sys;
using System.Data.Entity;

namespace Apps.DAL
{
    public partial class SysUserRepository : ISysUserRepository,IDisposable
    {
        /// <summary>
        /// 根据用户Id获取角色详情
        /// </summary>
        /// <param name="db"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        public List<UserRoleInfoModel> GetRoleByUserId(DBContainer db, string id)
        {
            var roles = db.P_Sys_GetRoleByUserId(id);
            List<UserRoleInfoModel> modelList = (from r in roles
                                                       select new UserRoleInfoModel()
                                                       {
                                                           Id = r.Id,
                                                           Name = r.Name,
                                                           Description = r.Description,
                                                           CreateTime = r.CreateTime,
                                                           CreatePerson = r.CreatePerson,
                                                           Flag = r.Flag
                                                       }).ToList();
            return modelList;
        }
        /// <summary>
        /// 更新角色用户表
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="roleIds"></param>
        public void UpdateSysRoleSysUser(string userId, string[] roleIds)
        {
            using (DBContainer db = new DBContainer())
            {
                db.P_Sys_DeleteSysRoleSysUserByUserId(userId);
                foreach( string roleId in roleIds)
                {
                    if ( !string.IsNullOrWhiteSpace(roleId))
                        db.P_Sys_UpdateSysRoleSysUser(roleId, userId);
                }
                db.SaveChanges();
            }
        }
        public void Dispose()
        {

        }
    }
}
