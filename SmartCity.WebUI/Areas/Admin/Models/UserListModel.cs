using SmartCity.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SmartCity.WebUI.Areas.Admin.Models
{
    public class UserListModel
    {
        public UserListModel()
        {
           UserIteams = new List<User>();
        }
        public IList<User> UserIteams { get; set; }
    }
}