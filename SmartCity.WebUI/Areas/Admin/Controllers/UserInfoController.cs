using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using SmartCity.Common;
using SmartCity.Domain.Abstract;
using SmartCity.Domain.Entities;
using SmartCity.WebUI.Areas.Admin.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SmartCity.WebUI.Areas.Admin.Controllers
{
    /// <summary>
    /// 用户管理模块控制器
    /// </summary>
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
            if (CurrentUser.ManagerType == "管理员" || CurrentUser.ManagerType == "超级管理员")
            {
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
            //普通管理员无操作权限
            return Json(new { IsSuccess = 1, Message = "无权限添加用户信息！" });
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
            if (CurrentUser.ManagerType == "超级管理员"|| CurrentUser.ManagerType == "管理员")
            {

                if (string.IsNullOrEmpty(model.UserPassword))
                {
                    //修改用户
                    var result = repository.EditUser(model);
                    if (result)
                    {
                        log.Info(Utils.GetIP(), CurrentUser.ManagerAccount, Request.Url.ToString(), "UserInfo", "用户修改，修改后的名称为：" + model.UserName);
                        return Json(new { IsSuccess = 0, Message = "修改成功！" });
                    }
                }
                else
                {
                    model.UserPassword = MD5Crypt.EncryptAli(model.UserPassword);
                    //修改用户
                    var result = repository.EditUserInfoPassWord(model);
                    if (result)
                    {
                        log.Info(Utils.GetIP(), CurrentUser.ManagerAccount, Request.Url.ToString(), "UserInfo", "用户修改，修改后的名称为：" + model.UserName);
                        return Json(new { IsSuccess = 0, Message = "修改成功！" });
                    }
                }
                return Json(new { IsSuccess = 1, Message = "修改失败，请稍后重试!" });
            }
            return Json(new { IsSuccess = 1, Message = "你无权限修改该数据！" });
        }
        [HttpPost]
        public ActionResult DeleteUserInfoByID(int OwnerID)
        {
            if (CurrentUser.ManagerType == "超级管理员" || CurrentUser.ManagerType == "管理员")
            {
                var result = repository.DeleteUser(OwnerID);
                if (result)
                {
                    log.Info(Utils.GetIP(), CurrentUser.ManagerAccount, Request.Url.ToString(), "UserInfo", "用户删除，删除的ID为：" + OwnerID);
                    return Json(new { IsSuccess = 0, Message = "删除成功！" });
                }
                return Json(new { IsSuccess = 1, Message = "删除失败，请稍后重试!" });
            }
            //普通管理员无操作权限
            return Json(new { IsSuccess = 1, Message = "你无权限删除该数据！" });
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

        /// <summary>
        /// 批量删除
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult BatchRemoveUserInfo(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return Json(new { IsSuccess = 1, Message = "你未选择删除的用户！" });
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
            if (repository.BatchRemoveUserInfo(List))
            {
                for (int i = 0; i < NameList.Count; i++)
                {
                    log.Info(Utils.GetIP(), CurrentUser.ManagerAccount, Request.Url.ToString(), "UserInfo", "用户删除，删除的用户为：" + NameList[i]);
                }
                return Json(new { IsSuccess = 0, Message = "删除成功！" });
            }
            return Json(new { IsSuccess = 1, Message = "删除失败，请稍后重试！" });
        }

        public ActionResult SearchUserInfoByID(int OwnerID)
        {
            var result = repository.GetUserInfoByID(OwnerID);
            return View(result.First());
        }

        /// <summary>
        /// 导出用户信息列表
        /// </summary>
        /// <returns></returns>
        public FileResult UserInfoDataToExcl()
        {
            var result = repository.GetUserInfoList().ToList();
            string[] colInfos = { "用户编号", "用户账户", "用户名字", "用户性别", "用户电话", "用户邮箱", "用户地址", "创建时间" };
            NpoiHelper Npoi = new NpoiHelper("用户信息报表记录", colInfos);
            ICellStyle cellStyle = Npoi.Workbook.CreateCellStyle();
            cellStyle.Alignment = HorizontalAlignment.Center;
            cellStyle.BorderBottom = NPOI.SS.UserModel.BorderStyle.Thin;
            cellStyle.BorderTop = NPOI.SS.UserModel.BorderStyle.Thin;
            cellStyle.BorderLeft = NPOI.SS.UserModel.BorderStyle.Thin;
            cellStyle.BorderRight = NPOI.SS.UserModel.BorderStyle.Thin;
            int k = 2;  //注意内容的行数并不是从第一行开始的
            int colCount = Npoi._params.Length;
            //先遍历dt 取出行数（dr数目），每行第一列添加一个序号的表头，再遍历表头信息数组填充数据
            for (int i = 0; i < result.Count; i++)
            {
                HSSFRow row = (HSSFRow)Npoi._sheet1.CreateRow(i + 2);
                row.CreateCell(0).SetCellValue(result[i].OwnerID.ToString());
                row.CreateCell(1).SetCellValue(result[i].UserAccount.ToString());
                row.CreateCell(2).SetCellValue(result[i].UserName.ToString());
                row.CreateCell(3).SetCellValue(result[i].UserSex==0?"男":"女");
                row.CreateCell(4).SetCellValue(result[i].UserPhone.ToString());
                row.CreateCell(5).SetCellValue(result[i].UserEmail.ToString());
                row.CreateCell(6).SetCellValue(result[i].UserAddress.ToString());
                row.CreateCell(7).SetCellValue(result[i].CreateTime.ToString());
            }
            System.IO.MemoryStream ms = new MemoryStream();
            Npoi.Workbook.Write(ms);
            ms.Seek(0, SeekOrigin.Begin);
            return File(ms, "application/vnd.ms-excel", HttpUtility.UrlPathEncode("用户信息报表记录" + DateTime.Now.ToString() + ".xls"));
        }
        #endregion
    }
}