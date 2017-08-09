using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirportBaggage.Models
{
    public class FlightDeparture
    {
        public string Id { get; set; }
        public string Gate { get; set; }
        public string Destination { get; set; }
        public DateTime DepartureTime { get; set; }
    }
}
