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
    public class SystemRoleDal : BaseDal<SystemRole>, ISystemRoleDal
    {
        public IQueryable<SystemRoleValueViewModel> GetSysRoleValue(string roleId)
        {
            var list = from r in dbContext.Set<SystemRole>()
                       join v in dbContext.Set<SystemRoleValue>()
                       on r.ID equals v.RoleId
                       join n in dbContext.Set<SystemMenu>()
                       on v.MenuId equals n.ID
                       where n.IsShow == true & r.ID == roleId
                       select new SystemRoleValueViewModel()
                       {
                           ID = r.ID,
                           RoleName = r.RoleName,
                           RoleType = r.RoleType.Value,
                           Description = r.Description,
                           Text1 = r.Text1,
                           MenuId = v.MenuId,
                           MenuName = n.MenuName,
                           Action = v.Action
                       };
            return list;
        }
    }
}
