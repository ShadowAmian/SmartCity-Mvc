using SmartCity.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartCity.Domain.Abstract
{
    public interface INoticeInfo
    {
        /// <summary>
        ///获取用户信息集合
        /// </summary>
        /// <returns></returns>
         IEnumerable<Notice> GetNewsList();
    }
}
