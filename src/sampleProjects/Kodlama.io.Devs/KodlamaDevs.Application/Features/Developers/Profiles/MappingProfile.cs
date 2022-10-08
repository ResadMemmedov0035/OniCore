using AutoMapper;
using KodlamaDevs.Application.Features.Developers.Commands;
using KodlamaDevs.Application.Features.Developers.DTOs;
using KodlamaDevs.Domain.Entities;
using OniCore.Persistence.Pagination;
using OniCore.Security.Entities;

namespace KodlamaDevs.Application.Features.Developers.Profiles
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<RegisterDeveloperCommand, Developer>().ReverseMap();

            CreateMap<Developer, AuthorizedDeveloperDTO>();

            CreateMap<Developer, DeletedDeveloperDTO>().ReverseMap();

            CreateMap<Developer, UpdateDeveloperCommand>().ReverseMap();
            CreateMap<Developer, UpdatedDeveloperDTO>().ReverseMap();

            CreateMap<IPagedList<Developer>, GetDeveloperListDTO>().ReverseMap();
            CreateMap<Developer, GetDeveloperListItemDTO>().ReverseMap();

            CreateMap<Developer, GetDeveloperByIdDTO>().ReverseMap();

            CreateMap<OperationClaim, GetDeveloperClaimListItemDTO>().ReverseMap();
            CreateMap<IPagedList<OperationClaim>, GetDeveloperClaimListDTO>().ReverseMap();
        }
    }
}
