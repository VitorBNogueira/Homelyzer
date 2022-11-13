using Domain.Entities;
using Domain.Enums;
using Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Commands.ListAdverts;

public sealed class ResetAdvertsHandler : IRequestHandler<ResetAdvertsCommand, bool>
{
    private readonly IAdvertRepository _advertRepo;
    private readonly IOwnerRepository _ownerRepo;
    public ResetAdvertsHandler(IAdvertRepository adRepo, IOwnerRepository ownerRepo)
    {
        _advertRepo = adRepo;
        _ownerRepo = ownerRepo;
    }
    public async Task<bool> Handle(ResetAdvertsCommand request, CancellationToken cancellationToken)
    {
        try
        {
            await _advertRepo.ClearAllAdverts();

            var initialOwners = new List<Owner>()
            {
                new Owner()
                {
                    Name = "Tó",
                    EmailContact = "to@gmail.com",
                    PhoneContact = "938525156"
                },
                new Owner()
                {
                    Name = "Guilherme Frusto",
                    EmailContact = "frusto@gmail.com",
                    PhoneContact = "921453687"
                },
                new Owner()
                {
                    Name = "Albino Pessegada",
                    EmailContact = "bino@gmail.com",
                    PhoneContact = "938525156"
                }
            };

            var initialAdverts = new List<Advert>() {
                new Advert()
                {
                    //AdvertId = 1,
                    Name = "Casa de Vila Nova",
                    Address = "Rua da Escola, Nº2A",
                    Price = "400€",
                    Type = EAdvertType.Rent,
                    MeetingTime = DateTime.Now,
                    IncludesBills = false,
                    OwnerId = 1,
                    Score = 100
                },
                new Advert()
                {
                    //AdvertId = 2,
                    Name = "Casa de Wroclaw",
                    Address = "Jableczka 17, Wroclaw",
                    Price = "200€",
                    Type = EAdvertType.Rent,
                    MeetingTime = DateTime.Now,
                    IncludesBills = true,
                    OwnerId = 2,
                    Score = 80
                },

                new Advert()
                {
                    //AdvertId = 3,
                    Name = "Casa do Porto",
                    Address = "Rua da Lapa",
                    Price = "330€",
                    Type = EAdvertType.Rent,
                    MeetingTime = DateTime.Now,
                    IncludesBills = true,
                    OwnerId = 1,
                    Score = 75
                }
            };

            await _ownerRepo.AddRangeAsync(initialOwners);
            await _ownerRepo.SaveChangesAsync();

            await _advertRepo.AddRangeAsync(initialAdverts);
            await _advertRepo.SaveChangesAsync();
        }
        catch (Exception x)
        {
            return false;
        }
        return true;
    }
}