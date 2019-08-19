using Layui_admin.IDAL.IEntityDal;
using Layui_admin.Model;
using Layui_admin.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Layui_admin.EFDAL.EntityDal
{
    public class SystemMenuDal : BaseDal<SystemMenu>, ISystemMenuDal
    {
        public IQueryable<SystemMenuViewModel> GetSystemMenu(int limit, int page, string menuName, out int count)
        {
            var data = from m in dbContext.Set<SystemMenu>()
                       join u in dbContext.Set<Admin_User>()
                       on m.CreateUserId equals u.Id
                       select new SystemMenuViewModel
                       {
                           ID = m.ID,
                           MenuName = m.MenuName,
                           LinkUrl = m.Icon,
                           Icon = m.Icon,
                           IsShow = m.IsShow,
                           CreateUserName = u.UserName,
                           CreateDate = m.CreateDate
                       };
            if (!string.IsNullOrEmpty(menuName))
            {
                data = data.Where(p => p.MenuName == menuName);
            }
            count = data.Count();
            return data.OrderByDescending(o => o.CreateDate).Skip((page - 1) * limit).Take(limit);
        }
    }
}
