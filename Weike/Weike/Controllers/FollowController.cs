using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WeiKe.Models;

namespace WeiKe.Controllers
{
    public class FollowController : Controller
    {
        [HttpPost]
        public ActionResult Follow(int user_id,int following_id)
        {
            Follow follow = new Follow(user_id, following_id, DateTime.Now);
            FollowDB.Insert(follow);
            return Json(true);
        }

        [HttpPost]
        public ActionResult CancelFollow(int user_id, int following_id)
        {
            FollowDB.Delete(user_id,following_id);
            return Json(true);
        }

        [HttpPost]
        public ActionResult MyFollowings(int user_id)
        {
            List<FollowData> followings = FollowDB.FindAllFollowings(user_id);
            return Json(followings);
        }

        [HttpPost]
        public ActionResult MyFollower(int user_id)
        {
            List<FollowData> followers = FollowDB.FindAllFollowers(user_id);
            return Json(followers);
        }
    }
}