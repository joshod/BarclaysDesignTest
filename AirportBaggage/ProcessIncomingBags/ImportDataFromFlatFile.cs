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
        static Models.BaggageData ImportDataFromFlatFile(string filePath)
        {
            var result = new Models.BaggageData()
            {
                ConveyorMovementDefinitions = new List<Models.ConveyorMovement>(),
                FlightDepartures = new List<Models.FlightDeparture>(),
                IncomingBags = new List<Models.IncomingBag>()
            };

            using (StreamReader sr = File.OpenText(filePath))
            {
                string currSection = null;

                string inputLine = String.Empty;
                while ((inputLine = sr.ReadLine()) != null)
                {
                    if (inputLine.StartsWith(SectionDeclaration))
                    {
                        currSection = inputLine.Remove(0, SectionDeclaration.Length + 1);
                    }
                    else
                    {
                        switch (currSection)
                        {
                            case ConveyorSectionName:
                                try
                                {
                                    var conveyerData = inputLine.Split(' ');

                                    result.ConveyorMovementDefinitions.Add(new Models.ConveyorMovement()
                                    {
                                        Node1 = conveyerData[0],
                                        Node2 = conveyerData[1],
                                        TravelTime = Convert.ToInt32(conveyerData[2])
                                    });

                                    result.ConveyorMovementDefinitions.Add(new Models.ConveyorMovement()
                                    {
                                        Node1 = conveyerData[1],
                                        Node2 = conveyerData[0],
                                        TravelTime = Convert.ToInt32(conveyerData[2])
                                    });
                                }
                                catch (Exception e)
                                {
                                    throw new Exception("Invalid data supplied. Could not parse conveyer movement declarations.", e);
                                }

                                break;
                            case DeparturesSectionName:
                                try
                                {
                                    var departureData = inputLine.Split(' ');

                                    var tempDeparture = new Models.FlightDeparture()
                                    {
                                        Id = departureData[0],
                                        Gate = departureData[1],
                                        Destination = departureData[2],
                                        DepartureTime = DateTime.ParseExact(departureData[3], "hh:mm", System.Globalization.CultureInfo.CurrentCulture)
                                    };

                                    result.FlightDepartures.Add(tempDeparture);
                                }
                                catch (Exception e)
                                {
                                    throw new Exception("Invalid data supplied. Could not parse departure flight declarations.", e);
                                }

                                break;
                            case BagsSectionName:
                                try
                                {
                                    var bagData = inputLine.Split(' ');

                                    var tempBag = new Models.IncomingBag()
                                    {
                                        BagNumber = bagData[0],
                                        EntryPoint = bagData[1],
                                        FlightId = bagData[2]
                                    };

                                    result.IncomingBags.Add(tempBag);
                                }
                                catch (Exception e)
                                {
                                    throw new Exception("Invalid data supplied. Could not parse incoming bag declarations.", e);
                                }

                                break;

                            default:
                                throw new Exception("Unknown section name was included in the source file.");
                        }
                    }
                }
            }

            return result;
        }
    }
}
