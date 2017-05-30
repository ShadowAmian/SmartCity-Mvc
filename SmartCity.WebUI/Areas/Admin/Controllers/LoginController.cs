using SmartCity.Common;
using SmartCity.Common.log4net.Ext;
using SmartCity.Domain.Abstract;
using SmartCity.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SmartCity.WebUI.Areas.Admin.Controllers
{
    /// <summary>
    /// 登录控制器
    /// </summary>
    public class LoginController : Controller
    {
        private IManagerInfo repository;
        /// <summary>
        /// 日志记录
        /// </summary>
        IExtLog log = ExtLogManager.GetLogger("dblog");
        public LoginController(IManagerInfo ManagerInfo)
        {
            this.repository = ManagerInfo;
        }
        // GET: Admin/Login
        [ActionName("AdminLogin")]
        [HttpGet]
        public ActionResult GetToLogin()
        {
            return View();
        }
        [ActionName("AdminLogin")]
        [HttpPost]
        public ActionResult PostToLogin(string AdminAccount, string AdminPassWord, string VCode)
        {
            var json = new JsonHelper() { Msg = "登录成功!", Status = "n" };
            try
            {
                if (Session["ValidateCodes"] != null && VCode.ToLower() == Session["ValidateCodes"].ToString())
                {
                    //调用登录验证接口,返回用户实体类
                    var result = repository.ManagerLogin(AdminAccount, Common.CryptHelper.EncryptAli(AdminPassWord));
                    if (result)
                    {
                        var ManagerModel = repository.GetManagerInfo(AdminAccount);
                        if (ManagerModel.First().IsEnable==1)
                        {
                            SessionHelper.SetSession("CurrentManager", ManagerModel.First());
                            json.Status = "y";
                            json.ReUrl = "/Admin/Home/Index";
                            log.Info(Utils.GetIP(), AdminAccount, Request.Url.ToString(), "Login", "系统登录，登录结果：" + json.Msg);
                        }
                        json.Msg = "你的账号已被系统管理员停用，请联系管理员！";
                        log.Info(Utils.GetIP(), AdminAccount, Request.Url.ToString(), "Login", "系统登录，登录结果：" + json.Msg);
                    }
                    else
                    {
                        json.Msg = "用户名或密码不正确!";
                        log.Info(Utils.GetIP(), AdminAccount, Request.Url.ToString(), "Login", "系统登录，登录结果：" + json.Msg);
                    }
                }
                else
                {
                    json.Msg = "验证码过期，请刷新验证码!";
                    log.Info(Utils.GetIP(), AdminAccount, Request.Url.ToString(), "Login", "系统登录，登录结果：" + json.Msg);
                }
            }
            catch (Exception e)
            {
                json.Msg = e.Message;
                log.Info(Utils.GetIP(), AdminAccount, Request.Url.ToString(), "Login", "系统登录，登录结果：" + json.Msg);
            }
            return Json(json, JsonRequestBehavior.AllowGet);
        }
    }
}