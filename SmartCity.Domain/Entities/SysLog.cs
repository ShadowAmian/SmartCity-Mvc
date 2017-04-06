using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartCity.Domain.Entities
{
    /// <summary>
    /// 系统日志
    /// </summary>
    public class SysLog
    {
        /// <summary>
        /// 系统编号
        /// </summary>
        public int ID { get; set; }
        /// <summary>
        /// 时间
        /// </summary>
        public DateTime Datas { get; set; }
        /// <summary>
        /// 等级
        /// </summary>
        public string Levels { get; set; }
        /// <summary>
        /// 等级
        /// </summary>
        public string Logger { get; set; }
        public string ClientUser { get; set; }
        public string ClientIP { get; set; }
        public string RequestUrl { get; set; }
        public string Action { get; set; }
        public string Message { get; set; }
        public string Exception { get; set; }

    }
}
