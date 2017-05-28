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
        ///获取系统日志集合
        /// </summary>
        /// <returns></returns>
        public IEnumerable<SysLog> GetSysList()
        {
            return Conn.Query<SysLog>("select * from SysLog_Table");
        }
        /// <summary>
        /// 查询日志内容By时间
        /// </summary>
        /// <param name="NewsID"></param>
        /// <returns></returns>
        public IEnumerable<SysLog> SerachSysLog( DateTime? startTime, DateTime? endTime)
        {
            string sql;
            if (startTime != null)
            {
                sql = "select * from SysLog_Table where Datas>=@Time1 and Datas<=@Time2 ";
            }
            else
            {
                sql = "select * from SysLog_Table";
            }
            return Conn.Query<SysLog>(sql,new { Time1=startTime,Time2=endTime });
        }


    }
}
