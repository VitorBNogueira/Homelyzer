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

public sealed class GetAdvertCommand : IRequest<IResponse>
{
    public GetAdvertCommand(int id)
    {
        Id = id;
    }

    public int Id { get; }
}
