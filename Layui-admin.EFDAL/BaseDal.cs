using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Layui_admin.EFDAL
{
    public class BaseDal<T> where T : class, new()
    {
        public DbContext dbContext = DbContextFactory.GetDbcontext();
        public T AddEntity(T entity)
        {
            dbContext.Set<T>().Add(entity);
            //dbContext.SaveChanges();
            return entity;
        }
        public bool Update(T entity)
        {
            if (dbContext.Entry<T>(entity).State == EntityState.Detached)
            {
                HandleDetached(entity);
            }
            dbContext.Set<T>().Attach(entity);
            dbContext.Entry<T>(entity).State = System.Data.Entity.EntityState.Modified;
            //return dbContext.SaveChanges() > 0;
            return true;
        }
        private bool HandleDetached(T entity)
        {
            var objectContext = ((IObjectContextAdapter)dbContext).ObjectContext;
            var entitySet = objectContext.CreateObjectSet<T>();
            var entityKey = objectContext.CreateEntityKey(entitySet.EntitySet.Name, entity);
            object foundSet;
            bool exists = objectContext.TryGetObjectByKey(entityKey, out foundSet);
            if (exists)
            {
                objectContext.Detach(foundSet); //从上下文中移除
            }
            return exists;
        }
        public bool Delete(T entity)
        {
            dbContext.Entry(entity).State = System.Data.Entity.EntityState.Deleted;
            //return dbContext.SaveChanges() > 0; 
            return true;
        }
        public IQueryable<T> GetEntitys(Expression<Func<T, bool>> whereLambda)
        {
            return dbContext.Set<T>().Where(whereLambda).AsQueryable();
        }

        public IQueryable<T> GetPages<S>(int pageSize, int pageIndex, out int count, Expression<Func<T, bool>> whereLambda, Expression<Func<T, S>> orderByLambda, bool IsAsc)
        {
            count = dbContext.Set<T>().Where(whereLambda).Count();
            if (IsAsc)
            {
                var temp = dbContext.Set<T>().Where(whereLambda).OrderBy(orderByLambda).Skip(pageSize * (pageIndex - 1)).Take(pageSize).AsQueryable();
                return temp;
            }
            else
            {
                var temp = dbContext.Set<T>().Where(whereLambda).OrderByDescending(orderByLambda).Skip(pageSize * (pageIndex - 1)).Take(pageSize).AsQueryable();
                return temp;
            }
        }

        /// <summary>
        /// 执行sql语句
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="parm"></param>
        /// <returns></returns>
        public int ExcuteSqlParm(string sql, object[] parm = null)
        {
            if (parm != null & parm.Length > 0)
            {
                return dbContext.Database.ExecuteSqlCommand(sql, parm);
            }
            return dbContext.Database.ExecuteSqlCommand(sql);
        }

        public int DeleteBywhereLambda(Expression<Func<T, bool>> whereLambda)
        {
            try
            {
                var entitys = GetEntitys(whereLambda);
                entitys.ToList().ForEach(entity => dbContext.Entry(entity).State = EntityState.Deleted); //不加这句也可以，为什么？
                dbContext.Set<T>().RemoveRange(entitys);
                return entitys.Count();
            }
            catch (Exception)
            {

                throw;
            }
        }

        public int Count(Expression<Func<T, bool>> whereLambda)
        {
            try
            {
                int count = dbContext.Set<T>().Where(whereLambda).Count();
                return count;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
