using AutoMapper;
using KodlamaDevs.Application.Services.Repositories;
using KodlamaDevs.Domain.Entities;
using MediatR;
using OniCore.Persistence.Pagination;

namespace KodlamaDevs.Application.Features.ProgrammingLanguages.Queries
{
    public record GetProgrammingLanguageListQuery(PageParams PageParams) : IRequest<GetProgrammingLanguageListDTO>;

    public class GetProgrammingLanguageListDTO : PagedListDTO<GetProgrammingLanguageListItemDTO> { }

    public class GetProgrammingLanguageListItemDTO
    {
        public int Id { get; init; }
        public string Name { get; init; } = string.Empty;
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
            IPagedList<ProgrammingLanguage> list = await _repository.GetListAsync(request.PageParams);
            return _mapper.Map<GetProgrammingLanguageListDTO>(list);
        }
    }
}
