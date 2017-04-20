using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartCity.Domain.Entities
{
    /// <summary>
    /// 论坛发帖
    /// </summary>
    public class Posts
    {
        /// <summary>
        /// 帖子编号
        /// </summary>
        public int PostsID { get; set; }
        /// <summary>
        /// 帖子标题
        /// </summary>
        public string PostsTitle { get; set; }
        /// <summary>
        /// 帖子标签
        /// </summary>
        public string PostsLable { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime { get; set; }
        /// <summary>
        /// 观看次数
        /// </summary>
        public int TimesWatched { get; set; }
        /// <summary>
        /// 评论数
        /// </summary>
        public int CommentsNumber { get; set; }
        /// <summary>
        /// 简要内容
        /// </summary>
        public string BriefContent { get; set; }
        /// <summary>
        /// 内容
        /// </summary>
        public string Contents{ get; set; }
        /// <summary>
        /// 用户model
        /// </summary>
        public User UserModel { get; set; }
    }
}
