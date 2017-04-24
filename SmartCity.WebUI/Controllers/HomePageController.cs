using SmartCity.Common;
using SmartCity.Common.log4net.Ext;
using SmartCity.Domain.Abstract;
using SmartCity.Domain.Concrete;
using SmartCity.Domain.Entities;
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
        /// <summary>
        /// 日志记录
        /// </summary>
        public IExtLog log = ExtLogManager.GetLogger("dblog");
        private INoticeInfo NewsInfoService;
        private IPostsInfo PostInfoService;
        private IUserInfo UserInfoService;
        public HomePageController(INoticeInfo NewsInfo, IPostsInfo PostInfo, IUserInfo UserInfos)
        {
            this.NewsInfoService = NewsInfo;
            this.PostInfoService = PostInfo;
            this.UserInfoService = UserInfos;
        }
        #endregion
        // GET: HomePage
        public ActionResult Index()
        {
            var model = SessionHelper.GetSession("HomeUserInfo");
            //获取通知公告
            var Model = new HomePageModel();
            var result = NewsInfoService.GetNewsList().ToList();
            Model.NewsItems = result;
            //获取论坛
            var PostsModel = PostInfoService.GetPostsInfoList().ToList();
            //获取热门帖子
            var HotPostsModel = PostInfoService.GetHotPostsInfo().ToList();
            Model.HotPostsItems = HotPostsModel;
            Model.PostsItems = PostsModel;
            Model.Title1 = "Hi, 请登录";
            Model.Tiltle2 = "我要注册";
            Model.TitleUrL1 = "#";
            Model.TitleUrl2 = "#";
            if (model!=null)
            {
              var models= model as User;
                Model.Title1 = "Hi, 欢迎你";
                Model.Tiltle2 = models.UserName;
            }
            return View(Model);
        }
        [HttpPost]
        public ActionResult UserLogin(string Account, string Password)
        {
            var json = new JsonHelper() { Msg = "登录成功!", Status = "n" };
            try
            {
                var result = UserInfoService.UserLogin(Account, Common.CryptHelper.EncryptAli(Password));
                if (result)
                {
                    var ManagerModel = UserInfoService.GetUserInfo(Account);
                    SessionHelper.SetSession("HomeUserInfo", ManagerModel.First());
                    json.Status = "y";
                    json.ReUrl = "/HomePage/Index";
                    json.UserName = ManagerModel.First().UserName;
                    log.Info(Utils.GetIP(), Account, Request.Url.ToString(), "Login", "系统登录，登录结果：" + json.Msg);
                }
                else
                {
                    json.Msg = "用户名或密码不正确!";
                    log.Info(Utils.GetIP(), Account, Request.Url.ToString(), "Login", "系统登录，登录结果：" + json.Msg);
                }
            }
            catch (Exception e)
            {
                json.Msg = e.Message;
                log.Info(Utils.GetIP(), Account, Request.Url.ToString(), "Login", "系统登录，登录结果：" + json.Msg);
            }
            return Json(json, JsonRequestBehavior.AllowGet);
        }
    }
}