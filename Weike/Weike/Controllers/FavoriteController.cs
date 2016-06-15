using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WeiKe.Models;

namespace WeiKe.Controllers
{
    public class FavoriteController : Controller
    {
        [HttpPost]
        public ActionResult Favorite(int user_id, int following_id)
        {
            Favorite favorite = new Favorite(user_id, following_id, DateTime.Now);
            FavoriteDB.Insert(favorite);
            return Json(true);
        }

        [HttpPost]
        public ActionResult CancelFavorite(int user_id, int following_id)
        {
            FavoriteDB.Delete(user_id, following_id);
            return Json(true);
        }

        [HttpPost]
        public ActionResult MyFavorites(int user_id)
        {
            List<FavoriteData> favorites = FavoriteDB.FindFavoriteWeikeByUserId(user_id);
            return Json(favorites);
        }

        [HttpPost]
        public ActionResult MyFollower(int user_id)
        {
            List<FollowData> followers = FollowDB.FindAllFollowers(user_id);
            return Json(followers);
        }
    }
}