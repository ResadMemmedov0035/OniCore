using KodlamaDevs.Application.Services.Repositories;
using OniCore.CrossCuttingConcerns.Exceptions.CustomExceptions;

namespace KodlamaDevs.Application.Features.Technologies.Rules
{
    public class TechnologyBusinessRules
    {
        private readonly ITechnologyRepository _repository;

        public TechnologyBusinessRules(ITechnologyRepository repository)
        {
            _repository = repository;
        }

        public async Task NameCannotBeDuplicated(string name)
        {
            if (await _repository.AnyAsync(x => x.Name == name))
                throw new BusinessException("Technology name already exists.");
        }
    }
}
