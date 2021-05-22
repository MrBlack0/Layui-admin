using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Http.Filters;
using System.Web.Mvc;

namespace Layui_admin.Views
{
    public class TestResultController : Controller
    {

        //过滤器 

        /// <summary>
        /// 1、返回一个ViewResult对象
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// 2、返回一个json格式的数据
        /// </summary>
        /// <returns></returns>
        public ActionResult Json()
        {
            var book = new { BookId = 1, BookName = "MVC框架" };
            return Json(book, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 3、返回JavaScript
        /// </summary>
        /// <returns></returns>
        public ActionResult JavaScript()
        {
            string js = "<script>alert('Welcome to ASP.NET MVC')</script>";
            return JavaScript(js);
        }

        /// <summary>
        /// 4、返回FilePath
        /// </summary>
        /// <returns></returns>
        public ActionResult FilePath()
        {
            //return File("~/Content/校长 - 带你去旅行.mp3", "audio/mp3");
            return new FilePathResult("~/Content/校长 - 带你去旅行.mp3", "audio/mp3");
        }

        /// <summary>
        /// 5、返回FileContent
        /// </summary>
        /// <returns></returns>
        public ActionResult FileContent()
        {
            string content = "Welcome To ASP.NET MVC";
            byte[] contents = System.Text.Encoding.UTF8.GetBytes(content);
            return File(contents, "text/plain");
        }

        /// <summary>
        /// 6、返回FileStream
        /// </summary>
        /// <returns></returns>
        public ActionResult FileStream()
        {
            string content = "Welcome To ASP.NET MVC";
            byte[] contents = System.Text.Encoding.UTF8.GetBytes(content);
            FileStream fs = new FileStream(Server.MapPath("~/Content/2 开发环境下载安装说明.doc"), FileMode.Open);
            return File(fs, "application/msword");
        }

        /// <summary>
        /// 7、返回 ContentResult
        /// </summary>
        /// <returns></returns>
        public ActionResult ContentResult()
        {
            string content = "<h1>Welcome To ASP.NET MVC</h1>";
            return Content(content);
        }
    }
}