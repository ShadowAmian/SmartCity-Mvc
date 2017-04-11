using SmartCity.Common;
using SmartCity.Domain.Abstract;
using SmartCity.WebUI.Areas.Admin.Models.FeedBacks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SmartCity.WebUI.Areas.Admin.Controllers
{
    /// <summary>
    /// 意见反馈控制器
    /// </summary>
    public class FeedBackController : AdminBaseController
    {

        #region 字段 构造函数
        private IFeedBackInfo repository;
        public FeedBackController(IFeedBackInfo FeedBack)
        {
            this.repository = FeedBack;
        }
        #endregion
        /// <summary>
        /// 意见列表
        /// </summary>
        /// <returns></returns>
        public ActionResult FeedBackList()
        {
            var model = repository.GetFeedBackInfoList().ToList();
            var Model = new FeedBackListModel();
            Model.FeedBackIteams = model;
            return View(Model);
        }
        /// <summary>
        /// 反馈删除
        /// </summary>
        /// <param name="NewsID"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult DeleteNews(int FeedBackID)
        {
            if (CurrentUser.ManagerType != "超级管理员"|| CurrentUser.ManagerType != "管理员")
            {
                return Json(new { IsSuccess = 1, Message = "你无权限删除该数据！" });
            }
            var result = repository.DeleteFeedBack(FeedBackID);
            if (result)
            {
                log.Info(Utils.GetIP(), CurrentUser.ManagerAccount, Request.Url.ToString(), "FeedBack", "意见反馈删除，删除的ID为：" + FeedBackID);
                return Json(new { IsSuccess = 0, Message = "删除成功！" });
            }
            return Json(new { IsSuccess = 1, Message = "删除失败，请稍后重试!" });
        }
    }
}