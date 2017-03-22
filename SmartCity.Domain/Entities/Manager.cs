using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartCity.Domain.Entities
{
   public class Manager
    {
        public int ManagerID { get; set; }
        public string ManagerName { get; set; }
        public string ManagerAccount { get; set; }
        public string ManagerPassword { get; set; }
        public string ManagerPhone { get; set; }
        public string ManagerEmail { get; set; }
        public string ManagerType { get; set; }
        public int IsEnable { get; set; }
        public DateTime CreateTime { get; set; }
    }
}
