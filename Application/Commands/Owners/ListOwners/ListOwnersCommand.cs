using Application.DTOs.Advert;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Commands.Owners;

public sealed class ListOwnersCommand : IRequest<List<OwnerDTO>>
{
}
