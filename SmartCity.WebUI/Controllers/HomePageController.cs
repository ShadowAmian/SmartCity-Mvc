using SmartCity.Domain.Abstract;
using SmartCity.WebUI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SmartCity.WebUI.Controllers
{
    public class HomePageController : Controller
    {
        #region 字段 构造函数
        private INoticeInfo NewsInfoService;
        private IPostsInfo PostInfoService;
        public HomePageController(INoticeInfo NewsInfo,IPostsInfo PostInfo)
        {
            this.NewsInfoService = NewsInfo;
            this.PostInfoService = PostInfo;
        }
        #endregion
        // GET: HomePage
        public ActionResult Index()
        {
            //获取通知公告
            var Model = new HomePageModel();
            var result = NewsInfoService.GetNewsList().ToList();
            Model.NewsItems = result;
            //获取论坛
            var PostsModel = PostInfoService.GetPostsInfoList().ToList();
            Model.PostsItems = PostsModel;
            return View(Model);
        }
    }
}