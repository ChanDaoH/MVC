using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Apps.IBLL;
using Apps.Models.Sys;
using Apps.Common;
using Microsoft.Practices.Unity;
using Apps.Models;
using Apps.Web.Core;


namespace Apps.Web.Controllers
{
    public class SysLogController : BaseController
    {
        ValidationErrors errors = new ValidationErrors();
        [Dependency]
        public ISysLogBLL logBLL { get; set; }
        // GET: SysLog
        [SupportFilter(ActionName = "Index")]
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        [SupportFilter(ActionName = "Index")]
        public JsonResult GetList(GridPager pager,string queryStr)
        {
            List<SysLogModel> list = logBLL.GetList(ref pager, queryStr);
            var json = new
            {
                total = pager.totalRows,
                rows = (from r in list
                        select new SysLogModel
                        {
                            Id = r.Id,
                            Operator = r.Operator,
                            Message = r.Message,
                            Result = r.Result,
                            Type = r.Type,
                            Module = r.Module,
                            CreateTime = r.CreateTime
                        }).ToArray()
            };
            return MyJson(json, JsonRequestBehavior.AllowGet,"");
        }

        #region 详细
        public ActionResult Details(string id)
        {
            SysLogModel model = logBLL.GetById(id);
            return View(model);
        }
        #endregion
        
        #region 删除
        public JsonResult Delete(string id)
        {
            if ( logBLL.Delete(ref errors,id) )
            {
                LogHandler.WriteServiceLog("虚拟用户", "id:" + id, "成功", "删除", "日志记录");
                return Json(JsonHandler.CreateMessage(1, "删除成功"), JsonRequestBehavior.AllowGet);
            }
            else
            {
                LogHandler.WriteServiceLog("虚拟用户", "id:" + id, "失败", "删除", "日志记录");
                return Json(JsonHandler.CreateMessage(0, "删除失败", errors.Error), JsonRequestBehavior.AllowGet);
            }
        }
        #endregion
    }
}