using SmartCity.Common;
using SmartCity.Common.log4net.Ext;
using SmartCity.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SmartCity.WebUI.Controllers
{
    public class BaseController : Controller
    {
        #region 字段和属性
        /// <summary>
        /// 日志记录
        /// </summary>
        public IExtLog log = ExtLogManager.GetLogger("dblog");

        public int PageCounts = 10;
        #endregion

        #region 用户对象
        /// <summary>
        /// 获取当前用户对象
        /// </summary>
        public User CurrentUserInfo
        {
            get
            {
                //从Session中获取用户对象
                if (SessionHelper.GetSession("HomeUserInfo") != null)
                {
                    return SessionHelper.GetSession("HomeUserInfo") as User;
                }
             
                return null;
            }
        }
        #endregion
    }
}