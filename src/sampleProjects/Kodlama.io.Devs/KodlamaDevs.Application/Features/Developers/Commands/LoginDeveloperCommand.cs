using AutoMapper;
using KodlamaDevs.Application.Features.Developers.DTOs;
using KodlamaDevs.Application.Features.Developers.Rules;
using KodlamaDevs.Application.Services.Repositories;
using KodlamaDevs.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using OniCore.Security.DTOs;
using OniCore.Security.Entities;
using OniCore.Security.Tokens;

namespace KodlamaDevs.Application.Features.Developers.Commands
{
    public class LoginDeveloperCommand : IRequest<LoggedDeveloperDTO>
    {
        public UIModel Model { get; set; } = new();
        public string IpAddress { get; set; } = string.Empty;

        public class UIModel : UserLoginDTO
        {

        }
    }

    public class LoginDeveloperCommandHandler : IRequestHandler<LoginDeveloperCommand, LoggedDeveloperDTO>
    {
        private readonly IDeveloperRepository _developerRepository;
        private readonly ITokenService _tokenService;
        private readonly DeveloperBusinessRules _businessRules;
        private readonly IMapper _mapper;

        public LoginDeveloperCommandHandler(IDeveloperRepository developerRepository, ITokenService tokenService, 
                                            DeveloperBusinessRules businessRules, IMapper mapper)
        {
            _developerRepository = developerRepository;
            _tokenService = tokenService;
            _businessRules = businessRules;
            _mapper = mapper;
        }

        public async Task<LoggedDeveloperDTO> Handle(LoginDeveloperCommand request, CancellationToken cancellationToken)
        {
            await _businessRules.DeveloperMustBeExists(request.Model.Email);

            Developer developer = await _developerRepository.GetAsync(x => x.Email == request.Model.Email, include: x => x.Include(x => x.OperationClaims));

            await _businessRules.PasswordMustBeCorrect(request.Model.Password, developer.PasswordHash, developer.PasswordSalt);

            AccessToken accessToken = _tokenService.CreateToken(developer);

            RefreshToken? refreshToken = developer.RefreshTokens.LastOrDefault();

            refreshToken = refreshToken != null && refreshToken.Expiration >= DateTime.UtcNow
                ? _tokenService.CreateRefreshToken(developer, request.IpAddress)
                : refreshToken;

            return new LoggedDeveloperDTO
            {
                RefreshToken = refreshToken!,
                Model = new() 
                {
                    AccessToken = accessToken,
                }
            };
        }
    }
}
