using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Layui_admin.Models
{
    public class LoginPaylod
    {
        public string UserName { get; set; }
        public string PassWord { get; set; }
        public DateTime Expire { get; set; }
    }
}