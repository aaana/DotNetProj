using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WeiKe.Controllers
{
    public class FollowActionController : Controller
    {
        // GET: FollowAction
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult follow(string userId)
        {
            // userId 指follow对象
            // todo


            return Json(userId);
        }

        [HttpPost]
        public ActionResult unfollow(string userId)
        {
            // userId 指follow对象
            // todo

            return Json(userId);
        }
    }
}