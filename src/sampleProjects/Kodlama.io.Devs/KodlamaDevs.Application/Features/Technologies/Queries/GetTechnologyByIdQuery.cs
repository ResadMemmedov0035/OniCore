using AutoMapper;
using KodlamaDevs.Application.Features.Technologies.DTOs;
using KodlamaDevs.Application.Services.Repositories;
using KodlamaDevs.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace KodlamaDevs.Application.Features.Technologies.Queries
{
    public class GetTechnologyByIdQuery : IRequest<GetTechnologyByIdDTO>
    {
        public int Id { get; set; }
    }

    public class GetTechnologyByIdQueryHandler : IRequestHandler<GetTechnologyByIdQuery, GetTechnologyByIdDTO>
    {
        private readonly ITechnologyRepository _repository;
        private readonly IMapper _mapper;

        public GetTechnologyByIdQueryHandler(ITechnologyRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<GetTechnologyByIdDTO> Handle(GetTechnologyByIdQuery request, CancellationToken cancellationToken)
        {
            Technology technology = await _repository.GetAsync(x => x.Id == request.Id, include: x => x.Include(x => x.ProgrammingLanguage));
            return _mapper.Map<GetTechnologyByIdDTO>(technology);
        }
    }
}
