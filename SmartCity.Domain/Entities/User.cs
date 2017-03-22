using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartCity.Domain.Entities
{
    /// <summary>
    /// 用户信息字段
    /// </summary>
    public  class User
    {
        /// <summary>
        /// 用户ID
        /// </summary>
        public int OwnerID { get; set; }
        /// <summary>
        /// 用户账户
        /// </summary>
        public string UserAccount { get; set; }
        /// <summary>
        /// 用户密码
        /// </summary>
        public string UserPassword { get; set; }
        /// <summary>
        /// 用户名字
        /// </summary>
        public string UserName { get; set;}
        /// <summary>
        /// 用户电话
        /// </summary>
        public int UserSex { get; set;}
        /// <summary>
        /// 用户电话
        /// </summary>
        public string UserPhone { get; set; }
        /// <summary>
        /// 用户邮箱
        /// </summary>
        public string UserEmail { get; set; }
        /// <summary>
        /// 用户地址
        /// </summary>
        public string UserAddress { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime { get; set; }


    }
}
