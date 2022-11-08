using AutoMapper;
using KodlamaDevs.Application.Features.Technologies.DTOs;
using KodlamaDevs.Application.Features.Technologies.Rules;
using KodlamaDevs.Application.Services.Repositories;
using KodlamaDevs.Domain.Entities;
using MediatR;

namespace KodlamaDevs.Application.Features.Technologies.Commands
{
    public class CreateTechnologyCommand : IRequest<CreatedTechnologyDTO>
    {
        public int ProgrammingLanguageId { get; set; }
        public string Name { get; set; } = string.Empty;
    }

    public class CreateTechnologyCommandHandler : IRequestHandler<CreateTechnologyCommand, CreatedTechnologyDTO>
    {
        private readonly ITechnologyRepository _repository;
        private readonly IMapper _mapper;
        private readonly TechnologyBusinessRules _businessRules;

        public CreateTechnologyCommandHandler(ITechnologyRepository repository, IMapper mapper, TechnologyBusinessRules businessRules)
        {
            _repository = repository;
            _mapper = mapper;
            _businessRules = businessRules;
        }

        public async Task<CreatedTechnologyDTO> Handle(CreateTechnologyCommand request, CancellationToken cancellationToken)
        {
            await _businessRules.NameCannotBeDuplicated(request.Name);

            Technology technology = _mapper.Map<Technology>(request);
            Technology addedTechnology = await _repository.CreateAsync(technology);
            return _mapper.Map<CreatedTechnologyDTO>(addedTechnology);
        }
    }
}
