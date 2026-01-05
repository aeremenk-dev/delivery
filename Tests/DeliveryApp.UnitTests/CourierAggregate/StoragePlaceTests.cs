using System;
using DeliveryApp.Core.Domain.Model.CourierAggregate;
using FluentAssertions;
using Xunit;

namespace DeliveryApp.UnitTests.Domain.Model.CourierAggregate;

public class StoragePlaceTests
{
    [Theory]
    [InlineData(null, 100)]
    [InlineData("", 100)]
    public void ValidateNames(string name, int totalVolume)
    {
        // Arrange and Act
        var creatingResult = StoragePlace.Create(name, totalVolume);

        // Assert
        creatingResult.Error.Code.Should().Be(ErrorCodes.ValueIsRequired);
    }

  
    [Fact]
    public void ValidateClearing()
    {
        // Arrange
        var orderId      = Guid.NewGuid();
        var storagePlace = Create.StoragePlace();
        storagePlace.Load(orderId, 1);

        // Act
        var clearingResult = storagePlace.Clear(orderId);

        // Assert.
        storagePlace.OrderId.Should().BeNull();
        clearingResult.IsSuccess.Should().BeTrue();
    }
}