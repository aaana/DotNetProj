using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WeiKe.Models;

namespace WeiKe.Controllers
{
    public class MessageController : Controller
    {
        // GET: Message
        public ActionResult Index(int type)
        {
            ViewBag.type = type.ToString();
            ViewBag.active = "Message/Index?type=" + type.ToString();
            if (Session["user"] != null)
            {
                User user = (User)Session["user"];
                ViewBag.user = user;
                
                ViewBag.followNotice = NoticeDB.FindAllNoticeByUserIdNType(user.id, "follow");
                ViewBag.likeNotice = NoticeDB.FindAllNoticeByUserIdNType(user.id, "like");
                ViewBag.commentNotice = NoticeDB.FindAllNoticeByUserIdNType(user.id, "comment");
                ViewBag.replyNotice = NoticeDB.FindAllNoticeByUserIdNType(user.id, "reply");
            }

            return View();
        }

        [HttpPost]
        public ActionResult ReadNotice(int notice_id)
        {
            NoticeDB.UpdateIsread(notice_id, true);
            return Json(new { success = 1 });
        }

        /*
        [HttpPost]
        public ActionResult message(int userId)
        {
            List<NoticeData> ndList = NoticeDB.FindUnreadNoticeByUserId(userId);
            List<NoticeData> followNDList = new List<NoticeData>();
            List<NoticeData> commentNDList = new List<NoticeData>();
            List<NoticeData> likeNDList = new List<NoticeData>();
            List<NoticeData> replyNDList = new List<NoticeData>();
            int followNoticeNum = 0;
            int commentNoticeNum = 0;
            int likeNoticeNum = 0;
            int replyNoticeNum = 0;

            foreach(NoticeData nd in ndList)
            {
                string type = nd.notice.type;
                if (type.Equals("like"))
                {
                    likeNoticeNum++;
                    followNDList.Add(nd);
                }
                else if (type.Equals("comment"))
                {
                    commentNoticeNum++;
                    commentNDList.Add(nd);
                }
                else if (type.Equals("reply"))
                {
                    replyNoticeNum++;
                    replyNDList.Add(nd);
                }
                else if (type.Equals("follow")) {
                    followNoticeNum++;
                    followNDList.Add(nd);
                }
            }

            return Json(new { likeNDList = likeNDList, commentNDList = commentNDList, replyNDList = replyNDList, followNDList = followNDList });
        }
        */
    }
}