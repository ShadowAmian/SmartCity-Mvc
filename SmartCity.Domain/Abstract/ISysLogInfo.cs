using SmartCity.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartCity.Domain.Abstract
{
    public interface ISysLogInfo
    {
        IEnumerable<SysLog> GetSysList();
    }
}
