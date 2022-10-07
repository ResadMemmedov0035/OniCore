using KodlamaDevs.Application.Services.Repositories;
using KodlamaDevs.Persistence.Contexts;
using OniCore.Persistence.Repositories;
using OniCore.Security.Entities;

namespace KodlamaDevs.Persistence.Repositories
{
    public class RefreshTokenRepository : EFCoreRepository<RefreshToken>, IRefreshTokenRepository
    {
        public RefreshTokenRepository(ApplicationDbContext context) : base(context)
        {

        }
    }
}