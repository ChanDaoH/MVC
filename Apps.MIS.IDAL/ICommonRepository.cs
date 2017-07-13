using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Apps.MIS.IDAL
{
    public interface ICommonRepository<T> where T : class
    {
        bool Create(T entity);
        bool Edit(T entity);
        bool Delete(T entity);
        /// <summary>
        /// 按主键删除
        /// </summary>
        /// <param name="keyValues"></param>
        int Delete(params object[] keyValues);
        /// <summary>
        /// 获得所有数据
        /// </summary>
        /// <returns></returns>
        T GetById(params object[] keyValues);

        IQueryable<T> GetList();
        /// <summary>
        /// 根据表达式获取数据
        /// </summary>
        /// <param name="whereLambda"></param>
        /// <returns></returns>
        IQueryable<T> GetList(Func<T, bool> whereLambda);

        bool IsExist(string id);

        int SaveChanges();

    }
}
