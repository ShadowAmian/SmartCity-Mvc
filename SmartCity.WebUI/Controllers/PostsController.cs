using SmartCity.Common;
using SmartCity.Domain.Abstract;
using SmartCity.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SmartCity.WebUI.Models;

namespace SmartCity.WebUI.Controllers
{
    public class PostsController : BaseController
    {
        #region 字段 构造函数
        /// <summary>
        /// 日志记录
        /// </summary>
        private INoticeInfo NewsInfoService;
        private IPostsInfo PostInfoService;
        private IUserInfo UserInfoService;
        private IReviewInfo ReviewInfoService;
        public PostsController(INoticeInfo NewsInfo, IPostsInfo PostInfo, IUserInfo UserInfos, IReviewInfo ReviewInfo)
        {
            this.NewsInfoService = NewsInfo;
            this.PostInfoService = PostInfo;
            this.UserInfoService = UserInfos;
            this.ReviewInfoService = ReviewInfo;
        }
        #endregion

        // GET: Posts
        public ActionResult Index()
        {
            var model = SessionHelper.GetSession("HomeUserInfo");
            //获取通知公告
            var Model = new PostsModel();
            int PageCount = 0;
            //获取论坛
            var PostsModel = PostInfoService.GetPostsInfoListByPage(5, 1, out PageCount).ToList();
            //获取热门帖子
            var HotPostsModel = PostInfoService.GetHotPostsInfo().ToList();
            //获取标签
            var PostsType = PostInfoService.SerachPostsType().ToList();
            //获取最新评论
            var LatestReviews = ReviewInfoService.GetLatestReviews().ToList();

            Model.HotPostsItems = HotPostsModel;
            Model.PostsItems = PostsModel;
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