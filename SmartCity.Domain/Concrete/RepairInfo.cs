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
    public class RepairInfo : RepositoryContext, IRepairInfo
    {
        /// <summary>
        ///获取报修信息集合
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Repair> GetRepairInfoList()
        {
            return Conn.Query<Repair, User, Manager, Repair>("select r.RepairID,r.RepairName,r.RepairType,r.RepairContent,r.MaintenanceStatus,r.RepairTime,r.CreateTime,u.UserName,u.UserPhone,u.UserAddress,m.ManagerName from Repair_Table as r join User_Table as u on r.OwnerID=u.OwnerID  join Manager_Table as m on r.ManagerID=m.ManagerID", (repair, user, manager) => { repair.UserInfo = user; repair.ManagerInfo = manager; return repair; }, splitOn: "UserName,ManagerName");
        }
        /// <summary>
        /// 修改报修状态
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
        /// <summary>
        /// 修改报修信息
        /// </summary>
        /// <param name="ID"></param>
        /// <param name="Status"></param>
        /// <returns></returns>
        public bool EditRepairToManager(int ManagerID, int id, DateTime time)
        {
            var resule = Conn.Execute("update Repair_Table set MaintenanceStatus=@MaintenanceStatus,ManagerID=@ManagerID,RepairTime=@RepairTime where RepairID=@RepairID", new { MaintenanceStatus = 4, ManagerID = ManagerID, RepairTime = time, RepairID = id });
            if (resule == 1)
            {
                return true;
            }
            return false;

        }
        /// <summary>
        /// 查询报修内容
        /// </summary>
        /// <param name="NewsID"></param>
        /// <returns></returns>
        public IEnumerable<Repair> SerachRepairByNewsName(string RepairName, DateTime? startTime, DateTime? endTime)
        {
            string sql;
            if (string.IsNullOrEmpty(RepairName) && startTime != null)
            {
                sql = "select r.RepairID,r.RepairName,r.RepairType,r.RepairContent,r.MaintenanceStatus,r.RepairTime,r.CreateTime,u.UserName,u.UserPhone,u.UserAddress,m.ManagerName from Repair_Table as r join User_Table as u on r.OwnerID = u.OwnerID  join Manager_Table as m on r.ManagerID = m.ManagerID where r.CreateTime Between @Time1 and @Time2";
            }
            else if (!string.IsNullOrEmpty(RepairName) && startTime != null)
            {
                sql = "select r.RepairID,r.RepairName,r.RepairType,r.RepairContent,r.MaintenanceStatus,r.RepairTime,r.CreateTime,u.UserName,u.UserPhone,u.UserAddress,m.ManagerName from Repair_Table as r join User_Table as u on r.OwnerID=u.OwnerID  join Manager_Table as m on r.ManagerID=m.ManagerID where r.RepairName=@RepairName and r.CreateTime Between @Time1 and @Time2";
            }
            else if (!string.IsNullOrEmpty(RepairName) && startTime == null)
            {
                sql = "select r.RepairID,r.RepairName,r.RepairType,r.RepairContent,r.MaintenanceStatus,r.RepairTime,r.CreateTime,u.UserName,u.UserPhone,u.UserAddress,m.ManagerName from Repair_Table as r join User_Table as u on r.OwnerID=u.OwnerID  join Manager_Table as m on r.ManagerID=m.ManagerID where r.RepairName=@RepairName";
            }
            else
            {
                sql = "select r.RepairID,r.RepairName,r.RepairType,r.RepairContent,r.MaintenanceStatus,r.RepairTime,r.CreateTime,u.UserName,u.UserPhone,u.UserAddress,m.ManagerName from Repair_Table as r join User_Table as u on r.OwnerID=u.OwnerID  join Manager_Table as m on r.ManagerID=m.ManagerID";
            }
            return Conn.Query<Repair, User, Manager, Repair>(sql, (repair, user, manager) => { repair.UserInfo = user; repair.ManagerInfo = manager; return repair; }, new { RepairName = RepairName, Time1 = startTime, Time2 = endTime }, null, true, splitOn: "UserName,ManagerName");
        }
        /// <summary>
        /// 批量删除
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool BatchRemoveRepairInfo(List<int> id)
        {
            var resule = Conn.Execute("delete from Repair_Table where RepairID in @RepairID ", new { RepairID = id.ToList() });
            if (resule == 1)
            {
                return true;
            }
            return false;
        }
        /// <summary>
        /// 维修类型统计
        /// </summary>
        /// <returns></returns>
        public IEnumerable<RepairTypes> SerachRepairType()
        {
            var resule = Conn.Query<RepairTypes>("select RepairType, count(*) MaintenanceStatusNumber  from Repair_Table group by RepairType");
            return resule;
        }
    }
}
