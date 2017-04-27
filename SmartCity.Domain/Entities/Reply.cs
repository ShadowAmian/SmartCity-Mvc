using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartCity.Domain.Entities
{
    /// <summary>
    /// 回复类
    /// </summary>
    public class Reply
    {
        /// <summary>
        /// 回复编号
        /// </summary>
        public int ReplyID { get; set; }
        /// <summary>
        /// 回复内容
        /// </summary>
        public int ReplyContent { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime { get; set; }
        /// <summary>
        /// 评论ID
        /// </summary>
        public int ReviewID { get; set; }
        /// <summary>
        /// 用户类Model
        /// </summary>
        public User UserModel { get; set; }

    }
}
