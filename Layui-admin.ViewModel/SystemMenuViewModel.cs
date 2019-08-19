using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Layui_admin.ViewModel
{
    public class SystemMenuViewModel
    {
        public string ID { get; set; }
        public string MenuName { get; set; }
        public string LinkUrl { get; set; }
        public string Icon { get; set; }
        public Nullable<bool> IsShow { get; set; }
        public string CreateUserName { get; set; }
        public Nullable<System.DateTime> CreateDate { get; set; }
    }
}
