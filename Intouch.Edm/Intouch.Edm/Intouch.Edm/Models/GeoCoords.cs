using System;
using System.Collections.Generic;
using System.Text;

namespace Intouch.Edm.Models
{
    public class GeoCoords
    {
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public bool IsGpsClose { get; set; }
    }
}
