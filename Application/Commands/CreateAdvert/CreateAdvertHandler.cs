using Domain.Entities;
using Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Commands.ListAdverts;

public sealed class CreateAdvertHandler : IRequestHandler<CreateAdvertCommand, bool>
{
    private readonly IAdvertRepository _advertRepo;
    public CreateAdvertHandler(IAdvertRepository repo)
    {
        _advertRepo = repo;
    }
    public async Task<bool> Handle(CreateAdvertCommand request, CancellationToken cancellationToken)
    {
        try
        {
            await _advertRepo.AddAsync(request.Advert);
        }
        catch (Exception)
        {
            return false;
        }

        return true;
    }
}