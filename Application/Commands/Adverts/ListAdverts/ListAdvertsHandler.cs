using Application.Common;
using Application.Contracts;
using Application.Contracts.Responses;
using Application.DTOs.Advert;
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

public sealed class ListAdvertsHandler : IRequestHandler<ListAdvertsCommand, IResponse>
{
    private readonly IAdvertRepository _advertRepo;
    private readonly IMapper _mapper;

    public ListAdvertsHandler(IAdvertRepository advertRepository, IMapper mapper)
    {
        _advertRepo = advertRepository;
        _mapper = mapper;
    }
    public async Task<IResponse> Handle(ListAdvertsCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var list = await _advertRepo.GetAllActive_Complete_Async();

            list = FilterAndOrder.Order<Advert>(list.AsQueryable(), request.Sort);

            return new AdvertListResponse(_mapper.Map<IEnumerable<AdvertDTO>>(list));
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
