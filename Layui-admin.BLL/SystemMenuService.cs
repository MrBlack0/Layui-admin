using Layui_admin.Model;
using Layui_admin.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Layui_admin.BLL
{
    public class SystemMenuService : BaseService<SystemMenu>
    {
        public override void SetCurrentDal()
        {
            CurrentDal = this.DbSession.SystemMenuDal;
        }
        public IQueryable<SystemMenuViewModel> GetSystemMenuBypage(int limit, int page, string menuName, out int count)
        {
            return this.DbSession.SystemMenuDal.GetSystemMenusByPage(limit, page, menuName, out count);
        }
    }
}
