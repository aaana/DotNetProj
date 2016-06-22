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
                return Json(new { success = true });
            }
            else
            {
                return Json(new { success = false, message = "用户尚未登录" });
            }
        }
    }

   
}