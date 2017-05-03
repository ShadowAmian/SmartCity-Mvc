using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartCity.Domain.Entities
{
    public class ProductInfo
    {
        /// <summary>
        /// 工件编号
        /// </summary>
        public long ProductID { get; set; }
        /// <summary>
        /// 订单编号
        /// </summary>
        public string OrderID { get; set; }
        /// <summary>
        /// 批次号
        /// </summary>
        public string BatchID { get; set; }
        /// <summary>
        /// 炉号
        /// </summary>
        public string HeadID { get; set; }
        /// <summary>
        /// 班次
        /// </summary>
        public string ClasseID { get; set; }
        /// <summary>
        /// 上线时间
        /// </summary>
        public DateTime StartTime { get; set; }
        /// <summary>
        /// 下线时间
        /// </summary>
        public DateTime EndTime { get; set; }
        /// <summary>
        /// 持续时间
        /// </summary>
        public long Duration { get; set; }
        /// <summary>
        /// 工作状态
        /// </summary>
        public int ProState { get; set; }
    }
}
