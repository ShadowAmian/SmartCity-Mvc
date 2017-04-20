using SmartCity.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SmartCity.WebUI.Models
{
    public class HomePageModel
    { 
        /// <summary>
        /// 公告类
        /// </summary>
        public List<Notice> NewsItems { get; set; }
        /// <summary>
        /// 论坛类
        /// </summary>
        public List<Posts> PostsItems { get; set; }


    }
}