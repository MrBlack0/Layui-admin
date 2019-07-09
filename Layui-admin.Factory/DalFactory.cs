using Layui_admin.EFDAL.EntityDal;
using Layui_admin.IDAL.IEntityDal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Layui_admin.Factory
{
    public class DalFactory
    {
        public static IAdminUserDal AdminUserDalFactory()
        {
            return new AdminUserDal();
        }
        public static IProductDal ProductDalFactory()
        {
            return new ProductDal();
        }
    }
}
