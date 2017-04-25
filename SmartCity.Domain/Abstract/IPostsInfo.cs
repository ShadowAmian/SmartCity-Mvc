using SmartCity.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartCity.Domain.Abstract
{
     public interface IPostsInfo
    {
        /// <summary>
        ///获取报修信息集合
        /// </summary>
        /// <returns></returns>
         IEnumerable<Posts> GetPostsInfoList();
        /// <summary>
        /// 获取热门报修信息集合
        /// </summary>
        /// <returns></returns>
         IEnumerable<Posts> GetHotPostsInfo();
        /// <summary>
        /// 批量删除
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
         bool BatchRemovePosts(List<int> id);
        /// <summary>
        /// 删除在线交流内容
        /// </summary>
        /// <param name="NewsID"></param>
        /// <returns></returns>
        bool DeletePosts(int PostsID);
        /// <summary>
        /// 查询论坛内容
        /// </summary>
        /// <param name="NewsID"></param>
        /// <returns></returns>
        IEnumerable<Posts> SerachPosts(List<int> id, DateTime? startTime, DateTime? endTime);
        /// <summary>
        ///获取报修信息集合ByID
        /// </summary>
        /// <returns></returns>
        IEnumerable<Posts> GetPostsInfoByID(int id);
        /// <summary>
        /// 论坛类型统计
        /// </summary>
        /// <returns></returns>
        IEnumerable<PostsType> SerachPostsType();
    }
}
