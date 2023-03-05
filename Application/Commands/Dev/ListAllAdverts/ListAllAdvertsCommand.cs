using Application.Contracts;
using MediatR;

namespace Application.Commands.Dev.ListAllAdverts;

public sealed class ListAllAdvertsCommand : IRequest<IResponse>
{
}
