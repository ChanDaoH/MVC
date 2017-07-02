using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.Practices.Unity;
using Apps.IBLL;
using Apps.Models;

namespace Apps.Web.Controllers
{
    public class HomeController : Controller
    {
        [Dependency]
        public IHomeBLL homeBLL { get; set; }
        // GET: Home
        public ActionResult Index()
        {
            return View();
        }
        /// <summary>
        /// 获取导航菜单
        /// </summary>
        /// <param name="moduleId">所属</param>
        /// <returns>树</returns>
        public JsonResult GetTree( string id )
        {
            List<SysModule> list = homeBLL.GetMenuByPersonId(id);
            var json =
            (
                from m in list
                select new
                {
                    id = m.Id,
                    text = m.Name,
                    value = m.Url,
                    showcheck = false,
                    complete = false,
                    isexpand = false,
                    checkstate = 0,
                    hasChildren = m.IsLast ? false : true,
                    Icon = m.Iconic
                }
            ).ToArray();
            return Json(json, JsonRequestBehavior.AllowGet);
        }
    }
}