using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartCity.Domain.Entities
{
    /// <summary>
    /// 报修表
    /// </summary>
    public class Repair
    {
        /// <summary>
        /// 报修编号
        /// </summary>
        public int RepairID { get; set; }
        /// <summary>
        /// 报修主题
        /// </summary>
        public string RepairName { get; set; }
        /// <summary>
        /// 报修类型
        /// </summary>
        public int RepairType { get; set; }
        /// <summary>
        /// 报修内容
        /// </summary>
        public string RepairContent { get; set; }
        /// <summary>
        /// 报修人信息
        /// </summary>
        public User UserInfo { get; set; }
        public long OwnerID { get; set; }
        /// <summary>
        /// 维修人信息
        /// </summary>
        public Manager ManagerInfo { get; set; }
        /// <summary>
        /// 报修状态
        /// </summary>
        public int MaintenanceStatus { get; set; }
        /// <summary>
        /// 报修时间
        /// </summary>
        public DateTime RepairTime { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime { get; set; }
    }
}
