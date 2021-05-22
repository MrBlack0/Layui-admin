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
        public IQueryable<SystemMenuViewModel> GetSystemMenusByPage(int limit, int page, string menuName, out int count)
        {
            var list = from m in dbContext.Set<SystemMenu>()
                       join u in dbContext.Set<Admin_User>()
                       on m.CreateUserId equals u.Id
                       select new SystemMenuViewModel
                       {
                           ID = m.ID,
                           MenuName = m.MenuName,
                           LinkUrl = m.LinkUrl,
                           Icon = m.Icon,
                           IsShow = m.IsShow,
                           CreateUserName = u.UserName,
                           CreateDate = m.CreateDate
                       };
            if (!string.IsNullOrEmpty(menuName))
            {
                list = list.Where(p => p.MenuName == menuName);
            }
            count = list.Count();
            return list.OrderByDescending(o => o.CreateDate).Skip((page - 1) * limit).Take(limit);
        }
        public IQueryable<SystemURoleMenuViewModel> GetSystemAllMenus()
        {
            var list = from m in dbContext.Set<SystemMenu>()
                       where m.IsShow == true
                       select new SystemURoleMenuViewModel
                       {
                           ID = m.ID,
                           ParentID = m.ParentID,
                           MenuName = m.MenuName,
                           LinkUrl = m.LinkUrl,
                           Sort = m.Sort,
                           Icon = m.Icon,
                           Action = "View,Show,Add,Modify,Delete"
                       };
            return list;
        }
        public IQueryable<SystemURoleMenuViewModel> GetSystemMenusByRoleId(string roleid)
        {
            var list = from rv in dbContext.Set<SystemRoleValue>()
                       join m in dbContext.Set<SystemMenu>()
                       on rv.MenuId equals m.ID
                       where rv.RoleId == roleid && m.IsShow == true
                       select new SystemURoleMenuViewModel
                       {
                           ID = m.ID,
                           ParentID = m.ParentID,
                           MenuName = m.MenuName,
                           LinkUrl = m.LinkUrl,
                           Sort = m.Sort,
                           Icon = m.Icon,
                           Action = rv.Action
                       };
            return list;
        }
    }
}
