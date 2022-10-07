using KodlamaDevs.Domain.Entities;
using OniCore.Persistence.Repositories;
using OniCore.Security.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KodlamaDevs.Application.Services.Repositories
{
    public interface IDeveloperRepository : IRepository<Developer>, IAsyncRepository<Developer>
    {
        Task SetOperationClaim(Developer developer, OperationClaim operationClaim, bool saveChanges = true);
    }
}
