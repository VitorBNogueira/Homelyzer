using Application.Interfaces;
using Domain.Entities;
using Domain.Enums;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Commands.Dev.ResetAdverts;

public sealed class ResetAdvertsHandler : IRequestHandler<ResetAdvertsCommand, bool>
{
    private readonly IAdvertRepository _advertRepo;
    private readonly IOwnerRepository _ownerRepo;
    private readonly IPictureRepository _pictureRepo;

    public ResetAdvertsHandler(IAdvertRepository adRepo, IOwnerRepository ownerRepo, IPictureRepository PictureRepo)
    {
        _advertRepo = adRepo;
        _ownerRepo = ownerRepo;
        _pictureRepo = PictureRepo;
    }
    public async Task<bool> Handle(ResetAdvertsCommand request, CancellationToken cancellationToken)
    {
        try
        {
            await _advertRepo.ClearAllAdverts();

            var placeholderPicture = new List<Picture>()
            {
                new Picture()
                {
                    Url ="https://vestnorden.com/wp-content/uploads/2018/03/house-placeholder.png",
                    AdvertId = 1
                },
                new Picture()
                {
                    Url ="https://vestnorden.com/wp-content/uploads/2018/03/house-placeholder.png",
                    AdvertId = 2
                },
                new Picture()
                {
                    Url ="https://vestnorden.com/wp-content/uploads/2018/03/house-placeholder.png",
                    AdvertId = 3
                }
            };

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
                    Score = 100,
                    Description = "Casa da aldeia, muito espaço, capaz de inundar às vezes, fria no Inverno, mas fresca no Verão. Sítio calmo, tirando o cão do vizinho às vezes à noite fdp"
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
                    Score = 80,
                    Description = "Cheap house in a nice area with lots of supermarkets around and even a shopping mall. Nice calm area, far from the city center, but Ubers are cheap 🤷‍♂️"
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
                    Score = 75,
                    Description = "Quase casa, mas tá perto da cidade e o quarto é espaçoso. Não tem persianas, paciência. Os donos são uns fdps, principalmente a velha fds pqp chata. Ao menos este tem janelas nos quartos."
                }
            };

            await _ownerRepo.AddRangeAsync(initialOwners);
            await _ownerRepo.SaveChangesAsync();

            await _advertRepo.AddRangeAsync(initialAdverts);
            await _advertRepo.SaveChangesAsync();

            await _pictureRepo.AddRangeAsync(placeholderPicture);
            await _pictureRepo.SaveChangesAsync();
        }
        catch (Exception x)
        {
            return false;
        }
        return true;
    }
}