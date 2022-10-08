using KodlamaDevs.Domain.Entities;
using OniCore.Persistence.Repositories;

namespace KodlamaDevs.Application.Services.Repositories
{
    public interface IProgrammingLanguageRepository : IRepository<ProgrammingLanguage>, IAsyncRepository<ProgrammingLanguage>
    {
    }
}
