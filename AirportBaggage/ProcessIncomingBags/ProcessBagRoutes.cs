using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AirportBaggage
{
    partial class ProcessIncomingBags
    {
        static List<Models.RoutedBag> ProcessBagRoutes(Models.BaggageData baggageSourceData)
        {
            List<Models.RoutedBag> results = new List<Models.RoutedBag>();

            foreach (var bag in baggageSourceData.IncomingBags)
            {
                var bagRoute = new Models.RoutedBag()
                {
                    BagNumber = bag.BagNumber,
                    RoutePoints = new List<string>(),
                    TotalTravelTime = 0
                };

                var currFlight = bag.FlightId;
                string departureGate = null;

                if(currFlight == TerminatedFlightId)
                {
                    departureGate = BaggageClaimGateId;
                }
                else
                {
                    departureGate = baggageSourceData.FlightDepartures.Single(x => x.Id == currFlight).Gate;
                }

                bagRoute.RoutePoints.Add(departureGate);

                var assignedRoute = GetBaggeRoutePoints(departureGate, bag.EntryPoint, baggageSourceData.ConveyorMovementDefinitions, maxSteps: baggageSourceData.ConveyorMovementDefinitions.Count);

                foreach(var direction in assignedRoute)
                {
                    bagRoute.RoutePoints.Add(direction.Node1);
                    bagRoute.TotalTravelTime += direction.TravelTime;
                }

                bagRoute.RoutePoints.Reverse();

                results.Add(bagRoute);
            }

            return results;
        }
    }
}
