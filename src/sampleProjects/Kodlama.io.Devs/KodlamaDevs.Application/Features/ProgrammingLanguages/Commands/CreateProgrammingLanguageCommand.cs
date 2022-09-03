using AutoMapper;
using KodlamaDevs.Application.Features.ProgrammingLanguages.DTOs;
using KodlamaDevs.Application.Features.ProgrammingLanguages.Rules;
using KodlamaDevs.Application.Services.Repositories;
using KodlamaDevs.Domain.Entities;
using MediatR;

namespace KodlamaDevs.Application.Features.ProgrammingLanguages.Commands
{
    public class CreateProgrammingLanguageCommand : IRequest<CreatedProgrammingLanguageDTO>
    {
        public string Name { get; set; } = string.Empty;
    }

    public class CreateProgrammingLanguageCommandHandler : IRequestHandler<CreateProgrammingLanguageCommand, CreatedProgrammingLanguageDTO>
    {
        private readonly IProgrammingLanguageRepository _repository;
        private readonly IMapper _mapper;
        private readonly ProgrammingLanguageBusinessRules _businessRules;

        public CreateProgrammingLanguageCommandHandler(IProgrammingLanguageRepository repository, IMapper mapper, 
            ProgrammingLanguageBusinessRules businessRules)
        {
            _repository = repository;
            _mapper = mapper;
            _businessRules = businessRules;
        }

        public async Task<CreatedProgrammingLanguageDTO> Handle(CreateProgrammingLanguageCommand request, CancellationToken cancellationToken)
        {
            await _businessRules.NameCannotBeDuplicated(request.Name);

            ProgrammingLanguage language = _mapper.Map<ProgrammingLanguage>(request);
            ProgrammingLanguage addedLanguage = await _repository.AddAsync(language);
            return _mapper.Map<CreatedProgrammingLanguageDTO>(addedLanguage);
        }
    }
}
