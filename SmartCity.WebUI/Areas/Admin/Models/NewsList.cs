using SmartCity.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SmartCity.WebUI.Areas.Admin.Models
{
    public class NewsList
    {

        public NewsList()
        {
            NewsIteams = new List<Notice>();
        }
        public IList<Notice> NewsIteams { get; set; }
    }
}