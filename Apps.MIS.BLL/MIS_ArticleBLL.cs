using Apps.Common;
using Apps.Models.Sys;
using Apps.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Apps.MIS.BLL
{
    public partial class MIS_ArticleBLL
    {
        public List<MIS_ArticleModel> GetListByPersonId(GridPager pager, string queryStr, string personId)
        {
            IQueryable<MIS_Article> queryData = null;
            if (queryStr != null) {
                queryData = m_Rep.GetList(a => a.Creater == personId && ((a.Id != null && a.Id.Contains(queryStr))

                                                        || (a.CategoryId != null && a.CategoryId.Contains(queryStr))
                                                        || (a.Title != null && a.Title.Contains(queryStr))
                                                        || (a.ImgUrl != null && a.ImgUrl.Contains(queryStr))
                                                        || (a.BodyContent != null && a.BodyContent.Contains(queryStr))



                                                        || (a.Checker != null && a.Checker.Contains(queryStr))

                                                        || (a.Creater != null && a.Creater.Contains(queryStr))));
            }
            else
            {
                queryData = m_Rep.GetList(a => a.Creater == personId);
            }
            pager.totalRows = queryData.Count();
            queryData = LinqHelper.SortingAndPaging(queryData, pager.sort, pager.order, pager.page, pager.rows);
            List<MIS_ArticleModel> modelList = (from r in queryData
                                                select new MIS_ArticleModel
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

                                                }).ToList();
            return modelList;
        }
    }
}
