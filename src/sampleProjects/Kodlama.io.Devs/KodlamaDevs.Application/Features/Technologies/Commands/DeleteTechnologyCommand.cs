using AutoMapper;
using KodlamaDevs.Application.Features.Technologies.DTOs;
using KodlamaDevs.Application.Services.Repositories;
using KodlamaDevs.Domain.Entities;
using MediatR;

namespace KodlamaDevs.Application.Features.Technologies.Commands
{
    public class DeleteTechnologyCommand : IRequest<DeletedTechnologyDTO>
    {
        public int Id { get; set; }
    }

    public class DeleteTechnologyCommandHandler : IRequestHandler<DeleteTechnologyCommand, DeletedTechnologyDTO>
    {
        private readonly ITechnologyRepository _repository;
        private readonly IMapper _mapper;

        public DeleteTechnologyCommandHandler(ITechnologyRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<DeletedTechnologyDTO> Handle(DeleteTechnologyCommand request, CancellationToken cancellationToken)
        {
            Technology technology = _mapper.Map<Technology>(request);
            Technology deletedTechnology = await _repository.DeleteAsync(technology);
            return _mapper.Map<DeletedTechnologyDTO>(deletedTechnology);
        }
    }
}
