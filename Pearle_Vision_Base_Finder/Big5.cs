using System;
using System.Collections.Generic;
using System.Text;

namespace Pearle_Vision_Base_Finder
{
    class Big5
    {
        public string Name { get; set; }
        public string Latitude { get; set; }
        public string Longitude { get; set; }
        public double latdouble { get; set; }
        public double longdouble { get; set; }
        public Dictionary<Bases, double> CloseBases { get; set; } = new Dictionary<Bases, double>();

    }
}
