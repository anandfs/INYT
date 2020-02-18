using INYTWebsite.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Xml;

namespace INYTWebsite.Code
{
    public static class Distance
    {
        // Handy structure for Long/Lat information
        public struct Coords
        {
            public double Longitude;
            public double Latitude;
        }

        // Unit calculations
        public enum Units
        {
            Miles,
            Kilometres
        }

        // Will return a null if the Google API is unable to find either post code, or the country constraint fails
        public static string BetweenTwoPostCodes(string postcodeA, string postcodeB, string countryCodeWithin, Units units)
        {
            //var ll1 = PostCodeToLongLat(postcodeA, countryCodeWithin);
            //if (!ll1.HasValue) return null;
            //var ll2 = PostCodeToLongLat(postcodeB, countryCodeWithin);
            //if (!ll2.HasValue) return null;
            //return ll1.Value.DistanceTo(ll2.Value, units);

            var apikey = "AIzaSyDf4qQJbhaC_2NwNzp1xqKVLhbSo1gup_8";

            DistanceModel distanceBetweenPostcodes = new DistanceModel();
            distanceBetweenPostcodes = GetDistanceBetweenTwoPostcodes(postcodeA, postcodeB, units, apikey);
            if (distanceBetweenPostcodes != null)
                return distanceBetweenPostcodes.rows[0].elements[0].distance.text;

            return string.Empty;
        }

        private static DistanceModel GetDistanceBetweenTwoPostcodes(string postcodeA, string postcodeB, Units units, string apikey)
        {
            // Download the XML response from Google
            var client = new WebClient();
            var encodedPostCodeA = HttpUtility.UrlEncode(postcodeA);
            var encodedPostCodeB = HttpUtility.UrlEncode(postcodeB);
            var url = string.Format("https://maps.googleapis.com/maps/api/distancematrix/json?units=imperial&origins={0}&destinations={1}&key={2}", postcodeA, postcodeB, apikey);
            RequestMethod requestMethod = new RequestMethod();
            HttpResponseMessage responseContent = requestMethod.GetContent(url);

            if (responseContent.StatusCode == HttpStatusCode.OK)
            {
                var distanceModel = JsonConvert.DeserializeObject<DistanceModel>(responseContent.Content.ReadAsStringAsync().Result);
                return distanceModel;
            }
            else if (responseContent.StatusCode == HttpStatusCode.BadRequest)
            {
                return null;
            }
            else if (responseContent.StatusCode == HttpStatusCode.InternalServerError)
            {
                return null;
            }

            return null;
        }

        // Overload for UK post codes
        public static string BetweenTwoUKPostCodes(string postcodeA, string postcodeB)
        {
            return BetweenTwoPostCodes(postcodeA, postcodeB, "GB", Units.Miles);
        }

        // Uses the Google API to resolve a post code (within the specified country)
        public static Coords? PostCodeToLongLat(string postcode, string countryCodeWithin)
        {
            // Download the XML response from Google
            var client = new WebClient();
            var encodedPostCode = HttpUtility.UrlEncode(postcode);
            var url = string.Format("http://maps.google.com/maps/geo?q={0}&output=xml", encodedPostCode);
            var xml = client.DownloadString(url);
            var doc = new XmlDocument();
            doc.LoadXml(xml);

            // Create a custom namespace manager
            var nsmgr = new XmlNamespaceManager(doc.NameTable);
            nsmgr.AddNamespace("ge", "http://earth.google.com/kml/2.0");
            nsmgr.AddNamespace("oa", "urn:oasis:names:tc:ciq:xsdschema:xAL:2.0");

            // Any results?
            var nodelist = doc.SelectNodes("//ge:kml/ge:Response/ge:Placemark", nsmgr);
            if (nodelist == null || nodelist.Count == 0) return null;

            // Results are already ordered by accuracy, so take the first one
            var node = nodelist[0];

            // Check the Country constraint
            var countryname = node.SelectSingleNode("oa:AddressDetails/oa:Country/oa:CountryNameCode", nsmgr);
            if (countryname.FirstChild.Value != countryCodeWithin)
                return null;

            // Get the raw Long/Lat coordinates (I wish there was a nicer way..
            // perhaps averaging the LongLat enclosing box?)
            var coords = node.SelectSingleNode("ge:Point/ge:coordinates", nsmgr).FirstChild.Value.Split(',');
            double longitude;
            double lattitude;
            if (!Double.TryParse(coords[0], out longitude)) return null;
            if (!Double.TryParse(coords[1], out lattitude)) return null;

            return new Coords
            {
                Longitude = longitude,
                Latitude = lattitude
            };
        }

        public static double DistanceTo(this Coords from, Coords to, Units units)
        {
            // Haversine Formula...
            var dLat1InRad = from.Latitude * (Math.PI / 180.0);
            var dLong1InRad = from.Longitude * (Math.PI / 180.0);
            var dLat2InRad = to.Latitude * (Math.PI / 180.0);
            var dLong2InRad = to.Longitude * (Math.PI / 180.0);

            var dLongitude = dLong2InRad - dLong1InRad;
            var dLatitude = dLat2InRad - dLat1InRad;

            // Intermediate result a.
            var a = Math.Pow(Math.Sin(dLatitude / 2.0), 2.0) +
                    Math.Cos(dLat1InRad) * Math.Cos(dLat2InRad) *
                    Math.Pow(Math.Sin(dLongitude / 2.0), 2.0);

            // Intermediate result c (great circle distance in Radians).
            var c = 2.0 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1.0 - a));

            // Unit of measurement
            var radius = 6371;
            if (units == Units.Miles) radius = 3959;

            return radius * c;
        }
    }
}
