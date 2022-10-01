using KodlamaDevs.Domain.Entities;
using OniCore.Persistence.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KodlamaDevs.Application.Services.Repositories
{
    public interface IDeveloperRepository : IRepository<Developer>, IAsyncRepository<Developer>
    {
        void SetRolesByClaimId(Developer developer, int[] claimsIds, bool saveChanges = true);
    }
}
