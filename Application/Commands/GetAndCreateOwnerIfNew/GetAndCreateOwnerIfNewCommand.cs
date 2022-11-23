﻿using Application.DTOs.Advert;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Commands.ListAdverts;

public sealed class GetAndCreateOwnerIfNewCommand : IRequest<OwnerDTO>
{
    public OwnerDTO Owner { get; set; }

	public GetAndCreateOwnerIfNewCommand(OwnerDTO owner)
	{
        Owner= owner;
    }
}


