using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartCity.Domain.Entities
{
    /// <summary>
    /// 评论列表
    /// </summary>
     public class Review
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
    }
}
