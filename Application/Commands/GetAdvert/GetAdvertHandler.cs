using Application.DTOs.Advert;
using AutoMapper;
using Domain.Entities;
using Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Commands.ListAdverts;

public sealed class GetAdvertHandler : IRequestHandler<GetAdvertCommand, AdvertDTO>
{
    private readonly IAdvertRepository _advertRepo;
    private readonly IMapper _mapper;

    public GetAdvertHandler(IAdvertRepository advertRepository, IMapper mapper)
    {
        _advertRepo = advertRepository;
        _mapper = mapper;
    }
    public async Task<AdvertDTO> Handle(GetAdvertCommand request, CancellationToken cancellationToken)
    {
        var ad = await _advertRepo.GetById_Complete_Async(request.Id);

        return _mapper.Map<AdvertDTO>(ad);
    }
}
