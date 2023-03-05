using Application.Contracts;
using MediatR;

namespace Application.Commands.Owners;

public sealed class ListOwnersCommand : IRequest<IResponse>
{
}
