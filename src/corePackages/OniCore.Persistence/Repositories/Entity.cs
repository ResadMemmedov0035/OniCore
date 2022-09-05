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

    public abstract class Entity<TId> : Entity where TId : struct
    {
        public new TId Id { get; set; }
    }
}
