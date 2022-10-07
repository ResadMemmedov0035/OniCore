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
using System.Text.Json.Serialization;

namespace KodlamaDevs.Application.Features.Developers.Commands
{
    public class RegisterDeveloperCommand : UserRegisterDTO, IRequest<AuthorizedDeveloperDTO>
    {
        //public RegisterDeveloperDTO RegisterDeveloperDTO { get; set; } = new();
        public string GithubAddress { get; set; } = string.Empty;
        [JsonIgnore]
        public string IpAddress { get; set; } = string.Empty;
    }

    public class RegisterDeveloperCommandHandler : IRequestHandler<RegisterDeveloperCommand, AuthorizedDeveloperDTO>
    {
        private readonly IDeveloperRepository _developerRepository;
        private readonly DeveloperBusinessRules _businessRules;
        private readonly ITokenService _tokenService;
        private readonly IOperationClaimRepository _operationClaimRepository;
        private readonly IRefreshTokenRepository _refreshTokenRepository;
        private readonly IMapper _mapper;

        public RegisterDeveloperCommandHandler(IDeveloperRepository developerRepository, ITokenService tokenService, 
                                               IOperationClaimRepository operationClaimRepository, IRefreshTokenRepository refreshTokenRepository,
                                               DeveloperBusinessRules businessRules, IMapper mapper)
        {
            _developerRepository = developerRepository;
            _businessRules = businessRules;
            _tokenService = tokenService;
            _operationClaimRepository = operationClaimRepository;
            _refreshTokenRepository = refreshTokenRepository;
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

            OperationClaim claim = await _operationClaimRepository.GetAsync(x => x.Name == "user");
            await _developerRepository.SetOperationClaim(developer, claim, saveChanges: false);

            Developer addedDeveloper = await _developerRepository.AddAsync(developer);

            AccessToken accessToken = _tokenService.CreateToken(addedDeveloper);
            RefreshToken refreshToken = _tokenService.CreateRefreshToken(developer, request.IpAddress);
            RefreshToken addedRefreshToken = await _refreshTokenRepository.AddAsync(refreshToken);

            AuthorizedDeveloperDTO authorizedDev = new()
            {
                AccessToken = accessToken,
                RefreshToken = addedRefreshToken
            };
            return _mapper.Map(addedDeveloper, authorizedDev);
        }
    }
}
