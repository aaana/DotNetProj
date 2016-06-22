using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WeiKe.Models;

namespace WeiKe.Controllers
{
    public class CommentController : Controller
    {
        // GET: Comment
        [HttpPost]
        public ActionResult NestedComments(int weike_id)
        {
            List<NestedComment> ncList = NestedComment.getAllCommentsByWeikeId(weike_id);
            return Json(new{ success = true, ncList});
        }

        [HttpPost]
        public ActionResult MyComments(int user_id)
        {
            List<CommentData> cdList = CommentDB.FindCommentDataByUserId(user_id);
            return Json(new { success = true, cdList });

        }

        [HttpPost]
        public ActionResult CommentDataByTime(int user_id,DateTime date)
        {
            List<CommentData> cdList = CommentDB.FindCommentDataByTime(user_id,date);
            return Json(new { success = true, cdList });

        }

        [HttpPost]
        public ActionResult Comment(int user_id, int weike_id, string content, int parent)
        {
            Comment comment = new Comment(0, user_id, weike_id, DateTime.Now, content);
            CommentDB.Insert(comment);
            return Json(true);
        }
    }
}