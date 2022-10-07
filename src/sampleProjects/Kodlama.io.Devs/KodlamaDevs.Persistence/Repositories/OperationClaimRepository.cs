using KodlamaDevs.Application.Services.Repositories;
using KodlamaDevs.Persistence.Contexts;
using OniCore.Persistence.Repositories;
using OniCore.Security.Entities;

namespace KodlamaDevs.Persistence.Repositories
{
    public class OperationClaimRepository : EFCoreRepository<OperationClaim>, IOperationClaimRepository
    {
        public OperationClaimRepository(ApplicationDbContext context) : base(context)
        {

        }
    }
}