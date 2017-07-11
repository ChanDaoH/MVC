using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Apps.Common;
using Apps.BLL.Core;
using Apps.IBLL;
using Apps.IDAL;
using Apps.Models.Sys;
using Apps.Models;
using Microsoft.Practices.Unity;

namespace Apps.BLL
{
    public class SysModuleOperateBLL:BaseBLL,ISysModuleOperateBLL
    {
        [Dependency]
        public ISysModuleOperateRepository Rep { get; set; }
        public List<SysModuleOperateModel> GetList(ref GridPager pager, string queryStr)
        {
            IQueryable<SysModuleOperate> queryData = null;
            queryData = Rep.GetList(entity => entity.ModuleId == queryStr);
            //queryData = queryData.Where();
            pager.totalRows = queryData.Count();
            queryData = LinqHelper.SortingAndPaging(queryData, pager.sort, pager.order, pager.page, pager.rows);
            List<SysModuleOperateModel> modelList = (from r in queryData
                                                      select new SysModuleOperateModel
                                                      {
                                                          Id = r.Id,
                                                          Name = r.Name,
                                                          KeyCode = r.KeyCode,
                                                          ModuleId = r.ModuleId,
                                                          IsValid = r.IsValid,
                                                          Sort = r.Sort
                                                      }).ToList();
            return modelList;
        }
        public bool Create(ref ValidationErrors errors, SysModuleOperateModel model)
        {
            try
            {
                SysModuleOperate entity = Rep.GetById(model.Id);
                if ( entity != null )
                {
                    errors.add(Suggestion.PrimaryRepeat);
                    return false;
                }
                entity = new SysModuleOperate()
                {
                    Id = model.Id,
                    Name = model.Name,
                    KeyCode = model.KeyCode,
                    ModuleId = model.ModuleId,
                    IsValid = model.IsValid,
                    Sort = model.Sort
                };
                if (Rep.Create(entity) )
                    return true;
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
                if (Rep.Delete(id) == 1)
                {
                    //清除无用的项
                    db.P_Sys_ClearUnusedRightOperate();
                    return true;
                }  
                else
                {
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
        public SysModuleOperateModel GetById(string id)
        {
            if ( IsExist(id) )
            {
                SysModuleOperate entity = Rep.GetById(id);
                SysModuleOperateModel model = new SysModuleOperateModel()
                {
                    Id = entity.Id,
                    Name = entity.Name,
                    KeyCode = entity.KeyCode,
                    ModuleId = entity.ModuleId,
                    IsValid = entity.IsValid,
                    Sort = entity.Sort
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
    }
}
