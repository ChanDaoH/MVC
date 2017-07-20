using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Apps.Web.Core;
using Apps.MIS.IBLL;
using Apps.Common;
using Apps.Models.Sys;
using Microsoft.Practices.Unity;


namespace Apps.Web.Areas.MIS.Controllers
{
    public class CategoryController : BaseController
    {
        //业务层注入
        [Dependency]
        public IMIS_Article_CategoryBLL m_BLL { get; set; }

        public ValidationErrors errors = new ValidationErrors();

        // GET: MIS/Article
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
            List<MIS_Article_CategoryModel> modelList = m_BLL.GetList(ref pager, queryStr);
            var json = new
            {
                total = pager.totalRows,
                rows = (from r in modelList
                        select new MIS_Article_CategoryModel()
                        {
                            Id = r.Id,
                            ChannelId = r.ChannelId,
                            Name = r.Name,
                            ParentId = r.ParentId,
                            Sort = r.Sort,
                            ImgUrl = r.ImgUrl,
                            BodyContent = r.BodyContent,
                            CreateTime = r.CreateTime,
                            Enable = r.Enable

                        }).ToArray()
            };
            return Json(json, JsonRequestBehavior.AllowGet);
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
        public JsonResult Create(MIS_Article_CategoryModel model)
        {
            if (model != null && ModelState.IsValid)
            {

                if (m_BLL.Create(ref errors, model))
                {
                    LogHandler.WriteServiceLog(GetUserId(), "Id" + model.Id, "成功", "创建", "MIS_Article_Category");
                    return Json(JsonHandler.CreateMessage(1, Suggestion.InsertSucceed));
                }
                else
                {
                    string ErrorCol = errors.Error;
                    LogHandler.WriteServiceLog(GetUserId(), "Id" + model.Id + "," + ErrorCol, "失败", "创建", "MIS_Article_Category");
                    return Json(JsonHandler.CreateMessage(0, Suggestion.InsertFail + ErrorCol));
                }
            }
            else
            {
                return Json(JsonHandler.CreateMessage(0, Suggestion.InsertFail));
            }
        }
        #endregion


        #region 详细
        [SupportFilter]
        public ActionResult Details(string id)
        {
            ViewBag.perm = GetPermission();
            MIS_Article_CategoryModel model = m_BLL.GetById(id);
            return View(model);
        }
        #endregion

        #region 修改
        [SupportFilter]
        public ActionResult Edit(string id)
        {
            ViewBag.perm = GetPermission();
            MIS_Article_CategoryModel entity = m_BLL.GetById(id);
            return View(entity);
        }

        [HttpPost]
        [SupportFilter]
        public JsonResult Edit(MIS_Article_CategoryModel model)
        {
            if (model != null && ModelState.IsValid)
            {

                if (m_BLL.Edit(ref errors, model))
                {
                    LogHandler.WriteServiceLog(GetUserId(), "Id" + model.Id + ","+model.Name, "成功", "修改", "文章栏目");
                    return Json(JsonHandler.CreateMessage(1, Suggestion.EditSucceed));
                }
                else
                {
                    string ErrorCol = errors.Error;
                    LogHandler.WriteServiceLog(GetUserId(), "Id" + model.Id + "," + ErrorCol, "失败", "修改", "文章栏目");
                    return Json(JsonHandler.CreateMessage(0, Suggestion.EditFail + ErrorCol));
                }
            }
            else
            {
                return Json(JsonHandler.CreateMessage(0, Suggestion.EditFail));
            }
        }
        #endregion
        #region 删除
        [HttpPost]
        [SupportFilter]
        public JsonResult Delete(string id)
        {
            if (!string.IsNullOrWhiteSpace(id))
            {
                if (m_BLL.Delete(ref errors, id))
                {
                    LogHandler.WriteServiceLog(GetUserId(), "Id:" + id, "成功", "删除", "文章栏目");
                    return Json(JsonHandler.CreateMessage(1, Suggestion.DeleteSucceed));
                }
                else
                {
                    string ErrorCol = errors.Error;
                    LogHandler.WriteServiceLog(GetUserId(), "Id" + id + "," + ErrorCol, "失败", "删除", "文章栏目");
                    return Json(JsonHandler.CreateMessage(0, Suggestion.DeleteFail + ErrorCol));
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

