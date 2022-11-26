using Application.DTOs.Advert;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Commands.ListAdverts;

public sealed class UpdateAdvertCommand : IRequest<bool>
{
    public AdvertDTO Advert { get; set; }

	public UpdateAdvertCommand(AdvertDTO ad)
	{
        Advert = ad;

    }
}
