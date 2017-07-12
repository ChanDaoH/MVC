using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Apps.Models.Sys;
using Apps.Common;


namespace Apps.IBLL
{
    public partial interface ISysExceptionBLL
    {
        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="pager"></param>
        /// <param name="queryStr"></param>
        /// <returns></returns>
       // List<SysExceptionModel> GetList(ref GridPager pager, string queryStr);
        /// <summary>
        /// 根据id获取实体
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
      //  SysExceptionModel GetById(string id);
        /// <summary>
        /// 创建
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        //bool Create(SysExceptionModel model);
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
      //  bool Delete(ref ValidationErrors errors, string id);
    }
}
