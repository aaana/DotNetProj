using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Web;
using System.Web.Mvc;
using WeiKe.Models;

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
        public ActionResult PublishPicWithUrl(string title,  string picUrl, string picSize, string subject, string des, string redirectPage)
        {
           ViewBag.title = title;
           ViewBag.src = picUrl;
            ViewBag.picSize = picSize;
           ViewBag.subject = subject;
           ViewBag.des = des;

            return RedirectToAction(redirectPage);
        }

        // 使用上传图片
        [HttpPost]
        public ActionResult PublishPicWithUpload(string title, string uploadPicSize, string subject, string des, string redirectPage)
        {
            string filename;
            DateTime newDate = DateTime.Now;
            User user = (User)Session["user"];

            int weike_id = -1;
            if (Request.Files["picUpload"].HasFile())
            {
                string mimetype = Request.Files["picUpload"].ContentType;
                string path = AppDomain.CurrentDomain.BaseDirectory + "uploads/";
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }
                filename = Path.GetFileName(Request.Files["picUpload"].FileName);
                Request.Files["picUpload"].SaveAs(Path.Combine(path, filename));

                filename = "../uploads/" + filename;
                Weike newWeike = new Weike(title, subject, user.id, filename, uploadPicSize, des, 0, newDate, 0);
                weike_id = WeikeDB.Insert(newWeike);
            }

            if (Request.Files["fileUpload"].HasFile() && weike_id != -1)
            {
                string mimetype = Request.Files["fileUpload"].ContentType;
                string path = AppDomain.CurrentDomain.BaseDirectory + "uploads/";
                filename = Path.GetFileName(Request.Files["fileUpload"].FileName);
                Request.Files["fileUpload"].SaveAs(Path.Combine(path, filename));

                path = "../uploads/" + filename;
                MyFileDB.Insert(new MyFile(0, filename, mimetype, path, weike_id));
            }

            return RedirectToAction(redirectPage);
        }

        // 使用上传文件
        [HttpPost]
        public ActionResult PublishFileWithUpload(string title, string subject, string des, string redirectPage)
        {
            string str = title + " " + subject + " " + des + " " + redirectPage;
            string filename;
            DateTime newDate = DateTime.Now;
            User user = (User)Session["user"];
            foreach (string upload in Request.Files)
            {
                if (!Request.Files[upload].HasFile()) continue;
                string mimetype = Request.Files[upload].ContentType;
                string path = AppDomain.CurrentDomain.BaseDirectory + "uploads/";
                filename = Path.GetFileName(Request.Files[upload].FileName);
                Request.Files[upload].SaveAs(Path.Combine(path, filename));

                //MyFileDB.Insert(new MyFile(0, filename, mimetype, path));
            }

            return RedirectToAction(redirectPage);
        }


        public FilePathResult GetFileFromDisk(string fileName, string mimeType)
        {
            string path = AppDomain.CurrentDomain.BaseDirectory + "uploads/";
            return File(path + fileName, mimeType, fileName);
        }
    }
}