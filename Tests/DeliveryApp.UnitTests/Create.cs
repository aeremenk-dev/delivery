using DeliveryApp.Core.Domain.Model.CourierAggregate;
using DeliveryApp.Core.Domain.Model.SharedKernel;

namespace DeliveryApp.UnitTests;

public static class Create
{
 
    public static StoragePlace StoragePlace(string name = "use case", int totalVolume = 10)
    {
        return Core.Domain.Model.CourierAggregate.StoragePlace.Create(name, totalVolume).Value;
    }
}