using AutoMapper;
using KodlamaDevs.Application.Features.Developers.DTOs;
using KodlamaDevs.Application.Features.Developers.Rules;
using KodlamaDevs.Application.Services.Repositories;
using KodlamaDevs.Domain.Entities;
using MediatR;
using OniCore.Security.DTOs;
using OniCore.Security.Entities;
using OniCore.Security.Hashing;
using OniCore.Security.Tokens;

namespace KodlamaDevs.Application.Features.Developers.Commands
{
    public class RegisterDeveloperCommand : UserRegisterDTO, IRequest<AuthorizedDeveloperDTO>
    {
        public string GithubAddress { get; set; } = string.Empty;
    }

    public class RegisterDeveloperCommandHandler : IRequestHandler<RegisterDeveloperCommand, AuthorizedDeveloperDTO>
    {
        private readonly IDeveloperRepository _developerRepository;
        private readonly DeveloperBusinessRules _businessRules;
        private readonly ITokenService _tokenService;
        private readonly IMapper _mapper;

        public RegisterDeveloperCommandHandler(IDeveloperRepository developerRepository, ITokenService tokenService,
                                               DeveloperBusinessRules businessRules, IMapper mapper)
        {
            _developerRepository = developerRepository;
            _businessRules = businessRules;
            _tokenService = tokenService;
            _mapper = mapper;
        }

        public async Task<AuthorizedDeveloperDTO> Handle(RegisterDeveloperCommand request, CancellationToken cancellationToken)
        {
            await _businessRules.EmailCannotBeDuplicated(request.Email);
            await _businessRules.GithubAddressCannotBeDuplicated(request.GithubAddress);

            HashHelper.CreateHash(request.Password, out byte[] hash, out byte[] hashSalt);

            Developer developer = _mapper.Map<Developer>(request);
            developer.PasswordHash = hash;
            developer.PasswordSalt = hashSalt;

            _developerRepository.SetRolesByClaimId(developer, new[] { 2 }, false);

            Developer addedDeveloper = await _developerRepository.AddAsync(developer);
            AccessToken accessToken = _tokenService.CreateToken(addedDeveloper);

            return _mapper.Map(accessToken, _mapper.Map<AuthorizedDeveloperDTO>(addedDeveloper));
        }
    }
}
