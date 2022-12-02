using Application.DTOs.Advert;
using AutoMapper;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace API;

public sealed class Mappings : Profile
{
    public Mappings()
    {
        CreateMap<string, Picture>()
            .ForMember(dest => dest.Url, opt => opt.MapFrom(src => src))
            ; 
        CreateMap<AdvertDTO, Advert>()
            .ForMember(dest => dest.Pictures, act => act.MapFrom(src => src.Pictures))
            //.ReverseMap()
            ;

        CreateMap<Advert, AdvertDTO>()
            .ForMember(dest => dest.Pictures, act => act.MapFrom(src => src.Pictures.Select(s => s.Url)))
            .ForMember(dest => dest.PhoneContact, act => act.MapFrom(src => src.Owner.PhoneContact))
            .ForMember(dest => dest.EmailContact, act => act.MapFrom(src => src.Owner.EmailContact))
            ;

        CreateMap<AdvertDTO, Owner>()
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.OwnerName))
            ;

        CreateMap<Picture, PictureDTO>().ReverseMap();

        CreateMap<Advert, AdvertDTOResponse>()
            .ForMember(dest => dest.Pictures, act => act.MapFrom(src => src.Pictures.Select(s => s.Url)))
            .ForMember(dest => dest.Type, act => act.MapFrom(src => (int)src.Type));
        ;

        CreateMap<AdvertDTO, OwnerDTO>()
            .ForMember(dest => dest.Name, act => act.MapFrom(src => src.OwnerName));

        CreateMap<Owner, OwnerDTO>().ReverseMap();
    }
}
