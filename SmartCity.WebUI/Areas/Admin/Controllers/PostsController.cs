using SmartCity.Common;
using SmartCity.Domain.Abstract;
using SmartCity.WebUI.Areas.Admin.Models.Postss;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SmartCity.WebUI.Areas.Admin.Controllers
{
    /// <summary>
    /// 在线交流模块控制器
    /// </summary>
    public class PostsController : AdminBaseController
    {
        #region 字段 构造函数

        private IPostsInfo PostInfoService;
        private IUserInfo UserInfoService;

        public PostsController(IPostsInfo PostInfo,IUserInfo UserInfo)
        {
            this.PostInfoService = PostInfo;
            this.UserInfoService = UserInfo;
        }
        #endregion

        // GET: Admin/Posts
        public ActionResult PostsList()
        {

            var model = PostInfoService.GetPostsInfoList();
            var Models = new PostsListModel();
            Models.PostsIteams = model.ToList();
            return View(Models);
        }
        /// <summary>
        /// 在线交流删除
        /// </summary>
        /// <param name="FeedBackID"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult DeletePosts(int PostsID)
        {
            if (CurrentUser.ManagerType != "超级管理员" || CurrentUser.ManagerType != "管理员")
            {
                return Json(new { IsSuccess = 1, Message = "你无权限删除该数据！" });
            }
            var result = PostInfoService.DeletePosts(PostsID);
            if (result)
            {
                log.Info(Utils.GetIP(), CurrentUser.ManagerAccount, Request.Url.ToString(), "Posts", "在线交流帖子删除，删除的ID为：" + PostsID);
                return Json(new { IsSuccess = 0, Message = "删除成功！" });
            }
            return Json(new { IsSuccess = 1, Message = "删除失败，请稍后重试!" });
        }
        /// <summary>
        /// 批量删除
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult BatchRemovePosts(string id)
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
            if (PostInfoService.BatchRemovePosts(List))
            {
                for (int i = 0; i < NameList.Count; i++)
                {
                    log.Info(Utils.GetIP(), CurrentUser.ManagerAccount, Request.Url.ToString(), "Posts", "在线交流帖子删除，删除的信息为：" + NameList[i]);
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
        public ActionResult SerachPosts(string UserName, DateTime? startTime, DateTime? endTime)
        {
            var Model = new PostsListModel();
            var id = UserInfoService.SearchUserInfoINUserID(UserName).ToList();
            var result = PostInfoService.SerachPosts(id, startTime, endTime).ToList();
            Model.PostsIteams = result;
            return View("PostsList", Model);
        }
    }
}