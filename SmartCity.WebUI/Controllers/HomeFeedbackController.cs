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
    public class HomeFeedbackController : BaseController
    {

        private IFeedBackInfo FeedBackService;

        public HomeFeedbackController(IFeedBackInfo FeedBackInfo)
        {
            this.FeedBackService = FeedBackInfo;
        }
        // GET: Feedback
        public ActionResult FeedBackIndex()
        {
            var model = SessionHelper.GetSession("HomeUserInfo");
            var Model = new FeedBackModel();
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
        public ActionResult FeedBackAdd(string Content)
        {

            if (CurrentUserInfo != null)
            {
                var model = new FeedBack();
                model.ComplaintContent = Content;
                model.CreateTime = DateTime.Now;
                model.OwnerID = CurrentUserInfo.OwnerID;
                var result = FeedBackService.FeedBackAdd(model);
                if (result)
                {
                    return Json(new { IsSuccess = 0, Message = "谢谢你的反馈，我们确定后会与你电话联系！" });
                }
                return Json(new { IsSuccess = 1, Message = "反馈失败，请稍后重试！" });
            }
            return Json(new { IsSuccess = 1, Message = "对不起，你还未登陆，不能进行意见反馈！" });
        

        }
    }
}