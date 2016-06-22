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
            List<FollowData> cuFdList = FollowDB.FindAllFollowings(userId);
            List<FollowData> fdList = new List<FollowData>();
            List<User> commonFollowList = new List<User>();
            if (user == null)
            {
                ViewBag.isCurrentUser = false;
                ViewBag.hasFollow = false;
            }
            else
            {
                ViewBag.followNoticeNum = NoticeDB.FindUnReadNoticeByUserIdNType(user.id, "follow").Count + NoticeDB.FindUnReadNoticeByUserIdNType(user.id, "unfollow").Count;
                ViewBag.likeNoticeNum = NoticeDB.FindUnReadNoticeByUserIdNType(user.id, "like").Count + NoticeDB.FindUnReadNoticeByUserIdNType(user.id, "dislike").Count;
                ViewBag.commentNoticeNum = NoticeDB.FindUnReadNoticeByUserIdNType(user.id, "comment").Count;
                ViewBag.replyNoticeNum = NoticeDB.FindUnReadNoticeByUserIdNType(user.id, "reply").Count;
                if (userId == user.id)
                {
                    ViewBag.isCurrentUser = true;
                    ViewBag.hasFollow = false;
                }
                else
                {
                    fdList = FollowDB.FindAllFollowings(user.id);
                    ViewBag.hasFollow = false;
                    foreach (FollowData fd in fdList)
                    {
                        if (fd.follow.following_id == userId)
                        {
                            ViewBag.hasFollow = true;
                            break;
                        }
                    }
                }
                ViewBag.isCurrentUser = false;

                foreach (FollowData fd in fdList)
                {
                    foreach (FollowData cufd in cuFdList)
                    {
                        if (fd.follow.following_id == cufd.follow.following_id)
                        {
                            commonFollowList.Add(UserDB.FindById(cufd.follow.following_id));
                        }
                    }
                }
            }
            ViewBag.commonFollowList = commonFollowList;            
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

            List<WeikeData> wdList = WeikeDB.FindByUserId(userId);
            Dictionary<WeikeData, bool> weikeData = new Dictionary<WeikeData, bool>();
            List<FavoriteData> FavoriteWeikeList = FavoriteDB.FindFavoriteWeikeByUserId(user.id);
            bool hasFavorited = false;
            foreach(WeikeData weike in wdList)
            {
                hasFavorited = false;
                foreach (FavoriteData fw in FavoriteWeikeList)
                {
                    if (weike.weike.weike_id == fw.weike.weike_id)
                    {
                        hasFavorited = true;
                    }
                }
                weikeData.Add(weike, hasFavorited);
            }
            ViewBag.weikeData = weikeData;
            ViewBag.active = "PersonalPage/PersonalPageWeike?userId=" + userId;

            return View();
        }

        public ActionResult PersonalPageLikes(int userId)
        {
            User user = (User)Session["user"];
            List<FollowData> cuFdList = FollowDB.FindAllFollowings(userId);
            List<FollowData> fdList = new List<FollowData>();
            List<User> commonFollowList = new List<User>();
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
                fdList = FollowDB.FindAllFollowings(user.id);
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

                foreach (FollowData fd in fdList)
                {
                    foreach (FollowData cufd in cuFdList)
                    {
                        if (fd.follow.following_id == cufd.follow.following_id)
                        {
                            commonFollowList.Add(UserDB.FindById(cufd.follow.following_id));
                        }
                    }
                }
            }
            ViewBag.commonFollowList = commonFollowList;

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

            ViewBag.active = "PersonalPage/PersonalPageLikes?userId=" + userId;

            return View();
        }

        public ActionResult PersonalPageFollows(int userId)
        {
            User user = (User)Session["user"];
            List<FollowData> cuFdList = FollowDB.FindAllFollowings(userId);
            List<FollowData> fdList = new List<FollowData>();
            List<User> commonFollowList = new List<User>();
            Dictionary<FollowData, bool> followList = new Dictionary<FollowData, bool>();
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
                fdList = FollowDB.FindAllFollowings(user.id);
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

                bool hasFollow = false;
                foreach (FollowData cufd in cuFdList)
                {
                    hasFollow = false;
                    foreach (FollowData fd in fdList)
                    {
                        if (fd.follow.following_id == cufd.follow.following_id)
                        {
                            hasFollow = true;
                            commonFollowList.Add(UserDB.FindById(cufd.follow.following_id));
                        }
                    }
                    followList.Add(cufd, hasFollow);
                }
             }
            ViewBag.commonFollowList = commonFollowList;
            ViewBag.followList = followList;

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

            ViewBag.active = "PersonalPage/PersonalPageFollows?userId=" + userId;
            return View();
        }
    }
}