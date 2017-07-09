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
    public class SysUserRepository : ISysUserRepository,IDisposable
    {
        public IQueryable<SysUser> GetList(DBContainer db)
        {
            IQueryable<SysUser> list = db.SysUser.AsQueryable();
            return list;
        }

        public int Create(SysUser entity)
        {
            using (DBContainer db = new DBContainer())
            {
                db.Set<SysUser>().Add(entity);
                return db.SaveChanges();
            }
        }

        public int Delete(string id)
        {
            using (DBContainer db = new DBContainer())
            {
                SysUser entity = db.SysUser.SingleOrDefault(a => a.Id == id);
                if (entity != null)
                {

                    db.Set<SysUser>().Remove(entity);
                }
                return db.SaveChanges();
            }
        }

        public int Edit(SysUser entity)
        {
            using (DBContainer db = new DBContainer())
            {
                db.Set<SysUser>().Attach(entity);
                db.Entry<SysUser>(entity).State = EntityState.Modified;
                return db.SaveChanges();
            }
        }

        public SysUser GetById(string id)
        {
            using (DBContainer db = new DBContainer())
            {
                return db.SysUser.SingleOrDefault(a => a.Id == id);
            }
        }

        public bool IsExist(string id)
        {
            using (DBContainer db = new DBContainer())
            {
                SysUser entity = GetById(id);
                if (entity != null)
                    return true;
                return false;
            }
        }
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
