using KodlamaDevs.Application.Services.Repositories;
using KodlamaDevs.Domain.Entities;
using KodlamaDevs.Persistence.Contexts;
using OniCore.Persistence.Repositories;

namespace KodlamaDevs.Persistence.Repositories
{
    public class ProgrammingLanguageRepository : EFCoreRepository<ProgrammingLanguage>, IProgrammingLanguageRepository
    {
        public ProgrammingLanguageRepository(ApplicationDbContext context) : base(context)
        {

        }
    }
}
