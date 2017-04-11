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
    }
}
