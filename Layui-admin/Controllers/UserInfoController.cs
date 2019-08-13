using Layui_admin.BLL;
using Layui_admin.Common;
using Layui_admin.jwt;
using Layui_admin.Model;
using Layui_admin.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;

namespace Layui_admin.Controllers
{
    public class UserInfoController : BaseController
    {
        [AllowAnonymous]
        [HttpGet]
        public ActionResult Login()
        {
            var cookie = HttpContext.Request.Cookies["authorize"];
            if (cookie != null || HttpContext.Request.IsAuthenticated)
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
        [HttpPost]
        [AllowAnonymous]
        public ActionResult LoginOut()
        {
            var res = ResModelFactory.ResDefault();
            var cookie = HttpContext.Response.Cookies["authorize"];
            if (cookie != null)
            {
                //删除cookie
                cookie.Expires = DateTime.Now.AddSeconds(-1);
            }
            return Json(res);
        }
        public ActionResult LayuiDemo()
        {
            return View();
        }
        public ActionResult List()
        {
            return View("UserList");
        }
        [HttpGet]
        public ActionResult GetUserList(string username, string email, int page = 1, int limit = 10)
        {
            int n = 0;
            int res = 9 / n;
            var result = ResModelFactory.ResDefaultData<Admin_User>();
            AdminUserService service = new AdminUserService();
            int count = 0;
            Expression<Func<Admin_User, bool>> where = p => true;
            if (!string.IsNullOrEmpty(username) && !string.IsNullOrEmpty(email))
            {
                where = p => p.UserName == username & p.Email == email;
            }
            else if (!string.IsNullOrEmpty(username) && string.IsNullOrEmpty(email))
            {
                where = p => p.UserName == username;
            }
            else if (string.IsNullOrEmpty(username) && !string.IsNullOrEmpty(email))
            {
                where = p => p.Email == email;
            }
            var list = service.GetPages(limit, page, out count, where, o => o.CreateDate, false);
            result.count = count;
            result.data = list.ToArray();
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult AddUser()
        {
            return View(new Admin_User());
        }
        [HttpGet]
        public ActionResult EditUser(string id)
        {
            AdminUserService service = new AdminUserService();
            var user = service.GetEntitys(p => p.Id == id).FirstOrDefault();
            return View("AddUser", user);
        }
        [HttpPost]
        public ActionResult EditUser(Admin_User entity)
        {
            var result = ResModelFactory.ResDefault();
            try
            {
                AdminUserService service = new AdminUserService();
                Admin_User model = service.GetEntitys(p => p.Id == entity.Id).FirstOrDefault();
                model.UserName = entity.UserName;
                model.NickName = entity.NickName;
                model.Phone = entity.Phone;
                model.Email = entity.Email;
                model.Brithday = entity.Brithday;
                model.Sex = entity.Sex;
                model.Introduce = entity.Introduce;

                model.ModifyUserID = CurrentUser.Id;
                model.ModifyUserName = CurrentUser.UserName;
                model.UpdateDate = DateTime.Now;
                var user = service.Update(model);
            }
            catch (Exception ex)
            {
                result = ResModelFactory.ResError(ex.Message);
            }
            return Json(result);
        }
        [HttpPost]
        public ActionResult AddUser(Admin_User entity)
        {
            var result = ResModelFactory.ResDefault();
            try
            {
                entity.Id = Guid.NewGuid().ToString();
                entity.CreateDate = DateTime.Now;
                entity.CreateUserID = CurrentUser.Id;
                entity.CreateUserName = CurrentUser.UserName;
                entity.DeleteMark = false;
                entity.Sex = 1;
                entity.Age = 23;
                AdminUserService service = new AdminUserService();
                service.Add(entity);

            }
            catch (Exception ex)
            {
                result = ResModelFactory.ResError(ex.Message);
            }
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public ActionResult UploadAvator(string userid)
        {
            if (string.IsNullOrEmpty(userid))
            {
                return Json(new { code = 0, msg = "参数有误" });
            }
            var files = HttpContext.Request.Files;
            HttpPostedFileBase file = files[0];
            string path = string.Empty;
            string url = string.Empty;
            do
            {
                url = "Content/images/" + DateTime.Now.ToString("yyMMddHHmmssfff") + new Random(unchecked((int)DateTime.Now.Ticks)).Next(1, 100) + "." + file.FileName.Split('.')[1];
                path = Server.MapPath("~/") + url;
            } while (System.IO.File.Exists(path));
            file.SaveAs(path);
            AdminUserService service = new AdminUserService();
            Admin_User entity = service.GetEntitys(p => p.Id == userid).FirstOrDefault();
            entity.Photo = "/" + url;
            service.Update(entity);
            return Json(new
            {
                code = 0,
                msg = "success",
                data = new { src = "/" + url }
            }, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult DelteUserByid(string userid)
        {
            if (string.IsNullOrEmpty(userid))
            {
                return Json(new { code = 999, msg = "参数有误" });
            }
            AdminUserService service = new AdminUserService();
            Admin_User entity = service.GetEntitys(p => p.Id == userid).FirstOrDefault();
            if (entity == null)
            {
                return Json(new { code = 999, msg = "为获取到用户信息" });
            }
            entity.DeleteMark = true;//删除
            service.Update(entity);
            return Json(new
            {
                code = 0,
                msg = "success"
            });
        }
        [HttpPost]
        public ActionResult DeleteUsers(string[] ids)
        {
            if (ids.Length <= 0)
            {
                return Json(new { code = 999, msg = "参数有误" });
            }
            AdminUserService service = new AdminUserService();
            object[] obj = new object[ids.Length];
            string parms = SqlParameterHelper.GetParameters(ids, ref obj);
            string sql = $"update admin_user set deletemark=1 where id in({parms})";

            int n = service.ExcuteSqlParm(sql, obj);
            return Json(new { code = 0, msg = "success" });
        }
    }
}
