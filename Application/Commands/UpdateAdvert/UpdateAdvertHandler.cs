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
            var ad = await _advertRepo.GetByIdAsync(request.Advert.AdvertId);

            if (ad == null)
            {
                //throw new Exception("Advert not found.");
                return false;
            }

            ad.Area = request.Advert.Area;
            ad.Address = request.Advert.Address;
            ad.Url = request.Advert.Url;
            ad.IncludesBills = request.Advert.IncludesBills;
            ad.Description = request.Advert.Description;
            ad.MeetingTime = request.Advert.MeetingTime;
            ad.Name = request.Advert.Name;
            ad.Price = request.Advert.Price;
            ad.Score = request.Advert.Score;
            ad.Type = (Domain.Enums.EAdvertType)(request.Advert.Type ?? 0);

            await _advertRepo.SaveChangesAsync();

            return true;
        }
        catch (Exception x)
        {
            return false;
        }

    }
}