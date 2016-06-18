using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WeiKe.Controllers
{
    public class AuthController : Controller
    {
        // GET: Auth
        public ActionResult Index(int type)
        {
            ViewBag.authType = type;
            return View();
        }

        [HttpPost]
        public ActionResult Login(string loginEmail, string loginPassword, string redirectPage)
        {
            // login with email and password


            return RedirectToAction("Index", redirectPage);

            // show value
            //ViewBag.to = redirectPage;
            //ViewBag.email = loginEmail;
            //ViewBag.pw = loginPassword;
            //return View();
        }

        [HttpPost]
        public ActionResult Signup(string signupEmail, string name, string password, string verifyPassword, string redirectPage)
        {
            // sign up
            return RedirectToAction("Index", redirectPage);

            // show value
            //ViewBag.to = redirectPage;
            //ViewBag.email = signupEmail;
            //ViewBag.pw = password;
            //return View();
        }
    }
}