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
    public class SysRoleRepository : ISysRoleRepository, IDisposable
    {
        public IQueryable<SysRole> GetList(DBContainer db)
        {
            return db.SysRole.AsQueryable();
        }
        public int Create(SysRole entity)
        {
            using (DBContainer db = new DBContainer())
            {
                db.Set<SysRole>().Add(entity);
                //db.SaveChanges();   //必须先保存一次 不然更新权限表无效
               // db.P_Sys_InsertSysRight(); //插入角色时，更新权限表
                return db.SaveChanges();
            }
        }
        public int Delete(string id)
        {
            using (DBContainer db = new DBContainer())
            {
                SysRole entity = db.SysRole.SingleOrDefault(r => r.Id == id);
                if (entity != null)
                {
                    db.Set<SysRole>().Remove(entity);
                }
                return db.SaveChanges();
            }
        }
        public int Edit(SysRole entity)
        {
            using (DBContainer db = new DBContainer())
            {
                db.Set<SysRole>().Attach(entity);
                db.Entry<SysRole>(entity).State = EntityState.Modified;
                return db.SaveChanges();
            }
        }
        public SysRole GetById(string id)
        {
            using (DBContainer db = new DBContainer())
            {
                return db.SysRole.SingleOrDefault(r => r.Id == id);
            }
        }
        public bool IsExist(string id)
        {
            using (DBContainer db = new DBContainer())
            {
                SysRole entity = GetById(id);
                if (entity != null)
                    return true;
                else
                    return false;
            }
        }

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
