using Application.Contracts;
using MediatR;

namespace Application.Commands.Adverts;

public sealed class DeleteAdvertCommand : IRequest<IResponse>
{
    public DeleteAdvertCommand(int id)
    {
        AdvertId = id;
    }

    public int AdvertId { get; }
}
