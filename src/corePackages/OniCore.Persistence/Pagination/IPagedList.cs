using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OniCore.Persistence.Pagination
{
    public interface IPagedList<T>
    {
        public int PageIndex { get; }
        public int PageSize { get; }
        public int TotalItems { get; }
        public int TotalPages { get; }
        public IList<T> Items { get; }
        public bool HasPrevious { get; }
        public bool HasNext { get; }
    }
}
