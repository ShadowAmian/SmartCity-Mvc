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
    /// 帖子操作类
    /// add 2017-04-15 by zhangmian
    /// </summary>
    public class PostsInfo : RepositoryContext, IPostsInfo
    {
        /// <summary>
        ///获取帖子信息集合
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Posts> GetPostsInfoList()
        {
            return Conn.Query<Posts, User, Posts>("select * from Posts_Table as P join User_Table as u on P.UserID=u.OwnerID  ", (posts, user) => { posts.UserModel = user; return posts; }, splitOn: "OwnerID");
        }
        /// <summary>
        ///获取帖子信息集合
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Posts> GetPostsInfoListByPage(int PageSize,int PageIndex,out int TotalPage)
        {
            var model = Conn.Query<PageCurr>("select total=count(*) from Posts_Table").ToList();
            var PageCount = PageSize * (PageIndex - 1);
            TotalPage=Convert.ToInt32( Math.Ceiling(Convert.ToDecimal((model.First().total + PageSize - 1) / PageSize)));
            var sql = "select top (@PageSize) * from Posts_Table as P join User_Table as u on P.UserID=u.OwnerID where PostsID not in( select top (@PageCount) PostsID from Posts_Table order by PostsID)order by PostsID";
            return Conn.Query<Posts, User, Posts>(sql, (posts, user) => { posts.UserModel = user; return posts; },new { PageSize = PageSize, PageCount = PageCount }, splitOn: "OwnerID");
        }
        /// <summary>
        ///获取帖子信息集合
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Posts> GetPostsInfoListByPageAndType(int PageSize, int PageIndex, out int TotalPage,string PostsLaber)
        {
            var model = Conn.Query<PageCurr>("select total=count(*) from Posts_Table").ToList();
            var PageCount = PageSize * (PageIndex - 1);
            TotalPage = Convert.ToInt32(Math.Ceiling(Convert.ToDecimal((model.First().total + PageSize - 1) / PageSize)));
            var sql = "select top (@PageSize) * from Posts_Table as P join User_Table as u on P.UserID=u.OwnerID where PostsLable=@PostsLable and  PostsID not in( select top (@PageCount) PostsID from Posts_Table order by PostsID)order by PostsID";
            return Conn.Query<Posts, User, Posts>(sql, (posts, user) => { posts.UserModel = user; return posts; }, new { PageSize = PageSize, PageCount = PageCount,PostsLable= PostsLaber }, splitOn: "OwnerID");
        }

        /// <summary>
        ///获取帖子信息集合
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Posts> GetPostsInfoByID(int id)
        {
            return Conn.Query<Posts, User, Posts>("select * from Posts_Table as P join User_Table as u on P.UserID=u.OwnerID where PostsID=@PostsID ", (posts, user) => { posts.UserModel = user; return posts; }, new { PostsID = id }, null, true, splitOn: "OwnerID");
        }
        /// <summary>
        /// 获取热门帖子信息集合
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Posts> GetHotPostsInfo()
        {
            return Conn.Query<Posts, User, Posts>("select top 5 * from Posts_Table as P join User_Table as u on P.UserID=u.OwnerID  order by TimesWatched desc;", (posts, user) => { posts.UserModel = user; return posts; }, splitOn: "OwnerID");
        }
        /// <summary>
        /// 删除在线帖子内容
        /// </summary>
        /// <param name="NewsID"></param>
        /// <returns></returns>
        public bool DeletePosts(int PostsID)
        {
            var resule = Conn.Execute("delete from Posts_Table where PostsID=@PostsID ", new { PostsID = PostsID });
            if (resule == 1)
            {
                return true;
            }
            return false;
        }
        /// <summary>
        /// 批量删除
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool BatchRemovePosts(List<int> id)
        {
            var resule = Conn.Execute("delete from Posts_Table where PostsID in @PostsID ", new { PostsID = id.ToList() });
            if (resule > 0)
            {
                return true;
            }
            return false;
        }
        /// <summary>
        /// 查询论坛内容
        /// </summary>
        /// <param name="NewsID"></param>
        /// <returns></returns>
        public IEnumerable<Posts> SerachPosts(List<int> id, DateTime? startTime, DateTime? endTime)
        {
            string sql;
            if (id == null && startTime != null)
            {
                sql = "select * from Posts_Table as P join User_Table as u on P.UserID = u.OwnerID where CreateTime Between @Time1 and @Time2";
            }
            else if (id != null && startTime != null)
            {
                sql = "select * from Posts_Table as P join User_Table as u on P.UserID = u.OwnerID where u.OwnerID in @OwnerID and CreateTime Between @Time1 and @Time2";
            }
            else if (id != null && startTime == null)
            {
                sql = "select * from Posts_Table as P join User_Table as u on P.UserID = u.OwnerID where u.OwnerID in @OwnerID";
            }
            else
            {
                sql = "select * from Posts_Table as P join User_Table as u on P.UserID = u.OwnerID";
            }
            return Conn.Query<Posts, User, Posts>(sql, (posts, user) => { posts.UserModel = user; return posts; }, new { OwnerID = id, Time1 = startTime, Time2 = endTime }, null, true, splitOn: "OwnerID");
        }
        /// <summary>
        /// 论坛类型统计
        /// </summary>
        /// <returns></returns>
        public IEnumerable<PostsType> SerachPostsType()
        {
            var resule = Conn.Query<PostsType>("select PostsLable, count(*) PostsLableCount from Posts_Table group by PostsLable");
            return resule;
        }
        /// <summary>
        /// 评论数加1
        /// </summary>
        /// <param name="PostsID"></param>
        /// <returns></returns>
        public bool AddNumberForReview(int PostsID)
        {
            var resule = Conn.Execute("update Posts_Table set CommentsNumber = CommentsNumber + 1 where PostsID=@PostsID; ", new { PostsID = PostsID });
            if (resule == 1)
            {
                return true;
            }
            return false;
        }
        /// <summary>
        /// 观看数+1
        /// </summary>
        /// <param name="PostsID"></param>
        /// <returns></returns>
        public bool AddNumberForWatch(int PostsID)
        {
            var resule = Conn.Execute("update Posts_Table set TimesWatched = TimesWatched + 1 where PostsID=@PostsID; ", new { PostsID = PostsID });
            if (resule == 1)
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// 帖子信息添加
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool PostsAdd(Posts model)
        {
            var resule = Conn.Execute("Insert into Posts_Table values(@PostsTitle,@PostsLable,@CreateTime,@TimesWatched,@CommentsNumber,@BriefContent,@Contents,@UserID)", model);
            if (resule == 1)
            {
                return true;
            }
            return false;
        }


    }
}
