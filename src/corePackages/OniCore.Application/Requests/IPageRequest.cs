using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OniCore.Application.Requests
{
    // TODO: Remove
    public interface IPageRequest
    {
        public int PageIndex { get; set; }
        public int PageSize { get; set; }
    }
}
