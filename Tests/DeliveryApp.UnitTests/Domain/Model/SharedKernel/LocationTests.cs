
using System;
using DeliveryApp.Core.Domain.Model.SharedKernel;
using FluentAssertions;
using Xunit;

namespace DeliveryApp.UnitTests.Domain.Model.SharedKernel;

public sealed class LocationShould
{
    
     [Fact]
    public void HasPrimitiveValidation()
    {
        Location sourceLocation = Location.Create(2,6).Value;
        Location targetLocation = Location.Create(4,9).Value;
        
        var expectedResult = sourceLocation.DistanceTo(targetLocation).Should().Be(7);
    }
}
