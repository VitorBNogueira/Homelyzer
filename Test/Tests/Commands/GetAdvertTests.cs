using Application;
using Application.Commands.Adverts;
using Application.Contracts;
using Application.Contracts.Responses;
using Application.DTOs.Advert;
using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Test.Fixtures;
using Xunit;

namespace Test.Tests.Commands;

public class GetAdvertTests : AdvertFixture
{
    [Fact]
    public async void GetAdvertHandler_AdvertExists_Success()
    {
        // Arrange
        MockAdvert();

        var command = new GetAdvertCommand(1);

        var handler = new GetAdvertHandler(
            AdvertRepositoryFake,
            MapperFake);

        // Act
        var result = await handler.Handle(command, CltToken);

        // Assert
        result.Should().NotBeNull();
        result.Should().BeAssignableTo<ISuccess>();
        result.Should().BeOfType<AdvertResponse>();
        ((AdvertResponse)result).Advert.Should().NotBeNull();
    }

    [Fact]
    public async void GetAdvertHandler_AdvertRepoEmpty_Failure()
    {
        // Arrange
        var command = new GetAdvertCommand(1);

        var handler = new GetAdvertHandler(
            AdvertRepositoryFake,
            MapperFake);

        // Act
        var result = await handler.Handle(command, CltToken);

        // Assert
        result.Should().NotBeNull();
        result.Should().BeAssignableTo<IClientFailure>();
        result.Should().BeOfType<ClientErrorResponse>();
        ((IClientFailure)result).ErrorCode.Should().Be(ErrorResults.ResourceNotFound().ErrorCode);
        _context.Adverts.Should().HaveCount(0);
    }

    [Fact]
    public async void GetAdvertHandler_AdvertDoesntExist_Failure()
    {
        // Arrange
        MockAdvert();

        var command = new GetAdvertCommand(2);

        var handler = new GetAdvertHandler(
            AdvertRepositoryFake,
            MapperFake);

        // Act
        var result = await handler.Handle(command, CltToken);

        // Assert
        result.Should().NotBeNull();
        result.Should().BeAssignableTo<IClientFailure>();
        result.Should().BeOfType<ClientErrorResponse>();
        ((IClientFailure)result).ErrorCode.Should().Be(ErrorResults.ResourceNotFound().ErrorCode);
        _context.Adverts.Should().HaveCount(1);
    }
}
