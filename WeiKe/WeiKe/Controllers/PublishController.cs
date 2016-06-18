using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WeiKe.Controllers
{
    public class PublishController : Controller
    {
        // GET: Publish
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Publish(string from)
        {
            ViewBag.from = from;
            return View();
        }

        // 使用图片链接
        [HttpPost]
        public ActionResult Publish(string title,  string src, string subject, string des, string redirectPage)
        {
            // ViewBag.title = title;
            // ViewBag.src = src;
            // ViewBag.subject = subject;
            // ViewBag.des = des;
            // return View();

            return RedirectToAction(redirectPage);
        }

        // 使用上传图片
        [HttpPost]
        public ActionResult PublishWithUpload(string title, string src, string subject, string des)
        {
            

            return View();
        }
    }
}