using SmartCity.Domain.Abstract;
using SmartCity.Domain.Entities;
using SmartCity.WebUI.Areas.Admin.Models;
using SmartCity.WebUI.Areas.Admin.Models.News;
using SmartCity.WebUI.Infrastructure.EnumData;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SmartCity.WebUI.Areas.Admin.Controllers
{
    /// <summary>
    /// 公告控制器
    /// </summary>
    public class NewsController : Controller
    {

        #region 字段 构造函数
        private INoticeInfo repository;
        public NewsController(INoticeInfo NewsInfo)
        {
            this.repository = NewsInfo;
        }
        #endregion
        #region 公告管理模块
        /// <summary>
        /// 公告列表
        /// </summary>
        /// <returns></returns>
        public ActionResult NewsList()
        {
            var model = new List<NewsModel>();
            var Model = new NewsList();
            var result = repository.GetNewsList().ToList();
            foreach (var item in result)
            {
                var newsModel = new NewsModel()
                {
                    NewsID = item.NewsID,
                    NewsTitle = item.NewsTitle,
                    NewsSimpleTitle = item.NewsSimpleTitle,
                    PublishStatus = Enum.GetName(typeof(NewsStatus), item.PublishStatus),
                    IsComment = item.IsComment,
                    NewsAuthor = item.NewsAuthor,
                    NewsChassify = item.NewsChassify,
                    NewsContent = item.NewsContent,
                    NewsDigest = item.NewsDigest,
                    NewsKaywords = item.NewsKaywords,
                    CreateTime=item.CreateTime
                };
                model.Add(newsModel);
            }
            Model.NewsIteams = model;
            //格式转换
            return View(Model);
        }
        /// <summary>
        /// 公告添加
        /// </summary>
        /// <returns></returns>
        [ActionName("NewsInfoAdd")]
        public ActionResult NewsAdd()
        {
            return View();
        }
        [ValidateInput(false)]
        [HttpPost, ActionName("NewsInfoAdd")]
        public ActionResult AddNewsInfo(NewsList model)
        {
            //if (CurrentUser.ManagerType != "超级管理员")
            //{
            //    //普通管理员无操作权限
            //    return Json(new { IsSuccess = 1, Message = "无权限添加该信息！" });
            //}

            var NoticeModel = new Notice();
         
            if (model.NewsImages != null)
            {
                string filePath = Path.Combine(HttpContext.Server.MapPath("../Images"), Path.GetFileName(model.NewsImages.FileName)+DateTime.Now.ToString());
                model.NewsImages.SaveAs(filePath);
            }
            else
            {
                NoticeModel.NewsImages = "../Images";
            }

            if (model.IsComment != 0)
            {
                NoticeModel.IsComment = 1;
            }
            else
            {
                NoticeModel.IsComment = model.IsComment;
            }
            NoticeModel.NewsAuthor = model.NewsAuthor;
            NoticeModel.NewsDigest = model.NewsDigest;
            NoticeModel.NewsChassify = model.NewsChassify;
            NoticeModel.NewsContent = model.NewsContent;
            NoticeModel.NewsKaywords = model.NewsKaywords;
            NoticeModel.NewsSimpleTitle = model.NewsSimpleTitle;
            NoticeModel.NewsTitle = model.NewsTitle;
            NoticeModel.PublishStatus = 4;
            NoticeModel.CreateTime = DateTime.Now;

            var result = repository.AddNews(NoticeModel);
            if (result)
            {
                //log.Info(Utils.GetIP(), CurrentUser.ManagerAccount, Request.Url.ToString(), "News", "通知公告添加，公告为：" + model.NewsTitle);
                return Json(new { IsSuccess = 0, Message = "添加成功!" });
            }
            return Json(new { IsSuccess = 1, Message = "添加失败，请稍后重试!" });
        }
        /// <summary>
        /// 修改公告状态
        /// </summary>
        /// <param name="NewsID"></param>
        /// <returns></returns>
        public ActionResult EditPublishStatus(int NewsID,int Status)
        {
            //if (CurrentUser.ManagerType != "超级管理员")
            //{
            //    //普通管理员无操作权限
            //    return Json(new { IsSuccess = 1, Message = "无权限添加该信息！" });
            //}
            var result = repository.EditPublishStatu(NewsID,Status);
            if (result)
            {
                //log.Info(Utils.GetIP(), CurrentUser.ManagerAccount, Request.Url.ToString(), "News", "通知公告状态修改添加);
                return Json(new { IsSuccess = 0, Message = "修改成功！" });

            }
            return Json(new { IsSuccess = 1, Message = "修改失败，请稍后重试!" });
        }
        /// <summary>
        /// 查看公告
        /// </summary>
        /// <param name="NewsID"></param>
        /// <returns></returns>
        public ActionResult SearchNews(int NewsID)
        {
            return View();
        }
        #endregion


    }
}