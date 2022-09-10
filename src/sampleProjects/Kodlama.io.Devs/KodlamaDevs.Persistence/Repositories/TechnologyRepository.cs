using KodlamaDevs.Application.Services.Repositories;
using KodlamaDevs.Domain.Entities;
using KodlamaDevs.Persistence.Contexts;
using OniCore.Persistence.Repositories;

namespace KodlamaDevs.Persistence.Repositories
{
    public class TechnologyRepository : EFCoreRepository<Technology>, ITechnologyRepository
    {
        public TechnologyRepository(ApplicationDbContext context) : base(context)
        {

        }
    }
}
