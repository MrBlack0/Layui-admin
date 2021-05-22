using Layui_admin.EFDAL;
using Layui_admin.IDAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Layui_admin.IDAL.IEntityDal;

namespace Layui_admin.Factory
{
    public class DbSession : IDbSession
    {
        public IAdminUserDal AdminUserDal => DalFactory.AdminUserDalFactory();
        public IProductDal ProductDal => DalFactory.ProductDalFactory();

        public ISystemMenuDal SystemMenuDal => DalFactory.SystemMenuDalFactory();
        public ISystemRoleDal SystemRoleDal => DalFactory.SystemRoleDalFactory();
        public ISystemRoleValueDal SystemRoleValueDal => DalFactory.SystemRoleValueDalFactory();
        #region 抽象工厂
        //public IAdDal AdDal { get { return DalFactory.AdDalFactory(); } }
        //public INewsDal NewsDal { get { return DalFactory.NewsDalFactory(); } }
        //public IAdminUserDal AdminUserDal { get { return DalFactory.AdminUserDalFactory(); } }

        #endregion

        /// <summary>
        /// 拿到当前上下文，并执行SaveChanges()方法 修改整体一起提交
        /// </summary>
        /// <returns></returns>
        public int SaveChanges()
        {
            return DbContextFactory.GetDbcontext().SaveChanges();
        }
    }
}
