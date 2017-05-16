using SmartCity.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartCity.Domain.Abstract
{
    public interface IManagerInfo
    {
        /// <summary>
        /// 管理员登录
        /// </summary>
        /// <param name="Account"></param>
        /// <param name="Password"></param>
        /// <returns></returns>
        bool ManagerLogin(string Account, string Password);
        /// <summary>
        ///获取用户信息By 用户Account
        /// </summary>
        /// <param name="Account"></param>
        /// <returns></returns>
        IEnumerable<Manager> GetManagerInfo(string Account);
        /// <summary>
        ///获取用户信息集合
        /// </summary>
        /// <returns></returns>
        IEnumerable<Manager> GetManagerInfoList();
        /// <summary>
        /// 用户是否启用修改
        /// </summary>
        /// <param name="ID"></param>
        /// <param name="isEnable"></param>
        /// <returns></returns>
        bool UpdateManagerEnable(int ID, int isEnable);
        /// <summary>
        /// 管理员添加
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        bool AddManager(Manager model);
        /// <summary>
        /// 管理员是否存在
        /// </summary>
        /// <returns></returns>
        bool ManagerIsExist(string Account);
        /// <summary>
        /// 管理员修改
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        bool EditManager(Manager model);
        /// <summary>
        ///获取用户信息By 用户ID
        /// </summary>
        /// <param name="Account"></param>
        /// <returns></returns>
        IEnumerable<Manager> GetManagerInfoByID(int ManagerID);
        /// <summary>
        /// 管理员密码修改
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
         bool EditManagerInfoPassword(Manager model);
        /// <summary>
        /// 管理员删除
        /// </summary>
        /// <param name="ManagerID"></param>
        /// <returns></returns>
        bool DeleteManager(int ManagerID);
        /// <summary>
        /// 根据名字搜索管理员
        /// </summary>
        /// <param name="ManagerName"></param>
        /// <returns></returns>
        IEnumerable<Manager> SearchManager(string ManagerName);
        /// <summary>
        /// 密码修改
        /// </summary>
        /// <param name="ManagerName"></param>
        /// <returns></returns>
        bool EditManagerPassword(int managerID, string oldpassword, string newpassword);
        /// <summary>
        /// 维修人员搜索
        /// </summary>
        /// <param name="ManagerName"></param>
        /// <returns></returns>
        IEnumerable<Manager> SearchMaintenance();
        /// <summary>
        /// 批量删除
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
         bool BatchRemoveManager(List<int> id);
    }
}
