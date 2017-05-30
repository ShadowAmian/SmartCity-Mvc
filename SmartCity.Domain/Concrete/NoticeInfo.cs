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
        /// 获取已发布的通知公告
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Notice> GetNewsListByPublished()
        {
            return Conn.Query<Notice>("select top 4 * from News_Table where PublishStatus=0 order by NewsID desc ");
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
        ///获取公告信息集合
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Notice> GetNoticeInfoByID(int id)
        {
            return Conn.Query<Notice>("select * from News_Table where NewsID=@NewsID ", new { NewsID=id});
        }
        /// <summary>
        /// 修改公告状态
        /// </summary>
        /// <param name="ID"></param>
        /// <param name="Status"></param>
        /// <returns></returns>
        public bool EditPublishStatu(int ID, int Status)
        {
            var resule = Conn.Execute("update News_Table set PublishStatus=@PublishStatus where NewsID=@NewsID", new { PublishStatus = Status, NewsID = ID });
            if (resule == 1)
            {
                return true;
            }
            return false;

        }
        /// <summary>
        /// 修改公告
        /// </summary>
        /// <param name="ID"></param>
        /// <param name="Status"></param>
        /// <returns></returns>
        public bool EditNewsInfo(Notice model)
        {
            var resule = Conn.Execute("update News_Table set NewsTitle=@NewsTitle,NewsSimpleTitle=@NewsSimpleTitle,NewsChassify=@NewsChassify,NewsKaywords=@NewsKaywords,NewsDigest=@NewsDigest,NewsAuthor=@NewsAuthor,NewsImages=@NewsImages,NewsContent=@NewsContent where NewsID=@NewsID", model);
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
            return Conn.Query<Notice>("select * from News_Table where NewsID=@NewsID", new { NewsID = NewsID });
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
        /// <summary>
        /// 查询公告内容
        /// </summary>
        /// <param name="NewsID"></param>
        /// <returns></returns>
        public IEnumerable<Notice> SerachNewsByNewsName(string NewsName, DateTime? startTime, DateTime? endTime)
        {
            string sql;
            if (string.IsNullOrEmpty(NewsName)&& startTime != null)
            {
                sql = "select * from News_Table where CreateTime Between @Time1 and @Time2";
            }
            else if (!string.IsNullOrEmpty(NewsName) && startTime != null)
            {
                sql = "select * from News_Table where NewsTitle=@NewsTitle and CreateTime Between @Time1 and @Time2";
            }
            else if (!string.IsNullOrEmpty(NewsName) && startTime == null)
            {
                sql = "select * from News_Table where NewsTitle=@NewsTitle";
            }
            else
            {
                sql = "select * from News_Table";
            }
            return Conn.Query<Notice>(sql, new { NewsTitle = NewsName, Time1 = startTime, Time2 = endTime });
        }
        /// <summary>
        /// 批量删除
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool BatchRemoveNoticeInfo(List<int> id)
        {
            var resule = Conn.Execute("delete from News_Table where NewsID in @NewsID ", new { NewsID = id.ToList() });
            if (resule >0)
            {
                return true;
            }
            return false;
        }
    }
}
