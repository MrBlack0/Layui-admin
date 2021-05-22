using Layui_admin.Model;
using Layui_admin.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Layui_admin.IDAL.IEntityDal
{
    public interface ISystemMenuDal : IBaseDal<SystemMenu>
    {
        IQueryable<SystemMenuViewModel> GetSystemMenusByPage(int limit, int page, string menuName, out int count);
        IQueryable<SystemURoleMenuViewModel> GetSystemMenusByRoleId(string roleid);
        IQueryable<SystemURoleMenuViewModel> GetSystemAllMenus();
    }
}
