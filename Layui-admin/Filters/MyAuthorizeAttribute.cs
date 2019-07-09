using Layui_admin.jwt;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Layui_admin.Filters
{
    public class MyAuthorizeAttribute : AuthorizeAttribute
    {
        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            //死否匿名请求
            var attr = filterContext.ActionDescriptor.GetCustomAttributes(true).OfType<AllowAnonymousAttribute>();
            bool isAnonymous = attr.Any(a => a is AllowAnonymousAttribute);
            if (isAnonymous)
            {
                base.OnAuthorization(filterContext);
                return;
            }
            // 是否是Ajax请求
            var bAjax = filterContext.HttpContext.Request.IsAjaxRequest();
            if (bAjax)
            {
                base.OnAuthorization(filterContext);
                return;
            }
            var context = filterContext.HttpContext;
            var authHeader = filterContext.HttpContext.Request.Cookies["authorize"];
            if (authHeader == null)
            {
                context.Response.StatusCode = (int)System.Net.HttpStatusCode.Forbidden;
                filterContext.HttpContext.Response.Redirect("~/UserInfo/Login", false);
                base.OnAuthorization(filterContext);
                return;
            }
            var userinfo = JwtHelper.GetJwtDecode(authHeader.Value);
            //验证用户操作是否在权限列表中  
            //var actionName = filterContext.ActionDescriptor.ActionName;
            //var controllerName = filterContext.ActionDescriptor.ControllerDescriptor.ControllerName;
            if (userinfo == null)
            {
                filterContext.HttpContext.Response.Redirect("~/UserInfo/Login", false);
            }

            base.OnAuthorization(filterContext);
            //return;
        }
        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            return true;
        }
    }
}