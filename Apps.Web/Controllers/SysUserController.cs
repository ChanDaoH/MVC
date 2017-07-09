using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Apps.Models.Sys;
using Apps.Common;
using Apps.IBLL;
using Microsoft.Practices.Unity;
using Apps.Web.Core;

namespace Apps.Web.Controllers
{
    public class SysUserController : BaseController
    {
        [Dependency]
        public ISysUserBLL userBLL { get; set; }

        ValidationErrors errors = new ValidationErrors();

        // GET: SysUser
        [SupportFilter(ActionName = "Index")]
        public ActionResult Index()
        {
            ViewBag.perm = GetPermission();
            return View();
        }

        [HttpPost]
        [SupportFilter(ActionName = "Index")]
        public JsonResult GetList(GridPager pager, string queryStr)
        {
            List<SysUserModel> list = userBLL.GetList(ref pager, queryStr);
            var json = new
            {
                total = pager.totalRows,
                rows = (from r in list
                        select new SysUserModel()
                        {

                            Id = r.Id,
                            UserName = r.UserName,
                            Password = r.Password,
                            TrueName = r.TrueName,
                            Card = r.Card,
                            MobileNumber = r.MobileNumber,
                            PhoneNumber = r.PhoneNumber,
                            QQ = r.QQ,
                            EmailAddress = r.EmailAddress,
                            OtherContact = r.OtherContact,
                            Province = r.Province,
                            City = r.City,
                            Village = r.Village,
                            Address = r.Address,
                            State = r.State,
                            CreateTime = r.CreateTime,
                            CreatePerson = r.CreatePerson,
                            Sex = r.Sex,
                            Birthday = r.Birthday,
                            JoinDate = r.JoinDate,
                            Marital = r.Marital,
                            Political = r.Political,
                            Nationality = r.Nationality,
                            Native = r.Native,
                            School = r.School,
                            Professional = r.Professional,
                            Degree = r.Degree,
                            DepId = r.DepId,
                            PosId = r.PosId,
                            Expertise = r.Expertise,
                            JobState = r.JobState,
                            Photo = r.Photo,
                            Attach = r.Attach,
                            Roles = r.Roles
                        }).ToArray()

            };

            return Json(json,JsonRequestBehavior.AllowGet);
        }

        #region 创建
        [SupportFilter]
        public ActionResult Create()
        {
            ViewBag.perm = GetPermission();
            return View();
        }

        [HttpPost]
        [SupportFilter]
        public JsonResult Create(SysUserModel model)
        {
            model.CreatePerson = GetUserId();
            model.CreateTime = ResultHelper.NowTime;
            model.JoinDate = ResultHelper.NowTime; 
            model.Birthday = ResultHelper.NowTime;
            model.PosId = "20001";
            model.DepId = "20000";
            //var error = ModelState.Values.SelectMany(r => r.Errors);
            if (model != null && ModelState.IsValid)
            {

                if (userBLL.Create(ref errors, model))
                {
                    LogHandler.WriteServiceLog(GetUserId(), "Id" + model.Id + ",UserName" + model.UserName, "成功", "创建", "SysUser");
                    return Json(JsonHandler.CreateMessage(1, Suggestion.InsertSucceed));
                }
                else
                {
                    string ErrorCol = errors.Error;
                    LogHandler.WriteServiceLog(GetUserId(), "Id" + model.Id + ",UserName" + model.UserName + "," + ErrorCol, "失败", "创建", "SysUser");
                    return Json(JsonHandler.CreateMessage(0, Suggestion.InsertFail + ErrorCol));
                }
            }
            else
            {
                return Json(JsonHandler.CreateMessage(0, Suggestion.InsertFail));
            }
        }
        #endregion

        #region 修改
        [SupportFilter]
        public ActionResult Edit(string id)
        {
            ViewBag.perm = GetPermission();
            SysUserModel entity = userBLL.GetById(id);
            return View(entity);
        }

