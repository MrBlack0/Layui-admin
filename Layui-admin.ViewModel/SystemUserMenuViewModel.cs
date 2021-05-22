using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Layui_admin.ViewModel
{
    public class SystemUserMenuViewModel
    {
        public string ID { get; set; }
        public string MenuName { get; set; }
        public string LinkUrl { get; set; }
        public int Sort { get; set; }
        public string Action { get; set; }
        public SystemUserMenuViewModel[] Child { get; set; }
    }
}
