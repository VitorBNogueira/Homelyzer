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

namespace Application.Commands.Adverts;

public sealed class ToggleAdvertHandler : IRequestHandler<ToggleAdvertCommand, bool>
{
    private readonly IAdvertRepository _advertRepo;

    public ToggleAdvertHandler(IAdvertRepository repo, IMapper mapper)
    {
        _advertRepo = repo;
    }
    public async Task<bool> Handle(ToggleAdvertCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var ad = await _advertRepo.GetByIdAsync(request.AdvertId);

            if (ad == null)
            {
                //throw new Exception("Advert not found.");
                return false;
            }

            ad.IsActive = request.IsActive;

            await _advertRepo.SaveChangesAsync();

            return true;
        }
        catch (Exception x)
        {
            return false;
        }

    }
}