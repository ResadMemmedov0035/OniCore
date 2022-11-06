using KodlamaDevs.Application.Features.ProgrammingLanguages.Rules;
using KodlamaDevs.Application.Services.Repositories;
using KodlamaDevs.Domain.Entities;
using MediatR;

namespace KodlamaDevs.Application.Features.ProgrammingLanguages.Queries
{
    public record GetProgrammingLanguageByIdQuery(int Id) : IRequest<GetProgrammingLanguageByIdDTO>;

    public record GetProgrammingLanguageByIdDTO(int Id, string Name);

    public class GetProgrammingLanguageByIdQueryHandler : IRequestHandler<GetProgrammingLanguageByIdQuery, GetProgrammingLanguageByIdDTO>
    {
        private readonly IProgrammingLanguageRepository _repository;
        private readonly ProgrammingLanguageBusinessRules _rules;

        public GetProgrammingLanguageByIdQueryHandler(IProgrammingLanguageRepository repository, ProgrammingLanguageBusinessRules rules)
        {
            _repository = repository;
            _rules = rules;
        }

        public async Task<GetProgrammingLanguageByIdDTO> Handle(GetProgrammingLanguageByIdQuery request, CancellationToken cancellationToken)
        {
            await _rules.ProgrammingLanguageShouldExists(request.Id);

            ProgrammingLanguage pl = await _repository.GetAsync(x => x.Id == request.Id);
            return new GetProgrammingLanguageByIdDTO(pl.Id, pl.Name);
        }
    }
}
