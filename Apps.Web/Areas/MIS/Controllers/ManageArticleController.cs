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
    public class ManageArticleController : BaseController
    {
        //业务层注入
        [Dependency]
        public IMIS_ArticleBLL m_BLL { get; set; }

        public ValidationErrors errors = new ValidationErrors();
        // GET: MIS/ManageArticle
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
            List<MIS_ArticleModel> modelList = m_BLL.GetList(ref pager, queryStr);
            var json = new
            {
                total = pager.totalRows,
                rows = (from r in modelList
                        select new MIS_ArticleModel()
                        {
                            Id = r.Id,
                            ChannelId = r.ChannelId,
                            CategoryId = r.CategoryId,
                            Title = r.Title,
                            ImgUrl = r.ImgUrl,
                            BodyContent = r.BodyContent,
                            Sort = r.Sort,
                            Click = r.Click,
                            CheckFlag = r.CheckFlag,
                            Checker = r.Checker,
                            CheckDateTime = r.CheckDateTime,
                            Creater = r.Creater,
                            CreateTime = r.CreateTime,

                        }).ToArray()
            };
            return Json(json, JsonRequestBehavior.AllowGet);
        }
        #region 详细
        [SupportFilter]
        public ActionResult Details(string id)
        {
            ViewBag.perm = GetPermission();
            MIS_ArticleModel model = m_BLL.GetById(id);
            return View(model);
        }
        #endregion

        #region 修改
        [SupportFilter]
        public ActionResult Edit(string id)
        {
            ViewBag.perm = GetPermission();
            MIS_ArticleModel entity = m_BLL.GetById(id);
            return View(entity);
        }

        [HttpPost]
        [SupportFilter]
        public JsonResult Edit(MIS_ArticleModel model)
        {
            if (model.CheckFlag )
            {
                model.Checker = GetUserId();
                model.CheckDateTime = DateTime.Now;

                if (model != null && ModelState.IsValid)
                {

                    if (m_BLL.Edit(ref errors, model))
                    {
                        LogHandler.WriteServiceLog(GetUserId(), "Id" + model.Id, "成功", "修改", "MIS_Article");
                        return Json(JsonHandler.CreateMessage(1, Suggestion.EditSucceed));
                    }
                    else
                    {
                        string ErrorCol = errors.Error;
                        LogHandler.WriteServiceLog(GetUserId(), "Id" + model.Id + "," + ErrorCol, "失败", "修改", "MIS_Article");
                        return Json(JsonHandler.CreateMessage(0, Suggestion.EditFail + ErrorCol));
                    }
                }
                else
                {
                    return Json(JsonHandler.CreateMessage(0, Suggestion.EditFail));
                }
            }
            else
            {
                return Json(JsonHandler.CreateMessage(0, "审核未通过"));
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
                    LogHandler.WriteServiceLog(GetUserId(), "Id:" + id, "成功", "删除", "MIS_Article");
                    return Json(JsonHandler.CreateMessage(1, Suggestion.DeleteSucceed));
                }
                else
                {
                    string ErrorCol = errors.Error;
                    LogHandler.WriteServiceLog(GetUserId(), "Id" + id + "," + ErrorCol, "失败", "删除", "MIS_Article");
                    return Json(JsonHandler.CreateMessage(0, Suggestion.DeleteFail + ErrorCol));
                }
            }
            else
            {
                return Json(JsonHandler.CreateMessage(0, Suggestion.DeleteFail));
            }


        }
        #endregion
        #region 创建
        [SupportFilter]
        public ActionResult Create()
        {
            ViewBag.perm = GetPermission();
            return View();
        }

        [HttpPost]
        [SupportFilter]
        public JsonResult Create(MIS_ArticleModel model)
        {
            model.Creater = GetUserId();
            model.Checker = GetUserId();
            model.CreateTime = DateTime.Now;
            model.CheckDateTime = DateTime.Now;
            model.CheckFlag = true;
            model.Sort = 0;
            model.Click = 0;
            model.Id = System.Guid.NewGuid().ToString("N");
            if (model != null && ModelState.IsValid)
            {

                if (m_BLL.Create(ref errors, model))
                {
                    LogHandler.WriteServiceLog(GetUserId(), "Id" + model.Id, "成功", "创建", "MIS_Article");
                    return Json(JsonHandler.CreateMessage(1, Suggestion.InsertSucceed));
                }
                else
                {
                    string ErrorCol = errors.Error;
                    LogHandler.WriteServiceLog(GetUserId(), "Id" + model.Id + "," + ErrorCol, "失败", "创建", "MIS_Article");
                    return Json(JsonHandler.CreateMessage(0, Suggestion.InsertFail + ErrorCol));
                }
            }
            else
            {
                return Json(JsonHandler.CreateMessage(0, Suggestion.InsertFail));
            }
        }
        #endregion
    }
}