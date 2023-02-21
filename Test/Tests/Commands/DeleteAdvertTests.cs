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

public class DeleteAdvertTests : AdvertFixture
{
    [Fact]
    public async void DeleteAdvertHandler_AdvertExists_Success()
    {
        // Arrange
        MockAdvert();

        var command = new DeleteAdvertCommand(1);

        var handler = new DeleteAdvertHandler(AdvertRepositoryFake, Mapper);

        // Act
        var result = await handler.Handle(command, CltToken);

        // Assert
        result.Should().NotBeNull();
        result.Should().BeOfType<Success>();
        _context.Owners.Should().HaveCount(0);
        _context.Adverts.Should().HaveCount(0);
    }

    [Fact]
    public async void DeleteAdvertHandler_NoAdvertExists_Failure()
    {
        // Arrange
        var command = new DeleteAdvertCommand(1);

        var handler = new DeleteAdvertHandler(AdvertRepositoryFake, Mapper);

        // Act
        var result = await handler.Handle(command, CltToken);

        // Assert
        result.Should().NotBeNull();
        result.Should().BeAssignableTo<IClientFailure>();
        result.Should().BeOfType<ClientErrorResponse>();
        ((IClientFailure)result).ErrorCode.Should().Be(ErrorResults.ResourceNotFound().ErrorCode);
        _context.Owners.Should().HaveCount(0);
        _context.Adverts.Should().HaveCount(0);
    }

    [Fact]
    public async void DeleteAdvertHandler_AdvertNotFound_Failure()
    {
        // Arrange
        MockAdvert();

        var command = new DeleteAdvertCommand(2);

        var handler = new DeleteAdvertHandler(AdvertRepositoryFake, Mapper);

        // Act
        var result = await handler.Handle(command, CltToken);

        // Assert
        result.Should().NotBeNull();
        result.Should().BeAssignableTo<IClientFailure>();
        result.Should().BeOfType<ClientErrorResponse>();
        ((IClientFailure)result).ErrorCode.Should().Be(ErrorResults.ResourceNotFound().ErrorCode);
        _context.Owners.Should().HaveCount(0);
        _context.Adverts.Should().HaveCount(1);
    }
}