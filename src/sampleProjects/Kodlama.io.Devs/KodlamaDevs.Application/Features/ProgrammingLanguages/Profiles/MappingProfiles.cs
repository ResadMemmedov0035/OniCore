using AutoMapper;
using KodlamaDevs.Application.Features.ProgrammingLanguages.Commands;
using KodlamaDevs.Application.Features.ProgrammingLanguages.Queries;
using KodlamaDevs.Domain.Entities;
using OniCore.Persistence.Pagination;

namespace KodlamaDevs.Application.Features.ProgrammingLanguages.Profiles
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<IPagedList<ProgrammingLanguage>, GetProgrammingLanguageListDTO>().ReverseMap();
            CreateMap<ProgrammingLanguage, GetProgrammingLanguageListItemDTO>().ReverseMap();
        }
    }
}
