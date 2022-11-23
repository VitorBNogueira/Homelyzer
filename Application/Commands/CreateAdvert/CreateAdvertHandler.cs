using AutoMapper;
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
    private readonly IMapper _mapper;

    public CreateAdvertHandler(IAdvertRepository repo, IMapper mapper)
    {
        _advertRepo = repo;
        this._mapper = mapper;
    }
    public async Task<bool> Handle(CreateAdvertCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var advert = _mapper.Map<Advert>(request.Advert);

            await _advertRepo.AddAsync(advert);
            await _advertRepo.SaveChangesAsync();
        }
        catch (Exception)
        {
            return false;
        }

        return true;
    }
}