using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OniCore.Persistence.Repositories
{
    public interface IQuery<T>
    {
        IQueryable<T> Source { get; }
    }
}
