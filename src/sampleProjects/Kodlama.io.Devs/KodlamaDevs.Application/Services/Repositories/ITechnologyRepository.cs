using KodlamaDevs.Domain.Entities;
using OniCore.Persistence.Repositories;

namespace KodlamaDevs.Application.Services.Repositories
{
    public interface ITechnologyRepository : IRepository<Technology>, IAsyncRepository<Technology>
    {
    }
}
