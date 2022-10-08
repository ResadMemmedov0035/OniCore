using AutoMapper;
using KodlamaDevs.Application.Features.ProgrammingLanguages.DTOs;
using KodlamaDevs.Application.Features.ProgrammingLanguages.Rules;
using KodlamaDevs.Application.Services.Repositories;
using KodlamaDevs.Domain.Entities;
using MediatR;

namespace KodlamaDevs.Application.Features.ProgrammingLanguages.Commands
{
    public class UpdateProgrammingLanguageCommand : IRequest<UpdatedProgrammingLanguageDTO>
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
    }

    public class UpdateProgrammingLanguageCommandHandler : IRequestHandler<UpdateProgrammingLanguageCommand, UpdatedProgrammingLanguageDTO>
    {
        private readonly IProgrammingLanguageRepository _repository;
        private readonly IMapper _mapper;
        private readonly ProgrammingLanguageBusinessRules _businessRules;

        public UpdateProgrammingLanguageCommandHandler(IProgrammingLanguageRepository repository, IMapper mapper,
            ProgrammingLanguageBusinessRules businessRules)
        {
            _repository = repository;
            _mapper = mapper;
            _businessRules = businessRules;
        }

        public async Task<UpdatedProgrammingLanguageDTO> Handle(UpdateProgrammingLanguageCommand request, CancellationToken cancellationToken)
        {
            await _businessRules.NameCannotBeDuplicated(request.Name);

            ProgrammingLanguage language = _mapper.Map<ProgrammingLanguage>(request);
            ProgrammingLanguage updatedLanguage = await _repository.UpdateAsync(language);
            return _mapper.Map<UpdatedProgrammingLanguageDTO>(updatedLanguage);
        }
    }
}
