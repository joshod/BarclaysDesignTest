using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirportBaggage.Models
{
    public class ConveyorMovement
    {
        public string Node1 { get; set; }
        public string Node2 { get; set; }
        public int TravelTime { get; set; }
    }
}
