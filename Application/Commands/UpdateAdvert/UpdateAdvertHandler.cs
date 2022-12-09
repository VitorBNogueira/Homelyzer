using Application.Common;
using Application.DTOs.Advert;
using AutoMapper;
using Domain.Entities;
using Domain.Enums;
using Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Commands.ListAdverts;

public sealed class UpdateAdvertHandler : IRequestHandler<UpdateAdvertCommand, bool>
{
    private readonly IAdvertRepository _advertRepo;
    private readonly IMapper _mapper;

    public UpdateAdvertHandler(IAdvertRepository repo, IMapper mapper)
    {
        _advertRepo = repo;
        _mapper = mapper;
    }
    public async Task<bool> Handle(UpdateAdvertCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var adToUpdate = _mapper.Map<Advert>(request.Advert);

            var ad = await _advertRepo.GetByIdAsync(adToUpdate.AdvertId);

            if (ad == null)
            {
                //throw new Exception("Advert not found.");
                return false;
            }


            if (DifferenceChecker.IsDifferent(ad, adToUpdate)){

                ad.Area = adToUpdate.Area;
                ad.Address = adToUpdate.Address;
                ad.Url = adToUpdate.Url;
                ad.IncludesBills = adToUpdate.IncludesBills;
                ad.Description = adToUpdate.Description;
                ad.MeetingTime = adToUpdate.MeetingTime;
                ad.Name = adToUpdate.Name;
                ad.Price = adToUpdate.Price;
                ad.Score = adToUpdate.Score;
                ad.PersonalNotes= adToUpdate.PersonalNotes;
                ad.Type = adToUpdate.Type;

                await _advertRepo.SaveChangesAsync();

                return true;
            }

            return false;
        }
        catch (Exception x)
        {
            return false;
        }

    }
}