using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirportBaggage.Models
{
    public class RoutedBag
    {
        public string BagNumber { get; set; }
        public List<string> RoutePoints { get; set; }
        public int TotalTravelTime { get; set; }
    }
}
