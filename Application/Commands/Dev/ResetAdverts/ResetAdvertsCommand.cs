using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Commands.Dev.ResetAdverts;

public sealed class ResetAdvertsCommand : IRequest<bool>
{
}
