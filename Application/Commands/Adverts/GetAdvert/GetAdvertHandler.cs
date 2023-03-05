using Application.Contracts;
using Application.Contracts.DTOs.Advert;
using Application.Contracts.Responses;
using Application.Interfaces;
using AutoMapper;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Commands.Adverts;

public sealed class GetAdvertHandler : IRequestHandler<GetAdvertCommand, IResponse>
{
    private readonly IAdvertRepository _advertRepo;
    private readonly IMapper _mapper;

    public GetAdvertHandler(IAdvertRepository advertRepository, IMapper mapper)
    {
        _advertRepo = advertRepository;
        _mapper = mapper;
    }
    public async Task<IResponse> Handle(GetAdvertCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var ad = await _advertRepo.GetById_Complete_Async(request.Id);

            if (ad == null)
            {
                return ErrorResults.ResourceNotFound();
            }

            return new AdvertResponse(_mapper.Map<AdvertDTO>(ad));
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
