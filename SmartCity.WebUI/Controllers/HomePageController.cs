using SmartCity.Common;
using SmartCity.Common.log4net.Ext;
using SmartCity.Domain.Abstract;
using SmartCity.Domain.Concrete;
using SmartCity.Domain.Entities;
using SmartCity.WebUI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace SmartCity.WebUI.Controllers
{
    public class HomePageController : BaseController
    {
        #region 字段 构造函数
        /// <summary>
        /// 日志记录
        /// </summary>
        public IExtLog log = ExtLogManager.GetLogger("dblog");
        private INoticeInfo NewsInfoService;
        private IPostsInfo PostInfoService;
        private IUserInfo UserInfoService;
        private IReviewInfo ReviewInfoService;
        public HomePageController(INoticeInfo NewsInfo, IPostsInfo PostInfo, IUserInfo UserInfos, IReviewInfo ReviewInfo)
        {
            this.NewsInfoService = NewsInfo;
            this.PostInfoService = PostInfo;
            this.UserInfoService = UserInfos;
            this.ReviewInfoService = ReviewInfo;
        }
        #endregion
        // GET: HomePage
        public ActionResult Index()
        {
            var model = SessionHelper.GetSession("HomeUserInfo");
            //获取通知公告
            var Model = new HomePageModel();
            var result = NewsInfoService.GetNewsListByPublished().ToList();
            Model.NewsItems = result;
            int PageCount = 0;
            //获取论坛
            var PostsModel = PostInfoService.GetPostsInfoListByPage(PageCounts, 1, out PageCount).ToList();
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
        public ActionResult GetPostInfoByPage(int Curr)
        {
            int PageCount = 0;
            var model = PostInfoService.GetPostsInfoListByPage(PageCounts, Curr, out PageCount).ToList();
            return Json(new { IsSuccess = 0, Items = model });
        }
    }
}