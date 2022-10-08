using AutoMapper;
using KodlamaDevs.Application.Features.Technologies.Commands;
using KodlamaDevs.Application.Features.Technologies.DTOs;
using KodlamaDevs.Domain.Entities;
using OniCore.Persistence.Pagination;

namespace KodlamaDevs.Application.Features.Technologies.Profiles
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<Technology, CreateTechnologyCommand>().ReverseMap();
            CreateMap<Technology, CreatedTechnologyDTO>().ReverseMap();

            CreateMap<Technology, UpdateTechnologyCommand>().ReverseMap();
            CreateMap<Technology, UpdatedTechnologyDTO>().ReverseMap();

            CreateMap<Technology, DeleteTechnologyCommand>().ReverseMap();
            CreateMap<Technology, DeletedTechnologyDTO>().ReverseMap();

            CreateMap<Technology, GetTechnologyByIdDTO>().ReverseMap();

            CreateMap<IPagedList<Technology>, GetTechnologyListDTO>().ReverseMap();
            CreateMap<Technology, GetTechnologyListItemDTO>().ReverseMap();
        }
    }
}
