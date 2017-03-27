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
    public class NewsController : Controller
    {
        // GET: Admin/News
        public ActionResult Index()
        {
            return View();
        }
    }
}