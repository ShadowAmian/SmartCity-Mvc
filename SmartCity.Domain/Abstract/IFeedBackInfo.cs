using SmartCity.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartCity.Domain.Abstract
{
   public interface IFeedBackInfo
    {
        /// <summary>
        ///获取意见反馈信息集合
        /// </summary>
        /// <returns></returns>
        IEnumerable<FeedBack> GetFeedBackInfoList();
        /// <summary>
        /// 删除反馈内容内容
        /// </summary>
        /// <param name="NewsID"></param>
        /// <returns></returns>
        bool DeleteFeedBack(int FeedBackID);
        /// <summary>
        /// 批量删除
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
         bool BatchRemoveFeedBack(List<int> id);
        /// <summary>
        /// 查询公告内容
        /// </summary>
        /// <param name="NewsID"></param>
        /// <returns></returns>
        IEnumerable<FeedBack> SerachFeedBack(List<int> id, DateTime? startTime, DateTime? endTime);
        /// <summary>
        /// 反馈信息添加
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
         bool FeedBackAdd(FeedBack model);
    }
}
