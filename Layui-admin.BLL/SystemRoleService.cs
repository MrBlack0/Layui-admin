using Layui_admin.Model;
using Layui_admin.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Layui_admin.BLL
{
    public class SystemRoleService : BaseService<SystemRole>
    {
        public override void SetCurrentDal()
        {
            CurrentDal = this.DbSession.SystemRoleDal;
        }
        public IQueryable<SystemRoleValueViewModel> GetSysRoleValue(string roleId)
        {
            return this.DbSession.SystemRoleDal.GetSysRoleValue(roleId);
        }

        /// <summary>
        /// 添加角色(包谷角色权限 所以得加事物)
        /// </summary>
        /// <param name="role"></param>
        /// <param name="roleval"></param>
        /// <returns></returns>
        public bool AddRoleAndRoleVal(SystemRole role, List<SystemRoleValue> rolevals)
        {
            this.DbSession.SystemRoleDal.AddEntity(role);
            foreach (var rval in rolevals)
            {
                this.DbSession.SystemRoleValueDal.AddEntity(rval);
            }
            return DbSession.SaveChanges() > 0;
        }
        public bool EditRole(SystemRole role, List<SystemRoleValue> rolevals, string sql, object[] parms)
        {
            //删除减少的权限
            if (!string.IsNullOrEmpty(sql))
            {
                int n = this.DbSession.SystemRoleDal.ExcuteSqlParm(sql, parms);
            }
            //添加角色
            this.DbSession.SystemRoleDal.Update(role);
            foreach (var rval in rolevals)
            {
                this.DbSession.SystemRoleValueDal.AddEntity(rval);
            }
            return DbSession.SaveChanges() > 0;
        }
    }
}
