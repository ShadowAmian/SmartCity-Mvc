using SmartCity.Domain.Abstract;
using SmartCity.WebUI.Areas.Admin.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SmartCity.WebUI.Areas.Admin.Controllers
{
    /// <summary>
    /// 公告控制器
    /// </summary>
    public class NewsController : AdminBaseController
    {

        #region 字段 构造函数
        private INoticeInfo repository;
        public NewsController(INoticeInfo NewsInfo)
        {
            this.repository = NewsInfo;
        }
        #endregion
        #region 公告管理模块
        /// <summary>
        /// 新闻列表
        /// </summary>
        /// <returns></returns>
        public ActionResult NewsList()
        {
            var model = new NewsList();
            model.NewsIteams = repository.GetNewsList().ToList();
            return View(model);
        }
        [ActionName("NewsInfoAdd")]
        public ActionResult NewsAdd()
        {
            return View();
        }
        [HttpPost,ActionName("NewsInfoAdd")]
        public ActionResult AddNewsInfo()
        {
            return View();
        }
        #endregion
    }
}