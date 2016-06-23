using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WeiKe.Models;

namespace WeiKe.Controllers
{
    public class AuthController : Controller
    {
        // GET: Auth
        public ActionResult Index(int type, string redirectPage, int status=1)
        {
            ViewBag.authType = type;
            ViewBag.status = status;
            ViewBag.redirectPage = redirectPage;
            return View();
        }

        [HttpPost]
        public ActionResult Login(string loginEmail, string loginPassword, string redirectPage)
        {
            // login with email and password

            User user = UserDB.FindByEmail(loginEmail);
            if (user == null)
            {
                //未注册
                //ViewBag.loginSuccess = -1;
                return RedirectToAction("Index", "Auth", new { type = 1, status=-1});
            }
            else
            {
                if (loginPassword.Equals(user.password))
                {
                    Session["user"] = user;
                    //ViewBag.loginSuccess = 1;
                    // ViewBag.user = user;
                    ViewBag.followNoticeNum = NoticeDB.FindUnReadNoticeByUserIdNType(user.id, "follow").Count + NoticeDB.FindUnReadNoticeByUserIdNType(user.id, "unfollow").Count;
                    ViewBag.likeNoticeNum = NoticeDB.FindUnReadNoticeByUserIdNType(user.id, "like").Count + NoticeDB.FindUnReadNoticeByUserIdNType(user.id, "dislike").Count;
                    ViewBag.commentNoticeNum = NoticeDB.FindUnReadNoticeByUserIdNType(user.id, "comment").Count;
                    ViewBag.replyNoticeNum = NoticeDB.FindUnReadNoticeByUserIdNType(user.id, "reply").Count;
                    return RedirectToAction("Index", redirectPage);

                }
                else
                {
                    //密码错误
                    ViewBag.loginSuccess = 0;
                    return RedirectToAction("Index", "Auth", new { type = 0, status = 0 });
                }
            }
            
        }

        [HttpPost]
        public ActionResult Signup(string signupEmail, string signupName, string signupPassword, string verifyPassword, string redirectPage)
        {
            // sign up
            int userId = UserDB.Insert(0, signupEmail, signupName, signupPassword,"暂无简介","暂无标签", "../resource/img/portrait.jpg");
            if (userId >= 1)
            {
                ViewBag.userId = userId;
                return RedirectToAction("Index", redirectPage);
            }
            else
            {
                return View();
            }
            
        }


       
        public ActionResult Logout()
        {
            // sign up
            Session["user"] = null;
            return RedirectToAction("Index","Home");
        }
    }
}