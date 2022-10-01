using AutoMapper;
using KodlamaDevs.Application.Features.Developers.DTOs;
using KodlamaDevs.Application.Features.Developers.Rules;
using KodlamaDevs.Application.Services.Repositories;
using KodlamaDevs.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using OniCore.Security.DTOs;
using OniCore.Security.Hashing;
using OniCore.Security.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KodlamaDevs.Application.Features.Developers.Commands
{
    public class LoginDeveloperCommand : UserLoginDTO, IRequest<AuthorizedDeveloperDTO>
    {

    }

    public class LoginDeveloperCommandHandler : IRequestHandler<LoginDeveloperCommand, AuthorizedDeveloperDTO>
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

        public async Task<AuthorizedDeveloperDTO> Handle(LoginDeveloperCommand request, CancellationToken cancellationToken)
        {
            await _businessRules.DeveloperMustBeExists(request.Email);

            Developer developer = await _developerRepository.GetAsync(x => x.Email == request.Email, include: x => x.Include(x => x.OperationClaims));

            _businessRules.PasswordMustBeCorrect(request.Password, developer.PasswordHash, developer.PasswordSalt);

            AccessToken accessToken = _tokenService.CreateToken(developer);

            return _mapper.Map(accessToken, _mapper.Map<AuthorizedDeveloperDTO>(developer));
        }
    }
}
