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
        ///获取所以用户评论信息
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Review> GetLatestReviewss()
        {
            return Conn.Query<Review, User, Posts, Review>("select * from Review_Table as r join User_Table as u on r.UserID=u.OwnerID  join Posts_Table as P on r.ForumID=P.PostsID", (review, user, posts) => { review.UserModel = user; review.PostsModel = posts; return review; },splitOn: "OwnerID,PostsID");
        }
        /// <summary>
        /// 删除反馈内容内容
        /// </summary>
        /// <param name="NewsID"></param>
        /// <returns></returns>
        public bool DeleteReviews(int ReviewsID)
        {
            var resule = Conn.Execute("delete from Review_Table where ReviewID=@ReviewID ", new { ReviewID = ReviewsID });
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
        public bool BatchRemoveReviews(List<int> id)
        {
            var resule = Conn.Execute("delete from Review_Table where ReviewID in @ReviewID ", new { ReviewID = id.ToList() });
            if (resule > 0)
            {
                return true;
            }
            return false;
        }
        /// <summary>
        /// 查询评论内容
        /// </summary>
        /// <param name="NewsID"></param>
        /// <returns></returns>
        public IEnumerable<Review> SerachReview(List<int> id, DateTime? startTime, DateTime? endTime)
        {
            string sql;
            if (id == null && startTime != null)
            {
                sql = "select * from Review_Table as r join User_Table as u on r.UserID=u.OwnerID  join Posts_Table as P on r.ForumID=P.PostsID where CreateTime Between @Time1 and @Time2";
            }
            else if (id != null && startTime != null)
            {
                sql = "select * from Review_Table as r join User_Table as u on r.UserID=u.OwnerID  join Posts_Table as P on r.ForumID=P.PostsID where u.OwnerID in @OwnerID and CreateTime Between @Time1 and @Time2";
            }
            else if (id != null && startTime == null)
            {
                sql = "select * from Review_Table as r join User_Table as u on r.UserID=u.OwnerID  join Posts_Table as P on r.ForumID=P.PostsID where u.OwnerID in @OwnerID";
            }
            else
            {
                sql = "select * from Review_Table as r join User_Table as u on r.UserID=u.OwnerID  join Posts_Table as P on r.ForumID=P.PostsID";
            }
            return Conn.Query<Review, User, Posts, Review>(sql, (review, user, posts) => { review.UserModel = user; review.PostsModel = posts; return review; }, new { OwnerID = id, Time1 = startTime, Time2 = endTime }, null, true, splitOn: "OwnerID,PostsID");
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
