using Layui_admin.Model;
using Layui_admin.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Layui_admin.IDAL.IEntityDal
{
    public interface ISystemRoleDal : IBaseDal<SystemRole>
    {
        IQueryable<SystemRoleValueViewModel> GetSysRoleValue(string roleId);
    }
}
