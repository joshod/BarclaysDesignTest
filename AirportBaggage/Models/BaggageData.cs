using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirportBaggage.Models
{
    public class BaggageData
    {
        public List<Models.ConveyorMovement> ConveyorMovementDefinitions { get; set; }
        public List<Models.FlightDeparture> FlightDepartures { get; set; }
        public List<Models.IncomingBag> IncomingBags { get; set; }
    }
}
