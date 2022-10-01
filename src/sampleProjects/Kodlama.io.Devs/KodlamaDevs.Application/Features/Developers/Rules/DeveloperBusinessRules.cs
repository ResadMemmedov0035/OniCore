using KodlamaDevs.Application.Services.Repositories;
using OniCore.CrossCuttingConcerns.Exceptions.CustomExceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KodlamaDevs.Application.Features.Developers.Rules
{
    public class DeveloperBusinessRules
    {
        private readonly IDeveloperRepository _developerRepository;

        public DeveloperBusinessRules(IDeveloperRepository developerRepository)
        {
            _developerRepository = developerRepository;
        }

        public async Task EmailCannotBeDuplicated(string email)
        {
            if (await _developerRepository.AnyAsync(x => x.Email == email))
                throw new BusinessException("The email already exists.");
        }

        public async Task GithubAddressCannotBeDuplicated(string githubAddress)
        {
            if (await _developerRepository.AnyAsync(x => x.GithubAddress == githubAddress))
                throw new BusinessException("The Github address already exists.");
        }
    }
}
