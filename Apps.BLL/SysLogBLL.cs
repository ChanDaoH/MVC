using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Apps.IBLL;
using Apps.IDAL;
using Apps.Models.Sys;
using Apps.Models;
using Apps.Common;
using Microsoft.Practices.Unity;
using Apps.BLL.Core;

namespace Apps.BLL
{
    public class SysLogBLL:ISysLogBLL
    {
        DBContainer db = new DBContainer();
        [Dependency]
        public ISysLogRepository Rep { get; set; }
        public List<SysLogModel> GetList(ref GridPager pager, string queryStr)
        {
            IQueryable<SysLog> list =  Rep.GetList(db);
            if ( !string.IsNullOrWhiteSpace(queryStr))
            {
                list = list.Where(r => r.Message.Contains(queryStr) || r.Module.Contains(queryStr));
            }
            pager.totalRows = list.Count();
            if ( pager.order == "desc")
            {
                list = list.OrderByDescending(r => r.CreateTime);
            }
            else
            {
                list = list.OrderBy(r => r.CreateTime);
            }
            return CreateModelList(ref pager, ref list);
        }
        public List<SysLogModel> CreateModelList(ref GridPager pager,ref IQueryable<SysLog> list )
        {
            if ( pager.page <= 1 )
            {
                list = list.Take(pager.rows);
            }
            else
            {
                list = list.Skip((pager.page - 1) * pager.rows).Take(pager.rows);
            }
            List<SysLogModel> modelList = (from r in list
                                           select new SysLogModel
                                           {
                                               Id = r.Id,
                                               Operator = r.Operator,
                                               Message = r.Message,
                                               Result = r.Result,
                                               Type = r.Type,
                                               Module = r.Module,
                                               CreateTime = r.CreateTime

                                           }).ToList();
            return modelList;
        }
        public SysLogModel GetById(string id)
        {
            SysLog entity = Rep.GetById(id);
            SysLogModel model = new SysLogModel();
            if (entity == null)
                return new SysLogModel();
            model.Id = entity.Id;
            model.Operator = entity.Operator;
            model.Message = entity.Message;
            model.Result = entity.Result;
            model.Type = entity.Type;
            model.Module = entity.Module;
            model.CreateTime = entity.CreateTime;
            return model;
        }
        /*
        public bool Create(SysLogModel model)
        {
            SysLog entity = new SysLog();
            entity.Id = model.Id;
            entity.Operator = model.Operator;
            entity.Message = model.Message;
            entity.Result = model.Result;
            entity.Type = model.Type;
            entity.Module = model.Module;
            entity.CreateTime = model.CreateTime;
            if ( (Rep.Create(entity) == 1))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        */
        public bool Delete(ref ValidationErrors errors, string id)
        {
            try
            {
                if (!string.IsNullOrWhiteSpace(id))
                {
                    if (Rep.Delete(id) == 1)
                    {
                        return true;
                    }
                    else
                    {
                        errors.add("删除失败");
                        return false;
                    }

                }
                else
                {
                    errors.add("主键为空");
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
    }
}
