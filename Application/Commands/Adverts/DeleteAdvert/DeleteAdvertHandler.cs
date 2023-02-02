using Application.Contracts;
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

public sealed class DeleteAdvertHandler : IRequestHandler<DeleteAdvertCommand, IResponse>
{
    private readonly IAdvertRepository _advertRepo;
    private readonly IMapper _mapper;

    public DeleteAdvertHandler(IAdvertRepository advertRepository, IMapper mapper)
    {
        _advertRepo = advertRepository;
        _mapper = mapper;
    }
    public async Task<IResponse> Handle(DeleteAdvertCommand request, CancellationToken cancellationToken)
    {
        try
        {
            await _advertRepo.RemoveByIdAsync(request.AdvertId);
            await _advertRepo.SaveChangesAsync();
        }
        catch (Exception)
        {
            // to be implemented
            //return ErrorResult.something;
            return Success.Instance;
        }

        return Success.Instance;
    }
}
