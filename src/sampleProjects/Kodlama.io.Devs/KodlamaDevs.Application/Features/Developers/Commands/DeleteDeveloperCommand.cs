using AutoMapper;
using KodlamaDevs.Application.Features.Developers.DTOs;
using KodlamaDevs.Application.Services.Repositories;
using KodlamaDevs.Domain.Entities;
using MediatR;
using OniCore.Application.Pipelines.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KodlamaDevs.Application.Features.Developers.Commands
{
    public class DeleteDeveloperCommand : IRequest<DeletedDeveloperDTO>, ISecuredRequest
    {
        public int Id { get; set; }

        public string[] RequiredRoles => new[] { "admin" };
    }

    public class DeleteDeveloperCommandHandler : IRequestHandler<DeleteDeveloperCommand, DeletedDeveloperDTO>
    {
        private readonly IDeveloperRepository _developerRepository;
        private readonly IMapper _mapper;

        public DeleteDeveloperCommandHandler(IDeveloperRepository developerRepository, IMapper mapper)
        {
            _developerRepository = developerRepository;
            _mapper = mapper;
        }

        public async Task<DeletedDeveloperDTO> Handle(DeleteDeveloperCommand request, CancellationToken cancellationToken)
        {
            Developer deletedDeveloper = await _developerRepository.DeleteAsync(new() { Id = request.Id });
            return _mapper.Map<DeletedDeveloperDTO>(deletedDeveloper);
        }
    }
}
