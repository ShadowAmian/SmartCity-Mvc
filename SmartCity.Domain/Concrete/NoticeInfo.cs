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
    /// 通告资讯管理类
    /// </summary>
    public class NoticeInfo: RepositoryContext,INoticeInfo
    {
        /// <summary>
        ///获取用户信息集合
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Notice> GetNewsList()
        {
            return Conn.Query<Notice>("select * from News_Table");
        }
    }
}
