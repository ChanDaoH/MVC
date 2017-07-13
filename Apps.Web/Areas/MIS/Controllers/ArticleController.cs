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
    public class ArticleController : BaseController
    {
        //业务层注入
        [Dependency]
        public IMIS_ArticleBLL m_BLL { get; set; }

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
        public JsonResult GetList(GridPager pager,string queryStr)
        {
            List<MIS_ArticleModel> modelList = m_BLL.GetList(ref pager, queryStr);
            var json = new {
                total = pager.totalRows,
                rows = (from r in modelList
                        select new MIS_ArticleModel() {
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

    }
}