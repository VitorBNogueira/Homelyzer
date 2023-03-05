using Application.Contracts;
using Application.Contracts.DTOs;
using MediatR;

namespace Application.Commands.Adverts;

public sealed class ListAdvertsCommand : IRequest<IResponse>
{
	public ListAdvertsCommand(SortDTO sort)
	{
        Sort = sort;
    }

    public SortDTO Sort { get; }
}
