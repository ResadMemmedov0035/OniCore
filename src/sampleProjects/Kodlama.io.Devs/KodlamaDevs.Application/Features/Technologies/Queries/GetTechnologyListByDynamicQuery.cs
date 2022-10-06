using AutoMapper;
using KodlamaDevs.Application.Features.Technologies.DTOs;
using KodlamaDevs.Application.Services.Repositories;
using KodlamaDevs.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using OniCore.Persistence.Dynamic;
using OniCore.Persistence.Pagination;

namespace KodlamaDevs.Application.Features.Technologies.Queries
{
    public class GetTechnologyListByDynamicQuery : IRequest<GetTechnologyListDTO>
    {
        public PageParams PageParams { get; set; } = new();
        public DynamicParams DynamicParams { get; set; } = new();
    }

    public class GetTechnologyListByDynamicQueryHandler : IRequestHandler<GetTechnologyListByDynamicQuery, GetTechnologyListDTO>
    {
        private readonly ITechnologyRepository _repository;
        private readonly IMapper _mapper;

        public GetTechnologyListByDynamicQueryHandler(ITechnologyRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<GetTechnologyListDTO> Handle(GetTechnologyListByDynamicQuery request, CancellationToken cancellationToken)
        {
            IPagedList<Technology> technologyList = await _repository.GetListAsync(request.PageParams, request.DynamicParams,
                                                                                   include: x => x.Include(x => x.ProgrammingLanguage));
            return _mapper.Map<GetTechnologyListDTO>(technologyList);
        }
    }
}
