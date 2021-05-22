using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Layui_admin.ViewModel
{
    public class SystemRoleValueViewModel
    {
        public string ID { get; set; }
        public string RoleName { get; set; }
        /// <summary>
        /// 角色类型(1-超管；2-普通角色)
        /// </summary>
        public int RoleType { get; set; }
        public string Description { get; set; }
        public string MenuId { get; set; }
        public string MenuName { get; set; }
        public string Action { get; set; }
        public string Text1 { get; set; }
    }
}
