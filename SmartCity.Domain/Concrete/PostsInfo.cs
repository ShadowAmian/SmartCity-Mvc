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
        ///获取报修信息集合
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Posts> GetPostsInfoList()
        {
            return Conn.Query<Posts, User, Posts>("select * from Posts_Table as P join User_Table as u on P.UserID=u.OwnerID  ", (posts, user) => { posts.UserModel = user; return posts; }, splitOn: "OwnerID");
        }
        /// <summary>
        ///获取报修信息集合
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Posts> GetPostsInfoByID(int id)
        {
            return Conn.Query<Posts, User, Posts>("select * from Posts_Table as P join User_Table as u on P.UserID=u.OwnerID where PostsID=@PostsID ", (posts, user) => { posts.UserModel = user; return posts; }, new { PostsID = id }, null, true, splitOn: "OwnerID");
        }
        /// <summary>
        /// 获取热门报修信息集合
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Posts> GetHotPostsInfo()
        {
            return Conn.Query<Posts, User, Posts>("select top 5 * from Posts_Table as P join User_Table as u on P.UserID=u.OwnerID  order by TimesWatched desc;", (posts, user) => { posts.UserModel = user; return posts; }, splitOn: "OwnerID");
        }
        /// <summary>
        /// 删除在线交流内容
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
            if (resule == 1)
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
    }
}
