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
        public ActionResult makeComment(int commentTargetId, string content,int weikeId)
        {
            // todo
            if((User)Session["user"] != null){
                int user_id = ((User)Session["user"]).id;
                string username = ((User)Session["user"]).name;
                if (commentTargetId != 0)
                {
                    Comment parent = CommentDB.FindCommentById(commentTargetId);
                    int parent_userId = parent.user_id;
                    if(user_id == parent_userId)
                    {
                        return Json(new { success = -1 });
                    }
                    Notice notice = new Notice(0, user_id,parent_userId,weikeId,"reply",false);
                    NoticeDB.Insert(notice);
                }
                else
                {
                    int userId = WeikeDB.FindByWeikeId(weikeId).weike.user_id;
                    Notice notice = new Notice(0, user_id, userId, weikeId, "comment", false);
                    NoticeDB.Insert(notice);
                }
                DateTime now = DateTime.Now;
                Comment comment = new Comment(0,user_id, weikeId,now,content,commentTargetId);
                int comment_id = CommentDB.Insert(comment);
                return Json(new { success = 1, commentId = comment_id, username = username, time = now});
            }
            else
            {
                return Json(new { success = 0 });
            }

            // id of the new comment
           
        }
    }
}