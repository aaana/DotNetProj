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
        public ActionResult Index()
        {
            return View();
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