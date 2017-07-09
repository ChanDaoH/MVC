using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Apps.IBLL;
using Apps.Common;
using Apps.BLL.Core;
using Apps.Models.Sys;
using Microsoft.Practices.Unity;
using Apps.Web.Core;

namespace Apps.Web.Controllers
{
    public class SysRoleController : BaseController
    {
        //业务层注入
        [Dependency]
        public ISysRoleBLL roleBLL { get; set; }
        //全局错误变量
        ValidationErrors errors = new ValidationErrors();

        // GET: SysRole
        [SupportFilter(ActionName = "Index")]
        public ActionResult Index()
        {
            ViewBag.perm = GetPermission();
            return View();
        }

        [HttpPost]
        [SupportFilter(ActionName = "Index")]
        public JsonResult GetList( GridPager pager, string queryStr)
        {
            List<SysRoleModel> modelList = roleBLL.GetList(ref pager, queryStr);
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
                            UserName = r.UserName
                        }).ToArray()
            };
            return Json(json, JsonRequestBehavior.AllowGet);
        }

        #region 创建
        [SupportFilter(ActionName = "Create")]
        public ActionResult Create()
        {
            ViewBag.perm = GetPermission();
            return View();
        }


        [HttpPost]
        [SupportFilter(ActionName = "Create")]
        public JsonResult Create(SysRoleModel model)
        {
            model.CreateTime = ResultHelper.NowTime;
            model.CreatePerson = GetUserId();
            if ( model != null && ModelState.IsValid )
            {
                if ( roleBLL.Create(ref errors,model) )
                {
                    LogHandler.WriteServiceLog(GetUserId(), "id:" + model.Id + ",Name:" + model.Name, "成功", "创建", "角色组管理");
                    return Json(JsonHandler.CreateMessage(1, Suggestion.InsertSucceed));
                }
                else
                {
                    string errorMes = errors.Error;
                    LogHandler.WriteServiceLog(GetUserId(), "id:" + model.Id + ",Name:" + model.Name+","+ errorMes, "失败", "创建", "角色组管理");
                    return Json(JsonHandler.CreateMessage(0, Suggestion.InsertFail + errorMes));
                }
            }
            else
            {
                return Json(JsonHandler.CreateMessage(0, Suggestion.InsertFail));
            }
        }
        #endregion

        #region 修改
        [SupportFilter(ActionName = "Edit")]
        public ActionResult Edit(string id)
        {
            ViewBag.perm = GetPermission();
            SysRoleModel model = roleBLL.GetById(id);
            return View(model);
        }

        [HttpPost]
        [SupportFilter(ActionName = "Edit")]
        public JsonResult Edit(SysRoleModel model)
        {
            if ( model != null && ModelState.IsValid )
            {
                if ( roleBLL.Edit(ref errors, model ))
                {
                    LogHandler.WriteServiceLog(GetUserId(), "id:" + model.Id + ",Name:" + model.Name, "成功", "修改", "角色组管理");
                    return Json(JsonHandler.CreateMessage(1, Suggestion.EditSucceed));
                }
                else
                {
                    string errorMes = errors.Error;
                    LogHandler.WriteServiceLog(GetUserId(), "id:" + model.Id + ",Name:" + model.Name + ","+errorMes, "失败", "修改", "角色组管理");
                    return Json(JsonHandler.CreateMessage(0, Suggestion.EditFail + errorMes));
                }
            }
            else
            {
                return Json(JsonHandler.CreateMessage(0, Suggestion.EditFail));
            }
        }
        #endregion
        #region 详细
        [SupportFilter(ActionName = "Details")]
        public ActionResult Details(string id)
        {
            SysRoleModel model = roleBLL.GetById(id);
            return View(model);
        }
        #endregion
        #region 删除
        [HttpPost]
        [SupportFilter(ActionName = "Delete")]
        public JsonResult Delete(string id)
        {
            if ( !string.IsNullOrWhiteSpace(id))
            {
                if ( roleBLL.Delete(ref errors, id))
                {
                    LogHandler.WriteServiceLog(GetUserId(), "id:" + id, "成功", "删除", "角色组管理");
                    return Json(JsonHandler.CreateMessage(1, Suggestion.DeleteSucceed));
                }
                else
                {
                    string errorMes = errors.Error;
                    LogHandler.WriteServiceLog(GetUserId(), "id:" + id+","+errorMes, "失败", "删除", "角色组管理");
                    return Json(JsonHandler.CreateMessage(0, Suggestion.DeleteFail + errorMes));
                }
            }
            else
            {
                return Json(JsonHandler.CreateMessage(0, Suggestion.DeleteFail));
            }
        }
        #endregion

        #region 分配角色
        [SupportFilter(ActionName = "Allot")]
        public ActionResult Allot(string id)
        {
            ViewBag.perm = GetPermission();
            ViewBag.roleId = id;
            return View();
        }

        [HttpPost]
        [SupportFilter(ActionName = "Allot")]
        public JsonResult GetUserByRoleId(GridPager pager,string id)
        {
            List<RoleUserInfoModel> modelList = roleBLL.GetUserByRoleId(ref pager ,id);
            var json = new
            {
                total = pager.totalRows,
                rows = (from r in modelList
                        select new RoleUserInfoModel() {
                            Id = r.Id,
                            UserName = r.UserName,
                            TrueName = r.TrueName,
                            Flag = r.Flag == "0" ? "0" : "1"
                        }).ToArray()
            };
            return Json(json,JsonRequestBehavior.AllowGet);
        }

        [SupportFilter(ActionName = "Save")]
        public JsonResult UpdateSysRoleSysUser(string roleId,string userIds)
        {
            string[] arr = userIds.Split(',');
            if ( roleBLL.UpdateSysRoleSysUser(ref errors,roleId, arr))
            {
                LogHandler.WriteServiceLog(GetUserId(), "roleId:" + roleId + ",userId:" + userIds, "成功", "分配用户", "角色设置");
                return Json(JsonHandler.CreateMessage(1, Suggestion.UpdateSucceed));
            }
            else
            {
                string errMes = errors.Error;
                LogHandler.WriteServiceLog(GetUserId(), "roleId:" + roleId + ",userId:" + userIds+","+errMes, "失败", "分配用户", "角色设置");
                return Json(JsonHandler.CreateMessage(0, Suggestion.UpdateFail+errMes));
            }
        }


        #endregion


    }
}