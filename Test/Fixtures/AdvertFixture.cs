using Domain.Entities;
using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test.Fixtures;

public class AdvertFixture : InMemoryDBConfig
{
    protected void MockAdvert()
    {
        _context.Adverts.Add(new Advert
        {
            AdvertId = 1,
            Name = "TestAd",
            Address = "TestAddress",
            Area = "20m2",
            Price = "800€",
            Type = EAdvertType.Rent,
            IncludesBills = true,
            Description = "Test Desc",
            PersonalNotes = "Test Notes",
            MeetingTime = new DateTime(2021, 05, 15, 15, 30, 00),
            Score = 100,
            Url = "www.test.com",
            OwnerId = 1,
            IsActive = true
        });

        _context.SaveChanges();
    }
    protected void MockOwner()
    {
        _context.Owners.Add(new Owner
        {
            OwnerId = 1,
            Name = "Test owner",
            EmailContact = "Test Email",
            PhoneContact = "932568742",
            IsActive = true,
        });

        _context.SaveChanges();
    }

    protected void MockPicture()
    {
        _context.Pictures.Add(new Picture
        {
            PictureId = 1,
            Url = "www.mockPics.com/mockPic.jpeg",
            IsActive = true,
        });

        _context.SaveChanges();
    }
}
