using System;
using System.Collections.Generic;
using WeiKe.Models;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WeiKe.Controllers
{
    public class PersonalPageController : Controller
    {
        // GET: PersonalPage
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult PersonalPageWeike(int userId)
        {
            User user = (User)Session["user"];
            if(user == null)
            {
                ViewBag.isCurrentUser = false;
                ViewBag.hasFollow = false;
            }
            else if (userId == user.id)
            {
                ViewBag.isCurrentUser = true;
                ViewBag.hasFollow = false;
            }
            else
            {
                List<FollowData> fdList = FollowDB.FindAllFollowings(user.id);
                ViewBag.hasFollow = false;
                foreach(FollowData fd in fdList)
                {
                    if (fd.follow.following_id == userId)
                    {
                        ViewBag.hasFollow = true;
                        break;
                    }
                }
                ViewBag.isCurrentUser = false;
            }
            User infoUser = UserDB.FindById(userId);
            Dictionary<string, string> personalInfo = new Dictionary<string, string>()
            {
                { "id", userId+""},
                { "name", infoUser.name},
                { "portraitSrc", "../resource/img/portrait.jpg"},
                { "email",infoUser.email},
                { "des", infoUser.des},
                { "tag", infoUser.tag},
                { "followCount",  infoUser.followNum+""},
                { "likeCount", infoUser.favoriteNum+""},
                { "weikeCount", infoUser.postNum+""}
            };
            ViewBag.personalInfo = personalInfo;
            Dictionary<string, string> follow3 = new Dictionary<string, string>()
            {
                {"id", "100"},
                {"name", "用户名"},
                {"imgSrc", "../resource/img/portrait.jpg"},
                {"email", "*********@qq.com"},
                {"hasFollow", "ture"},
                {"isCurrentUser", "false" }
            };
            Dictionary<string, string>[] commonFollowList = { follow3, follow3, follow3, follow3, follow3 };
            ViewBag.commonFollowList = commonFollowList;

            List<WeikeData> wdList = WeikeDB.FindByUserId(userId);

           ViewBag.weikeData = wdList;
           ViewBag.active = "PersonalPage/PersonalPageWeike?userId=" + userId;

            return View();
        }

        public ActionResult PersonalPageLikes(int userId)
        {
            User user = (User)Session["user"];
            
            if (user == null)
            {
                ViewBag.isCurrentUser = false;
                ViewBag.hasFollow = false;
            }
            else if (userId == user.id)
            {
                ViewBag.isCurrentUser = true;
                ViewBag.hasFollow = false;
            }
            else
            {
                List<FollowData> fdList = FollowDB.FindAllFollowings(user.id);
                List<FollowData> myFdList = FollowDB.FindAllFollowings(userId);
                
                ViewBag.hasFollow = false;
                foreach (FollowData fd in fdList)
                {
                    if (fd.follow.following_id == userId)
                    {
                        ViewBag.hasFollow = true;
                        break;
                    }
                }
                ViewBag.isCurrentUser = false;
            }

            User infoUser = UserDB.FindById(userId);
            Dictionary<string, string> personalInfo = new Dictionary<string, string>()
            {
                { "id", userId+""},
                { "name", infoUser.name},
                { "portraitSrc", "../resource/img/portrait.jpg"},
                { "email",infoUser.email},
                { "des", infoUser.des},
                { "tag", infoUser.tag},
                { "followCount",  infoUser.followNum+""},
                { "likeCount", infoUser.favoriteNum+""},
                { "weikeCount", infoUser.postNum+""}
            };
            ViewBag.personalInfo = personalInfo;

            List<FavoriteData> favoriteList = FavoriteDB.FindFavoriteWeikeByUserId(userId);
            List<WeikeData> favoriteWeikeList = new List<WeikeData>();
            foreach (FavoriteData fdata in favoriteList)
            {
                favoriteWeikeList.Add(WeikeDB.FindByWeikeId(fdata.weike.weike_id));
            }
            ViewBag.weikeData = favoriteList;


            Dictionary<string, string> follow3 = new Dictionary<string, string>()
            {
                {"id", "100"},
                {"name", "用户名"},
                {"imgSrc", "../resource/img/portrait.jpg"},
                {"email", "*********@qq.com"},
                {"hasFollow", "ture"},
                {"isCurrentUser", "false" }
            };
            Dictionary<string, string>[] commonFollowList = { follow3, follow3, follow3, follow3, follow3 };
            ViewBag.commonFollowList = commonFollowList;
            ViewBag.active = "PersonalPage/PersonalPageLikes?userId=" + userId;

            return View();
        }

        public ActionResult PersonalPageFollows(string userId)
        {
            if (userId == null || userId == "102")
            {
                ViewBag.isCurrentUser = true;
                ViewBag.hasFollow = false;
            }
            else if (userId == "100")
            {
                ViewBag.isCurrentUser = false;
                ViewBag.hasFollow = true;
            }
            else
            {
                ViewBag.isCurrentUser = false;
                ViewBag.hasFollow = false;
            }

            Dictionary<string, string> personalInfo = new Dictionary<string, string>()
            {
                { "id", userId},
                { "name", userId+ViewBag.isCurrentUser},
                { "portraitSrc", "../resource/img/portrait.jpg"},
                { "email", "*********@qq.com"},
                { "des", "我是一个开朗乐观、积极向上的学生"},
                { "tag", "标签1 标签2"},
                { "followCount",  "5"},
                { "likeCount", "12"},
                { "weikeCount", "10"}
            };
            ViewBag.personalInfo = personalInfo;
            Dictionary<string, string> follow1 = new Dictionary<string, string>()
            {
                {"id", "101"},
                {"name", "用户名101"},
                {"imgSrc", "../resource/img/portrait.jpg"},
                {"email", "*********@qq.com false"},
                {"hasFollow", "false"},
                {"isCurrentUser", "false" }
            };
            Dictionary<string, string> follow2 = new Dictionary<string, string>()
            {
                {"id", "102"},
                {"name", "用户名102"},
                {"imgSrc", "../resource/img/portrait.jpg"},
                {"email", "*********@qq.com"},
                {"hasFollow", "false"},
                {"isCurrentUser", "true" }
            };
            Dictionary<string, string> follow3 = new Dictionary<string, string>()
            {
                {"id", "100"},
                {"name", "用户名100"},
                {"imgSrc", "../resource/img/portrait.jpg"},
                {"email", "*********@qq.com true"},
                {"hasFollow", "true"},
                {"isCurrentUser", "false" }
            };
            Dictionary<string, string>[] commonFollowList = {follow3, follow3, follow3, follow3, follow3};
            ViewBag.commonFollowList = commonFollowList;
            Dictionary<string, string>[] followList = {follow1, follow2, follow3, follow1, follow1, follow1, follow3, follow3, follow1 };
            ViewBag.followList = followList;
            ViewBag.active = "PersonalPage/PersonalPageFollows?userId=" + userId;

            return View();
        }
    }
}