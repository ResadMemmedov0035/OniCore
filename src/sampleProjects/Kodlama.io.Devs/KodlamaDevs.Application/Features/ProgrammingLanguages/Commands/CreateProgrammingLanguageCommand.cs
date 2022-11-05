using KodlamaDevs.Application.Features.ProgrammingLanguages.Rules;
using KodlamaDevs.Application.Services.Repositories;
using KodlamaDevs.Domain.Entities;
using MediatR;

namespace KodlamaDevs.Application.Features.ProgrammingLanguages.Commands;

public record CreateProgrammingLanguageCommand(string Name) : IRequest<CreatedProgrammingLanguageDTO>;

public record CreatedProgrammingLanguageDTO(int Id, string Name);

public class CreateProgrammingLanguageCommandHandler : IRequestHandler<CreateProgrammingLanguageCommand, CreatedProgrammingLanguageDTO>
{
    private readonly IProgrammingLanguageRepository _repository;
    private readonly ProgrammingLanguageBusinessRules _rules;

    public CreateProgrammingLanguageCommandHandler(IProgrammingLanguageRepository repository, ProgrammingLanguageBusinessRules rules)
    {
        _repository = repository;
        _rules = rules;
    }

    public async Task<CreatedProgrammingLanguageDTO> Handle(CreateProgrammingLanguageCommand request, CancellationToken cancellationToken)
    {
        await _rules.NameCannotBeDuplicated(request.Name);

        ProgrammingLanguage pl = new() { Name = request.Name };
        ProgrammingLanguage createdPl = await _repository.AddAsync(pl);
        return new CreatedProgrammingLanguageDTO(createdPl.Id, createdPl.Name);
    }
}