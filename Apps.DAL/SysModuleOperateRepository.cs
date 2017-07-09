using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Apps.IDAL;
using Apps.Models;


namespace Apps.DAL
{
    public class SysModuleOperateRepository:ISysModuleOperateRepository,IDisposable
    {
        public IQueryable<SysModuleOperate> GetList(DBContainer db)
        {
            return db.SysModuleOperate.AsQueryable();
        }
        public int Create(SysModuleOperate entity)
        {
            using (DBContainer db = new DBContainer())
            {
                db.Set<SysModuleOperate>().Add(entity);
                return db.SaveChanges();
            }
        }
        public int Delete(string id)
        {
            using (DBContainer db = new DBContainer())
            {
                SysModuleOperate entity = db.SysModuleOperate.FirstOrDefault(m => m.Id == id);
                db.Set<SysModuleOperate>().Remove(entity);
                return db.SaveChanges();
            }
        }
        public SysModuleOperate GetById(string id)
        {
            using (DBContainer db = new DBContainer())
            {
                return db.SysModuleOperate.FirstOrDefault(m => m.Id == id);
            }
        }
        public bool IsExist(string id)
        {
            using (DBContainer db = new DBContainer())
            {
                SysModuleOperate entity = GetById(id);
                if (entity != null)
                    return true;
                return false;
            }
        }
        public void Dispose()
        {

        }
    }
}
