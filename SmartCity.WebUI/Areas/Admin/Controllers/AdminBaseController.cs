using SmartCity.Common;
using SmartCity.Common.log4net.Ext;
using SmartCity.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SmartCity.WebUI.Areas.Admin.Controllers
{
    /// <summary>
    /// 基类控制器
    /// </summary>
    public class AdminBaseController : Controller
    {

        #region 字段和属性
        /// <summary>
        /// 日志记录
        /// </summary>
        public IExtLog log = ExtLogManager.GetLogger("dblog");
        #endregion

        #region 用户对象
        /// <summary>
        /// 获取当前用户对象
        /// </summary>
        public Manager CurrentUser
        {
            get
            {
                //从Session中获取用户对象
                if (SessionHelper.GetSession("CurrentManager") != null)
                {
                    return SessionHelper.GetSession("CurrentManager") as Manager;
                }
                ////Session过期 通过Cookies中的信息 重新获取用户对象 并存储于Session中
                //var account = UserManage.GetAccountByCookie();
                //SessionHelper.SetSession("CurrentManager", account);
                //return account;
                return null;
            }
        }
        #endregion
        protected override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            #region 登录用户验证
            //1、判断Session对象是否存在
            if (filterContext.HttpContext.Session == null)
            {
                filterContext.HttpContext.Response.Write(
                       " <script type='text/javascript'> alert('~登录已过期，请重新登录');window.top.location='/Admin/Login/AdminLogin'; </script>");
                filterContext.RequestContext.HttpContext.Response.End();
                filterContext.Result = new EmptyResult();
                return;
            }
            //2、登录验证
            if (this.CurrentUser == null)
            {
                filterContext.HttpContext.Response.Write(
                    " <script type='text/javascript'> alert('登录已过期，请重新登录'); window.top.location='/Admin/Login/AdminLogin';</script>");
                filterContext.RequestContext.HttpContext.Response.End();
                filterContext.Result = new EmptyResult();
                return;
                #endregion
            }
        }
        }
    }