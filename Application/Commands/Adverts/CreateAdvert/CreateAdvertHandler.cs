﻿using Application.Contracts;
using Application.Exceptions;
using Application.Interfaces;
using AutoMapper;
using Domain.Entities;
using MediatR;
using System.Data.Common;

namespace Application.Commands.Adverts;

public sealed class CreateAdvertHandler : IRequestHandler<CreateAdvertCommand, IResponse>
{
    private readonly IAdvertRepository _advertRepo;
    private readonly IOwnerRepository _ownerRepo;
    private readonly IPictureRepository _picRepo;
    private readonly IMapper _mapper;

    public CreateAdvertHandler(IAdvertRepository adRepo, IOwnerRepository ownerRepo, IPictureRepository picRepo, IMapper mapper)
    {
        _advertRepo = adRepo;
        _ownerRepo = ownerRepo;
        _picRepo = picRepo;
        _mapper = mapper;
    }
    public async Task<IResponse> Handle(CreateAdvertCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var adOwner = new Owner();

            // Get or Create and Get Owner if it doesn't exist yet 
            adOwner = await GetOrCreateOwnerIfNewAsync(request);
            request.Advert.OwnerId = adOwner.OwnerId;

            // Create new Advert instance
            var advert = _mapper.Map<Advert>(request.Advert);
            advert.Owner = null;

            await _advertRepo.AddAsync(advert);
            var count = await _advertRepo.SaveChangesAsync();

            if (count == 0)
                return ErrorResults.DidNotCreateResource($"Advert: {advert.Name}");

            return Success.Instance;
        }
        catch (DbException ex)
        {
            return ErrorResults.DatabaseError(ex.Message);
        }
        catch (OwnerException ex)
        {
            return ErrorResults.DataConsistencyError(ex.Message);
        }
        catch (Exception ex)
        {
            return ErrorResults.UnexpectedError(ex.Message);
        }
    }

    private async Task<Owner> GetOrCreateOwnerIfNewAsync(CreateAdvertCommand request)
    {
        var dbOwner = new Owner();
        // Search for an owner with the same Name or Email or Phone Number
        try
        {
            dbOwner = (await _ownerRepo.FindAsync(o =>
                    !string.IsNullOrWhiteSpace(request.Advert.OwnerName) && (o.Name == request.Advert.OwnerName
                        || !string.IsNullOrWhiteSpace(request.Advert.EmailContact) && o.EmailContact == request.Advert.EmailContact
                        || !string.IsNullOrWhiteSpace(request.Advert.PhoneContact) && o.PhoneContact == request.Advert.PhoneContact)
                    && o.IsActive
                )).SingleOrDefault();
        }
        catch (Exception)
        {
            throw new RepeatingOwnerException("There is more than one Owner with the same name / email / number.");
        }

        if (dbOwner is not null)
                return dbOwner;

        // otherwise, create new and then return it
        var newOwner = new Owner();
        newOwner = _mapper.Map<Owner>(request.Advert);
        newOwner.IsActive = true;

        await _ownerRepo.AddAsync(newOwner);
        await _ownerRepo.SaveChangesAsync();

        newOwner = (await _ownerRepo.FindAsync(o => o.Name == newOwner.Name)).FirstOrDefault();

        return newOwner;
    }
}