using Application.DTOs.Advert;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Commands.Adverts;

public sealed class DeleteAdvertCommand : IRequest<bool>
{
    public DeleteAdvertCommand(int id)
    {
        AdvertId = id;
    }

    public int AdvertId { get; }
}
