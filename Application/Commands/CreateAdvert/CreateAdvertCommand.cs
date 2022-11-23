using Application.DTOs.Advert;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Commands.ListAdverts;

public sealed class CreateAdvertCommand : IRequest<bool>
{
    public NewAdvertDTORequest Advert { get; set; }

	public CreateAdvertCommand(NewAdvertDTORequest ad)
	{
        Advert = ad;

    }
}
