using Domain.Entities;
using Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Commands.ListAdverts;

public sealed class ListAdvertsHandler : IRequestHandler<ListAdvertsCommand, List<Advert>>
{
    private readonly IAdvertRepository _advertRepo;

    public ListAdvertsHandler(IAdvertRepository advertRepository)
    {
        _advertRepo = advertRepository;
    }
    public async Task<List<Advert>> Handle(ListAdvertsCommand request, CancellationToken cancellationToken)
    {
        var list = await _advertRepo.GetAllAsync();
        return list.ToList();
    }
}
