﻿using Application.Contracts;
using Application.DTOs.Advert;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Commands.Adverts;

public sealed class CreateAdvertCommand : IRequest<IResponse>
{
    public AdvertDTO Advert { get; set; }

    public CreateAdvertCommand(AdvertDTO ad)
    {
        Advert = ad;
    }
}
