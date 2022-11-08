using AutoMapper;
using KodlamaDevs.Application.Features.ProgrammingLanguages.Queries;
using KodlamaDevs.Domain.Entities;
using OniCore.Persistence.Pagination;

namespace KodlamaDevs.Application.Features.ProgrammingLanguages.Profiles
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<IPagedList<ProgrammingLanguage>, GetProgrammingLanguageListDTO>().ReverseMap();
            CreateMap<ProgrammingLanguage, GetProgrammingLanguageListItemDTO>().ReverseMap();
        }
    }
}
