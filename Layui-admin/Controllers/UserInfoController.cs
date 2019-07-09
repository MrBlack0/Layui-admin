using Layui_admin.BLL;
using Layui_admin.jwt;
using Layui_admin.Model;
using Layui_admin.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using System.Net.Http;
using System.Web.Mvc;

namespace Layui_admin.Controllers
{
    public class UserInfoController : Controller
    {
        [AllowAnonymous]
        [HttpGet]
        public ActionResult Login()
        {
            var cookie = HttpContext.Request.Cookies["authorize"];
            if (cookie != null)
            {
                return RedirectToAction("Index", "Index");
            }
            return View();
        }
        [HttpPost]
        [AllowAnonymous]
        public ActionResult Login(string userName, string password, string imageCode)
        {
            // 验证码
            var result = ResModelFactory.ResDefault();
            string code = Session["ImageCode"].ToString();
            if (!code.Equals(imageCode))
            {
                //验证码错误
                result.code = "999";
                result.msg = "验证码错误";
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            AdminUserService service = new AdminUserService();
            var user = service.GetEntitys(p => p.UserName == userName.Trim() && p.PassWord == password).FirstOrDefault();
            if (user == null)
            {
                result.code = "999";
                result.msg = "用户名或密码错误";
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            string token = JwtHelper.SetJwtEncode(userName, password);
            result.token = token;
            //服务端存储
            //客户端存储cookie
            System.Web.HttpCookie cookie = new System.Web.HttpCookie("authorize", token);
            cookie.Expires = DateTime.Now.AddDays(1);
            this.HttpContext.Response.Cookies.Add(cookie);
            //客户端加入header
            this.HttpContext.Response.Headers.Add("Authorization", token);
            return Json(result, JsonRequestBehavior.AllowGet);

        }
        public ActionResult LayuiDemo()
        {
            return View();
        }
        public ActionResult List()
        {
            return View();
        }
        [HttpGet]
        public ActionResult GetUserList()
        {
            var result = ResModelFactory.ResDefaultData<Admin_User>();
            AdminUserService service = new AdminUserService();
            var list = service.GetEntitys(p => 1 == 1).ToList();
            result.count = list.Count;
            result.data = list.ToArray();
            return Json(result, JsonRequestBehavior.AllowGet);
        }
    }
}
