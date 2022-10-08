using AutoMapper;
using KodlamaDevs.Application.Features.ProgrammingLanguages.DTOs;
using KodlamaDevs.Application.Services.Repositories;
using KodlamaDevs.Domain.Entities;
using MediatR;

namespace KodlamaDevs.Application.Features.ProgrammingLanguages.Queries
{
    public class GetProgrammingLanguageByIdQuery : IRequest<GetProgrammingLanguageByIdDTO>
    {
        public int Id { get; set; }
    }

    public class GetProgrammingLanguageByIdQueryHandler : IRequestHandler<GetProgrammingLanguageByIdQuery, GetProgrammingLanguageByIdDTO>
    {
        private readonly IProgrammingLanguageRepository _repository;
        private readonly IMapper _mapper;

        public GetProgrammingLanguageByIdQueryHandler(IProgrammingLanguageRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<GetProgrammingLanguageByIdDTO> Handle(GetProgrammingLanguageByIdQuery request, CancellationToken cancellationToken)
        {
            ProgrammingLanguage language = await _repository.GetAsync(x => x.Id == request.Id);
            return _mapper.Map<GetProgrammingLanguageByIdDTO>(language);
        }
    }
}
