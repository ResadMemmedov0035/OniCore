using KodlamaDevs.Application.Services.Repositories;
using KodlamaDevs.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using OniCore.CrossCuttingConcerns.Exceptions.CustomExceptions;
using OniCore.Security.Entities;
using OniCore.Security.Hashing;
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

        public void PasswordMustBeCorrect(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            if (!HashHelper.VerifyHash(password, passwordHash, passwordSalt))
                throw new AuthorizationException("The email or password is wrong.");
        }

        public async Task DeveloperMustBeExists(string email)
        {
            if (!await _developerRepository.AnyAsync(x => x.Email == email))
                throw new NotFoundException("Developer not found.");
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

