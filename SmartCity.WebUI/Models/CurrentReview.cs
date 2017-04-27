using SmartCity.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SmartCity.WebUI.Models
{
    public class CurrentReview
    {
        /// <summary>
        /// 评论编号
        /// </summary>
        public int ReviewID { get; set; }
        /// <summary>
        /// 评论内容
        /// </summary>
        public string ReviewContent { get; set; }
        /// <summary>
        /// 评论时间
        /// </summary>
        public DateTime CreateTime { get; set; }
        /// <summary>
        /// 回复model
        /// </summary>
        public List<Reply> ReplyModel { get; set; }
        /// <summary>
        /// 用户Model
        /// </summary>
        public User UserModel { get; set; }
        /// <summary>
        /// 论坛model
        /// </summary>
        public Posts PostsModel { get; set; }
    }
}