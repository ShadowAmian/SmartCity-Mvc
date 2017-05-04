using SmartCity.Domain.Abstract;
using SmartCity.Domain.Entities;
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
            return View();
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