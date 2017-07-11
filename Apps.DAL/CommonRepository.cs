using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Apps.IDAL;
using Apps.Models;
using System.Data.Entity;

namespace Apps.DAL
{
    public abstract class CommonRepository<T> : ICommonRepository<T> where T : class
    {
        DBContainer db;
        public CommonRepository(DBContainer context)
        {
            this.db = context;
        }
        public DBContainer Context
        {
            get { return db; }
        }
        /// <summary>
        /// 增
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public virtual bool Create(T entity)
        {
            db.Set<T>().Add(entity);
            return db.SaveChanges() > 0;
        }
        /// <summary>
        /// 改
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public virtual bool Edit(T entity)
        {
            if(db.Entry<T>(entity).State == EntityState.Modified)
            {
                return db.SaveChanges() > 0;
            }
            else if (db.Entry<T>(entity).State == EntityState.Detached)
            {
                try
                {
                    db.Set<T>().Attach(entity);
                    db.Entry<T>(entity).State = EntityState.Modified;
                    
                }
                catch(InvalidOperationException ex)
                {

                }
                return db.SaveChanges() > 0;
            }
            return false;
        }
        public virtual bool Delete(T entity)
        {
            db.Set<T>().Remove(entity);
            return db.SaveChanges() > 0;
        }
        public virtual int Delete(params object[] keyValues)
        {
            T entity = GetById(keyValues);
            if (entity != null)
            {
                db.Set<T>().Remove(entity);
                return db.SaveChanges();
            }
            return -1;
        }
        public virtual T GetById(params object[] keyValues)
        {
            return db.Set<T>().Find(keyValues);
        }
        public virtual IQueryable<T> GetList()
        {
            return db.Set<T>().AsQueryable();
        }
        public virtual IQueryable<T> GetList(Func<T,bool> whereLambda)
        {
            return db.Set<T>().Where(whereLambda).AsQueryable();
        }
        public virtual bool IsExist(string id)
        {
            T entity = GetById(id);
            return entity != null;
        }
        public virtual int SaveChanges()
        {
            return db.SaveChanges();
        }
    }
}
