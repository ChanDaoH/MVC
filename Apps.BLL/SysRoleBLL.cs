using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Apps.IBLL;
using Apps.IDAL;
using Apps.Common;
using Apps.Models;
using Apps.Models.Sys;
using Apps.BLL.Core;
using Microsoft.Practices.Unity;

namespace Apps.BLL
{
    public partial class SysRoleBLL :  ISysRoleBLL
    {
        //业务层注入
        [Dependency]
        public ISysRoleRepository Rep { get; set; }
        
        public override List<SysRoleModel> GetList(ref GridPager pager, string queryStr)
        {
            IQueryable<SysRole> queryData = null;
            if (!string.IsNullOrWhiteSpace(queryStr))
            {
                queryData = Rep.GetList(r => r.Name.Contains(queryStr));
            }
            else
            {
                queryData = Rep.GetList();
            }
            pager.totalRows = queryData.Count();
            queryData = LinqHelper.SortingAndPaging(queryData, pager.sort, pager.order, pager.page, pager.rows);
            List<SysRoleModel> modelList = (from r in queryData
                                            select new SysRoleModel() {
                                                Id = r.Id,
                                                Name = r.Name,
                                                Description = r.Description,
                                                CreatePerson = r.CreatePerson,
                                                CreateTime = r.CreateTime,
                                                UserName = (from a in r.SysUser
                                                            select a.UserName).ToList()
                                            }).ToList();
            return modelList;
        }
        public override bool Create(ref ValidationErrors errors, SysRoleModel model)
        {
            try
            {
                SysRole entity = Rep.GetById(model.Id);
                if ( entity != null )
                {
                    errors.add(Suggestion.PrimaryRepeat);
                    return false;
                }
                else
                {
                    entity = new SysRole() {
                        Id = model.Id,
                        Name = model.Name,
                        Description = model.Description,
                        CreatePerson = model.CreatePerson,
                        CreateTime = model.CreateTime
                    };
                    if (Rep.Create(entity) )
                    {
                        //原来想放置DAL层，可惜失败
                        db.P_Sys_InsertSysRight();   //插入角色时，更新权限表
                        //db.SaveChanges();   //未保存无效,错误的观点
                        return true;
                    }
                    else
                    {
                        errors.add(Suggestion.InsertFail);
                        return false;
                    }
                }
            }
            catch (Exception ex)
            {
                errors.add(ex.Message);
                ExceptionHandler.WriteException(ex);
                return false;
            }
        }
        
        /*
        public bool Edit(ref ValidationErrors errors, SysRoleModel model)
        {
            try
            {
                SysRole entity = Rep.GetById(model.Id);
                if (entity == null)
                {
                    errors.add(Suggestion.Disable);
                    return false;
                }
                entity = new SysRole()
                {
                    Id = model.Id,
                    Name = model.Name,
                    Description = model.Description,
                    CreatePerson = model.CreatePerson,
                    CreateTime = model.CreateTime
                };
                if (Rep.Edit(entity) )
                    return true;
                else
                {
                    errors.add(Suggestion.EditFail);
                    return false;
                }
            }
            catch(Exception ex)
            {
                errors.add(ex.Message);
                ExceptionHandler.WriteException(ex);
                return false;
            }
        }
        */
        /*
        public bool Delete(ref ValidationErrors errors, string id)
        {
            try
            {
                if (Rep.Delete(id) == 1)
                    return true;
                else
                {
                    errors.add(Suggestion.DeleteFail);
                    return false;
                }
            }
            catch ( Exception ex )
            {
                errors.add(ex.Message);
                ExceptionHandler.WriteException(ex);
                return false;
            }

        }
        */
        /*
        public SysRoleModel GetById(string id)
        {
            SysRole entity = Rep.GetById(id);
            if (entity == null)
                return null;
            SysRoleModel model = new SysRoleModel() {
                Id = entity.Id,
                Name = entity.Name,
                Description = entity.Description,
                CreatePerson = entity.CreatePerson,
                CreateTime = entity.CreateTime
            };
            return model;
        }
        public bool IsExist(string id)
        {
            return Rep.IsExist(id);
        }*/
        /// <summary>
        /// 根据角色ID获取用户分配详情
        /// </summary>
        /// <param name="pager"></param>
        /// <param name="roleId"></param>
        /// <returns></returns>
        public List<RoleUserInfoModel> GetUserByRoleId(ref GridPager pager, string roleId)
        {
            var users = Rep.GetUserByRoleId(db, roleId);
            pager.totalRows = users.Count();
            var queryData = users.AsQueryable();
            queryData = LinqHelper.SortingAndPaging(queryData, pager.sort, pager.order, pager.page, pager.rows);
            List<RoleUserInfoModel> modelList = (from r in queryData
                                                 select new RoleUserInfoModel()
                                                 {
                                                     Id = r.Id,
                                                     UserName = r.UserName,
                                                     TrueName = r.TrueName,
                                                     Flag = r.Flag
                                                 }).ToList();
            return modelList;
        }

        public bool UpdateSysRoleSysUser(ref ValidationErrors errors, string roleId, string[] userIds)
        {
            try
            {
                Rep.UpdateSysRoleSysUser(roleId, userIds);
                return true;
            }
            catch(Exception ex)
            {
                errors.add(ex.Message);
                ExceptionHandler.WriteException(ex);
                return false;
            }
        }
    }
}
