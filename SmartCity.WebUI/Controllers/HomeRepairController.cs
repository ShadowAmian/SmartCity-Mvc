using SmartCity.Common;
using SmartCity.Domain.Abstract;
using SmartCity.Domain.Entities;
using SmartCity.WebUI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SmartCity.WebUI.Controllers
{
    /// <summary>
    /// 报修控制器
    /// </summary>
    public class HomeRepairController : BaseController
    {
        private IRepairInfo RepairInfoService;

        public HomeRepairController(IRepairInfo RepairInfo)
        {
            this.RepairInfoService = RepairInfo;
        }
        // GET: HomeRepair
        public ActionResult RepairInfoAdd()
        {
            var model = SessionHelper.GetSession("HomeUserInfo");
            var Model = new RepairInfoModel();
            Model.Title1 = "Hi, 请登录";
            Model.Tiltle2 = "我要注册";
            Model.TitleUrL1 = "#";
            Model.TitleUrl2 = "#";
            if (model != null)
            {
                var models = model as User;
                Model.Title1 = "Hi, 欢迎你";
                Model.Tiltle2 = models.UserName;
            }
            return View(Model);
        }
        public ActionResult RepairInfoAddForUser(string RepairName, string RepairType, string RepairContent)
        {
            if (CurrentUserInfo != null)
            {
                var model = new Repair();
                model.RepairName = RepairName;
                model.RepairType = Convert.ToInt32(Enum.Parse(typeof(SmartCity.WebUI.Infrastructure.EnumData.RepairType), RepairType));
                model.RepairContent = RepairContent;
                model.OwnerID = CurrentUserInfo.OwnerID;
                model.MaintenanceStatus = 3;
                model.CreateTime = DateTime.Now;
                var result = RepairInfoService.RepairInfoAdd(model);
                if (result)
                {
                    return Json(new { IsSuccess = 0, Message = "谢谢你的反馈，我们确定后会与你电话联系！" });
                }
                return Json(new { IsSuccess = 1, Message = "反馈失败，请稍后重试！" });
            }
            return Json(new { IsSuccess = 1, Message = "对不起，你还未登陆，不能进行在线报修！" });


        }
    }
}