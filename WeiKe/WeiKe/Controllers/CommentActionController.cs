using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WeiKe.Models;

namespace WeiKe.Controllers
{
    public class CommentActionController : Controller
    {
        // GET: CommentAction
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult getCommentList(int weikeId)
        {
            // get comment list
            List<NestedComment> data = NestedComment.getAllCommentsByWeikeId(weikeId);
            ViewBag.data = data;
            return Json(new { comments = data});
        }
         
        [HttpPost]
        public ActionResult makeComment(string commentTargetId, string content)
        {
            // todo

            // id of the new comment
            int commentId = int.Parse(commentTargetId) + 1;
            return Json(commentId);
        }
    }
}