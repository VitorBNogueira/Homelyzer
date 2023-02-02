using Application.Contracts;
using Application.DTOs;
using Application.DTOs.Advert;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Commands.Adverts;

public sealed class ListAdvertsCommand : IRequest<IResponse>
{
	public ListAdvertsCommand(SortDTO sort)
	{
        Sort = sort;
    }

    public SortDTO Sort { get; }
}
