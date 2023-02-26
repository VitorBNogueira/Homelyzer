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
using System.Data.Common;
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
                return ErrorResults.ResourceNotFound();

            ad.IsActive = request.Activate;

            var changes = await _advertRepo.SaveChangesAsync();

            if (changes == 0)
                return ErrorResults.NothingChanged();

            return Success.Instance;
        }
        catch (DbException ex)
        {
            return ErrorResults.DatabaseError(ex.Message);
        }
        catch (Exception x)
        {
            return ErrorResults.UnexpectedError(x.Message);
        }
    }
}