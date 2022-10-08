using AutoMapper;
using KodlamaDevs.Application.Features.Developers.DTOs;
using KodlamaDevs.Application.Features.Developers.Rules;
using KodlamaDevs.Application.Services.Repositories;
using KodlamaDevs.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using OniCore.Security.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KodlamaDevs.Application.Features.Developers.Commands
{
    public class AddDeveloperClaimCommand : IRequest<AddedDeveloperClaimDTO>
    {
        public int Id { get; set; }
        public int ClaimId { get; set; }
    }

    public class AddDeveloperClaimCommandHandler : IRequestHandler<AddDeveloperClaimCommand, AddedDeveloperClaimDTO>
    {
        private readonly IDeveloperRepository _developerRepository;
        private readonly IOperationClaimRepository _claimRepository;
        private readonly IMapper _mapper;
        private readonly DeveloperBusinessRules _businessRules;

        public AddDeveloperClaimCommandHandler(IDeveloperRepository developerRepository, IOperationClaimRepository claimRepository,
                                               IMapper mapper, DeveloperBusinessRules businessRules)
        {
            _developerRepository = developerRepository;
            _claimRepository = claimRepository;
            _mapper = mapper;
            _businessRules = businessRules;
        }

        public async Task<AddedDeveloperClaimDTO> Handle(AddDeveloperClaimCommand request, CancellationToken cancellationToken)
        {
            await _businessRules.ClaimCannotBeDuplicated(request.Id, request.ClaimId);

            Developer developer = await _developerRepository.GetAsync(x => x.Id == request.Id);
            OperationClaim claim = await _claimRepository.GetAsync(x => x.Id == request.ClaimId);

            await _developerRepository.SetOperationClaimAsync(developer, claim);

            return new() { ClaimId = claim.Id, ClaimName = claim.Name, DeveloperId = developer.Id };
        }
    }
}
