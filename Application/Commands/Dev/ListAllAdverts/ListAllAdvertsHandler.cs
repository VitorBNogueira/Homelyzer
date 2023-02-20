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

namespace Application.Commands.Dev.ListAllAdverts;

public sealed class ListAllAdvertsHandler : IRequestHandler<ListAllAdvertsCommand, IResponse>
{
    private readonly IAdvertRepository _advertRepo;
    private readonly IMapper _mapper;

    public ListAllAdvertsHandler(IAdvertRepository advertRepository, IMapper mapper)
    {
        _advertRepo = advertRepository;
        _mapper = mapper;
    }
    public async Task<IResponse> Handle(ListAllAdvertsCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var list = await _advertRepo.GetAll_Complete_Async();

            return new AdvertListResponse(_mapper.Map<List<AdvertDTO>>(list));
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
