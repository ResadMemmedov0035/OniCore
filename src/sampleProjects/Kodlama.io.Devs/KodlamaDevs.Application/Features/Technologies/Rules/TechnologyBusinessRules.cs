using KodlamaDevs.Application.Services.Repositories;
using OniCore.CrossCuttingConcerns.Exceptions.CustomExceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
