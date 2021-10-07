using Commentor.GivEtPraj.Domain.Exceptions;

namespace Commentor.GivEtPraj.Domain.ValueObjects;

public class Coords : ValueObject
{
    public double Latitude { get; private set; }
    public double Longitude { get; private set; }

    private Coords()
    {
    }

    private Coords(double latitude, double longitude)
    {
        Latitude = latitude;
        Longitude = longitude;
    }

    public static Coords From(double latitude, double longitude)
    {
        var coords = new Coords(latitude, longitude);

        if (latitude is < -90 or > 90)
        {
            throw new InvalidLatitudeException(
                $"{nameof(latitude)} is out of range. Must be between -90 and 90.");
        }

        if (longitude is < -180 or > 180)
        {
            throw new InvalidLongitudeException(
                $"{nameof(longitude)} is out of range. Must be between -180 and 180.");
        }

        return coords;
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Latitude;
        yield return Longitude;
    }
}