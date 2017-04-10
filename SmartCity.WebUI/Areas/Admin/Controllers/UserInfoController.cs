using SmartCity.Common;
using SmartCity.Domain.Abstract;
using SmartCity.Domain.Entities;
using SmartCity.WebUI.Areas.Admin.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SmartCity.WebUI.Areas.Admin.Controllers
{
    public class UserInfoController : AdminBaseController
    {
        #region 字段 构造函数

        private IUserInfo repository;
        public UserInfoController(IUserInfo UserInfo)
        {
            this.repository = UserInfo;
        }
        #endregion

        #region 方法
        // GET: Admin/UserInfo
        public ActionResult Index()
        {
            var model = new UserListModel();
            model.UserIteams = repository.GetUserInfoList().ToList();
            return View(model);
        }
        /// <summary>
        /// 用户添加添加
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult AddUserByGet()
        {
            return View();
        }
        [HttpPost]
        public ActionResult AddUserByPost(User model)
        {
            if (CurrentUser.ManagerType != "管理员" && CurrentUser.ManagerType != "超级管理员")
            {
                //普通管理员无操作权限
                return Json(new { IsSuccess = 1, Message = "无权限添加用户信息！" });
            }
            //判断管理员是否存在
            var IsExist = repository.UserIsExist(model.UserAccount);
            if (IsExist)
            {
                return Json(new { IsSuccess = 1, Message = "添加失败，该用户已存在!" });
            }
            model.UserPassword = SmartCity.Common.MD5Crypt.EncryptAli(model.UserPassword);
            model.CreateTime = DateTime.Now;
            var result = repository.AddUser(model);
            if (result)
            {
                log.Info(Utils.GetIP(), CurrentUser.ManagerAccount, Request.Url.ToString(), "UserInfo", "用户添加，账号为：" + model.UserAccount);
                return Json(new { IsSuccess = 0, Message = "添加成功!" });
            }
            return Json(new { IsSuccess = 1, Message = "添加失败，请稍后重试!" });
        }
        /// <summary>
        /// 修改用户信息
        /// </summary>
        /// <returns></returns>
        [HttpGet, ActionName("UpdateUserInfo")]
        public ActionResult UpdateUserByGet(int OwnerID)
        {
            var result = repository.GetUserInfoByID(OwnerID);
            return View(result.First());
        }
        [HttpPost, ActionName("UpdateUserInfo")]
        public ActionResult UpdateUserByPost(User model)
        {
            if (CurrentUser.ManagerType != "超级管理员"|| CurrentUser.ManagerType != "管理员")
            {
                //普通管理员无操作权限
                return Json(new { IsSuccess = 1, Message = "你无权限修改该数据！" });
            }
            //修改用户
            var result = repository.EditUser(model);
            if (result)
            {
                log.Info(Utils.GetIP(), CurrentUser.ManagerAccount, Request.Url.ToString(), "UserInfo", "用户修改，修改后的名称为：" + model.UserName);
                return Json(new { IsSuccess = 0, Message = "修改成功！" });
            }
            return Json(new { IsSuccess = 1, Message = "修改失败，请稍后重试!" });
        }
        [HttpPost]
        public ActionResult DeleteUserInfoByID(int OwnerID)
        {
            if (CurrentUser.ManagerType != "超级管理员" || CurrentUser.ManagerType != "管理员")
            {
                //普通管理员无操作权限
                return Json(new { IsSuccess = 1, Message = "你无权限删除该数据！" });
            }
            var result = repository.DeleteUser(OwnerID);
            if (result)
            {
                log.Info(Utils.GetIP(), CurrentUser.ManagerAccount, Request.Url.ToString(), "UserInfo", "用户删除，删除的ID为：" + OwnerID);
                return Json(new { IsSuccess = 0, Message = "删除成功！" });
            }
            return Json(new { IsSuccess = 1, Message = "删除失败，请稍后重试!" });
        }
        public ActionResult SerachUserInfoByUserName(string UserName)
        {
           
            var result = repository.SearchUserInfo(UserName);
            var model = new UserListModel();
            if (result!=null)
            {
                model.UserIteams = result.ToList();
            }

            return View("Index", model);
        }
        #endregion
    }
}