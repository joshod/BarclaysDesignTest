using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirportBaggage.Models
{
    public class IncomingBag
    {
        public string BagNumber { get; set; }
        public string EntryPoint { get; set; }
        public string FlightId { get; set; }
    }
}
