using AutoMapper;
using KodlamaDevs.Application.Features.Technologies.DTOs;
using KodlamaDevs.Application.Features.Technologies.Rules;
using KodlamaDevs.Application.Services.Repositories;
using KodlamaDevs.Domain.Entities;
using MediatR;

namespace KodlamaDevs.Application.Features.Technologies.Commands
{
    public class UpdateTechnologyCommand : IRequest<UpdatedTechnologyDTO>
    {
        public int Id { get; set; }
        public int ProgrammingLanguageId { get; set; }
        public string Name { get; set; } = string.Empty;
    }

    public class UpdateTechnologyCommandHandler : IRequestHandler<UpdateTechnologyCommand, UpdatedTechnologyDTO>
    {
        private readonly ITechnologyRepository _repository;
        private readonly IMapper _mapper;
        private readonly TechnologyBusinessRules _businessRules;

        public UpdateTechnologyCommandHandler(ITechnologyRepository repository, IMapper mapper, TechnologyBusinessRules businessRules)
        {
            _repository = repository;
            _mapper = mapper;
            _businessRules = businessRules;
        }

        public async Task<UpdatedTechnologyDTO> Handle(UpdateTechnologyCommand request, CancellationToken cancellationToken)
        {
            await _businessRules.NameCannotBeDuplicated(request.Name);

            Technology technology = _mapper.Map<Technology>(request);
            Technology updatedTechnology = await _repository.UpdateAsync(technology);
            return _mapper.Map<UpdatedTechnologyDTO>(updatedTechnology);
        }
    }
}
