using AutoMapper;
using KodlamaDevs.Application.Features.Developers.Commands;
using KodlamaDevs.Application.Features.Developers.DTOs;
using KodlamaDevs.Domain.Entities;
using OniCore.Persistence.Pagination;
using OniCore.Security.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        }
    }
}
