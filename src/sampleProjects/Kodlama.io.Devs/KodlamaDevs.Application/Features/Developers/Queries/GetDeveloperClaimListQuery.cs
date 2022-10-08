using AutoMapper;
using KodlamaDevs.Application.Features.Developers.DTOs;
using KodlamaDevs.Application.Services.Repositories;
using KodlamaDevs.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using OniCore.Persistence.Pagination;
using OniCore.Security.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KodlamaDevs.Application.Features.Developers.Queries
{
    public class GetDeveloperClaimListQuery : IRequest<GetDeveloperClaimListDTO>
    {
        public int Id { get; set; }
        public PageParams PageParams { get; set; } = new();
    }

    public class GetDeveloperClaimListQueryHandler : IRequestHandler<GetDeveloperClaimListQuery, GetDeveloperClaimListDTO>
    {
        private readonly IDeveloperRepository _developerRepository;
        private readonly IMapper _mapper;

        public GetDeveloperClaimListQueryHandler(IDeveloperRepository developerRepository, IMapper mapper)
        {
            _developerRepository = developerRepository;
            _mapper = mapper;
        }

        public async Task<GetDeveloperClaimListDTO> Handle(GetDeveloperClaimListQuery request, CancellationToken cancellationToken)
        {
            Developer developer = await _developerRepository.GetAsync(x => x.Id == request.Id, include: x => x.Include(x => x.OperationClaims));

            IPagedList<OperationClaim> claims = new PagedList<OperationClaim>(developer.OperationClaims, request.PageParams.Index, request.PageParams.Size);

            return _mapper.Map<GetDeveloperClaimListDTO>(claims);
        }
    }
}
