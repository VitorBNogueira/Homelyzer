using Application.Contracts;
using Application.DTOs.Advert;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Commands.Dev.ListAllAdverts;

public sealed class ListAllAdvertsCommand : IRequest<IResponse>
{
}
