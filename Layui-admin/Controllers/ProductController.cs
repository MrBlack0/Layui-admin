using Layui_admin.BLL;
using Layui_admin.Model;
using Layui_admin.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Layui_admin.Controllers
{
    public class ProductController : Controller
    {
        // GET: Product
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Add()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Add(Product entity)//FormCollection
        {
            var result = ResModelFactory.ResDefault();
            try
            {
                entity.CreateTime = DateTime.Now;
                ProductService service = new ProductService();
                service.Add(entity);
                
            }
            catch (Exception ex)
            {
                result = ResModelFactory.ResError(ex.Message);
            }
            return Json(result,JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult UploadImg(HttpPostedFileBase fileData)
        {
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
            return Json(new
            {
                code = 0,
                msg = "success",
                data = new { src = "/" + url }
            }, JsonRequestBehavior.AllowGet);
        }

    }
}