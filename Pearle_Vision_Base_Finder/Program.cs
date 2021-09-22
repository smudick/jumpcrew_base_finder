using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using LumenWorks.Framework.IO.Csv;
//csvhelper
namespace Pearle_Vision_Base_Finder
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine(string.Join(",", args));

            using (StreamWriter writer = new StreamWriter("C://Users/smudi/big5.txt"))
            {
                Console.SetOut(writer);
                Result();
            }
        }
        static void Result()
        {
            //C:\Users\smudi\Downloads\Pearle.csv
            var csvTableBases = new DataTable();
            using (var csvReader = new CsvReader(new StreamReader(System.IO.File.OpenRead(@"C:\Users\smudi\Downloads\Bases.csv")), true))
            {
                csvTableBases.Load(csvReader);
            }
            //var csvTablePearle = new DataTable();

            //using (var csvReader = new CsvReader(new StreamReader(System.IO.File.OpenRead(file2)), true))
            //{
            //    csvTablePearle.Load(csvReader);
            //}
            //var csvTablePep = new DataTable();
            //using (var csvReader = new CsvReader(new StreamReader(System.IO.File.OpenRead(@"C:\Users\smudi\Downloads\PepBoys.csv")), true))
            //{
            //    csvTablePep.Load(csvReader);
            //}
            //var csvTableDW = new DataTable();
            //using (var csvReader = new CsvReader(new StreamReader(System.IO.File.OpenRead(@"C:\Users\smudi\Downloads\DW_NC.csv")), true))
            //{
            //    csvTableDW.Load(csvReader);
            //}
            var csvTableBig5 = new DataTable();
            using (var csvReader = new CsvReader(new StreamReader(System.IO.File.OpenRead(@"C:\Users\smudi\Downloads\big5.csv")), true))
            {
                csvTableBig5.Load(csvReader);
            }

            List<Bases> bases = new List<Bases>();
            //List<Pearle> pearleLocations = new List<Pearle>();
            //List<DW> DWLocations = new List<DW>();
            List<Big5> Big5Locations = new List<Big5>();

            for (int i = 0; i < csvTableBases.Rows.Count; i++)
            {
                bases.Add(new Bases
                {
                    siteid = csvTableBases.Rows[i][0].ToString(),
                    basename = csvTableBases.Rows[i][1].ToString(),
                    address1 = csvTableBases.Rows[i][2].ToString(),
                    address2 = csvTableBases.Rows[i][3].ToString(),
                    city = csvTableBases.Rows[i][4].ToString(),
                    state = csvTableBases.Rows[i][5].ToString(),
                    zip = csvTableBases.Rows[i][6].ToString(),
                    country_code = csvTableBases.Rows[i][7].ToString(),
                    latitude = csvTableBases.Rows[i][8].ToString(),
                    longitude = csvTableBases.Rows[i][9].ToString(),
                });
            }
            for (int i = 0; i < csvTableBig5.Rows.Count; i++)
            {
                Big5Locations.Add(new Big5
                {
                    Name = csvTableBig5.Rows[i][0].ToString(),
                    Latitude = csvTableBig5.Rows[i][1].ToString(),
                    Longitude = csvTableBig5.Rows[i][2].ToString(),
                });
            }

            foreach (var ahrnbase in bases)
            {
                if (ahrnbase.latitude != "NULL")
                {
                    ahrnbase.latdouble = Convert.ToDouble(ahrnbase.latitude);
                }
                if (ahrnbase.longitude != "NULL")
                {
                    ahrnbase.longdouble = Convert.ToDouble(ahrnbase.longitude);
                }
            }

            foreach (var location in Big5Locations)
            {
                location.latdouble = Convert.ToDouble(location.Latitude);
                location.longdouble = Convert.ToDouble(location.Longitude);
            }
            var basesChecked = 0;
            //var pearleLocationsChecked = 0;
            var Big5LocationsChecked = 0;
            var storesWithin15Miles = 0;
            foreach (var location in Big5Locations)
            {
                Big5LocationsChecked++;
                foreach (var ahrnbase in bases)
                {
                    if (ahrnbase.latitude != "NULL")
                    {
                        basesChecked++;
                        var dist = GetDistance(location.longdouble, location.latdouble, ahrnbase.longdouble, ahrnbase.latdouble);
                        var distInMiles = Math.Round(dist * .000621371192, 2);
                        if (distInMiles <= 15)
                        {
                            storesWithin15Miles++;
                            location.CloseBases.Add(ahrnbase, distInMiles);
                            //Console.WriteLine($"store # {location.NewStoreNumber} is {distInMiles} miles away from {ahrnbase.basename}");
                        }
                    }

                }
            }
            int totalBases = 0;
            foreach (var location in Big5Locations)
            {
                Console.WriteLine($"{location.Name}:");
                foreach (var (ahrnbase, distance) in location.CloseBases)
                {
                    Console.WriteLine($":{ahrnbase.basename} - {distance} miles away.");
                    totalBases++;
                }
            }
            //Console.WriteLine($"Total bases within 10 miles of a Pep Boys Location: {totalBases}");
            double GetDistance(double longitude, double latitude, double otherLongitude, double otherLatitude)
            {
                var d1 = latitude * (Math.PI / 180.0);
                var num1 = longitude * (Math.PI / 180.0);
                var d2 = otherLatitude * (Math.PI / 180.0);
                var num2 = otherLongitude * (Math.PI / 180.0) - num1;
                var d3 = Math.Pow(Math.Sin((d2 - d1) / 2.0), 2.0) + Math.Cos(d1) * Math.Cos(d2) * Math.Pow(Math.Sin(num2 / 2.0), 2.0);
                return 6376500.0 * (2.0 * Math.Atan2(Math.Sqrt(d3), Math.Sqrt(1.0 - d3)));
            }


        }
    }

}
