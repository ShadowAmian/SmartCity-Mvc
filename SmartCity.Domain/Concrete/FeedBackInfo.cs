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
        /// <summary>
        /// 批量删除
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool BatchRemoveFeedBack(List<int> id)
        {
            var resule = Conn.Execute("delete from Complaint_Table where ComplaintID in @ComplaintID ", new { ComplaintID = id.ToList() });
            if (resule >0)
            {
                return true;
            }
            return false;
        }
        /// <summary>
        /// 查询公告内容
        /// </summary>
        /// <param name="NewsID"></param>
        /// <returns></returns>
        public IEnumerable<FeedBack> SerachFeedBack(List<int> id, DateTime? startTime, DateTime? endTime)
        {
            string sql;
            if (id==null && startTime != null)
            {
                sql = "select c.ComplaintID,c.ComplaintContent,c.CreateTime,u.OwnerID,u.UserName from Complaint_Table as c join User_Table as u on c.OwnerID=u .OwnerID where CreateTime Between @Time1 and @Time2";
            }
            else if (id != null && startTime != null)
            {
                sql = "select c.ComplaintID,c.ComplaintContent,c.CreateTime,u.OwnerID,u.UserName from Complaint_Table as c join User_Table as u on c.OwnerID=u.OwnerID where u.OwnerID in @OwnerID and CreateTime Between @Time1 and @Time2";
            }
            else if (id != null && startTime == null)
            {
                sql = "select c.ComplaintID,c.ComplaintContent,c.CreateTime,u.OwnerID,u.UserName from Complaint_Table as c join User_Table as u on c.OwnerID=u.OwnerID where u.OwnerID in @OwnerID";
            }
            else
            {
                sql = "select c.ComplaintID,c.ComplaintContent,c.CreateTime,u.OwnerID,u.UserName from Complaint_Table as c join User_Table as u on c.OwnerID=u .OwnerID";
            }
            return Conn.Query<FeedBack, User, FeedBack>(sql, (feedback, user) => { feedback.OwnerInfo = user; return feedback; }, new { OwnerID = id, Time1 = startTime, Time2 = endTime },null,true, splitOn: "OwnerID");
        }

        /// <summary>
        /// 反馈信息添加
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool FeedBackAdd(FeedBack model)
        {
            var resule = Conn.Execute("Insert into Complaint_Table (OwnerID,ComplaintContent,CreateTime) values(@OwnerID,@ComplaintContent,@CreateTime)", model);
            if (resule == 1)
            {
                return true;
            }
            return false;
        }
    }
}
