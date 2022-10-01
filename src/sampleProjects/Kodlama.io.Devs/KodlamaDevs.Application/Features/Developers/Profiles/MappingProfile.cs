using AutoMapper;
using KodlamaDevs.Application.Features.Developers.Commands;
using KodlamaDevs.Application.Features.Developers.DTOs;
using KodlamaDevs.Domain.Entities;
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
            CreateMap<AccessToken, AuthorizedDeveloperDTO>();
        }
    }
}
