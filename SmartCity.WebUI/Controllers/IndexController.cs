using SmartCity.Domain.Abstract;
using SmartCity.Domain.Concrete;
using SmartCity.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SmartCity.Common.log4net.Ext;
using SmartCity.Common;

namespace SmartCity.WebUI.Controllers
{
    public class IndexController : Controller
    {
        private IUserInfo repository;
        /// <summary>
        /// 日志记录
        /// </summary>
        IExtLog log = ExtLogManager.GetLogger("dblog");

        public IndexController(IUserInfo UserInfoRepository)
        {
            this.repository = UserInfoRepository;
        }
        // GET: Home
        public ActionResult Index()
        {
            //log.Info(Utils.GetIP(), "847712857", Request.Url.ToString(), "Index", "插入成功");

            //var model = new Manager {ManagerName="只道寻常", ManagerAccount = "847712856", ManagerPassword = "017a33fe139de1fcefa8a35cc36d41a2", ManagerPhone = "13027782989", ManagerEmail = "847712857@qq.com", ManagerType = "管理员", IsEnable = 1, CreateTime = DateTime.Now };
            //var result = repository.Add(model);
            return View("Index","Manager",new { area="Admin"});
        }
    }
}