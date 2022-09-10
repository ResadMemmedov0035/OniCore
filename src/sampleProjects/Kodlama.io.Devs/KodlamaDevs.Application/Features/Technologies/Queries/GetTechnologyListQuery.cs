using AutoMapper;
using KodlamaDevs.Application.Features.Technologies.DTOs;
using KodlamaDevs.Application.Services.Repositories;
using KodlamaDevs.Domain.Entities;
using MediatR;
using OniCore.Persistence.Pagination;

namespace KodlamaDevs.Application.Features.Technologies.Queries
{
    public class GetTechnologyListQuery : IRequest<GetTechnologyListDTO>
    {
        public PaginationParams PaginationParams { get; set; } = new();
    }

    public class GetTechnologyListQueryHandler : IRequestHandler<GetTechnologyListQuery, GetTechnologyListDTO>
    {
        private readonly ITechnologyRepository _repository;
        private readonly IMapper _mapper;

        public GetTechnologyListQueryHandler(ITechnologyRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<GetTechnologyListDTO> Handle(GetTechnologyListQuery request, CancellationToken cancellationToken)
        {
            IPagedList<Technology> technologyList = await _repository.GetListAsync(request.PaginationParams);
            return _mapper.Map<GetTechnologyListDTO>(technologyList);
        }
    }
}
