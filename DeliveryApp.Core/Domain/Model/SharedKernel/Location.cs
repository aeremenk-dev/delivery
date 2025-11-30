//using Microsoft.IdentityModel.Tokens;
//using System.Security.Cryptography.X509Certificates;
using CSharpFunctionalExtensions;
using Primitives;
//using Npgsql.Replication;
//using Microsoft.EntityFrameworkCore.Metadata.Conventions;

namespace DeliveryApp.Core.Domain.Model.SharedKernel;

public sealed class Location : ValueObject
{
    public int X { get; }
    public int Y { get; }
    public static Location minPoint => new(1,1);
    public static Location maxPoint => new(10,10);
     
    private Location()
    {
    }

    private Location(int x, int y) : this()
    {
        X = x;
        Y = y;
    }

    public static Result<Location, Error> Create(int x, int y)
    {
         if (x < minPoint.X || x > maxPoint.X) return GeneralErrors.ValueIsInvalid(nameof(x));
         if (y < minPoint.Y || y > maxPoint.Y) return GeneralErrors.ValueIsInvalid(nameof(y));
         
         return new Location(x, y);
    }

    public static Result<Location, Error> CreateRandom()
    {
        Random random = new Random();

        int x = random.Next(minPoint.X, maxPoint.X);
        int y = random.Next(minPoint.Y, maxPoint.Y);

        var location = new Location(x, y);

        return location;
    }

    public Result<int, Error> DistanceTo(Location target)
    {        
        if (target == null) return GeneralErrors.ValueIsRequired(nameof(target));

        var deltaX = Math.Abs(this.X - target.X);
        var deltaY = Math.Abs(this.Y - target.Y);

        if (deltaX == 0 && deltaY == 0) return GeneralErrors.InvalidLength(nameof(target));

        var distance = deltaX + deltaY;

        return distance;
    }

    protected override IEnumerable<IComparable>GetEqualityComponents()
    {
        yield return X;
        yield return Y;
    }
}


