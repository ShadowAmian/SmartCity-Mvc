

using SmartCity.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartCity.Domain.Abstract
{
    /// <summary>
    /// 用户数据管理接口
    /// Add 2017/2/5 By zhangmian
    /// </summary>
    public partial interface IUserInfo
    {
        /// <summary>
        /// 用户登录
        /// </summary>
        /// <param name="Account"></param>
        /// <param name="Password"></param>
        /// <returns></returns>
        bool UserLogin(string Account, string Password);
        /// <summary>
        ///获取用户信息By 用户Account
        /// </summary>
        /// <param name="Account"></param>
        /// <returns></returns>
        IEnumerable<User> GetUserInfo(string Account);
        /// <summary>
        ///获取用户信息By 用户ID
        /// </summary>
        /// <param name="Account"></param>
        /// <returns></returns>
        IEnumerable<User> GetUserInfoByID(int UserID);
        /// <summary>
        ///获取用户信息集合
        /// </summary>
        /// <returns></returns>
        IEnumerable<User> GetUserInfoList();
        /// <summary>
        /// 用户添加
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
         bool AddUser(User model);
        /// <summary>
        /// 用户是否存在
        /// </summary>
        /// <returns></returns>
        bool UserIsExist(string Account);
        /// <summary>
        ///  用户修改
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
         bool EditUser(User model);
        /// <summary>
        /// 用户删除
        /// </summary>
        /// <param name="UserID"></param>
        /// <returns></returns>
        bool DeleteUser(int UserID);
        /// <summary>
        /// 用户搜索
        /// </summary>
        /// <param name="ManagerName"></param>
        /// <returns></returns>
        IEnumerable<User> SearchUserInfo(string UserName);
        /// <summary>
        /// 批量删除
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        bool BatchRemoveUserInfo(List<int> id);
    }
}
