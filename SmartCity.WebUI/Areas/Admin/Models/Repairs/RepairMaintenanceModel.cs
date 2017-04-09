using SmartCity.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SmartCity.WebUI.Areas.Admin.Models.Repairs
{
    public class RepairMaintenanceModel
    {

        public RepairMaintenanceModel()
        {
          Iteams = new List<Manager>();
        }
        public IList<Manager> Iteams { get; set; }
        public int RepairID { get; set; }
    }
}