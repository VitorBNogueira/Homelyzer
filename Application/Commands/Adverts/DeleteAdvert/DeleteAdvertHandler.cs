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

public sealed class DeleteAdvertHandler : IRequestHandler<DeleteAdvertCommand, bool>
{
    private readonly IAdvertRepository _advertRepo;
    private readonly IMapper _mapper;

    public DeleteAdvertHandler(IAdvertRepository advertRepository, IMapper mapper)
    {
        _advertRepo = advertRepository;
        _mapper = mapper;
    }
    public async Task<bool> Handle(DeleteAdvertCommand request, CancellationToken cancellationToken)
    {
        try
        {
            await _advertRepo.RemoveByIdAsync(request.AdvertId);
            await _advertRepo.SaveChangesAsync();
        }
        catch (Exception)
        {
            return false;
        }

        return true;
    }
}
