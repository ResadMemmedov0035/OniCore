using AutoMapper;
using KodlamaDevs.Application.Features.Developers.DTOs;
using KodlamaDevs.Application.Services.Repositories;
using KodlamaDevs.Domain.Entities;
using MediatR;
using OniCore.Persistence.Pagination;

namespace KodlamaDevs.Application.Features.Developers.Queries
{
    public class GetDeveloperListQuery : IRequest<GetDeveloperListDTO>
    {
        public PageParams PageParams { get; set; } = new();
    }

    public class GetDeveloperListQueryHandler : IRequestHandler<GetDeveloperListQuery, GetDeveloperListDTO>
    {
        private readonly IDeveloperRepository _developerRepository;
        private readonly IMapper _mapper;

        public GetDeveloperListQueryHandler(IDeveloperRepository developerRepository, IMapper mapper)
        {
            _developerRepository = developerRepository;
            _mapper = mapper;
        }

        public async Task<GetDeveloperListDTO> Handle(GetDeveloperListQuery request, CancellationToken cancellationToken)
        {
            IPagedList<Developer> developers = await _developerRepository.GetListAsync(request.PageParams);
            return _mapper.Map<GetDeveloperListDTO>(developers);
        }
    }
}
