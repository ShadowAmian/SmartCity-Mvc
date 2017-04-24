using SmartCity.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SmartCity.WebUI.Areas.Admin.Models.Postss
{
    /// <summary>
    /// 帖子Model集合
    /// 
    /// </summary>
    public class PostsListModel
    {
        public PostsListModel()
        {
            PostsIteams = new List<Posts>();
        }
        public IList<Posts> PostsIteams { get; set; }
    }
}