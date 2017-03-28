using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartCity.Domain.Entities
{
    /// <summary>
    /// 通知资讯管理
    /// Add zhangmian by 2017/2/13
    /// </summary>
    public partial class Notice
    {
        /// <summary>
        ///公告ID
        /// </summary>
        public int NewsID { get; set; }
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
        public string NewKaywords { get; set; }
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
        public string NewsImages { get; set; }
        /// <summary>
        /// 公告内容
        /// </summary>
        public string NewsContent { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime { get; set; }

    }
}
