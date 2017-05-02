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
    /// 回复表
    /// </summary>
    public class ReplyInfo : RepositoryContext, IReplyInfo
    {
        /// <summary>
        ///获取回复表内容
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Reply> GetLatestReviews(int ReviewID)
        {
            var sqlstr = "select * from Reply_Table as r join User_Table as u on r.UserID=u.OwnerID where r.ReviewID=@ReviewID ";
            return Conn.Query<Reply, User, Reply>(sqlstr, (reply, user) => { reply.UserModel = user; return reply; }, new { ReviewID = ReviewID }, null, true, splitOn: "OwnerID");
        }
        /// <summary>
        /// 回复评论
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool AddReply(Reply model)
        {
            var resule = Conn.Execute("Insert into Reply_Table values(@ReplyContent,@CreateTime,@UserID,@ReviewID)", model);
            if (resule == 1)
            {
                return true;
            }
            return false;
        }
    }
}
