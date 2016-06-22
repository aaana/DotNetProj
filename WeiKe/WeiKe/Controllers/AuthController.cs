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
            int userId = UserDB.Insert(0, signupEmail, signupName, signupPassword,"暂无简介","暂无标签");
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
    }
}