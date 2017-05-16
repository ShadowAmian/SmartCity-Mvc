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
        private IUserInfo UserInfoService;
        public FeedBackController(IFeedBackInfo FeedBack,IUserInfo UserInfo)
        {
            this.repository = FeedBack;
            this.UserInfoService = UserInfo;
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
        /// <param name="FeedBackID"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult DeleteFeedBack(int FeedBackID)
        {
            if (CurrentUser.ManagerType == "超级管理员"|| CurrentUser.ManagerType == "管理员")
            {
                var result = repository.DeleteFeedBack(FeedBackID);
                if (result)
                {
                    log.Info(Utils.GetIP(), CurrentUser.ManagerAccount, Request.Url.ToString(), "FeedBack", "意见反馈删除，删除的ID为：" + FeedBackID);
                    return Json(new { IsSuccess = 0, Message = "删除成功！" });
                }
                return Json(new { IsSuccess = 1, Message = "删除失败，请稍后重试!" });
        
            }
            return Json(new { IsSuccess = 1, Message = "你无权限删除该数据！" });

        }
        /// <summary>
        /// 批量删除
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult BatchRemoveFeedBack(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return Json(new { IsSuccess = 1, Message = "你未选择删除的信息！" });
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
            if (repository.BatchRemoveFeedBack(List))
            {
                for (int i = 0; i < NameList.Count; i++)
                {
                    log.Info(Utils.GetIP(), CurrentUser.ManagerAccount, Request.Url.ToString(), "FeedBack", "反馈信息删除，删除的信息为：" + NameList[i]);
                }
                return Json(new { IsSuccess = 0, Message = "删除成功！" });
            }
            return Json(new { IsSuccess = 1, Message = "删除失败，请稍后重试！" });
        }
        /// <summary>
        /// 反馈搜索
        /// </summary>
        /// <param name="NewsName"></param>
        /// <param name="startTime"></param>
        /// <param name="endTime"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult SerachFeedBack(string UserName, DateTime? startTime, DateTime? endTime)
        {
            var Model = new FeedBackListModel();
            var id = UserInfoService.SearchUserInfoINUserID(UserName).ToList();
            var result = repository.SerachFeedBack(id, startTime, endTime).ToList();
            Model.FeedBackIteams = result;
            return View("FeedBackList", Model);
        }
    }
}