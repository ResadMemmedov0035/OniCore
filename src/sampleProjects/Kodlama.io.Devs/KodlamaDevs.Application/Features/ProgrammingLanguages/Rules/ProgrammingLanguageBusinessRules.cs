using KodlamaDevs.Application.Services.Repositories;
using KodlamaDevs.Domain.Entities;
using OniCore.CrossCuttingConcerns.Exceptions.CustomExceptions;

namespace KodlamaDevs.Application.Features.ProgrammingLanguages.Rules
{
    public class ProgrammingLanguageBusinessRules
    {
        private readonly IProgrammingLanguageRepository _repository;

        public ProgrammingLanguageBusinessRules(IProgrammingLanguageRepository repository)
        {
            _repository = repository;
        }

        public async Task ProgrammingLanguageShouldExists(int id)
        {
            if (!await _repository.AnyAsync(x => x.Id == id))
                throw new NotFoundException(nameof(ProgrammingLanguage), id);
        }

        public async Task NameCannotBeDuplicated(string name)
        {
            if (await _repository.AnyAsync(x => x.Name == name))
                throw new BusinessException("Programming language name already exists.");
        }
    }
}
