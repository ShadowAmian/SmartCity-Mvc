using SmartCity.Common;
using SmartCity.Domain.Abstract;
using SmartCity.Domain.Entities;
using SmartCity.WebUI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SmartCity.WebUI.Controllers
{
    public class HomeNewsController : Controller
    {
        #region 字段 构造函数
        /// <summary>
        /// 日志记录
        /// </summary>
        private INoticeInfo NewsInfoService;
        private IPostsInfo PostInfoService;
        private IUserInfo UserInfoService;
        private IReviewInfo ReviewInfoService;
        public HomeNewsController(INoticeInfo NewsInfo, IPostsInfo PostInfo, IUserInfo UserInfos, IReviewInfo ReviewInfo)
        {
            this.NewsInfoService = NewsInfo;
            this.PostInfoService = PostInfo;
            this.UserInfoService = UserInfos;
            this.ReviewInfoService = ReviewInfo;
        }
        #endregion
        // GET: HomeNews
        public ActionResult HomeNewsList()
        {
            var model = SessionHelper.GetSession("HomeUserInfo");
            //获取通知公告
            var Model = new HomeNewsModel();
            var result = NewsInfoService.GetNewsListByPublished().ToList();
            Model.NewsItems = result;
            int PageCount = 0;
            //获取热门帖子
            var HotPostsModel = PostInfoService.GetHotPostsInfo().ToList();
            //获取标签
            var PostsType = PostInfoService.SerachPostsType().ToList();
            //获取最新评论
            var LatestReviews = ReviewInfoService.GetLatestReviews().ToList();
            Model.HotPostsItems = HotPostsModel;
            Model.PostsTypeItems = PostsType;
            Model.LatestReviewsItems = LatestReviews;
            Model.Title1 = "Hi, 请登录";
            Model.Tiltle2 = "我要注册";
            Model.TitleUrL1 = "#";
            Model.TitleUrl2 = "#";
            Model.PageCount = PageCount;
            if (model != null)
            {
                var models = model as User;
                Model.Title1 = "Hi, 欢迎你";
                Model.Tiltle2 = models.UserName;
            }
            return View(Model);
        }
    }
}