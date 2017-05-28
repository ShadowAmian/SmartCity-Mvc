using SmartCity.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartCity.Domain.Abstract
{
    public interface ISysLogInfo
    {
        IEnumerable<SysLog> GetSysList();
        /// <summary>
        /// 查询日志内容By时间
        /// </summary>
        /// <param name="NewsID"></param>
        /// <returns></returns>
         IEnumerable<SysLog> SerachSysLog(DateTime? startTime, DateTime? endTime);
    }
}
