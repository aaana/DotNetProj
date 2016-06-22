using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WeiKe.Models;

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
        public ActionResult Like(int weikeId)
        {
            // todo
            if ((User)Session["user"] != null)
            {
                int user_id = ((User)Session["user"]).id;
                Favorite favorite = new Favorite(user_id, weikeId, DateTime.Now);
                FavoriteDB.Insert(favorite);
                WeikeData wd =WeikeDB.FindByWeikeId(weikeId);
                Notice notice = new Notice(0, user_id, wd.weike.user_id, weikeId, "like", false,DateTime.Now);
                NoticeDB.Insert(notice);
                return Json(new { success=true });
            }
            else
            {
                return Json(new { success = false, message = "用户尚未登录" });
            }
                       
        }

        [HttpPost]
        public ActionResult Dislike(int weikeId)
        {
            // todo

            if ((User)Session["user"] != null)
            {
                int user_id = ((User)Session["user"]).id;
                FavoriteDB.Delete(user_id, weikeId);
                WeikeData wd = WeikeDB.FindByWeikeId(weikeId);
                Notice notice = new Notice(0, user_id, wd.weike.user_id, weikeId, "dislike", false,DateTime.Now);
                NoticeDB.Insert(notice);
                return Json(new { success = true });
            }
            else
            {
                return Json(new { success = false, message = "用户尚未登录" });
            }
        }
    }

   
}