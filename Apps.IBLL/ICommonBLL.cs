using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Apps.Common;
using Apps.Models.Sys;


namespace Apps.IBLL
{
    public interface ICommonBLL<T> where T : class
    {
        List<T> GetList(ref GridPager pager, string queryStr);
        bool Create(ref ValidationErrors errors, T model);
        bool Delete(ref ValidationErrors errors, string id);
        bool Edit(ref ValidationErrors errors, T model);
        T GetById(string id);
        bool IsExist(string id);
    }
}
