using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WeiKe.Models;

namespace WeiKe.Controllers
{
    public class SearchController : Controller
    {
        [HttpPost]
        public ActionResult SearchByAuthor(string author)
        {
            List<WeikeData> wdList = WeikeDB.FindByAuthor(author);
            return Json(new { success = true, wdList });
        }

        [HttpPost]
        public ActionResult SearchByTitle(string title)
        {
            List<WeikeData> wdList = WeikeDB.FindByTitle(title);
            return Json(new { success = true, wdList });
        }

        [HttpPost]
        public ActionResult SearchBySubject(string subject)
        {
            List<WeikeData> wdList = WeikeDB.FindBySubject(subject);
            return Json(new { success = true, wdList });
        }
    }
}