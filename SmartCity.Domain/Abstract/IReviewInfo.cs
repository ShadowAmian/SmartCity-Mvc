using SmartCity.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartCity.Domain.Abstract
{
    public interface IReviewInfo
    {
        /// <summary>
        ///获取最新评论信息
        /// </summary>
        /// <returns></returns>
        IEnumerable<Review> GetLatestReviews();
        /// <summary>
        ///获取最新评论信息
        /// </summary>
        /// <returns></returns>
        IEnumerable<Review> GetLatestReviewsByID(int id);

        /// <summary>
        /// 添加新的评论
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        bool AddReview(Review model);
        /// <summary>
        ///获取所以用户评论信息
        /// </summary>
        /// <returns></returns>
         IEnumerable<Review> GetLatestReviewss();
        /// <summary>
        /// 删除反馈内容内容
        /// </summary>
        /// <param name="NewsID"></param>
        /// <returns></returns>
         bool DeleteReviews(int ReviewsID);
        /// <summary>
        /// 批量删除
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
         bool BatchRemoveReviews(List<int> id);
        /// <summary>
        /// 查询评论内容
        /// </summary>
        /// <param name="NewsID"></param>
        /// <returns></returns>
        IEnumerable<Review> SerachReview(List<int> id, DateTime? startTime, DateTime? endTime);
    }
}
