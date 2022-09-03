using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OniCore.Persistence.Repositories
{
    public abstract class Entity
    {
        public int Id { get; set; }
    }

    public abstract class Entity<TId> where TId : struct
    {
        public TId Id { get; set; }
    }
}
