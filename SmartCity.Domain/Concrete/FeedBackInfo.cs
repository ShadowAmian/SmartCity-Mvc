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
    public class FeedBackInfo: RepositoryContext, IFeedBackInfo
    {
        /// <summary>
        ///获取意见反馈信息集合
        /// </summary>
        /// <returns></returns>
        public IEnumerable<FeedBack> GetFeedBackInfoList()
        {
            return Conn.Query<FeedBack, User, FeedBack>("select c.ComplaintID,c.ComplaintContent,c.CreateTime,u.OwnerID,u.UserName from Complaint_Table as c join User_Table as u on c.OwnerID=u .OwnerID ", (feedback, user) => { feedback.OwnerInfo = user; return feedback; }, splitOn: "OwnerID");
        }
        /// <summary>
        /// 删除反馈内容内容
        /// </summary>
        /// <param name="NewsID"></param>
        /// <returns></returns>
        public bool DeleteFeedBack(int FeedBackID)
        {
            var resule = Conn.Execute("delete from Complaint_Table where ComplaintID=@ComplaintID ", new { ComplaintID = FeedBackID });
            if (resule == 1)
            {
                return true;
            }
            return false;
        }
    }
}
