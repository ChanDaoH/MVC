using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Apps.Models;
using Apps.IDAL;

namespace Apps.DAL
{
    public class SysExceptionRepository:ISysExceptionRepository,IDisposable
    {
        public int Create(SysException entity)
        {
            using (DBContainer db = new DBContainer())
            {
                db.Set<SysException>().Add(entity);
                return db.SaveChanges();
            }
        }
        public IQueryable<SysException> GetList(DBContainer db)
        {
            IQueryable<SysException> list = db.SysException.AsQueryable();
            return list;
        }
        public SysException GetById(string id)
        {
            using (DBContainer db = new DBContainer())
            {
                return db.SysException.SingleOrDefault(r => r.Id == id);
            }
        }
        public int Delete(string id)
        {
            using (DBContainer db = new DBContainer())
            {
                SysException entity = db.SysException.SingleOrDefault(r => r.Id == id);
                db.Set<SysException>().Remove(entity);
                return db.SaveChanges();
            }
        }
        public void Dispose()
        {

        }
    }
}
