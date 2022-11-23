using Application.DTOs.Advert;
using AutoMapper;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API;

public sealed class Mappings : Profile
{
    public Mappings()
    {
        //CreateMap<List<Advert>, List<AdvertDTO>>();
        CreateMap<string, Picture>()
            .ForMember(dest => dest.Url, opt => opt.MapFrom(src => src))
            ; 
        CreateMap<NewAdvertDTORequest, Advert>()
            .ForMember(dest => dest.Pictures, act => act.MapFrom(src => src.Pictures))
            ;

        

        //CreateMap<List<string>, List<Picture>>();


        CreateMap<AdvertDTO, Advert>().ReverseMap();

        CreateMap<Advert, AdvertDTOResponse>()
            .ForMember(dest => dest.Pictures, act => act.MapFrom(src => src.Pictures.Select(s => s.Url)))
            .ForMember(dest => dest.Type, act => act.MapFrom(src => (int)src.Type));
        ;

        CreateMap<NewAdvertDTORequest, OwnerDTO>()
            .ForMember(dest => dest.Name, act => act.MapFrom(src => src.OwnerName));

        CreateMap<Owner, OwnerDTO>().ReverseMap();
    }
}
