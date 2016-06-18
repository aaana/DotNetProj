using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

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
        public ActionResult getCommentList(string weikeId)
        {
            // get comment list

            string commentList = "abc";
            return Json(commentList);
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