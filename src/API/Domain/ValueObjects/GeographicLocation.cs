using System.Diagnostics.CodeAnalysis;
using Commentor.GivEtPraj.Domain.Exceptions;

namespace Commentor.GivEtPraj.Domain.ValueObjects;

// Don't let ReSharper remove the private set because EF Core needs it
[SuppressMessage("ReSharper", "AutoPropertyCanBeMadeGetOnly.Local")]
public class GeographicLocation : ValueObject
{

    private GeographicLocation()
    {
    }

    private GeographicLocation(double latitude, double longitude)
    {
        Latitude = latitude;
        Longitude = longitude;
    }

    public double Latitude { get; private set; }
    public double Longitude { get; private set; }

    public static GeographicLocation From(double latitude, double longitude)
    {
        var geographicLocation = new GeographicLocation(latitude, longitude);

        if (latitude is < -90 or > 90)
            throw new InvalidLatitudeException(
                $"{nameof(latitude)} is out of range. Must be between -90 and 90.");

        if (longitude is < -180 or > 180)
            throw new InvalidLongitudeException(
                $"{nameof(longitude)} is out of range. Must be between -180 and 180.");

        return geographicLocation;
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Latitude;
        yield return Longitude;
    }
}