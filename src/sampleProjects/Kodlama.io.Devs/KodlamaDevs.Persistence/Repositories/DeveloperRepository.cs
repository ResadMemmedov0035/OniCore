using KodlamaDevs.Application.Services.Repositories;
using KodlamaDevs.Domain.Entities;
using KodlamaDevs.Persistence.Contexts;
using OniCore.Persistence.Repositories;
using OniCore.Security.Entities;

namespace KodlamaDevs.Persistence.Repositories
{
    public class DeveloperRepository : EFCoreRepository<Developer>, IDeveloperRepository
    {
        public DeveloperRepository(ApplicationDbContext context) : base(context)
        {

        }

        public async Task AddOperationClaimAsync(Developer developer, OperationClaim operationClaim, bool saveChanges = true)
        {
            developer.OperationClaims.Add(operationClaim);
            if (saveChanges)
                await Context.SaveChangesAsync();
        }

        public async Task RemoveOperationClaimAsync(Developer developer, OperationClaim operationClaim, bool saveChanges = true)
        {
            developer.OperationClaims.Remove(operationClaim);
            if (saveChanges)
                await Context.SaveChangesAsync();
        }
    }
}