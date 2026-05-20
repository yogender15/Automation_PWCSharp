using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMRC.CTP.Specs.Scenarios.Request.Factory.Fakers
{
    using Bogus.DataSets;
    using System;

    public static class AddressGenerator
    {
        private static readonly Random rand = new();

        private static string RandomFrom(string[] array)
        {
            return array[rand.Next(array.Length)];
        }

        private static string RandomStreet()
        {
            string[] prefixes =
            [
            "Oak","Pine","Maple","Willow","Cedar","Ash","Birch","Elm",
            "Hazel","Sycamore","Chestnut","Holly","Beech","Rowan","Yew"
        ];

            string[] types =
            [
            "Road","Street","Avenue","Lane","Close","Drive","Way",
            "Crescent","Place","Court","Grove","Park","Terrace","Mews"
        ];

            return $"{RandomFrom(prefixes)} {RandomFrom(types)}";
        }

        private static string RandomTown()
        {
            string[] towns =
            [
            "Riverton","Brookfield","Ashford","Hillview","Westhaven",
            "Kingswood","Fairford","Stonebridge","Redfield","Northwick",
            "Clearwater","Greenford","Lakeside","Southport","Eastbourne"
        ];

            return RandomFrom(towns);
        }

        private static string RandomLocality()
        {
            string[] localities =
            [
            "Westshire","Northvale","Eastmoor","Southbridge","Highland",
            "Lowlands","Riverdale","Greenshire","Brookeshire","Oakshire",
            "Kingshire","Queensvale","Forestshire","Meadowbrook"
        ];

            return RandomFrom(localities);
        }

        private static string RandomPostcodeUK()
        {
            static char RandomLetter() => (char)('A' + rand.Next(26));

            return $"{RandomLetter()}{RandomLetter()}" +
                   $"{rand.Next(10, 100)} " +
                   $"{rand.Next(0, 10)}" +
                   $"{RandomLetter()}{RandomLetter()}";
        }

        public static Address GenerateAddress()
        {
            return new Address
            {
                Street = RandomStreet(),
                Town = RandomTown(),
                Locality = RandomLocality(),
                Postcode = RandomPostcodeUK()
            };
        }
    }

    public class Address
    {
        public string? Street { get; set; }
        public string? Town { get; set; }
        public string? Locality { get; set; }
        public string? Postcode { get; set; }
    }
}