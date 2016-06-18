using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WeiKe.Controllers
{
    public class LikeActionController : Controller
    {
        // GET: LikeAction
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Like(string weikeId)
        {
            // todo

           
            return Json(weikeId);
        }

        [HttpPost]
        public ActionResult Dislike(string weikeId)
        {
            // todo

            return Json(weikeId);
        }
    }

   
}