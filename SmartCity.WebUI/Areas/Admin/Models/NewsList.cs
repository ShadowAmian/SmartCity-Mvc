using SmartCity.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SmartCity.WebUI.Areas.Admin.Models
{
    public class NewsList
    {

        public NewsList()
        {
            NewsIteams = new List<Notice>();
        }
        public IList<Notice> NewsIteams { get; set; }
        /// <summary>
        /// 公告标题
        /// </summary>
        public string NewsTitle { get; set; }
        /// <summary>
        /// 简单标题
        /// </summary>
        public string NewsSimpleTitle { get; set; }
        /// <summary>
        /// 公告类别
        /// </summary>
        public string NewsChassify { get; set; }
        /// <summary>
        /// 关键字
        /// </summary>
        public string NewsKaywords { get; set; }
        /// <summary>
        /// 公告摘要
        /// </summary>
        public string NewsDigest { get; set; }
        /// <summary>
        /// 公告作者
        /// </summary>
        public string NewsAuthor { get; set; }
        /// <summary>
        /// 是否发表
        /// </summary>
        public int IsComment { get; set; }
        /// <summary>
        /// 公告图片
        /// </summary>
        public HttpPostedFileBase NewsImages { get; set; }
        /// <summary>
        /// 公告内容
        /// </summary>
        public string NewsContent { get; set; }

    }
}