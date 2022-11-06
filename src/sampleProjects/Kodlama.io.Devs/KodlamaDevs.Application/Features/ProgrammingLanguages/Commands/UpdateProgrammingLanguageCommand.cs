using KodlamaDevs.Application.Features.ProgrammingLanguages.Rules;
using KodlamaDevs.Application.Services.Repositories;
using KodlamaDevs.Domain.Entities;
using MediatR;

namespace KodlamaDevs.Application.Features.ProgrammingLanguages.Commands
{
    public record UpdateProgrammingLanguageCommand(int Id, string Name) : IRequest<UpdatedProgrammingLanguageDTO>;

    public record UpdatedProgrammingLanguageDTO(int Id, string Name);

    public class UpdateProgrammingLanguageCommandHandler : IRequestHandler<UpdateProgrammingLanguageCommand, UpdatedProgrammingLanguageDTO>
    {
        private readonly IProgrammingLanguageRepository _repository;
        private readonly ProgrammingLanguageBusinessRules _rules;

        public UpdateProgrammingLanguageCommandHandler(IProgrammingLanguageRepository repository, ProgrammingLanguageBusinessRules rules)
        {
            _repository = repository;
            _rules = rules;
        }

        public async Task<UpdatedProgrammingLanguageDTO> Handle(UpdateProgrammingLanguageCommand request, CancellationToken cancellationToken)
        {
            await _rules.ProgrammingLanguageShouldExists(request.Id);
            await _rules.NameCannotBeDuplicated(request.Name);

            ProgrammingLanguage pl = new() { Id = request.Id, Name = request.Name };
            ProgrammingLanguage updatedPl = await _repository.UpdateAsync(pl);
            return new UpdatedProgrammingLanguageDTO(updatedPl.Id, updatedPl.Name);
        }
    }
}
