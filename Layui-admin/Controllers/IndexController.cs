using Layui_admin.BLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Layui_admin.Controllers
{
    public class IndexController : BaseController
    {
        // GET: Index
        public ActionResult Index()
        {
            //获取用户导航信息，及用户信息
            AdminUserService service = new AdminUserService();
            var navs = service.GetSysUserMenusByRole(CurrentUser.RoleID);
            ViewBag.UserInfo = CurrentUser;
            return View(navs);
        }
    }
}