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


    }
}
