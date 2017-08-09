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
        static List<Models.ConveyorMovement> GetBaggeRoutePoints(string destinationPoint, string entryPoint, List<Models.ConveyorMovement> conveyorDefinitions, int currStep = 0, int? maxSteps = null)
        {
            var result = new List<Models.ConveyorMovement>();

            if(maxSteps.HasValue && currStep >= maxSteps)
            {
                return null;
            }

            var directRoute = conveyorDefinitions.FirstOrDefault(x => x.Node2 == destinationPoint && x.Node1 == entryPoint);

            if (directRoute != null)
            {
                result.Add(directRoute);
            }
            else
            {
                var potentialStartingPoints = conveyorDefinitions.Where(x => x.Node2 == destinationPoint);

                var potentialRoutes = new List<List<Models.ConveyorMovement>>();

                foreach (var route in potentialStartingPoints)
                {
                    if (potentialRoutes != null && potentialRoutes.Any())
                    {
                        maxSteps = potentialRoutes.OrderBy(x => x.Count).First().Count < maxSteps ? potentialRoutes.OrderBy(x => x.Count).First().Count : maxSteps;
                    }

                    var currentRoute = GetBaggeRoutePoints(route.Node1, entryPoint, conveyorDefinitions, currStep + 1, maxSteps);

                    if (currentRoute != null && currentRoute.Any())
                    {
                        var hasDups = currentRoute.GroupBy(x => new { x.Node1, x.Node2 }).Any(x => x.Count() > 1);

                        if (!hasDups)
                        {
                            var tempRoute = new List<Models.ConveyorMovement>();

                            tempRoute.Add(route);
                            tempRoute.AddRange(currentRoute);

                            potentialRoutes.Add(tempRoute);
                        }
                    }
                }

                result = potentialRoutes.OrderBy(x => x.Count).FirstOrDefault();
            }

            return result;
        }
    }
}
