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
            /*
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
            }*/
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

            //List<WeikeModel> list = new List<WeikeModel>();
            //list.Add(new WeikeModel { title = "Senseless Suffering", subject = "English", author = "Jeremy Bentham", src = "../resource/img/8.jpg", size = "1280x853", description = "The question is not, 'Can they reason ?' nor, 'Can they talk ? ' but rather, 'Can they suffer ? '", star = 0 });
            //list.Add(new WeikeModel { title = "Rabbit Intelligence", subject = "English", author = "Robert Brault", src = "../resource/img/9.jpg", size = "865x1280", description = "If a rabbit defined intelligence the way man does, then the most intelligent animal would be a rabbit, followed by the animal most willing to obey the commands of a rabbit.", star = 44 });
            //list.Add(new WeikeModel { title = "Friendly Terms", subject = "English", author = "Samuel Butler", src = "../resource/img/10.jpg", size = "1280x1280", description = "Man is the only animal that can remain on friendly terms with the victims he intends to eat until he eats them. ", star = 44 });
            //list.Add(new WeikeModel { title = "Murder of Men", subject = "English", author = "Leonardo Da Vinci", src = "../resource/img/11.jpg", size = "1280x1280", description = "The time will come when men such as I will look upon the murder of animals as they now look upon the murder of men. ", star = 44 });
            //list.Add(new WeikeModel { title = "Highest Ethics", subject = "Math", author = "Thomas Edison", src = "../resource/img/1.jpg", size = "1280x853", description = "Non-violence leads to the highest ethics, which is the goal of all evolution. Until we stop harming all other living beings, we are still savages ", star = 44 });
            ViewBag.weikeData = wdList;

            return View();
        }

        public ActionResult PersonalPageLikes(string userId)
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

            //List<WeikeModel> list = new List<WeikeModel>();
            //list.Add(new WeikeModel { title = "Senseless Suffering", subject = "English", author = "Jeremy Bentham", src = "../resource/img/8.jpg", size = "1280x853", description = "The question is not, 'Can they reason ?' nor, 'Can they talk ? ' but rather, 'Can they suffer ? '", star = 0 });
            //list.Add(new WeikeModel { title = "Rabbit Intelligence", subject = "English", author = "Robert Brault", src = "../resource/img/9.jpg", size = "865x1280", description = "If a rabbit defined intelligence the way man does, then the most intelligent animal would be a rabbit, followed by the animal most willing to obey the commands of a rabbit.", star = 44 });
            //list.Add(new WeikeModel { title = "Friendly Terms", subject = "English", author = "Samuel Butler", src = "../resource/img/10.jpg", size = "1280x1280", description = "Man is the only animal that can remain on friendly terms with the victims he intends to eat until he eats them. ", star = 44 });
            //list.Add(new WeikeModel { title = "Murder of Men", subject = "English", author = "Leonardo Da Vinci", src = "../resource/img/11.jpg", size = "1280x1280", description = "The time will come when men such as I will look upon the murder of animals as they now look upon the murder of men. ", star = 44 });
            //list.Add(new WeikeModel { title = "Highest Ethics", subject = "Math", author = "Thomas Edison", src = "../resource/img/1.jpg", size = "1280x853", description = "Non-violence leads to the highest ethics, which is the goal of all evolution. Until we stop harming all other living beings, we are still savages ", star = 44 });
            //ViewBag.weikeData = list;

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

            return View();
        }
    }
}