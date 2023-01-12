using Application.DTOs.Advert;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Commands.Adverts;

public sealed class ToggleAdvertCommand : IRequest<bool>
{
    public int AdvertId { get; }
    public bool IsActive{ get; set; }

    public ToggleAdvertCommand(int advertId, bool isActive)
    {
        AdvertId = advertId;
        IsActive = isActive;
    }
}
