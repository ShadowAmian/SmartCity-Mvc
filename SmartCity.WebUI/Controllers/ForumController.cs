using SmartCity.Common;
using SmartCity.Common.log4net.Ext;
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
    public class ForumController : Controller
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
        private IReplyInfo ReplyInfoService;
        public ForumController(INoticeInfo NewsInfo, IPostsInfo PostInfo, IUserInfo UserInfos, IReviewInfo ReviewInfo, IReplyInfo ReplyInfo)
        {
            this.NewsInfoService = NewsInfo;
            this.PostInfoService = PostInfo;
            this.UserInfoService = UserInfos;
            this.ReviewInfoService = ReviewInfo;
            this.ReplyInfoService = ReplyInfo;
        }
        #endregion

        // GET: Forum
        public ActionResult FourumIndex(int ID)
        {
            var model = SessionHelper.GetSession("HomeUserInfo");
            //获取通知公告
            var Model = new FourumIndexModel();
            //获取论坛
            var PostsModel = PostInfoService.GetPostsInfoByID(ID).ToList();
            //获取热门帖子
            var HotPostsModel = PostInfoService.GetHotPostsInfo().ToList();
            //获取标签
            var PostsType = PostInfoService.SerachPostsType().ToList();
            //获取最新评论
            var LatestReviews = ReviewInfoService.GetLatestReviews().ToList();
            //显示当前评论
            var CurrentReviews = ReviewInfoService.GetLatestReviewsByID(ID).ToList();
            var CurrentModel = new CurrentReview();
            var CurrentList = new List<CurrentReview>();
            foreach (var item in CurrentReviews)
            {
                CurrentModel.ReviewID = item.ReviewID;
                CurrentModel.ReviewContent = item.ReviewContent;
                CurrentModel.CreateTime = item.CreateTime;
                CurrentModel.PostsModel = item.PostsModel;
                CurrentModel.UserModel = item.UserModel;
                List<Reply> ReplyList = ReplyInfoService.GetLatestReviews(item.ReviewID).ToList();
                CurrentModel.ReplyModel = null;
                if (ReplyList != null)
                {
                    CurrentModel.ReplyModel = ReplyList;
                }
                CurrentList.Add(CurrentModel);
            }
            Model.CurrentReviewItems = CurrentList;
            Model.HotPostsItems = HotPostsModel;
            Model.PostsItems = PostsModel;
            Model.PostsTypeItems = PostsType;
            Model.LatestReviewsItems = LatestReviews;
            Model.Title1 = "Hi, 请登录";
            Model.Tiltle2 = "我要注册";
            Model.TitleUrL1 = "#";
            Model.TitleUrl2 = "#";
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