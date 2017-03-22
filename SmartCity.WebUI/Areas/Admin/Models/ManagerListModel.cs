using SmartCity.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SmartCity.WebUI.Areas.Admin.Models
{
    public class ManagerListModel
    {

        public ManagerListModel()
        {
            MangerIteams = new List<Manager>();
        }
        public IList<Manager> MangerIteams { get; set; }
    }
}