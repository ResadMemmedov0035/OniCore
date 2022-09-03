using AutoMapper;
using KodlamaDevs.Application.Features.ProgrammingLanguages.DTOs;
using KodlamaDevs.Application.Services.Repositories;
using KodlamaDevs.Domain.Entities;
using MediatR;
using OniCore.Application.Requests;
using OniCore.Persistence.Pagination;

namespace KodlamaDevs.Application.Features.ProgrammingLanguages.Queries
{
    public class GetProgrammingLanguageListQuery : IRequest<GetProgrammingLanguageListDTO>, IPageRequest
    {
        public int PageIndex { get; set; }
        public int PageSize { get; set; } = 10;
    }

    public class GetProgrammingLanguageListQueryHandler : IRequestHandler<GetProgrammingLanguageListQuery, GetProgrammingLanguageListDTO>
    {
        private readonly IProgrammingLanguageRepository _repository;
        private readonly IMapper _mapper;

        public GetProgrammingLanguageListQueryHandler(IProgrammingLanguageRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<GetProgrammingLanguageListDTO> Handle(GetProgrammingLanguageListQuery request, CancellationToken cancellationToken)
        {
            IPagedList<ProgrammingLanguage> languageList = await _repository.GetListAsync(request.PageIndex, request.PageSize);
            return _mapper.Map<GetProgrammingLanguageListDTO>(languageList);
        }
    }
}
