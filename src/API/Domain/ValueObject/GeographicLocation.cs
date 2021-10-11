using Commentor.GivEtPraj.Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Commentor.GivEtPraj.Domain.ValueObject
{
    public class GeographicLocation : ValueObject
    {
        public double Latitude { get; private set; }
        public double Longitude {  get; private set; }

        private GeographicLocation() { }

        private GeographicLocation(double latitude, double longitude)
        {
            Latitude = latitude;
            Longitude = longitude;
        }

        public static GeographicLocation From(double latitude, double longitude)
        {
            GeographicLocation coords = new GeographicLocation(latitude, longitude);

            if(latitude is < -90 or > 90)
            {
                throw new InvalidLatitudeException($"{nameof(latitude)} is out of range. Must be between -90 and 90.");
            }

            if (longitude is < -180 or > 180)
            {
                throw new InvalidLongitudeException($"{nameof(longitude)} is out of range. Must be between -180 and 180.");
            }

            return coords;
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            // Using a yield return statement to return each element one at a time
            yield return Latitude;
            yield return Longitude;
        }
    }
}
