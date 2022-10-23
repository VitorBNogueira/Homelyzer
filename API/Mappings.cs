﻿using API.DTOs.Adverts;
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
        CreateMap<NewAdvertDTORequest, Advert>();
        CreateMap<AdvertDTO, Advert>().ReverseMap();
    }
}
