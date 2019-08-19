using Layui_admin.IDAL.IEntityDal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Layui_admin.IDAL
{
    public interface IDbSession
    {
        //IAdDal AdDal { get; }
        //INewsDal NewsDal { get; }
        IAdminUserDal AdminUserDal { get; }
        IProductDal ProductDal { get; }
        ISystemMenuDal SystemMenuDal { get; }
        int SaveChanges();
    }
}
