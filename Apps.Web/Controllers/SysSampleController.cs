using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Apps.IBLL;
using Apps.Models;
using Apps.Models.Sys;
using Microsoft.Practices.Unity;
using Apps.Common;
using Apps.Web.Core;

namespace Apps.Web.Controllers
{
    
    public class SysSampleController : BaseController
    {
        // GET: SysSample
        /// <summary>
        /// 业务层注入
        /// </summary>
        /// <returns></returns>
        //全局变量 异常记录
        ValidationErrors errors = new ValidationErrors();

        [Dependency]
        public ISysSampleBLL m_BLL { get; set; }

        public ActionResult Index()
        {
            //  List<SysSampleModel> list = m_BLL.GetList("");
            // return View(list);
           
            return View();
        }

        [HttpPost]
        [SupportFilter(ActionName = "Index")]
        public JsonResult Getlist(GridPager pager,string queryStr = null)
        {
            List<SysSampleModel> list = m_BLL.GetList(ref pager, queryStr);
            var json = new
            {
                total = pager.totalRows,
                rows = (from r in list
                        select new SysSampleModel()
                        {
                            Id = r.Id,
                            Name = r.Name,
                            Age = r.Age,
                            Bir = r.Bir,
                            Photo = r.Photo,
                            Note = r.Note,
                            CreateTime = r.CreateTime

                        }).ToArray()
            };
            return MyJson(json, JsonRequestBehavior.AllowGet,"");
        }
        #region 创建
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public JsonResult Create(SysSampleModel model)
        {
            if ( m_BLL.Create(ref errors,model))
            {
                LogHandler.WriteServiceLog("虚拟用户", "Id:" + model.Id + ",Name:" + model.Name, "成功", "创建", "样例程序");
                return MyJson(JsonHandler.CreateMessage(1,"插入成功"), JsonRequestBehavior.AllowGet,"");
            }
            else
            {
                LogHandler.WriteServiceLog("虚拟用户", "Id:" + model.Id + ",Name:" + model.Name + ","+errors.Error, "失败", "创建", "样例程序");
                return Json(JsonHandler.CreateMessage(0,"插入失败",errors.Error), JsonRequestBehavior.AllowGet);
            }
        }
        #endregion
        #region 修改
        public ActionResult Edit(string id)
        {
            SysSampleModel model = m_BLL.GetById(id);
            return View(model);
        }
        [HttpPost]
        public JsonResult Edit(SysSampleModel model)
        {

            //Convert.ToInt16("dddd");
            if ( m_BLL.Edit( ref errors ,model ) )
            {
                LogHandler.WriteServiceLog("虚拟用户", "Id:" + model.Id + ",Name:" + model.Name, "成功", "编辑", "样例程序");
                return Json(JsonHandler.CreateMessage(1,"修改成功"), JsonRequestBehavior.AllowGet);
            }
            else
            {
                LogHandler.WriteServiceLog("虚拟用户", "Id:" + model.Id + ",Name:" + model.Name+","+errors.Error, "失败", "编辑", "样例程序");
                return Json(JsonHandler.CreateMessage(0,"修改失败",errors.Error), JsonRequestBehavior.AllowGet);
            }
        }
        #endregion
        #region 详细
        public ActionResult Details(string id)
        {
            SysSampleModel model = m_BLL.GetById(id);
            return View(model);
        }
        #endregion
        #region 删除
        public JsonResult Delete(string id)
        {
            if( !string.IsNullOrWhiteSpace(id))
            {
                SysSampleModel model = m_BLL.GetById(id);
                if ( m_BLL.Delete(ref errors ,id ))
                {
                    LogHandler.WriteServiceLog("虚拟用户", "Id:" + model.Id + ",Name:" + model.Name, "成功", "删除", "样例程序");
                    return Json(JsonHandler.CreateMessage(1, "删除成功"), JsonRequestBehavior.AllowGet);
                }
                else
                {
                    LogHandler.WriteServiceLog("虚拟用户", "Id:" + model.Id + ",Name:" + model.Name, "失败", "删除", "样例程序");
                    return Json(JsonHandler.CreateMessage(0, "删除失败", errors.Error),JsonRequestBehavior.AllowGet);
                }
            }
            else
            {
                return Json(JsonHandler.CreateMessage(0, "删除失败", "未找到有效实体"), JsonRequestBehavior.AllowGet);
            }
        }
        #endregion
        [SupportFilter]
        public void Test()
        {

        }
    }
    
}