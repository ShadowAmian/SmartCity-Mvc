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
    }
}
