using Layui_admin.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Layui_admin.Models
{
    public class RoleAndRloeValues
    {
        public string Id { get; set; }
        public string RoleName { get; set; }
        public string Description { get; set; }
        public List<TreeDataViewModel> TreeData { get; set; }
    }
}