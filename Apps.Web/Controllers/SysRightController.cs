using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Apps.Common;
using Apps.IBLL;
using Apps.Models.Sys;
using Microsoft.Practices.Unity;
using Apps.Web.Core;

namespace Apps.Web.Controllers
{
    public class SysRightController : BaseController
    {
        [Dependency]
        public ISysRoleBLL roleBLL { get; set; }
        [Dependency]
        public ISysModuleBLL moduleBLL { get; set; }
        [Dependency]
        public ISysRightBLL rightBLL { get; set; }

        public ValidationErrors errors = new ValidationErrors();

        // GET: SysRight
        [SupportFilter(ActionName = "Index")]
        public ActionResult Index()
        {
            ViewBag.perm = GetPermission();
            return View();
        }
        /// <summary>
        /// 获取角色列表
        /// </summary>
        /// <param name="pager"></param>
        /// <returns></returns>
        public JsonResult GetRoleList(GridPager pager)
        {
            List<SysRoleModel> modelList = roleBLL.GetList(ref pager, "");
            var json = new
            {
                total = pager.totalRows,
                rows = (from r in modelList
                        select new SysRoleModel() {
                            Id = r.Id,
                            Name = r.Name,
                            Description = r.Description,
                            CreateTime = r.CreateTime,
                            CreatePerson = r.CreatePerson,
                        })
            };
            return Json(json, JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// 获取模块列表
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public JsonResult GetModuleList(string id)
        {
            if (id == null)
                id = "0";
            List<SysModuleModel> modelList = moduleBLL.GetList(id);
            var json = (from r in modelList
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
                            IsLast = r.IsLast,
                            state = moduleBLL.GetList(r.Id).Count() > 0 ? "closed" : "open"
                        });
            return Json(json, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetRightByRoleAndModule(GridPager pager,string roleId,string moduleId)
        {
            pager.page = 1;
            pager.rows = 10000;
            List<RightModuleOperateModel> modelList = rightBLL.GetRightByRoleAndModule(roleId, moduleId);
            pager.totalRows = modelList.Count();
            var json = new
            {
                total = pager.totalRows,
                rows = (from r in modelList
                        select new RightModuleOperateModel()
                        {
                            //id为rightoperate的id
                            Id = r.RightId + r.KeyCode,
                            Name = r.Name,
                            KeyCode = r.KeyCode,
                            isvalid = r.isvalid,
                            RightId = r.RightId
                        }).ToArray()
            };
            return Json(json, JsonRequestBehavior.AllowGet);
        }
        [SupportFilter(ActionName = "Save")]
        public bool UpdateRight(SysRightOperateModel model)
        {
            return rightBLL.UpdateRight(errors , model);
        }
    }
}