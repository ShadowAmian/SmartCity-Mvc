using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using SmartCity.Common;
using SmartCity.Domain.Abstract;
using SmartCity.Domain.Entities;
using SmartCity.WebUI.Areas.Admin.Models.Repairs;
using SmartCity.WebUI.Infrastructure.EnumData;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SmartCity.WebUI.Areas.Admin.Controllers
{
    /// <summary>
    /// 报修模块控制器
    /// </summary>
    public class RepairController : AdminBaseController
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
            bool Types = true;
            if (CurrentUser.ManagerType != "超级管理员")
            {
                Types = false;
                if (CurrentUser.ManagerType != "管理员")
                {
                    Types = false;
                }
                else
                {
                    Types = true;
                }
            }
            if (!Types)
            {
                var model = repository.GetRepairInfoListByManager(CurrentUser.ManagerID);
                var models = new RepairListModel();
                models.RepairIteams = model.ToList();
                return View(models);
            }
            else
            {
                var model = repository.GetRepairInfoList();
                var models = new RepairListModel();
                models.RepairIteams = model.ToList();
                return View(models);
            }
          
        }
        /// <summary>
        /// 修改维修状态
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ActionResult EditRepairStatus(int RepairID, int Status)
        {
            //if (CurrentUser.ManagerType != "超级管理员")
            //{
            //    //普通管理员无操作权限
            //    return Json(new { IsSuccess = 1, Message = "无权限添加该信息！" });
            //}
            var result = repository.EditRepairStatu(RepairID, Status);
            if (result)
            {
                log.Info(Utils.GetIP(), CurrentUser.ManagerAccount, Request.Url.ToString(), "Repair", "维修状态状态修改添加");
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
        
            var model = Manager.SearchMaintenance();
            var models = new RepairMaintenanceModel();
            models.Iteams = model.ToList();
            models.RepairID = RepairID;
            return View(models);
        }
        [HttpPost]
        public ActionResult MaintenanceDistributionByManager(int RepairID, int ManagerID, DateTime time)
        {
            if (CurrentUser.ManagerType == "管理员" || CurrentUser.ManagerType == "超级管理员")
            {
                var result = repository.EditRepairToManager(ManagerID, RepairID, time);
                if (result)
                {
                    log.Info(Utils.GetIP(), CurrentUser.ManagerAccount, Request.Url.ToString(), "Repair", "报修模块修改添加");
                    return Json(new { IsSuccess = 0, Message = "修改成功！" });

                }
                return Json(new { IsSuccess = 1, Message = "修改失败，请稍后重试!" });
            }
            //普通管理员无操作权限
            return Json(new { IsSuccess = 1, Message = "无权限添加该信息！" });
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
        /// <summary>
        /// 批量删除
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult BatchRemoveRepair(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return Json(new { IsSuccess = 1, Message = "你未选择删除用户！" });
            }

            List<int> List = new List<int>();
            List<string> NameList = new List<string>();
            string[] Ids = id.Split(',');
            for (int i = 0; i < Ids.Length; i++)
            {
                if (Ids[i] != "0" && Ids[i] != "")
                {
                    string[] DataName = Ids[i].Split('-');
                    List.Add(Convert.ToInt32(DataName[0]));
                    NameList.Add(DataName[1]);
                }
            }
            if (repository.BatchRemoveRepairInfo(List))
            {
                for (int i = 0; i < NameList.Count; i++)
                {
                    log.Info(Utils.GetIP(), CurrentUser.ManagerAccount, Request.Url.ToString(), "Repair", "维修记录删除，删除的记录为：" + NameList[i]);
                }
                return Json(new { IsSuccess = 0, Message = "删除成功！" });
            }
            return Json(new { IsSuccess = 1, Message = "删除失败，请稍后重试！" });
        }

        /// <summary>
        /// 导出维修记录列表
        /// </summary>
        /// <returns></returns>
        public FileResult RepairDataToExcl()
        {
            var result = repository.GetRepairInfoList().ToList();
            string[] colInfos = { "报修编号", "报修主题", "报修类型", "报修内容", "报修人名称", "报修人地址", "报修人电话", "维修人名称", "报修状态", "报修时间", "创建时间" };
            NpoiHelper Npoi = new NpoiHelper("维修记录", colInfos);
            ICellStyle cellStyle = Npoi.Workbook.CreateCellStyle();
            cellStyle.Alignment = HorizontalAlignment.Center;
            cellStyle.BorderBottom = NPOI.SS.UserModel.BorderStyle.Thin;
            cellStyle.BorderTop = NPOI.SS.UserModel.BorderStyle.Thin;
            cellStyle.BorderLeft = NPOI.SS.UserModel.BorderStyle.Thin;
            cellStyle.BorderRight = NPOI.SS.UserModel.BorderStyle.Thin;
            int k = 2;  //注意内容的行数并不是从第一行开始的
            int colCount = Npoi._params.Length;
            //先遍历dt 取出行数（dr数目），每行第一列添加一个序号的表头，再遍历表头信息数组填充数据
            for (int i = 0; i < result.Count; i++)
            {
                HSSFRow row = (HSSFRow)Npoi._sheet1.CreateRow(i + 2);
                row.CreateCell(0).SetCellValue(result[i].RepairID.ToString());
                row.CreateCell(1).SetCellValue(result[i].RepairName.ToString());
                row.CreateCell(2).SetCellValue(Enum.GetName(typeof(RepairType), result[i].RepairType).ToString());
                row.CreateCell(3).SetCellValue(result[i].RepairContent.ToString());
                row.CreateCell(4).SetCellValue(result[i].UserInfo.UserName.ToString());
                row.CreateCell(5).SetCellValue(result[i].UserInfo.UserAddress.ToString());
                row.CreateCell(6).SetCellValue(result[i].UserInfo.UserPhone.ToString());
                row.CreateCell(7).SetCellValue(result[i].ManagerInfo.ManagerName.ToString());
                row.CreateCell(8).SetCellValue(Enum.GetName(typeof(RepairStatus), result[i].MaintenanceStatus).ToString());
                row.CreateCell(9).SetCellValue(result[i].RepairTime.ToString());
                row.CreateCell(10).SetCellValue(result[i].CreateTime.ToString());
            }
            System.IO.MemoryStream ms = new MemoryStream();
            Npoi.Workbook.Write(ms);
            ms.Seek(0, SeekOrigin.Begin);
            return File(ms, "application/vnd.ms-excel", HttpUtility.UrlPathEncode("维修记录" + DateTime.Now.ToString() + ".xls"));
        }
        /// <summary>
        /// 维修统计
        /// </summary>
        /// <returns></returns>
        
        public ActionResult RepairStatistics()
        {
            return View();
        }
        [HttpPost]
        public ActionResult GetRepairStatistics(string StartDate, string EndDate)
        {
            var result = repository.SerachRepairType();
            return Json(new { IsSuccess = 0, data = result });
        }
    }
}