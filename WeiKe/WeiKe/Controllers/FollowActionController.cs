using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WeiKe.Models;

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
        public ActionResult Follow(int following_id)
        {
            // userId 指follow对象
            // todo
            if ((User)Session["user"] != null)
            {
                int user_id = ((User)Session["user"]).id;
                Follow follow = new Follow(user_id, following_id, DateTime.Now);
                FollowDB.Insert(follow);
                Notice notice = new Notice(0, user_id, following_id, 0, "follow", false,DateTime.Now);
                NoticeDB.Insert(notice);
                return Json(new { success = 1 });
            }
            else
            {
                return Json(new { success = 0 });
            }
        }

        [HttpPost]
        public ActionResult UnFollow(int following_id)
        {
            // userId 指follow对象
            // todo
            if ((User)Session["user"] != null)
            {
                int user_id = ((User)Session["user"]).id;
                FollowDB.Delete(user_id, following_id);
                Notice notice = new Notice(0, user_id, following_id, 0, "unfollow", false,DateTime.Now);
                NoticeDB.Insert(notice);
                return Json(new { success = 1 });
            }
            else
            {
                return Json(new { success = 0 });
            }
        }
    }
}