using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SmartCity.WebUI.Infrastructure.EnumData
{
    /// <summary>
    /// 维修状态
    /// </summary>
    public enum RepairStatus
    {
        已完成 = 0,
        未派单 = 1,
        未完成 = 2,
        未受理 = 3,
        进行中 = 4,
    }
}