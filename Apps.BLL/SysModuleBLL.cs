using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Apps.Common;
using Apps.Models;
using Apps.Models.Sys;
using Apps.BLL.Core;
using Apps.IBLL;
using Apps.IDAL;
using Microsoft.Practices.Unity;

namespace Apps.BLL
{
    public class SysModuleBLL : BaseBLL , ISysModuleBLL
    {
        [Dependency]
        public ISysModuleRepository Rep { get; set; }
        public List<SysModuleModel> GetList(string parentId)
        {
            IQueryable<SysModule> queryData = Rep.GetList(db);
            queryData = queryData.Where(r => r.ParentId == parentId && r.Id != "0" ).OrderBy(r => r.Sort);
            List<SysModuleModel> modelList = (from r in queryData
                                              select new SysModuleModel()
                                              {
                                                  Id = r.Id,
                                                  Name = r.Name,
                                                  EnglishName = r.EnglishName,
                                                  ParentId = r.ParentId,
                                                  Url = r.Url,
                                                  Iconic = r.Iconic,
                                                  Sort = r.Sort,
                                                  Remark = r.Remark,
                                                  Enable = r.Enable,
                                                  CreatePerson = r.CreatePerson,
                                                  CreateTime = r.CreateTime,
                                                  IsLast = r.IsLast
                                                  
                                              }).ToList();
            return modelList;
        }
        public List<SysModuleModel> GetModuleBySystem(string parentId)
        {
            IQueryable<SysModule> queryData = Rep.GetModuleBySystem(db, parentId);
            List<SysModuleModel> modelList = (from r in queryData
                                              select new SysModuleModel()
                                              {
                                                  Id = r.Id,
                                                  Name = r.Name,
                                                  EnglishName = r.EnglishName,
                                                  ParentId = r.ParentId,
                                                  Url = r.Url,
                                                  Iconic = r.Iconic,
                                                  Sort = r.Sort,
                                                  Remark = r.Remark,
                                                  Enable = r.Enable,
                                                  CreatePerson = r.CreatePerson,
                                                  CreateTime = r.CreateTime,
                                                  IsLast = r.IsLast
                                              }).ToList();
            return modelList;
        }
        public bool Create(ref ValidationErrors errors, SysModuleModel model)
        {
            try
            {
                SysModule entity = Rep.GetById(model.Id);
                if ( entity != null )
                {
                    errors.add(Suggestion.PrimaryRepeat);
                }
                entity = new SysModule()
                {
                    Id = model.Id,
                    Name = model.Name,
                    EnglishName = model.EnglishName,
                    ParentId = model.ParentId,
                    Url = model.Url,
                    Iconic = model.Iconic,
                    Sort = model.Sort,
                    Remark = model.Remark,
                    Enable = model.Enable,
                    CreatePerson = model.CreatePerson,
                    CreateTime = model.CreateTime,
                    IsLast = model.IsLast
                };
                if (Rep.Create(entity) == 1)
                {
                    //分配给角色
                    //
                    db.P_Sys_InsertSysRight();
                    return true;
                }
                else
                {
                    errors.add(Suggestion.InsertFail);
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
        public bool Delete(ref ValidationErrors errors, string id)
        {
            try
            {
                if ( db.SysModule.AsQueryable().Where( a => a.SysModule2.Id == id ).Count() > 0 )
                {
                    errors.add("有下属关联，请先删除下属！");
                    return false;
                }
                Rep.Delete(db, id);
                if (db.SaveChanges() > 0)
                    return true;
                else
                {
                    //清理无用的项
                    //
                    errors.add(Suggestion.DeleteFail);
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
        public bool Edit(ref ValidationErrors errors, SysModuleModel model)
        {
            try
            {
                SysModule entity = Rep.GetById(model.Id);
                if ( entity == null )
                {
                    errors.add(Suggestion.Disable);
                    return false;
                }
                entity = new SysModule()
                {
                    Id = model.Id,
                    Name = model.Name,
                    EnglishName = model.EnglishName,
                    ParentId = model.ParentId,
                    Url = model.Url,
                    Iconic = model.Iconic,
                    Sort = model.Sort,
                    Remark = model.Remark,
                    Enable = model.Enable,
                    CreatePerson = model.CreatePerson,
                    CreateTime = model.CreateTime,
                    IsLast = model.IsLast
                };
                if (Rep.Edit(entity) == 1)
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
        public SysModuleModel GetById(string id)
        {
            if (IsExist(id))
            {
                SysModule entity = Rep.GetById(id);
                SysModuleModel model = new SysModuleModel()
                {
                    Id = entity.Id,
                    Name = entity.Name,
                    EnglishName = entity.EnglishName,
                    ParentId = entity.ParentId,
                    Url = entity.Url,
                    Iconic = entity.Iconic,
                    Sort = entity.Sort,
                    Remark = entity.Remark,
                    Enable = entity.Enable,
                    CreatePerson = entity.CreatePerson,
                    CreateTime = entity.CreateTime,
                    IsLast = entity.IsLast
                };
                return model;
            }
            else
            {
                return null;
            }

        }
        public bool IsExist(string id)
        {
            return Rep.IsExist(id);
        }
        public void Dispose()
        {

        }
    }
}
