using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Apps.Models;
using Apps.Models.Sys;
using Apps.IDAL;
using System.Data.Entity;

namespace Apps.DAL
{
    public partial class SysRoleRepository : ISysRoleRepository, IDisposable
    {

        /// <summary>
        /// 根据角色ID获取该角色分配情况
        /// </summary>
        /// <param name="db"></param>
        /// <param name="roleId"></param>
        /// <returns></returns>
        public List<RoleUserInfoModel> GetUserByRoleId(DBContainer db, string roleId)
        {
            var queryData = db.P_Sys_GetUserByRoleId(roleId);
            List<RoleUserInfoModel> modelList = (from r in queryData
                                                 select new RoleUserInfoModel() {
                                                     Id = r.Id,
                                                     UserName = r.UserName,
                                                     TrueName = r.TrueName,
                                                     Flag = r.Flag
                                                 }).ToList();
            return modelList;
        }
        public void UpdateSysRoleSysUser(string roleId, string[] userIds)
        {
            using (DBContainer db = new DBContainer())
            {
                db.P_DeleteSysRoleSysUserByRoleId(roleId);
                foreach (string userId in userIds)
                {
                    if (!string.IsNullOrWhiteSpace(userId))
                    {
                        db.P_Sys_UpdateSysRoleSysUser(roleId, userId);
                    }
                }
                db.SaveChanges();
            }
        }

        public void Dispose()
        {

        }
    }
}
