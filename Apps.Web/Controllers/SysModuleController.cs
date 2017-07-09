using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Apps.Models.Sys;
using Apps.IBLL;
using Microsoft.Practices.Unity;
using Apps.Web.Core;
using Apps.Common;


namespace Apps.Web.Controllers
{
    public class SysModuleController : BaseController
    {
        ValidationErrors errors = new ValidationErrors();
        //业务层注入
        [Dependency]
        public ISysModuleBLL moduleBLL { get; set; }
        [Dependency]
        public ISysModuleOperateBLL operateBLL { get; set; }

        /// <summary>
        /// 主页
        /// </summary>
        /// <returns>视图</returns>
        [SupportFilter]
        public ActionResult Index()
        {
            ViewBag.perm = GetPermission();
            return View();
        }
        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost]
        [SupportFilter(ActionName = "Index")]
        public JsonResult GetList(string id)
        {
            if (id == null)
                id = "0";
            List<SysModuleModel> modelList = moduleBLL.GetList(id);
            //string tag = modelList.Count > 0 ? "closed" : "open";
            var json = (from r in modelList
                        select new SysModuleModel
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
                            state = moduleBLL.GetList(r.Id).Count > 0 ? "closed" : "open"   //动态获取state
                        }).ToArray();
            return Json(json, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        [SupportFilter(ActionName = "Index")]
        public JsonResult GetOptListByModule(GridPager pager, string mid)
        {
            pager.rows = 1000;
            pager.page = 1;
            List<SysModuleOperateModel> modelList = operateBLL.GetList(ref pager, mid);
            var json = new
            {
                total = pager.totalRows,
                rows = (from r in modelList
                        select new SysModuleOperateModel() {
                            Id = r.Id,
                            Name = r.Name,
                            KeyCode = r.KeyCode,
                            ModuleId = r.ModuleId,
                            IsValid = r.IsValid,
                            Sort = r.Sort
                        }).ToArray()
            };
            return Json(json, JsonRequestBehavior.AllowGet);
        }

        #region 创建模块
        [SupportFilter(ActionName = "Create")]
        public ActionResult Create(string id)
        {
            ViewBag.perm = GetPermission();
            SysModuleModel entity = new SysModuleModel()
            {
                ParentId = id,
                Enable = true,
                Sort = 0
            };
            return View(entity);
        }

        [HttpPost]
        [SupportFilter(ActionName = "Create")]
        public JsonResult Create(SysModuleModel model)
        {
            model.CreateTime = ResultHelper.NowTime;
            model.CreatePerson = GetUserId();
            if ( model != null &&  ModelState.IsValid )
            {
                if (moduleBLL.Create(ref errors, model))
                {
                    LogHandler.WriteServiceLog(GetUserId(), "id:" + model.Id + ",Name:" + model.Name, "成功", "创建", "SysModule");
                    return Json(JsonHandler.CreateMessage(1, Suggestion.InsertSucceed));
                }
                else
                {
                    string errorMes = errors.Error;
                    LogHandler.WriteServiceLog(GetUserId(), "id:" + model.Id + ",Name:" + model.Name + "," + errorMes, "失败", "创建", "SysModule");
                    return Json(JsonHandler.CreateMessage(0, Suggestion.InsertFail + errorMes));
                }
            }
            else
            {
                return Json(JsonHandler.CreateMessage(0, Suggestion.InsertFail));
            }
            
        }
        #endregion

        #region 创建模块操作码
        [SupportFilter(ActionName = "Create")]
        public ActionResult CreateOperate(string moduleId)
        {
            ViewBag.perm = GetPermission();
            SysModuleOperateModel model = new SysModuleOperateModel()
            {
                ModuleId = moduleId,
                IsValid = true
            };
            return View(model);
        }

        [HttpPost]
        [SupportFilter( ActionName = "Create")]
        public JsonResult CreateOperate(SysModuleOperateModel model)
        {
            model.Id = model.ModuleId + model.KeyCode;
            if ( model != null && ModelState.IsValid )
            {
                //判断是否已存在该操作码
                if (operateBLL.GetById(model.Id) != null)
                {
                    return Json(JsonHandler.CreateMessage(0, Suggestion.PrimaryRepeat));
                }
                if ( operateBLL.Create( ref errors, model ))
                {
                    LogHandler.WriteServiceLog(GetUserId(), "id:" + model.Id + "Name:" + model.Name, "成功", "创建", "模块操作码");
                    return Json(JsonHandler.CreateMessage(1, Suggestion.InsertSucceed));
                }
                else
                {
                    string errorMes = errors.Error;
                    LogHandler.WriteServiceLog(GetUserId(), "id:" + model.Id + "Name:" + model.Name + "," + errorMes, "成功", "创建", "模块操作码");
                    return Json(JsonHandler.CreateMessage(0, Suggestion.InsertFail + errorMes));
                }
            }
            else
            {
                return Json(JsonHandler.CreateMessage(0, Suggestion.InsertFail));
            }
            
        }
        #endregion

        #region 修改模块
        [SupportFilter( ActionName = "Edit")]
        public ActionResult Edit(string mid)
        {
            ViewBag.perm = GetPermission();
            SysModuleModel model = moduleBLL.GetById(mid);
            return View(model);
        }
        [HttpPost]
        [SupportFilter( ActionName = "Edit" )]
        public JsonResult Edit(SysModuleModel model)
        {
            if ( model != null && ModelState.IsValid )
            {
                if ( moduleBLL.Edit( ref errors, model ))
                {
                    LogHandler.WriteServiceLog(GetUserId(), "id:" + model.Id + "Name:" + model.Name, "成功", "修改", "SysModule");
                    return Json(JsonHandler.CreateMessage(1, Suggestion.EditSucceed));
                }
                else
                {
                    string errorMes = errors.Error;
                    LogHandler.WriteServiceLog(GetUserId(), "id:" + model.Id + "Name:" + model.Name+","+errorMes , "失败", "修改", "SysModule");
                    return Json(JsonHandler.CreateMessage(0, Suggestion.EditFail + errorMes));
                }
            }
            else
            {
                return  Json(JsonHandler.CreateMessage(0, Suggestion.EditFail));
            }
        }
        #endregion

        #region 删除模块
        [HttpPost]
        [SupportFilter(ActionName = "Delete")]
        public JsonResult Delete(string mid)
        {
            if ( !string.IsNullOrWhiteSpace(mid))
            {
                if ( moduleBLL.Delete(ref errors,mid))
                {
                    LogHandler.WriteServiceLog(GetUserId(), "id:" + mid, "成功", "删除", "SysModule");
                    return Json(JsonHandler.CreateMessage(1, Suggestion.DeleteSucceed));
                }
                else
                {
                    string errorMes = errors.Error;
                    LogHandler.WriteServiceLog(GetUserId(), "id:" + mid + "," + errorMes, "失败", "删除", "SysModule");
                    return Json(JsonHandler.CreateMessage(0, Suggestion.DeleteFail + errorMes));
                }
            }
            else
            {
                return Json(JsonHandler.CreateMessage(0, Suggestion.DeleteFail));
            }
        }

        [HttpPost]
        [SupportFilter( ActionName = "Delete")]
        public JsonResult DeleteOperate(string mid)
        {
            if (!string.IsNullOrWhiteSpace(mid))
            {
                if (operateBLL.Delete(ref errors, mid))
                {
                    LogHandler.WriteServiceLog(GetUserId(), "id:" + mid, "成功", "删除", "模块操作码");
                    return Json(JsonHandler.CreateMessage(1, Suggestion.DeleteSucceed));
                }
                else
                {
                    string errorMes = errors.Error;
                    LogHandler.WriteServiceLog(GetUserId(), "id:" + mid + "," + errorMes, "失败", "删除", "模块操作码");
                    return Json(JsonHandler.CreateMessage(0, Suggestion.DeleteFail + errorMes));
                }
            }
            else
            {
                return Json(JsonHandler.CreateMessage(0, Suggestion.DeleteFail));
            }
        }
        #endregion
    }
}