using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WeiKe.Models;

namespace WeiKe.Controllers
{
    public static class Helper
    {
        public static bool HasFile(this HttpPostedFileBase file)
        {
            return (file != null && file.ContentLength > 0) ? true : false;
        }
    }
    public class MyFileController : Controller
    {
        // GET: File
        public ActionResult Index()
        {
            return View();
        }
        
        public ActionResult Upload()
        {
            foreach (string upload in Request.Files)
            {
                if (!Request.Files[upload].HasFile()) continue;
                string mimetype = Request.Files[upload].ContentType;
                string path = AppDomain.CurrentDomain.BaseDirectory + "uploads/";
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }
                string filename = Path.GetFileName(Request.Files[upload].FileName);
                Request.Files[upload].SaveAs(Path.Combine(path, filename));
                MyFileDB.Insert(new MyFile(0, filename, mimetype,filename,1));
            }
            return View();
        }

        //public FilePathResult GetFileFromDisk()
        //{
           // string path = AppDomain.CurrentDomain.BaseDirectory + "uploads/";
           // string fileName = "捕获1.PNG";
           // return File(path + fileName, "text/plain", "捕获1.PNG");
       // }

        public FilePathResult GetFileFromDisk(string fileName,string mimetype)
        {
            string path = AppDomain.CurrentDomain.BaseDirectory + "uploads/";
            return File(path + fileName, mimetype, fileName);
        }
    }


    }
