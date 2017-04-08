using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SmartCity.Domain.Entities;

namespace SmartCity.WebUI.Areas.Admin.Models.Repairs
{
    public class RepairListModel
    {

        public RepairListModel()
        {
            RepairIteams = new List<Repair>();
        }
        public IList<Repair> RepairIteams { get; set; }

    }
}