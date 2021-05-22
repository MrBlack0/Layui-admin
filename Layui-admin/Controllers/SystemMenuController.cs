using Layui_admin.BLL;
using Layui_admin.Common;
using Layui_admin.Model;
using Layui_admin.Models;
using Layui_admin.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using System.Web.Mvc;

namespace Layui_admin.Controllers
{
    public class SystemMenuController : BaseController
    {
        // GET: SystemMenu
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult List()
        {
            return View("SysMenuList");
        }
        [HttpGet]
        public ActionResult SysMenuList(string menuname, int page = 1, int limit = 10)
        {
            var result = ResModelFactory.ResDefaultData<SystemMenuViewModel>();
            SystemMenuService service = new SystemMenuService();
            int count = 0;

            var list = service.GetSystemMenuBypage(limit, page, menuname, out count);
            result.count = count;
            result.data = list.ToList().ToArray();
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public ActionResult GetAllMenu()
        {
            var result = ResModelFactory.ResDefaultData<SystemMenu>();
            SystemMenuService service = new SystemMenuService();
            var list = service.GetEntitys(p => true);
            result.data = list.ToArray();
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public ActionResult GetMenuById(string id)
        {
            SystemMenuService service = new SystemMenuService();
            var menu = service.GetEntitys(p => p.ID == id).FirstOrDefault();
            return View("AddMenu", menu);
        }
        [HttpPost]
        public ActionResult EditMenu(SystemMenu entity)
        {
            var result = ResModelFactory.ResDefault();
            try
            {
                SystemMenuService service = new SystemMenuService();
                SystemMenu model = service.GetEntitys(p => p.ID == entity.ID).FirstOrDefault();
                model.MenuName = entity.MenuName;
                model.LinkUrl = entity.LinkUrl;
                model.ParentID = entity.ParentID;
                model.Icon = entity.Icon;
                model.IsShow = entity.IsShow;

                model.ModifyUserId = CurrentUser.Id;
                model.NodifyDate = DateTime.Now;
                var user = service.Update(model);
            }
            catch (Exception ex)
            {
                result = ResModelFactory.ResError(ex.Message);
            }
            return Json(result);
        }
        public ActionResult AddMenu()
        {
            return View(new SystemMenu());
        }
        [HttpPost]
        public ActionResult AddMenu(SystemMenu entity)
        {
            var result = ResModelFactory.ResDefault();
            try
            {
                entity.ID = Guid.NewGuid().ToString();
                entity.CreateDate = DateTime.Now;
                entity.CreateUserId = CurrentUser.Id;
                entity.IsShow = true;
                SystemMenuService service = new SystemMenuService();
                service.Add(entity);
            }
            catch (Exception ex)
            {
                result = ResModelFactory.ResError(ex.Message);
            }
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult DeleteMenus(string[] ids)
        {
            if (ids.Length <= 0)
            {
                return Json(new { code = 999, msg = "参数有误" });
            }
            SystemMenuService service = new SystemMenuService();
            object[] obj = new object[ids.Length];
            string parms = SqlParameterHelper.GetParameters(ids, ref obj);
            string sql = $"delete from SystemMenu where id in({parms})";
            int n = service.ExcuteSqlParm(sql, obj);
            return Json(new { code = 0, msg = "success" });
        }
    }
}