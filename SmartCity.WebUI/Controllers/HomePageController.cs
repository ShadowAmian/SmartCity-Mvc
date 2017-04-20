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
        public HomePageController(INoticeInfo NewsInfo)
        {
            this.NewsInfoService = NewsInfo;
        }
        #endregion
        // GET: HomePage
        public ActionResult Index()
        {
            //获取通知公告
            var Model = new HomePageModel();
            var result = NewsInfoService.GetNewsList().ToList();
            Model.NewsItems = result;
            return View(Model);
        }
    }
}