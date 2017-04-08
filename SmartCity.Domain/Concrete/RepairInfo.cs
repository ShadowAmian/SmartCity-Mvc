using SmartCity.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dapper;
using System.Threading.Tasks;
using SmartCity.Domain.Abstract;

namespace SmartCity.Domain.Concrete
{
    /// <summary>
    /// 报修信息表
    /// </summary>
    public class RepairInfo : RepositoryContext,IRepairInfo
    {
        /// <summary>
        ///获取报修信息集合
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Repair> GetRepairInfoList()
        {
            return Conn.Query<Repair,User,Manager,Repair>("select r.RepairID,r.RepairName,r.RepairType,r.RepairContent,r.MaintenanceStatus,r.RepairTime,r.CreateTime,u.UserName,u.UserPhone,u.UserAddress,m.ManagerName from Repair_Table as r join User_Table as u on r.OwnerID=u.OwnerID  join Manager_Table as m on r.ManagerID=m.ManagerID", (repair,user,manager)=> { repair.UserInfo = user;repair.ManagerInfo = manager;return repair; },splitOn: "UserName,ManagerName");
        }
        /// <summary>
        /// 修改公告状态
        /// </summary>
        /// <param name="ID"></param>
        /// <param name="Status"></param>
        /// <returns></returns>
        public bool EditRepairStatu(int ID, int Status)
        {
            var resule = Conn.Execute("update Repair_Table set MaintenanceStatus=@MaintenanceStatus where RepairID=@RepairID", new { MaintenanceStatus = Status, RepairID = ID });
            if (resule == 1)
            {
                return true;
            }
            return false;

        }
    }
}
