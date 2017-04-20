using SmartCity.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SmartCity.WebUI.Models
{
    public class HomePageModel
    { 
        public List<Notice> NewsItems { get; set; }
    }
}