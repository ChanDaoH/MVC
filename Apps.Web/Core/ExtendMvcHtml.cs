using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace Apps.Web.Core
{
    public static class ExtendMvcHtml
    {
        /// <summary>
        /// 普通按钮
        /// </summary>
        /// <param name="helper">htmlhelper</param>
        /// <param name="id">控件Id</param>
        /// <param name="icon">控件icon图标class</param>
        /// <param name="text">控件的名称</param>
        /// <param name="hr">分割线</param>
        /// <returns>html</returns>
        public static MvcHtmlString ToolButton(this HtmlHelper helper,string id,string icon,string text,bool hr)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("<a id=\"{0}\" style=\"float:left;\" class=\"l-btn l-btn-plain\">", id);
            sb.AppendFormat("<span class=\"l - btn - left\"><span class=\"l-btn-text {0}\" style=\"padding-left: 20px;\">",icon);
            sb.AppendFormat(text);
            sb.AppendFormat("</ span ></ span ></ a >");
            if ( hr)
            {
                sb.Append("<div class=\"datagrid - btn - separator\"></div>");
            }
            return new MvcHtmlString(sb.ToString());
        }
    }
}