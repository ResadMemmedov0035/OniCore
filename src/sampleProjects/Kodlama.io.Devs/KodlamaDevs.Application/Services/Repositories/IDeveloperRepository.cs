using KodlamaDevs.Domain.Entities;
using OniCore.Persistence.Repositories;
using OniCore.Security.Entities;

namespace KodlamaDevs.Application.Services.Repositories
{
    public interface IDeveloperRepository : IRepository<Developer>, IAsyncRepository<Developer>
    {
        Task AddOperationClaimAsync(Developer developer, OperationClaim operationClaim, bool saveChanges = true);
        Task RemoveOperationClaimAsync(Developer developer, OperationClaim operationClaim, bool saveChanges = true);
    }
}
