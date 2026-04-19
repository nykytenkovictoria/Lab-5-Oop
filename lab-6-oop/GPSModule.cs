using System;
using System.Collections.Generic;
using System.Text;

namespace lab_6_oop
{
    public class GPSModule
    {
        private double latitude;
        private double longitude;

        public (double Lat, double Lon) Location
        {
            get => (latitude, longitude);
            private set
            {
                latitude = value.Lat;
                longitude = value.Lon;
            }
        }

        public GPSModule()
        {
            latitude = 50.4501;
            longitude = 30.5234;
        }

        public void UpdateLocation(double lat, double lon)
        {
            if (lat < -90 || lat > 90 || lon < -180 || lon > 180)
                throw new InvalidInputException("Invalid GPS coordinates");
            Location = (lat, lon);
        }

        public double DistanceTo(double lat, double lon)
        {
            //approximation 
            double latDiff = Math.Abs(lat - latitude);
            double lonDiff = Math.Abs(lon - longitude);

            // Each degree is approximately 111 km
            double distance = (latDiff + lonDiff) * 111 / 2;

            return Math.Round(distance, 2);
        }

        public string FindPerson(Person person)
        {
            double dist = DistanceTo(person.LastKnownLatitude, person.LastKnownLongitude);
            return $"{person.Name} is {dist:F2} km away from you.";
        }

        public string GetCurrentLocation()
        {
            return $"{latitude:F2}°, {longitude:F2}°";
        }
    }
}
