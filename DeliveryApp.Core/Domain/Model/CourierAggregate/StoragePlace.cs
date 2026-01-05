using System.Diagnostics.CodeAnalysis;
using CSharpFunctionalExtensions;
using Microsoft.IdentityModel.Tokens;
using Primitives;

namespace DeliveryApp.Core.Domain.Model.CourierAggregate;

public sealed class StoragePlace : Entity<Guid>
{
    [ExcludeFromCodeCoverage]
    private StoragePlace()
    {

    }

    private StoragePlace(string name, int totalVolume)
    {
        Id              = Guid.NewGuid();
        Name            = name;
        TotalVolume     = totalVolume;
        RemainingVolume = totalVolume;
    }

    public string Name { get; }
    public int TotalVolume { get; }
    public int RemainingVolume { get; private set; } // for future use
    public Guid? OrderId { get; private set; }

    public static Result<StoragePlace, Error> Create(string name, int totalVolume)
    {
        if (string.IsNullOrEmpty(name)) return GeneralErrors.ValueIsRequired(nameof(name)); 
        if (totalVolume <= 0) return GeneralErrors.ValueIsInvalid(nameof(totalVolume));

        return new StoragePlace(name, totalVolume);
    }
    public bool Validate(Guid orderId, int orderVolume)
    {
        ArgumentOutOfRangeException.ThrowIfNegativeOrZero(orderVolume);

        return (OrderId is null || (OrderId is not null && OrderId == orderId)) && (orderVolume <= RemainingVolume);
    }

    public Result<int, Error> Load(Guid orderId, int orderVolume)
    {
        if (!Validate(orderId, orderVolume)) return Errors.OrderCannotBeStored();
        if (orderVolume > RemainingVolume)   return GeneralErrors.ValueIsInvalid(nameof(orderVolume));
        
        OrderId          = orderId;
        RemainingVolume -= orderVolume;
        
        return RemainingVolume;
    }

    public Result<bool, Error> Clear(Guid orderId)
    {
        if (OrderId is null || OrderId != orderId) return Errors.OrderCannotBeCleared();

        OrderId = null;

        return true;
    }

    public static class Errors
    {
        public static Error OrderCannotBeStored()
            => new Error("order.cannot.be.stored", "Order cannot be stored");

        public static Error OrderCannotBeCleared()
            => new Error("order.cannot.be.cleared", "Order cannot be cleared");
    }
}