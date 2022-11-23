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

public sealed class GetAndCreateOwnerIfNewHandler : IRequestHandler<GetAndCreateOwnerIfNewCommand, OwnerDTO>
{
    private readonly IOwnerRepository _ownerRepo;
    private readonly IMapper _mapper;

    public GetAndCreateOwnerIfNewHandler(IOwnerRepository repo, IMapper mapper)
    {
        _ownerRepo = repo;
        _mapper = mapper;
    }
    public async Task<OwnerDTO> Handle(GetAndCreateOwnerIfNewCommand request, CancellationToken cancellationToken)
    {
        try
        {
            // return Owner if it already exists
            var dbOwner = await _ownerRepo.FindAsync(o => 
                o.Name == request.Owner.Name
                || o.EmailContact == request.Owner.EmailContact
                || o.PhoneContact == request.Owner.PhoneContact
            );

            if (dbOwner.Any())
            {
                if (dbOwner.Count() >1)
                {
                    throw new Exception("There is more than one Owner with the same name / email / number.");
                }

                return _mapper.Map<OwnerDTO>(dbOwner.FirstOrDefault());
            }

            // create new and then return it, otherwise
            var newOwner = _mapper.Map<Owner>(request.Owner);
            await _ownerRepo.AddAsync(newOwner);
            await _ownerRepo.SaveChangesAsync();

            newOwner = (await _ownerRepo.FindAsync(o => o.Name == newOwner.Name)).FirstOrDefault();

            return _mapper.Map<OwnerDTO>(newOwner);
        }
        catch (Exception x)
        {
            throw x;
        }
    }
}