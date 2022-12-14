using Application.DTOs.Advert;
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
    private readonly IOwnerRepository _ownerRepo;
    private readonly IPictureRepository _picRepo;
    private readonly IMapper _mapper;

    public CreateAdvertHandler(IAdvertRepository repo, IOwnerRepository ownerRepo, IPictureRepository picRepo, IMapper mapper)
    {
        _advertRepo = repo;
        _ownerRepo = ownerRepo;
        _picRepo = picRepo;
        this._mapper = mapper;
    }
    public async Task<bool> Handle(CreateAdvertCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var adOwner = new Owner();

            // Get or Create and Get Owner if it doesn't exist yet 
            adOwner = await GetOrCreateOwnerIfNewAsync(request);
            request.Advert.OwnerId = adOwner.OwnerId;

            // Create new Advert
            var advert = _mapper.Map<Advert>(request.Advert);
            advert.Owner = null;

            await _advertRepo.AddAsync(advert);
            await _advertRepo.SaveChangesAsync();

            return true;
        }
        catch (Exception x)
        {
            return false;
        }

    }

    private async Task<Owner> GetOrCreateOwnerIfNewAsync(CreateAdvertCommand request)
    {
        var dbOwner = await _ownerRepo.FindAsync(o =>
                    o.Name == request.Advert.OwnerName
                    || o.EmailContact == request.Advert.EmailContact
                    || o.PhoneContact == request.Advert.PhoneContact
                );

        if (dbOwner.Any())
        {
            if (dbOwner.Count() > 1)
            {
                throw new Exception("There is more than one Owner with the same name / email / number.");
            }

            return dbOwner.FirstOrDefault();
        }

        // otherwise, create new and then return it
        var newOwner = new Owner();
        newOwner = _mapper.Map<Owner>(request.Advert);
        await _ownerRepo.AddAsync(newOwner);
        await _ownerRepo.SaveChangesAsync();

        newOwner = (await _ownerRepo.FindAsync(o => o.Name == newOwner.Name)).FirstOrDefault();

        return newOwner;
    }
}