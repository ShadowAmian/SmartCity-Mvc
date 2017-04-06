using SmartCity.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SmartCity.WebUI.Areas.Admin.Models.SysLogs
{
    public class SysLogModel
    {
        public SysLogModel()
        {
            SysLogIteams = new List<SysLog>();
        }
        public IList<SysLog> SysLogIteams { get; set; }
    }
}