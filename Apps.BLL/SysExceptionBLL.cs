using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Apps.IBLL;
using Apps.IDAL;
using Apps.Common;
using Apps.Models.Sys;
using Apps.Models;
using Microsoft.Practices.Unity;
using Apps.BLL.Core;

namespace Apps.BLL
{
    public class SysExceptionBLL:ISysExceptionBLL
    {
        DBContainer db = new DBContainer();
        [Dependency]
        public ISysExceptionRepository Rep { get; set; }
        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="pager"></param>
        /// <param name="queryStr"></param>
        /// <returns></returns>
        public List<SysExceptionModel> GetList(ref GridPager pager, string queryStr)
        {
            IQueryable<SysException> list = Rep.GetList(db);
            if ( !string.IsNullOrWhiteSpace(queryStr) )
            {
                list = list.Where(r => r.Message.Contains(queryStr));
            }
            pager.totalRows = list.Count();
            if (pager.order == "desc")
            {
                list = list.OrderByDescending(r => r.CreateTime);
            }
            else
            {
                list = list.OrderBy(r => r.CreateTime);
            }
            return CreateModelList(ref pager, ref list);
        }
        /// <summary>
        /// 创建griddata页
        /// </summary>
        /// <param name="pager"></param>
        /// <param name="list"></param>
        /// <returns></returns>
        public List<SysExceptionModel> CreateModelList(ref GridPager pager, ref IQueryable<SysException> list)
        {
            if (pager.page <= 1)
            {
                list = list.Take(pager.rows);
            }
            else
            {
                list = list.Skip((pager.page - 1) * pager.rows).Take(pager.rows);
            }
            List<SysExceptionModel> modelList = (from r in list
                                                 select new SysExceptionModel
                                                 {
                                                     Id = r.Id,
                                                     HelpLink = r.HelpLink,
                                                     Message = r.Message,
                                                     Source = r.Source,
                                                     StackTrace = r.StackTrace,
                                                     TargetSite = r.TargetSite,
                                                     Data = r.Data,
                                                     CreateTime = r.CreateTime
                                                 }).ToList();
            return modelList;
        }
        /// <summary>
        /// 根据id获取实体
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public SysExceptionModel GetById(string id)
        {
            SysException entity = Rep.GetById(id);
            if (entity == null)
                return null;
            SysExceptionModel model = new SysExceptionModel();
            model.Id = entity.Id;
            model.HelpLink = entity.HelpLink;
            model.Message = entity.Message;
            model.Source = entity.Source;
            model.StackTrace = entity.StackTrace;
            model.TargetSite = entity.TargetSite;
            model.Data = entity.Data;
            model.CreateTime = entity.CreateTime;
            return model;
        }
        /// <summary>
        /// 创建
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        /*
        public bool Create(SysExceptionModel model)
        {
            SysException entity = new SysException();
            entity.Id = model.Id;
            entity.HelpLink = model.HelpLink;
            entity.Message = model.Message;
            entity.Source = model.Source;
            entity.StackTrace = model.StackTrace;
            entity.TargetSite = model.TargetSite;
            entity.Data = model.Data;
            entity.CreateTime = model.CreateTime;
            if ( Rep.Create(entity) == 1 )
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        */
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
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
