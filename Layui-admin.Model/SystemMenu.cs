//------------------------------------------------------------------------------
// <auto-generated>
//     此代码已从模板生成。
//
//     手动更改此文件可能导致应用程序出现意外的行为。
//     如果重新生成代码，将覆盖对此文件的手动更改。
// </auto-generated>
//------------------------------------------------------------------------------

namespace Layui_admin.Model
{
    using System;
    using System.Collections.Generic;
    
    public partial class SystemMenu
    {
        public string ID { get; set; }
        public string MenuName { get; set; }
        public string ParentID { get; set; }
        public string LinkUrl { get; set; }
        public string Icon { get; set; }
        public Nullable<bool> IsShow { get; set; }
        public string CreateUserId { get; set; }
        public Nullable<System.DateTime> CreateDate { get; set; }
        public string ModifyUserId { get; set; }
        public Nullable<System.DateTime> NodifyDate { get; set; }
        public Nullable<int> Sort { get; set; }
    }
}
