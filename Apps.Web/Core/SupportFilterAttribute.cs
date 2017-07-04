using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Apps.Models.Sys;
using Apps.IBLL;
using Apps.BLL;
using Apps.DAL;
using Apps.IDAL;
using Microsoft.Practices.Unity;

namespace Apps.Web.Core
{
    public class SupportFilterAttribute :ActionFilterAttribute
    {
        public ISysUserBLL userBLL;
        public string ActionName { get; set; }
        private string Area;
        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            base.OnActionExecuted(filterContext);
        }

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            //获取路由
            //var routes = new RouteCollection();
            //RouteConfig.RegisterRoutes(routes);
           // RouteData routeData = routes.GetRouteData(filterContext.HttpContext);
            //获取控制器
            string ctlName = filterContext.Controller.ToString();
            //拆分
            string []ctlInfo = ctlName.Split('.');
            //controller action id
            string controller = null;
            string action = null;
            string id = null;
            //controller索引
            int ctlIndex = Array.IndexOf(ctlInfo, "Controllers") + 1;
            //获取控制器名称
            controller = ctlInfo[ctlIndex].Replace("Controller","");
            //获取区域
            int areaIndex = Array.IndexOf(ctlInfo, "Areas");
            if ( areaIndex > 0 )
            {
                //待验证
                Area = ctlInfo[areaIndex + 1];
            }
            //获取url
            string url = HttpContext.Current.Request.Url.ToString();
            string[] urlArr = url.Split('/');
            //获取url中控制器的索引
            int urlCtlIndex = Array.IndexOf(urlArr, controller);
            //action
            if ( urlArr.Count() > urlCtlIndex + 1 )
            {
                action = urlArr[urlCtlIndex + 1];
            }
            //id
            if ( urlArr.Count() > urlCtlIndex + 2 )
            {
                id = urlArr[urlCtlIndex + 2];
            }
            //筛选action
            action = string.IsNullOrEmpty(action) ? "Index" : action;
            int queryIndex = action.IndexOf('?', 0);
            if ( queryIndex > 0 )
            {
                action = action.Substring(0, queryIndex);
            }
            //筛选id
            id = string.IsNullOrEmpty(id) ? "" : id;
            //验证
            string filePath = HttpContext.Current.Request.FilePath;
            AccountModel account = filterContext.HttpContext.Session["Account"] as AccountModel;
            if (ValidatePermission(account, controller, action, filePath))
                return;
            else
            {
                filterContext.Result = new EmptyResult();
            }

        }

        public bool ValidatePermission( AccountModel account, string controller, string action, string filePath)
        {
            //question:ActionName代表了什么
            string actionName = string.IsNullOrEmpty(ActionName) ? action : ActionName;
            if (account != null)
            {
                List<PermModel> permList = null;
                if (!string.IsNullOrEmpty(Area))
                    controller = Area + "/" + controller;
                //获取权限
                permList = (List<PermModel>)HttpContext.Current.Session[filePath];
                if (permList == null)
                {
                    UnityContainer container = HttpContext.Current.Application["UnityContainer"] as UnityContainer;
                    userBLL = container.Resolve<ISysUserBLL>();
                    permList = userBLL.GetPermission(account.Id, controller);
                    HttpContext.Current.Session[filePath] = permList;
                }
                //当用户访问index时，只要权限>0就可以访问
                if (actionName.ToLower() == "index")
                {
                    if (permList.Count() > 0)
                        return true;
                }
                //查询当前Action 是否有操作权限，大于0表示有，否则没有
                int count = permList.Where(a => a.KeyCode.ToLower() == actionName.ToLower()).Count();
                if (count > 0)
                    return true;
                else
                {
                    HttpContext.Current.Response.Write("你没有操作权限，请联系管理员！");
                    return false;
                }
                    

            }
            else
            {
                HttpContext.Current.Response.Write("你没有操作权限，请联系管理员！");
                return false;
            }
                
        }
        public override void OnResultExecuted(ResultExecutedContext filterContext)
        {
            base.OnResultExecuted(filterContext);
        }
        public override void OnResultExecuting(ResultExecutingContext filterContext)
        {
            base.OnResultExecuting(filterContext);
        }
    }
}