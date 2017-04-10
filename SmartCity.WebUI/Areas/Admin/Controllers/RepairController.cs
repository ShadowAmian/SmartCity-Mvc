using SmartCity.Domain.Abstract;
using SmartCity.Domain.Entities;
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
        private IManagerInfo Manager;
        public RepairController(IRepairInfo RepairInfo, IManagerInfo Manager)
        {
            this.repository = RepairInfo;
            this.Manager = Manager;
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
        [HttpGet]
        public ActionResult MaintenanceDistribution(int RepairID)
        {
            var model=Manager.SearchMaintenance();
            var models = new RepairMaintenanceModel();
            models.Iteams = model.ToList();
            models.RepairID = RepairID;
            return View(models);
        }
        [HttpPost]
        public ActionResult MaintenanceDistributionByManager(int RepairID, int ManagerID,DateTime time)
        {
            //if (CurrentUser.ManagerType != "超级管理员")
            //{
            //    //普通管理员无操作权限
            //    return Json(new { IsSuccess = 1, Message = "无权限添加该信息！" });
            //}
            var result = repository.EditRepairToManager(ManagerID,RepairID,time);
            if (result)
            {
                //log.Info(Utils.GetIP(), CurrentUser.ManagerAccount, Request.Url.ToString(), "Repair", "通知公告状态修改添加);
                return Json(new { IsSuccess = 0, Message = "修改成功！" });

            }
            return Json(new { IsSuccess = 1, Message = "修改失败，请稍后重试!" });
        }
        /// <summary>
        /// 查询报修信息
        /// </summary>
        /// <param name="RepairName"></param>
        /// <param name="startTime"></param>
        /// <param name="endTime"></param>
        /// <returns></returns>
        public ActionResult SerachRepairByRepairName(string RepairName, DateTime? startTime, DateTime? endTime)
        {
            //if (CurrentUser.ManagerType != "超级管理员")
            //{
            //    //普通管理员无操作权限
            //    return Json(new { IsSuccess = 1, Message = "无权限添加该信息！" });
            //}
            var model = new RepairListModel();
            var result = repository.SerachRepairByNewsName(RepairName, startTime, endTime);
            model.RepairIteams = result.ToList();
            return View("RepairList", model);
        }
    }
}