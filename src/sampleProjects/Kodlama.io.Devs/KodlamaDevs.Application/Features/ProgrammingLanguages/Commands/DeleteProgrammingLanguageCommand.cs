using AutoMapper;
using KodlamaDevs.Application.Features.ProgrammingLanguages.DTOs;
using KodlamaDevs.Application.Services.Repositories;
using KodlamaDevs.Domain.Entities;
using MediatR;

namespace KodlamaDevs.Application.Features.ProgrammingLanguages.Commands
{
    public class DeleteProgrammingLanguageCommand : IRequest<DeletedProgrammingLanguageDTO>
    {
        public int Id { get; set; }
    }

    public class DeleteProgrammingLanguageCommandHandler : IRequestHandler<DeleteProgrammingLanguageCommand, DeletedProgrammingLanguageDTO>
    {
        private readonly IProgrammingLanguageRepository _repository;
        private readonly IMapper _mapper;

        public DeleteProgrammingLanguageCommandHandler(IProgrammingLanguageRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<DeletedProgrammingLanguageDTO> Handle(DeleteProgrammingLanguageCommand request, CancellationToken cancellationToken)
        {
            ProgrammingLanguage language = _mapper.Map<ProgrammingLanguage>(request);
            ProgrammingLanguage deletedLanguage = await _repository.DeleteAsync(language);
            return _mapper.Map<DeletedProgrammingLanguageDTO>(deletedLanguage);
        }
    }
}
