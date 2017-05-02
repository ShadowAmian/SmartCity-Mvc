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
    /// 日志模块
    /// </summary>
    public class SysLogInfo : RepositoryContext, ISysLogInfo
    {
        /// <summary>
        ///获取通告资讯集合
        /// </summary>
        /// <returns></returns>
        public IEnumerable<SysLog> GetSysList()
        {
            return Conn.Query<SysLog>("select * from SysLog_Table");
        }

    }
}
