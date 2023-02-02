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

public sealed class ToggleAdvertHandler : IRequestHandler<ToggleAdvertCommand, IResponse>
{
    private readonly IAdvertRepository _advertRepo;

    public ToggleAdvertHandler(IAdvertRepository repo, IMapper mapper)
    {
        _advertRepo = repo;
    }
    public async Task<IResponse> Handle(ToggleAdvertCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var ad = await _advertRepo.GetByIdAsync(request.AdvertId);

            if (ad == null)
            {
                // to be implemented
                //return ErrorResult.something;
            }

            ad.IsActive = request.IsActive;

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
    }