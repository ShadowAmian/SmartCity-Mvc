using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using SmartCity.Common;
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
    public class NewsController : AdminBaseController
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
                    CreateTime = item.CreateTime
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
                string filePath = Path.Combine(HttpContext.Server.MapPath("../Images"), Path.GetFileName(model.NewsImages.FileName) + DateTime.Now.ToString());
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
        public ActionResult EditPublishStatus(int NewsID, int Status)
        {
            //if (CurrentUser.ManagerType != "超级管理员")
            //{
            //    //普通管理员无操作权限
            //    return Json(new { IsSuccess = 1, Message = "无权限添加该信息！" });
            //}
            var result = repository.EditPublishStatu(NewsID, Status);
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
            var result = repository.SearchContent(NewsID).First();
            ViewBag.Content = result.NewsContent;
            return View();
        }
        /// <summary>
        /// 公告修改
        /// </summary>
        /// <returns></returns>
        [ActionName("NewsInfoEdit")]
        public ActionResult NewsEdit(int NewsID)
        {
            var result = repository.SearchContent(NewsID).First();
            var model = new NewsList();
            model.IsComment = result.IsComment;
            model.NewsAuthor = result.NewsAuthor;
            model.NewsChassify = result.NewsChassify;
            model.NewsContent = result.NewsContent;
            model.NewsDigest = result.NewsDigest;
            model.NewsKaywords = result.NewsKaywords;
            model.NewsSimpleTitle = result.NewsSimpleTitle;
            model.NewsTitle = result.NewsTitle;

            return View(model);
        }
        [HttpPost, ActionName("NewsInfoEdit")]
        public ActionResult EditNews(NewsList model)
        {
            //if (CurrentUser.ManagerType != "超级管理员")
            //{
            //    //普通管理员无操作权限
            //    return Json(new { IsSuccess = 1, Message = "你无权限修改该数据！" });
            //}
            var models = new Notice();
            models.NewsTitle = model.NewsTitle;
            models.NewsSimpleTitle = model.NewsSimpleTitle;
            models.NewsDigest = model.NewsDigest;
            models.NewsContent = model.NewsContent;
            models.NewsAuthor = model.NewsAuthor;
            models.NewsKaywords = model.NewsKaywords;
            models.NewsChassify = model.NewsChassify;
            models.IsComment = model.IsComment;
            models.NewsID = model.NewsID;
            //修改用户
            var result = repository.EditNewsInfo(models);
            if (result)
            {
                //log.Info(Utils.GetIP(), CurrentUser.ManagerAccount, Request.Url.ToString(), "News", "公告修改);
                return Json(new { IsSuccess = 0, Message = "修改成功！" });
            }
            return Json(new { IsSuccess = 1, Message = "修改失败，请稍后重试!" });

        }
        [HttpPost]
        public ActionResult DeleteNews(int NewsID)
        {
            //if (CurrentUser.ManagerType != "超级管理员")
            //{
            //    //普通管理员无操作权限
            //    return Json(new { IsSuccess = 1, Message = "你无权限删除该数据！" });
            //}
            var result = repository.DeleteNews(NewsID);
            if (result)
            {
                //log.Info(Utils.GetIP(), CurrentUser.ManagerAccount, Request.Url.ToString(), "Manager", "管理员删除，删除的ID为：" + ManagerID);
                return Json(new { IsSuccess = 0, Message = "删除成功！" });
            }
            return Json(new { IsSuccess = 1, Message = "删除失败，请稍后重试!" });
        }
        /// <summary>
        /// 公告搜索
        /// </summary>
        /// <param name="NewsName"></param>
        /// <param name="startTime"></param>
        /// <param name="endTime"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult SerachNewsByNewsName(string NewsName, DateTime? startTime, DateTime? endTime)
        {
            var model = new List<NewsModel>();
            var Model = new NewsList();
            var result = repository.SerachNewsByNewsName(NewsName, startTime, endTime).ToList();
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
                    CreateTime = item.CreateTime
                };
                model.Add(newsModel);
            }
            Model.NewsIteams = model;
            return View("NewsList", Model);
        }
        /// <summary>
        /// 批量删除
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult BatchRemoveNotice(string id)
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
            if (repository.BatchRemoveNoticeInfo(List))
            {
                for (int i = 0; i < NameList.Count; i++)
                {
                    log.Info(Utils.GetIP(), CurrentUser.ManagerAccount, Request.Url.ToString(), "Manager", "公告删除，删除的标题为：" + NameList[i]);
                }
                return Json(new { IsSuccess = 0, Message = "删除成功！" });
            }
            return Json(new { IsSuccess = 1, Message = "删除失败，请稍后重试！" });
        }
        /// <summary>
        /// 导出通知公告列表
        /// </summary>
        /// <returns></returns>
        public FileResult NewsDataToExcl()
        {
            var result = repository.GetNewsList().ToList();
            string[] colInfos = { "公告编号", "公告标题", "简单标题", "公告类别", "关键字", "公告摘要", "公告作者", "是否发表", "公告状态", "公告图片路径", "公告内容", "创建时间" };
            NpoiHelper Npoi = new NpoiHelper("通知公告记录", colInfos);
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
                row.CreateCell(0).SetCellValue(result[i].NewsID.ToString());
                row.CreateCell(1).SetCellValue(result[i].NewsTitle.ToString());
                row.CreateCell(2).SetCellValue(result[i].NewsSimpleTitle.ToString());
                row.CreateCell(3).SetCellValue(result[i].NewsChassify.ToString());
                row.CreateCell(4).SetCellValue(result[i].NewsKaywords.ToString());
                row.CreateCell(5).SetCellValue(result[i].NewsDigest.ToString());
                row.CreateCell(6).SetCellValue(result[i].NewsAuthor.ToString());
                row.CreateCell(7).SetCellValue(result[i].IsComment == 0 ? "是" : "否");
                row.CreateCell(8).SetCellValue(Enum.GetName(typeof(NewsStatus), result[i].PublishStatus).ToString());
                row.CreateCell(9).SetCellValue(result[i].NewsImages.ToString());
                row.CreateCell(10).SetCellValue(result[i].NewsContent.ToString());
                row.CreateCell(11).SetCellValue(result[i].CreateTime.ToString());
            }
            System.IO.MemoryStream ms = new MemoryStream();
            Npoi.Workbook.Write(ms);
            ms.Seek(0,SeekOrigin.Begin);
            return File(ms, "application/vnd.ms-excel", HttpUtility.UrlPathEncode("通知公告记录" +DateTime.Now.ToString()+".xls"));
        }

        #endregion
    }
}