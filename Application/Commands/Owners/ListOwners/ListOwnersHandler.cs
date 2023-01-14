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

namespace Application.Commands.Owners;

public sealed class ListOwnersHandler : IRequestHandler<ListOwnersCommand, List<OwnerDTO>>
{
    private readonly IOwnerRepository _ownerRepo;
    private readonly IMapper _mapper;

    public ListOwnersHandler(IOwnerRepository ownerRepository, IMapper mapper)
    {
        _ownerRepo = ownerRepository;
        _mapper = mapper;
    }
    public async Task<List<OwnerDTO>> Handle(ListOwnersCommand request, CancellationToken cancellationToken)
    {
        var list = await _ownerRepo.GetAllAsync();

        return _mapper.Map<List<OwnerDTO>>(list);
    }
}
