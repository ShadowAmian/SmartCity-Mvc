using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using SmartCity.Domain.Abstract;
using SmartCity.Domain.Entities;

namespace SmartCity.Domain.Concrete
{
    /// <summary>
    /// 用户信息管理
    /// </summary>
    public partial class UserInfo: RepositoryContext, IUserInfo
    {
        /// <summary>
        /// 用户登录
        /// </summary>
        /// <param name="Account"></param>
        /// <param name="Password"></param>
        /// <returns></returns>
        public bool UserLogin(string Account, string Password)
        {
            var resule = Conn.Query<User>("select * from User_Table where UserAccount=@UserAccount and UserPassword=@UserPassword", new { UserAccount = Account, UserPassword = Password });
            if (resule.Count<User>() != 0)
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
        public IEnumerable<User> GetUserInfo(string Account)
        {
            return Conn.Query<User>("select * from User_Table where UserAccount=@UserAccount", new { UserAccount = Account });
        }
        /// <summary>
        ///获取用户信息By 用户ID
        /// </summary>
        /// <param name="Account"></param>
        /// <returns></returns>
        public IEnumerable<User> GetUserInfoByID(int UserID)
        {
            return Conn.Query<User>("select * from User_Table where OwnerID=@OwnerID", new { OwnerID = UserID });
        }
        /// <summary>
        ///获取用户信息集合
        /// </summary>
        /// <returns></returns>
        public IEnumerable<User> GetUserInfoList()
        {
            return Conn.Query<User>("select * from User_Table ");
        }
        /// <summary>
        /// 用户添加
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool AddUser(User model)
        {
            var resule = Conn.Execute("Insert into User_Table values(@UserAccount,@UserPassword,@UserName,@UserSex,@UserPhone,@UserEmail,@UserAddress,@CreateTime)", model);
            if (resule == 1)
            {
                return true;
            }
            return false;
        }
        /// <summary>
        /// 用户是否存在
        /// </summary>
        /// <returns></returns>
        public bool UserIsExist(string Account)
        {
            var resule = Conn.Query<int>("Select Count(*) from User_Table where UserAccount=@UserAccount", new { UserAccount = Account });
            if (resule.First() > 0)
            {
                return true;
            }
            return false;
        }
        /// <summary>
        ///  用户修改
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool EditUser(User model)
        {
            var resule = Conn.Execute("update User_Table set UserName=@UserName,UserPhone=@UserPhone,UserEmail=@UserEmail,UserAddress=@UserAddress where OwnerID=@OwnerID", model);
            if (resule == 1)
            {
                return true;
            }
            return false;
        }
        /// <summary>
        ///  用户密码修改
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool EditUserInfoPassWord(User model)
        {
            var resule = Conn.Execute("update User_Table set UserPassword=@UserPassword,UserName=@UserName,UserPhone=@UserPhone,UserEmail=@UserEmail,UserAddress=@UserAddress where OwnerID=@OwnerID", model);
            if (resule == 1)
            {
                return true;
            }
            return false;
        }
        /// <summary>
        /// 管理员删除
        /// </summary>
        /// <param name="UserID"></param>
        /// <returns></returns>
        public bool DeleteUser(int OwnerID)
        {
            var resule = Conn.Execute("delete from User_Table where OwnerID=@OwnerID ", new { OwnerID = OwnerID });
            if (resule == 1)
            {
                return true;
            }
            return false;
        }
        /// <summary>
        /// 修改密码
        /// </summary>
        /// <param name="UserID"></param>
        /// <returns></returns>
        public bool EditUserPassword(int OwnerID, string OldUserPassword, string NewUserPassword)
        {
            var resule = Conn.Execute("update User_Table set UserPassword=@NewUserPassWord where OwnerID=@OwnerID and UserPassword=@OldUserPassword ", new { OwnerID = OwnerID, OldUserPassword = OldUserPassword, NewUserPassWord = NewUserPassword });
            if (resule == 1)
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// 用户搜索
        /// </summary>
        /// <param name="UserName"></param>
        /// <returns></returns>
        public IEnumerable<User> SearchUserInfo(string UserName)
        {
            if (string.IsNullOrEmpty(UserName))
            {
                return Conn.Query<User>("select * from User_Table ");
            }
            return Conn.Query<User>("select * from User_Table where UserName like @UserName ", new { UserName = "%" + UserName + "%" });
        }
        /// <summary>
        /// 用户搜索ID
        /// </summary>
        /// <param name="UserName"></param>
        /// <returns></returns>
        public IEnumerable<int> SearchUserInfoINUserID(string UserName)
        {
            if (string.IsNullOrEmpty(UserName))
            {
                return Conn.Query<int>("select OwnerID from User_Table ");
            }
            return Conn.Query<int>("select OwnerID from User_Table where UserName like @UserName ", new { UserName = "%" + UserName + "%" });
        }
        /// <summary>
        /// 批量删除
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool BatchRemoveUserInfo(List<int> id)
        {
            var resule = Conn.Execute("delete from User_Table where OwnerID in @OwnerID ", new { OwnerID = id.ToList() });
            if (resule > 0)
            {
                return true;
            }
            return false;
        }
    }
 
}
