using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartCity.Domain.Entities
{
    /// <summary>
    /// 公告实体类
    /// </summary>
    public class News
    {
        /// <summary>
        /// 公告编号
        /// </summary>
        public int NewsID { get; set; }
        /// <summary>
        /// 公告标题
        /// </summary>
        public string NewsTitle { get; set;  }
        /// <summary>
        /// 公告缩略标题
        /// </summary>
        public string NewsSimpleTitle { get; set; }
        /// <summary>
        /// 公告分类
        /// </summary>
        public string NewsCharssify { get; set; }
        /// <summary>
        /// 公告关键词
        /// </summary>
        public string NewsKaywords { get; set; }
        /// <summary>
        /// 公告摘要
        /// </summary>
        public string NewsDigest { get; set; }
        /// <summary>
        /// 公告作者
        /// </summary>
        public string Author { get; set; }
        /// <summary>
        /// 是否发布
        /// </summary>
        public int IsComment { get; set; }
        /// <summary>
        /// 新闻图片
        /// </summary>
        public string NewsImages { get; set; }
        /// <summary>
        /// 新闻内容
        /// </summary>
        public string NewsContent { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime { get; set; }

    }
}
