using Layui_admin.common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Layui_admin.Filters
{
    public class MyExceptionAttribute : HandleErrorAttribute
    {
        public override void OnException(ExceptionContext filterContext)
        {
            if (!filterContext.ExceptionHandled)
            {
                string message = string.Format("消息类型：{0}\r\n消息内容：{1}\r\n引发异常的方法：{2}\r\n引发异常源：{3}"
                    , filterContext.Exception.GetType().Name
                    , filterContext.Exception.Message
                     , filterContext.Exception.TargetSite
                     , filterContext.Exception.Source + filterContext.Exception.StackTrace
                     );

                //记录日志
                LogHelper.WriteError(message);
                //转向
                filterContext.ExceptionHandled = true;
                filterContext.Result = new RedirectResult("/Common/Error");
            }
            base.OnException(filterContext);
        }
    }
}