using Application;
using Application.Commands.Adverts;
using Application.Contracts;
using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Test.Fixtures;
using Xunit;

namespace Test.Tests.Commands;

public class ToggleAdvertTests : AdvertFixture
{
    [Fact]
    public async void DisableAdvert_AdvertIsEnabled_Success()
    {
        // Arrange
        MockAdvert();

        var command = new ToggleAdvertCommand(1, false);
        var handler = new ToggleAdvertHandler(AdvertRepositoryFake, Mapper);

        // Act
        var result = await handler.Handle(command, CltToken);

        // Assert
        result.Should().NotBeNull();
        result.Should().BeAssignableTo<ISuccess>();
        result.Should().BeOfType<Success>();
        _context.Adverts.Should().HaveCount(1);
        Assert.False(_context.Adverts.SingleOrDefault(x => x.AdvertId == 1).IsActive);
    }

    [Fact]
    public async void EnableAdvert_AdvertIsDisabled_Success()
    {
        // Arrange
        MockAdvert(isActive: false);

        var command = new ToggleAdvertCommand(1, true);
        var handler = new ToggleAdvertHandler(AdvertRepositoryFake, Mapper);

        // Act
        var result = await handler.Handle(command, CltToken);

        // Assert
        result.Should().NotBeNull();
        result.Should().BeAssignableTo<ISuccess>();
        result.Should().BeOfType<Success>();
        _context.Adverts.Should().HaveCount(1);
        Assert.True(_context.Adverts.SingleOrDefault(x => x.AdvertId == 1).IsActive);
    }

    [Theory]
    [InlineData(true)]
    [InlineData(false)]
    public async void ToggleAdvert_AdvertIsInSameState_Failure(bool active)
    {
        // Arrange
        MockAdvert(isActive: active);

        var command = new ToggleAdvertCommand(1, active);
        var handler = new ToggleAdvertHandler(AdvertRepositoryFake, Mapper);

        // Act
        var result = await handler.Handle(command, CltToken);

        // Assert
        result.Should().NotBeNull();
        result.Should().BeAssignableTo<IClientFailure>();
        _context.Adverts.Should().HaveCount(1);
        ((IClientFailure)result).ErrorCode.Should().Be(ErrorResults.NothingChanged().ErrorCode);
    }

    [Theory]
    [InlineData(true)]
    [InlineData(false)]
    public async void ToggleAdvert_AdvertDoesntExist_Failure(bool active)
    {
        // Arrange
        var command = new ToggleAdvertCommand(1, active);
        var handler = new ToggleAdvertHandler(AdvertRepositoryFake, Mapper);

        // Act
        var result = await handler.Handle(command, CltToken);

        // Assert
        result.Should().NotBeNull();
        result.Should().BeAssignableTo<IClientFailure>();
        _context.Adverts.Should().HaveCount(0);
        ((IClientFailure)result).ErrorCode.Should().Be(ErrorResults.ResourceNotFound().ErrorCode);
    }
}
