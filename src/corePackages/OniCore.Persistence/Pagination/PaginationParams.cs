using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OniCore.Persistence.Pagination
{
    public class PaginationParams
    {
        public int Index { get; set; }
        public int Size { get; set; } = 10;
    }
}
