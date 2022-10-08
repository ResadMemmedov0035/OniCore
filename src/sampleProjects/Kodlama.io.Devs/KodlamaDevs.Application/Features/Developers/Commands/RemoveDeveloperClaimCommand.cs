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
    public class RemoveDeveloperClaimCommand : IRequest<RemovedDeveloperClaimDTO>
    {
        public int Id { get; set; }
        public int ClaimId { get; set; }
    }

    public class RemoveDeveloperClaimCommandHandler : IRequestHandler<RemoveDeveloperClaimCommand, RemovedDeveloperClaimDTO>
    {
        private readonly IDeveloperRepository _developerRepository;
        private readonly IOperationClaimRepository _claimRepository;
        private readonly DeveloperBusinessRules _businessRules;

        public RemoveDeveloperClaimCommandHandler(IDeveloperRepository developerRepository, IOperationClaimRepository claimRepository,
                                                  DeveloperBusinessRules businessRules)
        {
            _developerRepository = developerRepository;
            _claimRepository = claimRepository;
            _businessRules = businessRules;
        }

        public async Task<RemovedDeveloperClaimDTO> Handle(RemoveDeveloperClaimCommand request, CancellationToken cancellationToken)
        {
            await _businessRules.ClaimMustExistsForDeveloper(request.Id, request.ClaimId);

            Developer developer = await _developerRepository.GetAsync(x => x.Id == request.Id, include: x => x.Include(x => x.OperationClaims));
            OperationClaim claim = await _claimRepository.GetAsync(x => x.Id == request.ClaimId);

            await _developerRepository.RemoveOperationClaimAsync(developer, claim);

            return new() { ClaimId = claim.Id, ClaimName = claim.Name, DeveloperId = developer.Id };
        }
    }
}
