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
    public class MyArticleController : BaseController
    {
        //业务层注入
        [Dependency]
        public IMIS_ArticleBLL m_BLL { get; set; }
        //错误信息
        public ValidationErrors errors = new ValidationErrors();
        // GET: MIS/MyArticle
        [SupportFilter]
        public ActionResult Index()
        {
            ViewBag.perm = GetPermission();
            return View();
        }

        [HttpPost]
        [SupportFilter(ActionName = "Index")]
        public JsonResult GetList(GridPager pager, string queryStr)
        {
            AccountModel account = GetAccount();
            if (account != null)
            {
                string userId = account.Id;
                List<MIS_ArticleModel> modelList = m_BLL.GetListByPersonId(pager, queryStr, userId);
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
            else
            {
                return Json(0, JsonRequestBehavior.AllowGet);
            }
        }

        #region 创建
        [SupportFilter(ActionName = "Create")]
        public ActionResult Create()
        {
            ViewBag.perm = GetPermission();
            return View();
        }

        [HttpPost]
        [SupportFilter]
        public JsonResult Create(MIS_ArticleModel model)
        {
            model.Id = System.Guid.NewGuid().ToString("N");
            model.ChannelId = 1;
            model.CheckFlag = false;
            model.Sort = 0;
            model.Click = 0;
            model.CreateTime = DateTime.Now;
            model.Creater = GetUserId();
            
            if (model != null && ModelState.IsValid)
            {
                if (m_BLL.Create(ref errors,model))
                {
                    LogHandler.WriteServiceLog(GetUserId(), "Id:" + model.Id + ",Title:" + model.Title,"成功", "Create", "文章管理系统");
                    return Json(JsonHandler.CreateMessage(1, Suggestion.InsertSucceed));
                }
                else
                {
                    string ErrorCol = errors.Error;
                    LogHandler.WriteServiceLog(GetUserId(), "Id:" + model.Id + ",Title:" + model.Title+","+ErrorCol, "失败", "Create", "文章管理系统");
                    return Json(JsonHandler.CreateMessage(0, Suggestion.InsertFail+ErrorCol));
                }
            }
            else
            {
                return Json(JsonHandler.CreateMessage(0, Suggestion.InsertFail));
            }
        }
        #endregion
        #region 删除
        [HttpPost]
        [SupportFilter]
        public JsonResult Delete(string id)
        {
            if (!string.IsNullOrEmpty(id))
            {
                if (m_BLL.Delete(ref errors,id))
                {
                    LogHandler.WriteServiceLog(GetUserId(), "id:" + id, "成功", "删除", "文章管理系统");
                    return Json(JsonHandler.CreateMessage(1, Suggestion.DeleteSucceed));
                }
                else
                {
                    string errorMes = errors.Error;
                    LogHandler.WriteServiceLog(GetUserId(), "id:" + id + "," + errorMes, "失败", "删除", "文章管理系统");
                    return Json(JsonHandler.CreateMessage(0, Suggestion.DeleteFail+errorMes));
                }
            }
            else
            {
                return Json(JsonHandler.CreateMessage(0, Suggestion.DeleteFail));
            }
        }
        #endregion
        #region 详细
        [SupportFilter]
        public ActionResult Details(string id)
        {
            ViewBag.perm = GetPermission();
            if (!string.IsNullOrEmpty(id))
            {
                MIS_ArticleModel model = m_BLL.GetById(id);
                return View(model);
            }
            else
            {
                MIS_ArticleModel model = new MIS_ArticleModel();
                return View(model);
            }
        }
        #endregion

    }
}