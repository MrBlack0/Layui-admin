using Layui_admin.BLL;
using Layui_admin.jwt;
using Layui_admin.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Layui_admin.Controllers
{
    public class BaseController : Controller
    {
        public static Admin_User CurrentUser
        {
            get
            {
                Admin_User user = null;
                var httpContent = System.Web.HttpContext.Current;
                var cookie = httpContent.Request.Cookies["authorize"];
                if (cookie == null)
                {
                    httpContent.Response.StatusCode = (int)System.Net.HttpStatusCode.Forbidden;
                    httpContent.Response.Redirect("~/UserInfo/Login", false);

                }
                string name = JwtHelper.GetJwtDecode(cookie.Value).UserName;
                user = new AdminUserService().GetEntitys(p => p.UserName == name).FirstOrDefault();
                return user;
            }
        }
    }
}