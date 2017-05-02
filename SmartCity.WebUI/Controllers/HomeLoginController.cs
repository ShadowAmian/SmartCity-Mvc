using SmartCity.Common;
using SmartCity.Common.log4net.Ext;
using SmartCity.Domain.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SmartCity.WebUI.Controllers
{
    public class HomeLoginController : Controller
    {

        #region 字段 构造函数
        /// <summary>
        /// 日志记录
        /// </summary>
        public IExtLog log = ExtLogManager.GetLogger("dblog");
        private IUserInfo UserInfoService;

        public HomeLoginController( IUserInfo UserInfos)
        {
            this.UserInfoService = UserInfos;
        }
        #endregion
        [HttpPost]
        public ActionResult UserLogin(string Account, string Password)
        {
            var json = new JsonHelper() { Msg = "登录成功!", Status = "n" };
            try
            {
                var result = UserInfoService.UserLogin(Account, Common.CryptHelper.EncryptAli(Password));
                if (result)
                {
                    var ManagerModel = UserInfoService.GetUserInfo(Account);
                    SessionHelper.SetSession("HomeUserInfo", ManagerModel.First());
                    json.Status = "y";
                    json.ReUrl = "/HomePage/Index";
                    json.UserName = ManagerModel.First().UserName;
                    log.Info(Utils.GetIP(), Account, Request.Url.ToString(), "Login", "系统登录，登录结果：" + json.Msg);
                }
                else
                {
                    json.Msg = "用户名或密码不正确!";
                    log.Info(Utils.GetIP(), Account, Request.Url.ToString(), "Login", "系统登录，登录结果：" + json.Msg);
                }
            }
            catch (Exception e)
            {
                json.Msg = e.Message;
                log.Info(Utils.GetIP(), Account, Request.Url.ToString(), "Login", "系统登录，登录结果：" + json.Msg);
            }
            return Json(json, JsonRequestBehavior.AllowGet);
        }
    }
}