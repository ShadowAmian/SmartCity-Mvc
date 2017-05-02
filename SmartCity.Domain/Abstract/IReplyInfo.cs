using SmartCity.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartCity.Domain.Abstract
{
    public interface IReplyInfo
    {
        /// <summary>
        ///获取回复表内容
        /// </summary>
        /// <returns></returns>
         IEnumerable<Reply> GetLatestReviews(int id);

        /// <summary>
        /// 回复评论
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        bool AddReply(Reply model);
    }
}
