using KodlamaDevs.Application.Services.Repositories;
using OniCore.CrossCuttingConcerns.Exceptions.CustomExceptions;
using OniCore.Security.Hashing;

namespace KodlamaDevs.Application.Features.Developers.Rules
{
    public class DeveloperBusinessRules
    {
        private readonly IDeveloperRepository _developerRepository;

        public DeveloperBusinessRules(IDeveloperRepository developerRepository)
        {
            _developerRepository = developerRepository;
        }

        public async Task ClaimMustExistsForDeveloper(int devId, int claimId)
        {
            bool any = await _developerRepository.AnyAsync(x => x.Id == devId && x.OperationClaims.Any(x => x.Id == claimId));
            if (!any)
                throw new BusinessException("The operation claim to remove must exists for this user.");
        }

        public async Task ClaimCannotBeDuplicatedForDeveloper(int devId, int claimId)
        {
            bool any = await _developerRepository.AnyAsync(x => x.Id == devId && x.OperationClaims.Any(x => x.Id == claimId));
            if (any)
                throw new BusinessException("The operation claim already exists for this user.");
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