        [HttpPost]
        [SupportFilter]
        public JsonResult Edit(SysUserModel model)
        {
            if (model != null && ModelState.IsValid)
            {

                if (userBLL.Edit(ref errors, model))
                {
                    LogHandler.WriteServiceLog(GetUserId(), "Id" + model.Id + ",UserName" + model.UserName, "成功", "修改", "SysUser");
                    return Json(JsonHandler.CreateMessage(1, Suggestion.EditSucceed));
                }
                else
                {
                    string ErrorCol = errors.Error;
                    LogHandler.WriteServiceLog(GetUserId(), "Id" + model.Id + ",UserName" + model.UserName + "," + ErrorCol, "失败", "修改", "SysUser");
                    return Json(JsonHandler.CreateMessage(0, Suggestion.EditFail + ErrorCol));
                }
            }
            else
            {
                return Json(JsonHandler.CreateMessage(0, Suggestion.EditFail));
            }
        }
        #endregion

        #region 详细
        [SupportFilter]
        public ActionResult Details(string id)
        {
            ViewBag.perm = GetPermission();
            SysUserModel entity = userBLL.GetById(id);
            return View(entity);
        }
        #endregion

        #region 删除
        [HttpPost]
        [SupportFilter]
        public JsonResult Delete(string id)
        {
            if (!string.IsNullOrWhiteSpace(id))
            {
                if (userBLL.Delete(ref errors, id))
                {
                    LogHandler.WriteServiceLog(GetUserId(), "Id:" + id, "成功", "删除", "SysUser");
                    return Json(JsonHandler.CreateMessage(1, Suggestion.DeleteSucceed));
                }
                else
                {
                    string ErrorCol = errors.Error;
                    LogHandler.WriteServiceLog(GetUserId(), "Id" + id + "," + ErrorCol, "失败", "删除", "SysUser");
                    return Json(JsonHandler.CreateMessage(0, Suggestion.DeleteFail + ErrorCol));
                }
            }
            else
            {
                return Json(JsonHandler.CreateMessage(0, Suggestion.DeleteFail));
            }


        }



        #endregion

        #region 获取角色
        [SupportFilter(ActionName = "Allot")]
        public ActionResult Allot(string id)
        {
            ViewBag.perm = GetPermission();
            ViewBag.userId = id;
            return View();
        }
        [HttpPost]
        [SupportFilter(ActionName = "Allot")]
        public JsonResult GetRoleByUserId(GridPager pager ,string id)
        {
            List<UserRoleInfoModel> modelList = userBLL.GetRoleByUserId(ref pager, id);
            var json = new
            {
                total = pager.totalRows,
                rows = (from r in modelList
                        select new UserRoleInfoModel()
                        {
                            Id = r.Id,
                            Name = r.Name,
                            Description = r.Description,
                            CreateTime = r.CreateTime,
                            CreatePerson = r.CreatePerson,
                            Flag = r.Flag == "0" ? "0" : "1"
                        }).ToArray()
            };
            return Json(json, JsonRequestBehavior.AllowGet);
        }

        #endregion

        #region 更新角色用户
        [SupportFilter(ActionName = "Save")]
        public JsonResult UpdateSysRoleSysUser(string userId,string roleIds)
        {
            string[] arr = roleIds.Split(',');
            if ( userBLL.UpdateSysRoleSysUser(ref errors,userId,arr))
            {
                LogHandler.WriteServiceLog(GetUserId(), "userId:" + userId + ",roleIds:" + roleIds, "成功", "角色分配", "用户设置");
                return Json(JsonHandler.CreateMessage(1, Suggestion.UpdateSucceed));
            }
            else
            {
                string ErrorCol = errors.Error;
                LogHandler.WriteServiceLog(GetUserId(), "userId:" + userId + ",roleIds:" + roleIds+","+ErrorCol, "失败", "角色分配", "用户设置");
                return Json(JsonHandler.CreateMessage(0, Suggestion.UpdateFail + ErrorCol));
            }
        }
        #endregion
    }
}