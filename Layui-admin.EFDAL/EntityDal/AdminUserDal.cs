using Layui_admin.IDAL;
using Layui_admin.IDAL.IEntityDal;
using Layui_admin.Model;
using Layui_admin.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Layui_admin.EFDAL.EntityDal
{
    public class AdminUserDal : BaseDal<Admin_User>, IAdminUserDal
    {
        public IQueryable<SystemUserInfoViewModel> GetSysUsers(string username, string email, int pageIndex, int pageSize, out int count)
        {
            var list = from u in dbContext.Set<Admin_User>()
                       join r in dbContext.Set<SystemRole>()
                       on u.RoleID equals r.ID
                       select new SystemUserInfoViewModel()
                       {
                           Id = u.Id,
                           UserName = u.UserName,
                           RealName = u.RealName,
                           NickName = u.NickName,
                           Email = u.Email,
                           Phone = u.Phone,
                           Sex = u.Sex,
                           Photo = u.Photo,
                           Introduce = u.Introduce,
                           Address = u.Address,
                           CreateUserName = u.CreateUserName,
                           CreateDate = u.CreateDate,
                           DeleteMark = u.DeleteMark,
                           RoleID = u.RoleID,
                           RoleName = r.RoleName,
                           RoleType = r.RoleType

                       };
            if (!string.IsNullOrEmpty(username))
            {
                list = list.Where(u => u.UserName == username);
            }
            if (!string.IsNullOrEmpty(email))
            {
                list = list.Where(u => u.Email == email);
            }
            count = list.Count();
            return list.OrderByDescending(o => o.CreateDate).Skip((pageIndex - 1) * pageSize).Take(pageSize);
        }
       
    }
}
