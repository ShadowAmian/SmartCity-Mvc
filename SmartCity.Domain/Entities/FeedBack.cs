using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartCity.Domain.Entities
{
    /// <summary>
    /// 意见反馈Model
    /// Add 2017-4-1 by zhangmian
    /// </summary>
    public class FeedBack
    {
        /// <summary>
        /// 意见编号
        /// </summary>
        public int ComplaintID { get; set; }
        /// <summary>
        /// 用户信息
        /// </summary>
        public User OwnerInfo { get; set; }
        /// <summary>
        /// 用户编号
        /// </summary>
        public int OwnerID { get; set; }
        /// <summary>
        /// 意见内容
        /// </summary>
        public string ComplaintContent { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime { get; set; }
    }
}
