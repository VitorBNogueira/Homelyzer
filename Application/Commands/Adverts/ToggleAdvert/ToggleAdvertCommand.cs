using Application.Contracts;
using MediatR;

namespace Application.Commands.Adverts;

public sealed class ToggleAdvertCommand : IRequest<IResponse>
{
    public int AdvertId { get; }
    public bool Activate{ get; set; }

    public ToggleAdvertCommand(int advertId, bool activate)
    {
        AdvertId = advertId;
        Activate = activate;
    }
}
