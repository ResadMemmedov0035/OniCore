using AutoMapper;
using KodlamaDevs.Application.Features.Developers.DTOs;
using KodlamaDevs.Application.Features.Developers.Rules;
using KodlamaDevs.Application.Services.Repositories;
using KodlamaDevs.Domain.Entities;
using MediatR;
using OniCore.Application.Pipelines.Authorization;
using System.Text.Json.Serialization;

namespace KodlamaDevs.Application.Features.Developers.Commands
{
    public class UpdateDeveloperCommand : IRequest<UpdatedDeveloperDTO>, ISecuredRequest
    {
        [JsonIgnore]
        public int Id { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string GithubAddress { get; set; } = string.Empty;

        public string[] RequiredRoles => new[] { "user" };
    }

    public class UpdateDeveloperCommandHandler : IRequestHandler<UpdateDeveloperCommand, UpdatedDeveloperDTO>
    {
        private readonly IDeveloperRepository _developerRepository;
        private readonly DeveloperBusinessRules _businessRules;
        private readonly IMapper _mapper;

        public UpdateDeveloperCommandHandler(IDeveloperRepository developerRepository, DeveloperBusinessRules businessRules, IMapper mapper)
        {
            _developerRepository = developerRepository;
            _businessRules = businessRules;
            _mapper = mapper;
        }

        public async Task<UpdatedDeveloperDTO> Handle(UpdateDeveloperCommand request, CancellationToken cancellationToken)
        {
            await _businessRules.GithubAddressCannotBeDuplicated(request.GithubAddress);

            Developer developer = await _developerRepository.GetAsync(x => x.Id == request.Id);
            developer = _mapper.Map(request, developer);

            Developer updatedDeveloper = await _developerRepository.UpdateAsync(developer);

            return _mapper.Map<UpdatedDeveloperDTO>(updatedDeveloper);
        }
    }
}
