using Layui_admin.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Layui_admin.App_Start
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
            // 全局身份验证过滤器 更方便 
            filters.Add(new MyAuthorizeAttribute());
            //全局注册exception
            filters.Add(new MyExceptionAttribute());
        }
    }
}