using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Apps.Models.Sys;
using Apps.Common;

namespace Apps.MIS.IBLL
{
    public partial interface IMIS_ArticleBLL
    {
        List<MIS_ArticleModel> GetListByPersonId(GridPager pager, string queryStr, string id);
    }
}
