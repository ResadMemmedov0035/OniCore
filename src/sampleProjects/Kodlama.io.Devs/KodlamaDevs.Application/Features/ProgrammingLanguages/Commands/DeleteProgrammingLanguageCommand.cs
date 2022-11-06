using AutoMapper;
using KodlamaDevs.Application.Features.ProgrammingLanguages.DTOs;
using KodlamaDevs.Application.Features.ProgrammingLanguages.Rules;
using KodlamaDevs.Application.Services.Repositories;
using KodlamaDevs.Domain.Entities;
using MediatR;

namespace KodlamaDevs.Application.Features.ProgrammingLanguages.Commands
{
    public record DeleteProgrammingLanguageCommand(int Id) : IRequest<DeletedProgrammingLanguageDTO>;

    public record DeletedProgrammingLanguageDTO(int Id);

    public class DeleteProgrammingLanguageCommandHandler : IRequestHandler<DeleteProgrammingLanguageCommand, DeletedProgrammingLanguageDTO>
    {
        private readonly IProgrammingLanguageRepository _repository;
        private readonly ProgrammingLanguageBusinessRules _rules;

        public DeleteProgrammingLanguageCommandHandler(IProgrammingLanguageRepository repository, ProgrammingLanguageBusinessRules rules)
        {
            _repository = repository;
            _rules = rules;
        }

        public async Task<DeletedProgrammingLanguageDTO> Handle(DeleteProgrammingLanguageCommand request, CancellationToken cancellationToken)
        {
            await _rules.ProgrammingLanguageShouldExists(request.Id);

            ProgrammingLanguage pl = new() { Id = request.Id };
            _ = await _repository.DeleteAsync(pl);
            return new DeletedProgrammingLanguageDTO(pl.Id);
        }
    }
}
