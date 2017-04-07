using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using SmartCity.Domain.Entities;
using SmartCity.Domain.Abstract;

namespace SmartCity.Domain.Concrete
{
    /// <summary>
    /// 管理员操作类
    /// </summary>
    public class ManagerInfo : RepositoryContext, IManagerInfo
    {
        /// <summary>
        /// 管理员登录
        /// </summary>
        /// <param name="Account"></param>
        /// <param name="Password"></param>
        /// <returns></returns>
        public bool ManagerLogin(string Account, string Password)
        {
            var resule = Conn.Query<Manager>("select * from Manager_Table where ManagerAccount=@ManagerAccount and ManagerPassword=@ManagerPassword", new { ManagerAccount = Account, ManagerPassword = Password });
            if (resule.Count<Manager>() != 0)
            {
                return true;
            }
            return false;
        }
        /// <summary>
        ///获取用户信息By 用户Account
        /// </summary>
        /// <param name="Account"></param>
        /// <returns></returns>
        public IEnumerable<Manager> GetManagerInfo(string Account)
        {
            return Conn.Query<Manager>("select * from Manager_Table where ManagerAccount=@ManagerAccount", new { ManagerAccount = Account});
        }
        /// <summary>
        ///获取用户信息By 用户ID
        /// </summary>
        /// <param name="Account"></param>
        /// <returns></returns>
        public IEnumerable<Manager> GetManagerInfoByID(int ManagerID)
        {
            return Conn.Query<Manager>("select * from Manager_Table where ManagerID=@ManagerID", new { ManagerID = ManagerID });
        }
        /// <summary>
        ///获取用户信息集合
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Manager> GetManagerInfoList()
        {
            return Conn.Query<Manager>("select * from Manager_Table ");
        }
        /// <summary>
        /// 用户是否启用修改
        /// </summary>
        /// <param name="ID"></param>
        /// <param name="isEnable"></param>
        /// <returns></returns>
        public bool UpdateManagerEnable(int ID, int isEnable)
        {
            var resule = Conn.Execute("update Manager_Table set IsEnable=@IsEnable where ManagerID=@ManagerID", new { IsEnable = isEnable, ManagerID = ID });
            if (resule==1)
            {
                return true;
            }
            return false;
        }
        /// <summary>
        /// 管理员添加
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool AddManager(Manager model)
        {
            var resule = Conn.Execute("Insert into Manager_Table values(@ManagerName,@ManagerAccount,@ManagerPassword,@ManagerPhone,@ManagerEmail,@ManagerType,@IsEnable,@CreateTime)", model);
            if (resule == 1)
            {
                return true;
            }
            return false;
        }
        /// <summary>
        /// 管理员是否存在
        /// </summary>
        /// <returns></returns>
        public bool ManagerIsExist(string Account)
        {
            var resule = Conn.Execute("Select Count(*) from Manager_Table where ManagerAccount=@ManagerAccount", new { ManagerAccount= Account });
            if (resule>0)
            {
                return true;
            }
            return false;
        }
        /// <summary>
        /// 管理员修改
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool EditManager(Manager model)
        {
            var resule = Conn.Execute("update Manager_Table set ManagerName=@ManagerName,ManagerPhone=@ManagerPhone,ManagerEmail=@ManagerEmail where ManagerID=@ManagerID", model);
            if (resule == 1)
            {
                return true;
            }
            return false;
        }
        /// <summary>
        /// 管理员删除
        /// </summary>
        /// <param name="ManagerID"></param>
        /// <returns></returns>
        public bool DeleteManager(int ManagerID)
        {
            var resule = Conn.Execute("delete from Manager_Table where ManagerID=@ManagerID ", new { ManagerID=ManagerID});
            if (resule == 1)
            {
                return true;
            }
            return false;
        }
        /// <summary>
        /// 管理员搜索
        /// </summary>
        /// <param name="ManagerName"></param>
        /// <returns></returns>
        public IEnumerable<Manager> SearchManager(string ManagerName)
        {
            return Conn.Query<Manager>("select * from Manager_Table where ManagerName like @ManagerName ", new { ManagerName = "%"+ManagerName +"%" });
        }
        /// <summary>
        /// 密码修改
        /// </summary>
        /// <param name="ManagerName"></param>
        /// <returns></returns>
        public IEnumerable<Manager> SearchManager(string ManagerName)
        {
            return Conn.Query<Manager>("select * from Manager_Table where ManagerName like @ManagerName ", new { ManagerName = "%" + ManagerName + "%" });
        }
    }
}
