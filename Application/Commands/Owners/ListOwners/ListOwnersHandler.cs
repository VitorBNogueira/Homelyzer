using Application.Contracts;
using Application.Contracts.DTOs.Owner;
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

namespace Application.Commands.Owners;

public sealed class ListOwnersHandler : IRequestHandler<ListOwnersCommand, IResponse>
{
    private readonly IOwnerRepository _ownerRepo;
    private readonly IMapper _mapper;

    public ListOwnersHandler(IOwnerRepository ownerRepository, IMapper mapper)
    {
        _ownerRepo = ownerRepository;
        _mapper = mapper;
    }
    public async Task<IResponse> Handle(ListOwnersCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var list = await _ownerRepo.GetAllAsync();
            return new OwnerListResponse(_mapper.Map<List<OwnerDTO>>(list));
        }
        catch (DbException ex)
        {
            return ErrorResults.DatabaseError(ex.Message);
        }
        catch(Exception ex)
        {
            return ErrorResults.UnexpectedError(ex.Message);
        }
    }
}
