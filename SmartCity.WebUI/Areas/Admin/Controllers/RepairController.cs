using SmartCity.Domain.Abstract;
using SmartCity.WebUI.Areas.Admin.Models.Repairs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SmartCity.WebUI.Areas.Admin.Controllers
{
    public class RepairController : Controller
    {

        #region 字段 构造函数
        private IRepairInfo repository;
        public RepairController(IRepairInfo RepairInfo)
        {
            this.repository = RepairInfo;
        }
        #endregion


        // GET: Admin/Repair
        public ActionResult RepairList()
        {
            var model = repository.GetRepairInfoList();
            var models = new RepairListModel();
            models.RepairIteams = model.ToList();
            return View(models);
        }
        /// <summary>
        /// 修改维修状态
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ActionResult EditRepairStatus(int RepairID,int Status)
        {
            //if (CurrentUser.ManagerType != "超级管理员")
            //{
            //    //普通管理员无操作权限
            //    return Json(new { IsSuccess = 1, Message = "无权限添加该信息！" });
            //}
            var result = repository.EditRepairStatu(RepairID, Status);
            if (result)
            {
                //log.Info(Utils.GetIP(), CurrentUser.ManagerAccount, Request.Url.ToString(), "Repair", "通知公告状态修改添加);
                return Json(new { IsSuccess = 0, Message = "修改成功！" });

            }
            return Json(new { IsSuccess = 1, Message = "修改失败，请稍后重试!" });
        }
        /// <summary>
        /// 维修任务分配
        /// </summary>
        /// <returns></returns>
        public ActionResult MaintenanceDistribution(int repairid)
        {
            return View();
        }

    }
}