using SmartCity.Domain.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartCity.Domain.Concrete
{
    public class ProductCheshi : IProductCheshi
    {
        public string GetName(string name)
        {
            return "你好" + name;
        }
    }
}
