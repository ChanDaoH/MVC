using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Apps.Models;
using Apps.IDAL;

namespace Apps.DAL
{
    public class SysLogRepository:ISysLogRepository,IDisposable
    {
        /// <summary>
        /// 获取列表
        /// </summary>
        /// <returns>数据列表</returns>
        public IQueryable<SysLog> GetList(DBContainer db)
        {
            IQueryable<SysLog> list = db.SysLog.AsQueryable();
            return list;
        }
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="deleteCollection"></param>
        /// <returns></returns>
        public int Delete(string id)
        {
            using (DBContainer db = new DBContainer())
            {
                SysLog entity = db.SysLog.SingleOrDefault(r => r.Id == id);
                db.Set<SysLog>().Remove(entity);
                return db.SaveChanges();
            }
        }
        /// <summary>
        /// 创建日志
        /// </summary>
        /// <param name="entity">日志</param>
        /// <returns></returns>
        public int Create(SysLog entity)
        {
            using (DBContainer db = new DBContainer())
            {
                db.Set<SysLog>().Add(entity);
                return db.SaveChanges();
            }
        }
        /// <summary>
        /// 根据Id获得实体
        /// </summary>
        /// <param name="id">id</param>
        /// <returns>实体</returns>
        public SysLog GetById(string id)
        {
            using (DBContainer db = new DBContainer())
            {
                return db.SysLog.SingleOrDefault(r => r.Id == id);
            }
        }

        public void Dispose()
        {

        }
    }
}
