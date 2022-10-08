using AutoMapper;
using KodlamaDevs.Application.Features.ProgrammingLanguages.Commands;
using KodlamaDevs.Application.Features.ProgrammingLanguages.DTOs;
using KodlamaDevs.Domain.Entities;
using OniCore.Persistence.Pagination;

namespace KodlamaDevs.Application.Features.ProgrammingLanguages.Profiles
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<ProgrammingLanguage, CreateProgrammingLanguageCommand>().ReverseMap();
            CreateMap<ProgrammingLanguage, CreatedProgrammingLanguageDTO>().ReverseMap();

            CreateMap<ProgrammingLanguage, UpdateProgrammingLanguageCommand>().ReverseMap();
            CreateMap<ProgrammingLanguage, UpdatedProgrammingLanguageDTO>().ReverseMap();

            CreateMap<ProgrammingLanguage, DeleteProgrammingLanguageCommand>().ReverseMap();
            CreateMap<ProgrammingLanguage, DeletedProgrammingLanguageDTO>().ReverseMap();

            CreateMap<IPagedList<ProgrammingLanguage>, GetProgrammingLanguageListDTO>().ReverseMap();
            CreateMap<ProgrammingLanguage, GetProgrammingLanguageListItemDTO>().ReverseMap();

            CreateMap<ProgrammingLanguage, GetProgrammingLanguageByIdDTO>().ReverseMap();
        }
    }
}
