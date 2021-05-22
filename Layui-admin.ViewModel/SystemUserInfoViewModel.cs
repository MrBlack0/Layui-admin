using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Layui_admin.ViewModel
{
    public class SystemUserInfoViewModel
    {
        public string Id { get; set; }
        public string NickName { get; set; }
        public string UserName { get; set; }
        public string RealName { get; set; }
        //public string PassWord { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public int Sex { get; set; }
        public string Photo { get; set; }
        //public int Age { get; set; }
        public Nullable<System.DateTime> Brithday { get; set; }
        public string Introduce { get; set; }
        public string Address { get; set; }
        public string LoginIp { get; set; }
        public string RoleID { get; set; }
        public string CreateUserID { get; set; }
        public string CreateUserName { get; set; }
        public System.DateTime CreateDate { get; set; }
        public string ModifyUserID { get; set; }
        public string ModifyUserName { get; set; }
        public Nullable<System.DateTime> UpdateDate { get; set; }
        public bool DeleteMark { get; set; }

        //viewmodel add
        public string RoleName { get; set; }
        public int? RoleType { get; set; }
    }
}
