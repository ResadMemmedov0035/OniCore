using AutoMapper;
using KodlamaDevs.Application.Features.Developers.DTOs;
using KodlamaDevs.Application.Features.Developers.Rules;
using KodlamaDevs.Application.Services.Repositories;
using KodlamaDevs.Domain.Constants;
using KodlamaDevs.Domain.Entities;
using MediatR;
using OniCore.Security.DTOs;
using OniCore.Security.Entities;
using OniCore.Security.Hashing;
using OniCore.Security.Tokens;

namespace KodlamaDevs.Application.Features.Developers.Commands
{
    public class RegisterDeveloperCommand : IRequest<RegisteredDeveloperDTO>
    {
        public UIModel Model { get; set; } = new();
        public string IpAddress { get; set; } = string.Empty;

        public class UIModel : UserRegisterDTO
        {
            public string GithubAddress { get; set; } = string.Empty;
        }
    }

    public class RegisterDeveloperCommandHandler : IRequestHandler<RegisterDeveloperCommand, RegisteredDeveloperDTO>
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

        public async Task<RegisteredDeveloperDTO> Handle(RegisterDeveloperCommand request, CancellationToken cancellationToken)
        {
            await _businessRules.EmailCannotBeDuplicated(request.Model.Email);
            await _businessRules.GithubAddressCannotBeDuplicated(request.Model.GithubAddress);

            #region Create Developer

            HashHelper.CreateHash(request.Model.Password, out byte[] hash, out byte[] hashSalt);

            Developer developer = _mapper.Map<Developer>(request.Model);
            developer.PasswordHash = hash;
            developer.PasswordSalt = hashSalt;

            OperationClaim claim = await _operationClaimRepository.GetAsync(x => x.Name == OperationClaims.User);
            developer.OperationClaims.Add(claim);

            Developer createdDeveloper = await _developerRepository.CreateAsync(developer);

            #endregion

            AccessToken accessToken = _tokenService.CreateToken(createdDeveloper);
            RefreshToken refreshToken = _tokenService.CreateRefreshToken(developer, request.IpAddress);
            RefreshToken createdRefreshToken = await _refreshTokenRepository.CreateAsync(refreshToken);

            return new RegisteredDeveloperDTO
            {
                Id = createdDeveloper.Id,
                AccessToken = accessToken,
                RefreshToken = createdRefreshToken
            };
        }
    }
}
