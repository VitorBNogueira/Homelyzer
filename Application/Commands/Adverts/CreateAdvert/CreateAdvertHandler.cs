using Application.Contracts;
using Application.DTOs.Advert;
using Application.Interfaces;
using AutoMapper;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Commands.Adverts;

public sealed class CreateAdvertHandler : IRequestHandler<CreateAdvertCommand, IResponse>
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
            await _advertRepo.SaveChangesAsync();

            return Success.Instance;
        }
        catch (Exception x)
        {
            // to be implemented
            //return ErrorResult.something;
            return Success.Instance;
        }
    }

    private async Task<Owner> GetOrCreateOwnerIfNewAsync(CreateAdvertCommand request)
    {
        var dbOwner = await _ownerRepo.FindAsync(o =>
                    !string.IsNullOrWhiteSpace(request.Advert.OwnerName) && o.Name == request.Advert.OwnerName
                    || !string.IsNullOrWhiteSpace(request.Advert.EmailContact) && o.EmailContact == request.Advert.EmailContact
                    || !string.IsNullOrWhiteSpace(request.Advert.PhoneContact) && o.PhoneContact == request.Advert.PhoneContact
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