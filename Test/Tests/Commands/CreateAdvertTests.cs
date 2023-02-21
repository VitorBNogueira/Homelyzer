using Application;
using Application.Commands.Adverts;
using Application.Contracts;
using Application.Contracts.Responses;
using Application.DTOs.Advert;
using Domain.Entities;
using FluentAssertions;
using Test.Fixtures;
using Xunit;

namespace Test.Tests.Commands;

public class CreateAdvertTests : AdvertFixture
{
    [Fact]
    // xUnit tests can be async VOID, unlike with other testing frameworks
    public async void CreateAdvertHandler_FullAd_OwnerExists_Success()
    {
        // Arrange
        MockOwner();

        var command = new CreateAdvertCommand(new AdvertDTO
        {
            Name = "test",
            Address = "test",
            Area = "test",
            IsActive = true,
            Url = "test",
            Description = "test",
            EmailContact = "test",
            IncludesBills = true,
            MeetingTime = DateTime.Now,
            OwnerName = "Test owner",
            PersonalNotes = "test",
            PhoneContact = "test",
            Pictures = new List<string> { "test", "test2" },
            Price = "test",
            Score = 100,
            Type = 1
        });

        var handler = new CreateAdvertHandler(
            AdvertRepositoryFake, 
            OwnerRepositoryFake, 
            PictureRepositoryFake,
            Mapper);

        // Act
        var result = await handler.Handle(command, CltToken);

        // Assert
        result.Should().NotBeNull();
        result.Should().BeOfType<Success>();
        _context.Owners.Should().HaveCount(1);
        _context.Adverts.Should().HaveCount(1);
    }

    [Fact]
    public async void CreateAdvertHandler_FullAd_OwnerDoesntExist_Success()
    {
        // Arrange
        MockOwner();

        var command = new CreateAdvertCommand(new AdvertDTO
        {
            Name = "test",
            Address = "test",
            Area = "test",
            IsActive = true,
            Url = "test",
            Description = "test",
            EmailContact = "test",
            IncludesBills = true,
            MeetingTime = DateTime.Now,
            OwnerName = "Test owner 2",
            PersonalNotes = "test",
            PhoneContact = "test",
            Pictures = new List<string> { "test", "test2" },
            Price = "test",
            Score = 100,
            Type = 1
        });

        var handler = new CreateAdvertHandler(
            AdvertRepositoryFake,
            OwnerRepositoryFake,
            PictureRepositoryFake,
            Mapper);

        // Act
        var result = await handler.Handle(command, CltToken);

        // Assert
        result.Should().NotBeNull();
        result.Should().BeOfType<Success>();
        _context.Adverts.Should().HaveCount(1);
        _context.Owners.Should().HaveCount(2);
    }

    [Fact]
    public async void CreateAdvertHandler_FullAd_RepeatingOwner_Failure()
    {
        // Arrange
        MockOwner();

        _context.Owners.Add(new Owner
        {
            OwnerId = 2,
            Name = "Test owner",
            EmailContact = "Test Email",
            PhoneContact = "932568742",
            IsActive = true,
        });

        _context.SaveChanges();

        var command = new CreateAdvertCommand(new AdvertDTO
        {
            Name = "test",
            Address = "test",
            Area = "test",
            IsActive = true,
            Url = "test",
            Description = "test",
            EmailContact = "test",
            IncludesBills = true,
            MeetingTime = DateTime.Now,
            OwnerName = "Test owner",
            PersonalNotes = "test",
            PhoneContact = "test",
            Pictures = new List<string> { "test", "test2" },
            Price = "test",
            Score = 100,
            Type = 1
        });

        var handler = new CreateAdvertHandler(
            AdvertRepositoryFake,
            OwnerRepositoryFake,
            PictureRepositoryFake,
            Mapper);

        // Act
        var result = await handler.Handle(command, CltToken);

        // Assert
        result.Should().NotBeNull();
        result.Should().BeOfType<ServerErrorResponse>();
        ((IServerFailure)result).ErrorCode.Should().Be(ErrorResults.DataConsistencyError("There is more than one Owner with the same name / email / number.").ErrorCode);
        _context.Adverts.Should().HaveCount(0);
        _context.Owners.Should().HaveCount(2);
    }
}