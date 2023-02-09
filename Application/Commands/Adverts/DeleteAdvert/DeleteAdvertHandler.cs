using Application.Contracts;
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
            var changes = await _advertRepo.SaveChangesAsync();

            if (changes == 0)
                return ErrorResults.ResourceNotFound();
        }
        catch (DbException ex)
        {
            return ErrorResults.DatabaseError(ex.Message);
        }
        catch (Exception x)
        {
            return ErrorResults.UnexpectedError(x.Message);
        }

        return Success.Instance;
    }
}
