using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.Practices.Unity;
using Apps.Models.Sys;
using Apps.IBLL;
using Apps.Common;
using Apps.Web.Core;

namespace Apps.Web.Controllers
{
    public class SysExceptionController : BaseController
    {
        //ValidationErrors
        ValidationErrors errors = new ValidationErrors();
        [Dependency]
        public ISysExceptionBLL exceptionBLL { get; set; }
        // GET: SysException
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public JsonResult GetList(GridPager pager,string queryStr)
        {
            List<SysExceptionModel> list = exceptionBLL.GetList(ref pager, queryStr);
            var json = new
            {
                total = pager.totalRows,
                rows = (from r in list
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
                        }).ToArray()
            };
            return MyJson(json, JsonRequestBehavior.AllowGet,"");
        }

        #region 详细
        public ActionResult Details(string id)
        {
            SysExceptionModel model = exceptionBLL.GetById(id);
            return View(model);
        }
        #endregion

        #region 删除
        public JsonResult Delete(string id)
        {
            if (exceptionBLL.Delete(ref errors,id))
            {
                LogHandler.WriteServiceLog("虚拟用户", "id:" + id, "成功", "删除", "异常记录");
                return Json(JsonHandler.CreateMessage(1,"删除成功"), JsonRequestBehavior.AllowGet);
            }
            else
            {
                LogHandler.WriteServiceLog("虚拟用户", "id:" + id, "失败", "删除", "异常记录");
                return Json(JsonHandler.CreateMessage(0, "删除失败",errors.Error), JsonRequestBehavior.AllowGet);
            }
        }
        #endregion

        public ActionResult Error()
        {
            BaseException ex = new BaseException();
            return View(ex);
        }
    }
}