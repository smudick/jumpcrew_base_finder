using System;
using System.Collections.Generic;
using System.Text;

namespace Pearle_Vision_Base_Finder
{
    public class Pearle
    {
        public string NewStoreNumber { get; set; }
        public string SiteNumber { get; set; }
        public string OldStoreNumber { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Zip { get; set; }
        public string Latitude { get; set; }
        public string Longitude { get; set; }
        public double latdouble { get; set; }
        public double longdouble { get; set; }
        public string County { get; set; }
        public string Country { get; set; }
        public Dictionary<Bases, double> CloseBases { get; set; } = new Dictionary<Bases, double>();
    }
}
