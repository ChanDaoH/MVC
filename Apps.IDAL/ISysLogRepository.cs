using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Apps.Models;

namespace Apps.IDAL
{
    public interface ISysLogRepository
    {
        /// <summary>
        /// 获取列表
        /// </summary>
        /// <returns>数据列表</returns>
        IQueryable<SysLog> GetList(DBContainer db);
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="deleteCollection"></param>
        /// <returns></returns>
        int Delete(string id);
        /// <summary>
        /// 创建日志
        /// </summary>
        /// <param name="entity">日志</param>
        /// <returns></returns>
        int Create(SysLog entity);
        /// <summary>
        /// 根据Id获得实体
        /// </summary>
        /// <param name="id">id</param>
        /// <returns>实体</returns>
        SysLog GetById(string id);
    }
}
