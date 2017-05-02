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
    /// 评论表
    /// </summary>
    public class ReviewInfo : RepositoryContext, IReviewInfo
    {
        /// <summary>
        ///获取最新评论信息
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Review> GetLatestReviews()
        {
            return Conn.Query<Review, User, Posts, Review>("select top 5 * from Review_Table as r join User_Table as u on r.UserID=u.OwnerID  join Posts_Table as P on r.ForumID=P.PostsID order by ReviewID desc", (review, user, posts) => { review.UserModel = user; review.PostsModel = posts; return review; }, splitOn: "OwnerID,PostsID");
        }
        /// <summary>
        ///获取最新评论信息
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Review> GetLatestReviewsByID(int id)
        {
            return Conn.Query<Review, User, Posts, Review>("select * from Review_Table as r join User_Table as u on r.UserID=u.OwnerID  join Posts_Table as P on r.ForumID=P.PostsID where r.ForumID=@ForumID", (review, user, posts) => { review.UserModel = user; review.PostsModel = posts; return review; },new { ForumID=id },null,true, splitOn: "OwnerID,PostsID");
        }
        /// <summary>
        /// 添加新的评论
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool AddReview(Review model)
        {
            var resule = Conn.Execute("Insert into Review_Table values(@ReviewContent,@CreateTime,@UserID,@ForumID)", model);
            if (resule == 1)
            {
                return true;
            }
            return false;
        }
    }
}
