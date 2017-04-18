using SmartCity.Common;
using SmartCity.Common.log4net.Ext;
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
    public class ManagerController : AdminBaseController
    {

        #region 字段 构造函数
        private IManagerInfo repository;
        public ManagerController(IManagerInfo ManagerInfo)
        {
            this.repository = ManagerInfo;
        }
        #endregion

        #region 方法
        /// <summary>
        /// 加载管理员信息
        /// </summary>
        /// <returns></returns>
        // GET: Admin/Manager
        public ActionResult Index()
        {
            var model = new ManagerListModel();
            model.MangerIteams = repository.GetManagerInfoList().Where(list=>list.ManagerType!="超级管理员").ToList();
      
            return View(model);
        }
        /// <summary>
        /// 修改管理员是否启用
        /// </summary>
        /// <param name="ManagerID"></param>
        /// <param name="ManagerType"></param>
        /// <param name="IsEnable"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult UpdateManagerEnable(int ManagerID, string ManagerType, int IsEnable)
        {
            if (CurrentUser.ManagerType != "超级管理员")
            {
                //普通管理员无操作权限
                return Json(new { IsSuccess = 0, Message = "无权限修改该信息！" });
            }

            if (ManagerType == "超级管理员")
            {
                //超级管理员账号不允许停用
                return Json(new { IsSuccess = 0, Message = "超级管理员账号不允许停用！" });
            }
            var resule = repository.UpdateManagerEnable(ManagerID, IsEnable);
            if (resule)
            {
                log.Info(Utils.GetIP(), CurrentUser.ManagerAccount, Request.Url.ToString(), "Manager", "管理员添加，账号ID为：" + ManagerID);
                return Json(new { IsSuccess = 0, Message = "修改成功！" });
            }
            return Json(new { IsSuccess = 1, Message = "操作失败，请稍后重试！" });
        }
        /// <summary>
        /// 管理员添加
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult AddManagerByGet()
        {
            return View();
        }
        [HttpPost]
        public ActionResult AddManagerByPost(Manager model)
        {
            if (CurrentUser.ManagerType != "超级管理员")
            {
                //普通管理员无操作权限
                return Json(new { IsSuccess = 1, Message = "无权限添加该信息！" });
            }
            //判断管理员是否存在
            var IsExist = repository.ManagerIsExist(model.ManagerAccount);
            if (IsExist)
            {
                return Json(new { IsSuccess = 1, Message = "添加失败，该用户已存在!" });
            }
            model.IsEnable = 1;
            model.CreateTime = DateTime.Now;
            var result = repository.AddManager(model);
            if (result)
            {
                log.Info(Utils.GetIP(), CurrentUser.ManagerAccount, Request.Url.ToString(), "Manager", "管理员添加，账号为：" + model.ManagerAccount);
                return Json(new { IsSuccess = 0, Message = "添加成功!" });
            }
            return Json(new { IsSuccess = 1, Message = "添加失败，请稍后重试!" });
        }
        /// <summary>
        /// 修改管理员信息
        /// </summary>
        /// <returns></returns>
        [HttpGet, ActionName("UpdateManagerInfo")]
        public ActionResult UpdateManagerByGet(int ManagerID)
        {
            var result = repository.GetManagerInfoByID(ManagerID);
            return View(result.First());
        }
        [HttpPost, ActionName("UpdateManagerInfo")]
        public ActionResult UpdateManagerByPost(Manager model)
        {
            if (CurrentUser.ManagerType != "超级管理员")
            {
                //普通管理员无操作权限
                return Json(new { IsSuccess = 1, Message = "你无权限修改该数据！" });
            }
            //修改用户
            var result = repository.EditManager(model);
            if (result)
            {
                log.Info(Utils.GetIP(), CurrentUser.ManagerAccount, Request.Url.ToString(), "Manager", "管理员修改，修改后的名称为：" + model.ManagerName);
                return Json(new { IsSuccess = 0, Message = "修改成功！" });
            }
            return Json(new { IsSuccess = 1, Message = "修改失败，请稍后重试!" });
        }
        /// <summary>
        /// 删除管理员信息
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ActionResult DeleteManager(int ManagerID)
        {
            if (CurrentUser.ManagerType != "超级管理员")
            {
                //普通管理员无操作权限
                return Json(new { IsSuccess = 1, Message = "你无权限删除该数据！" });
            }
            var result = repository.DeleteManager(ManagerID);
            if (result)
            {
                log.Info(Utils.GetIP(), CurrentUser.ManagerAccount, Request.Url.ToString(), "Manager", "管理员删除，删除的ID为：" + ManagerID);
                return Json(new { IsSuccess = 0, Message = "删除成功！" });
            }
            return Json(new { IsSuccess = 1, Message = "删除失败，请稍后重试!" });
        }
        /// <summary>
        /// 查询管理员信息
        /// </summary>
        /// <param name="ManagerName"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult SearchManagerByManagerName(string ManagerName)
        {
            if (CurrentUser.ManagerType != "超级管理员")
            {
                //普通管理员无操作权限
                return Json(new { IsSuccess = 1, Message = "你无权限查询该数据！" });
            }
            var result = repository.SearchManager(ManagerName);
            if (result!=null)
            {
                log.Info(Utils.GetIP(), CurrentUser.ManagerAccount, Request.Url.ToString(), "Manager", "管理员查询，查询的条件为：" + ManagerName);
                return Json(new { IsSuccess = 0, Message = result.ToList() });
            }
            return Json(new { IsSuccess = 1, Message = "查询失败，请稍后重试!" });
        }

        /// <summary>
        /// 密码修改
        /// </summary>
        /// <returns></returns>
        public ActionResult PassWordEdit()
        {
            if (CurrentUser != null)
            {
                return View(CurrentUser);
            }
            var model = new Manager();
            return View(model);
        }
        [HttpPost]
        public ActionResult EditPassWord(int userid, string password2, string password, string password1)
        {

            string OldPassWord = SmartCity.Common.MD5Crypt.EncryptAli(password2.Trim());
            string NewPassWord = SmartCity.Common.MD5Crypt.EncryptAli(password.Trim());

            var result = repository.EditManagerPassword(userid,OldPassWord,NewPassWord);
            if (result)
            {
                return Json(new { IsSuccess = 0, Message = "密码修改成功！" });
            }
            return Json(new { IsSuccess = 1, Message = "密码修改失败！" });
        }
        /// <summary>
        /// 批量删除
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult BatchRemoveManager(string id)
        {
                if (string.IsNullOrEmpty(id))
                {
                    return Json(new { IsSuccess = 1, Message = "你未选择删除用户！" });
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
                if (repository.BatchRemoveManager(List))
                {
                    for (int i = 0; i < NameList.Count; i++)
                    {
                    log.Info(Utils.GetIP(), CurrentUser.ManagerAccount, Request.Url.ToString(), "Manager", "管理员删除，删除的管理员为：" + NameList[i]);
                    }
                    return Json(new { IsSuccess = 0, Message = "删除成功！" });
                }
                return Json(new { IsSuccess = 1, Message = "删除失败，请稍后重试！" });
        }
        #endregion
    }
}