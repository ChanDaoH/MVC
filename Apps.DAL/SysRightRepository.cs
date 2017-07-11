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
    public partial class SysRightRepository:ISysRightRepository,IDisposable
    {
        /// <summary>
        /// 根据用户Id和控制器获取用户操作权限
        /// </summary>
        /// <param name="accountId">accountId</param>
        /// <param name="controller">controller</param>
        /// <returns></returns>
        public List<PermModel> GetPermission (string accountId, string controller)
        {
            using (DBContainer db = new DBContainer() )
            {
                var rights = db.P_Sys_GetRightOperate2(accountId, controller);
                List < PermModel > permList = (from r in rights
                                           select new PermModel
                                      {
                                          KeyCode = r.KeyCode,
                                          IsValid = r.IsValid
                                      }).ToList();
                return permList;
            }
        }
        /// <summary>
        /// 更新权限表
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>

        public bool UpdateRight(SysRightOperate model)
        {
            using (DBContainer db = new DBContainer())
            {
                SysRightOperate right = db.SysRightOperate.SingleOrDefault( r => r.Id == model.Id);
                if ( right != null )
                {
                    //也许right在上一句查询中已被添加到db容器中
                    right.IsValid = model.IsValid;
                    db.Set<SysRightOperate>().Attach(right);
                    db.Entry<SysRightOperate>(right).State = EntityState.Modified;
                }
                else
                {
                    db.Set<SysRightOperate>().Add(model);
                }
                //
                int i = db.SaveChanges();
                if (i > 0 )
                {
                    var sysRight = (from r in db.SysRight
                                    where r.Id == model.RightId
                                    select r).First();
                    db.P_Sys_UpdateSysRightRightFlag(sysRight.ModuleId, sysRight.RoleId);
                    return true;
                }
                return false;

            }
        }
        public List<RightModuleOperateModel> GetRightByRoleAndModule(string roleId, string moduleId)
        {
            using (DBContainer db = new DBContainer())
            {
                var operates = db.P_Sys_GetRightByRoleAndModule(roleId, moduleId);
                List<RightModuleOperateModel> modelList = (from r in operates
                                                           select new RightModuleOperateModel()
                                                           {
                                                               Id = r.Id,
                                                               Name = r.Name,
                                                               KeyCode = r.KeyCode,
                                                               ModuleId = r.ModuleId,
                                                               isvalid = r.isvalid,
                                                               Sort = r.Sort,
                                                               RightId = r.RightId
                                                           }).ToList();
                return modelList;
            }
        }
        public void Dispose()
        {

        }
    }
}
