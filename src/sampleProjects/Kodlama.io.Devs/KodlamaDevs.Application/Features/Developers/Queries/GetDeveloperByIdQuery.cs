using AutoMapper;
using KodlamaDevs.Application.Features.Developers.DTOs;
using KodlamaDevs.Application.Services.Repositories;
using KodlamaDevs.Domain.Entities;
using MediatR;

namespace KodlamaDevs.Application.Features.Developers.Queries
{
    public class GetDeveloperByIdQuery : IRequest<GetDeveloperByIdDTO>
    {
        public int Id { get; set; }
    }

    public class GetDeveloperByIdQueryHandler : IRequestHandler<GetDeveloperByIdQuery, GetDeveloperByIdDTO>
    {
        private readonly IDeveloperRepository _developerRepository;
        private readonly IMapper _mapper;

        public GetDeveloperByIdQueryHandler(IDeveloperRepository developerRepository, IMapper mapper)
        {
            _developerRepository = developerRepository;
            _mapper = mapper;
        }

        public async Task<GetDeveloperByIdDTO> Handle(GetDeveloperByIdQuery request, CancellationToken cancellationToken)
        {
            Developer developer = await _developerRepository.GetAsync(x => x.Id == request.Id);
            return _mapper.Map<GetDeveloperByIdDTO>(developer);
        }
    }
}
