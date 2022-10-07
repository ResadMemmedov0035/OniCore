using OniCore.Persistence.Repositories;
using OniCore.Security.Entities;

namespace KodlamaDevs.Application.Services.Repositories
{
    public interface IOperationClaimRepository : IRepository<OperationClaim>, IAsyncRepository<OperationClaim>
    {

    }
}
