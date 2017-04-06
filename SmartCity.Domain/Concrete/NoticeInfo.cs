using Dapper;
using SmartCity.Domain.Abstract;
using SmartCity.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartCity.Domain.Concrete
{
    /// <summary>
    /// 通告资讯管理类
    /// </summary>
    public class NoticeInfo : RepositoryContext, INoticeInfo
    {
        /// <summary>
        ///获取通告资讯集合
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Notice> GetNewsList()
        {
            return Conn.Query<Notice>("select * from News_Table");
        }
        /// <summary>
        /// 通告资讯添加
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool AddNews(Notice model)
        {
            var resule = Conn.Execute("Insert into News_Table values(@NewsTitle,@NewsSimpleTitle,@NewsChassify,@NewsKaywords,@NewsDigest,@NewsAuthor,@IsComment,@PublishStatus,@NewsImages,@NewsContent,@CreateTime)", model);
            if (resule == 1)
            {
                return true;
            }
            return false;
        }
        /// <summary>
        /// 修改公告状态
        /// </summary>
        /// <param name="ID"></param>
        /// <param name="Status"></param>
        /// <returns></returns>
        public bool EditPublishStatu(int ID,int Status)
        {
            var resule = Conn.Execute("update News_Table set PublishStatus=@PublishStatus where NewsID=@NewsID", new { PublishStatus=Status,NewsID=ID });
            if (resule == 1)
            {
                return true;
            }
            return false;

        }
        /// <summary>
        /// 查询公告内容
        /// </summary>
        /// <param name="NewsID"></param>
        /// <returns></returns>
        public IEnumerable<Notice> SearchContent(int NewsID)
        {
            return Conn.Query<Notice>("select * from News_Table where NewsID=@NewsID",new { NewsID=NewsID});
        }
        /// <summary>
        /// 删除公告内容
        /// </summary>
        /// <param name="NewsID"></param>
        /// <returns></returns>
        public bool DeleteNews(int NewsID)
        {
            var resule = Conn.Execute("delete from News_Table where NewsID=@NewsID ", new { NewsID = NewsID });
            if (resule == 1)
            {
                return true;
            }
            return false;
        }
    }
}
