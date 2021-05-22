using Layui_admin.Model;
using Layui_admin.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Layui_admin.BLL
{
    public class AdminUserService : BaseService<Admin_User>
    {
        public override void SetCurrentDal()
        {
            CurrentDal = this.DbSession.AdminUserDal;
        }

        public IQueryable<SystemUserInfoViewModel> GetSysUsers(string username, string email, int pageIndex, int pageSize, out int count)
        {
            return this.DbSession.AdminUserDal.GetSysUsers(username, email, pageIndex, pageSize, out count);
        }
        public List<SystemUserMenuViewModel> GetSysUserMenusByRole(string roleid)
        {
            List<SystemUserMenuViewModel> result = new List<SystemUserMenuViewModel>();
            var role = this.DbSession.SystemRoleDal.GetEntitys(p => p.ID == roleid).FirstOrDefault();
            List<SystemURoleMenuViewModel> menus = new List<SystemURoleMenuViewModel>();
            if (role.RoleType == 1)//超级管理
            {
                menus = this.DbSession.SystemMenuDal.GetSystemAllMenus().ToList();//所有导航信息
            }
            else
            {
                menus = this.DbSession.SystemMenuDal.GetSystemMenusByRoleId(roleid).ToList();
            }

            var parents = menus.Where(p => (string.IsNullOrEmpty(p.ParentID)));
            foreach (var par in parents.OrderBy(s => s.Sort))
            {
                SystemUserMenuViewModel m = new SystemUserMenuViewModel();
                m.ID = par.ID;
                m.MenuName = par.MenuName;
                m.LinkUrl = par.LinkUrl;
                m.Sort = par.Sort.Value;
                m.Action = "View,Show,Add,Modify,Delete";
                m.Child = ConstUserMenu(menus, par.ID);
                result.Add(m);
            }
            return result;
        }

        private SystemUserMenuViewModel[] ConstUserMenu(List<SystemURoleMenuViewModel> menus, string mid)
        {
            List<SystemUserMenuViewModel> result = new List<SystemUserMenuViewModel>();
            var parents = menus.Where(p => p.ParentID == mid);
            foreach (var par in parents.ToList().OrderBy(s => s.Sort))
            {
                SystemUserMenuViewModel m = new SystemUserMenuViewModel();
                m.ID = par.ID;
                m.MenuName = par.MenuName;
                m.LinkUrl = par.LinkUrl;
                m.Sort = par.Sort.Value;
                m.Action = "View,Show,Add,Modify,Delete";
                m.Child = ConstUserMenu(menus, par.ID);
                result.Add(m);
            }
            return result.ToArray();
        }
    }
}
