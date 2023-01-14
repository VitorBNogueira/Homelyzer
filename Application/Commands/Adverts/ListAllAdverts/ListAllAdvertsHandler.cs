using Application.DTOs.Advert;
using Application.Interfaces;
using AutoMapper;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Commands.Adverts;

public sealed class ListAllAdvertsHandler : IRequestHandler<ListAllAdvertsCommand, List<AdvertDTO>>
{
    private readonly IAdvertRepository _advertRepo;
    private readonly IMapper _mapper;

    public ListAllAdvertsHandler(IAdvertRepository advertRepository, IMapper mapper)
    {
        _advertRepo = advertRepository;
        _mapper = mapper;
    }
    public async Task<List<AdvertDTO>> Handle(ListAllAdvertsCommand request, CancellationToken cancellationToken)
    {
        var list = await _advertRepo.GetAll_Complete_Async();

        return _mapper.Map<List<AdvertDTO>>(list);
    }
}
