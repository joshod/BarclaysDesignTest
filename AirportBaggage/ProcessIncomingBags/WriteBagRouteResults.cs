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
        static void WriteBagRouteResults(List<Models.RoutedBag> bagRoutes, string resultsOutputFileName)
        {
            using (System.IO.StreamWriter sw = System.IO.File.AppendText(Path.Combine(WorkingDirectoryPath, OutputDirectoryName, resultsOutputFileName)))
            {
                foreach (var bagRoute in bagRoutes)
                {
                    sw.WriteLine($"{bagRoute.BagNumber} {String.Join(" ", bagRoute.RoutePoints)} : {bagRoute.TotalTravelTime}");
                }
            }
        }
    }
}
