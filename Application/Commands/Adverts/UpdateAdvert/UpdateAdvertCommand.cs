using Application.Contracts;
using Application.DTOs.Advert;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Commands.Adverts;

public sealed class UpdateAdvertCommand : IRequest<IResponse>
{
    public AdvertDTO Advert { get; set; }
    public int AdvertId { get; set; }

    public UpdateAdvertCommand(AdvertDTO ad, int id)
    {
        Advert = ad;
        AdvertId = id;
    }
}
