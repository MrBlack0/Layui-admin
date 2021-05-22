using Layui_admin.Model;
using Layui_admin.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Layui_admin.IDAL.IEntityDal
{
    public interface IAdminUserDal : IBaseDal<Admin_User>
    {
        IQueryable<SystemUserInfoViewModel> GetSysUsers(string username, string email, int pageIndex, int pageSize, out int count);
    }
}
