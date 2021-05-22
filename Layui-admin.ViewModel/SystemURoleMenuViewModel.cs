using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Layui_admin.ViewModel
{
    public class SystemURoleMenuViewModel
    {
        public string ID { get; set; }
        public string MenuName { get; set; }
        public string ParentID { get; set; }
        public string LinkUrl { get; set; }
        public int? Sort { get; set; }
        public string Icon { get; set; }
        public string Action { get; set; }
    }
}
