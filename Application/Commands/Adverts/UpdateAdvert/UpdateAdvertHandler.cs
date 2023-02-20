using Application.Common;
using Application.Contracts;
using Application.DTOs.Advert;
using Application.Interfaces;
using AutoMapper;
using Domain.Entities;
using Domain.Enums;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Commands.Adverts;

public sealed class UpdateAdvertHandler : IRequestHandler<UpdateAdvertCommand, IResponse>
{
    private readonly IAdvertRepository _advertRepo;
    private readonly IMapper _mapper;

    public UpdateAdvertHandler(IAdvertRepository repo, IMapper mapper)
    {
        _advertRepo = repo;
        _mapper = mapper;
    }
    public async Task<IResponse> Handle(UpdateAdvertCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var adToUpdate = _mapper.Map<Advert>(request.Advert);

            var ad = await _advertRepo.GetByIdAsync(request.AdvertId);

            if (ad == null)
            {
                return ErrorResults.ResourceNotFound();
            }

            if (DifferenceChecker.IsDifferent(ad, adToUpdate))
            {

                ad.Area = adToUpdate.Area;
                ad.Address = adToUpdate.Address;
                ad.Url = adToUpdate.Url;
                ad.IncludesBills = adToUpdate.IncludesBills;
                ad.Description = adToUpdate.Description;
                ad.MeetingTime = adToUpdate.MeetingTime;
                ad.Name = adToUpdate.Name;
                ad.Price = adToUpdate.Price;
                ad.Score = adToUpdate.Score;
                ad.PersonalNotes = adToUpdate.PersonalNotes;
                ad.Type = adToUpdate.Type;

                await _advertRepo.SaveChangesAsync();

                return Success.Instance;
            }

            return ErrorResults.NothingToUpdate();
        }
        catch (Exception x)
        {
            return ErrorResults.UnexpectedError(x.Message);
        }
    }
}