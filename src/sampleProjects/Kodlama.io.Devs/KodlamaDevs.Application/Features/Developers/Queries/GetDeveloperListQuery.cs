using AutoMapper;
using KodlamaDevs.Application.Features.Developers.DTOs;
using KodlamaDevs.Application.Services.Repositories;
using KodlamaDevs.Domain.Entities;
using MediatR;
using OniCore.Persistence.Pagination;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KodlamaDevs.Application.Features.Developers.Queries
{
    public class GetDeveloperListQuery : IRequest<GetDeveloperListDTO>
    {
        public PaginationParams PaginationParams { get; set; } = new();
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
            IPagedList<Developer> developers = await _developerRepository.GetListAsync(request.PaginationParams);
            return _mapper.Map<GetDeveloperListDTO>(developers);
        }
    }
}
