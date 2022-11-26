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

public sealed class ListAdvertsHandler : IRequestHandler<ListAdvertsCommand, List<AdvertDTOResponse>>
{
    private readonly IAdvertRepository _advertRepo;
    private readonly IMapper _mapper;

    public ListAdvertsHandler(IAdvertRepository advertRepository, IMapper mapper)
    {
        _advertRepo = advertRepository;
        _mapper = mapper;
    }
    public async Task<List<AdvertDTOResponse>> Handle(ListAdvertsCommand request, CancellationToken cancellationToken)
    {
        var list = await _advertRepo.GetAll_IncludePictures_Async();

        return _mapper.Map<List<AdvertDTOResponse>>(list);
    }
}
