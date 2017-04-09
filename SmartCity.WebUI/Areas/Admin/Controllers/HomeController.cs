using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SmartCity.WebUI.Areas.Admin.Controllers
{
    public class HomeController : AdminBaseController
    {
        // GET: Admin/Home
        public ActionResult Index()
        {
            if (CurrentUser!=null)
            {
                ViewBag.UserName = CurrentUser.ManagerName;
            }
            return View();
        }
       
    }
}