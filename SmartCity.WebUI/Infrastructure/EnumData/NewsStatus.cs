using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SmartCity.WebUI.Infrastructure.EnumData
{
    public enum NewsStatus
    {
        已发布 = 0,
        未通过 = 1,
        已下架 = 2,
        待审核 = 3,
        草稿=4,
        审核通过=5

    }
}