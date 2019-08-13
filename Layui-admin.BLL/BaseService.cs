using Layui_admin.Factory;
using Layui_admin.IDAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Layui_admin.BLL
{
    public abstract class BaseService<T> where T : class, new()
    {
        public IBaseDal<T> CurrentDal { get; set; }
        public IDbSession DbSession { get { return DbSessionFactory.GetCurrentDbSession(); } }
        public abstract void SetCurrentDal();
        public BaseService()
        {
            SetCurrentDal();
        }
        public T Add(T entity)
        {
            try
            {
                CurrentDal.AddEntity(entity);
                DbSession.SaveChanges();
                return entity;
            }
            catch (Exception ex)
            {
                throw;
            }

        }
        public bool Update(T entity)
        {
            CurrentDal.Update(entity);
            return DbSession.SaveChanges() > 0;
        }
        public bool Delete(T entity)
        {
            CurrentDal.Delete(entity);
            return DbSession.SaveChanges() > 0;
        }
        public IQueryable<T> GetEntitys(Expression<Func<T, bool>> whereLambda)
        {
            return CurrentDal.GetEntitys(whereLambda);
        }
        public IQueryable<T> GetPages<S>(int pageSize, int pageIndex, out int count, Expression<Func<T, bool>> whereLambda, Expression<Func<T, S>> orderByLambda, bool IsAsc)
        {
            return CurrentDal.GetPages<S>(pageSize, pageIndex, out count, whereLambda, orderByLambda, IsAsc);
        }

        public int DeleteBywhereLambda(Expression<Func<T, bool>> whereLambda)
        {
            try
            {
                CurrentDal.DeleteBywhereLambda(whereLambda);
                return DbSession.SaveChanges();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public int ExcuteSqlParm(string sql, object[] parm = null)
        {
            return CurrentDal.ExcuteSqlParm(sql, parm);
        }
        public int Count(Expression<Func<T, bool>> whereLambda)
        {
            return CurrentDal.Count(whereLambda);
        }
    }
}
