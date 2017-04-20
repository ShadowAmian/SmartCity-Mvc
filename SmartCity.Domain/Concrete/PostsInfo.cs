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
    public class PostsInfo : RepositoryContext,IPostsInfo
    {
        /// <summary>
        ///获取报修信息集合
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Posts> GetPostsInfoList()
        {
            return Conn.Query<Posts, User, Posts>("select * from Posts_Table as P join User_Table as u on P.UserID=u.OwnerID  ", (posts, user) => { posts.UserModel = user; return posts; }, splitOn: "OwnerID");
        }
    }
}
