using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Layui_admin.IDAL
{
    public interface IBaseDal<T> where T : class, new()
    {
        T AddEntity(T entity);
        bool Update(T entity);
        bool Delete(T entity);
        IQueryable<T> GetEntitys(Expression<Func<T, bool>> whereLambda);
        IQueryable<T> GetPages<S>(int pageSize, int pageIndex, out int count, Expression<Func<T, bool>> whereLambda, Expression<Func<T, S>> orderByLambda, bool IsAsc);
        int ExcuteSqlParm(string sql, object[] parm = null);
        int DeleteBywhereLambda(Expression<Func<T, bool>> whereLambda);
        int Count(Expression<Func<T, bool>> whereLambda);
    }
}
