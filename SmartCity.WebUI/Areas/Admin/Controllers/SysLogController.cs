using SmartCity.Domain.Abstract;
using SmartCity.WebUI.Areas.Admin.Models.SysLogs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SmartCity.WebUI.Areas.Admin.Controllers
{
    public class SysLogController : Controller
    {
        #region 字段 构造函数
        private ISysLogInfo repository;
        public SysLogController(ISysLogInfo SysLogInfo)
        {
            this.repository = SysLogInfo;
        }
        #endregion
        // GET: Admin/SysLog
        public ActionResult Index()
        {
            var model = repository.GetSysList();
            var models = new SysLogModel();
            models.SysLogIteams = model.ToList();
            return View(models);
        }
    }
}