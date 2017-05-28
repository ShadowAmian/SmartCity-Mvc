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
        public ActionResult SysLogIndex()
        {
            var model = repository.GetSysList();
            var models = new SysLogModel();
            models.SysLogIteams = model.ToList();
            return View(models);
        }
        /// <summary>
        /// 日志搜索
        /// </summary>
        /// <param name="NewsName"></param>
        /// <param name="startTime"></param>
        /// <param name="endTime"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult SerachSysLog( DateTime? startTime, DateTime? endTime)
        {
            var Model = new SysLogModel();
            var result = repository.SerachSysLog(startTime, endTime).ToList();
            Model.SysLogIteams = result;
            return View("SysLogIndex", Model);
        }
    }
}