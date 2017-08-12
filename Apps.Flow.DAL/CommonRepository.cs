using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Apps.Flow.IDAL;
using Apps.Flow.Models;
using System.Data.Entity;
using System.Linq.Expressions;

namespace Apps.Flow.DAL
{
    public abstract class CommonRepository<T> : ICommonRepository<T> where T : class
    {
        protected FlowContainer db;
        public CommonRepository(FlowContainer db)
        {
            this.db = db;
        }
        public FlowContainer Context
        {
            get
            {
                return db;
            }
        }
        public virtual bool Create(T model)
        {
            db.Set<T>().Add(model);
            return db.SaveChanges() > 0;
        }
        public virtual bool Edit(T model)
        {
            if(db.Entry<T>(model).State == EntityState.Modified)
            {
                return db.SaveChanges() > 0;
            }
            else if(db.Entry<T>(model).State == EntityState.Detached)
            {
                try
                {
                    db.Set<T>().Attach(model);
                    db.Entry<T>(model).State = EntityState.Modified;
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
            foreach( var item in keyValues)
            {
                T model = GetById(item);
                if( model != null)
                {
                    db.Set<T>().Remove(model);
                }
            }
            return db.SaveChanges();
        }
        public virtual T GetById(params object[] keyValues)
        {
            return db.Set<T>().Find(keyValues);
        }
        public virtual IQueryable<T> GetList()
        {
            return db.Set<T>();
        }
        public virtual IQueryable<T> GetList(Expression<Func<T,bool>> whereLambda)
        {
            return db.Set<T>().Where(whereLambda).AsQueryable();
        }
        public virtual IQueryable<T> GetList(int pageSize, int pageIndex, out int total
            , Expression<Func<T, bool>> whereLambda, bool isAsc, Expression<Func<T, bool>> orderByLambda)
        {
            var query = db.Set<T>().Where(whereLambda);
            total = query.Count();
            if( isAsc)
            {
                return query.OrderBy(orderByLambda).Skip<T>(pageIndex * (pageSize - 1)).Take<T>(pageSize);
            }
            else
            {
                return query.OrderByDescending(orderByLambda).Skip<T>(pageIndex * (pageSize - 1)).Take<T>(pageSize);
            }
        }
        public bool IsExist(object id)
        {
            return GetById(id) != null;
        }
        public int SaveChanges()
        {
            return db.SaveChanges();
        }
    }
}
