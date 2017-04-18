using SmartCity.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartCity.Domain.Abstract
{
    public interface IRepairInfo
    {
        /// <summary>
        ///获取报修信息集合
        /// </summary>
        /// <returns></returns>
        IEnumerable<Repair> GetRepairInfoList();
        /// <summary>
        /// 修改公告状态
        /// </summary>
        /// <param name="ID"></param>
        /// <param name="Status"></param>
        /// <returns></returns>
         bool EditRepairStatu(int ID, int Status);
        /// <summary>
        /// 修改报修信息
        /// </summary>
        /// <param name="ID"></param>
        /// <param name="Status"></param>
        /// <returns></returns>
        bool EditRepairToManager(int ManagerID, int id, DateTime time);

        /// <summary>
        /// 查询报修内容
        /// </summary>
        /// <param name="NewsID"></param>
        /// <returns></returns>
        IEnumerable<Repair> SerachRepairByNewsName(string RepairName, DateTime? startTime, DateTime? endTime);
        /// <summary>
        /// 批量删除
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
         bool BatchRemoveRepairInfo(List<int> id);
        /// <summary>
        /// 维修类型统计
        /// </summary>
        /// <returns></returns>
        IEnumerable<RepairTypes> SerachRepairType();
    }
}
