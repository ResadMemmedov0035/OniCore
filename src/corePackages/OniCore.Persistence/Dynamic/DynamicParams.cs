using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OniCore.Persistence.Dynamic
{
    public class DynamicParams
    {
        public IEnumerable<Sort>? Sorts { get; set; }
        public Filter? Filter { get; set; }
    }
}
