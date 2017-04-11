using SmartCity.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SmartCity.WebUI.Areas.Admin.Models.FeedBacks
{
    public class FeedBackListModel
    {
        public FeedBackListModel()
        {
            FeedBackIteams = new List<FeedBack>();
        }
        public IList<FeedBack> FeedBackIteams { get; set; }
    }
}