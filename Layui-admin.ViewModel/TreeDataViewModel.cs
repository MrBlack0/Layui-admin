using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Layui_admin.ViewModel
{
    public class TreeDataViewModel
    {
        public string id { get; set; }
        public string title { get; set; }
        public TreeDataViewModel[] children { get; set; }
        public bool spread { get; set; }
        public bool @checked { get; set; }
        public bool disabled { get; set; }
    }
}
