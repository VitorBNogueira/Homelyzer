using Application.Contracts;
using MediatR;

namespace Application.Commands.Adverts;

public sealed class GetAdvertCommand : IRequest<IResponse>
{
    public GetAdvertCommand(int id)
    {
        Id = id;
    }

    public int Id { get; }
}
