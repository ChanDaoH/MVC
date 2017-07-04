using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Apps.Core;
using Microsoft.Practices.Unity;
using Apps.Common;
using Apps.BLL.Core;

namespace Apps.Web
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            //启用压缩
            BundleTable.EnableOptimizations = true;
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            //注入 Ioc
            var container = new UnityContainer();
            DependencyRegisterType.Container_Sys(ref container);
            DependencyResolver.SetResolver(new UnityDependencyResolver(container));
            Application["UnityContainer"] = container;
            
        }

        protected void Application_Error(object sender, EventArgs e)
        {
            string s = HttpContext.Current.Request.Url.ToString();
            HttpServerUtility server = HttpContext.Current.Server;
            if (server.GetLastError() != null)
            {
                Exception lastError = server.GetLastError();
                // 此处进行异常记录，可以记录到数据库或文本，也可以使用其他日志记录组件。
                ExceptionHandler.WriteException(lastError);
                Application["LastError"] = lastError;
                int statusCode = HttpContext.Current.Response.StatusCode;
                string exceptionOperator = "/SysException/Error";
                //string exceptionOperator = "/SysException";
                try
                {
                    if (!String.IsNullOrEmpty(exceptionOperator))
                    {
                        exceptionOperator = new System.Web.UI.Control().ResolveUrl(exceptionOperator);
                        //string url = string.Format("{0}", exceptionOperator);   //成功
                        string url = string.Format("{0}?ErrorUrl={1}", exceptionOperator, server.UrlEncode(s)); //成功
                        string script = String.Format("<script type='text/javascript'>window.top.location='{0}';</script>", url);
                        Response.Write(script);
                       // Response.Write("啦啦啦");
                        Response.End();
                    }
                }
                catch { }
            }
        }
    }

    
}
